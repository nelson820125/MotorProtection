using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaProtocal.Constant
{
    public class MODBUSFunCodes
    {
        #region MODBUS-RTU

        public static byte RTU_READ_COIL_STATUS = 0x01; // Read Coil Status

        public static byte RTU_READ_INPUT_STATUS = 0x02; // Read Input Status

        public static byte RTU_READ_HOLDING_REGISTERS = 0x03; // Read Holding Registers

        public static byte RTU_READ_INPUT_REGISTERS = 0x04; // Read Input Registers

        public static byte RTU_FORCE_SIGNLE_COIL = 0x05; // Force Single Coil

        public static byte RTU_PRESET_SIGNLE_REGISTER = 0x06; // Preset Single Register

        public static byte RTU_READ_EXCEPTION_STATUS = 0x07; // Read Exception Status

        public static byte RTU_FETCH_COMM_EVENT_CTR = 0x0B; // Fetch Comm Event Ctr #11

        public static byte RTU_FETCH_COMM_EVENT_LOG = 0x0C; // Fetch Comm Event Log #12

        public static byte RTU_FORCE_MULTI_COILS = 0x0F; // Force Multi Coils #15

        public static byte RTU_PRESET_MULTI_REGS = 0x10; // Preset Multi Regs #16

        public static byte RTU_REPORT_SLAVE_ID = 0x11; // Report Slave ID #17

        public static byte RTU_READ_GENERAL_REFERENCE = 0x14; // Read General Reference #20

        public static byte RTU_WRITE_GENERAL_REFERENCE = 0x15; // Write General Reference #21

        public static byte RTU_MASK_WRITE_4X_REGISTER = 0x16; // Mask Write 4X Register #22

        public static byte RTU_READ_WRITE_4X_REGISTERS = 0x17; // Read/Write 4X Registers #23

        public static byte RTU_READ_FIFO_QUEUE = 0x18; // Read FIFO Queue #24

        public static byte RTU_ERROR_CODE_PRE = 0x80;

        #endregion
    }
}
