using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core;
using MotorProtection.Core.Log;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Constant;
using System.Threading;
using MotorProtection.Core.Cache;
using MotorProtection.Core.Controller;

namespace MotorProtection.JobManager.Controller
{
    public static class JobController
    {
        private static SerialComm _comm = new SerialComm();
        private static ProtocalController _protocalCtr = new ProtocalController();
        private static DeviceController _deviceCtr = new DeviceController();
        volatile static bool _keepReading;
        static Thread thread;

        private static void StartReading()
        {
            if (!_keepReading)
            {
                _keepReading = true;
                thread = new Thread(new ThreadStart(ReadPort));
                thread.Start();
            }
        }

        private static void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                thread.Join();
                thread = null;
            }
        }

        private static void WriteRequests()
        {
            List<DeviceConfigurationPool> pools = new List<DeviceConfigurationPool>();
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                pools = ctt.DeviceConfigurationPools.Where(p => p.Status == ConfigurationStatus.PROCESSING).ToList();
            }

            if (pools != null && pools.Count > 0)
            {
                DeviceConfigsController configCtrl = new DeviceConfigsController();
                foreach (DeviceConfigurationPool pool in pools)
                {
                    byte[] command = configCtrl.SyncSilverCommand(pool);
                    _comm.WritePort(command, 0, command.Length);

                    Thread.Sleep(500);
                    // read data from Slave.
                    byte[] readBuffer = _comm.ReadBuffer;
                    configCtrl.SyncSilverFinalize(pool, readBuffer);
                }
            }
        }

        private static void ReadPort()
        {
            while (_keepReading)
            {
                List<Device> devices = DeviceCache.GetAllDevices().Where(d => d.ParentID != null).ToList();
                if (devices.Count > 0)
                {
                    foreach (Device device in devices)
                    {
                        //deal with WRITE commands first
                        WriteRequests();

                        int attempts = 0;
                        while (true)
                        {
                            attempts++;

                            //send read register command.
                            byte[] command = _protocalCtr.ReadRegistersRequest(Convert.ToInt16(device.Address), RegisterAddresses.ProtectorStatusHi, RegisterAddresses.CurrentALo, 20);
                            _comm.WritePort(command, 0, command.Length);

                            Thread.Sleep(500); // wait for response for 500 missecond.
                            byte[] readBuffer = _comm.ReadBuffer;
                            int count = readBuffer.Length;
                            if (count > 0 && count == 45)// response structure is 1*addr | 1*func | 1*charNum | N*2values | 2*CRC, so 45 is the length of the available response data
                            {
                                //verify CRC of response data.
                                // caculate CRC
                                byte[] data = readBuffer.Take(43).ToArray();
                                Int16 crc = _protocalCtr.CalculateCRC(data);
                                byte[] readBufferCRC = readBuffer.Skip(43).Take(2).ToArray();
                                Array.Reverse(readBufferCRC);
                                if (crc == BitConverter.ToInt16(readBufferCRC, 0)) // CRC is correct
                                {
                                    _deviceCtr.ParsingDeviceStatus(readBuffer.Skip(3).Take(40).ToArray(), device.Address.Value); // just parsing values area
                                    break;
                                }
                                else // CRC is incorrect
                                {
                                    if (attempts >= AppConfig.SerialComm_Attempts)
                                    {
                                        LogController.LogError(LoggingLevel.Error, new Exception("Bad response")).Write();
                                        break;
                                    }

                                    continue;
                                }
                            }
                            else // data length is incorrect
                            {
                                if (attempts >= AppConfig.SerialComm_Attempts)
                                {
                                    LogController.LogError(LoggingLevel.Error, new Exception("Bad data. There maybe some issues on Slave - Slave ID: " + device.Address.ToString() + " and Name: " + device.Name)).Write();
                                    break;
                                }

                                continue;
                            }
                        }
                    }

                    // get status of protector every 5 seconds.
                    Thread.Sleep(5000);
                }
            }
        }

        public static void Start()
        {
            _comm.serialPort.PortName = AppConfig.SerialComm_PortName;
            _comm.serialPort.BaudRate = string.IsNullOrEmpty(AppConfig.SerialComm_BaudRate) ? 9600 : Convert.ToInt32(AppConfig.SerialComm_BaudRate);
            _comm.serialPort.DataBits = 8;
            _comm.serialPort.StopBits = System.IO.Ports.StopBits.One;
            _comm.serialPort.Parity = System.IO.Ports.Parity.None;
            _comm.serialPort.ReceivedBytesThreshold = 1;

            try
            {
                string err;
                _comm.Open(out err);

                if (!string.IsNullOrEmpty(err))
                {
                    LogController.LogError(LoggingLevel.Error).Add("Description", err).Write();
                }
                else
                {
                    _keepReading = false;
                    thread = null;
                    StartReading();
                }
            }
            catch (TimeoutException)
            {
                LogController.LogError(LoggingLevel.Error).Add("Description", "Serial Port Reading Timeout！").Write();
            }
            catch (Exception ex)
            {
                LogController.LogError(LoggingLevel.Error, ex).Add("Description", ex.Message).Write();
            }
        }

        public static void Stop()
        {
            if (_comm.IsOpen)
            {
                StopReading();
                _comm.Close();
            }
        }
    }
}
