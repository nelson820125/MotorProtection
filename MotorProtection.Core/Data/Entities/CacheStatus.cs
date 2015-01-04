using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    public partial class CacheStatus
    {
        #region Primitive Properties

        public virtual string Application { get; set; }

        public virtual string Group { get; set; }

        public virtual System.DateTime Timestamp { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<CacheStatus> CacheStatus
        {
            get { return _cacheStatus ?? (_cacheStatus = CreateObjectSet<CacheStatus>("CacheStatus")); }
        }
        private ObjectSet<CacheStatus> _cacheStatus;
    }
}
