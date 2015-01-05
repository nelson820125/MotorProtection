using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Constant;
using MotorProtection.Core.Data.Entities;

namespace MotorProtection.Core.Cache
{
    public class DeviceCache : IEntityCache
    {
        #region IEntityCache Members

        private const string _key = CacheKey.Device;

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
                return ctt.Devices.OrderBy(d => d.DeviceID).ToList();
            }
        }

        #endregion

        #region [private static]

        public DeviceCache()
        {
            CacheController.CacheUpdated += new CacheController.CacheUpdatedEventHandler(CacheController_CacheUpdated);
        }

        static void CacheController_CacheUpdated(CacheUpdatedEventArgs e)
        {
            if (e.Key == _key)
            {
                s_idDeviceMap = null;
            }
        }

        private static Dictionary<int, Device> s_idDeviceMap;

        private static Dictionary<int, Device> IdDeviceMap
        {
            get
            {
                if (s_idDeviceMap == null)
                {
                    Dictionary<int, Device> map = new Dictionary<int, Device>();
                    foreach (var device in GetAllDevices())
                    {
                        if (!map.ContainsKey(device.DeviceID))
                            map.Add(device.DeviceID, device);
                    }
                    s_idDeviceMap = map;
                }
                return s_idDeviceMap;
            }
        } 

        #endregion

        public static List<Device> GetAllDevices()
        {
            return (List<Device>)CacheController.GetCache(_key);
        }

        public static Device GetDeviceById(int? deviceId)
        {
            Device device = new Device();
            int id = (deviceId == null) ? 0 : deviceId.Value;
            IdDeviceMap.TryGetValue(id, out device);
            return device;
        }

        public static bool AddDevice(Device device)
        {
            if (!Contains(device.DeviceID))
            {
                IdDeviceMap.Add(device.DeviceID, device);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Contains(int deviceId)
        {
            return IdDeviceMap.ContainsKey(deviceId);
        }
    }
}
