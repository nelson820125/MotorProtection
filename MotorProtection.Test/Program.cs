﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Data;
using AlphaProtocal.Core.MODBUS;
using MotorProtection.Core.Data.Entities;

namespace MotorProtection.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                SystemConfig config = new SystemConfig()
                {
                    Name = "test",
                    Value = "test"
                };
                ctt.SystemConfigs.AddObject(config);
                ctt.SaveChanges();
            }
            Console.ReadLine();
        }
    }
}
