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

    public class SqlContractNexusProviderBase : ContractNexusProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractNexusProviderBase()
        {
        }

        public SqlContractNexusProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<ContractNexus> entities)
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
            copy.DestinationTableName = "ContractNexus";
            DataTable table = new DataTable();
            table.Columns.Add("ContractNexusCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractChangeCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Code", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Type", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Name", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Person", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Date", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("Path", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Money", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractNexusCode", "ContractNexusCode");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("ContractChangeCode", "ContractChangeCode");
            copy.ColumnMappings.Add("Code", "Code");
            copy.ColumnMappings.Add("Type", "Type");
            copy.ColumnMappings.Add("Name", "Name");
            copy.ColumnMappings.Add("ID", "ID");
            copy.ColumnMappings.Add("Person", "Person");
            copy.ColumnMappings.Add("Date", "Date");
            copy.ColumnMappings.Add("Path", "Path");
            copy.ColumnMappings.Add("Money", "Money");
            foreach (ContractNexus nexus in entities)
            {
                if (nexus.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractNexusCode"] = nexus.ContractNexusCode;
                    row["ContractCode"] = nexus.ContractCode;
                    row["ContractChangeCode"] = nexus.ContractChangeCode;
                    row["Code"] = nexus.Code;
                    row["Type"] = nexus.Type;
                    row["Name"] = nexus.Name;
                    row["ID"] = nexus.ID;
                    row["Person"] = nexus.Person;
                    row["Date"] = nexus.Date.HasValue ? ((object) nexus.Date) : ((object) DBNull.Value);
                    row["Path"] = nexus.Path;
                    row["Money"] = nexus.Money.HasValue ? ((object) nexus.Money) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (ContractNexus nexus in entities)
            {
                if (nexus.EntityState == EntityState.Added)
                {
                    nexus.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractNexusCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractNexusCode", DbType.AnsiString, contractNexusCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(ContractNexus), new object[] { contractNexusCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<ContractNexus> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<ContractNexus>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractNexusCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Code", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Type", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Name", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Person", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Date", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Path", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Money", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractnexuscode ") || text.Trim().StartsWith("contractnexuscode="))
                {
                    database.SetParameterValue(command, "@ContractNexusCode", text.Replace("contractnexuscode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractchangecode ") || text.Trim().StartsWith("contractchangecode="))
                {
                    database.SetParameterValue(command, "@ContractChangeCode", text.Replace("contractchangecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("code ") || text.Trim().StartsWith("code="))
                {
                    database.SetParameterValue(command, "@Code", text.Replace("code", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("type ") || text.Trim().StartsWith("type="))
                {
                    database.SetParameterValue(command, "@Type", text.Replace("type", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("name ") || text.Trim().StartsWith("name="))
                {
                    database.SetParameterValue(command, "@Name", text.Replace("name", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("id ") || text.Trim().StartsWith("id="))
                {
                    database.SetParameterValue(command, "@ID", text.Replace("id", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("person ") || text.Trim().StartsWith("person="))
                {
                    database.SetParameterValue(command, "@Person", text.Replace("person", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("date ") || text.Trim().StartsWith("date="))
                {
                    database.SetParameterValue(command, "@Date", text.Replace("date", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("path ") || text.Trim().StartsWith("path="))
                {
                    database.SetParameterValue(command, "@Path", text.Replace("path", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("money ") && !text.Trim().StartsWith("money="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@Money", text.Replace("money", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<ContractNexus> rows = new TList<ContractNexus>();
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
                ContractNexusProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractNexus> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_Find_Dynamic", typeof(ContractNexusColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<ContractNexus> rows = new TList<ContractNexus>();
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
                ContractNexusProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<ContractNexus> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<ContractNexus> rows = new TList<ContractNexus>();
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
                ContractNexusProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractNexus> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_GetByContractChangeCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, contractChangeCode);
            IDataReader reader = null;
            TList<ContractNexus> rows = new TList<ContractNexus>();
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
                ContractNexusProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override ContractNexus GetByContractNexusCode(TransactionManager transactionManager, string contractNexusCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_GetByContractNexusCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractNexusCode", DbType.AnsiString, contractNexusCode);
            IDataReader reader = null;
            TList<ContractNexus> rows = new TList<ContractNexus>();
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
                ContractNexusProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractNexus> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<ContractNexus> rows = new TList<ContractNexus>();
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
                    ContractNexusProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, ContractNexus entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractNexusCode", DbType.AnsiString, entity.ContractNexusCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
            database.AddInParameter(command, "@Code", DbType.AnsiString, entity.Code);
            database.AddInParameter(command, "@Type", DbType.AnsiString, entity.Type);
            database.AddInParameter(command, "@Name", DbType.AnsiString, entity.Name);
            database.AddInParameter(command, "@ID", DbType.AnsiString, entity.ID);
            database.AddInParameter(command, "@Person", DbType.AnsiString, entity.Person);
            database.AddInParameter(command, "@Date", DbType.DateTime, entity.Date.HasValue ? ((object) entity.Date) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Path", DbType.AnsiString, entity.Path);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalContractNexusCode = entity.ContractNexusCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, ContractNexus entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractNexus_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractNexusCode", DbType.AnsiString, entity.ContractNexusCode);
            database.AddInParameter(command, "@OriginalContractNexusCode", DbType.AnsiString, entity.OriginalContractNexusCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
            database.AddInParameter(command, "@Code", DbType.AnsiString, entity.Code);
            database.AddInParameter(command, "@Type", DbType.AnsiString, entity.Type);
            database.AddInParameter(command, "@Name", DbType.AnsiString, entity.Name);
            database.AddInParameter(command, "@ID", DbType.AnsiString, entity.ID);
            database.AddInParameter(command, "@Person", DbType.AnsiString, entity.Person);
            database.AddInParameter(command, "@Date", DbType.DateTime, entity.Date.HasValue ? ((object) entity.Date) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Path", DbType.AnsiString, entity.Path);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
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
            entity.OriginalContractNexusCode = entity.ContractNexusCode;
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

