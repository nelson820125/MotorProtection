using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MotorProtection.Core
{
    public static class SysConfig
    {
        public static string ApplicationName // e.g. <add key="ApplicationName" value="Alpha App" />
        {
            get { return ConfigurationManager.AppSettings["ApplicationName"]; }
        }

        public static int CacheSyncInterval
        {
            get { return string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheSyncInterval"]) ? 5000 : int.Parse(ConfigurationManager.AppSettings["CacheSyncInterval"]); }
        }
    }
}
