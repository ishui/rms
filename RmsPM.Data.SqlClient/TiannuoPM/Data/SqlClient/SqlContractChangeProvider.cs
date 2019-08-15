namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [CLSCompliant(true), DataObject]
    public class SqlContractChangeProvider : SqlContractChangeProviderBase
    {
        public SqlContractChangeProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

