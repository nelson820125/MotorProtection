using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int address = 40000;
            byte[] addrHex = BitConverter.GetBytes(address);
            foreach (byte a in addrHex)
            {
                Console.WriteLine(Convert.ToString(a, 16));
            }

            Console.ReadLine();
        }
    }
}
