using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    public partial class Device
    {
        #region Primitive Properties

        public virtual int DeviceID { get; set; }

        public virtual string Name { get; set; }

        public virtual int? Address { get; set; }

        public virtual int? ParentID { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual DateTime? RestartAt { get; set; }

        public virtual decimal? CurrentA { get; set; }

        public virtual decimal? CurrentB { get; set; }

        public virtual decimal? CurrentC { get; set; }

        public virtual decimal? VoltageA { get; set; }

        public virtual decimal? VoltageB { get; set; }

        public virtual decimal? VoltageC { get; set; }

        public virtual decimal? Power { get; set; }

        public virtual DateTime? AlarmAt { get; set; }

        public virtual DateTime? StopAt { get; set; }

        public virtual decimal? TemperatureA { get; set; }

        public virtual decimal? TemperatureB { get; set; }

        public virtual decimal? TemperatureC { get; set; }

        public virtual decimal? Temperature { get; set; }

        public virtual bool? FirstRMStatus { get; set; }

        public virtual bool? SecondRMStatus { get; set; }

        public virtual int? Status { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<DeviceConfig> DeviceConfigs { get; set; }

        public virtual ICollection<Device> Devices1 { get; set; }

        public virtual Device Device1 { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<Device> Devices
        {
            get { return _devices ?? (_devices = CreateObjectSet<Device>("Devices")); }
        }
        private ObjectSet<Device> _devices;
    }
}
