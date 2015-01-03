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
            using (MotorProtection.Core.Data.Entities.MotorProtectorEntities entity = new Core.Data.Entities.MotorProtectorEntities())
            {
                var user = entity.Users.FirstOrDefault();
                Console.WriteLine(user.UserName);
                Console.WriteLine(user.Password);
            }
            Console.ReadLine();
        }
    }
}
