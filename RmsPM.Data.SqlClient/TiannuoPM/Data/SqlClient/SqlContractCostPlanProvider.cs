namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [CLSCompliant(true), DataObject]
    public class SqlContractCostPlanProvider : SqlContractCostPlanProviderBase
    {
        public SqlContractCostPlanProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

