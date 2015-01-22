using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Constant;
using MotorProtection.Core.Controller;
using AlphaProtocal.Core.MODBUS;

namespace MotorProtection.UnitTest
{
    [TestClass]
    public class DeviceConfigMock
    {
        private DeviceConfigsController _dcCtrl = new DeviceConfigsController();
        private MODBUSRTU _modbus = new MODBUSRTU();
        /// <summary>
        /// Verify if the command format generated is correct
        /// DeviceConfigurationPool.Address = 1
        /// DeviceConfigurationPool.FunCode = 3
        /// DeviceConfigurationPool.Commands = 0x00 0x01 0x00 0x01
        /// </summary>
        [TestMethod]
        public void ReadConfigurationFromSilverCommand()
        {
            DeviceConfigurationPool pool = new DeviceConfigurationPool()
            {
                Address = 1,
                FunCode = FunctionCodes.READ_REGISTERS,
                Commands = "0x00 0x01 0x00 0x01",
                Description = "Read one register",
                UserID = 1,
                CreateTime = DateTime.Now,
                Attempt = 0,
                Status = ConfigurationStatus.PROCESSING
            };

            byte[] result = _dcCtrl.SyncSilverCommand(pool);
            string resultStr = BitConverter.ToString(result);
            byte[] expected = new byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x01 };
            byte[] crc = BitConverter.GetBytes(_modbus.CRC16(expected));
            byte[] expectedWithCRC = new byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x01, crc[1], crc[0] };
            string expectedWithCRCStr = BitConverter.ToString(expectedWithCRC);

            Assert.AreEqual(expectedWithCRCStr, resultStr);
        }
    }
}
