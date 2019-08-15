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

    public class SqlMaterialPurchasProviderBase : MaterialPurchasProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlMaterialPurchasProviderBase()
        {
        }

        public SqlMaterialPurchasProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<MaterialPurchas> entities)
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
            copy.DestinationTableName = "MaterialPurchas";
            DataTable table = new DataTable();
            table.Columns.Add("MaterialPurchasID", typeof(int)).AllowDBNull = false;
            table.Columns.Add("MaterialPurchasCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PurchasUnitCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PurchasDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Title", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Description", typeof(string)).AllowDBNull = true;
            table.Columns.Add("FollowUserCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("MaterialPurchasID", "MaterialPurchasID");
            copy.ColumnMappings.Add("MaterialPurchasCode", "MaterialPurchasCode");
            copy.ColumnMappings.Add("PurchasUnitCode", "PurchasUnitCode");
            copy.ColumnMappings.Add("PurchasDate", "PurchasDate");
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("Title", "Title");
            copy.ColumnMappings.Add("Description", "Description");
            copy.ColumnMappings.Add("FollowUserCode", "FollowUserCode");
            copy.ColumnMappings.Add("Status", "Status");
            foreach (MaterialPurchas purchas in entities)
            {
                if (purchas.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["MaterialPurchasID"] = purchas.MaterialPurchasID;
                    row["MaterialPurchasCode"] = purchas.MaterialPurchasCode;
                    row["PurchasUnitCode"] = purchas.PurchasUnitCode;
                    row["PurchasDate"] = purchas.PurchasDate.HasValue ? ((object) purchas.PurchasDate) : ((object) DBNull.Value);
                    row["ProjectCode"] = purchas.ProjectCode;
                    row["Title"] = purchas.Title;
                    row["Description"] = purchas.Description;
                    row["FollowUserCode"] = purchas.FollowUserCode;
                    row["Status"] = purchas.Status;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (MaterialPurchas purchas in entities)
            {
                if (purchas.EntityState == EntityState.Added)
                {
                    purchas.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, int materialPurchasID)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, materialPurchasID);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(MaterialPurchas), new object[] { materialPurchasID }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<MaterialPurchas> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<MaterialPurchas>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@MaterialPurchasCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PurchasUnitCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PurchasDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Title", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Description", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@FollowUserCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("materialpurchasid ") || text.Trim().StartsWith("materialpurchasid="))
                {
                    database.SetParameterValue(command, "@MaterialPurchasID", text.Replace("materialpurchasid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("materialpurchascode ") || text.Trim().StartsWith("materialpurchascode="))
                {
                    database.SetParameterValue(command, "@MaterialPurchasCode", text.Replace("materialpurchascode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("purchasunitcode ") || text.Trim().StartsWith("purchasunitcode="))
                {
                    database.SetParameterValue(command, "@PurchasUnitCode", text.Replace("purchasunitcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("purchasdate ") || text.Trim().StartsWith("purchasdate="))
                {
                    database.SetParameterValue(command, "@PurchasDate", text.Replace("purchasdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("title ") || text.Trim().StartsWith("title="))
                {
                    database.SetParameterValue(command, "@Title", text.Replace("title", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("description ") || text.Trim().StartsWith("description="))
                {
                    database.SetParameterValue(command, "@Description", text.Replace("description", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("followusercode ") || text.Trim().StartsWith("followusercode="))
                {
                    database.SetParameterValue(command, "@FollowUserCode", text.Replace("followusercode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
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
            TList<MaterialPurchas> rows = new TList<MaterialPurchas>();
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
                MaterialPurchasProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<MaterialPurchas> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_Find_Dynamic", typeof(MaterialPurchasColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<MaterialPurchas> rows = new TList<MaterialPurchas>();
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
                MaterialPurchasProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<MaterialPurchas> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<MaterialPurchas> rows = new TList<MaterialPurchas>();
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
                MaterialPurchasProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override MaterialPurchas GetByMaterialPurchasID(TransactionManager transactionManager, int materialPurchasID, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_GetByMaterialPurchasID", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, materialPurchasID);
            IDataReader reader = null;
            TList<MaterialPurchas> rows = new TList<MaterialPurchas>();
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
                MaterialPurchasProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<MaterialPurchas> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_GetByProjectCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, projectCode);
            IDataReader reader = null;
            TList<MaterialPurchas> rows = new TList<MaterialPurchas>();
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
                MaterialPurchasProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<MaterialPurchas> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<MaterialPurchas> rows = new TList<MaterialPurchas>();
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
                    MaterialPurchasProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, MaterialPurchas entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_Insert", this._useStoredProcedure);
            database.AddOutParameter(command, "@MaterialPurchasID", DbType.Int32, 4);
            database.AddInParameter(command, "@MaterialPurchasCode", DbType.AnsiString, entity.MaterialPurchasCode);
            database.AddInParameter(command, "@PurchasUnitCode", DbType.AnsiString, entity.PurchasUnitCode);
            database.AddInParameter(command, "@PurchasDate", DbType.DateTime, entity.PurchasDate.HasValue ? ((object) entity.PurchasDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@Title", DbType.AnsiString, entity.Title);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
            database.AddInParameter(command, "@FollowUserCode", DbType.AnsiString, entity.FollowUserCode);
            database.AddInParameter(command, "@Status", DbType.AnsiString, entity.Status);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.MaterialPurchasID = (int) database.GetParameterValue(command, "@MaterialPurchasID");
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, MaterialPurchas entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchas_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, entity.MaterialPurchasID);
            database.AddInParameter(command, "@MaterialPurchasCode", DbType.AnsiString, entity.MaterialPurchasCode);
            database.AddInParameter(command, "@PurchasUnitCode", DbType.AnsiString, entity.PurchasUnitCode);
            database.AddInParameter(command, "@PurchasDate", DbType.DateTime, entity.PurchasDate.HasValue ? ((object) entity.PurchasDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@Title", DbType.AnsiString, entity.Title);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
            database.AddInParameter(command, "@FollowUserCode", DbType.AnsiString, entity.FollowUserCode);
            database.AddInParameter(command, "@Status", DbType.AnsiString, entity.Status);
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

