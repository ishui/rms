namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [CLSCompliant(true), DataObject]
    public class SqlInspectSituationProvider : SqlInspectSituationProviderBase
    {
        public SqlInspectSituationProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

