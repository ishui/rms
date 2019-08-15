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

    public class SqlContractMaterialPlanProviderBase : ContractMaterialPlanProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractMaterialPlanProviderBase()
        {
        }

        public SqlContractMaterialPlanProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<ContractMaterialPlan> entities)
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
            copy.DestinationTableName = "ContractMaterialPlan";
            DataTable table = new DataTable();
            table.Columns.Add("ContractMaterialPlanCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractMaterialCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PlanDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("PlanQty", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractMaterialPlanCode", "ContractMaterialPlanCode");
            copy.ColumnMappings.Add("ContractMaterialCode", "ContractMaterialCode");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("PlanDate", "PlanDate");
            copy.ColumnMappings.Add("PlanQty", "PlanQty");
            foreach (ContractMaterialPlan plan in entities)
            {
                if (plan.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractMaterialPlanCode"] = plan.ContractMaterialPlanCode;
                    row["ContractMaterialCode"] = plan.ContractMaterialCode;
                    row["ContractCode"] = plan.ContractCode;
                    row["PlanDate"] = plan.PlanDate.HasValue ? ((object) plan.PlanDate) : ((object) DBNull.Value);
                    row["PlanQty"] = plan.PlanQty.HasValue ? ((object) plan.PlanQty) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (ContractMaterialPlan plan in entities)
            {
                if (plan.EntityState == EntityState.Added)
                {
                    plan.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractMaterialPlanCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialPlanCode", DbType.AnsiString, contractMaterialPlanCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(ContractMaterialPlan), new object[] { contractMaterialPlanCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<ContractMaterialPlan> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<ContractMaterialPlan>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractMaterialPlanCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PlanDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@PlanQty", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractmaterialplancode ") || text.Trim().StartsWith("contractmaterialplancode="))
                {
                    database.SetParameterValue(command, "@ContractMaterialPlanCode", text.Replace("contractmaterialplancode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractmaterialcode ") || text.Trim().StartsWith("contractmaterialcode="))
                {
                    database.SetParameterValue(command, "@ContractMaterialCode", text.Replace("contractmaterialcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("plandate ") || text.Trim().StartsWith("plandate="))
                {
                    database.SetParameterValue(command, "@PlanDate", text.Replace("plandate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("planqty ") && !text.Trim().StartsWith("planqty="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@PlanQty", text.Replace("planqty", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<ContractMaterialPlan> rows = new TList<ContractMaterialPlan>();
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
                ContractMaterialPlanProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractMaterialPlan> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_Find_Dynamic", typeof(ContractMaterialPlanColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<ContractMaterialPlan> rows = new TList<ContractMaterialPlan>();
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
                ContractMaterialPlanProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<ContractMaterialPlan> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<ContractMaterialPlan> rows = new TList<ContractMaterialPlan>();
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
                ContractMaterialPlanProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractMaterialPlan> GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_GetByContractMaterialCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, contractMaterialCode);
            IDataReader reader = null;
            TList<ContractMaterialPlan> rows = new TList<ContractMaterialPlan>();
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
                ContractMaterialPlanProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override ContractMaterialPlan GetByContractMaterialPlanCode(TransactionManager transactionManager, string contractMaterialPlanCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_GetByContractMaterialPlanCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialPlanCode", DbType.AnsiString, contractMaterialPlanCode);
            IDataReader reader = null;
            TList<ContractMaterialPlan> rows = new TList<ContractMaterialPlan>();
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
                ContractMaterialPlanProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractMaterialPlan> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<ContractMaterialPlan> rows = new TList<ContractMaterialPlan>();
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
                    ContractMaterialPlanProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, ContractMaterialPlan entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialPlanCode", DbType.AnsiString, entity.ContractMaterialPlanCode);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, entity.ContractMaterialCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@PlanDate", DbType.DateTime, entity.PlanDate.HasValue ? ((object) entity.PlanDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanQty", DbType.Decimal, entity.PlanQty.HasValue ? ((object) entity.PlanQty) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalContractMaterialPlanCode = entity.ContractMaterialPlanCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, ContractMaterialPlan entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractMaterialPlan_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractMaterialPlanCode", DbType.AnsiString, entity.ContractMaterialPlanCode);
            database.AddInParameter(command, "@OriginalContractMaterialPlanCode", DbType.AnsiString, entity.OriginalContractMaterialPlanCode);
            database.AddInParameter(command, "@ContractMaterialCode", DbType.AnsiString, entity.ContractMaterialCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@PlanDate", DbType.DateTime, entity.PlanDate.HasValue ? ((object) entity.PlanDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanQty", DbType.Decimal, entity.PlanQty.HasValue ? ((object) entity.PlanQty) : ((object) DBNull.Value));
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
            entity.OriginalContractMaterialPlanCode = entity.ContractMaterialPlanCode;
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

