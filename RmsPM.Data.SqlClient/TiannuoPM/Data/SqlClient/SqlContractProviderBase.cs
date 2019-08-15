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

    public class SqlContractProviderBase : ContractProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlContractProviderBase()
        {
        }

        public SqlContractProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<Contract> entities)
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
            copy.DestinationTableName = "Contract";
            DataTable table = new DataTable();
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ContractID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Type", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SupplierCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Supplier2Code", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("TotalMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("CreateDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("CreatePerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("LastModifyPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("LastModifyDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            table.Columns.Add("CheckPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CheckOpinion", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CheckDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("ContractObject", typeof(string)).AllowDBNull = true;
            table.Columns.Add("UnitCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ThirdParty", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BeforeAccountTotalMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("oldSumMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("OriginalMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Mostly", typeof(int)).AllowDBNull = true;
            table.Columns.Add("BiddingCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BudgetMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AdjustMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("DevelopUnit", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CreateMode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("WorkTime", typeof(string)).AllowDBNull = true;
            table.Columns.Add("MarkSegment", typeof(string)).AllowDBNull = true;
            table.Columns.Add("GroupName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Building", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PayMode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("QualityRequire", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractArea", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ContractDefaultValueCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BaoHan", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerformingCircs", typeof(string)).AllowDBNull = true;
            table.Columns.Add("AccountStatus", typeof(int)).AllowDBNull = true;
            table.Columns.Add("AuditingStatus", typeof(int)).AllowDBNull = true;
            table.Columns.Add("ChangeStatus", typeof(int)).AllowDBNull = true;
            table.Columns.Add("ChangeCount", typeof(int)).AllowDBNull = true;
            table.Columns.Add("WorkStartDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("WorkEndDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("PerCash0", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash1", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash2", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash3", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash4", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash5", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash6", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash7", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash8", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PerCash9", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("StampDutyID", typeof(int)).AllowDBNull = true;
            table.Columns.Add("StampDuty", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AdIssueDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("MoneyType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ExchangeRate", typeof(decimal)).AllowDBNull = true;
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("ContractID", "ContractID");
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("ContractName", "ContractName");
            copy.ColumnMappings.Add("Type", "Type");
            copy.ColumnMappings.Add("SupplierCode", "SupplierCode");
            copy.ColumnMappings.Add("Supplier2Code", "Supplier2Code");
            copy.ColumnMappings.Add("ContractPerson", "ContractPerson");
            copy.ColumnMappings.Add("ContractDate", "ContractDate");
            copy.ColumnMappings.Add("TotalMoney", "TotalMoney");
            copy.ColumnMappings.Add("CreateDate", "CreateDate");
            copy.ColumnMappings.Add("CreatePerson", "CreatePerson");
            copy.ColumnMappings.Add("LastModifyPerson", "LastModifyPerson");
            copy.ColumnMappings.Add("LastModifyDate", "LastModifyDate");
            copy.ColumnMappings.Add("Remark", "Remark");
            copy.ColumnMappings.Add("Status", "Status");
            copy.ColumnMappings.Add("CheckPerson", "CheckPerson");
            copy.ColumnMappings.Add("CheckOpinion", "CheckOpinion");
            copy.ColumnMappings.Add("CheckDate", "CheckDate");
            copy.ColumnMappings.Add("ContractObject", "ContractObject");
            copy.ColumnMappings.Add("UnitCode", "UnitCode");
            copy.ColumnMappings.Add("ThirdParty", "ThirdParty");
            copy.ColumnMappings.Add("BeforeAccountTotalMoney", "BeforeAccountTotalMoney");
            copy.ColumnMappings.Add("oldSumMoney", "oldSumMoney");
            copy.ColumnMappings.Add("OriginalMoney", "OriginalMoney");
            copy.ColumnMappings.Add("Mostly", "Mostly");
            copy.ColumnMappings.Add("BiddingCode", "BiddingCode");
            copy.ColumnMappings.Add("BudgetMoney", "BudgetMoney");
            copy.ColumnMappings.Add("AdjustMoney", "AdjustMoney");
            copy.ColumnMappings.Add("DevelopUnit", "DevelopUnit");
            copy.ColumnMappings.Add("CreateMode", "CreateMode");
            copy.ColumnMappings.Add("WorkTime", "WorkTime");
            copy.ColumnMappings.Add("MarkSegment", "MarkSegment");
            copy.ColumnMappings.Add("GroupName", "GroupName");
            copy.ColumnMappings.Add("Building", "Building");
            copy.ColumnMappings.Add("PayMode", "PayMode");
            copy.ColumnMappings.Add("QualityRequire", "QualityRequire");
            copy.ColumnMappings.Add("ContractArea", "ContractArea");
            copy.ColumnMappings.Add("ContractDefaultValueCode", "ContractDefaultValueCode");
            copy.ColumnMappings.Add("BaoHan", "BaoHan");
            copy.ColumnMappings.Add("PerformingCircs", "PerformingCircs");
            copy.ColumnMappings.Add("AccountStatus", "AccountStatus");
            copy.ColumnMappings.Add("AuditingStatus", "AuditingStatus");
            copy.ColumnMappings.Add("ChangeStatus", "ChangeStatus");
            copy.ColumnMappings.Add("ChangeCount", "ChangeCount");
            copy.ColumnMappings.Add("WorkStartDate", "WorkStartDate");
            copy.ColumnMappings.Add("WorkEndDate", "WorkEndDate");
            copy.ColumnMappings.Add("PerCash0", "PerCash0");
            copy.ColumnMappings.Add("PerCash1", "PerCash1");
            copy.ColumnMappings.Add("PerCash2", "PerCash2");
            copy.ColumnMappings.Add("PerCash3", "PerCash3");
            copy.ColumnMappings.Add("PerCash4", "PerCash4");
            copy.ColumnMappings.Add("PerCash5", "PerCash5");
            copy.ColumnMappings.Add("PerCash6", "PerCash6");
            copy.ColumnMappings.Add("PerCash7", "PerCash7");
            copy.ColumnMappings.Add("PerCash8", "PerCash8");
            copy.ColumnMappings.Add("PerCash9", "PerCash9");
            copy.ColumnMappings.Add("StampDutyID", "StampDutyID");
            copy.ColumnMappings.Add("StampDuty", "StampDuty");
            copy.ColumnMappings.Add("AdIssueDate", "AdIssueDate");
            copy.ColumnMappings.Add("MoneyType", "MoneyType");
            copy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
            foreach (Contract contract in entities)
            {
                if (contract.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ContractCode"] = contract.ContractCode;
                    row["ContractID"] = contract.ContractID;
                    row["ProjectCode"] = contract.ProjectCode;
                    row["ContractName"] = contract.ContractName;
                    row["Type"] = contract.Type;
                    row["SupplierCode"] = contract.SupplierCode;
                    row["Supplier2Code"] = contract.Supplier2Code;
                    row["ContractPerson"] = contract.ContractPerson;
                    row["ContractDate"] = contract.ContractDate.HasValue ? ((object) contract.ContractDate) : ((object) DBNull.Value);
                    row["TotalMoney"] = contract.TotalMoney.HasValue ? ((object) contract.TotalMoney) : ((object) DBNull.Value);
                    row["CreateDate"] = contract.CreateDate.HasValue ? ((object) contract.CreateDate) : ((object) DBNull.Value);
                    row["CreatePerson"] = contract.CreatePerson;
                    row["LastModifyPerson"] = contract.LastModifyPerson;
                    row["LastModifyDate"] = contract.LastModifyDate.HasValue ? ((object) contract.LastModifyDate) : ((object) DBNull.Value);
                    row["Remark"] = contract.Remark;
                    row["Status"] = contract.Status.HasValue ? ((object) contract.Status) : ((object) DBNull.Value);
                    row["CheckPerson"] = contract.CheckPerson;
                    row["CheckOpinion"] = contract.CheckOpinion;
                    row["CheckDate"] = contract.CheckDate.HasValue ? ((object) contract.CheckDate) : ((object) DBNull.Value);
                    row["ContractObject"] = contract.ContractObject;
                    row["UnitCode"] = contract.UnitCode;
                    row["ThirdParty"] = contract.ThirdParty;
                    row["BeforeAccountTotalMoney"] = contract.BeforeAccountTotalMoney.HasValue ? ((object) contract.BeforeAccountTotalMoney) : ((object) DBNull.Value);
                    row["oldSumMoney"] = contract.OldSumMoney.HasValue ? ((object) contract.OldSumMoney) : ((object) DBNull.Value);
                    row["OriginalMoney"] = contract.OriginalMoney.HasValue ? ((object) contract.OriginalMoney) : ((object) DBNull.Value);
                    row["Mostly"] = contract.Mostly.HasValue ? ((object) contract.Mostly) : ((object) DBNull.Value);
                    row["BiddingCode"] = contract.BiddingCode;
                    row["BudgetMoney"] = contract.BudgetMoney.HasValue ? ((object) contract.BudgetMoney) : ((object) DBNull.Value);
                    row["AdjustMoney"] = contract.AdjustMoney.HasValue ? ((object) contract.AdjustMoney) : ((object) DBNull.Value);
                    row["DevelopUnit"] = contract.DevelopUnit;
                    row["CreateMode"] = contract.CreateMode;
                    row["WorkTime"] = contract.WorkTime;
                    row["MarkSegment"] = contract.MarkSegment;
                    row["GroupName"] = contract.GroupName;
                    row["Building"] = contract.Building;
                    row["PayMode"] = contract.PayMode;
                    row["QualityRequire"] = contract.QualityRequire;
                    row["ContractArea"] = contract.ContractArea;
                    row["ContractDefaultValueCode"] = contract.ContractDefaultValueCode;
                    row["BaoHan"] = contract.BaoHan.HasValue ? ((object) contract.BaoHan) : ((object) DBNull.Value);
                    row["PerformingCircs"] = contract.PerformingCircs;
                    row["AccountStatus"] = contract.AccountStatus.HasValue ? ((object) contract.AccountStatus) : ((object) DBNull.Value);
                    row["AuditingStatus"] = contract.AuditingStatus.HasValue ? ((object) contract.AuditingStatus) : ((object) DBNull.Value);
                    row["ChangeStatus"] = contract.ChangeStatus.HasValue ? ((object) contract.ChangeStatus) : ((object) DBNull.Value);
                    row["ChangeCount"] = contract.ChangeCount.HasValue ? ((object) contract.ChangeCount) : ((object) DBNull.Value);
                    row["WorkStartDate"] = contract.WorkStartDate.HasValue ? ((object) contract.WorkStartDate) : ((object) DBNull.Value);
                    row["WorkEndDate"] = contract.WorkEndDate.HasValue ? ((object) contract.WorkEndDate) : ((object) DBNull.Value);
                    row["PerCash0"] = contract.PerCash0.HasValue ? ((object) contract.PerCash0) : ((object) DBNull.Value);
                    row["PerCash1"] = contract.PerCash1.HasValue ? ((object) contract.PerCash1) : ((object) DBNull.Value);
                    row["PerCash2"] = contract.PerCash2.HasValue ? ((object) contract.PerCash2) : ((object) DBNull.Value);
                    row["PerCash3"] = contract.PerCash3.HasValue ? ((object) contract.PerCash3) : ((object) DBNull.Value);
                    row["PerCash4"] = contract.PerCash4.HasValue ? ((object) contract.PerCash4) : ((object) DBNull.Value);
                    row["PerCash5"] = contract.PerCash5.HasValue ? ((object) contract.PerCash5) : ((object) DBNull.Value);
                    row["PerCash6"] = contract.PerCash6.HasValue ? ((object) contract.PerCash6) : ((object) DBNull.Value);
                    row["PerCash7"] = contract.PerCash7.HasValue ? ((object) contract.PerCash7) : ((object) DBNull.Value);
                    row["PerCash8"] = contract.PerCash8.HasValue ? ((object) contract.PerCash8) : ((object) DBNull.Value);
                    row["PerCash9"] = contract.PerCash9.HasValue ? ((object) contract.PerCash9) : ((object) DBNull.Value);
                    row["StampDutyID"] = contract.StampDutyID.HasValue ? ((object) contract.StampDutyID) : ((object) DBNull.Value);
                    row["StampDuty"] = contract.StampDuty.HasValue ? ((object) contract.StampDuty) : ((object) DBNull.Value);
                    row["AdIssueDate"] = contract.AdIssueDate.HasValue ? ((object) contract.AdIssueDate) : ((object) DBNull.Value);
                    row["MoneyType"] = contract.MoneyType;
                    row["ExchangeRate"] = contract.ExchangeRate.HasValue ? ((object) contract.ExchangeRate) : ((object) DBNull.Value);
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (Contract contract in entities)
            {
                if (contract.EntityState == EntityState.Added)
                {
                    contract.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string contractCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, contractCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(Contract), new object[] { contractCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<Contract> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<Contract>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Type", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SupplierCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Supplier2Code", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@TotalMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@CreateDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@CreatePerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@LastModifyPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@LastModifyDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@ContractObject", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ThirdParty", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BeforeAccountTotalMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@OldSumMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Mostly", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@BiddingCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BudgetMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AdjustMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@DevelopUnit", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CreateMode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@WorkTime", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@MarkSegment", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@GroupName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Building", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayMode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@QualityRequire", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractArea", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ContractDefaultValueCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BaoHan", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerformingCircs", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@AccountStatus", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@AuditingStatus", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@ChangeStatus", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@ChangeCount", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@WorkStartDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@WorkEndDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@PerCash0", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash1", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash2", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash3", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash4", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash5", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash6", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash7", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash8", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PerCash9", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@StampDutyID", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@StampDuty", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AdIssueDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractid ") || text.Trim().StartsWith("contractid="))
                {
                    database.SetParameterValue(command, "@ContractID", text.Replace("contractid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractname ") || text.Trim().StartsWith("contractname="))
                {
                    database.SetParameterValue(command, "@ContractName", text.Replace("contractname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("type ") || text.Trim().StartsWith("type="))
                {
                    database.SetParameterValue(command, "@Type", text.Replace("type", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("suppliercode ") || text.Trim().StartsWith("suppliercode="))
                {
                    database.SetParameterValue(command, "@SupplierCode", text.Replace("suppliercode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("supplier2code ") || text.Trim().StartsWith("supplier2code="))
                {
                    database.SetParameterValue(command, "@Supplier2Code", text.Replace("supplier2code", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractperson ") || text.Trim().StartsWith("contractperson="))
                {
                    database.SetParameterValue(command, "@ContractPerson", text.Replace("contractperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractdate ") || text.Trim().StartsWith("contractdate="))
                {
                    database.SetParameterValue(command, "@ContractDate", text.Replace("contractdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("totalmoney ") || text.Trim().StartsWith("totalmoney="))
                {
                    database.SetParameterValue(command, "@TotalMoney", text.Replace("totalmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("createdate ") || text.Trim().StartsWith("createdate="))
                {
                    database.SetParameterValue(command, "@CreateDate", text.Replace("createdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("createperson ") || text.Trim().StartsWith("createperson="))
                {
                    database.SetParameterValue(command, "@CreatePerson", text.Replace("createperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("lastmodifyperson ") || text.Trim().StartsWith("lastmodifyperson="))
                {
                    database.SetParameterValue(command, "@LastModifyPerson", text.Replace("lastmodifyperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("lastmodifydate ") || text.Trim().StartsWith("lastmodifydate="))
                {
                    database.SetParameterValue(command, "@LastModifyDate", text.Replace("lastmodifydate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("remark ") || text.Trim().StartsWith("remark="))
                {
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("status ") || text.Trim().StartsWith("status="))
                {
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkperson ") || text.Trim().StartsWith("checkperson="))
                {
                    database.SetParameterValue(command, "@CheckPerson", text.Replace("checkperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkopinion ") || text.Trim().StartsWith("checkopinion="))
                {
                    database.SetParameterValue(command, "@CheckOpinion", text.Replace("checkopinion", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkdate ") || text.Trim().StartsWith("checkdate="))
                {
                    database.SetParameterValue(command, "@CheckDate", text.Replace("checkdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractobject ") || text.Trim().StartsWith("contractobject="))
                {
                    database.SetParameterValue(command, "@ContractObject", text.Replace("contractobject", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("unitcode ") || text.Trim().StartsWith("unitcode="))
                {
                    database.SetParameterValue(command, "@UnitCode", text.Replace("unitcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("thirdparty ") || text.Trim().StartsWith("thirdparty="))
                {
                    database.SetParameterValue(command, "@ThirdParty", text.Replace("thirdparty", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("beforeaccounttotalmoney ") || text.Trim().StartsWith("beforeaccounttotalmoney="))
                {
                    database.SetParameterValue(command, "@BeforeAccountTotalMoney", text.Replace("beforeaccounttotalmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("oldsummoney ") || text.Trim().StartsWith("oldsummoney="))
                {
                    database.SetParameterValue(command, "@oldSumMoney", text.Replace("oldsummoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("originalmoney ") || text.Trim().StartsWith("originalmoney="))
                {
                    database.SetParameterValue(command, "@OriginalMoney", text.Replace("originalmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("mostly ") || text.Trim().StartsWith("mostly="))
                {
                    database.SetParameterValue(command, "@Mostly", text.Replace("mostly", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("biddingcode ") || text.Trim().StartsWith("biddingcode="))
                {
                    database.SetParameterValue(command, "@BiddingCode", text.Replace("biddingcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("budgetmoney ") || text.Trim().StartsWith("budgetmoney="))
                {
                    database.SetParameterValue(command, "@BudgetMoney", text.Replace("budgetmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("adjustmoney ") || text.Trim().StartsWith("adjustmoney="))
                {
                    database.SetParameterValue(command, "@AdjustMoney", text.Replace("adjustmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("developunit ") || text.Trim().StartsWith("developunit="))
                {
                    database.SetParameterValue(command, "@DevelopUnit", text.Replace("developunit", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("createmode ") || text.Trim().StartsWith("createmode="))
                {
                    database.SetParameterValue(command, "@CreateMode", text.Replace("createmode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("worktime ") || text.Trim().StartsWith("worktime="))
                {
                    database.SetParameterValue(command, "@WorkTime", text.Replace("worktime", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("marksegment ") || text.Trim().StartsWith("marksegment="))
                {
                    database.SetParameterValue(command, "@MarkSegment", text.Replace("marksegment", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("groupname ") || text.Trim().StartsWith("groupname="))
                {
                    database.SetParameterValue(command, "@GroupName", text.Replace("groupname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("building ") || text.Trim().StartsWith("building="))
                {
                    database.SetParameterValue(command, "@Building", text.Replace("building", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymode ") || text.Trim().StartsWith("paymode="))
                {
                    database.SetParameterValue(command, "@PayMode", text.Replace("paymode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("qualityrequire ") || text.Trim().StartsWith("qualityrequire="))
                {
                    database.SetParameterValue(command, "@QualityRequire", text.Replace("qualityrequire", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractarea ") || text.Trim().StartsWith("contractarea="))
                {
                    database.SetParameterValue(command, "@ContractArea", text.Replace("contractarea", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractdefaultvaluecode ") || text.Trim().StartsWith("contractdefaultvaluecode="))
                {
                    database.SetParameterValue(command, "@ContractDefaultValueCode", text.Replace("contractdefaultvaluecode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("baohan ") || text.Trim().StartsWith("baohan="))
                {
                    database.SetParameterValue(command, "@BaoHan", text.Replace("baohan", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("performingcircs ") || text.Trim().StartsWith("performingcircs="))
                {
                    database.SetParameterValue(command, "@PerformingCircs", text.Replace("performingcircs", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("accountstatus ") || text.Trim().StartsWith("accountstatus="))
                {
                    database.SetParameterValue(command, "@AccountStatus", text.Replace("accountstatus", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("auditingstatus ") || text.Trim().StartsWith("auditingstatus="))
                {
                    database.SetParameterValue(command, "@AuditingStatus", text.Replace("auditingstatus", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changestatus ") || text.Trim().StartsWith("changestatus="))
                {
                    database.SetParameterValue(command, "@ChangeStatus", text.Replace("changestatus", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("changecount ") || text.Trim().StartsWith("changecount="))
                {
                    database.SetParameterValue(command, "@ChangeCount", text.Replace("changecount", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("workstartdate ") || text.Trim().StartsWith("workstartdate="))
                {
                    database.SetParameterValue(command, "@WorkStartDate", text.Replace("workstartdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("workenddate ") || text.Trim().StartsWith("workenddate="))
                {
                    database.SetParameterValue(command, "@WorkEndDate", text.Replace("workenddate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash0 ") || text.Trim().StartsWith("percash0="))
                {
                    database.SetParameterValue(command, "@PerCash0", text.Replace("percash0", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash1 ") || text.Trim().StartsWith("percash1="))
                {
                    database.SetParameterValue(command, "@PerCash1", text.Replace("percash1", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash2 ") || text.Trim().StartsWith("percash2="))
                {
                    database.SetParameterValue(command, "@PerCash2", text.Replace("percash2", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash3 ") || text.Trim().StartsWith("percash3="))
                {
                    database.SetParameterValue(command, "@PerCash3", text.Replace("percash3", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash4 ") || text.Trim().StartsWith("percash4="))
                {
                    database.SetParameterValue(command, "@PerCash4", text.Replace("percash4", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash5 ") || text.Trim().StartsWith("percash5="))
                {
                    database.SetParameterValue(command, "@PerCash5", text.Replace("percash5", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash6 ") || text.Trim().StartsWith("percash6="))
                {
                    database.SetParameterValue(command, "@PerCash6", text.Replace("percash6", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash7 ") || text.Trim().StartsWith("percash7="))
                {
                    database.SetParameterValue(command, "@PerCash7", text.Replace("percash7", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash8 ") || text.Trim().StartsWith("percash8="))
                {
                    database.SetParameterValue(command, "@PerCash8", text.Replace("percash8", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("percash9 ") || text.Trim().StartsWith("percash9="))
                {
                    database.SetParameterValue(command, "@PerCash9", text.Replace("percash9", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("stampdutyid ") || text.Trim().StartsWith("stampdutyid="))
                {
                    database.SetParameterValue(command, "@StampDutyID", text.Replace("stampdutyid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("stampduty ") || text.Trim().StartsWith("stampduty="))
                {
                    database.SetParameterValue(command, "@StampDuty", text.Replace("stampduty", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("adissuedate ") || text.Trim().StartsWith("adissuedate="))
                {
                    database.SetParameterValue(command, "@AdIssueDate", text.Replace("adissuedate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("moneytype ") || text.Trim().StartsWith("moneytype="))
                {
                    database.SetParameterValue(command, "@MoneyType", text.Replace("moneytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("exchangerate ") && !text.Trim().StartsWith("exchangerate="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@ExchangeRate", text.Replace("exchangerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<Contract> rows = new TList<Contract>();
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
                ContractProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Contract> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_Find_Dynamic", typeof(ContractColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<Contract> rows = new TList<Contract>();
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
                ContractProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<Contract> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<Contract> rows = new TList<Contract>();
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
                ContractProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override Contract GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_GetByContractCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, contractCode);
            IDataReader reader = null;
            TList<Contract> rows = new TList<Contract>();
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
                ContractProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Contract> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_GetByProjectCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, projectCode);
            IDataReader reader = null;
            TList<Contract> rows = new TList<Contract>();
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
                ContractProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Contract> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<Contract> rows = new TList<Contract>();
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
                    ContractProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, Contract entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@ContractID", DbType.AnsiString, entity.ContractID);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@ContractName", DbType.AnsiString, entity.ContractName);
            database.AddInParameter(command, "@Type", DbType.AnsiString, entity.Type);
            database.AddInParameter(command, "@SupplierCode", DbType.AnsiString, entity.SupplierCode);
            database.AddInParameter(command, "@Supplier2Code", DbType.AnsiString, entity.Supplier2Code);
            database.AddInParameter(command, "@ContractPerson", DbType.AnsiString, entity.ContractPerson);
            database.AddInParameter(command, "@ContractDate", DbType.DateTime, entity.ContractDate.HasValue ? ((object) entity.ContractDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalMoney", DbType.Decimal, entity.TotalMoney.HasValue ? ((object) entity.TotalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreateDate", DbType.DateTime, entity.CreateDate.HasValue ? ((object) entity.CreateDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreatePerson", DbType.AnsiString, entity.CreatePerson);
            database.AddInParameter(command, "@LastModifyPerson", DbType.AnsiString, entity.LastModifyPerson);
            database.AddInParameter(command, "@LastModifyDate", DbType.DateTime, entity.LastModifyDate.HasValue ? ((object) entity.LastModifyDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, entity.CheckOpinion);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ContractObject", DbType.AnsiString, entity.ContractObject);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, entity.UnitCode);
            database.AddInParameter(command, "@ThirdParty", DbType.AnsiString, entity.ThirdParty);
            database.AddInParameter(command, "@BeforeAccountTotalMoney", DbType.Decimal, entity.BeforeAccountTotalMoney.HasValue ? ((object) entity.BeforeAccountTotalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OldSumMoney", DbType.Decimal, entity.OldSumMoney.HasValue ? ((object) entity.OldSumMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Mostly", DbType.Int32, entity.Mostly.HasValue ? ((object) entity.Mostly) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BiddingCode", DbType.AnsiString, entity.BiddingCode);
            database.AddInParameter(command, "@BudgetMoney", DbType.Decimal, entity.BudgetMoney.HasValue ? ((object) entity.BudgetMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AdjustMoney", DbType.Decimal, entity.AdjustMoney.HasValue ? ((object) entity.AdjustMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@DevelopUnit", DbType.AnsiString, entity.DevelopUnit);
            database.AddInParameter(command, "@CreateMode", DbType.AnsiString, entity.CreateMode);
            database.AddInParameter(command, "@WorkTime", DbType.AnsiString, entity.WorkTime);
            database.AddInParameter(command, "@MarkSegment", DbType.AnsiString, entity.MarkSegment);
            database.AddInParameter(command, "@GroupName", DbType.AnsiString, entity.GroupName);
            database.AddInParameter(command, "@Building", DbType.AnsiString, entity.Building);
            database.AddInParameter(command, "@PayMode", DbType.AnsiString, entity.PayMode);
            database.AddInParameter(command, "@QualityRequire", DbType.AnsiString, entity.QualityRequire);
            database.AddInParameter(command, "@ContractArea", DbType.AnsiString, entity.ContractArea);
            database.AddInParameter(command, "@ContractDefaultValueCode", DbType.AnsiString, entity.ContractDefaultValueCode);
            database.AddInParameter(command, "@BaoHan", DbType.Decimal, entity.BaoHan.HasValue ? ((object) entity.BaoHan) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerformingCircs", DbType.AnsiString, entity.PerformingCircs);
            database.AddInParameter(command, "@AccountStatus", DbType.Int32, entity.AccountStatus.HasValue ? ((object) entity.AccountStatus) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AuditingStatus", DbType.Int32, entity.AuditingStatus.HasValue ? ((object) entity.AuditingStatus) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeStatus", DbType.Int32, entity.ChangeStatus.HasValue ? ((object) entity.ChangeStatus) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeCount", DbType.Int32, entity.ChangeCount.HasValue ? ((object) entity.ChangeCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@WorkStartDate", DbType.DateTime, entity.WorkStartDate.HasValue ? ((object) entity.WorkStartDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@WorkEndDate", DbType.DateTime, entity.WorkEndDate.HasValue ? ((object) entity.WorkEndDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash0", DbType.Decimal, entity.PerCash0.HasValue ? ((object) entity.PerCash0) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash1", DbType.Decimal, entity.PerCash1.HasValue ? ((object) entity.PerCash1) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash2", DbType.Decimal, entity.PerCash2.HasValue ? ((object) entity.PerCash2) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash3", DbType.Decimal, entity.PerCash3.HasValue ? ((object) entity.PerCash3) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash4", DbType.Decimal, entity.PerCash4.HasValue ? ((object) entity.PerCash4) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash5", DbType.Decimal, entity.PerCash5.HasValue ? ((object) entity.PerCash5) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash6", DbType.Decimal, entity.PerCash6.HasValue ? ((object) entity.PerCash6) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash7", DbType.Decimal, entity.PerCash7.HasValue ? ((object) entity.PerCash7) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash8", DbType.Decimal, entity.PerCash8.HasValue ? ((object) entity.PerCash8) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash9", DbType.Decimal, entity.PerCash9.HasValue ? ((object) entity.PerCash9) : ((object) DBNull.Value));
            database.AddInParameter(command, "@StampDutyID", DbType.Int32, entity.StampDutyID.HasValue ? ((object) entity.StampDutyID) : ((object) DBNull.Value));
            database.AddInParameter(command, "@StampDuty", DbType.Decimal, entity.StampDuty.HasValue ? ((object) entity.StampDuty) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AdIssueDate", DbType.DateTime, entity.AdIssueDate.HasValue ? ((object) entity.AdIssueDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalContractCode = entity.ContractCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, Contract entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Contract_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@OriginalContractCode", DbType.AnsiString, entity.OriginalContractCode);
            database.AddInParameter(command, "@ContractID", DbType.AnsiString, entity.ContractID);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@ContractName", DbType.AnsiString, entity.ContractName);
            database.AddInParameter(command, "@Type", DbType.AnsiString, entity.Type);
            database.AddInParameter(command, "@SupplierCode", DbType.AnsiString, entity.SupplierCode);
            database.AddInParameter(command, "@Supplier2Code", DbType.AnsiString, entity.Supplier2Code);
            database.AddInParameter(command, "@ContractPerson", DbType.AnsiString, entity.ContractPerson);
            database.AddInParameter(command, "@ContractDate", DbType.DateTime, entity.ContractDate.HasValue ? ((object) entity.ContractDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalMoney", DbType.Decimal, entity.TotalMoney.HasValue ? ((object) entity.TotalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreateDate", DbType.DateTime, entity.CreateDate.HasValue ? ((object) entity.CreateDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CreatePerson", DbType.AnsiString, entity.CreatePerson);
            database.AddInParameter(command, "@LastModifyPerson", DbType.AnsiString, entity.LastModifyPerson);
            database.AddInParameter(command, "@LastModifyDate", DbType.DateTime, entity.LastModifyDate.HasValue ? ((object) entity.LastModifyDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, entity.CheckOpinion);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ContractObject", DbType.AnsiString, entity.ContractObject);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, entity.UnitCode);
            database.AddInParameter(command, "@ThirdParty", DbType.AnsiString, entity.ThirdParty);
            database.AddInParameter(command, "@BeforeAccountTotalMoney", DbType.Decimal, entity.BeforeAccountTotalMoney.HasValue ? ((object) entity.BeforeAccountTotalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OldSumMoney", DbType.Decimal, entity.OldSumMoney.HasValue ? ((object) entity.OldSumMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@OriginalMoney", DbType.Decimal, entity.OriginalMoney.HasValue ? ((object) entity.OriginalMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Mostly", DbType.Int32, entity.Mostly.HasValue ? ((object) entity.Mostly) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BiddingCode", DbType.AnsiString, entity.BiddingCode);
            database.AddInParameter(command, "@BudgetMoney", DbType.Decimal, entity.BudgetMoney.HasValue ? ((object) entity.BudgetMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AdjustMoney", DbType.Decimal, entity.AdjustMoney.HasValue ? ((object) entity.AdjustMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@DevelopUnit", DbType.AnsiString, entity.DevelopUnit);
            database.AddInParameter(command, "@CreateMode", DbType.AnsiString, entity.CreateMode);
            database.AddInParameter(command, "@WorkTime", DbType.AnsiString, entity.WorkTime);
            database.AddInParameter(command, "@MarkSegment", DbType.AnsiString, entity.MarkSegment);
            database.AddInParameter(command, "@GroupName", DbType.AnsiString, entity.GroupName);
            database.AddInParameter(command, "@Building", DbType.AnsiString, entity.Building);
            database.AddInParameter(command, "@PayMode", DbType.AnsiString, entity.PayMode);
            database.AddInParameter(command, "@QualityRequire", DbType.AnsiString, entity.QualityRequire);
            database.AddInParameter(command, "@ContractArea", DbType.AnsiString, entity.ContractArea);
            database.AddInParameter(command, "@ContractDefaultValueCode", DbType.AnsiString, entity.ContractDefaultValueCode);
            database.AddInParameter(command, "@BaoHan", DbType.Decimal, entity.BaoHan.HasValue ? ((object) entity.BaoHan) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerformingCircs", DbType.AnsiString, entity.PerformingCircs);
            database.AddInParameter(command, "@AccountStatus", DbType.Int32, entity.AccountStatus.HasValue ? ((object) entity.AccountStatus) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AuditingStatus", DbType.Int32, entity.AuditingStatus.HasValue ? ((object) entity.AuditingStatus) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeStatus", DbType.Int32, entity.ChangeStatus.HasValue ? ((object) entity.ChangeStatus) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ChangeCount", DbType.Int32, entity.ChangeCount.HasValue ? ((object) entity.ChangeCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@WorkStartDate", DbType.DateTime, entity.WorkStartDate.HasValue ? ((object) entity.WorkStartDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@WorkEndDate", DbType.DateTime, entity.WorkEndDate.HasValue ? ((object) entity.WorkEndDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash0", DbType.Decimal, entity.PerCash0.HasValue ? ((object) entity.PerCash0) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash1", DbType.Decimal, entity.PerCash1.HasValue ? ((object) entity.PerCash1) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash2", DbType.Decimal, entity.PerCash2.HasValue ? ((object) entity.PerCash2) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash3", DbType.Decimal, entity.PerCash3.HasValue ? ((object) entity.PerCash3) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash4", DbType.Decimal, entity.PerCash4.HasValue ? ((object) entity.PerCash4) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash5", DbType.Decimal, entity.PerCash5.HasValue ? ((object) entity.PerCash5) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash6", DbType.Decimal, entity.PerCash6.HasValue ? ((object) entity.PerCash6) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash7", DbType.Decimal, entity.PerCash7.HasValue ? ((object) entity.PerCash7) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash8", DbType.Decimal, entity.PerCash8.HasValue ? ((object) entity.PerCash8) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PerCash9", DbType.Decimal, entity.PerCash9.HasValue ? ((object) entity.PerCash9) : ((object) DBNull.Value));
            database.AddInParameter(command, "@StampDutyID", DbType.Int32, entity.StampDutyID.HasValue ? ((object) entity.StampDutyID) : ((object) DBNull.Value));
            database.AddInParameter(command, "@StampDuty", DbType.Decimal, entity.StampDuty.HasValue ? ((object) entity.StampDuty) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AdIssueDate", DbType.DateTime, entity.AdIssueDate.HasValue ? ((object) entity.AdIssueDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MoneyType", DbType.AnsiString, entity.MoneyType);
            database.AddInParameter(command, "@ExchangeRate", DbType.Decimal, entity.ExchangeRate.HasValue ? ((object) entity.ExchangeRate) : ((object) DBNull.Value));
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
            entity.OriginalContractCode = entity.ContractCode;
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

