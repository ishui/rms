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

    public class SqlDictionaryNameProviderBase : DictionaryNameProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlDictionaryNameProviderBase()
        {
        }

        public SqlDictionaryNameProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<DictionaryName> entities)
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
            copy.DestinationTableName = "DictionaryName";
            DataTable table = new DataTable();
            table.Columns.Add("DictionaryNameCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("NAME", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("DictionaryNameCode", "DictionaryNameCode");
            copy.ColumnMappings.Add("NAME", "NAME");
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("Remark", "Remark");
            foreach (DictionaryName name in entities)
            {
                if (name.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["DictionaryNameCode"] = name.DictionaryNameCode;
                    row["NAME"] = name.NAME;
                    row["ProjectCode"] = name.ProjectCode;
                    row["Remark"] = name.Remark;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (DictionaryName name in entities)
            {
                if (name.EntityState == EntityState.Added)
                {
                    name.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string dictionaryNameCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, dictionaryNameCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(DictionaryName), new object[] { dictionaryNameCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<DictionaryName> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<DictionaryName>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@NAME", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("dictionarynamecode ") || text.Trim().StartsWith("dictionarynamecode="))
                {
                    database.SetParameterValue(command, "@DictionaryNameCode", text.Replace("dictionarynamecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("name ") || text.Trim().StartsWith("name="))
                {
                    database.SetParameterValue(command, "@NAME", text.Replace("name", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("remark ") && !text.Trim().StartsWith("remark="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<DictionaryName> rows = new TList<DictionaryName>();
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
                DictionaryNameProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<DictionaryName> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_Find_Dynamic", typeof(DictionaryNameColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<DictionaryName> rows = new TList<DictionaryName>();
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
                DictionaryNameProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<DictionaryName> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<DictionaryName> rows = new TList<DictionaryName>();
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
                DictionaryNameProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override DictionaryName GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_GetByDictionaryNameCode", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, dictionaryNameCode);
            IDataReader reader = null;
            TList<DictionaryName> rows = new TList<DictionaryName>();
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
                DictionaryNameProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<DictionaryName> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<DictionaryName> rows = new TList<DictionaryName>();
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
                    DictionaryNameProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, DictionaryName entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, entity.DictionaryNameCode);
            database.AddInParameter(command, "@NAME", DbType.AnsiString, entity.NAME);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalDictionaryNameCode = entity.DictionaryNameCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, DictionaryName entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.DictionaryName_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@DictionaryNameCode", DbType.AnsiString, entity.DictionaryNameCode);
            database.AddInParameter(command, "@OriginalDictionaryNameCode", DbType.AnsiString, entity.OriginalDictionaryNameCode);
            database.AddInParameter(command, "@NAME", DbType.AnsiString, entity.NAME);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
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
            entity.OriginalDictionaryNameCode = entity.DictionaryNameCode;
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

