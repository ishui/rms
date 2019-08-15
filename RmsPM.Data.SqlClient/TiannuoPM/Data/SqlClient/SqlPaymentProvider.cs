namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.ComponentModel;

    [DataObject, CLSCompliant(true)]
    public class SqlPaymentProvider : SqlPaymentProviderBase
    {
        public SqlPaymentProvider(string connectionString, bool useStoredProcedure, string providerInvariantName) : base(connectionString, useStoredProcedure, providerInvariantName)
        {
        }
    }
}

