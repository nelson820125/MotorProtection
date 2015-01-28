using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    [Serializable]
    public partial class DeviceConfigurationLog
    {
        #region Primitive Properties

        public virtual int ID { set; get; }

        public virtual int Address { set; get; }

        public virtual int FunCode { set; get; }

        public virtual string Commands { set; get; }

        public virtual string Description { set; get; }

        public virtual int UserID { set; get; }

        public virtual DateTime CreateTime { set; get; }

        public virtual int? Status { set; get; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<DeviceConfigurationLog> DeviceConfigurationLogs
        {
            get { return _configLogs ?? (_configLogs = CreateObjectSet<DeviceConfigurationLog>("DeviceConfigurationLogs")); }
        }
        private ObjectSet<DeviceConfigurationLog> _configLogs;
    }
}
