namespace TiannuoPM.Data
{
    using EnvDTE;
    using EnvDTE80;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Configuration.Provider;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Configuration;
    using TiannuoPM.Data.Bases;

    [CLSCompliant(true)]
    public sealed class DataRepository
    {
        private static volatile System.Configuration.Configuration _config = null;
        private static Dictionary<string, ConnectionProvider> _connections;
        private static volatile NetTiersProvider _provider = null;
        private static volatile NetTiersProviderCollection _providers = null;
        private static volatile NetTiersServiceSection _section = null;
        private static object SyncRoot = new object();

        private DataRepository()
        {
        }

        public static void AddConnection(string connectionStringName, string connectionString)
        {
            lock (SyncRoot)
            {
                Connections.Remove(connectionStringName);
                ConnectionProvider provider = new ConnectionProvider(connectionStringName, connectionString);
                Connections.Add(connectionStringName, provider);
            }
        }

        public static TransactionManager CreateTransaction()
        {
            return _provider.CreateTransaction();
        }

        private static System.Configuration.Configuration GetDesignTimeConfig()
        {
            ExeConfigurationFileMap fileMap = null;
            string text = null;
            DTE2 activeObject = (DTE2) Marshal.GetActiveObject("VisualStudio.DTE.8.0");
            if (activeObject != null)
            {
                activeObject.SuppressUI = true;
                ProjectItem item = activeObject.Solution.FindProjectItem("web.config");
                if (item != null)
                {
                    FileInfo info = new FileInfo(item.ContainingProject.FullName);
                    text = string.Format(@"{0}\{1}", info.Directory.FullName, item.Name);
                    fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = text;
                }
            }
            return System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        public static void LoadProvider(NetTiersProvider provider)
        {
            LoadProvider(provider, false);
        }

        public static void LoadProvider(NetTiersProvider provider, bool setAsDefault)
        {
            object syncRoot;
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (_providers == null)
            {
                lock ((syncRoot = SyncRoot))
                {
                    if (_providers == null)
                    {
                        _providers = new NetTiersProviderCollection();
                    }
                }
            }
            if (_providers[provider.Name] == null)
            {
                lock ((syncRoot = _providers.SyncRoot))
                {
                    _providers.Add(provider);
                }
            }
            if ((_provider == null) || setAsDefault)
            {
                lock ((syncRoot = SyncRoot))
                {
                    if ((_provider == null) || setAsDefault)
                    {
                        _provider = provider;
                    }
                }
            }
        }

        private static void LoadProviders()
        {
            if (_provider == null)
            {
                lock (SyncRoot)
                {
                    if (_provider == null)
                    {
                        _providers = new NetTiersProviderCollection();
                        ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
                        _provider = _providers[NetTiersSection.DefaultProvider];
                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default NetTiersProvider");
                        }
                    }
                }
            }
        }

        public static System.Configuration.Configuration Configuration
        {
            get
            {
                if (_config == null)
                {
                    if (HttpContext.Current != null)
                    {
                        _config = WebConfigurationManager.OpenWebConfiguration("~");
                    }
                    else
                    {
                        string exePath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".config", "").Replace(".temp", "");
                        if (exePath.ToLower().Contains("devenv.exe"))
                        {
                            _config = GetDesignTimeConfig();
                        }
                        else
                        {
                            _config = System.Configuration.ConfigurationManager.OpenExeConfiguration(exePath);
                        }
                    }
                }
                return _config;
            }
        }

        public static Dictionary<string, ConnectionProvider> Connections
        {
            get
            {
                if (_connections == null)
                {
                    lock (SyncRoot)
                    {
                        if (_connections == null)
                        {
                            _connections = new Dictionary<string, ConnectionProvider>();
                            foreach (ConnectionStringSettings settings in ConnectionStrings)
                            {
                                _connections.Add(settings.Name, new ConnectionProvider(settings.Name));
                            }
                        }
                    }
                }
                return _connections;
            }
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if ((_config == null) && (_section != null))
                {
                    return WebConfigurationManager.ConnectionStrings;
                }
                return Configuration.ConnectionStrings.ConnectionStrings;
            }
        }

        public static ContractAccountProviderBase ContractAccountProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractAccountProvider;
            }
        }

        public static ContractBillProviderBase ContractBillProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractBillProvider;
            }
        }

        public static ContractChangeProviderBase ContractChangeProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractChangeProvider;
            }
        }

        public static ContractCostChangeProviderBase ContractCostChangeProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractCostChangeProvider;
            }
        }

        public static ContractCostPlanProviderBase ContractCostPlanProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractCostPlanProvider;
            }
        }

        public static ContractCostProviderBase ContractCostProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractCostProvider;
            }
        }

        public static ContractMaterialPlanProviderBase ContractMaterialPlanProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractMaterialPlanProvider;
            }
        }

        public static ContractMaterialProviderBase ContractMaterialProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractMaterialProvider;
            }
        }

        public static ContractNexusProviderBase ContractNexusProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractNexusProvider;
            }
        }

        public static ContractProviderBase ContractProvider
        {
            get
            {
                LoadProviders();
                return _provider.ContractProvider;
            }
        }

        public static DictionaryItemProviderBase DictionaryItemProvider
        {
            get
            {
                LoadProviders();
                return _provider.DictionaryItemProvider;
            }
        }

        public static DictionaryNameProviderBase DictionaryNameProvider
        {
            get
            {
                LoadProviders();
                return _provider.DictionaryNameProvider;
            }
        }

        public static InspectSituationProviderBase InspectSituationProvider
        {
            get
            {
                LoadProviders();
                return _provider.InspectSituationProvider;
            }
        }

        public static MaterialProviderBase MaterialProvider
        {
            get
            {
                LoadProviders();
                return _provider.MaterialProvider;
            }
        }

        public static MaterialPurchasDtlProviderBase MaterialPurchasDtlProvider
        {
            get
            {
                LoadProviders();
                return _provider.MaterialPurchasDtlProvider;
            }
        }

        public static MaterialPurchasProviderBase MaterialPurchasProvider
        {
            get
            {
                LoadProviders();
                return _provider.MaterialPurchasProvider;
            }
        }

        public static NetTiersServiceSection NetTiersSection
        {
            get
            {
                _section = WebConfigurationManager.GetSection("netTiersService") as NetTiersServiceSection;
                if (_section == null)
                {
                    _section = WebConfigurationManager.GetSection("TiannuoPM.Data") as NetTiersServiceSection;
                }
                if (_section == null)
                {
                    foreach (ConfigurationSection section in Configuration.Sections)
                    {
                        if (section is NetTiersServiceSection)
                        {
                            _section = section as NetTiersServiceSection;
                            break;
                        }
                    }
                }
                if (_section == null)
                {
                    throw new ProviderException("Unable to load NetTiersServiceSection");
                }
                return _section;
            }
        }

        public static PaymentItemProviderBase PaymentItemProvider
        {
            get
            {
                LoadProviders();
                return _provider.PaymentItemProvider;
            }
        }

        public static PaymentProviderBase PaymentProvider
        {
            get
            {
                LoadProviders();
                return _provider.PaymentProvider;
            }
        }

        public static PayoutItemProviderBase PayoutItemProvider
        {
            get
            {
                LoadProviders();
                return _provider.PayoutItemProvider;
            }
        }

        public static PayoutProviderBase PayoutProvider
        {
            get
            {
                LoadProviders();
                return _provider.PayoutProvider;
            }
        }

        public static ProjectProviderBase ProjectProvider
        {
            get
            {
                LoadProviders();
                return _provider.ProjectProvider;
            }
        }

        public static NetTiersProvider Provider
        {
            get
            {
                LoadProviders();
                return _provider;
            }
        }

        public static NetTiersProviderCollection Providers
        {
            get
            {
                LoadProviders();
                return _providers;
            }
        }

        public static SystemUserProviderBase SystemUserProvider
        {
            get
            {
                LoadProviders();
                return _provider.SystemUserProvider;
            }
        }

        public static TroubleProviderBase TroubleProvider
        {
            get
            {
                LoadProviders();
                return _provider.TroubleProvider;
            }
        }

        public sealed class ConnectionProvider
        {
            private string _connectionString;
            private string _connectionStringName;
            private NetTiersProvider _provider;
            private NetTiersProviderCollection _providers;

            public ConnectionProvider(string connectionStringName)
            {
                this._connectionStringName = connectionStringName;
            }

            public ConnectionProvider(string connectionStringName, string connectionString)
            {
                this._connectionString = connectionString;
                this._connectionStringName = connectionStringName;
            }

            private void LoadProviders()
            {
                DataRepository.LoadProviders();
                if (this._providers == null)
                {
                    lock (DataRepository.SyncRoot)
                    {
                        if (this._providers == null)
                        {
                            for (int i = 0; i < DataRepository.NetTiersSection.Providers.Count; i++)
                            {
                                DataRepository.NetTiersSection.Providers[i].Parameters["connectionStringName"] = this._connectionStringName;
                                DataRepository.NetTiersSection.Providers[i].Parameters.Remove("connectionString");
                                if (!string.IsNullOrEmpty(this._connectionString))
                                {
                                    DataRepository.NetTiersSection.Providers[i].Parameters["connectionString"] = this._connectionString;
                                }
                            }
                            this._providers = new NetTiersProviderCollection();
                            ProvidersHelper.InstantiateProviders(DataRepository.NetTiersSection.Providers, this._providers, typeof(NetTiersProvider));
                            this._provider = this._providers[DataRepository.NetTiersSection.DefaultProvider];
                        }
                    }
                }
            }

            public NetTiersProvider Provider
            {
                get
                {
                    this.LoadProviders();
                    return this._provider;
                }
            }

            public NetTiersProviderCollection Providers
            {
                get
                {
                    this.LoadProviders();
                    return this._providers;
                }
            }
        }
    }
}

