namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [CLSCompliant(true), DataObject]
    public class SqlContractBillProvider : SqlContractBillProviderBase
    {
        public SqlContractBillProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

