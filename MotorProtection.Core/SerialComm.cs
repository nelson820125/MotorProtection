using MotorProtection.Constant;
using MotorProtection.Core.Cache;
using MotorProtection.Core.Controller;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Core.Log;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotorProtection.Core
{
    public class SerialComm
    {
        public delegate void EventHandle(byte[] readBuffer, int address);
        public event EventHandle DataReceived;
        public ProtocalController _protocalCtr = new ProtocalController();

        public SerialPort serialPort;
        Thread thread;
        volatile bool _keepReading;

        public SerialComm()
        {
            serialPort = new SerialPort();
            thread = null;
            _keepReading = false;
        }

        public SerialComm(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            thread = null;
            _keepReading = false;
        }

        public bool IsOpen
        {
            get {
                return serialPort.IsOpen;
            }
        }

        private void StartReading()
        {
            if (!_keepReading)
            {
                _keepReading = true;
                thread = new Thread(new ThreadStart(ReadPort));
                thread.Start();
            }
        }

        private void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                thread.Join();
                thread = null;
            }
        }

        private void ReadPort()
        {
            while (_keepReading)
            {
                if (serialPort.IsOpen)
                {
                    try
                    {
                        List<Device> devices = DeviceCache.GetAllDevices();
                        if (devices.Count > 0)
                        {
                            foreach (Device device in devices)
                            {
                                int attempts = 0;
                                while (true)
                                {
                                    attempts++;

                                    // send read register command.
                                    byte[] command = _protocalCtr.ReadRegistersRequest(Convert.ToInt16(device.Address), RegisterAddresses.ProtectorStatusHi, RegisterAddresses.CurrentALo, 20);
                                    WritePort(command, 0, command.Length);

                                    // read data from Slave.
                                    int count = serialPort.BytesToRead;
                                    if (count > 0 && count != 44) // response structure is 1*addr | 1*func | 1*charNum | N*2values | 1*CRC, so 42 is the length of the available response data
                                    {
                                        byte[] readBuffer = new byte[count];
                                        serialPort.Read(readBuffer, 0, count);

                                        // verify CRC of response data.
                                        // caculate CRC
                                        byte[] data = readBuffer.Take(43).ToArray();
                                        byte crc = _protocalCtr.CalculateCRC(data);
                                        if (crc == readBuffer.Last()) // CRC is correct
                                        {
                                            if (DataReceived != null)
                                                DataReceived(readBuffer.Skip(3).Take(40).ToArray(), device.Address.Value); // just parsing values area
                                        }
                                        else // CRC is incorrect
                                        {
                                            if (attempts >= AppConfig.SerialComm_Attempts)
                                            {
                                                LogController.LogError(LoggingLevel.Level1, new Exception("Bad response")).Write();
                                                break;
                                            }

                                            continue;
                                        }
                                    }
                                    else // data length is incorrect
                                    {
                                        if (attempts >= AppConfig.SerialComm_Attempts)
                                        {
                                            LogController.LogError(LoggingLevel.Level1, new Exception("Bad data. There maybe some issues on Slave - Slave ID: " + device.Address.ToString() + " and Name: " + device.Name)).Write();
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
                    catch (TimeoutException)
                    {
                        throw new Exception("Serial Port Reading Timeout！");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }                    
                }
            }
        }

        public void Open(out string err)
        {
            Close();
            serialPort.Open();
            err = "";
            if (serialPort.IsOpen)
            {
                StartReading();
            }
            else
            {
                err = "Serail Port Openning failed！";
            }
        }

        public void Close()
        {
            StopReading();
            serialPort.Close();
        }

        public void WritePort(byte[] send, int offSet, int count)
        {
            if (IsOpen)
            {
                serialPort.Write(send, offSet, count);
            }
        }
    }
}
