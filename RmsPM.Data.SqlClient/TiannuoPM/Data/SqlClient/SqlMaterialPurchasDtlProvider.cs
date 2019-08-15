namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [CLSCompliant(true), DataObject]
    public class SqlMaterialPurchasDtlProvider : SqlMaterialPurchasDtlProviderBase
    {
        public SqlMaterialPurchasDtlProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

