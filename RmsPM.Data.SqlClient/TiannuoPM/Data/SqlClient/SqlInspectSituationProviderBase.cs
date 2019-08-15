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

    public class SqlInspectSituationProviderBase : InspectSituationProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlInspectSituationProviderBase()
        {
        }

        public SqlInspectSituationProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<InspectSituation> entities)
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
            copy.DestinationTableName = "InspectSituation";
            DataTable table = new DataTable();
            table.Columns.Add("InspectSituationID", typeof(int)).AllowDBNull = false;
            table.Columns.Add("InspectSituationNO", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("InspectDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("Weather", typeof(string)).AllowDBNull = true;
            table.Columns.Add("InspectUserIpecialty", typeof(string)).AllowDBNull = true;
            table.Columns.Add("InspectUser", typeof(string)).AllowDBNull = true;
            table.Columns.Add("KeyPoint", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            copy.ColumnMappings.Add("InspectSituationID", "InspectSituationID");
            copy.ColumnMappings.Add("InspectSituationNO", "InspectSituationNO");
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("InspectDate", "InspectDate");
            copy.ColumnMappings.Add("Weather", "Weather");
            copy.ColumnMappings.Add("InspectUserIpecialty", "InspectUserIpecialty");
            copy.ColumnMappings.Add("InspectUser", "InspectUser");
            copy.ColumnMappings.Add("KeyPoint", "KeyPoint");
            copy.ColumnMappings.Add("Status", "Status");
            foreach (InspectSituation situation in entities)
            {
                if (situation.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["InspectSituationID"] = situation.InspectSituationID;
                    row["InspectSituationNO"] = situation.InspectSituationNO;
                    row["ProjectCode"] = situation.ProjectCode;
                    row["InspectDate"] = situation.InspectDate.HasValue ? ((object) situation.InspectDate) : ((object) DBNull.Value);
                    row["Weather"] = situation.Weather;
                    row["InspectUserIpecialty"] = situation.InspectUserIpecialty;
                    row["InspectUser"] = situation.InspectUser;
                    row["KeyPoint"] = situation.KeyPoint;
                    row["Status"] = situation.Status.HasValue ? ((object) situation.Status) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (InspectSituation situation in entities)
            {
                if (situation.EntityState == EntityState.Added)
                {
                    situation.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, int inspectSituationID)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, inspectSituationID);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(InspectSituation), new object[] { inspectSituationID }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<InspectSituation> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<InspectSituation>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@InspectSituationNO", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@InspectDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Weather", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@InspectUserIpecialty", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@InspectUser", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@KeyPoint", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("inspectsituationid ") || text.Trim().StartsWith("inspectsituationid="))
                {
                    database.SetParameterValue(command, "@InspectSituationID", text.Replace("inspectsituationid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inspectsituationno ") || text.Trim().StartsWith("inspectsituationno="))
                {
                    database.SetParameterValue(command, "@InspectSituationNO", text.Replace("inspectsituationno", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inspectdate ") || text.Trim().StartsWith("inspectdate="))
                {
                    database.SetParameterValue(command, "@InspectDate", text.Replace("inspectdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("weather ") || text.Trim().StartsWith("weather="))
                {
                    database.SetParameterValue(command, "@Weather", text.Replace("weather", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inspectuseripecialty ") || text.Trim().StartsWith("inspectuseripecialty="))
                {
                    database.SetParameterValue(command, "@InspectUserIpecialty", text.Replace("inspectuseripecialty", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inspectuser ") || text.Trim().StartsWith("inspectuser="))
                {
                    database.SetParameterValue(command, "@InspectUser", text.Replace("inspectuser", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("keypoint ") || text.Trim().StartsWith("keypoint="))
                {
                    database.SetParameterValue(command, "@KeyPoint", text.Replace("keypoint", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
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
            TList<InspectSituation> rows = new TList<InspectSituation>();
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
                InspectSituationProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<InspectSituation> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_Find_Dynamic", typeof(InspectSituationColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<InspectSituation> rows = new TList<InspectSituation>();
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
                InspectSituationProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<InspectSituation> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<InspectSituation> rows = new TList<InspectSituation>();
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
                InspectSituationProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override InspectSituation GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_GetByInspectSituationID", this._useStoredProcedure);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, inspectSituationID);
            IDataReader reader = null;
            TList<InspectSituation> rows = new TList<InspectSituation>();
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
                InspectSituationProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<InspectSituation> GetByInspectUser(TransactionManager transactionManager, string inspectUser, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_GetByInspectUser", this._useStoredProcedure);
            database.AddInParameter(command, "@InspectUser", DbType.AnsiString, inspectUser);
            IDataReader reader = null;
            TList<InspectSituation> rows = new TList<InspectSituation>();
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
                InspectSituationProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<InspectSituation> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_GetByProjectCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, projectCode);
            IDataReader reader = null;
            TList<InspectSituation> rows = new TList<InspectSituation>();
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
                InspectSituationProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<InspectSituation> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<InspectSituation> rows = new TList<InspectSituation>();
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
                    InspectSituationProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, InspectSituation entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_Insert", this._useStoredProcedure);
            database.AddOutParameter(command, "@InspectSituationID", DbType.Int32, 4);
            database.AddInParameter(command, "@InspectSituationNO", DbType.AnsiString, entity.InspectSituationNO);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@InspectDate", DbType.DateTime, entity.InspectDate.HasValue ? ((object) entity.InspectDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Weather", DbType.AnsiString, entity.Weather);
            database.AddInParameter(command, "@InspectUserIpecialty", DbType.AnsiString, entity.InspectUserIpecialty);
            database.AddInParameter(command, "@InspectUser", DbType.AnsiString, entity.InspectUser);
            database.AddInParameter(command, "@KeyPoint", DbType.AnsiString, entity.KeyPoint);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.InspectSituationID = (int) database.GetParameterValue(command, "@InspectSituationID");
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, InspectSituation entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.InspectSituation_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@InspectSituationID", DbType.Int32, entity.InspectSituationID);
            database.AddInParameter(command, "@InspectSituationNO", DbType.AnsiString, entity.InspectSituationNO);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@InspectDate", DbType.DateTime, entity.InspectDate.HasValue ? ((object) entity.InspectDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Weather", DbType.AnsiString, entity.Weather);
            database.AddInParameter(command, "@InspectUserIpecialty", DbType.AnsiString, entity.InspectUserIpecialty);
            database.AddInParameter(command, "@InspectUser", DbType.AnsiString, entity.InspectUser);
            database.AddInParameter(command, "@KeyPoint", DbType.AnsiString, entity.KeyPoint);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
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

