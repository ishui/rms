namespace TiannuoPM.Entities
{
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.Caching.BackingStoreImplementations;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Cryptography.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public static class EntityCache
    {
        private static volatile Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager cacheManager;
        private static volatile Microsoft.Practices.EnterpriseLibrary.Caching.CacheManagerFactory cacheManagerFactory;
        private static string cacheManagerKey = "TiannuoPM.Entities.EntityCache";
        private static string configurationFile;
        private static object syncObject = new object();

        public static void AddCache(string id, object entity)
        {
            CacheManager.Add(id, entity);
        }

        public static void FlushCache()
        {
            CacheManager.Flush();
        }

        public static void FlushCacheManager()
        {
            cacheManager = null;
        }

        internal static DictionaryConfigurationSource GenerateConfiguration()
        {
            DictionaryConfigurationSource source = new DictionaryConfigurationSource();
            source.Add("cachingConfiguration", GenerateDefaultCacheManagerSettings());
            return source;
        }

        private static CacheManagerSettings GenerateDefaultCacheManagerSettings()
        {
            CacheManagerSettings settings = new CacheManagerSettings();
            settings.BackingStores.Add(new CacheStorageData("inMemoryWithEncryptor", typeof(NullBackingStore), "dpapiEncryptor"));
            settings.EncryptionProviders.Add(new SymmetricStorageEncryptionProviderData("dpapiEncryptor", "dpapi1"));
            settings.CacheManagers.Add(new CacheManagerData(cacheManagerKey, 0x2710, 0x3e8, 100, "inMemoryWithEncryptor"));
            return settings;
        }

        public static T GetItem<T>(string id) where T: class
        {
            return (CacheManager.GetData(id) as T);
        }

        public static void RemoveItem(string id)
        {
            CacheManager.Remove(id);
        }

        public static Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager CacheManager
        {
            get
            {
                if (cacheManager == null)
                {
                    cacheManager = CacheManagerFactory.Create(cacheManagerKey);
                }
                return cacheManager;
            }
        }

        public static Microsoft.Practices.EnterpriseLibrary.Caching.CacheManagerFactory CacheManagerFactory
        {
            get
            {
                if (cacheManagerFactory == null)
                {
                    lock (syncObject)
                    {
                        IConfigurationSource configurationSource = null;
                        if ((ConfigurationFile != null) && File.Exists(ConfigurationFile))
                        {
                            configurationSource = new FileConfigurationSource(ConfigurationFile);
                            cacheManagerFactory = new Microsoft.Practices.EnterpriseLibrary.Caching.CacheManagerFactory(configurationSource);
                        }
                        else
                        {
                            try
                            {
                                IConfigurationSource source2 = new SystemConfigurationSource();
                                cacheManagerFactory = new Microsoft.Practices.EnterpriseLibrary.Caching.CacheManagerFactory(source2);
                                cacheManagerFactory.Create(CacheManagerKey);
                            }
                            catch (Exception)
                            {
                                cacheManagerFactory = new Microsoft.Practices.EnterpriseLibrary.Caching.CacheManagerFactory(GenerateConfiguration());
                            }
                        }
                    }
                }
                return cacheManagerFactory;
            }
            set
            {
                cacheManagerFactory = value;
            }
        }

        public static string CacheManagerKey
        {
            get
            {
                return cacheManagerKey;
            }
            set
            {
                lock (syncObject)
                {
                    cacheManagerKey = value;
                }
            }
        }

        public static string ConfigurationFile
        {
            get
            {
                return configurationFile;
            }
            set
            {
                lock (syncObject)
                {
                    configurationFile = value;
                }
            }
        }
    }
}

