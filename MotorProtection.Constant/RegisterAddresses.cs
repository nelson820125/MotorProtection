using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Constant
{
    public class RegisterAddresses
    {
        #region the address of settings of protector

        public static byte ProtectorSettingHi = 0x9C; // the high 8-bit address of settings of protector.

        public static byte ProtectorStatusResetLo = 0x40; // the low 8-bit address of resetting the status of protector.

        public static byte ProtectorPowerSettingLo = 0x41; // the low 8-bit address of power setting of protector.

        public static byte ProtectModeLo = 0x42; // the mode of protection

        public static byte MIRatioLo = 0x43; // mutual inductor ratio

        public static byte AlarmThresholdLo = 0x44; // the threshold of alarm

        public static byte StopThresholdLo = 0x45; // the threshold of stop

        public static byte FirstRMMode = 0x46; // #1 relay mode

        public static byte SecondRMMode = 0x47; // #2 relay mode

        #endregion

        #region the address of status of protector

        public static byte ProtectorStatusHi = 0x75; // the high 8-bit address of status of protector

        public static byte CurrentALo = 0x30; // Current-A

        public static byte CurrentBLo = 0x31; // Current-B

        public static byte CurrentCLo = 0x32; // Current-C

        public static byte VoltageALo = 0x33; // Voltage-A

        public static byte VoltageBLo = 0x34; // Voltage-B

        public static byte VoltageCLo = 0x35; // Voltage-C

        public static byte PowerLo = 0x36; // power

        public static byte AlarmSecMinLo = 0x37; // the second and minute of alarm

        public static byte AlarmHrDayLo = 0x38; // the hour and day of alarm

        public static byte AlarmMonYearLo = 0x39; // the month and low-2-year of alarm

        public static byte StopSecMinLo = 0x37; // the second and minute of stop

        public static byte StopHrDayLo = 0x3B; // the hour and day of stop

        public static byte StopMonYearLo = 0x3C; // the month and low-2-year of stop 

        public static byte TemperatureALo = 0x3D; // temperature-A

        public static byte TemperatureBLo = 0x3E; // temperature-B

        public static byte TemperatureCLo = 0x3F; // temperature-C

        public static byte TemperatureLo = 0x40; // temperature

        public static byte FirstRMStatus = 0x41; // the status of RM #1

        public static byte SecondRMStatus = 0x42; // the status of RM #2

        #endregion
    }
}
