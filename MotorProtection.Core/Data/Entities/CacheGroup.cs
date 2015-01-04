using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    public partial class CacheGroup
    {
        #region Primitive Properties

        public virtual string Group { get; set; }

        public virtual System.DateTime Timestamp { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<CacheGroup> CacheGroups
        {
            get { return _cacheGroups ?? (_cacheGroups = CreateObjectSet<CacheGroup>("CacheGroups")); }
        }
        private ObjectSet<CacheGroup> _cacheGroups;
    }
}
