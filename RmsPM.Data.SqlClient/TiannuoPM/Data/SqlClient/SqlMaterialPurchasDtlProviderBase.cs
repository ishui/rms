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

    public class SqlMaterialPurchasDtlProviderBase : MaterialPurchasDtlProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlMaterialPurchasDtlProviderBase()
        {
        }

        public SqlMaterialPurchasDtlProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<MaterialPurchasDtl> entities)
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
            copy.DestinationTableName = "MaterialPurchasDtl";
            DataTable table = new DataTable();
            table.Columns.Add("MaterialPurchasDtlID", typeof(int)).AllowDBNull = false;
            table.Columns.Add("MaterialPurchasID", typeof(int)).AllowDBNull = true;
            table.Columns.Add("TypeStandard", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Unit", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Number", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("NeedDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("SignDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("SearchPriceDtl", typeof(string)).AllowDBNull = true;
            table.Columns.Add("FinalPrice", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("MaterialPurchasDtlID", "MaterialPurchasDtlID");
            copy.ColumnMappings.Add("MaterialPurchasID", "MaterialPurchasID");
            copy.ColumnMappings.Add("TypeStandard", "TypeStandard");
            copy.ColumnMappings.Add("Unit", "Unit");
            copy.ColumnMappings.Add("Number", "Number");
            copy.ColumnMappings.Add("NeedDate", "NeedDate");
            copy.ColumnMappings.Add("SignDate", "SignDate");
            copy.ColumnMappings.Add("SearchPriceDtl", "SearchPriceDtl");
            copy.ColumnMappings.Add("FinalPrice", "FinalPrice");
            foreach (MaterialPurchasDtl dtl in entities)
            {
                if (dtl.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["MaterialPurchasDtlID"] = dtl.MaterialPurchasDtlID;
                    row["MaterialPurchasID"] = dtl.MaterialPurchasID.HasValue ? ((object) dtl.MaterialPurchasID) : ((object) DBNull.Value);
                    row["TypeStandard"] = dtl.TypeStandard;
                    row["Unit"] = dtl.Unit;
                    row["Number"] = dtl.Number.HasValue ? ((object) dtl.Number) : ((object) DBNull.Value);
                    row["NeedDate"] = dtl.NeedDate.HasValue ? ((object) dtl.NeedDate) : ((object) DBNull.Value);
                    row["SignDate"] = dtl.SignDate.HasValue ? ((object) dtl.SignDate) : ((object) DBNull.Value);
                    row["SearchPriceDtl"] = dtl.SearchPriceDtl;
                    row["FinalPrice"] = dtl.FinalPrice.HasValue ? ((object) dtl.FinalPrice) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (MaterialPurchasDtl dtl in entities)
            {
                if (dtl.EntityState == EntityState.Added)
                {
                    dtl.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, int materialPurchasDtlID)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialPurchasDtlID", DbType.Int32, materialPurchasDtlID);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(MaterialPurchasDtl), new object[] { materialPurchasDtlID }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<MaterialPurchasDtl> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<MaterialPurchasDtl>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@MaterialPurchasDtlID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@TypeStandard", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Unit", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Number", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@NeedDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@SignDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@SearchPriceDtl", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@FinalPrice", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("materialpurchasdtlid ") || text.Trim().StartsWith("materialpurchasdtlid="))
                {
                    database.SetParameterValue(command, "@MaterialPurchasDtlID", text.Replace("materialpurchasdtlid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("materialpurchasid ") || text.Trim().StartsWith("materialpurchasid="))
                {
                    database.SetParameterValue(command, "@MaterialPurchasID", text.Replace("materialpurchasid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("typestandard ") || text.Trim().StartsWith("typestandard="))
                {
                    database.SetParameterValue(command, "@TypeStandard", text.Replace("typestandard", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("unit ") || text.Trim().StartsWith("unit="))
                {
                    database.SetParameterValue(command, "@Unit", text.Replace("unit", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("number ") || text.Trim().StartsWith("number="))
                {
                    database.SetParameterValue(command, "@Number", text.Replace("number", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("needdate ") || text.Trim().StartsWith("needdate="))
                {
                    database.SetParameterValue(command, "@NeedDate", text.Replace("needdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("signdate ") || text.Trim().StartsWith("signdate="))
                {
                    database.SetParameterValue(command, "@SignDate", text.Replace("signdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("searchpricedtl ") || text.Trim().StartsWith("searchpricedtl="))
                {
                    database.SetParameterValue(command, "@SearchPriceDtl", text.Replace("searchpricedtl", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("finalprice ") && !text.Trim().StartsWith("finalprice="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@FinalPrice", text.Replace("finalprice", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<MaterialPurchasDtl> rows = new TList<MaterialPurchasDtl>();
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
                MaterialPurchasDtlProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<MaterialPurchasDtl> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_Find_Dynamic", typeof(MaterialPurchasDtlColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<MaterialPurchasDtl> rows = new TList<MaterialPurchasDtl>();
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
                MaterialPurchasDtlProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<MaterialPurchasDtl> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<MaterialPurchasDtl> rows = new TList<MaterialPurchasDtl>();
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
                MaterialPurchasDtlProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override MaterialPurchasDtl GetByMaterialPurchasDtlID(TransactionManager transactionManager, int materialPurchasDtlID, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_GetByMaterialPurchasDtlID", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialPurchasDtlID", DbType.Int32, materialPurchasDtlID);
            IDataReader reader = null;
            TList<MaterialPurchasDtl> rows = new TList<MaterialPurchasDtl>();
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
                MaterialPurchasDtlProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<MaterialPurchasDtl> GetByMaterialPurchasID(TransactionManager transactionManager, int? materialPurchasID, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_GetByMaterialPurchasID", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, materialPurchasID);
            IDataReader reader = null;
            TList<MaterialPurchasDtl> rows = new TList<MaterialPurchasDtl>();
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
                MaterialPurchasDtlProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<MaterialPurchasDtl> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<MaterialPurchasDtl> rows = new TList<MaterialPurchasDtl>();
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
                    MaterialPurchasDtlProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, MaterialPurchasDtl entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_Insert", this._useStoredProcedure);
            database.AddOutParameter(command, "@MaterialPurchasDtlID", DbType.Int32, 4);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, entity.MaterialPurchasID.HasValue ? ((object) entity.MaterialPurchasID) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TypeStandard", DbType.AnsiString, entity.TypeStandard);
            database.AddInParameter(command, "@Unit", DbType.AnsiString, entity.Unit);
            database.AddInParameter(command, "@Number", DbType.Decimal, entity.Number.HasValue ? ((object) entity.Number) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NeedDate", DbType.DateTime, entity.NeedDate.HasValue ? ((object) entity.NeedDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SignDate", DbType.DateTime, entity.SignDate.HasValue ? ((object) entity.SignDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SearchPriceDtl", DbType.AnsiString, entity.SearchPriceDtl);
            database.AddInParameter(command, "@FinalPrice", DbType.Decimal, entity.FinalPrice.HasValue ? ((object) entity.FinalPrice) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.MaterialPurchasDtlID = (int) database.GetParameterValue(command, "@MaterialPurchasDtlID");
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, MaterialPurchasDtl entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.MaterialPurchasDtl_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialPurchasDtlID", DbType.Int32, entity.MaterialPurchasDtlID);
            database.AddInParameter(command, "@MaterialPurchasID", DbType.Int32, entity.MaterialPurchasID.HasValue ? ((object) entity.MaterialPurchasID) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TypeStandard", DbType.AnsiString, entity.TypeStandard);
            database.AddInParameter(command, "@Unit", DbType.AnsiString, entity.Unit);
            database.AddInParameter(command, "@Number", DbType.Decimal, entity.Number.HasValue ? ((object) entity.Number) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NeedDate", DbType.DateTime, entity.NeedDate.HasValue ? ((object) entity.NeedDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SignDate", DbType.DateTime, entity.SignDate.HasValue ? ((object) entity.SignDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SearchPriceDtl", DbType.AnsiString, entity.SearchPriceDtl);
            database.AddInParameter(command, "@FinalPrice", DbType.Decimal, entity.FinalPrice.HasValue ? ((object) entity.FinalPrice) : ((object) DBNull.Value));
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

