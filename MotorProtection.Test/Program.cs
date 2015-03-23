using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Data;
using AlphaProtocal.Core.MODBUS;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Constant;
using MotorProtection.Core.Cache;
using MotorProtection.Core.Log;
using MotorProtection.Core;
using MotorProtection.Core.Controller;
using System.Threading;

namespace MotorProtection.Test
{
    class Program
    {
        private static SerialComm _comm = new SerialComm();
        private static ProtocalController _protocalCtrl = new ProtocalController();
        private static ProtocalController _protocalCtr = new ProtocalController();
        private static DeviceController _deviceCtr = new DeviceController();

        static void Main(string[] args)
        {
            //byte[] START_ADDRESS = new byte[] { RegisterAddresses.ProtectorSettingHi, RegisterAddresses.ProtectorPowerSettingLo };
            //byte[] READ_REGISTER_NUM = new byte[] { 0x00, 0x07 };
            ////TODO
            //string command = "";

            //StringBuilder builder = new StringBuilder();
            //builder.Clear();

            //foreach (byte addr in START_ADDRESS)
            //{
            //    if (builder.Length != 0)
            //        builder.Append(" ");
            //    builder.Append(addr.ToString("X2"));
            //}

            //foreach (byte o in READ_REGISTER_NUM)
            //{
            //    if (builder.Length != 0)
            //        builder.Append(" ");
            //    builder.Append(o.ToString("X2"));
            //}

            //command = builder.ToString();

            //int a = 40001;
            //byte[] a1 = BitConverter.GetBytes(a);

            //string str = "10.5";
            //byte[] s = BitConverter.GetBytes((float)Convert.ToDecimal(str));
            //Console.WriteLine(command);

            //try
            //{

            //CacheController.Initialize();
            //}
            //catch (Exception ex)
            //{
            //    string str = ex.Message;
            //}
            

            //StartPort();

            DeviceConfigurationPool pool = new DeviceConfigurationPool();
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                pool.Address = 1;
                pool.FunCode = FunctionCodes.READ_REGISTERS;
                pool.Commands = "9C 40 00 08";
                pool.Description = "";
                pool.UserID = 1;
                pool.CreateTime = DateTime.Now;
                pool.Attempt = 0;
                pool.Status = ConfigurationStatus.PROCESSING;
                pool.JobRemovable = false;

                ctt.DeviceConfigurationPools.AddObject(pool);
                ctt.SaveChanges();
            }

            //LogController.LogEvent(AuditingLevel.High, "Service", "Started").Write();
            //LogController.LogError(LoggingLevel.Error, new Exception("Bad data. There maybe some issues on Slave - Slave ID: ")).Write();

            //decimal test = 32.01m;
            //Console.WriteLine(test.ToString());
            //byte addr = 0x01;
            //byte func = 0x03;
            //byte[] data = new byte[] { 0x01, 0x03, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE4, 0x59 };
            //ReadSliverConfig(data);
            //MODBUSRTU modubus = new MODBUSRTU();
            //byte[] result = modubus.PresetMultiRegisters(addr, data, data.Length);

            Console.ReadLine();

            //ProtocalController ctrl = new ProtocalController();
            //byte[] result = ctrl.ReadRegistersRequest(1, 0x75, 0x30, 20);
        }

        private static void ReadSliverConfig(byte[] receivedData)
        {
            byte errorCode = (byte)(AlphaProtocal.Constant.MODBUSFunCodes.RTU_ERROR_CODE_PRE + AlphaProtocal.Constant.MODBUSFunCodes.RTU_READ_HOLDING_REGISTERS);
            bool isSuccess = true;
            if (receivedData.Length > 0)
            {
                if (receivedData[1] == errorCode) // return errors. and log exception code.
                {
                    LogController.LogError(LoggingLevel.Error).Add("Description", "Reading sliver's configuration is failed! the exception code: " + Convert.ToString(receivedData[2], 16)).Write();
                    //InvalidResponse(config);
                    isSuccess = false;
                }
                else
                {
                    byte[] data = receivedData.Take(19).ToArray();
                    Int16 crc = _protocalCtrl.CalculateCRC(data);
                    byte[] receivedDataCRC = receivedData.Skip(19).Take(2).ToArray();
                    Array.Reverse(receivedDataCRC);
                    if (crc == BitConverter.ToInt16(receivedDataCRC, 0)) // CRC is correct.
                    {
                        var newConfig = ConvertSilverDataToDeviceConfigsLog(receivedData);
                        isSuccess = UpdateDeviceConfigsLog(1, newConfig);
                    }
                }
            }
        }

        private static bool UpdateDeviceConfigsLog(int address, DeviceConfigsLog newConfig)
        {
            bool isSuccess = false;
            if (newConfig != null)
            {
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    var configLog = ctt.DeviceConfigsLogs.Where(dcl => dcl.ID == address).FirstOrDefault();
                    if (configLog != null)
                    {
                        configLog.Status = newConfig.Status;
                        configLog.ProtectPower = newConfig.ProtectPower;
                        configLog.ProtectMode = newConfig.ProtectMode;
                        configLog.MIRatio = newConfig.MIRatio;
                        configLog.AlarmThreshold = newConfig.AlarmThreshold;
                        configLog.StopThreshold = newConfig.StopThreshold;
                        configLog.FirstRMMode = newConfig.FirstRMMode;
                        configLog.SecondRMMode = newConfig.SecondRMMode;
                    }
                    else
                    {
                        newConfig.ID = address;
                        ctt.DeviceConfigsLogs.AddObject(newConfig);
                    }

                    ctt.SaveChanges();

                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        private static DeviceConfigsLog ConvertSilverDataToDeviceConfigsLog(byte[] receivedData)
        {
            byte[] dataBuffer = receivedData.Skip(3).Take(16).ToArray();
            Array.Reverse(dataBuffer);

            DeviceConfigsLog config = new DeviceConfigsLog()
            {
                SecondRMMode = BitConverter.ToUInt16(dataBuffer.Take(2).ToArray(), 0),
                FirstRMMode = BitConverter.ToUInt16(dataBuffer.Skip(2).Take(2).ToArray(), 0),
                StopThreshold = BitConverter.ToUInt16(dataBuffer.Skip(4).Take(2).ToArray(), 0),
                AlarmThreshold = BitConverter.ToUInt16(dataBuffer.Skip(6).Take(2).ToArray(), 0),
                MIRatio = BitConverter.ToUInt16(dataBuffer.Skip(8).Take(2).ToArray(), 0),
                ProtectMode = BitConverter.ToUInt16(dataBuffer.Skip(10).Take(2).ToArray(), 0),
                ProtectPower = (decimal)((double)BitConverter.ToUInt16(dataBuffer.Skip(12).Take(2).ToArray(), 0) / 100),
                Status = BitConverter.ToUInt16(dataBuffer.Skip(14).Take(2).ToArray(), 0)
            };
            return config;
        }

        private static void ReadPort()
        {
            while (true)
            {
                List<Device> devices = DeviceCache.GetAllDevices().Where(d => d.ParentID != null).ToList();
                if (devices.Count > 0)
                {
                    foreach (Device device in devices)
                    {
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

        private static void StartPort()
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
                    ReadPort();
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
                _comm.Close();
        }

        /// <summary>
        /// Get buffer data at the port to parse
        /// </summary>
        private static void serialPort_DataReceived(byte[] readBuffer, int address)
        {
            if (readBuffer != null)
            {
                // update device information in DB
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    Device device = ctt.Devices.Where(d => d.Address == address).FirstOrDefault();
                    int alarmSec = 0, alarmMin = 0, alarmHr = 0, alarmDay = 0, alarmMon = 0, alarmYear = 0, stopSec = 0, stopMin = 0, stopHr = 0, stopDay = 0, stopMon = 0, stopYear = 0;
                    int count = 0;
                    for (int i = 0; i < readBuffer.Length; i = i + 2)
                    {
                        count++;

                        //parsing received data.
                        byte[] data = new byte[2];
                        data[0] = readBuffer[i];
                        data[1] = readBuffer[i + 1];
                        switch (count)
                        {
                            case 1: // current A value
                                Array.Reverse(data);
                                device.CurrentA = (decimal)((double)BitConverter.ToUInt16(data, 0) / 100);
                                break;
                            case 2: // current B value
                                Array.Reverse(data);
                                device.CurrentB = (decimal)((double)BitConverter.ToUInt16(data, 0) / 100);
                                break;
                            case 3: // current C value
                                Array.Reverse(data);
                                device.CurrentC = (decimal)((double)BitConverter.ToUInt16(data, 0) / 100);
                                break;
                            case 4: // Voltage A value
                                Array.Reverse(data);
                                device.VoltageA = (decimal)((double)BitConverter.ToUInt16(data, 0) / 100);
                                break;
                            case 5: // Voltage B value
                                Array.Reverse(data);
                                device.VoltageB = (decimal)((double)BitConverter.ToUInt16(data, 0) / 100);
                                break;
                            case 6: // Voltage C value
                                Array.Reverse(data);
                                device.VoltageC = (decimal)((double)BitConverter.ToUInt16(data, 0) / 100);
                                break;
                            case 7: // Power value
                                Array.Reverse(data);
                                device.Power = (decimal)((double)BitConverter.ToUInt16(data, 0) / 100);
                                break;
                            case 8: // alarm second and minute value
                                byte[] secBytes = new byte[] { data[0], 0x00 };
                                byte[] minBytes = new byte[] { data[1], 0x00 };
                                alarmSec = BitConverter.ToInt16(secBytes, 0);
                                alarmMin = BitConverter.ToInt16(minBytes, 0);
                                break;
                            case 9: // alarm hour and day value
                                byte[] hrBytes = new byte[] { data[0], 0x00 };
                                byte[] dayBytes = new byte[] { data[1], 0x00 };
                                alarmHr = BitConverter.ToInt16(hrBytes, 0);
                                alarmDay = BitConverter.ToInt16(dayBytes, 0);
                                break;
                            case 10: // alarm month and year value
                                byte[] monBytes = new byte[] { data[0], 0x00 };
                                byte[] yearBytes = new byte[] { data[1], 0x00 };
                                alarmMon = BitConverter.ToInt16(monBytes, 0);
                                alarmYear = BitConverter.ToInt16(yearBytes, 0);
                                break;
                            case 11: // stop second and minute value
                                byte[] stopSecBytes = new byte[] { data[0], 0x00 };
                                byte[] stopMinBytes = new byte[] { data[1], 0x00 };
                                stopSec = BitConverter.ToInt16(stopSecBytes, 0);
                                stopMin = BitConverter.ToInt16(stopMinBytes, 0);
                                break;
                            case 12: // stop hour and day value
                                byte[] stopHrBytes = new byte[] { data[0], 0x00 };
                                byte[] stopDayBytes = new byte[] { data[1], 0x00 };
                                stopHr = BitConverter.ToInt16(stopHrBytes, 0);
                                stopDay = BitConverter.ToInt16(stopDayBytes, 0);
                                break;
                            case 13: // stop month and year value
                                byte[] stopMonBytes = new byte[] { data[0], 0x00 };
                                byte[] stopYearBytes = new byte[] { data[1], 0x00 };
                                stopMon = BitConverter.ToInt16(stopMonBytes, 0);
                                stopYear = BitConverter.ToInt16(stopYearBytes, 0);
                                break;
                            case 14: // Temperature A value
                                Array.Reverse(data);
                                device.TemperatureA = (decimal)((double)BitConverter.ToInt16(data, 0) / 100);
                                break;
                            case 15: // Temperature B value
                                Array.Reverse(data);
                                device.TemperatureB = (decimal)((double)BitConverter.ToInt16(data, 0) / 100);
                                break;
                            case 16: // Temperature C value
                                Array.Reverse(data);
                                device.TemperatureC = (decimal)((double)BitConverter.ToInt16(data, 0) / 100);
                                break;
                            case 17: // Temperature value
                                Array.Reverse(data);
                                device.Temperature = (decimal)((double)BitConverter.ToInt16(data, 0) / 100);
                                break;
                            case 18:
                                Array.Reverse(data);
                                device.FirstRMStatus = BitConverter.ToInt16(data, 0) == 1 ? true : false;
                                break;
                            case 19:
                                Array.Reverse(data);
                                device.SecondRMStatus = BitConverter.ToInt16(data, 0) == 1 ? true : false;
                                break;
                            case 20:
                                Array.Reverse(data);
                                Int16 status = BitConverter.ToInt16(data, 0);
                                if (status == 0) // 0 - working
                                    device.Status = ProtectorStatus.Normal;
                                else if (status == 15) // 0x0f - alarm
                                    device.Status = ProtectorStatus.Alarm;
                                else if (status == 255) // 0xff - stopped
                                    device.Status = ProtectorStatus.Stopped;
                                break;
                        }
                    }

                    // set alarm and stop time to device object
                    int currentYear = DateTime.Now.Year;
                    alarmYear = (alarmYear + 2000) > currentYear ? alarmYear + 1900 : alarmYear + 2000;
                    stopYear = (stopYear + 2000) > currentYear ? stopYear + 1900 : stopYear + 2000;

                    device.AlarmAt = new DateTime(alarmYear, alarmMon, alarmDay, alarmHr, alarmMin, alarmSec);
                    device.StopAt = new DateTime(stopYear, stopMon, stopDay, stopHr, stopMin, stopSec);

                    device.UpdateTime = DateTime.Now;

                    ctt.SaveChanges();
                }
            }
        }
    }
}
