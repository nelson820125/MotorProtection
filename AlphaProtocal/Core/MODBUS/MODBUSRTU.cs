using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaProtocal.Constant;

namespace AlphaProtocal.Core.MODBUS
{
    public class MODBUSRTU : IMODBUS
    {
        /// <summary>
        /// Read the values of Registers
        /// </summary>
        public byte[] ReadHoldingRegisters(byte address, byte[] data, int dataLen)
        {
            return RegisterOperation(address, MODBUSFunCodes.RTU_READ_HOLDING_REGISTERS, data, dataLen);
        }

        /// <summary>
        /// Preset the register
        /// </summary>
        public byte[] PresetSingleRegister(byte address, byte[] data, int dataLen)
        {
            return RegisterOperation(address, MODBUSFunCodes.RTU_PRESET_SIGNLE_REGISTER, data, dataLen);
        }

        /// <summary>
        /// Preset the register
        /// </summary>
        public byte[] PresetMultiRegisters(byte address, byte[] data, int dataLen)
        {
            return RegisterOperation(address, MODBUSFunCodes.RTU_PRESET_MULTI_REGS, data, dataLen);
        }

        /// <summary>
        /// Calculate CRC
        /// </summary>
        public Int16 CRC16(byte[] data)
        {
            byte CRCHi = 0xFF; // high byte of CRC initialized.
            byte CRCLo = 0xFF; // low byte of CRC initialized.
            int index;

            foreach (byte d in data)
            {
                index = CRCHi ^ d;
                CRCHi = (byte)(CRCLo ^ CRCTable.authCRCHi[index]);
                CRCLo = CRCTable.authCRCLo[index];
            }

            return (Int16)(CRCHi << 8 | CRCLo);
        }

        /// <summary>
        /// operate the special register using function code.
        /// </summary>
        byte[] RegisterOperation(byte address, byte funCode, byte[] data, int dataLen)
        {
            byte[] aduFrame = new byte[dataLen + 4];

            aduFrame[0] = address;
            aduFrame[1] = funCode;
            data.CopyTo(aduFrame, 2);

            byte[] crc = BitConverter.GetBytes(CRC16(aduFrame.Take(dataLen + 2).ToArray()));
            Array.Reverse(crc);
            crc.CopyTo(aduFrame, dataLen + 2);

            return aduFrame;
        }
    }
}
