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

    public class SqlMaterialProviderBase : MaterialProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlMaterialProviderBase()
        {
        }

        public SqlMaterialProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<Material> entities)
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
            copy.DestinationTableName = "Material";
            DataTable table = new DataTable();
            table.Columns.Add("MaterialCode", typeof(int)).AllowDBNull = false;
            table.Columns.Add("MaterialName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("GroupCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Spec", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Unit", typeof(string)).AllowDBNull = true;
            table.Columns.Add("StandardPrice", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("InputPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("InputDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("MaterialCode", "MaterialCode");
            copy.ColumnMappings.Add("MaterialName", "MaterialName");
            copy.ColumnMappings.Add("GroupCode", "GroupCode");
            copy.ColumnMappings.Add("Spec", "Spec");
            copy.ColumnMappings.Add("Unit", "Unit");
            copy.ColumnMappings.Add("StandardPrice", "StandardPrice");
            copy.ColumnMappings.Add("InputPerson", "InputPerson");
            copy.ColumnMappings.Add("InputDate", "InputDate");
            copy.ColumnMappings.Add("Remark", "Remark");
            foreach (Material material in entities)
            {
                if (material.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["MaterialCode"] = material.MaterialCode;
                    row["MaterialName"] = material.MaterialName;
                    row["GroupCode"] = material.GroupCode;
                    row["Spec"] = material.Spec;
                    row["Unit"] = material.Unit;
                    row["StandardPrice"] = material.StandardPrice.HasValue ? ((object) material.StandardPrice) : ((object) DBNull.Value);
                    row["InputPerson"] = material.InputPerson;
                    row["InputDate"] = material.InputDate.HasValue ? ((object) material.InputDate) : ((object) DBNull.Value);
                    row["Remark"] = material.Remark;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (Material material in entities)
            {
                if (material.EntityState == EntityState.Added)
                {
                    material.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, int materialCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, materialCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(Material), new object[] { materialCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<Material> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<Material>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@MaterialName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Spec", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Unit", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@StandardPrice", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@InputPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@InputDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("materialcode ") || text.Trim().StartsWith("materialcode="))
                {
                    database.SetParameterValue(command, "@MaterialCode", text.Replace("materialcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("materialname ") || text.Trim().StartsWith("materialname="))
                {
                    database.SetParameterValue(command, "@MaterialName", text.Replace("materialname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("groupcode ") || text.Trim().StartsWith("groupcode="))
                {
                    database.SetParameterValue(command, "@GroupCode", text.Replace("groupcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("spec ") || text.Trim().StartsWith("spec="))
                {
                    database.SetParameterValue(command, "@Spec", text.Replace("spec", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("unit ") || text.Trim().StartsWith("unit="))
                {
                    database.SetParameterValue(command, "@Unit", text.Replace("unit", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("standardprice ") || text.Trim().StartsWith("standardprice="))
                {
                    database.SetParameterValue(command, "@StandardPrice", text.Replace("standardprice", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inputperson ") || text.Trim().StartsWith("inputperson="))
                {
                    database.SetParameterValue(command, "@InputPerson", text.Replace("inputperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("inputdate ") || text.Trim().StartsWith("inputdate="))
                {
                    database.SetParameterValue(command, "@InputDate", text.Replace("inputdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
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
            TList<Material> rows = new TList<Material>();
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
                MaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Material> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_Find_Dynamic", typeof(MaterialColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<Material> rows = new TList<Material>();
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
                MaterialProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<Material> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<Material> rows = new TList<Material>();
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
                MaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override Material GetByMaterialCode(TransactionManager transactionManager, int materialCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_GetByMaterialCode", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, materialCode);
            IDataReader reader = null;
            TList<Material> rows = new TList<Material>();
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
                MaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Material> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<Material> rows = new TList<Material>();
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
                    MaterialProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, Material entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, entity.MaterialCode);
            database.AddInParameter(command, "@MaterialName", DbType.AnsiString, entity.MaterialName);
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, entity.GroupCode);
            database.AddInParameter(command, "@Spec", DbType.AnsiString, entity.Spec);
            database.AddInParameter(command, "@Unit", DbType.AnsiString, entity.Unit);
            database.AddInParameter(command, "@StandardPrice", DbType.Decimal, entity.StandardPrice.HasValue ? ((object) entity.StandardPrice) : ((object) DBNull.Value));
            database.AddInParameter(command, "@InputPerson", DbType.AnsiString, entity.InputPerson);
            database.AddInParameter(command, "@InputDate", DbType.DateTime, entity.InputDate.HasValue ? ((object) entity.InputDate) : ((object) DBNull.Value));
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
            entity.OriginalMaterialCode = entity.MaterialCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, Material entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Material_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, entity.MaterialCode);
            database.AddInParameter(command, "@OriginalMaterialCode", DbType.Int32, entity.OriginalMaterialCode);
            database.AddInParameter(command, "@MaterialName", DbType.AnsiString, entity.MaterialName);
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, entity.GroupCode);
            database.AddInParameter(command, "@Spec", DbType.AnsiString, entity.Spec);
            database.AddInParameter(command, "@Unit", DbType.AnsiString, entity.Unit);
            database.AddInParameter(command, "@StandardPrice", DbType.Decimal, entity.StandardPrice.HasValue ? ((object) entity.StandardPrice) : ((object) DBNull.Value));
            database.AddInParameter(command, "@InputPerson", DbType.AnsiString, entity.InputPerson);
            database.AddInParameter(command, "@InputDate", DbType.DateTime, entity.InputDate.HasValue ? ((object) entity.InputDate) : ((object) DBNull.Value));
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
            entity.OriginalMaterialCode = entity.MaterialCode;
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

