namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [DataObject, CLSCompliant(true)]
    public class SqlProjectProvider : SqlProjectProviderBase
    {
        public SqlProjectProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

