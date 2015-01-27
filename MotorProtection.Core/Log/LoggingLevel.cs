using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Log
{
    public enum LoggingLevel
    {
        All = -1,
        Off = 0,
        Fatal = 10,
        Error = 20,
        Warning = 30,
        Information = 40,
        Verbose = 50
    }
}
