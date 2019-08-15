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

    public class SqlContractCostChangeProviderBase : ContractCostChangeProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractCostChangeProviderBase()
        {
        }

        public SqlContractCostChangeProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<ContractCostChange> entities)
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
            copy.DestinationTableName = "ContractCostChange";
            DataTable table = new DataTable();
            table.Columns.Add("ContractCostChangeCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractCostCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractChangeCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Cash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ChangeCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("NewCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("OriginalCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("TotalChangeCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("MoneyType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ExchangeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Money", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ChangeMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("NewMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("OriginalMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("TotalChangeMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("CostCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CostBudgetSetCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Description", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractCostChangeCode", "ContractCostChangeCode");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("ContractCostCode", "ContractCostCode");
            copy.ColumnMappings.Add("ContractChangeCode", "ContractChangeCode");
            copy.ColumnMappings.Add("Cash", "Cash");
            copy.ColumnMappings.Add("ChangeCash", "ChangeCash");
            copy.ColumnMappings.Add("NewCash", "NewCash");
            copy.ColumnMappings.Add("OriginalCash", "OriginalCash");
            copy.ColumnMappings.Add("TotalChangeCash", "TotalChangeCash");
            copy.ColumnMappings.Add("MoneyType", "MoneyType");
            copy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
            copy.ColumnMappings.Add("Money", "Money");
            copy.ColumnMappings.Add("ChangeMoney", "ChangeMoney");
            copy.ColumnMappings.Add("NewMoney", "NewMoney");
            copy.ColumnMappings.Add("OriginalMoney", "OriginalMoney");
            copy.ColumnMappings.Add("TotalChangeMoney", "TotalChangeMoney");
            copy.ColumnMappings.Add("CostCode", "CostCode");
            copy.ColumnMappings.Add("CostBudgetSetCode", "CostBudgetSetCode");
            copy.ColumnMappings.Add("Description", "Description");
            copy.ColumnMappings.Add("Status", "Status");
            foreach (ContractCostChange change in entities)
            {
                if (change.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractCostChangeCode"] = change.ContractCostChangeCode;
                    row["ContractCode"] = change.ContractCode;
                    row["ContractCostCode"] = change.ContractCostCode;
                    row["ContractChangeCode"] = change.ContractChangeCode;
                    row["Cash"] = change.Cash.HasValue ? ((object) change.Cash) : ((object) DBNull.Value);
                    row["ChangeCash"] = change.ChangeCash.HasValue ? ((object) change.ChangeCash) : ((object) DBNull.Value);
                    row["NewCash"] = change.NewCash.HasValue ? ((object) change.NewCash) : ((object) DBNull.Value);
                    row["OriginalCash"] = change.OriginalCash.HasValue ? ((object) change.OriginalCash) : ((object) DBNull.Value);
                    row["TotalChangeCash"] = change.TotalChangeCash.HasValue ? ((object) change.TotalChangeCash) : ((object) DBNull.Value);
                    row["MoneyType"] = change.MoneyType;
                    row["ExchangeRate"] = change.ExchangeRate.HasValue ? ((object) change.ExchangeRate) : ((object) DBNull.Value);
                    row["Money"] = change.Money.HasValue ? ((object) change.Money) : ((object) DBNull.Value);
                    row["ChangeMoney"] = change.ChangeMoney.HasValue ? ((object) change.ChangeMoney) : ((object) DBNull.Value);
                    row["NewMoney"] = change.NewMoney.HasValue ? ((object) change.NewMoney) : ((object) DBNull.Value);
                    row["OriginalMoney"] = change.OriginalMoney.HasValue ? ((object) change.OriginalMoney) : ((object) DBNull.Value);
                    row["TotalChangeMoney"] = change.TotalChangeMoney.HasValue ? ((object) change.TotalChangeMoney) : ((object) DBNull.Value);
                    row["CostCode"] = change.CostCode;
                    row["CostBudgetSetCode"] = change.CostBudgetSetCode;
                    row["Description"] = change.Description;
                    row["Status"] = change.Status.HasValue ? ((object) change.Status) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (ContractCostChange change in entities)
            {
                if (change.EntityState == EntityState.Added)
                {
                    change.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractCostChangeCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostChangeCode", DbType.AnsiString, contractCostChangeCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(ContractCostChange), new object[] { contractCostChangeCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<ContractCostChange> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<ContractCostChange>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractCostChangeCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Cash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ChangeCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@NewCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@OriginalCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@TotalChangeCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Money", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ChangeMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@NewMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@TotalChangeMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Description", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractcostchangecode ") || text.Trim().StartsWith("contractcostchangecode="))
                {
                    database.SetParameterValue(command, "@ContractCostChangeCode", text.Replace("contractcostchangecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcostcode ") || text.Trim().StartsWith("contractcostcode="))
                {
                    database.SetParameterValue(command, "@ContractCostCode", text.Replace("contractcostcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractchangecode ") || text.Trim().StartsWith("contractchangecode="))
                {
                    database.SetParameterValue(command, "@ContractChangeCode", text.Replace("contractchangecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("cash ") || text.Trim().StartsWith("cash="))
                {
                    database.SetParameterValue(command, "@Cash", text.Replace("cash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changecash ") || text.Trim().StartsWith("changecash="))
                {
                    database.SetParameterValue(command, "@ChangeCash", text.Replace("changecash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("newcash ") || text.Trim().StartsWith("newcash="))
                {
                    database.SetParameterValue(command, "@NewCash", text.Replace("newcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("originalcash ") || text.Trim().StartsWith("originalcash="))
                {
                    database.SetParameterValue(command, "@OriginalCash", text.Replace("originalcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("totalchangecash ") || text.Trim().StartsWith("totalchangecash="))
                {
                    database.SetParameterValue(command, "@TotalChangeCash", text.Replace("totalchangecash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("moneytype ") || text.Trim().StartsWith("moneytype="))
                {
                    database.SetParameterValue(command, "@MoneyType", text.Replace("moneytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("exchangerate ") || text.Trim().StartsWith("exchangerate="))
                {
                    database.SetParameterValue(command, "@ExchangeRate", text.Replace("exchangerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("money ") || text.Trim().StartsWith("money="))
                {
                    database.SetParameterValue(command, "@Money", text.Replace("money", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changemoney ") || text.Trim().StartsWith("changemoney="))
                {
                    database.SetParameterValue(command, "@ChangeMoney", text.Replace("changemoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("newmoney ") || text.Trim().StartsWith("newmoney="))
                {
                    database.SetParameterValue(command, "@NewMoney", text.Replace("newmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("originalmoney ") || text.Trim().StartsWith("originalmoney="))
                {
                    database.SetParameterValue(command, "@OriginalMoney", text.Replace("originalmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("totalchangemoney ") || text.Trim().StartsWith("totalchangemoney="))
                {
                    database.SetParameterValue(command, "@TotalChangeMoney", text.Replace("totalchangemoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("costcode ") || text.Trim().StartsWith("costcode="))
                {
                    database.SetParameterValue(command, "@CostCode", text.Replace("costcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
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
                    if (!text.Trim().StartsWith("status ") && !text.Trim().StartsWith("status="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<ContractCostChange> rows = new TList<ContractCostChange>();
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
                ContractCostChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractCostChange> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_Find_Dynamic", typeof(ContractCostChangeColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<ContractCostChange> rows = new TList<ContractCostChange>();
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
                ContractCostChangeProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<ContractCostChange> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<ContractCostChange> rows = new TList<ContractCostChange>();
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
                ContractCostChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractCostChange> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_GetByContractChangeCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, contractChangeCode);
            IDataReader reader = null;
            TList<ContractCostChange> rows = new TList<ContractCostChange>();
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
                ContractCostChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override ContractCostChange GetByContractCostChangeCode(TransactionManager transactionManager, string contractCostChangeCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_GetByContractCostChangeCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostChangeCode", DbType.AnsiString, contractCostChangeCode);
            IDataReader reader = null;
            TList<ContractCostChange> rows = new TList<ContractCostChange>();
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
                ContractCostChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractCostChange> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<ContractCostChange> rows = new TList<ContractCostChange>();
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
                    ContractCostChangeProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, ContractCostChange entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostChangeCode", DbType.AnsiString, entity.ContractCostChangeCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, entity.ContractCostCode);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
            database.AddInParameter(command, "@Cash", DbType.Decimal, entity.Cash.HasValue ? ((object) entity.Cash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeCash", DbType.Decimal, entity.ChangeCash.HasValue ? ((object) entity.ChangeCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NewCash", DbType.Decimal, entity.NewCash.HasValue ? ((object) entity.NewCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalCash", DbType.Decimal, entity.OriginalCash.HasValue ? ((object) entity.OriginalCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalChangeCash", DbType.Decimal, entity.TotalChangeCash.HasValue ? ((object) entity.TotalChangeCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeMoney", DbType.Decimal, entity.ChangeMoney.HasValue ? ((object) entity.ChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NewMoney", DbType.Decimal, entity.NewMoney.HasValue ? ((object) entity.NewMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalChangeMoney", DbType.Decimal, entity.TotalChangeMoney.HasValue ? ((object) entity.TotalChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, entity.CostCode);
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, entity.CostBudgetSetCode);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
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
            entity.OriginalContractCostChangeCode = entity.ContractCostChangeCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, ContractCostChange entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractCostChange_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCostChangeCode", DbType.AnsiString, entity.ContractCostChangeCode);
            database.AddInParameter(command, "@OriginalContractCostChangeCode", DbType.AnsiString, entity.OriginalContractCostChangeCode);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@ContractCostCode", DbType.AnsiString, entity.ContractCostCode);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
            database.AddInParameter(command, "@Cash", DbType.Decimal, entity.Cash.HasValue ? ((object) entity.Cash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeCash", DbType.Decimal, entity.ChangeCash.HasValue ? ((object) entity.ChangeCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NewCash", DbType.Decimal, entity.NewCash.HasValue ? ((object) entity.NewCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalCash", DbType.Decimal, entity.OriginalCash.HasValue ? ((object) entity.OriginalCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalChangeCash", DbType.Decimal, entity.TotalChangeCash.HasValue ? ((object) entity.TotalChangeCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeMoney", DbType.Decimal, entity.ChangeMoney.HasValue ? ((object) entity.ChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NewMoney", DbType.Decimal, entity.NewMoney.HasValue ? ((object) entity.NewMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalChangeMoney", DbType.Decimal, entity.TotalChangeMoney.HasValue ? ((object) entity.TotalChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CostCode", DbType.AnsiString, entity.CostCode);
            database.AddInParameter(command, "@CostBudgetSetCode", DbType.AnsiString, entity.CostBudgetSetCode);
            database.AddInParameter(command, "@Description", DbType.AnsiString, entity.Description);
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
            entity.OriginalContractCostChangeCode = entity.ContractCostChangeCode;
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

