using System;
using System.Linq;
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

        #region Command format validations

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

        /// <summary>
        /// Verify if the command format generated is correct
        /// DeviceConfigurationPool.Address = 1
        /// DeviceConfigurationPool.FunCode = 2
        /// DeviceConfigurationPool.Commands = 0x9C 0x40 0xA5 0xA5 --- 9C40 is protector's reset register
        /// </summary>
        [TestMethod]
        public void ResetSilverCommand()
        {
            DeviceConfigurationPool pool = new DeviceConfigurationPool()
            {
                Address = 1,
                FunCode = FunctionCodes.WRITE_SINGLE_REGISTER,
                Commands = "0x9C 0x40 0xA5 0xA5",
                Description = "Write one register",
                UserID = 1,
                CreateTime = DateTime.Now,
                Attempt = 0,
                Status = ConfigurationStatus.PROCESSING
            };

            byte[] result = _dcCtrl.SyncSilverCommand(pool);
            string resultStr = BitConverter.ToString(result);
            byte[] expected = new byte[] { 0x01, 0x06, 0x9C, 0x40, 0xA5, 0xA5 };
            byte[] crc = BitConverter.GetBytes(_modbus.CRC16(expected));
            byte[] expectedWithCRC = new byte[] { 0x01, 0x06, 0x9C, 0x40, 0xA5, 0xA5, crc[1], crc[0] };
            string expectedWithCRCStr = BitConverter.ToString(expectedWithCRC);

            Assert.AreEqual(expectedWithCRCStr, resultStr);
        }

        /// <summary>
        /// Verify if the command format generated is correct
        /// DeviceConfigurationPool.Address = 1
        /// DeviceConfigurationPool.FunCode = 3
        /// DeviceConfigurationPool.Commands = 0x9C 0x41 0x00 0x07 0x00 0x64 0x00 0x01 0x00 0x0A 0x00 0x0A 0x00 0x0A 0x00 0x02 0x00 0x01 --- 9C41 is the start register address
        /// addr 9C41: 100KW
        /// addr 9C42: power mode
        /// addr 9C43: 10:1
        /// addr 9C44: 10%
        /// addr 9C45: 10%
        /// addr 9C46: open at alarm
        /// addr 9C47: close at alarm
        /// </summary>
        [TestMethod]
        public void WriteConfigurationToSliverCommand()
        {
            DeviceConfigurationPool pool = new DeviceConfigurationPool()
            {
                Address = 1,
                FunCode = FunctionCodes.WRITE_MULTI_REGITERS,
                Commands = "0x9C 0x41 0x00 0x07 0x0E 0x00 0x64 0x00 0x01 0x00 0x0A 0x00 0x0A 0x00 0x0A 0x00 0x02 0x00 0x01",
                Description = "Write multi register",
                UserID = 1,
                CreateTime = DateTime.Now,
                Attempt = 0,
                Status = ConfigurationStatus.PROCESSING
            };

            byte[] result = _dcCtrl.SyncSilverCommand(pool);
            string resultStr = BitConverter.ToString(result);
            byte[] expected = new byte[] { 0x01, 0x10, 0x9C, 0x41, 0x00, 0x07, 0x0E, 0x00, 0x64, 0x00, 0x01, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x02, 0x00, 0x01 };
            byte[] crc = BitConverter.GetBytes(_modbus.CRC16(expected));
            byte[] expectedWithCRC = new byte[] { 0x01, 0x10, 0x9C, 0x41, 0x00, 0x07, 0x0E, 0x00, 0x64, 0x00, 0x01, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x02, 0x00, 0x01, crc[1], crc[0] };
            string expectedWithCRCStr = BitConverter.ToString(expectedWithCRC);

            Assert.AreEqual(expectedWithCRCStr, resultStr);
        }

        #endregion

        #region Data convert

        /// <summary>
        /// Verify if the data convertation from received bytes to DeviceConfig object is correct
        /// </summary>
        [TestMethod]
        public void ConvertNewConfigurationToDeviceConfig()
        {
            DeviceConfig config = new DeviceConfig();

            string command = "0x9C 0x41 0x00 0x07 0x0E 0x00 0x64 0x00 0x01 0x00 0x0A 0x00 0x0A 0x00 0x0A 0x00 0x02 0x00 0x01";
            string[] commands = command.Split(' ');
            byte[] updateData = new byte[commands.Length - 5];
            for (int i = 5, j = 0; i < commands.Length; i++, j++)
            {
                updateData[j] = Convert.ToByte(commands[i], 16);
            }

            _dcCtrl.ConvertNewConfigurationToDeviceConfigForTest(updateData, config);

            // power is 100 KW
            Assert.AreEqual(1m, config.ProtectPower);

            // protect mode is 1
            Assert.AreEqual(1, config.ProtectMode);

            // MI ratio is 10:1
            Assert.AreEqual(10, config.MIRatio);

            // alarm threshold is 10%
            Assert.AreEqual(0.1m, config.AlarmThreshold);

            //stop threshold is 10%
            Assert.AreEqual(0.1m, config.StopThreshold);

            //first RM mode is openning at alarm
            Assert.AreEqual(2, config.FirstRMMode);

            //second RM mode is opennning at stop
            Assert.AreEqual(1, config.SecondRMMode);
        }

        /// <summary>
        /// Verify if the data convertation from received bytes to DeviceConfig object is correct
        /// </summary>
        [TestMethod]
        public void ConvertSilverDataToDeviceConfig()
        {
            byte[] receivedData = new byte[] { 0x01, 0x03, 0x10, 0x00, 0x00, 0x00, 0x64, 0x00, 0x01, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x02, 0x00, 0x01 };
            byte[] crc = BitConverter.GetBytes(_modbus.CRC16(receivedData));
            byte[] receivedDataWithCRC = new byte[] { 0x01, 0x03, 0x10, 0x00, 0x00, 0x00, 0x64, 0x00, 0x01, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x0A, 0x00, 0x02, 0x00, 0x01, crc[1], crc[0] };

            DeviceConfig config = _dcCtrl.ConvertSilverDataToDeviceConfigForTest(receivedDataWithCRC);

            //Status is working
            Assert.AreEqual(0, config.Status);

            // power is 100 KW
            Assert.AreEqual(1m, config.ProtectPower);

            // protect mode is 1
            Assert.AreEqual(1, config.ProtectMode);

            // MI ratio is 10:1
            Assert.AreEqual(10, config.MIRatio);

            // alarm threshold is 10%
            Assert.AreEqual(0.1m, config.AlarmThreshold);

            //stop threshold is 10%
            Assert.AreEqual(0.1m, config.StopThreshold);

            //first RM mode is openning at alarm
            Assert.AreEqual(2, config.FirstRMMode);

            //second RM mode is opennning at stop
            Assert.AreEqual(1, config.SecondRMMode);
        }

        /// <summary>
        /// Verify if resetting is correct
        /// </summary>
        [TestMethod]
        public void ResetSliver()
        {
            byte[] receivedData = new byte[] { 0x01, 0x06, 0x9C, 0x40, 0x00, 0x00 };
            byte[] crc = BitConverter.GetBytes(_modbus.CRC16(receivedData));
            byte[] receivedDataWithCRC = new byte[] { 0x01, 0x06, 0x9C, 0x40, 0x00, 0x00, crc[1], crc[0] };
            Array.Reverse(receivedDataWithCRC);
            Int16 status = BitConverter.ToInt16(receivedDataWithCRC.Skip(2).Take(2).ToArray(), 0);

            //Status is working
            Assert.AreEqual(0, status);
        }

        #endregion
    }
}
