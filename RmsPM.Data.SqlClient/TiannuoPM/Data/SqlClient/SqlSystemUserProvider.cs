namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [DataObject, CLSCompliant(true)]
    public class SqlSystemUserProvider : SqlSystemUserProviderBase
    {
        public SqlSystemUserProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

