namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using RmsPM.DAL.EntityDAO;

    public class ViseSystem
    {
        public static DataTable GetPayDetail(DataTable tb, string applicationCode)
        {
            CashRule rule = new CashRule();
            rule.CashMessageTypeCode = applicationCode;
            rule.CashMessageType = "现场签证";
            foreach (DataRow row2 in rule.DetailCostTable.Rows)
            {
                DataRow row = tb.NewRow();
                row["PaymentItemCode"] = SystemManageDAO.GetNewSysCode("PaymentItemCode");
                row["CostCode"] = row2["CostCode"];
                row["CostBudgetSetCode"] = row2["CashMessageCostBudgeSetCode"];
                row["PBSType"] = row2["CashMessagePBSType"];
                row["PBSCode"] = row2["CashMessagePBSCode"];
                row["ItemCash"] = row2["Cash"];
                row["MoneyType"] = row2["MoneyType"];
                row["ExchangeRate"] = row2["ExchangeRate"];
                row["ItemMoney"] = row2["RMB"];
                tb.Rows.Add(row);
            }
            return tb;
        }

        public static string ShowStateMessage(string state)
        {
            switch (state)
            {
                case "0":
                    return "未审";

                case "1":
                    return "审核中";

                case "2":
                    return "已评审";

                case "3":
                    return "已结算";

                case "4":
                    return "未通过";
            }
            return "";
        }
    }
}

