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

    public class SqlDictionaryItemProviderBase : DictionaryItemProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlDictionaryItemProviderBase()
        {
        }

        public SqlDictionaryItemProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<DictionaryItem> entities)
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
            copy.DestinationTableName = "DictionaryItem";
            DataTable table = new DataTable();
            table.Columns.Add("DictionaryItemCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("DictionaryNameCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SortID", typeof(int)).AllowDBNull = true;
            table.Columns.Add("Name", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("DictionaryItemCode", "DictionaryItemCode");
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("DictionaryNameCode", "DictionaryNameCode");
            copy.ColumnMappings.Add("SortID", "SortID");
            copy.ColumnMappings.Add("Name", "Name");
            foreach (DictionaryItem item in entities)
            {
                if (item.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["DictionaryItemCode"] = item.DictionaryItemCode;
                    row["ProjectCode"] = item.ProjectCode;
                    row["DictionaryNameCode"] = item.DictionaryNameCode;
                    row["SortID"] = item.SortID.HasValue ? ((object) item.SortID) : ((object) DBNull.Value);
                    row["Name"] = item.Name;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (DictionaryItem item in entities)
            {
                if (item.EntityState == EntityState.Added)
                {
                    item.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string dictionaryItemCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryItemCode", DbType.AnsiString, dictionaryItemCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(DictionaryItem), new object[] { dictionaryItemCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<DictionaryItem> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<DictionaryItem>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@DictionaryItemCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SortID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@Name", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("dictionaryitemcode ") || text.Trim().StartsWith("dictionaryitemcode="))
                {
                    database.SetParameterValue(command, "@DictionaryItemCode", text.Replace("dictionaryitemcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("dictionarynamecode ") || text.Trim().StartsWith("dictionarynamecode="))
                {
                    database.SetParameterValue(command, "@DictionaryNameCode", text.Replace("dictionarynamecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("sortid ") || text.Trim().StartsWith("sortid="))
                {
                    database.SetParameterValue(command, "@SortID", text.Replace("sortid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("name ") && !text.Trim().StartsWith("name="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@Name", text.Replace("name", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<DictionaryItem> rows = new TList<DictionaryItem>();
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
                DictionaryItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<DictionaryItem> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_Find_Dynamic", typeof(DictionaryItemColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<DictionaryItem> rows = new TList<DictionaryItem>();
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
                DictionaryItemProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<DictionaryItem> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<DictionaryItem> rows = new TList<DictionaryItem>();
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
                DictionaryItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override DictionaryItem GetByDictionaryItemCode(TransactionManager transactionManager, string dictionaryItemCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_GetByDictionaryItemCode", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryItemCode", DbType.AnsiString, dictionaryItemCode);
            IDataReader reader = null;
            TList<DictionaryItem> rows = new TList<DictionaryItem>();
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
                DictionaryItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<DictionaryItem> GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_GetByDictionaryNameCode", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, dictionaryNameCode);
            IDataReader reader = null;
            TList<DictionaryItem> rows = new TList<DictionaryItem>();
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
                DictionaryItemProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<DictionaryItem> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<DictionaryItem> rows = new TList<DictionaryItem>();
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
                    DictionaryItemProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, DictionaryItem entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryItemCode", DbType.AnsiString, entity.DictionaryItemCode);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, entity.DictionaryNameCode);
            database.AddInParameter(command, "@SortID", DbType.Int32, entity.SortID.HasValue ? ((object) entity.SortID) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Name", DbType.AnsiString, entity.Name);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalDictionaryItemCode = entity.DictionaryItemCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, DictionaryItem entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryItem_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryItemCode", DbType.AnsiString, entity.DictionaryItemCode);
            database.AddInParameter(command, "@OriginalDictionaryItemCode", DbType.AnsiString, entity.OriginalDictionaryItemCode);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, entity.DictionaryNameCode);
            database.AddInParameter(command, "@SortID", DbType.Int32, entity.SortID.HasValue ? ((object) entity.SortID) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Name", DbType.AnsiString, entity.Name);
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
            entity.OriginalDictionaryItemCode = entity.DictionaryItemCode;
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

