namespace TiannuoPM.Data.SqlClient
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Data.Bases;
    using TiannuoPM.Entities;

    public class SqlPayoutItemProviderBase : PayoutItemProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlPayoutItemProviderBase()
        {
        }

        public SqlPayoutItemProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<PayoutItem> entities)
        {
            SqlBulkCopy copy = null;
            if ((transactionManager != null) && transactionManager.IsOpen)
            {
                SqlConnection connection = transactionManager.TransactionObject.Connection as SqlConnection;
                SqlTransaction externalTransaction = transactionManager.TransactionObject as SqlTransaction;
                copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, externalTransaction);
            }
            else
            {
                copy = new SqlBulkCopy(this._connectionString, SqlBulkCopyOptions.CheckConstraints);
            }
            copy.BulkCopyTimeout = 360;
            copy.DestinationTableName = "PayoutItem";
            DataTable table = new DataTable();
            table.Columns.Add("PayoutItemCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("PayoutCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PaymentItemCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PayoutMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("SubjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            table.Columns.Add("AlloType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("IsManualAlloc", typeof(int)).AllowDBNull = true;
            table.Columns.Add("PayoutCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("MoneyType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ExchangeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PayoutMoneyType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PayoutExchangeRate", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("PayoutItemCode", "PayoutItemCode");
            copy.ColumnMappings.Add("PayoutCode", "PayoutCode");
            copy.ColumnMappings.Add("PaymentItemCode", "PaymentItemCode");
            copy.ColumnMappings.Add("PayoutMoney", "PayoutMoney");
            copy.ColumnMappings.Add("SubjectCode", "SubjectCode");
            copy.ColumnMappings.Add("Remark", "Remark");
            copy.ColumnMappings.Add("AlloType", "AlloType");
            copy.ColumnMappings.Add("IsManualAlloc", "IsManualAlloc");
            copy.ColumnMappings.Add("PayoutCash", "PayoutCash");
            copy.ColumnMappings.Add("MoneyType", "MoneyType");
            copy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
            copy.ColumnMappings.Add("PayoutMoneyType", "PayoutMoneyType");
            copy.ColumnMappings.Add("PayoutExchangeRate", "PayoutExchangeRate");
            foreach (PayoutItem item in entities)
            {
                if (item.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["PayoutItemCode"] = item.PayoutItemCode;
                    row["PayoutCode"] = item.PayoutCode;
                    row["PaymentItemCode"] = item.PaymentItemCode;
                    row["PayoutMoney"] = item.PayoutMoney.HasValue ? ((object) item.PayoutMoney) : ((object) DBNull.Value);
                    row["SubjectCode"] = item.SubjectCode;
                    row["Remark"] = item.Remark;
                    row["AlloType"] = item.AlloType;
                    row["IsManualAlloc"] = item.IsManualAlloc.HasValue ? ((object) item.IsManualAlloc) : ((object) DBNull.Value);
                    row["PayoutCash"] = item.PayoutCash.HasValue ? ((object) item.PayoutCash) : ((object) DBNull.Value);
                    row["MoneyType"] = item.MoneyType;
                    row["ExchangeRate"] = item.ExchangeRate.HasValue ? ((object) item.ExchangeRate) : ((object) DBNull.Value);
                    row["PayoutMoneyType"] = item.PayoutMoneyType;
                    row["PayoutExchangeRate"] = item.PayoutExchangeRate.HasValue ? ((object) item.PayoutExchangeRate) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (PayoutItem item in entities)
            {
                if (item.EntityState == EntityState.Added)
                {
                    item.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string payoutItemCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutItemCode", DbType.AnsiString, payoutItemCode);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            if (DataRepository.Provider.EnableEntityTracking)
            {
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(PayoutItem), new object[] { payoutItemCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<PayoutItem> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<PayoutItem>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@PayoutItemCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayoutMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@SubjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@AlloType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@IsManualAlloc", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@PayoutCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PayoutMoneyType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayoutExchangeRate", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("payoutitemcode ") || text.Trim().StartsWith("payoutitemcode="))
                {
                    database.SetParameterValue(command, "@PayoutItemCode", text.Replace("payoutitemcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutcode ") || text.Trim().StartsWith("payoutcode="))
                {
                    database.SetParameterValue(command, "@PayoutCode", text.Replace("payoutcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymentitemcode ") || text.Trim().StartsWith("paymentitemcode="))
                {
                    database.SetParameterValue(command, "@PaymentItemCode", text.Replace("paymentitemcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutmoney ") || text.Trim().StartsWith("payoutmoney="))
                {
                    database.SetParameterValue(command, "@PayoutMoney", text.Replace("payoutmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("subjectcode ") || text.Trim().StartsWith("subjectcode="))
                {
                    database.SetParameterValue(command, "@SubjectCode", text.Replace("subjectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("remark ") || text.Trim().StartsWith("remark="))
                {
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("allotype ") || text.Trim().StartsWith("allotype="))
                {
                    database.SetParameterValue(command, "@AlloType", text.Replace("allotype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ismanualalloc ") || text.Trim().StartsWith("ismanualalloc="))
                {
                    database.SetParameterValue(command, "@IsManualAlloc", text.Replace("ismanualalloc", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutcash ") || text.Trim().StartsWith("payoutcash="))
                {
                    database.SetParameterValue(command, "@PayoutCash", text.Replace("payoutcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("moneytype ") || text.Trim().StartsWith("moneytype="))
                {
                    database.SetParameterValue(command, "@MoneyType", text.Replace("moneytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("exchangerate ") || text.Trim().StartsWith("exchangerate="))
                {
                    database.SetParameterValue(command, "@ExchangeRate", text.Replace("exchangerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutmoneytype ") || text.Trim().StartsWith("payoutmoneytype="))
                {
                    database.SetParameterValue(command, "@PayoutMoneyType", text.Replace("payoutmoneytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("payoutexchangerate ") && !text.Trim().StartsWith("payoutexchangerate="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@PayoutExchangeRate", text.Replace("payoutexchangerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<PayoutItem> rows = new TList<PayoutItem>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                PayoutItemProviderBaseCore.Fill(reader, rows, start, pageLength);
                if (reader.NextResult() && reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<PayoutItem> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_Find_Dynamic", typeof(PayoutItemColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<PayoutItem> rows = new TList<PayoutItem>();
            IDataReader reader = null;
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                PayoutItemProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
                count = rows.Count;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<PayoutItem> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<PayoutItem> rows = new TList<PayoutItem>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, dbCommand);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, dbCommand);
                }
                PayoutItemProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<PayoutItem> GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_GetByPaymentItemCode", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, paymentItemCode);
            IDataReader reader = null;
            TList<PayoutItem> rows = new TList<PayoutItem>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                PayoutItemProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<PayoutItem> GetByPayoutCode(TransactionManager transactionManager, string payoutCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_GetByPayoutCode", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, payoutCode);
            IDataReader reader = null;
            TList<PayoutItem> rows = new TList<PayoutItem>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                PayoutItemProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override PayoutItem GetByPayoutItemCode(TransactionManager transactionManager, string payoutItemCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_GetByPayoutItemCode", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutItemCode", DbType.AnsiString, payoutItemCode);
            IDataReader reader = null;
            TList<PayoutItem> rows = new TList<PayoutItem>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                PayoutItemProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (reader.NextResult() && reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if (rows.Count == 1)
            {
                return rows[0];
            }
            if (rows.Count != 0)
            {
                throw new DataException("Cannot find the unique instance of the class.");
            }
            return null;
        }

        public override TList<PayoutItem> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<PayoutItem> rows = new TList<PayoutItem>();
            try
            {
                try
                {
                    if (transactionManager != null)
                    {
                        reader = Utility.ExecuteReader(transactionManager, command);
                    }
                    else
                    {
                        reader = Utility.ExecuteReader(database, command);
                    }
                    PayoutItemProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
                    count = rows.Count;
                    if (reader.NextResult() && reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                    return rows;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override bool Insert(TransactionManager transactionManager, PayoutItem entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutItemCode", DbType.AnsiString, entity.PayoutItemCode);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, entity.PayoutCode);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, entity.PaymentItemCode);
            database.AddInParameter(command, "@PayoutMoney", DbType.Decimal, entity.PayoutMoney.HasValue ? ((object) entity.PayoutMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SubjectCode", DbType.AnsiString, entity.SubjectCode);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@AlloType", DbType.AnsiString, entity.AlloType);
            database.AddInParameter(command, "@IsManualAlloc", DbType.Int32, entity.IsManualAlloc.HasValue ? ((object) entity.IsManualAlloc) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutCash", DbType.Decimal, entity.PayoutCash.HasValue ? ((object) entity.PayoutCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutMoneyType", DbType.AnsiString, entity.PayoutMoneyType);
            database.AddInParameter(command, "@PayoutExchangeRate", DbType.Decimal, entity.PayoutExchangeRate.HasValue ? ((object) entity.PayoutExchangeRate) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalPayoutItemCode = entity.PayoutItemCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, PayoutItem entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PayoutItem_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutItemCode", DbType.AnsiString, entity.PayoutItemCode);
            database.AddInParameter(command, "@OriginalPayoutItemCode", DbType.AnsiString, entity.OriginalPayoutItemCode);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, entity.PayoutCode);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, entity.PaymentItemCode);
            database.AddInParameter(command, "@PayoutMoney", DbType.Decimal, entity.PayoutMoney.HasValue ? ((object) entity.PayoutMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SubjectCode", DbType.AnsiString, entity.SubjectCode);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@AlloType", DbType.AnsiString, entity.AlloType);
            database.AddInParameter(command, "@IsManualAlloc", DbType.Int32, entity.IsManualAlloc.HasValue ? ((object) entity.IsManualAlloc) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutCash", DbType.Decimal, entity.PayoutCash.HasValue ? ((object) entity.PayoutCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutMoneyType", DbType.AnsiString, entity.PayoutMoneyType);
            database.AddInParameter(command, "@PayoutExchangeRate", DbType.Decimal, entity.PayoutExchangeRate.HasValue ? ((object) entity.PayoutExchangeRate) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            if (DataRepository.Provider.EnableEntityTracking)
            {
                EntityManager.StopTracking(entity.EntityTrackingKey);
            }
            entity.OriginalPayoutItemCode = entity.PayoutItemCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
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

