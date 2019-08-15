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

    public class SqlPaymentProviderBase : PaymentProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlPaymentProviderBase()
        {
        }

        public SqlPaymentProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<Payment> entities)
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
            copy.DestinationTableName = "Payment";
            DataTable table = new DataTable();
            table.Columns.Add("PaymentCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("PaymentTitle", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PaymentID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("VoucherID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("RecieptCount", typeof(int)).AllowDBNull = true;
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ApplyPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ApplyDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("Accountant", typeof(string)).AllowDBNull = true;
            table.Columns.Add("AccountDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("Payer", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PayDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("Purpose", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Money", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("CheckPerson", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CheckDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("CheckOpinion", typeof(string)).AllowDBNull = true;
            table.Columns.Add("IsContract", typeof(int)).AllowDBNull = true;
            table.Columns.Add("ContractCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            table.Columns.Add("WBSCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("IsApportion", typeof(int)).AllowDBNull = true;
            table.Columns.Add("SupplyCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("UnitCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SupplyName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            table.Columns.Add("OldMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("GroupCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BankName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BankAccount", typeof(string)).AllowDBNull = true;
            table.Columns.Add("OtherAttachMent", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PayType", typeof(int)).AllowDBNull = true;
            table.Columns.Add("Issue", typeof(int)).AllowDBNull = true;
            table.Columns.Add("IssueMode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("FactPayDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("TotalPayMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("APMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("UPMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("SupplierApplyMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("MaxPayMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PlanPayMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ContractMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AdjustedContractMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PayoutMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("APCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("UPCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PayoutCash", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash0", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash1", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash2", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash3", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash4", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash5", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash6", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash7", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash8", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AHCash9", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PaymentName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("TotalViseChangeMoney", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("SumCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PaymentCodition", typeof(string)).AllowDBNull = true;
            table.Columns.Add("CheckRemark", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("PaymentCode", "PaymentCode");
            copy.ColumnMappings.Add("PaymentTitle", "PaymentTitle");
            copy.ColumnMappings.Add("PaymentID", "PaymentID");
            copy.ColumnMappings.Add("VoucherID", "VoucherID");
            copy.ColumnMappings.Add("RecieptCount", "RecieptCount");
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("ApplyPerson", "ApplyPerson");
            copy.ColumnMappings.Add("ApplyDate", "ApplyDate");
            copy.ColumnMappings.Add("Accountant", "Accountant");
            copy.ColumnMappings.Add("AccountDate", "AccountDate");
            copy.ColumnMappings.Add("Payer", "Payer");
            copy.ColumnMappings.Add("PayDate", "PayDate");
            copy.ColumnMappings.Add("Purpose", "Purpose");
            copy.ColumnMappings.Add("Money", "Money");
            copy.ColumnMappings.Add("CheckPerson", "CheckPerson");
            copy.ColumnMappings.Add("CheckDate", "CheckDate");
            copy.ColumnMappings.Add("CheckOpinion", "CheckOpinion");
            copy.ColumnMappings.Add("IsContract", "IsContract");
            copy.ColumnMappings.Add("ContractCode", "ContractCode");
            copy.ColumnMappings.Add("Status", "Status");
            copy.ColumnMappings.Add("WBSCode", "WBSCode");
            copy.ColumnMappings.Add("IsApportion", "IsApportion");
            copy.ColumnMappings.Add("SupplyCode", "SupplyCode");
            copy.ColumnMappings.Add("UnitCode", "UnitCode");
            copy.ColumnMappings.Add("SupplyName", "SupplyName");
            copy.ColumnMappings.Add("Remark", "Remark");
            copy.ColumnMappings.Add("OldMoney", "OldMoney");
            copy.ColumnMappings.Add("GroupCode", "GroupCode");
            copy.ColumnMappings.Add("BankName", "BankName");
            copy.ColumnMappings.Add("BankAccount", "BankAccount");
            copy.ColumnMappings.Add("OtherAttachMent", "OtherAttachMent");
            copy.ColumnMappings.Add("PayType", "PayType");
            copy.ColumnMappings.Add("Issue", "Issue");
            copy.ColumnMappings.Add("IssueMode", "IssueMode");
            copy.ColumnMappings.Add("FactPayDate", "FactPayDate");
            copy.ColumnMappings.Add("TotalPayMoney", "TotalPayMoney");
            copy.ColumnMappings.Add("AHMoney", "AHMoney");
            copy.ColumnMappings.Add("APMoney", "APMoney");
            copy.ColumnMappings.Add("UPMoney", "UPMoney");
            copy.ColumnMappings.Add("SupplierApplyMoney", "SupplierApplyMoney");
            copy.ColumnMappings.Add("MaxPayMoney", "MaxPayMoney");
            copy.ColumnMappings.Add("PlanPayMoney", "PlanPayMoney");
            copy.ColumnMappings.Add("ContractMoney", "ContractMoney");
            copy.ColumnMappings.Add("AdjustedContractMoney", "AdjustedContractMoney");
            copy.ColumnMappings.Add("PayoutMoney", "PayoutMoney");
            copy.ColumnMappings.Add("AHCash", "AHCash");
            copy.ColumnMappings.Add("APCash", "APCash");
            copy.ColumnMappings.Add("UPCash", "UPCash");
            copy.ColumnMappings.Add("PayoutCash", "PayoutCash");
            copy.ColumnMappings.Add("AHCash0", "AHCash0");
            copy.ColumnMappings.Add("AHCash1", "AHCash1");
            copy.ColumnMappings.Add("AHCash2", "AHCash2");
            copy.ColumnMappings.Add("AHCash3", "AHCash3");
            copy.ColumnMappings.Add("AHCash4", "AHCash4");
            copy.ColumnMappings.Add("AHCash5", "AHCash5");
            copy.ColumnMappings.Add("AHCash6", "AHCash6");
            copy.ColumnMappings.Add("AHCash7", "AHCash7");
            copy.ColumnMappings.Add("AHCash8", "AHCash8");
            copy.ColumnMappings.Add("AHCash9", "AHCash9");
            copy.ColumnMappings.Add("PaymentName", "PaymentName");
            copy.ColumnMappings.Add("TotalViseChangeMoney", "TotalViseChangeMoney");
            copy.ColumnMappings.Add("SumCode", "SumCode");
            copy.ColumnMappings.Add("PaymentCodition", "PaymentCodition");
            copy.ColumnMappings.Add("CheckRemark", "CheckRemark");
            foreach (Payment payment in entities)
            {
                if (payment.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["PaymentCode"] = payment.PaymentCode;
                    row["PaymentTitle"] = payment.PaymentTitle;
                    row["PaymentID"] = payment.PaymentID;
                    row["VoucherID"] = payment.VoucherID;
                    row["RecieptCount"] = payment.RecieptCount.HasValue ? ((object) payment.RecieptCount) : ((object) DBNull.Value);
                    row["ProjectCode"] = payment.ProjectCode;
                    row["ApplyPerson"] = payment.ApplyPerson;
                    row["ApplyDate"] = payment.ApplyDate.HasValue ? ((object) payment.ApplyDate) : ((object) DBNull.Value);
                    row["Accountant"] = payment.Accountant;
                    row["AccountDate"] = payment.AccountDate.HasValue ? ((object) payment.AccountDate) : ((object) DBNull.Value);
                    row["Payer"] = payment.Payer;
                    row["PayDate"] = payment.PayDate.HasValue ? ((object) payment.PayDate) : ((object) DBNull.Value);
                    row["Purpose"] = payment.Purpose;
                    row["Money"] = payment.Money.HasValue ? ((object) payment.Money) : ((object) DBNull.Value);
                    row["CheckPerson"] = payment.CheckPerson;
                    row["CheckDate"] = payment.CheckDate.HasValue ? ((object) payment.CheckDate) : ((object) DBNull.Value);
                    row["CheckOpinion"] = payment.CheckOpinion;
                    row["IsContract"] = payment.IsContract.HasValue ? ((object) payment.IsContract) : ((object) DBNull.Value);
                    row["ContractCode"] = payment.ContractCode;
                    row["Status"] = payment.Status.HasValue ? ((object) payment.Status) : ((object) DBNull.Value);
                    row["WBSCode"] = payment.WBSCode;
                    row["IsApportion"] = payment.IsApportion.HasValue ? ((object) payment.IsApportion) : ((object) DBNull.Value);
                    row["SupplyCode"] = payment.SupplyCode;
                    row["UnitCode"] = payment.UnitCode;
                    row["SupplyName"] = payment.SupplyName;
                    row["Remark"] = payment.Remark;
                    row["OldMoney"] = payment.OldMoney.HasValue ? ((object) payment.OldMoney) : ((object) DBNull.Value);
                    row["GroupCode"] = payment.GroupCode;
                    row["BankName"] = payment.BankName;
                    row["BankAccount"] = payment.BankAccount;
                    row["OtherAttachMent"] = payment.OtherAttachMent;
                    row["PayType"] = payment.PayType.HasValue ? ((object) payment.PayType) : ((object) DBNull.Value);
                    row["Issue"] = payment.Issue.HasValue ? ((object) payment.Issue) : ((object) DBNull.Value);
                    row["IssueMode"] = payment.IssueMode;
                    row["FactPayDate"] = payment.FactPayDate.HasValue ? ((object) payment.FactPayDate) : ((object) DBNull.Value);
                    row["TotalPayMoney"] = payment.TotalPayMoney.HasValue ? ((object) payment.TotalPayMoney) : ((object) DBNull.Value);
                    row["AHMoney"] = payment.AHMoney.HasValue ? ((object) payment.AHMoney) : ((object) DBNull.Value);
                    row["APMoney"] = payment.APMoney.HasValue ? ((object) payment.APMoney) : ((object) DBNull.Value);
                    row["UPMoney"] = payment.UPMoney.HasValue ? ((object) payment.UPMoney) : ((object) DBNull.Value);
                    row["SupplierApplyMoney"] = payment.SupplierApplyMoney.HasValue ? ((object) payment.SupplierApplyMoney) : ((object) DBNull.Value);
                    row["MaxPayMoney"] = payment.MaxPayMoney.HasValue ? ((object) payment.MaxPayMoney) : ((object) DBNull.Value);
                    row["PlanPayMoney"] = payment.PlanPayMoney.HasValue ? ((object) payment.PlanPayMoney) : ((object) DBNull.Value);
                    row["ContractMoney"] = payment.ContractMoney.HasValue ? ((object) payment.ContractMoney) : ((object) DBNull.Value);
                    row["AdjustedContractMoney"] = payment.AdjustedContractMoney.HasValue ? ((object) payment.AdjustedContractMoney) : ((object) DBNull.Value);
                    row["PayoutMoney"] = payment.PayoutMoney.HasValue ? ((object) payment.PayoutMoney) : ((object) DBNull.Value);
                    row["AHCash"] = payment.AHCash.HasValue ? ((object) payment.AHCash) : ((object) DBNull.Value);
                    row["APCash"] = payment.APCash.HasValue ? ((object) payment.APCash) : ((object) DBNull.Value);
                    row["UPCash"] = payment.UPCash.HasValue ? ((object) payment.UPCash) : ((object) DBNull.Value);
                    row["PayoutCash"] = payment.PayoutCash.HasValue ? ((object) payment.PayoutCash) : ((object) DBNull.Value);
                    row["AHCash0"] = payment.AHCash0.HasValue ? ((object) payment.AHCash0) : ((object) DBNull.Value);
                    row["AHCash1"] = payment.AHCash1.HasValue ? ((object) payment.AHCash1) : ((object) DBNull.Value);
                    row["AHCash2"] = payment.AHCash2.HasValue ? ((object) payment.AHCash2) : ((object) DBNull.Value);
                    row["AHCash3"] = payment.AHCash3.HasValue ? ((object) payment.AHCash3) : ((object) DBNull.Value);
                    row["AHCash4"] = payment.AHCash4.HasValue ? ((object) payment.AHCash4) : ((object) DBNull.Value);
                    row["AHCash5"] = payment.AHCash5.HasValue ? ((object) payment.AHCash5) : ((object) DBNull.Value);
                    row["AHCash6"] = payment.AHCash6.HasValue ? ((object) payment.AHCash6) : ((object) DBNull.Value);
                    row["AHCash7"] = payment.AHCash7.HasValue ? ((object) payment.AHCash7) : ((object) DBNull.Value);
                    row["AHCash8"] = payment.AHCash8.HasValue ? ((object) payment.AHCash8) : ((object) DBNull.Value);
                    row["AHCash9"] = payment.AHCash9.HasValue ? ((object) payment.AHCash9) : ((object) DBNull.Value);
                    row["PaymentName"] = payment.PaymentName;
                    row["TotalViseChangeMoney"] = payment.TotalViseChangeMoney.HasValue ? ((object) payment.TotalViseChangeMoney) : ((object) DBNull.Value);
                    row["SumCode"] = payment.SumCode;
                    row["PaymentCodition"] = payment.PaymentCodition;
                    row["CheckRemark"] = payment.CheckRemark;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (Payment payment in entities)
            {
                if (payment.EntityState == EntityState.Added)
                {
                    payment.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string paymentCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, paymentCode);
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
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(Payment), new object[] { paymentCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<Payment> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<Payment>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PaymentTitle", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PaymentID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@VoucherID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@RecieptCount", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ApplyPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ApplyDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Accountant", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@AccountDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Payer", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@Purpose", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Money", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@IsContract", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@WBSCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@IsApportion", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@SupplyCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SupplyName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@OldMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BankName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BankAccount", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@OtherAttachMent", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PayType", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@Issue", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@IssueMode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@FactPayDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@TotalPayMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@APMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@UPMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@SupplierApplyMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@MaxPayMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PlanPayMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ContractMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AdjustedContractMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PayoutMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@APCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@UPCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PayoutCash", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash0", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash1", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash2", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash3", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash4", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash5", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash6", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash7", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash8", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AHCash9", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PaymentName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@TotalViseChangeMoney", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@SumCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PaymentCodition", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@CheckRemark", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("paymentcode ") || text.Trim().StartsWith("paymentcode="))
                {
                    database.SetParameterValue(command, "@PaymentCode", text.Replace("paymentcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymenttitle ") || text.Trim().StartsWith("paymenttitle="))
                {
                    database.SetParameterValue(command, "@PaymentTitle", text.Replace("paymenttitle", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymentid ") || text.Trim().StartsWith("paymentid="))
                {
                    database.SetParameterValue(command, "@PaymentID", text.Replace("paymentid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("voucherid ") || text.Trim().StartsWith("voucherid="))
                {
                    database.SetParameterValue(command, "@VoucherID", text.Replace("voucherid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("recieptcount ") || text.Trim().StartsWith("recieptcount="))
                {
                    database.SetParameterValue(command, "@RecieptCount", text.Replace("recieptcount", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("applyperson ") || text.Trim().StartsWith("applyperson="))
                {
                    database.SetParameterValue(command, "@ApplyPerson", text.Replace("applyperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("applydate ") || text.Trim().StartsWith("applydate="))
                {
                    database.SetParameterValue(command, "@ApplyDate", text.Replace("applydate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("accountant ") || text.Trim().StartsWith("accountant="))
                {
                    database.SetParameterValue(command, "@Accountant", text.Replace("accountant", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("accountdate ") || text.Trim().StartsWith("accountdate="))
                {
                    database.SetParameterValue(command, "@AccountDate", text.Replace("accountdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payer ") || text.Trim().StartsWith("payer="))
                {
                    database.SetParameterValue(command, "@Payer", text.Replace("payer", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paydate ") || text.Trim().StartsWith("paydate="))
                {
                    database.SetParameterValue(command, "@PayDate", text.Replace("paydate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("purpose ") || text.Trim().StartsWith("purpose="))
                {
                    database.SetParameterValue(command, "@Purpose", text.Replace("purpose", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("money ") || text.Trim().StartsWith("money="))
                {
                    database.SetParameterValue(command, "@Money", text.Replace("money", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkperson ") || text.Trim().StartsWith("checkperson="))
                {
                    database.SetParameterValue(command, "@CheckPerson", text.Replace("checkperson", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkdate ") || text.Trim().StartsWith("checkdate="))
                {
                    database.SetParameterValue(command, "@CheckDate", text.Replace("checkdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("checkopinion ") || text.Trim().StartsWith("checkopinion="))
                {
                    database.SetParameterValue(command, "@CheckOpinion", text.Replace("checkopinion", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("iscontract ") || text.Trim().StartsWith("iscontract="))
                {
                    database.SetParameterValue(command, "@IsContract", text.Replace("iscontract", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractcode ") || text.Trim().StartsWith("contractcode="))
                {
                    database.SetParameterValue(command, "@ContractCode", text.Replace("contractcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("status ") || text.Trim().StartsWith("status="))
                {
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("wbscode ") || text.Trim().StartsWith("wbscode="))
                {
                    database.SetParameterValue(command, "@WBSCode", text.Replace("wbscode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("isapportion ") || text.Trim().StartsWith("isapportion="))
                {
                    database.SetParameterValue(command, "@IsApportion", text.Replace("isapportion", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("supplycode ") || text.Trim().StartsWith("supplycode="))
                {
                    database.SetParameterValue(command, "@SupplyCode", text.Replace("supplycode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("unitcode ") || text.Trim().StartsWith("unitcode="))
                {
                    database.SetParameterValue(command, "@UnitCode", text.Replace("unitcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("supplyname ") || text.Trim().StartsWith("supplyname="))
                {
                    database.SetParameterValue(command, "@SupplyName", text.Replace("supplyname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("remark ") || text.Trim().StartsWith("remark="))
                {
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("oldmoney ") || text.Trim().StartsWith("oldmoney="))
                {
                    database.SetParameterValue(command, "@OldMoney", text.Replace("oldmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("groupcode ") || text.Trim().StartsWith("groupcode="))
                {
                    database.SetParameterValue(command, "@GroupCode", text.Replace("groupcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("bankname ") || text.Trim().StartsWith("bankname="))
                {
                    database.SetParameterValue(command, "@BankName", text.Replace("bankname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("bankaccount ") || text.Trim().StartsWith("bankaccount="))
                {
                    database.SetParameterValue(command, "@BankAccount", text.Replace("bankaccount", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("otherattachment ") || text.Trim().StartsWith("otherattachment="))
                {
                    database.SetParameterValue(command, "@OtherAttachMent", text.Replace("otherattachment", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paytype ") || text.Trim().StartsWith("paytype="))
                {
                    database.SetParameterValue(command, "@PayType", text.Replace("paytype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("issue ") || text.Trim().StartsWith("issue="))
                {
                    database.SetParameterValue(command, "@Issue", text.Replace("issue", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("issuemode ") || text.Trim().StartsWith("issuemode="))
                {
                    database.SetParameterValue(command, "@IssueMode", text.Replace("issuemode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("factpaydate ") || text.Trim().StartsWith("factpaydate="))
                {
                    database.SetParameterValue(command, "@FactPayDate", text.Replace("factpaydate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("totalpaymoney ") || text.Trim().StartsWith("totalpaymoney="))
                {
                    database.SetParameterValue(command, "@TotalPayMoney", text.Replace("totalpaymoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahmoney ") || text.Trim().StartsWith("ahmoney="))
                {
                    database.SetParameterValue(command, "@AHMoney", text.Replace("ahmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("apmoney ") || text.Trim().StartsWith("apmoney="))
                {
                    database.SetParameterValue(command, "@APMoney", text.Replace("apmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("upmoney ") || text.Trim().StartsWith("upmoney="))
                {
                    database.SetParameterValue(command, "@UPMoney", text.Replace("upmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("supplierapplymoney ") || text.Trim().StartsWith("supplierapplymoney="))
                {
                    database.SetParameterValue(command, "@SupplierApplyMoney", text.Replace("supplierapplymoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("maxpaymoney ") || text.Trim().StartsWith("maxpaymoney="))
                {
                    database.SetParameterValue(command, "@MaxPayMoney", text.Replace("maxpaymoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("planpaymoney ") || text.Trim().StartsWith("planpaymoney="))
                {
                    database.SetParameterValue(command, "@PlanPayMoney", text.Replace("planpaymoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("contractmoney ") || text.Trim().StartsWith("contractmoney="))
                {
                    database.SetParameterValue(command, "@ContractMoney", text.Replace("contractmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("adjustedcontractmoney ") || text.Trim().StartsWith("adjustedcontractmoney="))
                {
                    database.SetParameterValue(command, "@AdjustedContractMoney", text.Replace("adjustedcontractmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutmoney ") || text.Trim().StartsWith("payoutmoney="))
                {
                    database.SetParameterValue(command, "@PayoutMoney", text.Replace("payoutmoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash ") || text.Trim().StartsWith("ahcash="))
                {
                    database.SetParameterValue(command, "@AHCash", text.Replace("ahcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("apcash ") || text.Trim().StartsWith("apcash="))
                {
                    database.SetParameterValue(command, "@APCash", text.Replace("apcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("upcash ") || text.Trim().StartsWith("upcash="))
                {
                    database.SetParameterValue(command, "@UPCash", text.Replace("upcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("payoutcash ") || text.Trim().StartsWith("payoutcash="))
                {
                    database.SetParameterValue(command, "@PayoutCash", text.Replace("payoutcash", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash0 ") || text.Trim().StartsWith("ahcash0="))
                {
                    database.SetParameterValue(command, "@AHCash0", text.Replace("ahcash0", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash1 ") || text.Trim().StartsWith("ahcash1="))
                {
                    database.SetParameterValue(command, "@AHCash1", text.Replace("ahcash1", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash2 ") || text.Trim().StartsWith("ahcash2="))
                {
                    database.SetParameterValue(command, "@AHCash2", text.Replace("ahcash2", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash3 ") || text.Trim().StartsWith("ahcash3="))
                {
                    database.SetParameterValue(command, "@AHCash3", text.Replace("ahcash3", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash4 ") || text.Trim().StartsWith("ahcash4="))
                {
                    database.SetParameterValue(command, "@AHCash4", text.Replace("ahcash4", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash5 ") || text.Trim().StartsWith("ahcash5="))
                {
                    database.SetParameterValue(command, "@AHCash5", text.Replace("ahcash5", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash6 ") || text.Trim().StartsWith("ahcash6="))
                {
                    database.SetParameterValue(command, "@AHCash6", text.Replace("ahcash6", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash7 ") || text.Trim().StartsWith("ahcash7="))
                {
                    database.SetParameterValue(command, "@AHCash7", text.Replace("ahcash7", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash8 ") || text.Trim().StartsWith("ahcash8="))
                {
                    database.SetParameterValue(command, "@AHCash8", text.Replace("ahcash8", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ahcash9 ") || text.Trim().StartsWith("ahcash9="))
                {
                    database.SetParameterValue(command, "@AHCash9", text.Replace("ahcash9", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymentname ") || text.Trim().StartsWith("paymentname="))
                {
                    database.SetParameterValue(command, "@PaymentName", text.Replace("paymentname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("totalvisechangemoney ") || text.Trim().StartsWith("totalvisechangemoney="))
                {
                    database.SetParameterValue(command, "@TotalViseChangeMoney", text.Replace("totalvisechangemoney", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("sumcode ") || text.Trim().StartsWith("sumcode="))
                {
                    database.SetParameterValue(command, "@SumCode", text.Replace("sumcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("paymentcodition ") || text.Trim().StartsWith("paymentcodition="))
                {
                    database.SetParameterValue(command, "@PaymentCodition", text.Replace("paymentcodition", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("checkremark ") && !text.Trim().StartsWith("checkremark="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@CheckRemark", text.Replace("checkremark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<Payment> rows = new TList<Payment>();
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
                PaymentProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Payment> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_Find_Dynamic", typeof(PaymentColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<Payment> rows = new TList<Payment>();
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
                PaymentProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override TList<Payment> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<Payment> rows = new TList<Payment>();
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
                PaymentProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Payment> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_GetByContractCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, contractCode);
            IDataReader reader = null;
            TList<Payment> rows = new TList<Payment>();
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
                PaymentProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override Payment GetByPaymentCode(TransactionManager transactionManager, string paymentCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_GetByPaymentCode", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, paymentCode);
            IDataReader reader = null;
            TList<Payment> rows = new TList<Payment>();
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
                PaymentProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Payment> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_GetByProjectCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, projectCode);
            IDataReader reader = null;
            TList<Payment> rows = new TList<Payment>();
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
                PaymentProviderBaseCore.Fill(reader, rows, start, pageLength);
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

        public override TList<Payment> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<Payment> rows = new TList<Payment>();
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
                    PaymentProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
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

        public override bool Insert(TransactionManager transactionManager, Payment entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, entity.PaymentCode);
            database.AddInParameter(command, "@PaymentTitle", DbType.AnsiString, entity.PaymentTitle);
            database.AddInParameter(command, "@PaymentID", DbType.AnsiString, entity.PaymentID);
            database.AddInParameter(command, "@VoucherID", DbType.AnsiString, entity.VoucherID);
            database.AddInParameter(command, "@RecieptCount", DbType.Int32, entity.RecieptCount.HasValue ? ((object) entity.RecieptCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@ApplyPerson", DbType.AnsiString, entity.ApplyPerson);
            database.AddInParameter(command, "@ApplyDate", DbType.DateTime, entity.ApplyDate.HasValue ? ((object) entity.ApplyDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Accountant", DbType.AnsiString, entity.Accountant);
            database.AddInParameter(command, "@AccountDate", DbType.DateTime, entity.AccountDate.HasValue ? ((object) entity.AccountDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Payer", DbType.AnsiString, entity.Payer);
            database.AddInParameter(command, "@PayDate", DbType.DateTime, entity.PayDate.HasValue ? ((object) entity.PayDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Purpose", DbType.AnsiString, entity.Purpose);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, entity.CheckOpinion);
            database.AddInParameter(command, "@IsContract", DbType.Int32, entity.IsContract.HasValue ? ((object) entity.IsContract) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@WBSCode", DbType.AnsiString, entity.WBSCode);
            database.AddInParameter(command, "@IsApportion", DbType.Int32, entity.IsApportion.HasValue ? ((object) entity.IsApportion) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SupplyCode", DbType.AnsiString, entity.SupplyCode);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, entity.UnitCode);
            database.AddInParameter(command, "@SupplyName", DbType.AnsiString, entity.SupplyName);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@OldMoney", DbType.Decimal, entity.OldMoney.HasValue ? ((object) entity.OldMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, entity.GroupCode);
            database.AddInParameter(command, "@BankName", DbType.AnsiString, entity.BankName);
            database.AddInParameter(command, "@BankAccount", DbType.AnsiString, entity.BankAccount);
            database.AddInParameter(command, "@OtherAttachMent", DbType.AnsiString, entity.OtherAttachMent);
            database.AddInParameter(command, "@PayType", DbType.Int32, entity.PayType.HasValue ? ((object) entity.PayType) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Issue", DbType.Int32, entity.Issue.HasValue ? ((object) entity.Issue) : ((object) DBNull.Value));
            database.AddInParameter(command, "@IssueMode", DbType.AnsiString, entity.IssueMode);
            database.AddInParameter(command, "@FactPayDate", DbType.DateTime, entity.FactPayDate.HasValue ? ((object) entity.FactPayDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalPayMoney", DbType.Decimal, entity.TotalPayMoney.HasValue ? ((object) entity.TotalPayMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHMoney", DbType.Decimal, entity.AHMoney.HasValue ? ((object) entity.AHMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@APMoney", DbType.Decimal, entity.APMoney.HasValue ? ((object) entity.APMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UPMoney", DbType.Decimal, entity.UPMoney.HasValue ? ((object) entity.UPMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SupplierApplyMoney", DbType.Decimal, entity.SupplierApplyMoney.HasValue ? ((object) entity.SupplierApplyMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MaxPayMoney", DbType.Decimal, entity.MaxPayMoney.HasValue ? ((object) entity.MaxPayMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanPayMoney", DbType.Decimal, entity.PlanPayMoney.HasValue ? ((object) entity.PlanPayMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ContractMoney", DbType.Decimal, entity.ContractMoney.HasValue ? ((object) entity.ContractMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AdjustedContractMoney", DbType.Decimal, entity.AdjustedContractMoney.HasValue ? ((object) entity.AdjustedContractMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutMoney", DbType.Decimal, entity.PayoutMoney.HasValue ? ((object) entity.PayoutMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash", DbType.Decimal, entity.AHCash.HasValue ? ((object) entity.AHCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@APCash", DbType.Decimal, entity.APCash.HasValue ? ((object) entity.APCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UPCash", DbType.Decimal, entity.UPCash.HasValue ? ((object) entity.UPCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutCash", DbType.Decimal, entity.PayoutCash.HasValue ? ((object) entity.PayoutCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash0", DbType.Decimal, entity.AHCash0.HasValue ? ((object) entity.AHCash0) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash1", DbType.Decimal, entity.AHCash1.HasValue ? ((object) entity.AHCash1) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash2", DbType.Decimal, entity.AHCash2.HasValue ? ((object) entity.AHCash2) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash3", DbType.Decimal, entity.AHCash3.HasValue ? ((object) entity.AHCash3) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash4", DbType.Decimal, entity.AHCash4.HasValue ? ((object) entity.AHCash4) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash5", DbType.Decimal, entity.AHCash5.HasValue ? ((object) entity.AHCash5) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash6", DbType.Decimal, entity.AHCash6.HasValue ? ((object) entity.AHCash6) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash7", DbType.Decimal, entity.AHCash7.HasValue ? ((object) entity.AHCash7) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash8", DbType.Decimal, entity.AHCash8.HasValue ? ((object) entity.AHCash8) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash9", DbType.Decimal, entity.AHCash9.HasValue ? ((object) entity.AHCash9) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PaymentName", DbType.AnsiString, entity.PaymentName);
            database.AddInParameter(command, "@TotalViseChangeMoney", DbType.Decimal, entity.TotalViseChangeMoney.HasValue ? ((object) entity.TotalViseChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SumCode", DbType.AnsiString, entity.SumCode);
            database.AddInParameter(command, "@PaymentCodition", DbType.AnsiString, entity.PaymentCodition);
            database.AddInParameter(command, "@CheckRemark", DbType.AnsiString, entity.CheckRemark);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalPaymentCode = entity.PaymentCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, Payment entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Payment_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@PaymentCode", DbType.AnsiString, entity.PaymentCode);
            database.AddInParameter(command, "@OriginalPaymentCode", DbType.AnsiString, entity.OriginalPaymentCode);
            database.AddInParameter(command, "@PaymentTitle", DbType.AnsiString, entity.PaymentTitle);
            database.AddInParameter(command, "@PaymentID", DbType.AnsiString, entity.PaymentID);
            database.AddInParameter(command, "@VoucherID", DbType.AnsiString, entity.VoucherID);
            database.AddInParameter(command, "@RecieptCount", DbType.Int32, entity.RecieptCount.HasValue ? ((object) entity.RecieptCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@ApplyPerson", DbType.AnsiString, entity.ApplyPerson);
            database.AddInParameter(command, "@ApplyDate", DbType.DateTime, entity.ApplyDate.HasValue ? ((object) entity.ApplyDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Accountant", DbType.AnsiString, entity.Accountant);
            database.AddInParameter(command, "@AccountDate", DbType.DateTime, entity.AccountDate.HasValue ? ((object) entity.AccountDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Payer", DbType.AnsiString, entity.Payer);
            database.AddInParameter(command, "@PayDate", DbType.DateTime, entity.PayDate.HasValue ? ((object) entity.PayDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Purpose", DbType.AnsiString, entity.Purpose);
            database.AddInParameter(command, "@Money", DbType.Decimal, entity.Money.HasValue ? ((object) entity.Money) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckPerson", DbType.AnsiString, entity.CheckPerson);
            database.AddInParameter(command, "@CheckDate", DbType.DateTime, entity.CheckDate.HasValue ? ((object) entity.CheckDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CheckOpinion", DbType.AnsiString, entity.CheckOpinion);
            database.AddInParameter(command, "@IsContract", DbType.Int32, entity.IsContract.HasValue ? ((object) entity.IsContract) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ContractCode", DbType.AnsiString, entity.ContractCode);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@WBSCode", DbType.AnsiString, entity.WBSCode);
            database.AddInParameter(command, "@IsApportion", DbType.Int32, entity.IsApportion.HasValue ? ((object) entity.IsApportion) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SupplyCode", DbType.AnsiString, entity.SupplyCode);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, entity.UnitCode);
            database.AddInParameter(command, "@SupplyName", DbType.AnsiString, entity.SupplyName);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@OldMoney", DbType.Decimal, entity.OldMoney.HasValue ? ((object) entity.OldMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@GroupCode", DbType.AnsiString, entity.GroupCode);
            database.AddInParameter(command, "@BankName", DbType.AnsiString, entity.BankName);
            database.AddInParameter(command, "@BankAccount", DbType.AnsiString, entity.BankAccount);
            database.AddInParameter(command, "@OtherAttachMent", DbType.AnsiString, entity.OtherAttachMent);
            database.AddInParameter(command, "@PayType", DbType.Int32, entity.PayType.HasValue ? ((object) entity.PayType) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Issue", DbType.Int32, entity.Issue.HasValue ? ((object) entity.Issue) : ((object) DBNull.Value));
            database.AddInParameter(command, "@IssueMode", DbType.AnsiString, entity.IssueMode);
            database.AddInParameter(command, "@FactPayDate", DbType.DateTime, entity.FactPayDate.HasValue ? ((object) entity.FactPayDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalPayMoney", DbType.Decimal, entity.TotalPayMoney.HasValue ? ((object) entity.TotalPayMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHMoney", DbType.Decimal, entity.AHMoney.HasValue ? ((object) entity.AHMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@APMoney", DbType.Decimal, entity.APMoney.HasValue ? ((object) entity.APMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UPMoney", DbType.Decimal, entity.UPMoney.HasValue ? ((object) entity.UPMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SupplierApplyMoney", DbType.Decimal, entity.SupplierApplyMoney.HasValue ? ((object) entity.SupplierApplyMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@MaxPayMoney", DbType.Decimal, entity.MaxPayMoney.HasValue ? ((object) entity.MaxPayMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanPayMoney", DbType.Decimal, entity.PlanPayMoney.HasValue ? ((object) entity.PlanPayMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ContractMoney", DbType.Decimal, entity.ContractMoney.HasValue ? ((object) entity.ContractMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AdjustedContractMoney", DbType.Decimal, entity.AdjustedContractMoney.HasValue ? ((object) entity.AdjustedContractMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutMoney", DbType.Decimal, entity.PayoutMoney.HasValue ? ((object) entity.PayoutMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash", DbType.Decimal, entity.AHCash.HasValue ? ((object) entity.AHCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@APCash", DbType.Decimal, entity.APCash.HasValue ? ((object) entity.APCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UPCash", DbType.Decimal, entity.UPCash.HasValue ? ((object) entity.UPCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PayoutCash", DbType.Decimal, entity.PayoutCash.HasValue ? ((object) entity.PayoutCash) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash0", DbType.Decimal, entity.AHCash0.HasValue ? ((object) entity.AHCash0) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash1", DbType.Decimal, entity.AHCash1.HasValue ? ((object) entity.AHCash1) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash2", DbType.Decimal, entity.AHCash2.HasValue ? ((object) entity.AHCash2) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash3", DbType.Decimal, entity.AHCash3.HasValue ? ((object) entity.AHCash3) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash4", DbType.Decimal, entity.AHCash4.HasValue ? ((object) entity.AHCash4) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash5", DbType.Decimal, entity.AHCash5.HasValue ? ((object) entity.AHCash5) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash6", DbType.Decimal, entity.AHCash6.HasValue ? ((object) entity.AHCash6) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash7", DbType.Decimal, entity.AHCash7.HasValue ? ((object) entity.AHCash7) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash8", DbType.Decimal, entity.AHCash8.HasValue ? ((object) entity.AHCash8) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AHCash9", DbType.Decimal, entity.AHCash9.HasValue ? ((object) entity.AHCash9) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PaymentName", DbType.AnsiString, entity.PaymentName);
            database.AddInParameter(command, "@TotalViseChangeMoney", DbType.Decimal, entity.TotalViseChangeMoney.HasValue ? ((object) entity.TotalViseChangeMoney) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SumCode", DbType.AnsiString, entity.SumCode);
            database.AddInParameter(command, "@PaymentCodition", DbType.AnsiString, entity.PaymentCodition);
            database.AddInParameter(command, "@CheckRemark", DbType.AnsiString, entity.CheckRemark);
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
            entity.OriginalPaymentCode = entity.PaymentCode;
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

