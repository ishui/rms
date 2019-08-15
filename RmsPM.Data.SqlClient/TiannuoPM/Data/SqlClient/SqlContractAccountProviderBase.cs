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

    public class SqlContractAccountProviderBase : ContractAccountProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractAccountProviderBase()
        {
        }

        public SqlContractAccountProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<ContractAccount> entities)
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
            copy.DestinationTableName = "ContractAccount";
            DataTable table = new DataTable();
            table.Columns.Add("ContractAccountCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractAccountID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Reason", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            table.Columns.Add("CreateDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("CreatePerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractChangeCode", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractAccountCode", "ContractAccountCode");
            copy.ColumnMappings.Add("ContractAccountID", "ContractAccountID");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("Reason", "Reason");
            copy.ColumnMappings.Add("Status", "Status");
            copy.ColumnMappings.Add("CreateDate", "CreateDate");
            copy.ColumnMappings.Add("CreatePerson", "CreatePerson");
            copy.ColumnMappings.Add("ContractChangeCode", "ContractChangeCode");
            foreach (ContractAccount account in entities)
            {
                if (account.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractAccountCode"] = account.ContractAccountCode;
                    row["ContractAccountID"] = account.ContractAccountID;
                    row["ContractCode"] = account.ContractCode;
                    row["Reason"] = account.Reason;
                    row["Status"] = account.Status.HasValue ? ((object) account.Status) : ((object) DBNull.Value);
                    row["CreateDate"] = account.CreateDate.HasValue ? ((object) account.CreateDate) : ((object) DBNull.Value);
                    row["CreatePerson"] = account.CreatePerson;
                    row["ContractChangeCode"] = account.ContractChangeCode;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (ContractAccount account in entities)
            {
                if (account.EntityState == EntityState.Added)
                {
                    account.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractAccountCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractAccountCode", DbType.AnsiString, contractAccountCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(ContractAccount), new object[] { contractAccountCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<ContractAccount> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<ContractAccount>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractAccountCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractAccountID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Reason", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@CreateDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@CreatePerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractaccountcode ") || text.Trim().StartsWith("contractaccountcode="))
                {
                    database.SetParameterValue(command, "@ContractAccountCode", text.Replace("contractaccountcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractaccountid ") || text.Trim().StartsWith("contractaccountid="))
                {
                    database.SetParameterValue(command, "@ContractAccountID", text.Replace("contractaccountid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("reason ") || text.Trim().StartsWith("reason="))
                {
                    database.SetParameterValue(command, "@Reason", text.Replace("reason", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("status ") || text.Trim().StartsWith("status="))
                {
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("createdate ") || text.Trim().StartsWith("createdate="))
                {
                    database.SetParameterValue(command, "@CreateDate", text.Replace("createdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("createperson ") || text.Trim().StartsWith("createperson="))
                {
                    database.SetParameterValue(command, "@CreatePerson", text.Replace("createperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("contractchangecode ") && !text.Trim().StartsWith("contractchangecode="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@ContractChangeCode", text.Replace("contractchangecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<ContractAccount> rows = new TList<ContractAccount>();
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
                ContractAccountProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractAccount> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_Find_Dynamic", typeof(ContractAccountColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<ContractAccount> rows = new TList<ContractAccount>();
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
                ContractAccountProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<ContractAccount> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<ContractAccount> rows = new TList<ContractAccount>();
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
                ContractAccountProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override ContractAccount GetByContractAccountCode(TransactionManager transactionManager, string contractAccountCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_GetByContractAccountCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractAccountCode", DbType.AnsiString, contractAccountCode);
            IDataReader reader = null;
            TList<ContractAccount> rows = new TList<ContractAccount>();
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
                ContractAccountProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractAccount> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_GetByContractCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, contractCode);
            IDataReader reader = null;
            TList<ContractAccount> rows = new TList<ContractAccount>();
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
                ContractAccountProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractAccount> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<ContractAccount> rows = new TList<ContractAccount>();
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
                    ContractAccountProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, ContractAccount entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractAccountCode", DbType.AnsiString, entity.ContractAccountCode);
            database.AddInParameter(command, "@ContractAccountID", DbType.AnsiString, entity.ContractAccountID);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@Reason", DbType.AnsiString, entity.Reason);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreateDate", DbType.DateTime, entity.CreateDate.HasValue ? ((object) entity.CreateDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreatePerson", DbType.AnsiString, entity.CreatePerson);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalContractAccountCode = entity.ContractAccountCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, ContractAccount entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractAccount_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractAccountCode", DbType.AnsiString, entity.ContractAccountCode);
            database.AddInParameter(command, "@OriginalContractAccountCode", DbType.AnsiString, entity.OriginalContractAccountCode);
            database.AddInParameter(command, "@ContractAccountID", DbType.AnsiString, entity.ContractAccountID);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@Reason", DbType.AnsiString, entity.Reason);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreateDate", DbType.DateTime, entity.CreateDate.HasValue ? ((object) entity.CreateDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreatePerson", DbType.AnsiString, entity.CreatePerson);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
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
            entity.OriginalContractAccountCode = entity.ContractAccountCode;
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

