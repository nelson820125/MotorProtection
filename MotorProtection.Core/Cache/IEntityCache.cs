using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Core.Cache
{
    /// <summary>
    /// Key: the key in the cache dictionary
    /// method is only invoked when the method name is set in the executeAt property of the cache configuration section
    /// Execution order:
    ///    - Initialize
    ///      Invoked in CacheController.Initialize
    ///    - Synchronize
    ///      Invoked in CacheController.Synchronize
    /// </summary>
    public interface IEntityCache
    {
        string Key { get; }

        object Initialize();

        object Synchronize();
    }
}
