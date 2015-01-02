using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Data.Entities
{
    public partial class MotorProtectorEntities : ObjectContext
    {
        public const string ConnectionString = "name=MotorProtectorEntities";
        public const string ContainerName = "MotorProtectorEntities";

        #region Constructors

        public MotorProtectorEntities()
            : base(ConnectionString, ContainerName)
        {
        }

        #endregion
    }
}
