using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Data;
using AlphaProtocal.Core.MODBUS;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Constant;

namespace MotorProtection.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] START_ADDRESS = new byte[] { RegisterAddresses.ProtectorSettingHi, RegisterAddresses.ProtectorPowerSettingLo };
            byte[] READ_REGISTER_NUM = new byte[] { 0x00, 0x07 };
            //TODO
            string command = "";

            StringBuilder builder = new StringBuilder();
            builder.Clear();

            foreach (byte addr in START_ADDRESS)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(addr.ToString("X2"));
            }

            foreach (byte o in READ_REGISTER_NUM)
            {
                if (builder.Length != 0)
                    builder.Append(" ");
                builder.Append(o.ToString("X2"));
            }

            command = builder.ToString();

            int a = 40001;
            byte[] a1 = BitConverter.GetBytes(a);

            string str = "10.5";
            byte[] s = BitConverter.GetBytes((float)Convert.ToDecimal(str));
            Console.WriteLine(command);
            Console.ReadLine();
        }
    }
}
