using AlphaProtocal.Core.MODBUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Controller
{
    public class ProtocalController
    {
        #region MODBUS-RTU

        private MODBUSRTU _modebus = new MODBUSRTU();

        /// <summary>
        /// Create request of reading registers
        /// </summary>
        /// <returns></returns>
        public byte[] ReadRegistersRequest(Int16 address, byte startRegisterAddressHi, byte startRegisterAddressLo, Int16 registerOffset)
        {
            byte addr = BitConverter.GetBytes(address)[0];
            byte[] data = new byte[4];
            data[0] = startRegisterAddressHi;
            data[1] = startRegisterAddressLo;
            if (registerOffset <= 0)
            {
                data[2] = 0x00;
                data[3] = 0x01;
            }
            else
            {
                byte[] offset = BitConverter.GetBytes(registerOffset);
                data[2] = offset[1];
                data[3] = offset[0];
            }

            return _modebus.ReadHoldingRegisters(addr, data, 4);
        }

        /// <summary>
        /// Create request of single register writing
        /// </summary>
        /// <returns></returns>
        public byte[] WriteSingleRegisterRequest(Int16 address, byte registerAddressHi, byte registerAddressLo, byte[] sendData)
        {
            byte addr = BitConverter.GetBytes(address)[0];
            byte[] data = new byte[4];
            data[0] = registerAddressHi;
            data[1] = registerAddressLo;
            data[2] = sendData[0];
            data[3] = sendData[1];

            return _modebus.PresetSingleRegister(addr, data, 4);
        }

        /// <summary>
        /// Create request of single register writing
        /// PDU format
        /// FunCode         1byte
        /// StartAddress    2bytes
        /// RegisterNum     2bytes
        /// ByteNum         1byte -- 2*N
        /// RegisterValue   2*Nbytes
        /// </summary>
        /// <returns></returns>
        public byte[] WriteMultiRegistersRequest(Int16 address, byte registerAddressHi, byte registerAddressLo, Int16 registerOffset, byte[] sendData)
        {
            byte addr = BitConverter.GetBytes(address)[0];
            byte[] data = new byte[registerOffset * 2 + 6];
            data[0] = registerAddressHi;
            data[1] = registerAddressLo;
            byte[] offsets = BitConverter.GetBytes(registerOffset);
            data[2] = offsets[1];
            data[3] = offsets[0];
            data[4] = BitConverter.GetBytes(registerOffset * 2)[0];
            sendData.CopyTo(data, 5);

            return _modebus.PresetMultiRegisters(addr, data, data.Length);
        }

        /// <summary>
        /// Calculate CRC of data
        /// </summary>
        /// <returns></returns>
        public Int16 CalculateCRC(byte[] data)
        {
            return _modebus.CRC16(data);
        }

        #endregion
    }
}
