using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Data;

namespace MotorProtection.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO
            byte a = (byte)(AlphaProtocal.Constant.MODBUSFunCodes.RTU_ERROR_CODE_PRE + AlphaProtocal.Constant.MODBUSFunCodes.RTU_READ_HOLDING_REGISTERS);
            byte b = 0x83;
            Console.WriteLine(Convert.ToString(a, 16));
            Console.WriteLine(Convert.ToString(b, 16));
            Console.ReadLine();
        }
    }
}
