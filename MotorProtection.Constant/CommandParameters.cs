using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Constant
{
    public class CommandParameters
    {
        public static byte[] ResetProtector = new byte[] { 0xaa, 0x55};

        public static byte[] ClearAlarm = new byte[] { 0xa5, 0xa5};

        public static int CurrentProtection = 0;

        public static int PowerProtection = 1;

        public static int RMForClosingByAlarm = 1;

        public static int RMForOpenningByAlarm = 2;

        public static int RMForClosingByStop = 3;

        public static int RMForOpenningByStop = 4;

        public static int RMClosed = 1;

        public static int RMOpenned = 0;
    }
}
