using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    public partial class SystemConfig
    {
        #region Primitive Properties

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<SystemConfig> SystemConfigs
        {
            get { return _systemConfigs ?? (_systemConfigs = CreateObjectSet<SystemConfig>("SystemConfig")); }
        }
        private ObjectSet<SystemConfig> _systemConfigs;
    }
}
