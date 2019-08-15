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

    public class SqlPaymentItemProviderBase : PaymentItemProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlPaymentItemProviderBase()
        {
        }

        public SqlPaymentItemProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<PaymentItem> entities)
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
            copy.DestinationTableName = "PaymentItem";
            DataTable table = new DataTable();
            table.Columns.Add("PaymentItemCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("PaymentCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CostCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PaymentType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ItemMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Summary", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            table.Columns.Add("AllocateCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("OldItemMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AlloType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CostBudgetSetCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PBSType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PBSCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Description", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractCostCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ItemCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("MoneyType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ExchangeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash0", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash1", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash2", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash3", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash4", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash5", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash6", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash7", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash8", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ItemCash9", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("PaymentItemCode", "PaymentItemCode");
            copy.ColumnMappings.Add("PaymentCode", "PaymentCode");
            copy.ColumnMappings.Add("CostCode", "CostCode");
            copy.ColumnMappings.Add("PaymentType", "PaymentType");
            copy.ColumnMappings.Add("ItemMoney", "ItemMoney");
            copy.ColumnMappings.Add("Summary", "Summary");
            copy.ColumnMappings.Add("Remark", "Remark");
            copy.ColumnMappings.Add("AllocateCode", "AllocateCode");
            copy.ColumnMappings.Add("OldItemMoney", "OldItemMoney");
            copy.ColumnMappings.Add("AlloType", "AlloType");
            copy.ColumnMappings.Add("CostBudgetSetCode", "CostBudgetSetCode");
            copy.ColumnMappings.Add("PBSType", "PBSType");
            copy.ColumnMappings.Add("PBSCode", "PBSCode");
            copy.ColumnMappings.Add("Description", "Description");
            copy.ColumnMappings.Add("ContractCostCode", "ContractCostCode");
            copy.ColumnMappings.Add("ItemCash", "ItemCash");
            copy.ColumnMappings.Add("MoneyType", "MoneyType");
            copy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
            copy.ColumnMappings.Add("ItemCash0", "ItemCash0");
            copy.ColumnMappings.Add("ItemCash1", "ItemCash1");
            copy.ColumnMappings.Add("ItemCash2", "ItemCash2");
            copy.ColumnMappings.Add("ItemCash3", "ItemCash3");
            copy.ColumnMappings.Add("ItemCash4", "ItemCash4");
            copy.ColumnMappings.Add("ItemCash5", "ItemCash5");
            copy.ColumnMappings.Add("ItemCash6", "ItemCash6");
            copy.ColumnMappings.Add("ItemCash7", "ItemCash7");
            copy.ColumnMappings.Add("ItemCash8", "ItemCash8");
            copy.ColumnMappings.Add("ItemCash9", "ItemCash9");
            foreach (PaymentItem item in entities)
            {
                if (item.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["PaymentItemCode"] = item.PaymentItemCode;
                    row["PaymentCode"] = item.PaymentCode;
                    row["CostCode"] = item.CostCode;
                    row["PaymentType"] = item.PaymentType;
                    row["ItemMoney"] = item.ItemMoney.HasValue ? ((object) item.ItemMoney) : ((object) DBNull.Value);
                    row["Summary"] = item.Summary;
                    row["Remark"] = item.Remark;
                    row["AllocateCode"] = item.AllocateCode;
                    row["OldItemMoney"] = item.OldItemMoney.HasValue ? ((object) item.OldItemMoney) : ((object) DBNull.Value);
                    row["AlloType"] = item.AlloType;
                    row["CostBudgetSetCode"] = item.CostBudgetSetCode;
                    row["PBSType"] = item.PBSType;
                    row["PBSCode"] = item.PBSCode;
                    row["Description"] = item.Description;
                    row["ContractCostCode"] = item.ContractCostCode;
                    row["ItemCash"] = item.ItemCash.HasValue ? ((object) item.ItemCash) : ((object) DBNull.Value);
                    row["MoneyType"] = item.MoneyType;
                    row["ExchangeRate"] = item.ExchangeRate.HasValue ? ((object) item.ExchangeRate) : ((object) DBNull.Value);
                    row["ItemCash0"] = item.ItemCash0.HasValue ? ((object) item.ItemCash0) : ((object) DBNull.Value);
                    row["ItemCash1"] = item.ItemCash1.HasValue ? ((object) item.ItemCash1) : ((object) DBNull.Value);
                    row["ItemCash2"] = item.ItemCash2.HasValue ? ((object) item.ItemCash2) : ((object) DBNull.Value);
                    row["ItemCash3"] = item.ItemCash3.HasValue ? ((object) item.ItemCash3) : ((object) DBNull.Value);
                    row["ItemCash4"] = item.ItemCash4.HasValue ? ((object) item.ItemCash4) : ((object) DBNull.Value);
                    row["ItemCash5"] = item.ItemCash5.HasValue ? ((object) item.ItemCash5) : ((object) DBNull.Value);
                    row["ItemCash6"] = item.ItemCash6.HasValue ? ((object) item.ItemCash6) : ((object) DBNull.Value);
                    row["ItemCash7"] = item.ItemCash7.HasValue ? ((object) item.ItemCash7) : ((object) DBNull.Value);
                    row["ItemCash8"] = item.ItemCash8.HasValue ? ((object) item.ItemCash8) : ((object) DBNull.Value);
                    row["ItemCash9"] = item.ItemCash9.HasValue ? ((object) item.ItemCash9) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (PaymentItem item in entities)
            {
                if (item.EntityState == EntityState.Added)
                {
                    item.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string paymentItemCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, paymentItemCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(PaymentItem), new object[] { paymentItemCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<PaymentItem> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<PaymentItem>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PaymentType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ItemMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Summary", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@AllocateCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@OldItemMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AlloType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PBSType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PBSCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Description", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ItemCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash0", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash1", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash2", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash3", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash4", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash5", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash6", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash7", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash8", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ItemCash9", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("paymentitemcode ") || text.Trim().StartsWith("paymentitemcode="))
                {
                    database.SetParameterValue(command, "@PaymentItemCode", text.Replace("paymentitemcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymentcode ") || text.Trim().StartsWith("paymentcode="))
                {
                    database.SetParameterValue(command, "@PaymentCode", text.Replace("paymentcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("costcode ") || text.Trim().StartsWith("costcode="))
                {
                    database.SetParameterValue(command, "@CostCode", text.Replace("costcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymenttype ") || text.Trim().StartsWith("paymenttype="))
                {
                    database.SetParameterValue(command, "@PaymentType", text.Replace("paymenttype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemmoney ") || text.Trim().StartsWith("itemmoney="))
                {
                    database.SetParameterValue(command, "@ItemMoney", text.Replace("itemmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("summary ") || text.Trim().StartsWith("summary="))
                {
                    database.SetParameterValue(command, "@Summary", text.Replace("summary", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("remark ") || text.Trim().StartsWith("remark="))
                {
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("allocatecode ") || text.Trim().StartsWith("allocatecode="))
                {
                    database.SetParameterValue(command, "@AllocateCode", text.Replace("allocatecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("olditemmoney ") || text.Trim().StartsWith("olditemmoney="))
                {
                    database.SetParameterValue(command, "@OldItemMoney", text.Replace("olditemmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("allotype ") || text.Trim().StartsWith("allotype="))
                {
                    database.SetParameterValue(command, "@AlloType", text.Replace("allotype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("costbudgetsetcode ") || text.Trim().StartsWith("costbudgetsetcode="))
                {
                    database.SetParameterValue(command, "@CostBudgetSetCode", text.Replace("costbudgetsetcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("pbstype ") || text.Trim().StartsWith("pbstype="))
                {
                    database.SetParameterValue(command, "@PBSType", text.Replace("pbstype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("pbscode ") || text.Trim().StartsWith("pbscode="))
                {
                    database.SetParameterValue(command, "@PBSCode", text.Replace("pbscode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("description ") || text.Trim().StartsWith("description="))
                {
                    database.SetParameterValue(command, "@Description", text.Replace("description", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcostcode ") || text.Trim().StartsWith("contractcostcode="))
                {
                    database.SetParameterValue(command, "@ContractCostCode", text.Replace("contractcostcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash ") || text.Trim().StartsWith("itemcash="))
                {
                    database.SetParameterValue(command, "@ItemCash", text.Replace("itemcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("moneytype ") || text.Trim().StartsWith("moneytype="))
                {
                    database.SetParameterValue(command, "@MoneyType", text.Replace("moneytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("exchangerate ") || text.Trim().StartsWith("exchangerate="))
                {
                    database.SetParameterValue(command, "@ExchangeRate", text.Replace("exchangerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash0 ") || text.Trim().StartsWith("itemcash0="))
                {
                    database.SetParameterValue(command, "@ItemCash0", text.Replace("itemcash0", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash1 ") || text.Trim().StartsWith("itemcash1="))
                {
                    database.SetParameterValue(command, "@ItemCash1", text.Replace("itemcash1", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash2 ") || text.Trim().StartsWith("itemcash2="))
                {
                    database.SetParameterValue(command, "@ItemCash2", text.Replace("itemcash2", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash3 ") || text.Trim().StartsWith("itemcash3="))
                {
                    database.SetParameterValue(command, "@ItemCash3", text.Replace("itemcash3", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash4 ") || text.Trim().StartsWith("itemcash4="))
                {
                    database.SetParameterValue(command, "@ItemCash4", text.Replace("itemcash4", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash5 ") || text.Trim().StartsWith("itemcash5="))
                {
                    database.SetParameterValue(command, "@ItemCash5", text.Replace("itemcash5", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash6 ") || text.Trim().StartsWith("itemcash6="))
                {
                    database.SetParameterValue(command, "@ItemCash6", text.Replace("itemcash6", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash7 ") || text.Trim().StartsWith("itemcash7="))
                {
                    database.SetParameterValue(command, "@ItemCash7", text.Replace("itemcash7", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("itemcash8 ") || text.Trim().StartsWith("itemcash8="))
                {
                    database.SetParameterValue(command, "@ItemCash8", text.Replace("itemcash8", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("itemcash9 ") && !text.Trim().StartsWith("itemcash9="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@ItemCash9", text.Replace("itemcash9", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<PaymentItem> rows = new TList<PaymentItem>();
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
                PaymentItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<PaymentItem> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_Find_Dynamic", typeof(PaymentItemColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<PaymentItem> rows = new TList<PaymentItem>();
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
                PaymentItemProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<PaymentItem> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<PaymentItem> rows = new TList<PaymentItem>();
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
                PaymentItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<PaymentItem> GetByContractCostCode(TransactionManager transactionManager, string contractCostCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_GetByContractCostCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, contractCostCode);
            IDataReader reader = null;
            TList<PaymentItem> rows = new TList<PaymentItem>();
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
                PaymentItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<PaymentItem> GetByPaymentCode(TransactionManager transactionManager, string paymentCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_GetByPaymentCode", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, paymentCode);
            IDataReader reader = null;
            TList<PaymentItem> rows = new TList<PaymentItem>();
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
                PaymentItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override PaymentItem GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_GetByPaymentItemCode", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, paymentItemCode);
            IDataReader reader = null;
            TList<PaymentItem> rows = new TList<PaymentItem>();
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
                PaymentItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<PaymentItem> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<PaymentItem> rows = new TList<PaymentItem>();
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
                    PaymentItemProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, PaymentItem entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, entity.PaymentItemCode);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, entity.PaymentCode);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, entity.CostCode);
            database.AddInParameter(command, "@PaymentType", DbType.AnsiString, entity.PaymentType);
            database.AddInParameter(command, "@ItemMoney", DbType.Decimal, entity.ItemMoney.HasValue ? ((object) entity.ItemMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Summary", DbType.AnsiString, entity.Summary);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@AllocateCode", DbType.AnsiString, entity.AllocateCode);
            database.AddInParameter(command, "@OldItemMoney", DbType.Decimal, entity.OldItemMoney.HasValue ? ((object) entity.OldItemMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AlloType", DbType.AnsiString, entity.AlloType);
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, entity.CostBudgetSetCode);
            database.AddInParameter(command, "@PBSType", DbType.AnsiString, entity.PBSType);
            database.AddInParameter(command, "@PBSCode", DbType.AnsiString, entity.PBSCode);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, entity.ContractCostCode);
            database.AddInParameter(command, "@ItemCash", DbType.Decimal, entity.ItemCash.HasValue ? ((object) entity.ItemCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash0", DbType.Decimal, entity.ItemCash0.HasValue ? ((object) entity.ItemCash0) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash1", DbType.Decimal, entity.ItemCash1.HasValue ? ((object) entity.ItemCash1) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash2", DbType.Decimal, entity.ItemCash2.HasValue ? ((object) entity.ItemCash2) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash3", DbType.Decimal, entity.ItemCash3.HasValue ? ((object) entity.ItemCash3) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash4", DbType.Decimal, entity.ItemCash4.HasValue ? ((object) entity.ItemCash4) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash5", DbType.Decimal, entity.ItemCash5.HasValue ? ((object) entity.ItemCash5) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash6", DbType.Decimal, entity.ItemCash6.HasValue ? ((object) entity.ItemCash6) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash7", DbType.Decimal, entity.ItemCash7.HasValue ? ((object) entity.ItemCash7) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash8", DbType.Decimal, entity.ItemCash8.HasValue ? ((object) entity.ItemCash8) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash9", DbType.Decimal, entity.ItemCash9.HasValue ? ((object) entity.ItemCash9) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalPaymentItemCode = entity.PaymentItemCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, PaymentItem entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.PaymentItem_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentItemCode", DbType.AnsiString, entity.PaymentItemCode);
            database.AddInParameter(command, "@OriginalPaymentItemCode", DbType.AnsiString, entity.OriginalPaymentItemCode);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, entity.PaymentCode);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, entity.CostCode);
            database.AddInParameter(command, "@PaymentType", DbType.AnsiString, entity.PaymentType);
            database.AddInParameter(command, "@ItemMoney", DbType.Decimal, entity.ItemMoney.HasValue ? ((object) entity.ItemMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Summary", DbType.AnsiString, entity.Summary);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@AllocateCode", DbType.AnsiString, entity.AllocateCode);
            database.AddInParameter(command, "@OldItemMoney", DbType.Decimal, entity.OldItemMoney.HasValue ? ((object) entity.OldItemMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AlloType", DbType.AnsiString, entity.AlloType);
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, entity.CostBudgetSetCode);
            database.AddInParameter(command, "@PBSType", DbType.AnsiString, entity.PBSType);
            database.AddInParameter(command, "@PBSCode", DbType.AnsiString, entity.PBSCode);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, entity.ContractCostCode);
            database.AddInParameter(command, "@ItemCash", DbType.Decimal, entity.ItemCash.HasValue ? ((object) entity.ItemCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash0", DbType.Decimal, entity.ItemCash0.HasValue ? ((object) entity.ItemCash0) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash1", DbType.Decimal, entity.ItemCash1.HasValue ? ((object) entity.ItemCash1) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash2", DbType.Decimal, entity.ItemCash2.HasValue ? ((object) entity.ItemCash2) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash3", DbType.Decimal, entity.ItemCash3.HasValue ? ((object) entity.ItemCash3) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash4", DbType.Decimal, entity.ItemCash4.HasValue ? ((object) entity.ItemCash4) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash5", DbType.Decimal, entity.ItemCash5.HasValue ? ((object) entity.ItemCash5) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash6", DbType.Decimal, entity.ItemCash6.HasValue ? ((object) entity.ItemCash6) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash7", DbType.Decimal, entity.ItemCash7.HasValue ? ((object) entity.ItemCash7) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash8", DbType.Decimal, entity.ItemCash8.HasValue ? ((object) entity.ItemCash8) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ItemCash9", DbType.Decimal, entity.ItemCash9.HasValue ? ((object) entity.ItemCash9) : ((object) DBNull.Value));
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
            entity.OriginalPaymentItemCode = entity.PaymentItemCode;
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

