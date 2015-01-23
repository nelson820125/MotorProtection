using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Data;
using AlphaProtocal.Core.MODBUS;

namespace MotorProtection.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO
            MODBUSRTU _modbus = new MODBUSRTU();
            byte[] receivedData = new byte[] { 0x01, 0x03, 0x10, 0x00, 0x00, 0x00, 0x64, 0x00, 0x01, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x02, 0x00, 0x01 };
            Int16 crc = _modbus.CRC16(receivedData);
            byte[] crcBytes = BitConverter.GetBytes(crc);
            Int16 crc1 = BitConverter.ToInt16(crcBytes, 0);
            Console.ReadLine();
        }
    }
}
