using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Constant;

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
                    var configLog = new DeviceConfigurationLog() { 
                        Address = config.Address,
                        FunCode = config.FunCode,
                        Commands = config.Commands,
                        Description = config.Description,
                        UserID = config.UserID,
                        CreateTime = DateTime.Now
                    };

                    ctt.DeviceConfigurationLogs.AddObject(configLog);
                    ctt.DeviceConfigurationPools.DeleteObject(config);
                    ctt.SaveChanges();
                }
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
                case FunctionCodes.WIRTE_SINGLE_REGISTER:
                    break;
                case FunctionCodes.WRITE_MULTI_REGITERS:
                    break;
            }

            return command;
        }

        public void SyncSilverFinalize()
        {
 
        }

        public byte[] ReadConfigurationFromSilverCommand(DeviceConfigurationPool config)
        {
            Int16 addr = Convert.ToInt16(config.Address);
            byte registerAddrHi = Convert.ToByte(config.Commands.Split(' ')[0], 16);
            byte registerAddrLow = Convert.ToByte(config.Commands.Split(' ')[1], 16);
            Int16 offset = Convert.ToInt16(config.Commands.Split(' ')[2], 16);

            return _protocalCtrl.ReadRegistersRequest(addr, registerAddrHi, registerAddrLow, offset);
        }
    }
}
