using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaProtocal.Core.MODBUS
{
    interface IMODBUS
    {
        /// <summary>
        /// Read Registers
        /// </summary>
        byte[] ReadHoldingRegisters(byte address, byte[] data, int dataLen);

        /// <summary>
        /// Preset the register
        /// </summary>
        byte[] PresetSingleRegister(byte address, byte[] data, int dataLen);

        /// <summary>
        /// Calculate CRC
        /// </summary>
        byte CRC16(byte[] data);
    }
}
