using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Cache;

namespace MotorProtection.Core
{
    public class AppConfig
    {
        #region MODBUS

        public static string SerialComm_PortName
        {
            get { return SystemConfigCache.Contains("SerialComm_PortName") ? SystemConfigCache.GetValue("SerialComm_PortName") : "COM1"; }
        }

        public static string SerialComm_BaudRate
        {
            get { return SystemConfigCache.Contains("SerialComm_BaudRate") ? SystemConfigCache.GetValue("SerialComm_BaudRate") : "9600"; }
        }

        #endregion

        #region System

        public static int SerialComm_Attempts
        {
            get { return SystemConfigCache.Contains("SerialComm_Attempts") ? Convert.ToInt32(SystemConfigCache.GetValue("SerialComm_Attempts")) : 1; }
        }

        public static string Audio_Alarm_FilePath
        {
            get { return SystemConfigCache.Contains("Audio_Alarm_FilePath") ? SystemConfigCache.GetValue("Audio_Alarm_FilePath") : ""; }
        }

        #endregion
    }
}
