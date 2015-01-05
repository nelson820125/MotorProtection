using MotorProtection.Constant;
using MotorProtection.Core.Cache;
using MotorProtection.Core.Controller;
using MotorProtection.Core.Data.Entities;
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
        public delegate void EventHandle(byte[] readBuffer);
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
                                // send read register command.
                                byte[] command = _protocalCtr.ReadRegistersRequest(Convert.ToInt16(device.Address), RegisterAddresses.ProtectorStatusHi, RegisterAddresses.CurrentALo, 19);
                                WritePort(command, 0, command.Length);

                                // read data from Slave.
                                int count = serialPort.BytesToRead;
                                if (count > 0)
                                {
                                    byte[] readBuffer = new byte[count];
                                    serialPort.Read(readBuffer, 0, count);
                                    if (DataReceived != null)
                                        DataReceived(readBuffer);                                    
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
