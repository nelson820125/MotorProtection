using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Cache;

namespace MotorProtection.Core
{
    public class AppConfig
    {
        public static string SerialComm_PortName
        {
            get { return SystemConfigCache.Contains("SerialComm_PortName") ? SystemConfigCache.GetValue("SerialComm_PortName") : ""; }
        }

        public static string SerialComm_BaudRate
        {
            get { return SystemConfigCache.Contains("SerialComm_BaudRate") ? SystemConfigCache.GetValue("SerialComm_BaudRate") : ""; }
        }
    }
}
