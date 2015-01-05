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

            MODBUSRTU modbus = new MODBUSRTU();
            return modbus.ReadHoldingRegisters(addr, data, 4);
        }

        #endregion
    }
}
