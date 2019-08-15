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

    public class SqlContractChangeProviderBase : ContractChangeProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractChangeProviderBase()
        {
        }

        public SqlContractChangeProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<ContractChange> entities)
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
            copy.DestinationTableName = "ContractChange";
            DataTable table = new DataTable();
            table.Columns.Add("ContractChangeCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractChangeId", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Voucher", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Money", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ChangeMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("NewMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("OriginalMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("TotalChangeMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("SupplierChangeMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ConsultantAuditMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ProjectAuditMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ChangeReason", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            table.Columns.Add("ChangePerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ChangeDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("ChangeType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CheckPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CheckDate", typeof(DateTime)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractChangeCode", "ContractChangeCode");
            copy.ColumnMappings.Add("ContractChangeId", "ContractChangeId");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("Voucher", "Voucher");
            copy.ColumnMappings.Add("Money", "Money");
            copy.ColumnMappings.Add("ChangeMoney", "ChangeMoney");
            copy.ColumnMappings.Add("NewMoney", "NewMoney");
            copy.ColumnMappings.Add("OriginalMoney", "OriginalMoney");
            copy.ColumnMappings.Add("TotalChangeMoney", "TotalChangeMoney");
            copy.ColumnMappings.Add("SupplierChangeMoney", "SupplierChangeMoney");
            copy.ColumnMappings.Add("ConsultantAuditMoney", "ConsultantAuditMoney");
            copy.ColumnMappings.Add("ProjectAuditMoney", "ProjectAuditMoney");
            copy.ColumnMappings.Add("ChangeReason", "ChangeReason");
            copy.ColumnMappings.Add("Status", "Status");
            copy.ColumnMappings.Add("ChangePerson", "ChangePerson");
            copy.ColumnMappings.Add("ChangeDate", "ChangeDate");
            copy.ColumnMappings.Add("ChangeType", "ChangeType");
            copy.ColumnMappings.Add("CheckPerson", "CheckPerson");
            copy.ColumnMappings.Add("CheckDate", "CheckDate");
            foreach (ContractChange change in entities)
            {
                if (change.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractChangeCode"] = change.ContractChangeCode;
                    row["ContractChangeId"] = change.ContractChangeId;
                    row["ContractCode"] = change.ContractCode;
                    row["Voucher"] = change.Voucher;
                    row["Money"] = change.Money.HasValue ? ((object) change.Money) : ((object) DBNull.Value);
                    row["ChangeMoney"] = change.ChangeMoney.HasValue ? ((object) change.ChangeMoney) : ((object) DBNull.Value);
                    row["NewMoney"] = change.NewMoney.HasValue ? ((object) change.NewMoney) : ((object) DBNull.Value);
                    row["OriginalMoney"] = change.OriginalMoney.HasValue ? ((object) change.OriginalMoney) : ((object) DBNull.Value);
                    row["TotalChangeMoney"] = change.TotalChangeMoney.HasValue ? ((object) change.TotalChangeMoney) : ((object) DBNull.Value);
                    row["SupplierChangeMoney"] = change.SupplierChangeMoney.HasValue ? ((object) change.SupplierChangeMoney) : ((object) DBNull.Value);
                    row["ConsultantAuditMoney"] = change.ConsultantAuditMoney.HasValue ? ((object) change.ConsultantAuditMoney) : ((object) DBNull.Value);
                    row["ProjectAuditMoney"] = change.ProjectAuditMoney.HasValue ? ((object) change.ProjectAuditMoney) : ((object) DBNull.Value);
                    row["ChangeReason"] = change.ChangeReason;
                    row["Status"] = change.Status.HasValue ? ((object) change.Status) : ((object) DBNull.Value);
                    row["ChangePerson"] = change.ChangePerson;
                    row["ChangeDate"] = change.ChangeDate.HasValue ? ((object) change.ChangeDate) : ((object) DBNull.Value);
                    row["ChangeType"] = change.ChangeType;
                    row["CheckPerson"] = change.CheckPerson;
                    row["CheckDate"] = change.CheckDate.HasValue ? ((object) change.CheckDate) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (ContractChange change in entities)
            {
                if (change.EntityState == EntityState.Added)
                {
                    change.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractChangeCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, contractChangeCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(ContractChange), new object[] { contractChangeCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<ContractChange> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<ContractChange>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractChangeId", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Voucher", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Money", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ChangeMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@NewMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@TotalChangeMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@SupplierChangeMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ConsultantAuditMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ProjectAuditMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ChangeReason", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@ChangePerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ChangeDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@ChangeType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractchangecode ") || text.Trim().StartsWith("contractchangecode="))
                {
                    database.SetParameterValue(command, "@ContractChangeCode", text.Replace("contractchangecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractchangeid ") || text.Trim().StartsWith("contractchangeid="))
                {
                    database.SetParameterValue(command, "@ContractChangeId", text.Replace("contractchangeid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("voucher ") || text.Trim().StartsWith("voucher="))
                {
                    database.SetParameterValue(command, "@Voucher", text.Replace("voucher", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
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
                else if (text.Trim().StartsWith("supplierchangemoney ") || text.Trim().StartsWith("supplierchangemoney="))
                {
                    database.SetParameterValue(command, "@SupplierChangeMoney", text.Replace("supplierchangemoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("consultantauditmoney ") || text.Trim().StartsWith("consultantauditmoney="))
                {
                    database.SetParameterValue(command, "@ConsultantAuditMoney", text.Replace("consultantauditmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectauditmoney ") || text.Trim().StartsWith("projectauditmoney="))
                {
                    database.SetParameterValue(command, "@ProjectAuditMoney", text.Replace("projectauditmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changereason ") || text.Trim().StartsWith("changereason="))
                {
                    database.SetParameterValue(command, "@ChangeReason", text.Replace("changereason", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("status ") || text.Trim().StartsWith("status="))
                {
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changeperson ") || text.Trim().StartsWith("changeperson="))
                {
                    database.SetParameterValue(command, "@ChangePerson", text.Replace("changeperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changedate ") || text.Trim().StartsWith("changedate="))
                {
                    database.SetParameterValue(command, "@ChangeDate", text.Replace("changedate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changetype ") || text.Trim().StartsWith("changetype="))
                {
                    database.SetParameterValue(command, "@ChangeType", text.Replace("changetype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkperson ") || text.Trim().StartsWith("checkperson="))
                {
                    database.SetParameterValue(command, "@CheckPerson", text.Replace("checkperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("checkdate ") && !text.Trim().StartsWith("checkdate="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@CheckDate", text.Replace("checkdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<ContractChange> rows = new TList<ContractChange>();
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
                ContractChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractChange> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_Find_Dynamic", typeof(ContractChangeColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<ContractChange> rows = new TList<ContractChange>();
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
                ContractChangeProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<ContractChange> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<ContractChange> rows = new TList<ContractChange>();
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
                ContractChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override ContractChange GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_GetByContractChangeCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, contractChangeCode);
            IDataReader reader = null;
            TList<ContractChange> rows = new TList<ContractChange>();
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
                ContractChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractChange> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_GetByContractCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, contractCode);
            IDataReader reader = null;
            TList<ContractChange> rows = new TList<ContractChange>();
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
                ContractChangeProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<ContractChange> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<ContractChange> rows = new TList<ContractChange>();
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
                    ContractChangeProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, ContractChange entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
            database.AddInParameter(command, "@ContractChangeId", DbType.AnsiString, entity.ContractChangeId);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@Voucher", DbType.AnsiString, entity.Voucher);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeMoney", DbType.Decimal, entity.ChangeMoney.HasValue ? ((object) entity.ChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NewMoney", DbType.Decimal, entity.NewMoney.HasValue ? ((object) entity.NewMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalChangeMoney", DbType.Decimal, entity.TotalChangeMoney.HasValue ? ((object) entity.TotalChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SupplierChangeMoney", DbType.Decimal, entity.SupplierChangeMoney.HasValue ? ((object) entity.SupplierChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ConsultantAuditMoney", DbType.Decimal, entity.ConsultantAuditMoney.HasValue ? ((object) entity.ConsultantAuditMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectAuditMoney", DbType.Decimal, entity.ProjectAuditMoney.HasValue ? ((object) entity.ProjectAuditMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeReason", DbType.AnsiString, entity.ChangeReason);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangePerson", DbType.AnsiString, entity.ChangePerson);
            database.AddInParameter(command, "@ChangeDate", DbType.DateTime, entity.ChangeDate.HasValue ? ((object) entity.ChangeDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeType", DbType.AnsiString, entity.ChangeType);
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalContractChangeCode = entity.ContractChangeCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, ContractChange entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.ContractChange_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractChangeCode", DbType.AnsiString, entity.ContractChangeCode);
            database.AddInParameter(command, "@OriginalContractChangeCode", DbType.AnsiString, entity.OriginalContractChangeCode);
            database.AddInParameter(command, "@ContractChangeId", DbType.AnsiString, entity.ContractChangeId);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@Voucher", DbType.AnsiString, entity.Voucher);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeMoney", DbType.Decimal, entity.ChangeMoney.HasValue ? ((object) entity.ChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@NewMoney", DbType.Decimal, entity.NewMoney.HasValue ? ((object) entity.NewMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalChangeMoney", DbType.Decimal, entity.TotalChangeMoney.HasValue ? ((object) entity.TotalChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SupplierChangeMoney", DbType.Decimal, entity.SupplierChangeMoney.HasValue ? ((object) entity.SupplierChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ConsultantAuditMoney", DbType.Decimal, entity.ConsultantAuditMoney.HasValue ? ((object) entity.ConsultantAuditMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectAuditMoney", DbType.Decimal, entity.ProjectAuditMoney.HasValue ? ((object) entity.ProjectAuditMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeReason", DbType.AnsiString, entity.ChangeReason);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangePerson", DbType.AnsiString, entity.ChangePerson);
            database.AddInParameter(command, "@ChangeDate", DbType.DateTime, entity.ChangeDate.HasValue ? ((object) entity.ChangeDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeType", DbType.AnsiString, entity.ChangeType);
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
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
            entity.OriginalContractChangeCode = entity.ContractChangeCode;
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

