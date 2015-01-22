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
            byte[] b = new byte[] { 0x01, 0x03, 0x1b, 0x8c, 0x00, 0x08 };
            MODBUSRTU modbus = new MODBUSRTU();
            Int16 f = modbus.CRC16(b);
            byte[] crc = BitConverter.GetBytes(modbus.CRC16(b));
            byte[] c = new byte[] { 0x01, 0x03, 0x1b, 0x8c, 0x00, 0x08, crc[1], crc[0] };
            Console.ReadLine();
        }
    }
}
