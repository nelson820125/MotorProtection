using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Constant;
using MotorProtection.Core.Log;

namespace MotorProtection.Core.Controller
{
    public class DeviceConfigsController
    {
        #region Properties

        private ProtocalController _protocalCtrl = new ProtocalController();

        #endregion

        /// <summary>
        /// Add new configuration to pool
        /// </summary>
        public void AddDeviceConfiguration(DeviceConfigurationPool newConfig)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                ctt.DeviceConfigurationPools.AddObject(newConfig);
                ctt.SaveChanges();
            }
        }

        /// <summary>
        /// Update attempt of configuration in pool
        /// </summary>
        public void UpdateAttempt(int configId)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var config = ctt.DeviceConfigurationPools.Where(dc => dc.ID == configId).FirstOrDefault();
                if (config != null)
                {
                    config.Attempt = config.Attempt + 1;
                    ctt.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Delete the special configuration from pool
        /// </summary>
        public void DeleteDeviceConfigurationFromPool(int configId)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var config = ctt.DeviceConfigurationPools.Where(dc => dc.ID == configId).FirstOrDefault();
                if (config != null)
                {
                    ctt.DeviceConfigurationPools.DeleteObject(config);
                    ctt.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Move configuration from pool to log table if sync to silver successfully.
        /// </summary>
        public void UpdatePoolAfterSuccess(int configId)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var config = ctt.DeviceConfigurationPools.Where(dc => dc.ID == configId).FirstOrDefault();
                if (config != null)
                {
                    var configLog = new DeviceConfigurationLog()
                    {
                        Address = config.Address,
                        FunCode = config.FunCode,
                        Commands = config.Commands,
                        Description = config.Description,
                        UserID = config.UserID,
                        Status = ConfigurationStatus.SUCCESS,
                        CreateTime = DateTime.Now
                    };

                    ctt.DeviceConfigurationLogs.AddObject(configLog);
                    ctt.DeviceConfigurationPools.DeleteObject(config);
                    ctt.SaveChanges();
                }
            }
        }

        public void AddDeviceConfigurationLog(DeviceConfigurationLog log)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                ctt.DeviceConfigurationLogs.AddObject(log);
                ctt.SaveChanges();
            }
        }

        /// <summary>
        /// Create sync command.
        /// </summary>
        public byte[] SyncSilverCommand(DeviceConfigurationPool config)
        {
            byte[] command = null;

            switch (config.FunCode)
            {
                case FunctionCodes.READ_REGISTERS:
                    command = ReadConfigurationFromSilverCommand(config);
                    break;
                case FunctionCodes.WRITE_SINGLE_REGISTER:
                    command = ResetSilverCommand(config);
                    break;
                case FunctionCodes.WRITE_MULTI_REGITERS:
                    command = WriteConfigurationToSliverCommand(config);
                    break;
            }

            return command;
        }

        public void SyncSilverFinalize(DeviceConfigurationPool config, byte[] receivedData)
        {
            switch (config.FunCode)
            {
                case FunctionCodes.READ_REGISTERS:
                    SyncSilverSettingsToDeviceConfig(config, receivedData);
                    break;
                case FunctionCodes.WRITE_SINGLE_REGISTER:
                    ResetSilver(config, receivedData);
                    break;
                case FunctionCodes.WRITE_MULTI_REGITERS:
                    SyncDeviceConfigToSilver(config, receivedData);
                    break;
            }
        }

        private byte[] ReadConfigurationFromSilverCommand(DeviceConfigurationPool config)
        {
            Int16 addr = Convert.ToInt16(config.Address);
            string[] commands = config.Commands.Split(' ');
            byte registerAddrHi = Convert.ToByte(commands[0], 16);
            byte registerAddrLow = Convert.ToByte(commands[1], 16);
            byte[] offsetBytes = new byte[] { Convert.ToByte(commands[2], 16), Convert.ToByte(commands[3], 16) };
            Array.Reverse(offsetBytes);
            Int16 offset = BitConverter.ToInt16(offsetBytes, 0);

            return _protocalCtrl.ReadRegistersRequest(addr, registerAddrHi, registerAddrLow, offset);
        }

        /// <summary>
        /// Reset register
        /// </summary>
        /// <returns></returns>
        private byte[] ResetSilverCommand(DeviceConfigurationPool config)
        {
            Int16 addr = Convert.ToInt16(config.Address);
            string[] commands = config.Commands.Split(' ');
            byte registerAddrHi = Convert.ToByte(commands[0], 16);
            byte registerAddrLow = Convert.ToByte(commands[1], 16);
            byte[] data = new byte[] { Convert.ToByte(commands[2], 16), Convert.ToByte(commands[3], 16) };

            return _protocalCtrl.WriteSingleRegisterRequest(addr, registerAddrHi, registerAddrLow, data);
        }

        /// <summary>
        /// Write commands to multi registers
        /// </summary>
        /// <returns></returns>
        private byte[] WriteConfigurationToSliverCommand(DeviceConfigurationPool config)
        {
            Int16 addr = Convert.ToInt16(config.Address);
            string[] commands = config.Commands.Split(' ');
            byte registerAddrHi = Convert.ToByte(commands[0], 16);
            byte registerAddrLow = Convert.ToByte(commands[1], 16);
            byte[] offsetBytes = new byte[] { Convert.ToByte(commands[2], 16), Convert.ToByte(commands[3], 16) };
            Array.Reverse(offsetBytes);
            Int16 offset = BitConverter.ToInt16(offsetBytes, 0);
            byte[] data = new byte[commands.Length - 5];
            for (int i = 5, j = 0; i < commands.Length; i++, j++)
            {
                data[j] = Convert.ToByte(commands[i], 16);
            }

            return _protocalCtrl.WriteMultiRegistersRequest(addr, registerAddrHi, registerAddrLow, offset, data);
        }

        private void InvalidResponse(DeviceConfigurationPool config)
        {
            if (config.Attempt < AppConfig.SerialComm_Attempts)
            {
                UpdateAttempt(config.ID);
            }
            else
            {
                //move data to log table, and delete from pool table.
                config.Status = ConfigurationStatus.ERROR;
                DeviceConfigurationLog log = new DeviceConfigurationLog()
                {
                    Address = config.Address,
                    FunCode = config.FunCode,
                    Commands = config.Commands,
                    Description = config.Description,
                    UserID = config.UserID,
                    CreateTime = DateTime.Now,
                    Status = config.Status,
                };

                AddDeviceConfigurationLog(log);
                DeleteDeviceConfigurationFromPool(config.ID);
            }
        }

        private DeviceConfig ConvertSilverDataToDeviceConfig(byte[] receivedData)
        {
            byte[] dataBuffer = receivedData.Skip(3).Take(16).ToArray();
            Array.Reverse(dataBuffer);

            DeviceConfig config = new DeviceConfig()
            {
                SecondRMMode = BitConverter.ToInt16(dataBuffer.Take(2).ToArray(), 0),
                FirstRMMode = BitConverter.ToInt16(dataBuffer.Skip(2).Take(2).ToArray(), 0),
                StopThreshold = (decimal)BitConverter.ToInt16(dataBuffer.Skip(4).Take(2).ToArray(), 0) / 100,
                AlarmThreshold = (decimal)BitConverter.ToInt16(dataBuffer.Skip(6).Take(2).ToArray(), 0) / 100,
                MIRatio = BitConverter.ToInt16(dataBuffer.Skip(8).Take(2).ToArray(), 0),
                ProtectMode = BitConverter.ToInt16(dataBuffer.Skip(10).Take(2).ToArray(), 0),
                ProtectPower = (decimal)BitConverter.ToInt16(dataBuffer.Skip(12).Take(2).ToArray(), 0) / 100,
                Status = BitConverter.ToInt16(dataBuffer.Skip(14).Take(2).ToArray(), 0)
            };
            return config;
        }

        #region Sync from Sliver to Databse

        /// <summary>
        /// update the related config data against new parsing configuration.
        /// </summary>
        private bool UpdateDeviceConfig(int address, DeviceConfig newConfig)
        {
            bool isSuccess = false;
            if (newConfig != null)
            {
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    var device = ctt.Devices.Where(d => d.Address == address).FirstOrDefault();
                    if (device != null)
                    {
                        var deviceConfig = ctt.DeviceConfigs.Where(dc => dc.DeviceID == device.DeviceID).FirstOrDefault();
                        if (deviceConfig != null)
                        {
                            deviceConfig.Status = newConfig.Status;
                            deviceConfig.ProtectPower = newConfig.ProtectPower;
                            deviceConfig.ProtectMode = newConfig.ProtectMode;
                            deviceConfig.MIRatio = newConfig.MIRatio;
                            deviceConfig.AlarmThreshold = newConfig.AlarmThreshold;
                            deviceConfig.StopThreshold = newConfig.StopThreshold;
                            deviceConfig.FirstRMMode = newConfig.FirstRMMode;
                            deviceConfig.SecondRMMode = newConfig.SecondRMMode;
                            ctt.SaveChanges();

                            isSuccess = true;
                        }
                    }
                }
            }
            return isSuccess;
        }

        private DeviceConfig GetDeviceConfigByAddress(int address)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var device = ctt.Devices.Where(d => d.Address == address).FirstOrDefault();
                if (device != null)
                {
                    var deviceConfig = ctt.DeviceConfigs.Where(dc => dc.DeviceID == device.DeviceID).FirstOrDefault();
                    return deviceConfig;
                }
                else
                {
                    return null;
                }
            }
        }

        private void ConvertNewConfigurationToDeviceConfig(byte[] updateData, DeviceConfig newConfig)
        {
            Array.Reverse(updateData);

            newConfig.SecondRMMode = BitConverter.ToInt16(updateData.Take(2).ToArray(), 0);
            newConfig.FirstRMMode = BitConverter.ToInt16(updateData.Skip(2).Take(2).ToArray(), 0);
            newConfig.StopThreshold = (decimal)BitConverter.ToInt16(updateData.Skip(4).Take(2).ToArray(), 0) / 100;
            newConfig.AlarmThreshold = (decimal)BitConverter.ToInt16(updateData.Skip(6).Take(2).ToArray(), 0) / 100;
            newConfig.MIRatio = BitConverter.ToInt16(updateData.Skip(8).Take(2).ToArray(), 0);
            newConfig.ProtectMode = BitConverter.ToInt16(updateData.Skip(10).Take(2).ToArray(), 0);
            newConfig.ProtectPower = (decimal)BitConverter.ToInt16(updateData.Skip(12).Take(2).ToArray(), 0) / 100;
        }

        /// <summary>
        /// Verify received data and update the database
        /// </summary>
        private void SyncSilverSettingsToDeviceConfig(DeviceConfigurationPool config, byte[] receivedData)
        {
            byte errorCode = (byte)(AlphaProtocal.Constant.MODBUSFunCodes.RTU_ERROR_CODE_PRE + AlphaProtocal.Constant.MODBUSFunCodes.RTU_READ_HOLDING_REGISTERS);
            bool isSuccess = true;

            if (receivedData.Length > 0)
            {
                if (receivedData[1] == errorCode) // return errors. and log exception code.
                {
                    LogController.LogError(LoggingLevel.Error).Add("Description", "Reading sliver's configuration is failed! the exception code: " + Convert.ToString(receivedData[2], 16)).Write();
                    InvalidResponse(config);
                    isSuccess = false;
                }
                else
                {
                    byte[] data = receivedData.Take(19).ToArray();
                    Int16 crc = _protocalCtrl.CalculateCRC(data);
                    byte[] receivedDataCRC = receivedData.Skip(19).Take(2).ToArray();
                    Array.Reverse(receivedDataCRC);
                    if (crc == BitConverter.ToInt16(receivedDataCRC, 0)) // CRC is correct.
                    {
                        var newConfig = ConvertSilverDataToDeviceConfig(receivedData);
                        isSuccess = UpdateDeviceConfig(config.Address, newConfig);

                        if (!isSuccess)
                        {
                            LogController.LogError(LoggingLevel.Error).Add("Description", "There is no configuration of Address: " + config.Address).Write();
                            DeleteDeviceConfigurationFromPool(config.ID);
                        }
                    }
                    else
                    {
                        LogController.LogError(LoggingLevel.Error).Add("Description", "Bad response").Write();
                        InvalidResponse(config);
                        isSuccess = false;
                    }
                }
            }
            else
            {
                LogController.LogError(LoggingLevel.Error).Add("Description", "Invalid response: No response data").Write();
                InvalidResponse(config);
                isSuccess = false;
            }

            if (isSuccess)
            {
                UpdatePoolAfterSuccess(config.ID);
            }
        }

        #endregion

        #region Sync from Databse to Sliver

        private void ResetSilver(DeviceConfigurationPool config, byte[] receivedData)
        {
            byte errorCode = (byte)(AlphaProtocal.Constant.MODBUSFunCodes.RTU_ERROR_CODE_PRE + AlphaProtocal.Constant.MODBUSFunCodes.RTU_PRESET_SIGNLE_REGISTER);
            bool isSuccess = true;

            if (receivedData.Length > 0)
            {
                if (receivedData[1] == errorCode) // return errors. and log exception code.
                {
                    LogController.LogError(LoggingLevel.Error).Add("Description", "Writing sliver's configuration is failed! the exception code: " + Convert.ToString(receivedData[2], 16)).Write();
                    InvalidResponse(config);
                    isSuccess = false;
                }
                else
                {
                    byte[] data = receivedData.Take(6).ToArray();
                    Int16 crc = _protocalCtrl.CalculateCRC(data);
                    byte[] receivedDataCRC = receivedData.Skip(6).Take(2).ToArray();
                    Array.Reverse(receivedDataCRC);
                    if (crc == BitConverter.ToInt16(receivedDataCRC, 0)) // CRC is correct.
                    {
                        var newConfig = GetDeviceConfigByAddress(config.Address);
                        Array.Reverse(receivedData);
                        newConfig.Status = BitConverter.ToInt16(receivedData.Skip(2).Take(2).ToArray(), 0);
                        isSuccess = UpdateDeviceConfig(config.Address, newConfig);

                        if (!isSuccess)
                        {
                            LogController.LogError(LoggingLevel.Error).Add("Description", "There is no configuration of Address: " + config.Address).Write();
                            DeleteDeviceConfigurationFromPool(config.ID);
                        }
                    }
                    else
                    {
                        LogController.LogError(LoggingLevel.Error).Add("Description", "Bad response").Write();
                        InvalidResponse(config);
                        isSuccess = false;
                    }
                }
            }
            else
            {
                LogController.LogError(LoggingLevel.Error).Add("Description", "Invalid response: No response data").Write();
                InvalidResponse(config);
                isSuccess = false;
            }

            if (isSuccess)
            {
                UpdatePoolAfterSuccess(config.ID);
            }
        }

        /// <summary>
        /// sync multi-registers
        /// </summary>
        private void SyncDeviceConfigToSilver(DeviceConfigurationPool config, byte[] receivedData)
        {
            byte errorCode = (byte)(AlphaProtocal.Constant.MODBUSFunCodes.RTU_ERROR_CODE_PRE + AlphaProtocal.Constant.MODBUSFunCodes.RTU_PRESET_MULTI_REGS);
            bool isSuccess = true;

            if (receivedData.Length > 0)
            {
                if (receivedData[1] == errorCode) // return errors. and log exception code.
                {
                    LogController.LogError(LoggingLevel.Error).Add("Description", "Writing sliver's configuration is failed! the exception code: " + Convert.ToString(receivedData[2], 16)).Write();
                    InvalidResponse(config);
                    isSuccess = false;
                }
                else
                {
                    byte[] data = receivedData.Take(6).ToArray();
                    Int16 crc = _protocalCtrl.CalculateCRC(data);
                    byte[] receivedDataCRC = receivedData.Skip(6).Take(2).ToArray();
                    Array.Reverse(receivedDataCRC);
                    if (crc == BitConverter.ToInt16(receivedDataCRC, 0)) // CRC is correct.
                    {
                        var newConfig = GetDeviceConfigByAddress(config.Address);
                        string[] commands = config.Commands.Split(' ');
                        byte[] updateData = new byte[commands.Length - 5];
                        for (int i = 5, j = 0; i < commands.Length; i++, j++)
                        {
                            updateData[j] = Convert.ToByte(commands[i], 16);
                        }
                        ConvertNewConfigurationToDeviceConfig(updateData, newConfig);
                        isSuccess = UpdateDeviceConfig(config.Address, newConfig);

                        if (!isSuccess)
                        {
                            LogController.LogError(LoggingLevel.Error).Add("Description", "There is no configuration of Address: " + config.Address).Write();
                            DeleteDeviceConfigurationFromPool(config.ID);
                        }
                    }
                    else
                    {
                        LogController.LogError(LoggingLevel.Error).Add("Description", "Bad response").Write();
                        InvalidResponse(config);
                        isSuccess = false;
                    }
                }
            }
            else
            {
                LogController.LogError(LoggingLevel.Error).Add("Description", "Invalid response: No response data").Write();
                InvalidResponse(config);
                isSuccess = false;
            }

            if (isSuccess)
            {
                UpdatePoolAfterSuccess(config.ID);
            }
        }

        #endregion

        #region For test

        public void ConvertNewConfigurationToDeviceConfigForTest(byte[] updateData, DeviceConfig newConfig)
        {
            ConvertNewConfigurationToDeviceConfig(updateData, newConfig);
        }

        public DeviceConfig ConvertSilverDataToDeviceConfigForTest(byte[] receivedData)
        {
            return ConvertSilverDataToDeviceConfig(receivedData);
        }

        #endregion
    }
}