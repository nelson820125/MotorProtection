using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    [Serializable]
    public partial class DeviceConfigsLog
    {
        #region Primitive Properties

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int ID { get; set; }

        public virtual decimal? ProtectPower { get; set; }

        public virtual int? ProtectMode { get; set; }

        public virtual int? MIRatio { get; set; }

        public virtual int? AlarmThreshold { get; set; }

        public virtual int? StopThreshold { get; set; }

        public virtual int? FirstRMMode { get; set; }

        public virtual int? SecondRMMode { get; set; }

        public virtual DateTime? UpdateTime { get; set; }

        public virtual int Status { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<DeviceConfigsLog> DeviceConfigsLogs
        {
            get { return _deviceConfigsLogs ?? (_deviceConfigsLogs = CreateObjectSet<DeviceConfigsLog>("DeviceConfigsLogs")); }
        }
        private ObjectSet<DeviceConfigsLog> _deviceConfigsLogs;
    }
}
