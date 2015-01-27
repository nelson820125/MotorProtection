using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    [Serializable]
    public partial class User
    {
        #region Primitive Properties

        public virtual int UserID { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual DateTime? LastLoginAt { get; set; }

        public virtual bool IsLocked { get; set; }

        public virtual DateTime? LockedAt { get; set; }

        public virtual string FailedPasswordAttempts { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual DateTime CreateTime { get; set; }

        #endregion
    }

    public partial class MotorProtectorEntities : ObjectContext
    {
        public ObjectSet<User> Users
        {
            get { return _users ?? (_users = CreateObjectSet<User>("Users")); }
        }
        private ObjectSet<User> _users;
    }
}
