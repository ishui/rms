namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class PaymentRule
    {
        public static int IsPayoutMoneyIncludeNotCheck = 0;

        public static void AddBuildingCBVoucherCode(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("VoucherCode"))
                {
                    dt.Columns.Add("VoucherCode", typeof(string));
                }
                if (!dt.Columns.Contains("VoucherID"))
                {
                    dt.Columns.Add("VoucherID", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    string buildingCode = ConvertRule.ToString(row["BuildingCode"]);
                    string projectCode = ConvertRule.ToString(row["ProjectCode"]);
                    if (ConvertRule.ToString(row["VoucherCode"]) == "")
                    {
                        string code = GetBuildingCBVoucherCode(buildingCode, projectCode);
                        row["VoucherCode"] = code;
                        row["VoucherID"] = GetVoucherName(code);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void AutoCreatePayoutFromPayment(string PaymentCode, string User)
        {
            try
            {
                EntityData data = PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                if (data.HasRecord())
                {
                    DataRow currentRow = data.CurrentRow;
                    DataTable table = data.Tables["PaymentItem"];
                    string code = "";
                    PayoutStrategyBuilder builder = new PayoutStrategyBuilder("Payout");
                    builder.AddStrategy(new Strategy(PayoutStrategyName.PaymentCode, PaymentCode));
                    string queryString = builder.BuildMainQueryString();
                    QueryAgent agent = new QueryAgent();
                    EntityData entity = agent.FillEntityData("V_Payout", queryString);
                    if (entity.HasRecord())
                    {
                        code = entity.GetString("PayoutCode");
                    }
                    entity.Dispose();
                    agent.Dispose();
                    entity = PaymentDAO.GetStandard_PayoutByCode(code);
                    DataTable dt = entity.Tables["PayoutItem"];
                    DataRow row = null;
                    if (entity.HasRecord())
                    {
                        row = entity.CurrentRow;
                    }
                    else
                    {
                        row = entity.CurrentTable.NewRow();
                        code = SystemManageDAO.GetNewSysCode("PayoutCode");
                        row["PayoutCode"] = code;
                        row["PayoutID"] = row["PayoutCode"];
                        entity.CurrentTable.Rows.Add(row);
                        string projectName = ProjectRule.GetProjectName(data.GetString("ProjectCode"));
                        string systemGroupCodeBySortID = SystemGroupRule.GetSystemGroupCodeBySortID(SystemGroupRule.GetSystemGroupSortIDByGroupNameAndClassCode(projectName, "0602"), "0602");
                        if (systemGroupCodeBySortID == "")
                        {
                            throw new Exception(string.Format("未找到该项目对应的付款类型（类型名称为“{0}”），不能生成付款单", projectName));
                        }
                        row["GroupCode"] = systemGroupCodeBySortID;
                        row["ProjectCode"] = currentRow["ProjectCode"];
                        row["PayoutDate"] = DateTime.Today;
                        row["Payer"] = currentRow["Payer"];
                        row["IsApportioned"] = 0;
                    }
                    row["InputPerson"] = User;
                    row["InputDate"] = DateTime.Today;
                    row["CheckPerson"] = User;
                    row["CheckDate"] = DateTime.Today;
                    row["Status"] = 1;
                    row["PaymentCodes"] = PaymentCode;
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row3 = dt.Rows[i];
                        string text6 = ConvertRule.ToString(row3["PaymentItemCode"]);
                        if (table.Select("PaymentItemCode='" + text6 + "'").Length == 0)
                        {
                            row3.Delete();
                        }
                    }
                    foreach (DataRow row4 in table.Rows)
                    {
                        DataRow row5;
                        DataRow[] rowArray = dt.Select("PaymentItemCode='" + ConvertRule.ToString(row4["PaymentItemCode"]) + "'");
                        if (rowArray.Length == 0)
                        {
                            row5 = dt.NewRow();
                            string newSysCode = SystemManageDAO.GetNewSysCode("PayoutItemCode");
                            row5["PayoutItemCode"] = newSysCode;
                            row5["PayoutCode"] = code;
                            row5["PaymentItemCode"] = row4["PaymentItemCode"];
                            dt.Rows.Add(row5);
                        }
                        else
                        {
                            row5 = rowArray[0];
                        }
                        row5["PayoutMoney"] = ConvertRule.ToDecimalObj(row4["ItemMoney"]);
                        row5["PayoutCash"] = row5["PayoutMoney"];
                        row5["MoneyType"] = row4["MoneyType"];
                        row5["ExchangeRate"] = ConvertRule.ToDecimalObj(row4["ExchangeRate"]);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        row["MoneyType"] = dt.Rows[0]["MoneyType"];
                        row["ExchangeRate"] = dt.Rows[0]["ExchangeRate"];
                    }
                    row["Money"] = MathRule.SumColumn(dt, "PayoutMoney");
                    row["Cash"] = row["Money"];
                    PaymentDAO.SubmitAllStandard_Payout(entity);
                    entity.Dispose();
                    UpdatePaymentStatusByPayout(code, User);
                }
                data.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable BuildVoucherDetailTableFromBuildingCB(string codes, string ProjectCode)
        {
            EntityData data = new EntityData("VoucherDetail");
            DataTable dt = data.CurrentTable;
            BuildVoucherDetailTableFromBuildingCB(dt, codes, ProjectCode);
            return dt;
        }

        public static DataTable BuildVoucherDetailTableFromBuildingCB(DataTable dt, string codes, string ProjectCode)
        {
            string[] textArray = codes.Split(",".ToCharArray());
            decimal num = 0M;
            for (int i = 0; i < textArray.Length; i++)
            {
                string code = textArray[i];
                EntityData buildingByCode = ProductDAO.GetBuildingByCode(code);
                if (buildingByCode.HasRecord())
                {
                    decimal @decimal = buildingByCode.GetDecimal("TotalCost");
                    if (@decimal != 0M)
                    {
                        num += @decimal;
                        DataRow row = dt.NewRow();
                        row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                        row["DebitMoney"] = @decimal;
                        row["CrebitMoney"] = 0;
                        row["Summary"] = "结转经营成本";
                        row["RelaType"] = "楼栋成本";
                        row["RelaCode"] = code;
                        dt.Rows.Add(row);
                        row = dt.NewRow();
                        row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                        row["CrebitMoney"] = @decimal;
                        row["DebitMoney"] = 0;
                        row["Summary"] = "结转经营成本";
                        row["RelaType"] = "楼栋成本";
                        row["RelaCode"] = code;
                        dt.Rows.Add(row);
                    }
                }
                buildingByCode.Dispose();
            }
            return dt;
        }

        public static DataTable BuildVoucherDetailTableFromSal(string codes, string param, string ProjectCode)
        {
            EntityData data = new EntityData("VoucherDetail");
            DataTable dt = data.CurrentTable;
            int num = BuildVoucherDetailTableFromSal(dt, codes, param, ProjectCode);
            return dt;
        }

        public static int BuildVoucherDetailTableFromSal(DataTable dt, string codes, string param, string ProjectCode)
        {
            DataRow row;
            int num = 0;
            string[] textArray = codes.Split(",".ToCharArray());
            DataTable table = new DataTable();
            table.Columns.Add("ContractCode", typeof(string));
            table.Columns.Add("Money", typeof(decimal));
            table.Columns.Add("SuplCode", typeof(string));
            table.Columns.Add("SuplName", typeof(string));
            table.Columns.Add("RelaCode", typeof(string));
            decimal d = 0M;
            for (int i = 0; i < textArray.Length; i++)
            {
                string code = textArray[i];
                EntityData salPayByCode = SalDAO.GetSalPayByCode(code);
                if (salPayByCode.HasRecord())
                {
                    DataRow row2 = salPayByCode.CurrentTable.Rows[0];
                    string text2 = salPayByCode.GetString("ContractCode");
                    decimal num4 = decimal.Parse(row2["PayMoney"].ToString());
                    if (num4 != 0M)
                    {
                        EntityData salPayPlanByPayCode = SalDAO.GetSalPayPlanByPayCode(code);
                        if (!salPayPlanByPayCode.HasRecord())
                        {
                            DataRow row3 = salPayPlanByPayCode.CurrentTable.NewRow();
                            row3["PayPlanCode"] = text2;
                            row3["ContractCode"] = text2;
                            row3["PlanMoney"] = num4;
                            row3["PayMoney"] = num4;
                            row3["ProjectCode"] = ProjectCode;
                            salPayPlanByPayCode.CurrentTable.Rows.Add(row3);
                        }
                        int count = salPayPlanByPayCode.CurrentTable.Rows.Count;
                        for (int j = 0; j < count; j++)
                        {
                            string salSuplNameBySuplCode;
                            string suplCode;
                            salPayPlanByPayCode.SetCurrentRow(j);
                            decimal @decimal = salPayPlanByPayCode.GetDecimal("PayMoney");
                            d += @decimal;
                            row = dt.NewRow();
                            row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                            dt.Rows.Add(row);
                            row["CrebitMoney"] = 0;
                            try
                            {
                                row["DebitMoney"] = @decimal;
                            }
                            catch
                            {
                                row["DebitMoney"] = 0;
                            }
                            if (param == "1")
                            {
                                row["Summary"] = "面积补差";
                            }
                            else
                            {
                                row["Summary"] = "房款";
                            }
                            row["RelaType"] = "销售收入";
                            row["RelaCode"] = code;
                            DataRow[] rowArray = table.Select("ContractCode='" + text2 + "'");
                            if (rowArray.Length == 0)
                            {
                                EntityData salContractByCode = SalDAO.GetSalContractByCode(text2);
                                suplCode = salContractByCode.GetString("SuplCode");
                                salSuplNameBySuplCode = SalRule.GetSalSuplNameBySuplCode(suplCode, ProjectCode);
                                salContractByCode.Dispose();
                                DataRow row4 = table.NewRow();
                                row4["ContractCode"] = text2;
                                row4["Money"] = @decimal;
                                row4["SuplCode"] = suplCode;
                                row4["SuplName"] = salSuplNameBySuplCode;
                                row4["RelaCode"] = "(" + code + ")";
                                table.Rows.Add(row4);
                            }
                            else
                            {
                                rowArray[0]["Money"] = decimal.Parse(rowArray[0]["Money"].ToString()) + @decimal;
                                suplCode = rowArray[0]["SuplCode"].ToString();
                                salSuplNameBySuplCode = rowArray[0]["SuplName"].ToString();
                                rowArray[0]["RelaCode"] = rowArray[0]["RelaCode"].ToString() + "(" + code + ")";
                            }
                            if (param == "1")
                            {
                                row["SupplyCode"] = suplCode;
                            }
                            else
                            {
                                row["BillNo"] = salSuplNameBySuplCode;
                            }
                        }
                        salPayPlanByPayCode.Dispose();
                    }
                }
                salPayByCode.Dispose();
            }
            string text5 = param;
            if ((text5 != null) && (text5 == "1"))
            {
                if (d < 0M)
                {
                    foreach (DataRow row5 in dt.Rows)
                    {
                        row5["DebitMoney"] = decimal.Negate((decimal) row5["DebitMoney"]);
                    }
                    d = decimal.Negate(d);
                    num = 1;
                    row = dt.NewRow();
                    row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                    row["CrebitMoney"] = d;
                    row["DebitMoney"] = 0;
                    row["Summary"] = "面积补差";
                    row["RelaType"] = "销售收入";
                    dt.Rows.Add(row);
                    return num;
                }
                foreach (DataRow row5 in table.Rows)
                {
                    row = dt.NewRow();
                    row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                    row["CrebitMoney"] = decimal.Parse(row5["Money"].ToString());
                    row["DebitMoney"] = 0;
                    row["Summary"] = "面积补差";
                    row["RelaType"] = "销售收入";
                    row["RelaCode"] = row5["RelaCode"].ToString();
                    row["SupplyCode"] = row5["SuplCode"].ToString();
                    dt.Rows.Add(row);
                }
                return num;
            }
            foreach (DataRow row5 in table.Rows)
            {
                row = dt.NewRow();
                row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                row["CrebitMoney"] = decimal.Parse(row5["Money"].ToString());
                row["DebitMoney"] = 0;
                row["Summary"] = "房款";
                row["RelaType"] = "销售收入";
                row["RelaCode"] = row5["RelaCode"].ToString();
                row["SupplyCode"] = row5["SuplCode"].ToString();
                dt.Rows.Add(row);
            }
            return num;
        }

        public static DataTable BuildVoucherDetailTableFromSal(string codes, string param, string ProjectCode, ref int iReturn)
        {
            EntityData data = new EntityData("VoucherDetail");
            DataTable dt = data.CurrentTable;
            iReturn = BuildVoucherDetailTableFromSal(dt, codes, param, ProjectCode);
            return dt;
        }

        public static DataTable BuildVoucherDetailTableFromSalCB(string codes, decimal cost, string ProjectCode)
        {
            EntityData data = new EntityData("VoucherDetail");
            DataTable dt = data.CurrentTable;
            BuildVoucherDetailTableFromSalCB(dt, codes, cost, ProjectCode);
            return dt;
        }

        public static void BuildVoucherDetailTableFromSalCB(DataTable dt, string codes, decimal cost, string ProjectCode)
        {
            DataRow row;
            string[] textArray = codes.Split(",".ToCharArray());
            decimal num = 0M;
            for (int i = 0; i < textArray.Length; i++)
            {
                string text = textArray[i];
                decimal num3 = cost;
                num += num3;
                row = dt.NewRow();
                row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                dt.Rows.Add(row);
                row["CrebitMoney"] = 0;
                try
                {
                    row["DebitMoney"] = num3;
                }
                catch
                {
                    row["DebitMoney"] = 0;
                }
                row["Summary"] = "结转经营成本";
                row["RelaType"] = "销售成本";
                row["RelaCode"] = text;
            }
            row = dt.NewRow();
            row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
            row["CrebitMoney"] = num;
            row["DebitMoney"] = 0;
            row["Summary"] = "结转经营成本";
            row["RelaType"] = "销售成本";
            dt.Rows.Add(row);
        }

        public static DataTable BuildVoucherDetailTableFromSalJieZhuan(string codes, string param, string ProjectCode)
        {
            EntityData data = new EntityData("VoucherDetail");
            DataTable dt = data.CurrentTable;
            BuildVoucherDetailTableFromSalJieZhuan(dt, codes, param, ProjectCode);
            return dt;
        }

        public static void BuildVoucherDetailTableFromSalJieZhuan(DataTable dt, string codes, string param, string ProjectCode)
        {
            DataRow row;
            string[] textArray = codes.Split(",".ToCharArray());
            decimal num = 0M;
            for (int i = 0; i < textArray.Length; i++)
            {
                string code = textArray[i];
                EntityData salContractByCode = SalDAO.GetSalContractByCode(code);
                if (salContractByCode.HasRecord())
                {
                    DataRow row2 = salContractByCode.CurrentTable.Rows[0];
                    decimal num3 = 0M;
                    try
                    {
                        num3 = decimal.Parse(row2["TotalPayMoney"].ToString());
                    }
                    catch
                    {
                    }
                    num += num3;
                    row = dt.NewRow();
                    row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                    dt.Rows.Add(row);
                    row["CrebitMoney"] = 0;
                    try
                    {
                        row["DebitMoney"] = num3;
                    }
                    catch
                    {
                        row["DebitMoney"] = 0;
                    }
                    row["Summary"] = "结转经营收入";
                    row["RelaType"] = "销售收入结转";
                    row["RelaCode"] = code;
                    row["SupplyCode"] = salContractByCode.GetString("SuplCode");
                }
                salContractByCode.Dispose();
            }
            row = dt.NewRow();
            row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
            row["CrebitMoney"] = num;
            row["DebitMoney"] = 0;
            row["Summary"] = "结转经营收入";
            row["RelaType"] = "销售收入结转";
            dt.Rows.Add(row);
        }

        public static DataTable BuildVoucherDetailTableFromSalJT(string codes, decimal money, string ProjectCode)
        {
            EntityData data = new EntityData("VoucherDetail");
            DataTable dt = data.CurrentTable;
            BuildVoucherDetailTableFromSalJT(dt, codes, money, ProjectCode);
            return dt;
        }

        public static void BuildVoucherDetailTableFromSalJT(DataTable dt, string codes, decimal money, string ProjectCode)
        {
            decimal num = 0M;
            string code = codes;
            decimal val = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            decimal num5 = 0M;
            val = Math.Round((decimal) (money * 0.05M), 2);
            num3 = Math.Round((decimal) (val * 0.07M), 2);
            num4 = Math.Round((decimal) (val * 0.03M), 2);
            num5 = Math.Round((decimal) (val * 0.01M), 2);
            num = (((num + val) + num3) + num4) + num5;
            DataRow row = dt.NewRow();
            row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
            dt.Rows.Add(row);
            row["CrebitMoney"] = 0;
            try
            {
                row["DebitMoney"] = num;
            }
            catch
            {
                row["DebitMoney"] = 0;
            }
            row["Summary"] = "计提税金";
            row["RelaType"] = "销售收入计提税金";
            row["RelaCode"] = code;
            row = VoucherDetailNewRowJT(dt, code, val);
            row = VoucherDetailNewRowJT(dt, code, num3);
            row = VoucherDetailNewRowJT(dt, code, num4);
            row = VoucherDetailNewRowJT(dt, code, num5);
        }

        public static string CanBuildVoucherFromPayment(string codes)
        {
            foreach (string text2 in codes.Split(",".ToCharArray()))
            {
                EntityData payoutByCode = PaymentDAO.GetPayoutByCode(text2);
                try
                {
                    if (payoutByCode.HasRecord())
                    {
                        int @int = payoutByCode.GetInt("Status");
                        string text3 = payoutByCode.GetString("PayoutID");
                        if (@int != 1)
                        {
                            return string.Format("付款单“{0}”未审核，不能生成凭证", text3);
                        }
                        if (IsExistsVoucherFromRelaCode(text2, "付款"))
                        {
                            return string.Format("付款单“{0}”已生成凭证，请检查", text3);
                        }
                    }
                }
                finally
                {
                    payoutByCode.Dispose();
                }
            }
            return "";
        }

        public static string CheckIssueBeforeSave(string Issue, string ContractCode, string PaymentCode)
        {
            try
            {
                if (ConvertRule.ToString(Issue) == "")
                {
                    return "";
                }
                int num = ConvertRule.ToInt(Issue);
                string intString = "";
                if (PaymentCode != "")
                {
                    EntityData paymentByCode = PaymentDAO.GetPaymentByCode(PaymentCode);
                    if (paymentByCode.HasRecord())
                    {
                        intString = paymentByCode.GetIntString("Issue");
                    }
                    paymentByCode.Dispose();
                }
                if (Issue != intString)
                {
                    string val = "";
                    string queryString = string.Format("select max(issue) from Payment where ContractCode = '{0}' and PaymentCode <> '{1}'", ContractCode, PaymentCode);
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        val = ConvertRule.ToString(agent.ExecuteScalar(queryString));
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                    if (val != "")
                    {
                        int num2 = ConvertRule.ToInt(val);
                        if (num <= num2)
                        {
                            return string.Format("请款期数必须大于上期请款期数（第 {0} 期）", num2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        public static void CheckPayment(string PaymentCode, string CheckOpinion, string User)
        {
            try
            {
                EntityData entity = PaymentDAO.GetPaymentByCode(PaymentCode);
                if (entity.HasRecord())
                {
                    DataRow currentRow = entity.CurrentRow;
                    currentRow["Status"] = 1;
                    currentRow["CheckOpinion"] = CheckOpinion;
                    currentRow["CheckDate"] = DateTime.Now;
                    currentRow["CheckPerson"] = User;
                    PaymentDAO.UpdatePayment(entity);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CheckPaymentAccount(string PaymentCode, string User)
        {
            try
            {
                EntityData entity = PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                if (entity.HasRecord())
                {
                    DataRow currentRow = entity.CurrentRow;
                    currentRow["Status"] = 2;
                    currentRow["AccountDate"] = DateTime.Today;
                    currentRow["Accountant"] = User;
                    DataTable dt = entity.Tables["PaymentItem"];
                    foreach (DataRow row2 in dt.Rows)
                    {
                        string objPaymentItemCode = ConvertRule.ToString(row2["PaymentItemCode"]);
                        decimal num = ConvertRule.ToDecimal(row2["ItemMoney"]);
                        decimal num2 = ConvertRule.ToDecimal(row2["ItemCash"]);
                        decimal num3 = ConvertRule.ToDecimal(row2["ExchangeRate"]);
                        row2["OldItemMoney"] = num;
                        decimal payoutCashByPaymentItem = GetPayoutCashByPaymentItem(objPaymentItemCode);
                        if (num2 != payoutCashByPaymentItem)
                        {
                            row2["ItemCash"] = payoutCashByPaymentItem;
                            row2["ItemCash0"] = payoutCashByPaymentItem;
                            row2["ItemMoney"] = Math.Round((decimal) (ConvertRule.ToDecimal(row2["ItemCash"]) * num3), 2);
                        }
                    }
                    currentRow["OldMoney"] = currentRow["Money"];
                    decimal num5 = MathRule.SumColumn(dt, "ItemMoney");
                    currentRow["Money"] = num5;
                    PaymentDAO.UpdateStandard_Payment(entity);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string CheckPaymentCostLimit(string PaymentCode, string ContractCode, DataTable tbCurrentPaymentItem, ref DataTable tbResult)
        {
            string text = "";
            EntityData data = ContractDAO.GetStandard_ContractByCode(ContractCode);
            EntityData paymentByContractCode = PaymentDAO.GetPaymentByContractCode(ContractCode);
            DataTable currentTable = paymentByContractCode.CurrentTable;
            DataTable table2 = data.Tables["ContractCostCash"];
            table2.Columns.Add("PaymentedCash", typeof(decimal));
            foreach (DataRow row in table2.Rows)
            {
                row["PaymentedCash"] = 0;
            }
            foreach (DataRow row2 in currentTable.Rows)
            {
                if (row2["PaymentCode"].ToString() != PaymentCode)
                {
                    EntityData paymentItemByPaymentCode = PaymentDAO.GetPaymentItemByPaymentCode(row2["PaymentCode"].ToString());
                    foreach (DataRow row3 in paymentItemByPaymentCode.CurrentTable.Rows)
                    {
                        foreach (DataRow row4 in table2.Rows)
                        {
                            if (row3["AllocateCode"].ToString() == row4["ContractCostCode"].ToString())
                            {
                                row4["PaymentedCash"] = decimal.Parse(row4["PaymentedCash"].ToString()) + decimal.Parse(row3["ItemCash"].ToString());
                            }
                        }
                    }
                    paymentItemByPaymentCode.Dispose();
                }
            }
            if (tbCurrentPaymentItem != null)
            {
                foreach (DataRow row5 in tbCurrentPaymentItem.Rows)
                {
                    foreach (DataRow row4 in table2.Rows)
                    {
                        if (row5["AllocateCode"].ToString() == row4["ContractCostCode"].ToString())
                        {
                            row4["PaymentedCash"] = decimal.Parse(row4["PaymentedCash"].ToString()) + decimal.Parse(row5["ItemCash"].ToString());
                        }
                    }
                }
            }
            foreach (DataRow row4 in table2.Rows)
            {
                if (decimal.Parse(row4["PaymentedCash"].ToString()) > decimal.Parse(row4["Cash"].ToString()))
                {
                    text = "请款总额超出合同费用总额";
                    DataRow row6 = tbResult.NewRow();
                    row6["title"] = "请款总额检查";
                    row6["desc"] = text;
                    row6["ErrLevel"] = 1;
                    tbResult.Rows.Add(row6);
                    break;
                }
            }
            paymentByContractCode.Dispose();
            data.Dispose();
            return text;
        }

        public static string CheckPaymentItemMoneyByAllocate(string PaymentCode, DataTable tbPaymentItem, DataTable tbAllocate, bool IsContractNew, int status)
        {
            string text2;
            try
            {
                string text = "";
                DataTable tbResult = CreatePaymentCheckResultTable();
                CheckPaymentItemMoneyByAllocate(tbResult, PaymentCode, tbPaymentItem, tbAllocate, IsContractNew, status, false);
                if (tbResult.Rows.Count > 0)
                {
                    text = tbResult.Rows[0]["Desc"].ToString();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void CheckPaymentItemMoneyByAllocate(DataTable tbResult, string PaymentCode, DataTable tbPaymentItem, DataTable tbAllocate, bool IsContractNew, int status, bool isCheckAll)
        {
            try
            {
                if (tbResult == null)
                {
                    tbResult = CreatePaymentCheckResultTable();
                }
                int num = 0;
                DataTable table = PaymentItemGroupByAllocate(tbPaymentItem);
                DataTable table2 = null;
                EntityData paymentItemByPaymentCode = null;
                if (status > 0)
                {
                    paymentItemByPaymentCode = PaymentDAO.GetPaymentItemByPaymentCode(PaymentCode);
                    table2 = PaymentItemGroupByAllocate(paymentItemByPaymentCode.CurrentTable);
                    paymentItemByPaymentCode.Dispose();
                }
                foreach (DataRow row in table.Rows)
                {
                    DataRow[] rowArray;
                    string text = ConvertRule.ToString(row["Summary"]);
                    string text2 = ConvertRule.ToString(row["AllocateCode"]);
                    decimal num2 = ConvertRule.ToDecimal(row["ItemCash"]);
                    decimal num3 = 0M;
                    if (IsContractNew)
                    {
                        rowArray = tbAllocate.Select("ContractCostCode='" + text2 + "'");
                    }
                    else
                    {
                        rowArray = tbAllocate.Select("AllocateCode='" + text2 + "'");
                    }
                    if (rowArray.Length > 0)
                    {
                        num3 = ConvertRule.ToDecimal(rowArray[0]["Cash"]);
                    }
                    decimal num4 = 0M;
                    decimal num5 = 0M;
                    if (table2 != null)
                    {
                        DataRow[] rowArray2 = table2.Select("AllocateCode='" + text2 + "'");
                        if (rowArray2.Length > 0)
                        {
                            num5 = ConvertRule.ToDecimal(rowArray2[0]["ItemMoney"]);
                        }
                    }
                    num4 -= num5;
                    decimal num6 = num3 - num4;
                    if (num2 > num6)
                    {
                        string text3 = "超付";
                        string text4 = string.Format("款项“{0}”的请款金额“{1}”超出了合同的未请款金额“{2}”", text, num2, num6);
                        num++;
                        DataRow row2 = tbResult.NewRow();
                        row2["title"] = text3;
                        row2["desc"] = text4;
                        row2["ErrLevel"] = 1;
                        tbResult.Rows.Add(row2);
                        if (!isCheckAll)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CheckPaymentItemMoneyByDynamicCost(DataTable tbResult, DataTable tbPayment, DataTable tbPaymentItem, int status, bool isCheckAll)
        {
            try
            {
                if (tbPayment.Rows.Count != 0)
                {
                    string paymentCode = tbPayment.Rows[0]["PaymentCode"].ToString();
                    string projectCode = ConvertRule.ToString(tbPayment.Rows[0]["ProjectCode"]);
                    object objPayDate = tbPayment.Rows[0]["PayDate"];
                    CheckPaymentItemMoneyByDynamicCost(tbResult, paymentCode, projectCode, objPayDate, tbPaymentItem, status, isCheckAll);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CheckPaymentItemMoneyByDynamicCost(DataTable tbResult, string PaymentCode, string ProjectCode, object objPayDate, DataTable tbPaymentItem, int status, bool isCheckAll)
        {
            try
            {
                if (tbResult == null)
                {
                    tbResult = CreatePaymentCheckResultTable();
                }
                DateTime payDate = DateTime.Today;
                if (ConvertRule.ToString(objPayDate) != "")
                {
                    payDate = DateTime.Parse(ConvertRule.ToString(objPayDate));
                }
                int budgetControlPower = SystemRule.GetBudgetControlPower(ProjectCode);
                if (budgetControlPower != 1)
                {
                    int num2 = 0;
                    DataTable table = PaymentItemGroupByCostCode(tbPaymentItem);
                    DataTable table2 = null;
                    EntityData paymentItemByPaymentCode = null;
                    if (status > 0)
                    {
                        paymentItemByPaymentCode = PaymentDAO.GetPaymentItemByPaymentCode(PaymentCode);
                        table2 = PaymentItemGroupByCostCode(paymentItemByPaymentCode.CurrentTable);
                        paymentItemByPaymentCode.Dispose();
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        string code;
                        string costCode = ConvertRule.ToString(row["CostCode"]);
                        decimal num3 = ConvertRule.ToDecimal(row["ItemMoney"]);
                        decimal num4 = 0M;
                        if (table2 != null)
                        {
                            DataRow[] rowArray = table2.Select("CostCode='" + costCode + "'");
                            if (rowArray.Length > 0)
                            {
                                num4 = ConvertRule.ToDecimal(rowArray[0]["ItemMoney"]);
                            }
                        }
                        string startDate = "";
                        string endDate = "";
                        decimal dynamicCost = 0M;
                        if (budgetControlPower == 2)
                        {
                            dynamicCost = CBSRule.GetDynamicCost(ProjectCode, costCode);
                            if (dynamicCost == 0M)
                            {
                                code = costCode;
                                while ((dynamicCost == 0M) && (code != ""))
                                {
                                    code = CBSRule.GetParentCostCode(code);
                                    dynamicCost = CBSRule.GetDynamicCost(ProjectCode, code);
                                }
                                if (dynamicCost != 0M)
                                {
                                    costCode = code;
                                }
                            }
                        }
                        else
                        {
                            dynamicCost = CBSRule.GetDynamicCost(ProjectCode, costCode, payDate, ref startDate, ref endDate);
                            if (dynamicCost == 0M)
                            {
                                code = costCode;
                                while ((dynamicCost == 0M) && (code != ""))
                                {
                                    code = CBSRule.GetParentCostCode(code);
                                    dynamicCost = CBSRule.GetDynamicCost(ProjectCode, code, payDate, ref startDate, ref endDate);
                                }
                                if (dynamicCost != 0M)
                                {
                                    costCode = code;
                                }
                            }
                        }
                        decimal num6 = CBSRule.GetAHMoney(costCode, "", payDate.ToString("yyyy-MM-dd")) - num4;
                        decimal num7 = dynamicCost - num6;
                        if (num3 > num7)
                        {
                            string text5 = costCode + " " + CBSRule.GetCostName(costCode);
                            string text6 = "";
                            string text7 = "";
                            if (budgetControlPower == 2)
                            {
                                text6 = "预算不符";
                                text7 = string.Format("费用项“{0}”的请款金额“{1}”超出了剩余动态预算“{2}”", text5, num3, num7);
                            }
                            else
                            {
                                text6 = "预算不符";
                                text7 = string.Format("费用项“{0}”的请款金额“{1}”超出了本期（{3} 至 {4}）剩余动态预算“{2}”", new object[] { text5, num3, num7, startDate, endDate });
                            }
                            num2++;
                            DataRow row2 = tbResult.NewRow();
                            row2["title"] = text6;
                            row2["desc"] = text7;
                            row2["ErrLevel"] = 1;
                            tbResult.Rows.Add(row2);
                            if (!isCheckAll)
                            {
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CheckPaymentMoneyByContractPlan(DataTable tbResult, DataTable tbPayment, DataTable tbPaymentItem, DataTable tbAllocation, DataTable tbCostPlan, bool IsContractNew, int status, bool isCheckAll)
        {
            try
            {
                if (tbPayment.Rows.Count != 0)
                {
                    string paymentCode = tbPayment.Rows[0]["PaymentCode"].ToString();
                    string projectCode = ConvertRule.ToString(tbPayment.Rows[0]["ProjectCode"]);
                    string contractCode = ConvertRule.ToString(tbPayment.Rows[0]["ContractCode"]);
                    object objPayDate = tbPayment.Rows[0]["PayDate"];
                    CheckPaymentMoneyByContractPlan(tbResult, paymentCode, projectCode, contractCode, objPayDate, tbPaymentItem, tbAllocation, tbCostPlan, IsContractNew, status, isCheckAll);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CheckPaymentMoneyByContractPlan(DataTable tbResult, string PaymentCode, string ProjectCode, string ContractCode, object objPayDate, DataTable tbPaymentItem, DataTable tbAllocation, DataTable tbCostPlan, bool IsContractNew, int status, bool isCheckAll)
        {
            try
            {
                if (tbResult == null)
                {
                    tbResult = CreatePaymentCheckResultTable();
                }
                DateTime today = DateTime.Today;
                if (ConvertRule.ToString(objPayDate) != "")
                {
                    today = DateTime.Parse(ConvertRule.ToString(objPayDate));
                }
                DataTable table = PaymentItemGroupByAllocate(tbPaymentItem);
                foreach (DataRow row in table.Rows)
                {
                    string text = ConvertRule.ToString(row["Summary"]);
                    string text2 = ConvertRule.ToString(row["AllocateCode"]);
                    decimal num = ConvertRule.ToDecimal(row["ItemMoney"]);
                    object obj2 = DBNull.Value;
                    DataRow[] rowArray = null;
                    if (!IsContractNew)
                    {
                        rowArray = tbAllocation.Select("AllocateCode='" + text2 + "'");
                    }
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        obj2 = ConvertRule.ToDate(rowArray[0]["PlanningPayDate"]);
                    }
                    if (!IsContractNew && ((obj2 == DBNull.Value) || (today < ((DateTime) obj2))))
                    {
                        string text3 = "";
                        string text4 = "";
                        if (obj2 == DBNull.Value)
                        {
                            text3 = "提前支付";
                            text4 = string.Format("款项 {0} 无合同计划付款日期", text);
                        }
                        else
                        {
                            text3 = "提前支付";
                            text4 = string.Format("款项 {0} 的合同计划付款日期为{1:yyyy-MM-dd}，本次最后付款日{2:yyyy-MM-dd}比计划提前", text, obj2, today);
                        }
                        DataRow row2 = tbResult.NewRow();
                        row2["title"] = text3;
                        row2["desc"] = text4;
                        row2["ErrLevel"] = 0;
                        tbResult.Rows.Add(row2);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CheckPayout(string PayoutCode, string CheckOpinion, string User)
        {
            try
            {
                EntityData entity = PaymentDAO.GetPayoutByCode(PayoutCode);
                if (entity.HasRecord())
                {
                    DataRow currentRow = entity.CurrentRow;
                    currentRow["Status"] = 1;
                    currentRow["CheckOpinion"] = CheckOpinion;
                    currentRow["CheckDate"] = DateTime.Now;
                    currentRow["CheckPerson"] = User;
                    PaymentDAO.UpdatePayout(entity);
                    UpdatePaymentStatusByPayout(PayoutCode, User);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable CreatePaymentCheckResultTable()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Title", typeof(string));
                table.Columns.Add("Desc", typeof(string));
                table.Columns.Add("ErrLevel", typeof(string));
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void DeleteAllUFProject()
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    agent.ExecuteSql("delete UFProject");
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteAllUFUnit()
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    agent.ExecuteSql("delete UFUnit");
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePayment(string PaymentCode)
        {
            try
            {
                EntityData entity = PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                PaymentDAO.DeleteStandard_Payment(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePayout(string PayoutCode)
        {
            try
            {
                EntityData entity = PaymentDAO.GetStandard_PayoutByCode(PayoutCode);
                PaymentDAO.DeleteStandard_Payout(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteVoucher(string VoucherCode)
        {
            try
            {
                EntityData entity = PaymentDAO.GetStandard_VoucherByCode(VoucherCode);
                PaymentDAO.DeleteStandard_Voucher(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GeneratePaymentItemCashTable()
        {
            EntityData data = new EntityData("PaymentItem");
            return GeneratePaymentItemCashTable(data.CurrentTable);
        }

        public static DataTable GeneratePaymentItemCashTable(DataTable pm_dtItem)
        {
            DataTable table2;
            try
            {
                DataTable currentTable;
                if (pm_dtItem == null)
                {
                    EntityData data = new EntityData("PaymentItem");
                    currentTable = data.CurrentTable;
                    data.Dispose();
                }
                else
                {
                    currentTable = pm_dtItem;
                }
                if (!currentTable.Columns.Contains("CostName"))
                {
                    currentTable.Columns.Add("CostName", Type.GetType("System.String"));
                }
                currentTable.Columns.Add("BuildingCodeAll", Type.GetType("System.String"));
                currentTable.Columns.Add("BuildingNameAll", Type.GetType("System.String"));
                currentTable.Columns.Add("TotalPayoutMoney", Type.GetType("System.Decimal"));
                currentTable.Columns.Add("RemainItemMoney", Type.GetType("System.Decimal"));
                GeneratePaymentItemCashTableValue(currentTable);
                table2 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void GeneratePaymentItemCashTableValue(DataRow dr)
        {
            try
            {
                string paymentItemCode = ConvertRule.ToString(dr["PaymentItemCode"]);
                string code = ConvertRule.ToString(dr["CostCode"]);
                string alloType = ConvertRule.ToString(dr["AlloType"]);
                string buildingCodeAll = "";
                string buildingNameAll = "";
                dr["CostName"] = CBSRule.GetCostName(code);
                if ((paymentItemCode != "") && (paymentItemCode.Substring(0, 1) != "-"))
                {
                    GetPaymentItemBuildingNameAll(paymentItemCode, alloType, ref buildingCodeAll, ref buildingNameAll);
                    dr["BuildingCodeAll"] = buildingCodeAll;
                    dr["BuildingNameAll"] = buildingNameAll;
                }
                dr["TotalPayoutMoney"] = GetPayoutMoneyByPaymentItem(paymentItemCode);
                dr["RemainItemMoney"] = ConvertRule.ToDecimal(dr["ItemMoney"]) - ConvertRule.ToDecimal(dr["TotalPayoutMoney"]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GeneratePaymentItemCashTableValue(DataTable pm_DataTable)
        {
            try
            {
                foreach (DataRow row in pm_DataTable.Rows)
                {
                    GeneratePaymentItemCashTableValue(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GeneratePaymentItemTable(DataTable Atb)
        {
            DataTable table2;
            try
            {
                DataTable tb;
                if (Atb == null)
                {
                    EntityData data = new EntityData("PaymentItem");
                    tb = data.CurrentTable;
                    data.Dispose();
                }
                else
                {
                    tb = Atb;
                }
                if (!tb.Columns.Contains("CostName"))
                {
                    tb.Columns.Add("CostName", Type.GetType("System.String"));
                }
                tb.Columns.Add("BuildingCodeAll", Type.GetType("System.String"));
                tb.Columns.Add("BuildingNameAll", Type.GetType("System.String"));
                tb.Columns.Add("TotalPayoutMoney", Type.GetType("System.Decimal"));
                tb.Columns.Add("RemainItemMoney", Type.GetType("System.Decimal"));
                tb.Columns.Add("TotalPayoutCash", Type.GetType("System.Decimal"));
                tb.Columns.Add("RemainItemCash", Type.GetType("System.Decimal"));
                GeneratePaymentItemTableValue(tb);
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void GeneratePaymentItemTableValue(DataRow dr)
        {
            try
            {
                string paymentItemCode = ConvertRule.ToString(dr["PaymentItemCode"]);
                string code = ConvertRule.ToString(dr["CostCode"]);
                string alloType = ConvertRule.ToString(dr["AlloType"]);
                string buildingCodeAll = "";
                string buildingNameAll = "";
                dr["CostName"] = CBSRule.GetCostName(code);
                if ((paymentItemCode != "") && (paymentItemCode.Substring(0, 1) != "-"))
                {
                    GetPaymentItemBuildingNameAll(paymentItemCode, alloType, ref buildingCodeAll, ref buildingNameAll);
                    dr["BuildingCodeAll"] = buildingCodeAll;
                    dr["BuildingNameAll"] = buildingNameAll;
                }
                dr["TotalPayoutMoney"] = GetPayoutMoneyByPaymentItem(paymentItemCode);
                dr["RemainItemMoney"] = ConvertRule.ToDecimal(dr["ItemMoney"]) - ConvertRule.ToDecimal(dr["TotalPayoutMoney"]);
                dr["TotalPayoutCash"] = GetPayoutCashByPaymentItem(paymentItemCode);
                dr["RemainItemCash"] = ConvertRule.ToDecimal(dr["ItemCash"]) - ConvertRule.ToDecimal(dr["TotalPayoutCash"]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GeneratePaymentItemTableValue(DataTable tb)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    GeneratePaymentItemTableValue(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GeneratePayoutItemTable(string PayoutCode)
        {
            DataTable table2;
            try
            {
                PayoutItemStrategyBuilder builder = new PayoutItemStrategyBuilder("V_PayoutItem");
                builder.AddStrategy(new Strategy(PayoutItemStrategyName.PayoutCode, PayoutCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("V_PayoutItem", queryString);
                agent.Dispose();
                DataTable tb = data.CurrentTable;
                data.Dispose();
                if (!tb.Columns.Contains("CostName"))
                {
                    tb.Columns.Add("CostName", typeof(string));
                }
                tb.Columns.Add("BuildingCodeAll", typeof(string));
                tb.Columns.Add("BuildingNameAll", typeof(string));
                tb.Columns.Add("TotalPayoutMoney", typeof(decimal));
                tb.Columns.Add("RemainItemMoney", typeof(decimal));
                tb.Columns.Add("TotalPayoutCash", typeof(decimal));
                tb.Columns.Add("RemainItemCash", typeof(decimal));
                tb.Columns.Add("Checked", typeof(int));
                GeneratePayoutItemTableValue(tb);
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void GeneratePayoutItemTableValue(DataRow dr)
        {
            try
            {
                string payoutItemCode = ConvertRule.ToString(dr["PayoutItemCode"]);
                string objPaymentItemCode = ConvertRule.ToString(dr["PaymentItemCode"]);
                string code = ConvertRule.ToString(dr["CostCode"]);
                string alloType = ConvertRule.ToString(dr["AlloType"]);
                string buildingCodeAll = "";
                string buildingNameAll = "";
                dr["CostName"] = CBSRule.GetCostName(code);
                if ((payoutItemCode != "") && (payoutItemCode.Substring(0, 1) != "-"))
                {
                    GetPayoutItemBuildingNameAll(payoutItemCode, alloType, ref buildingCodeAll, ref buildingNameAll);
                    dr["BuildingCodeAll"] = buildingCodeAll;
                    dr["BuildingNameAll"] = buildingNameAll;
                }
                dr["TotalPayoutMoney"] = GetPayoutMoneyByPaymentItem(objPaymentItemCode);
                dr["RemainItemMoney"] = ConvertRule.ToDecimal(dr["ItemMoney"]) - ConvertRule.ToDecimal(dr["TotalPayoutMoney"]);
                dr["TotalPayoutCash"] = GetPayoutCashByPaymentItem(objPaymentItemCode);
                dr["RemainItemCash"] = ConvertRule.ToDecimal(dr["ItemCash"]) - ConvertRule.ToDecimal(dr["TotalPayoutCash"]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GeneratePayoutItemTableValue(DataTable tb)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    GeneratePayoutItemTableValue(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetAlloTypeName(string AlloType)
        {
            string text2;
            try
            {
                string text = "";
                string text3 = AlloType.ToUpper();
                if (text3 != null)
                {
                    if (text3 != "P")
                    {
                        if (text3 == "U")
                        {
                            goto Label_0043;
                        }
                        if (text3 == "B")
                        {
                            goto Label_004B;
                        }
                    }
                    else
                    {
                        text = "项目";
                    }
                }
                goto Label_0053;
            Label_0043:
                text = "单位工程";
                goto Label_0053;
            Label_004B:
                text = "楼栋";
            Label_0053:
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetAllPayoutItemAlloTypeDetail(string PayoutItemCodes, ref string AlloType, ref string AlloTypeName, ref bool IsManual)
        {
            DataTable table3;
            try
            {
                AlloType = "";
                AlloTypeName = "";
                IsManual = false;
                DataTable table = null;
                string[] textArray = PayoutItemCodes.Split(",".ToCharArray());
                int num = -1;
                foreach (string text in textArray)
                {
                    num++;
                    if (num == 0)
                    {
                        table = GetPayoutItemAlloTypeDetail(text, ref AlloType, ref AlloTypeName, ref IsManual);
                        break;
                    }
                }
                table3 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static string GetBuildingCBVoucherCode(string BuildingCode, string ProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetBuildingCBVoucherCode('{0}', '{1}')", BuildingCode, ProjectCode));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void GetBuildingNameAllByTable(DataTable tb, string FieldBuildingCode, ref string BuildingCodeAll, ref string BuildingNameAll)
        {
            try
            {
                BuildingCodeAll = "";
                BuildingNameAll = "";
                foreach (DataRow row in tb.Rows)
                {
                    string buildingCode = ConvertRule.ToString(row[FieldBuildingCode]);
                    string buildingName = ProductRule.GetBuildingName(buildingCode);
                    if (BuildingCodeAll.Length > 0)
                    {
                        BuildingCodeAll = BuildingCodeAll + ",";
                        BuildingNameAll = BuildingNameAll + ",";
                    }
                    BuildingCodeAll = BuildingCodeAll + buildingCode;
                    BuildingNameAll = BuildingNameAll + buildingName;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static int GetIssueByContractCode(string contractCode)
        {
            int num = 0;
            PaymentStrategyBuilder builder = new PaymentStrategyBuilder();
            builder.AddStrategy(new Strategy(PaymentStrategyName.ContractCode, contractCode));
            string queryString = builder.BuildQueryIssueString() + " Order By Issue Desc";
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("Payment", queryString);
            agent.Dispose();
            if (data.Tables["Payment"].Rows.Count > 0)
            {
                num = (data.Tables["Payment"].Rows[0]["Issue"] == DBNull.Value) ? 0 : ((int) data.Tables["Payment"].Rows[0]["Issue"]);
            }
            num++;
            return num;
        }

        public static string GetNextVoucherCode()
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    text = (ConvertRule.ToInt(agent.ExecuteScalar("select max(cast(VoucherCode as int)) from Voucher where IsNumeric(VoucherCode) = 1")) + 1).ToString();
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static decimal GetPaymentCashByContractCostCashCode(string pm_sContractCostCashCode, int pm_iIncludeNotCheck)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                if (pm_sContractCostCashCode == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPaymentCashByContractCostCashCode('{0}', {1})", pm_sContractCostCashCode, pm_iIncludeNotCheck));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetPaymentID(string PaymentCode)
        {
            string text2;
            try
            {
                string text = "";
                if (PaymentCode != "")
                {
                    EntityData paymentByCode = PaymentDAO.GetPaymentByCode(PaymentCode);
                    if (paymentByCode.HasRecord())
                    {
                        text = paymentByCode.GetString("PaymentID");
                    }
                    paymentByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetPaymentIsContractName(int IsContract)
        {
            string text2;
            try
            {
                string text = "";
                switch (IsContract)
                {
                    case 0:
                        text = "非合同请款";
                        break;

                    case 1:
                        text = "合同请款";
                        break;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void GetPaymentItemBuildingNameAll(string PaymentItemCode, string AlloType, ref string BuildingCodeAll, ref string BuildingNameAll)
        {
            try
            {
                BuildingCodeAll = "";
                BuildingNameAll = "";
                EntityData paymentItemBuildingByPaymentItemCode = PaymentDAO.GetPaymentItemBuildingByPaymentItemCode(PaymentItemCode);
                switch (AlloType.ToUpper())
                {
                    case "P":
                        BuildingNameAll = "项目";
                        break;

                    case "U":
                        GetPBSUnitNameAllByTable(paymentItemBuildingByPaymentItemCode.CurrentTable, "PBSUnitCode", ref BuildingCodeAll, ref BuildingNameAll);
                        break;

                    default:
                        GetBuildingNameAllByTable(paymentItemBuildingByPaymentItemCode.CurrentTable, "BuildingCode", ref BuildingCodeAll, ref BuildingNameAll);
                        break;
                }
                paymentItemBuildingByPaymentItemCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static decimal GetPaymentMoneyByContractAllocate(object objAllocateCode, int IncludeNotCheck)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                string text = ConvertRule.ToString(objAllocateCode);
                if (text == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPaymentMoneyByContractAllocate('{0}', {1})", text, IncludeNotCheck));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetPaymentPlan(string PaymentCode, string ProjectCode, string startDate, string endDate)
        {
            decimal num = 0M;
            PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
            builder.AddStrategy(new Strategy(PaymentItemStrategyName.PaymentCode, PaymentCode));
            builder.AddStrategy(new Strategy(PaymentItemStrategyName.ProjectCode, ProjectCode));
            if ((startDate != "") || (endDate != ""))
            {
                ArrayList pas = new ArrayList();
                pas.Add(startDate);
                pas.Add(endDate);
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, pas));
            }
            string queryString = builder.BuildQueryViewString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("V_PaymentItem", queryString);
            agent.Dispose();
            string[] arrColumnName = new string[] { "ItemMoney" };
            num = MathRule.SumColumn(data.CurrentTable, arrColumnName)[0];
            data.Dispose();
            return num;
        }

        public static string GetPaymentStatusName(int status)
        {
            string text2;
            try
            {
                string text = "";
                switch (status)
                {
                    case 0:
                        text = "申请";
                        break;

                    case 1:
                        text = "已审";
                        break;

                    case 2:
                        text = "结算";
                        break;

                    case 3:
                        text = "申请流程中";
                        break;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static decimal GetPaymentUHCashByContractCostCashCode(string pm_sContractCostCashCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                if (pm_sContractCostCashCode == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPaymentUHCashByContractCostCashCode('{0}')", pm_sContractCostCashCode));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetPayoutCashByPaymentItem(object objPaymentItemCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                string text = ConvertRule.ToString(objPaymentItemCode);
                if (text == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPayoutCashByPaymentItem('{0}', {1})", text, IsPayoutMoneyIncludeNotCheck));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetPayoutCashByPaymentItemIncludeNotCheck(object objPaymentItemCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                string text = ConvertRule.ToString(objPaymentItemCode);
                if (text == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPayoutCashByPaymentItem('{0}', {1})", text, 1));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetPayoutID(string PayoutCode)
        {
            string text2;
            try
            {
                string text = "";
                if (PayoutCode != "")
                {
                    EntityData payoutByCode = PaymentDAO.GetPayoutByCode(PayoutCode);
                    if (payoutByCode.HasRecord())
                    {
                        text = payoutByCode.GetString("PayoutID");
                    }
                    payoutByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetPayoutItemAlloTypeDetail(string PayoutItemCode, ref string AlloType, ref string AlloTypeName, ref bool IsManual)
        {
            DataTable table2;
            try
            {
                AlloType = "";
                AlloTypeName = "";
                IsManual = false;
                DataTable table = new DataTable();
                table.Columns.Add("BuildingCode", typeof(string));
                table.Columns.Add("BuildingName", typeof(string));
                table.Columns.Add("Money", typeof(decimal));
                EntityData payoutItemByCode = PaymentDAO.GetPayoutItemByCode(PayoutItemCode);
                if (payoutItemByCode.HasRecord())
                {
                    AlloType = payoutItemByCode.GetString("AlloType");
                    AlloTypeName = GetAlloTypeName(AlloType);
                    if ((AlloType == "B") || (AlloType == "U"))
                    {
                        EntityData payoutItemBuildingByPayoutItemCode = PaymentDAO.GetPayoutItemBuildingByPayoutItemCode(PayoutItemCode);
                        foreach (DataRow row in payoutItemBuildingByPayoutItemCode.CurrentTable.Rows)
                        {
                            DataRow row2 = table.NewRow();
                            if (AlloType == "B")
                            {
                                string buildingCode = ConvertRule.ToString(row["BuildingCode"]);
                                row2["BuildingCode"] = buildingCode;
                                row2["BuildingName"] = ProductRule.GetBuildingName(buildingCode);
                            }
                            else
                            {
                                string pbsUnitCode = ConvertRule.ToString(row["PBSUnitCode"]);
                                row2["BuildingCode"] = pbsUnitCode;
                                row2["BuildingName"] = ProductRule.GetPBSUnitName(pbsUnitCode);
                            }
                            row2["Money"] = row["ItemBuildingMoney"];
                            if (ConvertRule.ToDecimal(row["ItemBuildingMoney"]) != 0M)
                            {
                                IsManual = true;
                            }
                            table.Rows.Add(row2);
                        }
                        payoutItemBuildingByPayoutItemCode.Dispose();
                    }
                }
                payoutItemByCode.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void GetPayoutItemBuildingNameAll(string PayoutItemCode, string AlloType, ref string BuildingCodeAll, ref string BuildingNameAll)
        {
            try
            {
                BuildingCodeAll = "";
                BuildingNameAll = "";
                EntityData payoutItemBuildingByPayoutItemCode = PaymentDAO.GetPayoutItemBuildingByPayoutItemCode(PayoutItemCode);
                switch (AlloType.ToUpper())
                {
                    case "P":
                        BuildingNameAll = "项目";
                        break;

                    case "U":
                        GetPBSUnitNameAllByTable(payoutItemBuildingByPayoutItemCode.CurrentTable, "PBSUnitCode", ref BuildingCodeAll, ref BuildingNameAll);
                        break;

                    default:
                        GetBuildingNameAllByTable(payoutItemBuildingByPayoutItemCode.CurrentTable, "BuildingCode", ref BuildingCodeAll, ref BuildingNameAll);
                        break;
                }
                payoutItemBuildingByPayoutItemCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static decimal GetPayoutMoneyByPayment(object objPaymentCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                string text = ConvertRule.ToString(objPaymentCode);
                if (text == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPayoutMoneyByPayment('{0}', {1})", text, IsPayoutMoneyIncludeNotCheck));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetPayoutMoneyByPayment(string objPaymentCode, string calBegin, string calEnd, string strProjectCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                if (objPaymentCode == "")
                {
                    return num;
                }
                SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup");
                string queryString = " select * from V_Payout where  ProjectCode =  '" + strProjectCode + "'  and exists (select 1 from V_PayoutItem i where i.PayoutCode = V_Payout.PayoutCode and i.PaymentCode = '" + objPaymentCode + "') ";
                if (calBegin.Length > 0)
                {
                    queryString = queryString + "and  convert(datetime,convert(varchar(10),PayoutDate,121)) >= '" + calBegin + "' ";
                }
                if (calEnd.Length > 0)
                {
                    queryString = queryString + "and  convert(datetime,convert(varchar(10),PayoutDate,121)) <= '" + calEnd + "' ";
                }
                if (IsPayoutMoneyIncludeNotCheck == 0)
                {
                    queryString = queryString + "and status > 0";
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("V_Payout", queryString);
                agent.Dispose();
                string[] arrColumnName = new string[] { "Money" };
                num = MathRule.SumColumn(data.CurrentTable, arrColumnName)[0];
                data.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetPayoutMoneyByPaymentItem(object objPaymentItemCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                string text = ConvertRule.ToString(objPaymentItemCode);
                if (text == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPayoutMoneyByPaymentItem('{0}', {1})", text, IsPayoutMoneyIncludeNotCheck));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetPayoutMoneyByPaymentItemIncludeNotCheck(object objPaymentItemCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                string text = ConvertRule.ToString(objPaymentItemCode);
                if (text == "")
                {
                    return num;
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPayoutMoneyByPaymentItem('{0}', {1})", text, 1));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = decimal.Parse(obj2.ToString());
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetPayoutStatusName(int status)
        {
            string text2;
            try
            {
                string text = "";
                switch (status)
                {
                    case 0:
                        text = "待审";
                        break;

                    case 1:
                        text = "已审";
                        break;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void GetPBSUnitNameAllByTable(DataTable tb, string FieldPBSUnitCode, ref string PBSUnitCodeAll, ref string PBSUnitNameAll)
        {
            try
            {
                PBSUnitCodeAll = "";
                PBSUnitNameAll = "";
                foreach (DataRow row in tb.Rows)
                {
                    string pBSUnitCode = ConvertRule.ToString(row[FieldPBSUnitCode]);
                    string pBSUnitName = PBSRule.GetPBSUnitName(pBSUnitCode);
                    if (PBSUnitCodeAll.Length > 0)
                    {
                        PBSUnitCodeAll = PBSUnitCodeAll + ",";
                        PBSUnitNameAll = PBSUnitNameAll + ",";
                    }
                    PBSUnitCodeAll = PBSUnitCodeAll + pBSUnitCode;
                    PBSUnitNameAll = PBSUnitNameAll + pBSUnitName;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetUFProjectName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code != "")
                {
                    EntityData uFProjectByCode = PaymentDAO.GetUFProjectByCode(code);
                    if (uFProjectByCode.HasRecord())
                    {
                        text = uFProjectByCode.GetString("UFProjectName");
                    }
                    uFProjectByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUFUnitName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code != "")
                {
                    EntityData uFUnitByCode = PaymentDAO.GetUFUnitByCode(code);
                    if (uFUnitByCode.HasRecord())
                    {
                        text = uFUnitByCode.GetString("UFUnitName");
                    }
                    uFUnitByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetVoucherName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code != "")
                {
                    EntityData voucherByCode = PaymentDAO.GetVoucherByCode(code);
                    if (voucherByCode.HasRecord())
                    {
                        text = voucherByCode.GetString("VoucherID");
                    }
                    voucherByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static bool IsExistsVoucherFromRelaCode(string codes, string RelaType)
        {
            foreach (string text in codes.Split(",".ToCharArray()))
            {
                EntityData voucherDetailByRelaCode = PaymentDAO.GetVoucherDetailByRelaCode(RelaType, text);
                try
                {
                    if (voucherDetailByRelaCode.HasRecord())
                    {
                        return true;
                    }
                }
                finally
                {
                    voucherDetailByRelaCode.Dispose();
                }
            }
            return false;
        }

        public static bool IsExistsVoucherFromSal(string codes)
        {
            return IsExistsVoucherFromRelaCode(codes, "销售收入");
        }

        public static bool IsExistsVoucherFromSalJZ(string codes)
        {
            return IsExistsVoucherFromRelaCode(codes, "销售收入结转");
        }

        public static bool IsVoucherIDExists(string VoucherID, string VoucherCode, string ProjectCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                VoucherStrategyBuilder builder = new VoucherStrategyBuilder();
                builder.AddStrategy(new Strategy(VoucherStrategyName.VoucherID, VoucherID));
                builder.AddStrategy(new Strategy(VoucherStrategyName.VoucherCodeNot, VoucherCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Voucher", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    flag = true;
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static DataTable PaymentItemGroupByAllocate(DataTable tbPaymentItem)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("AllocateCode", typeof(string));
                table.Columns.Add("Summary", typeof(string));
                table.Columns.Add("ItemMoney", typeof(decimal));
                foreach (DataRow row in tbPaymentItem.Rows)
                {
                    string text = ConvertRule.ToString(row["AllocateCode"]);
                    DataRow row2 = null;
                    DataRow[] rowArray = table.Select("AllocateCode='" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        row2 = rowArray[0];
                    }
                    else
                    {
                        row2 = table.NewRow();
                        row2["AllocateCode"] = text;
                        row2["Summary"] = ConvertRule.ToString(row["Summary"]);
                        row2["ItemMoney"] = 0;
                        table.Rows.Add(row2);
                    }
                    row2["ItemMoney"] = ConvertRule.ToDecimal(row2["ItemMoney"]) + ConvertRule.ToDecimal(row["ItemMoney"]);
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable PaymentItemGroupByCostCode(DataTable tbPaymentItem)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("CostCode", typeof(string));
                table.Columns.Add("ItemMoney", typeof(decimal));
                foreach (DataRow row in tbPaymentItem.Rows)
                {
                    string text = ConvertRule.ToString(row["CostCode"]);
                    DataRow row2 = null;
                    DataRow[] rowArray = table.Select("CostCode='" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        row2 = rowArray[0];
                    }
                    else
                    {
                        row2 = table.NewRow();
                        row2["CostCode"] = text;
                        row2["ItemMoney"] = 0;
                        table.Rows.Add(row2);
                    }
                    row2["ItemMoney"] = ConvertRule.ToDecimal(row2["ItemMoney"]) + ConvertRule.ToDecimal(row["ItemMoney"]);
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static bool PaymentOutStatusChange(string pm_sPaymentCode, int pm_iStatus)
        {
            return PaymentOutStatusChange(pm_sPaymentCode, pm_iStatus, null, true);
        }

        public static bool PaymentOutStatusChange(EntityData pm_Entity, int pm_iStatus)
        {
            return PaymentOutStatusChange(pm_Entity, "", pm_iStatus, null, false);
        }

        public static bool PaymentOutStatusChange(string pm_sPaymentCode, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return PaymentOutStatusChange(pm_sPaymentCode, pm_iStatus, pm_iOriginalStatus, true);
        }

        public static bool PaymentOutStatusChange(EntityData pm_Entity, int pm_iStatus, bool pm_bSubmitData)
        {
            return PaymentOutStatusChange(pm_Entity, "", pm_iStatus, null, pm_bSubmitData);
        }

        public static bool PaymentOutStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return PaymentOutStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, false);
        }

        public static bool PaymentOutStatusChange(StandardEntityDAO dao, string pm_sPaymentCode, int pm_iStatus)
        {
            if (dao == null)
            {
                dao = new StandardEntityDAO("Standard_Payout");
            }
            else
            {
                dao.EntityName = "Standard_Payout";
            }
            EntityData data = PaymentDAO.GetStandard_PayoutByCode(pm_sPaymentCode, dao);
            bool flag = PaymentOutStatusChange(data, "", pm_iStatus, null, false);
            dao.SubmitEntity(data);
            return flag;
        }

        public static bool PaymentOutStatusChange(string pm_sPaymentCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return PaymentOutStatusChange(PaymentDAO.GetStandard_PayoutByCode(pm_sPaymentCode), pm_sPaymentCode, pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool PaymentOutStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return PaymentOutStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool PaymentOutStatusChange(EntityData pm_Entity, string pm_sPayoutCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                pm_Entity.SetCurrentTable("Payout");
                if (pm_sPayoutCode.Trim() == "")
                {
                    if (pm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("PayoutCode='{0}'", pm_sPayoutCode.Trim());
                    if (pm_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in pm_Entity.CurrentTable.Select(filterExpression))
                {
                    if (pm_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["Status"] != DBNull.Value) && ((((int) row["Status"]) == (nullable = pm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["Status"] = pm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["Status"] = pm_iStatus;
                    }
                }
                if (flag && pm_bSubmitData)
                {
                    PaymentDAO.SubmitAllStandard_Payout(pm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool PaymentStatusChange(string pm_sPaymentCode, int pm_iStatus)
        {
            return PaymentStatusChange(pm_sPaymentCode, pm_iStatus, null, true);
        }

        public static bool PaymentStatusChange(EntityData pm_Entity, int pm_iStatus)
        {
            return PaymentStatusChange(pm_Entity, "", pm_iStatus, null, false);
        }

        public static bool PaymentStatusChange(string pm_sPaymentCode, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return PaymentStatusChange(pm_sPaymentCode, pm_iStatus, pm_iOriginalStatus, true);
        }

        public static bool PaymentStatusChange(EntityData pm_Entity, int pm_iStatus, bool pm_bSubmitData)
        {
            return PaymentStatusChange(pm_Entity, "", pm_iStatus, null, pm_bSubmitData);
        }

        public static bool PaymentStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return PaymentStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, false);
        }

        public static bool PaymentStatusChange(StandardEntityDAO dao, string pm_sPaymentCode, int pm_iStatus)
        {
            if (dao == null)
            {
                dao = new StandardEntityDAO("Standard_Payment");
            }
            else
            {
                dao.EntityName = "Standard_Payment";
            }
            EntityData data = PaymentDAO.GetStandard_PaymentByCode(pm_sPaymentCode, dao);
            bool flag = PaymentStatusChange(data, "", pm_iStatus, null, false);
            dao.SubmitEntity(data);
            return flag;
        }

        public static bool PaymentStatusChange(string pm_sPaymentCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return PaymentStatusChange(PaymentDAO.GetStandard_PaymentByCode(pm_sPaymentCode), pm_sPaymentCode, pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool PaymentStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return PaymentStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool PaymentStatusChange(EntityData pm_Entity, string pm_sPaymentCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                pm_Entity.SetCurrentTable("Payment");
                if (pm_sPaymentCode.Trim() == "")
                {
                    if (pm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("PaymentCode='{0}'", pm_sPaymentCode.Trim());
                    if (pm_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in pm_Entity.CurrentTable.Select(filterExpression))
                {
                    if (pm_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["Status"] != DBNull.Value) && ((((int) row["Status"]) == (nullable = pm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["Status"] = pm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["Status"] = pm_iStatus;
                    }
                }
                if (flag && pm_bSubmitData)
                {
                    PaymentDAO.SubmitAllStandard_Payment(pm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void SetPaymentItemCashDefaultFromContractAllocate(DataRow pm_drPaymentItem, string pm_sAllocateCode, string pm_sContractCode, string pm_sContractCostCashCode)
        {
            try
            {
                EntityData data = ContractDAO.GetStandard_ContractByCode(pm_sContractCode);
                if (data.HasRecord())
                {
                    string text = data.GetString("AlloType");
                    foreach (DataRow row in data.Tables["ContractCost"].Select(string.Format("ContractCostCode='{0}'", pm_sAllocateCode)))
                    {
                        pm_drPaymentItem["CostCode"] = row["CostCode"];
                        pm_drPaymentItem["CostName"] = CBSRule.GetCostName(ConvertRule.ToString(pm_drPaymentItem["CostCode"]));
                        pm_drPaymentItem["AlloType"] = text;
                        pm_drPaymentItem["Summary"] = pm_drPaymentItem["CostName"];
                        pm_drPaymentItem["CostBudgetSetCode"] = row["CostBudgetSetCode"];
                        pm_drPaymentItem["PBSType"] = row["PBSType"];
                        pm_drPaymentItem["PBSCode"] = row["PBSCode"];
                    }
                    if (ConvertRule.ToString(pm_drPaymentItem["BuildingCodeAll"]) == "")
                    {
                        string pBSUnitCodeAll = "";
                        string pBSUnitNameAll = "";
                        switch (text.ToUpper())
                        {
                            case "P":
                                pBSUnitNameAll = "项目";
                                break;

                            case "U":
                                GetPBSUnitNameAllByTable(data.Tables["ContractBuilding"], "PBSUnitCode", ref pBSUnitCodeAll, ref pBSUnitNameAll);
                                break;

                            default:
                                GetBuildingNameAllByTable(data.Tables["ContractBuilding"], "BuildingCode", ref pBSUnitCodeAll, ref pBSUnitNameAll);
                                break;
                        }
                        pm_drPaymentItem["BuildingCodeAll"] = pBSUnitCodeAll;
                        pm_drPaymentItem["BuildingNameAll"] = pBSUnitNameAll;
                    }
                    foreach (DataRow row in data.Tables["CointractCostCash"].Select(string.Format("ContractCostCashCode='{0}'", pm_sContractCostCashCode)))
                    {
                        decimal paymentCashByContractCostCashCode = GetPaymentCashByContractCostCashCode(pm_sContractCostCashCode, 1);
                        decimal num3 = ConvertRule.ToDecimal(row["Cash"]) - paymentCashByContractCostCashCode;
                        if (num3 < 0M)
                        {
                            num3 = 0M;
                        }
                        pm_drPaymentItem["ItemCash"] = num3;
                        pm_drPaymentItem["ExchangeRate"] = row["ExchangeRate"];
                        pm_drPaymentItem["ItemMoney"] = num3 * ((decimal) row["ExchangeRate"]);
                    }
                }
                data.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetPaymentItemDefaultFromContractAllocate(DataRow drPaymentItem, string AllocateCode, string ContractCode, bool IsContractNew)
        {
            SetPaymentItemDefaultFromContractAllocate(drPaymentItem, AllocateCode, ContractCode, IsContractNew, "-1");
        }

        public static void SetPaymentItemDefaultFromContractAllocate(DataRow drPaymentItem, string AllocateCode, string ContractCode, bool IsContractNew, string pm_sContractCostCashCode)
        {
            try
            {
                EntityData data = ContractDAO.GetStandard_ContractByCode(ContractCode);
                if (data.HasRecord())
                {
                    DataRow[] rowArray;
                    string text = data.GetString("AlloType");
                    if (IsContractNew)
                    {
                        rowArray = data.Tables["ContractCost"].Select("ContractCostCode='" + AllocateCode + "'");
                    }
                    else
                    {
                        rowArray = data.Tables["ContractAllocation"].Select("AllocateCode='" + AllocateCode + "'");
                    }
                    if (rowArray.Length > 0)
                    {
                        drPaymentItem["CostCode"] = ConvertRule.ToString(rowArray[0]["CostCode"]);
                        drPaymentItem["CostName"] = CBSRule.GetCostName(ConvertRule.ToString(drPaymentItem["CostCode"]));
                        drPaymentItem["AlloType"] = text;
                        if (IsContractNew)
                        {
                            drPaymentItem["Summary"] = drPaymentItem["CostName"];
                            drPaymentItem["CostBudgetSetCode"] = rowArray[0]["CostBudgetSetCode"];
                            drPaymentItem["PBSType"] = rowArray[0]["PBSType"];
                            drPaymentItem["PBSCode"] = rowArray[0]["PBSCode"];
                            if (pm_sContractCostCashCode != "-1")
                            {
                                string filterExpression = "";
                                if (pm_sContractCostCashCode != "")
                                {
                                    filterExpression = string.Format("ContractCostCashCode='{0}'", pm_sContractCostCashCode);
                                }
                                else
                                {
                                    filterExpression = string.Format("ContractCostCode='{0}'", AllocateCode);
                                }
                                foreach (DataRow row in data.Tables["ContractCostCash"].Select(filterExpression))
                                {
                                    drPaymentItem["MoneyType"] = row["MoneyType"];
                                    drPaymentItem["ExchangeRate"] = row["ExchangeRate"];
                                }
                            }
                        }
                        else
                        {
                            drPaymentItem["Summary"] = ConvertRule.ToString(rowArray[0]["ItemName"]);
                        }
                        if (ConvertRule.ToString(drPaymentItem["BuildingCodeAll"]) == "")
                        {
                            string pBSUnitCodeAll = "";
                            string pBSUnitNameAll = "";
                            switch (text.ToUpper())
                            {
                                case "P":
                                    pBSUnitNameAll = "项目";
                                    break;

                                case "U":
                                    GetPBSUnitNameAllByTable(data.Tables["ContractBuilding"], "PBSUnitCode", ref pBSUnitCodeAll, ref pBSUnitNameAll);
                                    break;

                                default:
                                    GetBuildingNameAllByTable(data.Tables["ContractBuilding"], "BuildingCode", ref pBSUnitCodeAll, ref pBSUnitNameAll);
                                    break;
                            }
                            drPaymentItem["BuildingCodeAll"] = pBSUnitCodeAll;
                            drPaymentItem["BuildingNameAll"] = pBSUnitNameAll;
                        }
                    }
                    else
                    {
                        drPaymentItem["Summary"] = DBNull.Value;
                        drPaymentItem["CostCode"] = DBNull.Value;
                        drPaymentItem["CostName"] = DBNull.Value;
                        drPaymentItem["BuildingCodeAll"] = DBNull.Value;
                        drPaymentItem["BuildingNameAll"] = DBNull.Value;
                        drPaymentItem["AlloType"] = DBNull.Value;
                    }
                }
                data.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePaymentStatusByPayout(DataTable tb, string User)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    string code = ConvertRule.ToString(row["PaymentCode"]);
                    bool flag = false;
                    EntityData entity = PaymentDAO.GetPaymentByCode(code);
                    if (entity.HasRecord())
                    {
                        bool flag2 = true;
                        EntityData paymentItemByPaymentCode = PaymentDAO.GetPaymentItemByPaymentCode(code);
                        foreach (DataRow row2 in paymentItemByPaymentCode.CurrentTable.Rows)
                        {
                            string objPaymentItemCode = ConvertRule.ToString(row2["PaymentItemCode"]);
                            decimal num = ConvertRule.ToDecimal(row2["ItemCash"]);
                            if (GetPayoutCashByPaymentItem(objPaymentItemCode) < num)
                            {
                                flag2 = false;
                                break;
                            }
                        }
                        paymentItemByPaymentCode.Dispose();
                        DataRow currentRow = entity.CurrentRow;
                        decimal @decimal = entity.GetDecimal("Money");
                        decimal payoutMoneyByPayment = GetPayoutMoneyByPayment(code);
                        currentRow["PayoutMoney"] = payoutMoneyByPayment;
                        flag = true;
                        if (flag2)
                        {
                            if (entity.GetInt("status") != 2)
                            {
                                currentRow["Status"] = 2;
                                currentRow["AccountDate"] = DateTime.Today;
                                currentRow["Accountant"] = User;
                            }
                        }
                        else if (entity.GetInt("status") == 2)
                        {
                            currentRow["Status"] = 1;
                            currentRow["AccountDate"] = DBNull.Value;
                            currentRow["Accountant"] = DBNull.Value;
                        }
                        if (flag)
                        {
                            PaymentDAO.UpdatePayment(entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePaymentStatusByPayout(string PayoutCode, string User)
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    DataTable table = agent.ExecSqlForDataSet(string.Format("select distinct i.PaymentCode from PayoutItem a, PaymentItem i where a.PayoutCode = '{0}' and a.PaymentItemCode = i.PaymentItemCode", PayoutCode)).Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        string code = ConvertRule.ToString(row["PaymentCode"]);
                        bool flag = false;
                        EntityData entity = PaymentDAO.GetPaymentByCode(code);
                        if (entity.HasRecord())
                        {
                            bool flag2 = true;
                            EntityData paymentItemByPaymentCode = PaymentDAO.GetPaymentItemByPaymentCode(code);
                            foreach (DataRow row2 in paymentItemByPaymentCode.CurrentTable.Rows)
                            {
                                string objPaymentItemCode = ConvertRule.ToString(row2["PaymentItemCode"]);
                                decimal num = ConvertRule.ToDecimal(row2["ItemCash"]);
                                if (GetPayoutCashByPaymentItem(objPaymentItemCode) < num)
                                {
                                    flag2 = false;
                                    break;
                                }
                            }
                            paymentItemByPaymentCode.Dispose();
                            DataRow currentRow = entity.CurrentRow;
                            decimal @decimal = entity.GetDecimal("Money");
                            decimal payoutMoneyByPayment = GetPayoutMoneyByPayment(code);
                            currentRow["PayoutMoney"] = payoutMoneyByPayment;
                            flag = true;
                            if (flag2)
                            {
                                if (entity.GetInt("status") != 2)
                                {
                                    currentRow["Status"] = 2;
                                    currentRow["AccountDate"] = DateTime.Today;
                                    currentRow["Accountant"] = User;
                                }
                            }
                            else if (entity.GetInt("status") == 2)
                            {
                                currentRow["Status"] = 1;
                                currentRow["AccountDate"] = DBNull.Value;
                                currentRow["Accountant"] = DBNull.Value;
                            }
                            if (flag)
                            {
                                PaymentDAO.UpdatePayment(entity);
                            }
                        }
                    }
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void VoucherDetailAddColumnCustName(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("CustName"))
                {
                    dt.Columns.Add("CustName", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (ConvertRule.ToString(row["CustName"]) == "")
                    {
                        string supplierCode = ConvertRule.ToString(row["CustCode"]);
                        row["CustName"] = ProjectRule.GetSupplierName(supplierCode);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void VoucherDetailAddColumnSubjectName(DataTable dt, string SubjectSetCode)
        {
            try
            {
                if (!dt.Columns.Contains("SubjectName"))
                {
                    dt.Columns.Add("SubjectName", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (ConvertRule.ToString(row["SubjectName"]) == "")
                    {
                        string subjectCode = ConvertRule.ToString(row["SubjectCode"]);
                        row["SubjectName"] = SubjectRule.GetSubjectFullName(subjectCode, SubjectSetCode);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void VoucherDetailAddColumnSuplName(DataTable dt, string ProjectCode)
        {
            try
            {
                if (!dt.Columns.Contains("SupplyName"))
                {
                    dt.Columns.Add("SupplyName", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (ConvertRule.ToString(row["SupplyName"]) == "")
                    {
                        string suplCode = ConvertRule.ToString(row["SupplyCode"]);
                        row["SupplyName"] = SalRule.GetSalSuplNameBySuplCode(suplCode, ProjectCode);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void VoucherDetailAddColumnUFProjectName(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("UFProjectName"))
                {
                    dt.Columns.Add("UFProjectName", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (ConvertRule.ToString(row["UFProjectName"]) == "")
                    {
                        string code = ConvertRule.ToString(row["UFProjectCode"]);
                        row["UFProjectName"] = GetUFProjectName(code);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void VoucherDetailAddColumnUFUnitName(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("UFUnitName"))
                {
                    dt.Columns.Add("UFUnitName", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (ConvertRule.ToString(row["UFUnitName"]) == "")
                    {
                        string code = ConvertRule.ToString(row["UFUnitCode"]);
                        row["UFUnitName"] = SystemRule.GetUnitName(code);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataRow VoucherDetailNewRowJT(DataTable dt, string code, decimal val)
        {
            DataRow row = dt.NewRow();
            row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
            dt.Rows.Add(row);
            row["DebitMoney"] = 0;
            try
            {
                row["CrebitMoney"] = val;
            }
            catch
            {
                row["CrebitMoney"] = 0;
            }
            row["Summary"] = "计提税金";
            row["RelaType"] = "销售收入计提税金";
            row["RelaCode"] = code;
            return row;
        }
    }
}

