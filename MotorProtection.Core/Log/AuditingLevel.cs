using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Log
{
    public enum AuditingLevel
    {
        All = -1,
        Off = 0,
        Unexpected = 10,
        High = 20,
        Medium = 30,
        Low = 40,
        Verbose = 50
    }
}
