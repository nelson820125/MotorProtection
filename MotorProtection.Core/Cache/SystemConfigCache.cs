using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Constant;
using MotorProtection.Core.Data.Entities;

namespace MotorProtection.Core.Cache
{
    public class SystemConfigCache : IEntityCache
    {
        #region IEntityCache Members

        private const string _key = CacheKey.SystemConfig;

        public string Key
        {
            get { return _key; }
        }

        public object Initialize()
        {
            return Load();
        }

        public object Synchronize()
        {
            return Load();
        }

        private object Load()
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                ctt.ContextOptions.ProxyCreationEnabled = false;

                var systemConfigs = ctt.SystemConfigs.ToList();

                Dictionary<string, string> configs = new Dictionary<string, string>();
                foreach (var systemConfig in systemConfigs)
                {
                    configs.Add(systemConfig.Name, systemConfig.Value);
                }

                return configs;
            }
        }

        #endregion

        private static Dictionary<string, string> Configs
        {
            get { return (Dictionary<string, string>)CacheController.GetCache(_key); }
        }

        public static string GetValue(string name)
        {
            string returnName = "";
            Configs.TryGetValue(name, out returnName);

            return returnName;
        }

        public static bool Contains(string name)
        {
            return Configs.ContainsKey(name);
        }
    }
}
