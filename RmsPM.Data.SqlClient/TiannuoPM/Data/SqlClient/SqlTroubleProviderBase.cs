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

    public class SqlTroubleProviderBase : TroubleProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlTroubleProviderBase()
        {
        }

        public SqlTroubleProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<Trouble> entities)
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
            copy.DestinationTableName = "Trouble";
            DataTable table = new DataTable();
            table.Columns.Add("TroubleID", typeof(int)).AllowDBNull = false;
            table.Columns.Add("InspectSituationID", typeof(int)).AllowDBNull = false;
            table.Columns.Add("Requirement", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Suggestion", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ExecutionTime", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("place", typeof(string)).AllowDBNull = true;
            table.Columns.Add("TroubleCompendium", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = false;
            copy.ColumnMappings.Add("TroubleID", "TroubleID");
            copy.ColumnMappings.Add("InspectSituationID", "InspectSituationID");
            copy.ColumnMappings.Add("Requirement", "Requirement");
            copy.ColumnMappings.Add("Suggestion", "Suggestion");
            copy.ColumnMappings.Add("ExecutionTime", "ExecutionTime");
            copy.ColumnMappings.Add("place", "place");
            copy.ColumnMappings.Add("TroubleCompendium", "TroubleCompendium");
            copy.ColumnMappings.Add("Remark", "Remark");
            copy.ColumnMappings.Add("Status", "Status");
            foreach (Trouble trouble in entities)
            {
                if (trouble.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["TroubleID"] = trouble.TroubleID;
                    row["InspectSituationID"] = trouble.InspectSituationID;
                    row["Requirement"] = trouble.Requirement;
                    row["Suggestion"] = trouble.Suggestion;
                    row["ExecutionTime"] = trouble.ExecutionTime.HasValue ? ((object) trouble.ExecutionTime) : ((object) DBNull.Value);
                    row["place"] = trouble.Place;
                    row["TroubleCompendium"] = trouble.TroubleCompendium;
                    row["Remark"] = trouble.Remark;
                    row["Status"] = trouble.Status;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (Trouble trouble in entities)
            {
                if (trouble.EntityState == EntityState.Added)
                {
                    trouble.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, int troubleID)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@TroubleID", DbType.Int32, troubleID);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(Trouble), new object[] { troubleID }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<Trouble> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<Trouble>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@TroubleID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@Requirement", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Suggestion", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ExecutionTime", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Place", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@TroubleCompendium", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("troubleid ") || text.Trim().StartsWith("troubleid="))
                {
                    database.SetParameterValue(command, "@TroubleID", text.Replace("troubleid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inspectsituationid ") || text.Trim().StartsWith("inspectsituationid="))
                {
                    database.SetParameterValue(command, "@InspectSituationID", text.Replace("inspectsituationid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("requirement ") || text.Trim().StartsWith("requirement="))
                {
                    database.SetParameterValue(command, "@Requirement", text.Replace("requirement", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("suggestion ") || text.Trim().StartsWith("suggestion="))
                {
                    database.SetParameterValue(command, "@Suggestion", text.Replace("suggestion", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("executiontime ") || text.Trim().StartsWith("executiontime="))
                {
                    database.SetParameterValue(command, "@ExecutionTime", text.Replace("executiontime", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("place ") || text.Trim().StartsWith("place="))
                {
                    database.SetParameterValue(command, "@place", text.Replace("place", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("troublecompendium ") || text.Trim().StartsWith("troublecompendium="))
                {
                    database.SetParameterValue(command, "@TroubleCompendium", text.Replace("troublecompendium", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("remark ") || text.Trim().StartsWith("remark="))
                {
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("status ") && !text.Trim().StartsWith("status="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<Trouble> rows = new TList<Trouble>();
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
                TroubleProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Trouble> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_Find_Dynamic", typeof(TroubleColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<Trouble> rows = new TList<Trouble>();
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
                TroubleProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<Trouble> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<Trouble> rows = new TList<Trouble>();
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
                TroubleProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Trouble> GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_GetByInspectSituationID", this._useStoredProcedure);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, inspectSituationID);
            IDataReader reader = null;
            TList<Trouble> rows = new TList<Trouble>();
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
                TroubleProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override Trouble GetByTroubleID(TransactionManager transactionManager, int troubleID, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_GetByTroubleID", this._useStoredProcedure);
            database.AddInParameter(command, "@TroubleID", DbType.Int32, troubleID);
            IDataReader reader = null;
            TList<Trouble> rows = new TList<Trouble>();
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
                TroubleProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Trouble> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<Trouble> rows = new TList<Trouble>();
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
                    TroubleProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, Trouble entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_Insert", this._useStoredProcedure);
            database.AddOutParameter(command, "@TroubleID", DbType.Int32, 4);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, entity.InspectSituationID);
            database.AddInParameter(command, "@Requirement", DbType.AnsiString, entity.Requirement);
            database.AddInParameter(command, "@Suggestion", DbType.AnsiString, entity.Suggestion);
            database.AddInParameter(command, "@ExecutionTime", DbType.DateTime, entity.ExecutionTime.HasValue ? ((object) entity.ExecutionTime) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Place", DbType.AnsiString, entity.Place);
            database.AddInParameter(command, "@TroubleCompendium", DbType.AnsiString, entity.TroubleCompendium);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.TroubleID = (int) database.GetParameterValue(command, "@TroubleID");
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, Trouble entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Trouble_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@TroubleID", DbType.Int32, entity.TroubleID);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, entity.InspectSituationID);
            database.AddInParameter(command, "@Requirement", DbType.AnsiString, entity.Requirement);
            database.AddInParameter(command, "@Suggestion", DbType.AnsiString, entity.Suggestion);
            database.AddInParameter(command, "@ExecutionTime", DbType.DateTime, entity.ExecutionTime.HasValue ? ((object) entity.ExecutionTime) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Place", DbType.AnsiString, entity.Place);
            database.AddInParameter(command, "@TroubleCompendium", DbType.AnsiString, entity.TroubleCompendium);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status);
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

