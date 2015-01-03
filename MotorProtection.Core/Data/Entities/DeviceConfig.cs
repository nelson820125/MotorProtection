using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    public partial class DeviceConfig
    {
        #region Primitive Properties

        public virtual int DeviceConfigID { get; set; }

        public virtual int DeviceID { get; set; }

        public virtual DateTime? ResetAt { get; set; }

        public virtual decimal? ProtectPower { get; set; }

        public virtual int? ProtectMode { get; set; }

        public virtual int? MIRatio { get; set; }

        public virtual decimal? AlarmThreshold { get; set; }

        public virtual decimal? StopThreshold { get; set; }

        public virtual int? FirstRMMode { get; set; }

        public virtual int? SecondRMMode { get; set; }

        public virtual DateTime UpdateTime { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Device Device { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<DeviceConfig> DeviceConfigs
        {
            get { return _deviceConfigs ?? (_deviceConfigs = CreateObjectSet<DeviceConfig>("DeviceConfigs")); }
        }
        private ObjectSet<DeviceConfig> _deviceConfigs;
    }
}
