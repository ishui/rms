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

    public class SqlContractMaterialProviderBase : ContractMaterialProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractMaterialProviderBase()
        {
        }

        public SqlContractMaterialProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<ContractMaterial> entities)
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
            copy.DestinationTableName = "ContractMaterial";
            DataTable table = new DataTable();
            table.Columns.Add("ContractMaterialCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("MaterialCode", typeof(int)).AllowDBNull = true;
            table.Columns.Add("Qty", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Price", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Money", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractMaterialCode", "ContractMaterialCode");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("MaterialCode", "MaterialCode");
            copy.ColumnMappings.Add("Qty", "Qty");
            copy.ColumnMappings.Add("Price", "Price");
            copy.ColumnMappings.Add("Money", "Money");
            foreach (ContractMaterial material in entities)
            {
                if (material.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractMaterialCode"] = material.ContractMaterialCode;
                    row["ContractCode"] = material.ContractCode;
                    row["MaterialCode"] = material.MaterialCode.HasValue ? ((object) material.MaterialCode) : ((object) DBNull.Value);
                    row["Qty"] = material.Qty.HasValue ? ((object) material.Qty) : ((object) DBNull.Value);
                    row["Price"] = material.Price.HasValue ? ((object) material.Price) : ((object) DBNull.Value);
                    row["Money"] = material.Money.HasValue ? ((object) material.Money) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (ContractMaterial material in entities)
            {
                if (material.EntityState == EntityState.Added)
                {
                    material.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractMaterialCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, contractMaterialCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(ContractMaterial), new object[] { contractMaterialCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<ContractMaterial> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<ContractMaterial>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@Qty", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Price", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Money", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractmaterialcode ") || text.Trim().StartsWith("contractmaterialcode="))
                {
                    database.SetParameterValue(command, "@ContractMaterialCode", text.Replace("contractmaterialcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("materialcode ") || text.Trim().StartsWith("materialcode="))
                {
                    database.SetParameterValue(command, "@MaterialCode", text.Replace("materialcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("qty ") || text.Trim().StartsWith("qty="))
                {
                    database.SetParameterValue(command, "@Qty", text.Replace("qty", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("price ") || text.Trim().StartsWith("price="))
                {
                    database.SetParameterValue(command, "@Price", text.Replace("price", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
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
            TList<ContractMaterial> rows = new TList<ContractMaterial>();
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
                ContractMaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractMaterial> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_Find_Dynamic", typeof(ContractMaterialColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<ContractMaterial> rows = new TList<ContractMaterial>();
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
                ContractMaterialProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<ContractMaterial> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<ContractMaterial> rows = new TList<ContractMaterial>();
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
                ContractMaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractMaterial> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_GetByContractCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, contractCode);
            IDataReader reader = null;
            TList<ContractMaterial> rows = new TList<ContractMaterial>();
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
                ContractMaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override ContractMaterial GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_GetByContractMaterialCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, contractMaterialCode);
            IDataReader reader = null;
            TList<ContractMaterial> rows = new TList<ContractMaterial>();
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
                ContractMaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractMaterial> GetByMaterialCode(TransactionManager transactionManager, int? materialCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_GetByMaterialCode", this._useStoredProcedure);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, materialCode);
            IDataReader reader = null;
            TList<ContractMaterial> rows = new TList<ContractMaterial>();
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
                ContractMaterialProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractMaterial> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<ContractMaterial> rows = new TList<ContractMaterial>();
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
                    ContractMaterialProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, ContractMaterial entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, entity.ContractMaterialCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, entity.MaterialCode.HasValue ? ((object) entity.MaterialCode) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Qty", DbType.Decimal, entity.Qty.HasValue ? ((object) entity.Qty) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Price", DbType.Decimal, entity.Price.HasValue ? ((object) entity.Price) : ((object) DBNull.Value));
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
            entity.OriginalContractMaterialCode = entity.ContractMaterialCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, ContractMaterial entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterial_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, entity.ContractMaterialCode);
            database.AddInParameter(command, "@OriginalContractMaterialCode", DbType.AnsiString, entity.OriginalContractMaterialCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@MaterialCode", DbType.Int32, entity.MaterialCode.HasValue ? ((object) entity.MaterialCode) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Qty", DbType.Decimal, entity.Qty.HasValue ? ((object) entity.Qty) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Price", DbType.Decimal, entity.Price.HasValue ? ((object) entity.Price) : ((object) DBNull.Value));
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
            entity.OriginalContractMaterialCode = entity.ContractMaterialCode;
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

