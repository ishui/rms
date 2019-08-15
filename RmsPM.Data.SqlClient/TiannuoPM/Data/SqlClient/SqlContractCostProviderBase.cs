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

    public class SqlContractCostProviderBase : ContractCostProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractCostProviderBase()
        {
        }

        public SqlContractCostProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<ContractCost> entities)
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
            copy.DestinationTableName = "ContractCost";
            DataTable table = new DataTable();
            table.Columns.Add("ContractCostCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CostCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Amount", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Money", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("UnitPrise", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Moneycash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("OriginalMoneycash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("MoneyType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ExchangeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("CostBudgetSetCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Description", typeof(string)).AllowDBNull = true;
            table.Columns.Add("OriginalMoney", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractCostCode", "ContractCostCode");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("CostCode", "CostCode");
            copy.ColumnMappings.Add("Amount", "Amount");
            copy.ColumnMappings.Add("Money", "Money");
            copy.ColumnMappings.Add("UnitPrise", "UnitPrise");
            copy.ColumnMappings.Add("Moneycash", "Moneycash");
            copy.ColumnMappings.Add("OriginalMoneycash", "OriginalMoneycash");
            copy.ColumnMappings.Add("MoneyType", "MoneyType");
            copy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
            copy.ColumnMappings.Add("CostBudgetSetCode", "CostBudgetSetCode");
            copy.ColumnMappings.Add("Description", "Description");
            copy.ColumnMappings.Add("OriginalMoney", "OriginalMoney");
            foreach (ContractCost cost in entities)
            {
                if (cost.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractCostCode"] = cost.ContractCostCode;
                    row["ContractCode"] = cost.ContractCode;
                    row["CostCode"] = cost.CostCode;
                    row["Amount"] = cost.Amount.HasValue ? ((object) cost.Amount) : ((object) DBNull.Value);
                    row["Money"] = cost.Money.HasValue ? ((object) cost.Money) : ((object) DBNull.Value);
                    row["UnitPrise"] = cost.UnitPrise.HasValue ? ((object) cost.UnitPrise) : ((object) DBNull.Value);
                    row["Moneycash"] = cost.Moneycash.HasValue ? ((object) cost.Moneycash) : ((object) DBNull.Value);
                    row["OriginalMoneycash"] = cost.OriginalMoneycash.HasValue ? ((object) cost.OriginalMoneycash) : ((object) DBNull.Value);
                    row["MoneyType"] = cost.MoneyType;
                    row["ExchangeRate"] = cost.ExchangeRate.HasValue ? ((object) cost.ExchangeRate) : ((object) DBNull.Value);
                    row["CostBudgetSetCode"] = cost.CostBudgetSetCode;
                    row["Description"] = cost.Description;
                    row["OriginalMoney"] = cost.OriginalMoney.HasValue ? ((object) cost.OriginalMoney) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (ContractCost cost in entities)
            {
                if (cost.EntityState == EntityState.Added)
                {
                    cost.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractCostCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, contractCostCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(ContractCost), new object[] { contractCostCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<ContractCost> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<ContractCost>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Amount", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Money", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@UnitPrise", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Moneycash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@OriginalMoneycash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Description", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractcostcode ") || text.Trim().StartsWith("contractcostcode="))
                {
                    database.SetParameterValue(command, "@ContractCostCode", text.Replace("contractcostcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("costcode ") || text.Trim().StartsWith("costcode="))
                {
                    database.SetParameterValue(command, "@CostCode", text.Replace("costcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("amount ") || text.Trim().StartsWith("amount="))
                {
                    database.SetParameterValue(command, "@Amount", text.Replace("amount", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("money ") || text.Trim().StartsWith("money="))
                {
                    database.SetParameterValue(command, "@Money", text.Replace("money", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("unitprise ") || text.Trim().StartsWith("unitprise="))
                {
                    database.SetParameterValue(command, "@UnitPrise", text.Replace("unitprise", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("moneycash ") || text.Trim().StartsWith("moneycash="))
                {
                    database.SetParameterValue(command, "@Moneycash", text.Replace("moneycash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("originalmoneycash ") || text.Trim().StartsWith("originalmoneycash="))
                {
                    database.SetParameterValue(command, "@OriginalMoneycash", text.Replace("originalmoneycash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("moneytype ") || text.Trim().StartsWith("moneytype="))
                {
                    database.SetParameterValue(command, "@MoneyType", text.Replace("moneytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("exchangerate ") || text.Trim().StartsWith("exchangerate="))
                {
                    database.SetParameterValue(command, "@ExchangeRate", text.Replace("exchangerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("costbudgetsetcode ") || text.Trim().StartsWith("costbudgetsetcode="))
                {
                    database.SetParameterValue(command, "@CostBudgetSetCode", text.Replace("costbudgetsetcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("description ") || text.Trim().StartsWith("description="))
                {
                    database.SetParameterValue(command, "@Description", text.Replace("description", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("originalmoney ") && !text.Trim().StartsWith("originalmoney="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@OriginalMoney", text.Replace("originalmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<ContractCost> rows = new TList<ContractCost>();
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
                ContractCostProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractCost> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_Find_Dynamic", typeof(ContractCostColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<ContractCost> rows = new TList<ContractCost>();
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
                ContractCostProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<ContractCost> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<ContractCost> rows = new TList<ContractCost>();
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
                ContractCostProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractCost> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_GetByContractCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, contractCode);
            IDataReader reader = null;
            TList<ContractCost> rows = new TList<ContractCost>();
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
                ContractCostProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override ContractCost GetByContractCostCode(TransactionManager transactionManager, string contractCostCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_GetByContractCostCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, contractCostCode);
            IDataReader reader = null;
            TList<ContractCost> rows = new TList<ContractCost>();
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
                ContractCostProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractCost> GetByCostCode(TransactionManager transactionManager, string costCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_GetByCostCode", this._useStoredProcedure);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, costCode);
            IDataReader reader = null;
            TList<ContractCost> rows = new TList<ContractCost>();
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
                ContractCostProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractCost> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<ContractCost> rows = new TList<ContractCost>();
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
                    ContractCostProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, ContractCost entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, entity.ContractCostCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, entity.CostCode);
            database.AddInParameter(command, "@Amount", DbType.Decimal, entity.Amount.HasValue ? ((object) entity.Amount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UnitPrise", DbType.Decimal, entity.UnitPrise.HasValue ? ((object) entity.UnitPrise) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Moneycash", DbType.Decimal, entity.Moneycash.HasValue ? ((object) entity.Moneycash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoneycash", DbType.Decimal, entity.OriginalMoneycash.HasValue ? ((object) entity.OriginalMoneycash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, entity.CostBudgetSetCode);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalContractCostCode = entity.ContractCostCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, ContractCost entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCost_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, entity.ContractCostCode);
            database.AddInParameter(command, "@OriginalContractCostCode", DbType.AnsiString, entity.OriginalContractCostCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, entity.CostCode);
            database.AddInParameter(command, "@Amount", DbType.Decimal, entity.Amount.HasValue ? ((object) entity.Amount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UnitPrise", DbType.Decimal, entity.UnitPrise.HasValue ? ((object) entity.UnitPrise) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Moneycash", DbType.Decimal, entity.Moneycash.HasValue ? ((object) entity.Moneycash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoneycash", DbType.Decimal, entity.OriginalMoneycash.HasValue ? ((object) entity.OriginalMoneycash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, entity.CostBudgetSetCode);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
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
            entity.OriginalContractCostCode = entity.ContractCostCode;
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

