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

    public class SqlPayoutProviderBase : PayoutProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlPayoutProviderBase()
        {
        }

        public SqlPayoutProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<Payout> entities)
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
            copy.DestinationTableName = "Payout";
            DataTable table = new DataTable();
            table.Columns.Add("PayoutCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("PayoutID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PaymentCodes", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PayoutDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("PaymentType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Payer", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SupplyCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BillNo", typeof(string)).AllowDBNull = true;
            table.Columns.Add("InvoNo", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BankName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BankAccount", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SubjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Money", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            table.Columns.Add("InputPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("InputDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("CheckPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CheckDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("CheckOpinion", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SupplyName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ReceiptCount", typeof(int)).AllowDBNull = true;
            table.Columns.Add("GroupCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("IsApportioned", typeof(int)).AllowDBNull = true;
            table.Columns.Add("Cash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("MoneyType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ExchangeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("VoucherNo", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SubjectSetCode", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("PayoutCode", "PayoutCode");
            copy.ColumnMappings.Add("PayoutID", "PayoutID");
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("PaymentCodes", "PaymentCodes");
            copy.ColumnMappings.Add("PayoutDate", "PayoutDate");
            copy.ColumnMappings.Add("PaymentType", "PaymentType");
            copy.ColumnMappings.Add("Payer", "Payer");
            copy.ColumnMappings.Add("SupplyCode", "SupplyCode");
            copy.ColumnMappings.Add("BillNo", "BillNo");
            copy.ColumnMappings.Add("InvoNo", "InvoNo");
            copy.ColumnMappings.Add("BankName", "BankName");
            copy.ColumnMappings.Add("BankAccount", "BankAccount");
            copy.ColumnMappings.Add("SubjectCode", "SubjectCode");
            copy.ColumnMappings.Add("Money", "Money");
            copy.ColumnMappings.Add("Status", "Status");
            copy.ColumnMappings.Add("InputPerson", "InputPerson");
            copy.ColumnMappings.Add("InputDate", "InputDate");
            copy.ColumnMappings.Add("CheckPerson", "CheckPerson");
            copy.ColumnMappings.Add("CheckDate", "CheckDate");
            copy.ColumnMappings.Add("CheckOpinion", "CheckOpinion");
            copy.ColumnMappings.Add("SupplyName", "SupplyName");
            copy.ColumnMappings.Add("Remark", "Remark");
            copy.ColumnMappings.Add("ReceiptCount", "ReceiptCount");
            copy.ColumnMappings.Add("GroupCode", "GroupCode");
            copy.ColumnMappings.Add("IsApportioned", "IsApportioned");
            copy.ColumnMappings.Add("Cash", "Cash");
            copy.ColumnMappings.Add("MoneyType", "MoneyType");
            copy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
            copy.ColumnMappings.Add("VoucherNo", "VoucherNo");
            copy.ColumnMappings.Add("SubjectSetCode", "SubjectSetCode");
            foreach (Payout payout in entities)
            {
                if (payout.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["PayoutCode"] = payout.PayoutCode;
                    row["PayoutID"] = payout.PayoutID;
                    row["ProjectCode"] = payout.ProjectCode;
                    row["PaymentCodes"] = payout.PaymentCodes;
                    row["PayoutDate"] = payout.PayoutDate.HasValue ? ((object) payout.PayoutDate) : ((object) DBNull.Value);
                    row["PaymentType"] = payout.PaymentType;
                    row["Payer"] = payout.Payer;
                    row["SupplyCode"] = payout.SupplyCode;
                    row["BillNo"] = payout.BillNo;
                    row["InvoNo"] = payout.InvoNo;
                    row["BankName"] = payout.BankName;
                    row["BankAccount"] = payout.BankAccount;
                    row["SubjectCode"] = payout.SubjectCode;
                    row["Money"] = payout.Money.HasValue ? ((object) payout.Money) : ((object) DBNull.Value);
                    row["Status"] = payout.Status.HasValue ? ((object) payout.Status) : ((object) DBNull.Value);
                    row["InputPerson"] = payout.InputPerson;
                    row["InputDate"] = payout.InputDate.HasValue ? ((object) payout.InputDate) : ((object) DBNull.Value);
                    row["CheckPerson"] = payout.CheckPerson;
                    row["CheckDate"] = payout.CheckDate.HasValue ? ((object) payout.CheckDate) : ((object) DBNull.Value);
                    row["CheckOpinion"] = payout.CheckOpinion;
                    row["SupplyName"] = payout.SupplyName;
                    row["Remark"] = payout.Remark;
                    row["ReceiptCount"] = payout.ReceiptCount.HasValue ? ((object) payout.ReceiptCount) : ((object) DBNull.Value);
                    row["GroupCode"] = payout.GroupCode;
                    row["IsApportioned"] = payout.IsApportioned.HasValue ? ((object) payout.IsApportioned) : ((object) DBNull.Value);
                    row["Cash"] = payout.Cash.HasValue ? ((object) payout.Cash) : ((object) DBNull.Value);
                    row["MoneyType"] = payout.MoneyType;
                    row["ExchangeRate"] = payout.ExchangeRate.HasValue ? ((object) payout.ExchangeRate) : ((object) DBNull.Value);
                    row["VoucherNo"] = payout.VoucherNo;
                    row["SubjectSetCode"] = payout.SubjectSetCode;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (Payout payout in entities)
            {
                if (payout.EntityState == EntityState.Added)
                {
                    payout.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string payoutCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, payoutCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(Payout), new object[] { payoutCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<Payout> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<Payout>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayoutID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PaymentCodes", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayoutDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@PaymentType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Payer", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SupplyCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BillNo", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@InvoNo", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BankName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BankAccount", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SubjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Money", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@InputPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@InputDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SupplyName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ReceiptCount", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@IsApportioned", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@Cash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@VoucherNo", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SubjectSetCode", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("payoutcode ") || text.Trim().StartsWith("payoutcode="))
                {
                    database.SetParameterValue(command, "@PayoutCode", text.Replace("payoutcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutid ") || text.Trim().StartsWith("payoutid="))
                {
                    database.SetParameterValue(command, "@PayoutID", text.Replace("payoutid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymentcodes ") || text.Trim().StartsWith("paymentcodes="))
                {
                    database.SetParameterValue(command, "@PaymentCodes", text.Replace("paymentcodes", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutdate ") || text.Trim().StartsWith("payoutdate="))
                {
                    database.SetParameterValue(command, "@PayoutDate", text.Replace("payoutdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymenttype ") || text.Trim().StartsWith("paymenttype="))
                {
                    database.SetParameterValue(command, "@PaymentType", text.Replace("paymenttype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payer ") || text.Trim().StartsWith("payer="))
                {
                    database.SetParameterValue(command, "@Payer", text.Replace("payer", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("supplycode ") || text.Trim().StartsWith("supplycode="))
                {
                    database.SetParameterValue(command, "@SupplyCode", text.Replace("supplycode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("billno ") || text.Trim().StartsWith("billno="))
                {
                    database.SetParameterValue(command, "@BillNo", text.Replace("billno", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("invono ") || text.Trim().StartsWith("invono="))
                {
                    database.SetParameterValue(command, "@InvoNo", text.Replace("invono", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("bankname ") || text.Trim().StartsWith("bankname="))
                {
                    database.SetParameterValue(command, "@BankName", text.Replace("bankname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("bankaccount ") || text.Trim().StartsWith("bankaccount="))
                {
                    database.SetParameterValue(command, "@BankAccount", text.Replace("bankaccount", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("subjectcode ") || text.Trim().StartsWith("subjectcode="))
                {
                    database.SetParameterValue(command, "@SubjectCode", text.Replace("subjectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("money ") || text.Trim().StartsWith("money="))
                {
                    database.SetParameterValue(command, "@Money", text.Replace("money", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("status ") || text.Trim().StartsWith("status="))
                {
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inputperson ") || text.Trim().StartsWith("inputperson="))
                {
                    database.SetParameterValue(command, "@InputPerson", text.Replace("inputperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inputdate ") || text.Trim().StartsWith("inputdate="))
                {
                    database.SetParameterValue(command, "@InputDate", text.Replace("inputdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkperson ") || text.Trim().StartsWith("checkperson="))
                {
                    database.SetParameterValue(command, "@CheckPerson", text.Replace("checkperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkdate ") || text.Trim().StartsWith("checkdate="))
                {
                    database.SetParameterValue(command, "@CheckDate", text.Replace("checkdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkopinion ") || text.Trim().StartsWith("checkopinion="))
                {
                    database.SetParameterValue(command, "@CheckOpinion", text.Replace("checkopinion", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("supplyname ") || text.Trim().StartsWith("supplyname="))
                {
                    database.SetParameterValue(command, "@SupplyName", text.Replace("supplyname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("remark ") || text.Trim().StartsWith("remark="))
                {
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("receiptcount ") || text.Trim().StartsWith("receiptcount="))
                {
                    database.SetParameterValue(command, "@ReceiptCount", text.Replace("receiptcount", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("groupcode ") || text.Trim().StartsWith("groupcode="))
                {
                    database.SetParameterValue(command, "@GroupCode", text.Replace("groupcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("isapportioned ") || text.Trim().StartsWith("isapportioned="))
                {
                    database.SetParameterValue(command, "@IsApportioned", text.Replace("isapportioned", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("cash ") || text.Trim().StartsWith("cash="))
                {
                    database.SetParameterValue(command, "@Cash", text.Replace("cash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("moneytype ") || text.Trim().StartsWith("moneytype="))
                {
                    database.SetParameterValue(command, "@MoneyType", text.Replace("moneytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("exchangerate ") || text.Trim().StartsWith("exchangerate="))
                {
                    database.SetParameterValue(command, "@ExchangeRate", text.Replace("exchangerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("voucherno ") || text.Trim().StartsWith("voucherno="))
                {
                    database.SetParameterValue(command, "@VoucherNo", text.Replace("voucherno", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("subjectsetcode ") && !text.Trim().StartsWith("subjectsetcode="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@SubjectSetCode", text.Replace("subjectsetcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<Payout> rows = new TList<Payout>();
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
                PayoutProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Payout> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_Find_Dynamic", typeof(PayoutColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<Payout> rows = new TList<Payout>();
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
                PayoutProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<Payout> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<Payout> rows = new TList<Payout>();
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
                PayoutProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override Payout GetByPayoutCode(TransactionManager transactionManager, string payoutCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_GetByPayoutCode", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, payoutCode);
            IDataReader reader = null;
            TList<Payout> rows = new TList<Payout>();
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
                PayoutProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Payout> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_GetByProjectCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, projectCode);
            IDataReader reader = null;
            TList<Payout> rows = new TList<Payout>();
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
                PayoutProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Payout> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<Payout> rows = new TList<Payout>();
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
                    PayoutProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, Payout entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, entity.PayoutCode);
            database.AddInParameter(command, "@PayoutID", DbType.AnsiString, entity.PayoutID);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@PaymentCodes", DbType.AnsiString, entity.PaymentCodes);
            database.AddInParameter(command, "@PayoutDate", DbType.DateTime, entity.PayoutDate.HasValue ? ((object) entity.PayoutDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PaymentType", DbType.AnsiString, entity.PaymentType);
            database.AddInParameter(command, "@Payer", DbType.AnsiString, entity.Payer);
            database.AddInParameter(command, "@SupplyCode", DbType.AnsiString, entity.SupplyCode);
            database.AddInParameter(command, "@BillNo", DbType.AnsiString, entity.BillNo);
            database.AddInParameter(command, "@InvoNo", DbType.AnsiString, entity.InvoNo);
            database.AddInParameter(command, "@BankName", DbType.AnsiString, entity.BankName);
            database.AddInParameter(command, "@BankAccount", DbType.AnsiString, entity.BankAccount);
            database.AddInParameter(command, "@SubjectCode", DbType.AnsiString, entity.SubjectCode);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@InputPerson", DbType.AnsiString, entity.InputPerson);
            database.AddInParameter(command, "@InputDate", DbType.DateTime, entity.InputDate.HasValue ? ((object) entity.InputDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, entity.CheckOpinion);
            database.AddInParameter(command, "@SupplyName", DbType.AnsiString, entity.SupplyName);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@ReceiptCount", DbType.Int32, entity.ReceiptCount.HasValue ? ((object) entity.ReceiptCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, entity.GroupCode);
            database.AddInParameter(command, "@IsApportioned", DbType.Int32, entity.IsApportioned.HasValue ? ((object) entity.IsApportioned) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Cash", DbType.Decimal, entity.Cash.HasValue ? ((object) entity.Cash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@VoucherNo", DbType.AnsiString, entity.VoucherNo);
            database.AddInParameter(command, "@SubjectSetCode", DbType.AnsiString, entity.SubjectSetCode);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalPayoutCode = entity.PayoutCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, Payout entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payout_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@PayoutCode", DbType.AnsiString, entity.PayoutCode);
            database.AddInParameter(command, "@OriginalPayoutCode", DbType.AnsiString, entity.OriginalPayoutCode);
            database.AddInParameter(command, "@PayoutID", DbType.AnsiString, entity.PayoutID);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@PaymentCodes", DbType.AnsiString, entity.PaymentCodes);
            database.AddInParameter(command, "@PayoutDate", DbType.DateTime, entity.PayoutDate.HasValue ? ((object) entity.PayoutDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PaymentType", DbType.AnsiString, entity.PaymentType);
            database.AddInParameter(command, "@Payer", DbType.AnsiString, entity.Payer);
            database.AddInParameter(command, "@SupplyCode", DbType.AnsiString, entity.SupplyCode);
            database.AddInParameter(command, "@BillNo", DbType.AnsiString, entity.BillNo);
            database.AddInParameter(command, "@InvoNo", DbType.AnsiString, entity.InvoNo);
            database.AddInParameter(command, "@BankName", DbType.AnsiString, entity.BankName);
            database.AddInParameter(command, "@BankAccount", DbType.AnsiString, entity.BankAccount);
            database.AddInParameter(command, "@SubjectCode", DbType.AnsiString, entity.SubjectCode);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@InputPerson", DbType.AnsiString, entity.InputPerson);
            database.AddInParameter(command, "@InputDate", DbType.DateTime, entity.InputDate.HasValue ? ((object) entity.InputDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, entity.CheckOpinion);
            database.AddInParameter(command, "@SupplyName", DbType.AnsiString, entity.SupplyName);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@ReceiptCount", DbType.Int32, entity.ReceiptCount.HasValue ? ((object) entity.ReceiptCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, entity.GroupCode);
            database.AddInParameter(command, "@IsApportioned", DbType.Int32, entity.IsApportioned.HasValue ? ((object) entity.IsApportioned) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Cash", DbType.Decimal, entity.Cash.HasValue ? ((object) entity.Cash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@VoucherNo", DbType.AnsiString, entity.VoucherNo);
            database.AddInParameter(command, "@SubjectSetCode", DbType.AnsiString, entity.SubjectSetCode);
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
            entity.OriginalPayoutCode = entity.PayoutCode;
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

