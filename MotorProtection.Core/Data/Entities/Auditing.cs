using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    [Serializable]
    public partial class Auditing
    {
        #region Primitive Properties

        public virtual int ID { get; set; }

        public virtual int Level { get; set; }

        public virtual Nullable<int> UserID { get; set; }

        public virtual string Attributes { get; set; }

        public virtual DateTime CreateTime { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<Auditing> Auditings
        {
            get { return _auditings ?? (_auditings = CreateObjectSet<Auditing>("Auditing")); }
        }
        private ObjectSet<Auditing> _auditings;
    }
}
