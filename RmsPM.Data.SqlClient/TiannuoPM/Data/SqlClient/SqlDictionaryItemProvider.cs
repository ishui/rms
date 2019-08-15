namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [DataObject, CLSCompliant(true)]
    public class SqlDictionaryItemProvider : SqlDictionaryItemProviderBase
    {
        public SqlDictionaryItemProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

