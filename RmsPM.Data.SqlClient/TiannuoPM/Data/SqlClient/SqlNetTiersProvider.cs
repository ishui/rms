namespace TiannuoPM.Data.SqlClient
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Data;
    using System.Data.Common;
    using TiannuoPM.Data;
    using TiannuoPM.Data.Bases;

    public sealed class SqlNetTiersProvider : NetTiersProvider
    {
        private string _applicationName;
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;
        private TiannuoPM.Data.SqlClient.SqlContractAccountProvider innerSqlContractAccountProvider;
        private TiannuoPM.Data.SqlClient.SqlContractBillProvider innerSqlContractBillProvider;
        private TiannuoPM.Data.SqlClient.SqlContractChangeProvider innerSqlContractChangeProvider;
        private TiannuoPM.Data.SqlClient.SqlContractCostChangeProvider innerSqlContractCostChangeProvider;
        private TiannuoPM.Data.SqlClient.SqlContractCostPlanProvider innerSqlContractCostPlanProvider;
        private TiannuoPM.Data.SqlClient.SqlContractCostProvider innerSqlContractCostProvider;
        private TiannuoPM.Data.SqlClient.SqlContractMaterialPlanProvider innerSqlContractMaterialPlanProvider;
        private TiannuoPM.Data.SqlClient.SqlContractMaterialProvider innerSqlContractMaterialProvider;
        private TiannuoPM.Data.SqlClient.SqlContractNexusProvider innerSqlContractNexusProvider;
        private TiannuoPM.Data.SqlClient.SqlContractProvider innerSqlContractProvider;
        private TiannuoPM.Data.SqlClient.SqlDictionaryItemProvider innerSqlDictionaryItemProvider;
        private TiannuoPM.Data.SqlClient.SqlDictionaryNameProvider innerSqlDictionaryNameProvider;
        private TiannuoPM.Data.SqlClient.SqlInspectSituationProvider innerSqlInspectSituationProvider;
        private TiannuoPM.Data.SqlClient.SqlMaterialProvider innerSqlMaterialProvider;
        private TiannuoPM.Data.SqlClient.SqlMaterialPurchasDtlProvider innerSqlMaterialPurchasDtlProvider;
        private TiannuoPM.Data.SqlClient.SqlMaterialPurchasProvider innerSqlMaterialPurchasProvider;
        private TiannuoPM.Data.SqlClient.SqlPaymentItemProvider innerSqlPaymentItemProvider;
        private TiannuoPM.Data.SqlClient.SqlPaymentProvider innerSqlPaymentProvider;
        private TiannuoPM.Data.SqlClient.SqlPayoutItemProvider innerSqlPayoutItemProvider;
        private TiannuoPM.Data.SqlClient.SqlPayoutProvider innerSqlPayoutProvider;
        private TiannuoPM.Data.SqlClient.SqlProjectProvider innerSqlProjectProvider;
        private TiannuoPM.Data.SqlClient.SqlSystemUserProvider innerSqlSystemUserProvider;
        private TiannuoPM.Data.SqlClient.SqlTroubleProvider innerSqlTroubleProvider;
        private static object syncRoot = new object();

        public override TransactionManager CreateTransaction()
        {
            return new TransactionManager(this._connectionString);
        }

        public override DataSet ExecuteDataSet(DbCommand commandWrapper)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteDataSet(commandWrapper);
        }

        public override DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteDataSet(commandType, commandText);
        }

        public override DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteDataSet(storedProcedureName, parameterValues);
        }

        public override DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand commandWrapper)
        {
            return transactionManager.Database.ExecuteDataSet(commandWrapper, transactionManager.TransactionObject);
        }

        public override DataSet ExecuteDataSet(TransactionManager transactionManager, CommandType commandType, string commandText)
        {
            return transactionManager.Database.ExecuteDataSet(transactionManager.TransactionObject, commandType, commandText);
        }

        public override DataSet ExecuteDataSet(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
        {
            return transactionManager.Database.ExecuteDataSet(transactionManager.TransactionObject, storedProcedureName, parameterValues);
        }

        public override void ExecuteNonQuery(DbCommand commandWrapper)
        {
            new SqlDatabase(this._connectionString).ExecuteNonQuery(commandWrapper);
        }

        public override int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteNonQuery(commandType, commandText);
        }

        public override int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteNonQuery(storedProcedureName, parameterValues);
        }

        public override void ExecuteNonQuery(TransactionManager transactionManager, DbCommand commandWrapper)
        {
            new SqlDatabase(this._connectionString).ExecuteNonQuery(commandWrapper, transactionManager.TransactionObject);
        }

        public override int ExecuteNonQuery(TransactionManager transactionManager, CommandType commandType, string commandText)
        {
            return transactionManager.Database.ExecuteNonQuery(transactionManager.TransactionObject, commandType, commandText);
        }

        public override int ExecuteNonQuery(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteNonQuery(transactionManager.TransactionObject, storedProcedureName, parameterValues);
        }

        public override IDataReader ExecuteReader(DbCommand commandWrapper)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteReader(commandWrapper);
        }

        public override IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteReader(commandType, commandText);
        }

        public override IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteReader(storedProcedureName, parameterValues);
        }

        public override IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand commandWrapper)
        {
            return transactionManager.Database.ExecuteReader(commandWrapper, transactionManager.TransactionObject);
        }

        public override IDataReader ExecuteReader(TransactionManager transactionManager, CommandType commandType, string commandText)
        {
            return transactionManager.Database.ExecuteReader(transactionManager.TransactionObject, commandType, commandText);
        }

        public override IDataReader ExecuteReader(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
        {
            return transactionManager.Database.ExecuteReader(transactionManager.TransactionObject, storedProcedureName, parameterValues);
        }

        public override object ExecuteScalar(DbCommand commandWrapper)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteScalar(commandWrapper);
        }

        public override object ExecuteScalar(CommandType commandType, string commandText)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteScalar(commandType, commandText);
        }

        public override object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            return database.ExecuteScalar(storedProcedureName, parameterValues);
        }

        public override object ExecuteScalar(TransactionManager transactionManager, DbCommand commandWrapper)
        {
            return transactionManager.Database.ExecuteScalar(commandWrapper, transactionManager.TransactionObject);
        }

        public override object ExecuteScalar(TransactionManager transactionManager, CommandType commandType, string commandText)
        {
            return transactionManager.Database.ExecuteScalar(transactionManager.TransactionObject, commandType, commandText);
        }

        public override object ExecuteScalar(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
        {
            return transactionManager.Database.ExecuteScalar(transactionManager.TransactionObject, storedProcedureName, parameterValues);
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "SqlNetTiersProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "NetTiers Sql provider");
            }
            base.Initialize(name, config);
            this._applicationName = config["applicationName"];
            if (string.IsNullOrEmpty(this._applicationName))
            {
                this._applicationName = "/";
            }
            config.Remove("applicationName");
            string text = config["useStoredProcedure"];
            if (string.IsNullOrEmpty(text))
            {
                throw new ProviderException("Empty or missing useStoredProcedure");
            }
            this._useStoredProcedure = Convert.ToBoolean(config["useStoredProcedure"]);
            config.Remove("useStoredProcedure");
            this._connectionString = config["connectionString"];
            config.Remove("connectionString");
            string text2 = config["connectionStringName"];
            config.Remove("connectionStringName");
            if (string.IsNullOrEmpty(this._connectionString))
            {
                if (string.IsNullOrEmpty(text2))
                {
                    throw new ProviderException("Empty or missing connectionStringName");
                }
                if (DataRepository.ConnectionStrings[text2] == null)
                {
                    throw new ProviderException("Missing connection string");
                }
                this._connectionString = DataRepository.ConnectionStrings[text2].ConnectionString;
            }
            if (string.IsNullOrEmpty(this._connectionString))
            {
                throw new ProviderException("Empty connection string");
            }
            this._providerInvariantName = config["providerInvariantName"];
            if (string.IsNullOrEmpty(this._providerInvariantName))
            {
                throw new ProviderException("Empty or missing providerInvariantName");
            }
            config.Remove("providerInvariantName");
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        public override ContractAccountProviderBase ContractAccountProvider
        {
            get
            {
                if (this.innerSqlContractAccountProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractAccountProvider == null)
                        {
                            this.innerSqlContractAccountProvider = new TiannuoPM.Data.SqlClient.SqlContractAccountProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractAccountProvider;
            }
        }

        public override ContractBillProviderBase ContractBillProvider
        {
            get
            {
                if (this.innerSqlContractBillProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractBillProvider == null)
                        {
                            this.innerSqlContractBillProvider = new TiannuoPM.Data.SqlClient.SqlContractBillProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractBillProvider;
            }
        }

        public override ContractChangeProviderBase ContractChangeProvider
        {
            get
            {
                if (this.innerSqlContractChangeProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractChangeProvider == null)
                        {
                            this.innerSqlContractChangeProvider = new TiannuoPM.Data.SqlClient.SqlContractChangeProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractChangeProvider;
            }
        }

        public override ContractCostChangeProviderBase ContractCostChangeProvider
        {
            get
            {
                if (this.innerSqlContractCostChangeProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractCostChangeProvider == null)
                        {
                            this.innerSqlContractCostChangeProvider = new TiannuoPM.Data.SqlClient.SqlContractCostChangeProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractCostChangeProvider;
            }
        }

        public override ContractCostPlanProviderBase ContractCostPlanProvider
        {
            get
            {
                if (this.innerSqlContractCostPlanProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractCostPlanProvider == null)
                        {
                            this.innerSqlContractCostPlanProvider = new TiannuoPM.Data.SqlClient.SqlContractCostPlanProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractCostPlanProvider;
            }
        }

        public override ContractCostProviderBase ContractCostProvider
        {
            get
            {
                if (this.innerSqlContractCostProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractCostProvider == null)
                        {
                            this.innerSqlContractCostProvider = new TiannuoPM.Data.SqlClient.SqlContractCostProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractCostProvider;
            }
        }

        public override ContractMaterialPlanProviderBase ContractMaterialPlanProvider
        {
            get
            {
                if (this.innerSqlContractMaterialPlanProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractMaterialPlanProvider == null)
                        {
                            this.innerSqlContractMaterialPlanProvider = new TiannuoPM.Data.SqlClient.SqlContractMaterialPlanProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractMaterialPlanProvider;
            }
        }

        public override ContractMaterialProviderBase ContractMaterialProvider
        {
            get
            {
                if (this.innerSqlContractMaterialProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractMaterialProvider == null)
                        {
                            this.innerSqlContractMaterialProvider = new TiannuoPM.Data.SqlClient.SqlContractMaterialProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractMaterialProvider;
            }
        }

        public override ContractNexusProviderBase ContractNexusProvider
        {
            get
            {
                if (this.innerSqlContractNexusProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractNexusProvider == null)
                        {
                            this.innerSqlContractNexusProvider = new TiannuoPM.Data.SqlClient.SqlContractNexusProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractNexusProvider;
            }
        }

        public override ContractProviderBase ContractProvider
        {
            get
            {
                if (this.innerSqlContractProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlContractProvider == null)
                        {
                            this.innerSqlContractProvider = new TiannuoPM.Data.SqlClient.SqlContractProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlContractProvider;
            }
        }

        public override DictionaryItemProviderBase DictionaryItemProvider
        {
            get
            {
                if (this.innerSqlDictionaryItemProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlDictionaryItemProvider == null)
                        {
                            this.innerSqlDictionaryItemProvider = new TiannuoPM.Data.SqlClient.SqlDictionaryItemProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlDictionaryItemProvider;
            }
        }

        public override DictionaryNameProviderBase DictionaryNameProvider
        {
            get
            {
                if (this.innerSqlDictionaryNameProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlDictionaryNameProvider == null)
                        {
                            this.innerSqlDictionaryNameProvider = new TiannuoPM.Data.SqlClient.SqlDictionaryNameProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlDictionaryNameProvider;
            }
        }

        public override InspectSituationProviderBase InspectSituationProvider
        {
            get
            {
                if (this.innerSqlInspectSituationProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlInspectSituationProvider == null)
                        {
                            this.innerSqlInspectSituationProvider = new TiannuoPM.Data.SqlClient.SqlInspectSituationProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlInspectSituationProvider;
            }
        }

        public override bool IsTransactionSupported
        {
            get
            {
                return true;
            }
        }

        public override MaterialProviderBase MaterialProvider
        {
            get
            {
                if (this.innerSqlMaterialProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlMaterialProvider == null)
                        {
                            this.innerSqlMaterialProvider = new TiannuoPM.Data.SqlClient.SqlMaterialProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlMaterialProvider;
            }
        }

        public override MaterialPurchasDtlProviderBase MaterialPurchasDtlProvider
        {
            get
            {
                if (this.innerSqlMaterialPurchasDtlProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlMaterialPurchasDtlProvider == null)
                        {
                            this.innerSqlMaterialPurchasDtlProvider = new TiannuoPM.Data.SqlClient.SqlMaterialPurchasDtlProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlMaterialPurchasDtlProvider;
            }
        }

        public override MaterialPurchasProviderBase MaterialPurchasProvider
        {
            get
            {
                if (this.innerSqlMaterialPurchasProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlMaterialPurchasProvider == null)
                        {
                            this.innerSqlMaterialPurchasProvider = new TiannuoPM.Data.SqlClient.SqlMaterialPurchasProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlMaterialPurchasProvider;
            }
        }

        public override PaymentItemProviderBase PaymentItemProvider
        {
            get
            {
                if (this.innerSqlPaymentItemProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlPaymentItemProvider == null)
                        {
                            this.innerSqlPaymentItemProvider = new TiannuoPM.Data.SqlClient.SqlPaymentItemProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlPaymentItemProvider;
            }
        }

        public override PaymentProviderBase PaymentProvider
        {
            get
            {
                if (this.innerSqlPaymentProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlPaymentProvider == null)
                        {
                            this.innerSqlPaymentProvider = new TiannuoPM.Data.SqlClient.SqlPaymentProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlPaymentProvider;
            }
        }

        public override PayoutItemProviderBase PayoutItemProvider
        {
            get
            {
                if (this.innerSqlPayoutItemProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlPayoutItemProvider == null)
                        {
                            this.innerSqlPayoutItemProvider = new TiannuoPM.Data.SqlClient.SqlPayoutItemProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlPayoutItemProvider;
            }
        }

        public override PayoutProviderBase PayoutProvider
        {
            get
            {
                if (this.innerSqlPayoutProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlPayoutProvider == null)
                        {
                            this.innerSqlPayoutProvider = new TiannuoPM.Data.SqlClient.SqlPayoutProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlPayoutProvider;
            }
        }

        public override ProjectProviderBase ProjectProvider
        {
            get
            {
                if (this.innerSqlProjectProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlProjectProvider == null)
                        {
                            this.innerSqlProjectProvider = new TiannuoPM.Data.SqlClient.SqlProjectProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlProjectProvider;
            }
        }

        public string ProviderInvariantName
        {
            get
            {
                return this._providerInvariantName;
            }
            set
            {
                this._providerInvariantName = value;
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractAccountProvider SqlContractAccountProvider
        {
            get
            {
                return (this.ContractAccountProvider as TiannuoPM.Data.SqlClient.SqlContractAccountProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractBillProvider SqlContractBillProvider
        {
            get
            {
                return (this.ContractBillProvider as TiannuoPM.Data.SqlClient.SqlContractBillProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractChangeProvider SqlContractChangeProvider
        {
            get
            {
                return (this.ContractChangeProvider as TiannuoPM.Data.SqlClient.SqlContractChangeProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractCostChangeProvider SqlContractCostChangeProvider
        {
            get
            {
                return (this.ContractCostChangeProvider as TiannuoPM.Data.SqlClient.SqlContractCostChangeProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractCostPlanProvider SqlContractCostPlanProvider
        {
            get
            {
                return (this.ContractCostPlanProvider as TiannuoPM.Data.SqlClient.SqlContractCostPlanProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractCostProvider SqlContractCostProvider
        {
            get
            {
                return (this.ContractCostProvider as TiannuoPM.Data.SqlClient.SqlContractCostProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractMaterialPlanProvider SqlContractMaterialPlanProvider
        {
            get
            {
                return (this.ContractMaterialPlanProvider as TiannuoPM.Data.SqlClient.SqlContractMaterialPlanProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractMaterialProvider SqlContractMaterialProvider
        {
            get
            {
                return (this.ContractMaterialProvider as TiannuoPM.Data.SqlClient.SqlContractMaterialProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractNexusProvider SqlContractNexusProvider
        {
            get
            {
                return (this.ContractNexusProvider as TiannuoPM.Data.SqlClient.SqlContractNexusProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlContractProvider SqlContractProvider
        {
            get
            {
                return (this.ContractProvider as TiannuoPM.Data.SqlClient.SqlContractProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlDictionaryItemProvider SqlDictionaryItemProvider
        {
            get
            {
                return (this.DictionaryItemProvider as TiannuoPM.Data.SqlClient.SqlDictionaryItemProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlDictionaryNameProvider SqlDictionaryNameProvider
        {
            get
            {
                return (this.DictionaryNameProvider as TiannuoPM.Data.SqlClient.SqlDictionaryNameProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlInspectSituationProvider SqlInspectSituationProvider
        {
            get
            {
                return (this.InspectSituationProvider as TiannuoPM.Data.SqlClient.SqlInspectSituationProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlMaterialProvider SqlMaterialProvider
        {
            get
            {
                return (this.MaterialProvider as TiannuoPM.Data.SqlClient.SqlMaterialProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlMaterialPurchasDtlProvider SqlMaterialPurchasDtlProvider
        {
            get
            {
                return (this.MaterialPurchasDtlProvider as TiannuoPM.Data.SqlClient.SqlMaterialPurchasDtlProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlMaterialPurchasProvider SqlMaterialPurchasProvider
        {
            get
            {
                return (this.MaterialPurchasProvider as TiannuoPM.Data.SqlClient.SqlMaterialPurchasProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlPaymentItemProvider SqlPaymentItemProvider
        {
            get
            {
                return (this.PaymentItemProvider as TiannuoPM.Data.SqlClient.SqlPaymentItemProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlPaymentProvider SqlPaymentProvider
        {
            get
            {
                return (this.PaymentProvider as TiannuoPM.Data.SqlClient.SqlPaymentProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlPayoutItemProvider SqlPayoutItemProvider
        {
            get
            {
                return (this.PayoutItemProvider as TiannuoPM.Data.SqlClient.SqlPayoutItemProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlPayoutProvider SqlPayoutProvider
        {
            get
            {
                return (this.PayoutProvider as TiannuoPM.Data.SqlClient.SqlPayoutProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlProjectProvider SqlProjectProvider
        {
            get
            {
                return (this.ProjectProvider as TiannuoPM.Data.SqlClient.SqlProjectProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlSystemUserProvider SqlSystemUserProvider
        {
            get
            {
                return (this.SystemUserProvider as TiannuoPM.Data.SqlClient.SqlSystemUserProvider);
            }
        }

        public TiannuoPM.Data.SqlClient.SqlTroubleProvider SqlTroubleProvider
        {
            get
            {
                return (this.TroubleProvider as TiannuoPM.Data.SqlClient.SqlTroubleProvider);
            }
        }

        public override SystemUserProviderBase SystemUserProvider
        {
            get
            {
                if (this.innerSqlSystemUserProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlSystemUserProvider == null)
                        {
                            this.innerSqlSystemUserProvider = new TiannuoPM.Data.SqlClient.SqlSystemUserProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlSystemUserProvider;
            }
        }

        public override TroubleProviderBase TroubleProvider
        {
            get
            {
                if (this.innerSqlTroubleProvider == null)
                {
                    lock (syncRoot)
                    {
                        if (this.innerSqlTroubleProvider == null)
                        {
                            this.innerSqlTroubleProvider = new TiannuoPM.Data.SqlClient.SqlTroubleProvider(this._connectionString, this._useStoredProcedure, this._providerInvariantName);
                        }
                    }
                }
                return this.innerSqlTroubleProvider;
            }
        }

        public bool UseStoredProcedure
        {
            get
            {
                return this._useStoredProcedure;
            }
            set
            {
                this._useStoredProcedure = value;
            }
        }
    }
}

