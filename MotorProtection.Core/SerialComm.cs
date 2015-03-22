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
        private List<byte> bufferTemp = new List<byte>();

        public SerialPort serialPort;

        public SerialComm()
        {
            serialPort = new SerialPort();
        }

        public SerialComm(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }

        public bool IsOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }

        public byte[] ReadBuffer
        {
            get {
                return bufferTemp.ToArray();
            }
        }

        public void Open(out string err)
        {
            Close();
            serialPort.Open();
            err = "";
            if (!serialPort.IsOpen)
            {
                err = "Serail Port Openning failed！";
                return;
            }

            serialPort.DataReceived += serialPort_DataReceived;
        }
        
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int count = serialPort.BytesToRead;
            if (count > 0)
            {
                byte[] buffer = new byte[count];
                serialPort.Read(buffer, 0, count);
                bufferTemp.AddRange(buffer);
            }
        }

        public void Close()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        public void WritePort(byte[] send, int offSet, int count)
        {
            if (IsOpen)
            {
                bufferTemp.Clear();
                serialPort.Write(send, offSet, count);
            }
        }
    }
}
