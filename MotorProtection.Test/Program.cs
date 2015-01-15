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
            string str = "0x0F";
            Console.WriteLine(Convert.ToInt16(str, 16).ToString());
            Console.ReadLine();
        }
    }
}
