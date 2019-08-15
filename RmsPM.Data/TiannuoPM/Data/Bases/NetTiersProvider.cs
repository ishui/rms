namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class NetTiersProvider : ProviderBase
    {
        private int defaultCommandTimeout = 30;
        private bool enableEntityTracking = true;
        private bool enableListTracking = false;
        private bool enableMethodAuthorization = false;
        private Type entityCreationalFactoryType = null;
        private static object syncObject = new object();
        private bool useEntityFactory = true;

        protected NetTiersProvider()
        {
        }

        public virtual TransactionManager CreateTransaction()
        {
            throw new NotSupportedException();
        }

        public abstract DataSet ExecuteDataSet(DbCommand commandWrapper);
        public abstract DataSet ExecuteDataSet(CommandType commandType, string commandText);
        public abstract DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues);
        public abstract DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand commandWrapper);
        public abstract DataSet ExecuteDataSet(TransactionManager transactionManager, CommandType commandType, string commandText);
        public abstract DataSet ExecuteDataSet(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues);
        public abstract void ExecuteNonQuery(DbCommand commandWrapper);
        public abstract int ExecuteNonQuery(CommandType commandType, string commandText);
        public abstract int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues);
        public abstract void ExecuteNonQuery(TransactionManager transactionManager, DbCommand commandWrapper);
        public abstract int ExecuteNonQuery(TransactionManager transactionManager, CommandType commandType, string commandText);
        public abstract int ExecuteNonQuery(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues);
        public abstract IDataReader ExecuteReader(DbCommand commandWrapper);
        public abstract IDataReader ExecuteReader(CommandType commandType, string commandText);
        public abstract IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues);
        public abstract IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand commandWrapper);
        public abstract IDataReader ExecuteReader(TransactionManager transactionManager, CommandType commandType, string commandText);
        public abstract IDataReader ExecuteReader(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues);
        public abstract object ExecuteScalar(DbCommand commandWrapper);
        public abstract object ExecuteScalar(CommandType commandType, string commandText);
        public abstract object ExecuteScalar(string storedProcedureName, params object[] parameterValues);
        public abstract object ExecuteScalar(TransactionManager transactionManager, DbCommand commandWrapper);
        public abstract object ExecuteScalar(TransactionManager transactionManager, CommandType commandType, string commandText);
        public abstract object ExecuteScalar(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues);
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            string text = config["entityFactoryType"];
            lock (syncObject)
            {
                if (string.IsNullOrEmpty(text))
                {
                    this.entityCreationalFactoryType = typeof(EntityFactory);
                }
                else
                {
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (assembly.FullName.Split(new char[] { ',' })[0] == text.Substring(0, text.LastIndexOf('.')))
                        {
                            this.entityCreationalFactoryType = assembly.GetType(text, false, true);
                            break;
                        }
                    }
                }
                if (this.entityCreationalFactoryType == null)
                {
                    Assembly assembly2 = null;
                    try
                    {
                        assembly2 = Assembly.Load(text.Substring(0, text.LastIndexOf('.')));
                    }
                    catch
                    {
                    }
                    if (assembly2 != null)
                    {
                        this.entityCreationalFactoryType = assembly2.GetType(text, false, true);
                    }
                }
                if (this.entityCreationalFactoryType == null)
                {
                    throw new ArgumentNullException("Could not find a valid entity factory configured in assemblies.  .netTiers can not continue.");
                }
                bool.TryParse(config["enableEntityTracking"], out this.enableEntityTracking);
                bool.TryParse(config["enableListTracking"], out this.enableListTracking);
                bool.TryParse(config["useEntityFactory"], out this.useEntityFactory);
                bool.TryParse(config["enableMethodAuthorization"], out this.enableMethodAuthorization);
                int.TryParse(config["defaultCommandTimeout"], out this.defaultCommandTimeout);
            }
        }

        public virtual ContractAccountProviderBase ContractAccountProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractBillProviderBase ContractBillProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractChangeProviderBase ContractChangeProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractCostChangeProviderBase ContractCostChangeProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractCostPlanProviderBase ContractCostPlanProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractCostProviderBase ContractCostProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractMaterialPlanProviderBase ContractMaterialPlanProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractMaterialProviderBase ContractMaterialProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractNexusProviderBase ContractNexusProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ContractProviderBase ContractProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual int DefaultCommandTimeout
        {
            get
            {
                return this.defaultCommandTimeout;
            }
            set
            {
                this.defaultCommandTimeout = value;
            }
        }

        public virtual DictionaryItemProviderBase DictionaryItemProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual DictionaryNameProviderBase DictionaryNameProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool EnableEntityTracking
        {
            get
            {
                return this.enableEntityTracking;
            }
            set
            {
                this.enableEntityTracking = value;
            }
        }

        public virtual bool EnableListTracking
        {
            get
            {
                return this.enableListTracking;
            }
            set
            {
                this.enableListTracking = value;
            }
        }

        public virtual bool EnableMethodAuthorization
        {
            get
            {
                return this.enableMethodAuthorization;
            }
            set
            {
                this.enableMethodAuthorization = value;
            }
        }

        public virtual Type EntityCreationalFactoryType
        {
            get
            {
                return this.entityCreationalFactoryType;
            }
            set
            {
                this.entityCreationalFactoryType = value;
            }
        }

        public virtual InspectSituationProviderBase InspectSituationProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public abstract bool IsTransactionSupported { get; }

        public virtual MaterialProviderBase MaterialProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual MaterialPurchasDtlProviderBase MaterialPurchasDtlProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual MaterialPurchasProviderBase MaterialPurchasProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual PaymentItemProviderBase PaymentItemProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual PaymentProviderBase PaymentProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual PayoutItemProviderBase PayoutItemProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual PayoutProviderBase PayoutProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ProjectProviderBase ProjectProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual SystemUserProviderBase SystemUserProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual TroubleProviderBase TroubleProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool UseEntityFactory
        {
            get
            {
                return this.useEntityFactory;
            }
            set
            {
                this.useEntityFactory = value;
            }
        }
    }
}

