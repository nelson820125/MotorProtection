using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Constant
{
    public class JobManagerKey
    {
        public const string JOB_NAME = "MotorProtection JobManager";
    }

    public enum JobOperation
    {
        Start,
        Stop,
        None
    }
}
