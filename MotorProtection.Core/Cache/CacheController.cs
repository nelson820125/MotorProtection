using MotorProtection.Core.Common;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Core.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MotorProtection.Core.Cache
{
    public static class CacheController
    {
        private static readonly object s_objCriticalSection = new object();

        private static readonly int _syncInterval = SysConfig.CacheSyncInterval;

        private static Dictionary<string, DateTime> s_groups;

        private static Dictionary<string, CacheDataSet> s_cache;

        private static Thread s_thread;

        private static bool s_initialized = false;
        public static bool CacheInitialized { get { return s_initialized; } }

        #region [ event ]

        public delegate void CacheUpdatedEventHandler(CacheUpdatedEventArgs e);

        public static event CacheUpdatedEventHandler CacheUpdated;

        private static void OnCacheUpdated(CacheUpdatedEventArgs e)
        {
            if (CacheUpdated != null)
            {
                CacheUpdated(e);
            }
        }

        #endregion

        public static string GetSyncThreadStatus()
        {
            return s_thread == null ? null : s_thread.ThreadState.ToString();
        }

        // this is to re-try initializing cache when Initialize was failed
        private static void ReTryInitialize()
        {
            Thread.Sleep(_syncInterval);

            Initialize();
        }

        public static void Initialize()
        {
            lock (s_objCriticalSection)
            {
                try
                {
                    s_initialized = false;
                    s_cache = new Dictionary<string, CacheDataSet>();
                    s_groups = new Dictionary<string, DateTime>();

                    var cacheSettings = GetCacheSettings();

                    #region Initialize

                    foreach (CacheGroupElement dEle in cacheSettings.CacheGroupElements)
                    {
                        ArrayList updatedCacheKeys = new ArrayList();
                        s_groups[dEle.Group] = GetCacheGroupTimestamp(dEle.Group);

                        foreach (CacheElement cEle in dEle.CacheElements)
                        {
                            IEntityCache entityCache = (IEntityCache)Activator.CreateInstance(Type.GetType(cEle.Type));

                            try
                            {
                                s_cache[entityCache.Key] = new CacheDataSet
                                {
                                    Cache = entityCache.Initialize(),
                                    Group = dEle.Group,
                                    Name = cEle.Name,
                                    Expires = cEle.Expires <= 0 ? DateTime.MaxValue : DateTime.Now.AddSeconds(cEle.Expires)
                                };

                                updatedCacheKeys.Add(entityCache.Key);
                            }
                            catch (Exception e)
                            {
                                if (cEle.Required) throw e;
                                else
                                {
                                    // add a NULL entry to cache
                                    s_cache[entityCache.Key] = new CacheDataSet
                                    {
                                        Cache = null,
                                        Group = dEle.Group,
                                        Name = cEle.Name,
                                        Expires = DateTime.Now
                                    };

                                    LogController.LogError(LoggingLevel.Error, e).Add("Type", "Cache").Add("Action", "Initialize")
                                        .Add("Name", cEle.Name).Add("Required", "false").Write();
                                }
                            }
                        }

                        foreach (var key in updatedCacheKeys)
                        {
                            OnCacheUpdated(new CacheUpdatedEventArgs(key.ToString(), true)); // fire CacheUpdated event
                        }

                        UpdateCacheStatus(dEle.Group);
                    }

                    #endregion

                    s_initialized = true;

                    if (s_thread != null) s_thread.Abort();
                    s_thread = new Thread(new ThreadStart(Synchronize));
                    s_thread.Start();

                    LogController.LogEvent(AuditingLevel.High, "Cache", "Initialized").Write();
                }
                catch (Exception e)
                {
                    if (s_cache != null) { s_cache.Clear(); s_cache = null; }
                    if (s_groups != null) { s_groups.Clear(); s_groups = null; }
                    if (s_thread != null) { s_thread.Abort(); s_thread = null; }
                    s_initialized = false;

                    Thread t = new Thread(new ThreadStart(ReTryInitialize));
                    t.Start();

                    LogController.LogError(LoggingLevel.Error, e).Add("Type", "Cache").Add("Action", "Initialize").Write();
                }

            }
        }

        private static void Synchronize()
        {
            while (true)
            {
                Thread.Sleep(_syncInterval);

                try
                {
                    CacheSection cacheSettings = GetCacheSettings();

                    #region Cache Entity expired (also includes failed non-required cache)

                    foreach (var entry in s_cache)
                    {
                        if (entry.Value.Expires >= DateTime.Now) continue;

                        var cEle = cacheSettings.CacheGroupElements[entry.Value.Group].CacheElements[entry.Value.Name];

                        IEntityCache entityCache = (IEntityCache)Activator.CreateInstance(Type.GetType(cEle.Type));

                        try
                        {
                            var obj = entityCache.Synchronize();
                            s_cache[entityCache.Key] = new CacheDataSet
                            {
                                Cache = obj,
                                Group = entry.Value.Group,
                                Name = cEle.Name,
                                Expires = cEle.Expires <= 0 ? DateTime.MaxValue : DateTime.Now.AddSeconds(cEle.Expires)
                            };

                            OnCacheUpdated(new CacheUpdatedEventArgs(entityCache.Key, false)); // fire CacheUpdated event
                        }
                        catch (Exception e)
                        {
                            // DO NOT need to update cache, keep the original vlaue in the cache
                            LogController.LogError(LoggingLevel.Error, e).Add("Type", "Cache").Add("Action", "Synchronize")
                                .Add("Name", cEle.Name).Add("Required", "false").Write();
                        }
                    }

                    #endregion

                    #region Cache Group expired

                    foreach (string group in s_groups.Keys.ToArray())
                    {
                        DateTime timestamp = GetCacheGroupTimestamp(group);
                        if (s_groups[group] < timestamp)
                        {
                            lock (s_objCriticalSection)
                            {
                                ArrayList updatedCacheKeys = new ArrayList();
                                CacheGroupElement dEle = cacheSettings.CacheGroupElements[group];

                                #region Synchronize

                                var cache = new Dictionary<string, CacheDataSet>();

                                foreach (CacheElement cEle in dEle.CacheElements)
                                {
                                    IEntityCache entityCache = (IEntityCache)Activator.CreateInstance(Type.GetType(cEle.Type));

                                    try
                                    {
                                        cache[entityCache.Key] = new CacheDataSet
                                        {
                                            Cache = entityCache.Synchronize(),
                                            Group = dEle.Group,
                                            Name = cEle.Name,
                                            Expires = cEle.Expires <= 0 ? DateTime.MaxValue : DateTime.Now.AddSeconds(cEle.Expires)
                                        };

                                        updatedCacheKeys.Add(entityCache.Key);
                                    }
                                    catch (Exception e)
                                    {
                                        if (cEle.Required) throw e;
                                        else
                                        {
                                            // reset the cache to null.
                                            cache[entityCache.Key] = new CacheDataSet
                                            {
                                                Cache = null,
                                                Group = dEle.Group,
                                                Name = cEle.Name,
                                                Expires = DateTime.Now
                                            };
                                            LogController.LogError(LoggingLevel.Error, e).Add("Type", "Cache").Add("Action", "Synchronize")
                                                .Add("Name", cEle.Name).Add("Required", "false").Write();
                                        }
                                    }
                                }

                                foreach (var entry in s_cache) // move other cache entities to the new cache collection, so s_cache is refreshed at once.
                                {
                                    if (entry.Value.Group != group) cache[entry.Key] = entry.Value;
                                }

                                s_cache = cache;

                                s_groups[group] = timestamp;

                                foreach (var key in updatedCacheKeys)
                                {
                                    OnCacheUpdated(new CacheUpdatedEventArgs(key.ToString(), false)); // fire CacheUpdated event
                                }

                                UpdateCacheStatus(group);

                                LogController.LogEvent(AuditingLevel.High, "Cache", "Synchronized").Add("Group", group).Write();

                                #endregion
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception e)
                {
                    LogController.LogError(LoggingLevel.Error, e).Add("Type", "Cache").Add("Action", "Synchronize").Write();
                }
            }
        }

        private static bool IsMatch(string executeAt, string lookFor)
        {
            return Regex.IsMatch(executeAt, ".*\\b" + lookFor + "\\b.*", RegexOptions.IgnoreCase);
        }

        private static CacheSection GetCacheSettings()
        {
            return (CacheSection)ConfigurationManager.GetSection("alpha/cache");
        }

        private static DateTime GetCacheGroupTimestamp(string group)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var cacheGroup = ctt.CacheGroups.FirstOrDefault(cd => cd.Group == group);
                return cacheGroup == null ? DateTime.MinValue : cacheGroup.Timestamp; 
            }            
        }

        private static void UpdateCacheStatus(string group)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var cacheStatus = ctt.CacheStatus.FirstOrDefault(cs => cs.Application == SysConfig.ApplicationName && cs.Group == group);

                if (cacheStatus == null)
                {
                    cacheStatus = new CacheStatus
                    {
                        Application = SysConfig.ApplicationName,
                        Group = group,
                        Timestamp = DateTime.Now
                    };
                    ctt.CacheStatus.AddObject(cacheStatus);
                }
                else
                {
                    cacheStatus.Timestamp = DateTime.Now;
                }

                ctt.SaveChanges();
            }
        }

        public static object GetCache(string key)
        {
            if (s_cache.ContainsKey(key))
            {
                return s_cache[key].Cache;
            }
            else
            {
                return null;
            }
        }

        public static void UpdateCacheGroupTimestamp(string group)
        {
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                var cacheGroup = ctt.CacheGroups.FirstOrDefault(cd => cd.Group == group);
                if (cacheGroup == null)
                {
                    cacheGroup = new CacheGroup
                    {
                        Group = group,
                        Timestamp = DateTime.Now
                    };
                    ctt.CacheGroups.AddObject(cacheGroup);
                }
                else
                {
                    cacheGroup.Timestamp = DateTime.Now;
                }
                ctt.SaveChanges();
            }
        }

        public static void UpdateAllCacheGroupTimestamp()
        {
            var groups = new List<string>();
            using (MotorProtectorEntities ctt = new MotorProtectorEntities())
            {
                groups = ctt.CacheStatus.Select(o => o.Group).Distinct().ToList();
            }

            foreach (string group in groups)
            {
                UpdateCacheGroupTimestamp(group);
            }
        }

        private class CacheDataSet
        {
            public object Cache;
            public string Group;
            public string Name;
            public DateTime Expires;
        }
    }
}
