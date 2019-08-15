namespace RmsPM.BLL
{
    using System;
    using System.Data;

    public class BiddingManage : Bidding
    {
        public DataTable GetBiddingCosts()
        {
            DataTable biddingCosts;
            try
            {
                if (base.BiddingCode == null)
                {
                    throw new Exception("招投标计划代码未实例化！");
                }
                BiddingCost cost = new BiddingCost();
                cost.BiddingCode = base.BiddingCode;
                biddingCosts = cost.GetBiddingCosts();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return biddingCosts;
        }

        public DataTable GetBiddingEmits()
        {
            DataTable biddingEmits;
            try
            {
                if (base.BiddingCode == null)
                {
                    throw new Exception("招投标计划代码未实利化！");
                }
                BiddingEmit emit = new BiddingEmit();
                emit.BiddingCode = base.BiddingCode;
                biddingEmits = emit.GetBiddingEmits();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return biddingEmits;
        }

        public DataTable GetBiddingPrejudications()
        {
            DataTable biddingPrejudications;
            try
            {
                if (base.BiddingCode == null)
                {
                    throw new Exception("招投标计划代码未实例化！");
                }
                BiddingPrejudication prejudication = new BiddingPrejudication();
                prejudication.BiddingCode = base.BiddingCode;
                biddingPrejudications = prejudication.GetBiddingPrejudications();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return biddingPrejudications;
        }

        public static ContractDefaultValue GetContractDefaultValue(string Code)
        {
            ContractDefaultValue value2 = new ContractDefaultValue();
            BiddingMessage message = new BiddingMessage();
            message.BiddingMessageCode = Code;
            value2.BiddingCode = message.BiddingCode;
            value2.ContractName = message.ContractName;
            value2.ContractNumber = message.ContractNember;
            value2.ContractRemark = message.Remark;
            value2.ContractType = message.ContractType;
            value2.SupplierCode = message.Supplier;
            Bidding bidding = new Bidding();
            bidding.BiddingCode = message.BiddingCode;
            value2.Mostly = bidding.Accessory == "1";
            value2.ObligateMoney = bidding.ObligateMoney;
            value2.ProjectCode = bidding.ProjectCode;
            value2.UnitCode = bidding.BiddingRemark1;
            DataRow[] rowArray = bidding.GetBiddingReturn().Select("BiddingReturnCode in (" + message.BiddingReturnCode + "'')");
            BiddingMoney[] moneyArray = new BiddingMoney[rowArray.Length];
            int index = 0;
            decimal num2 = 0M;
            foreach (DataRow row in rowArray)
            {
                BiddingDtl dtl = new BiddingDtl();
                dtl.BiddingDtlCode = row["BiddingDtlCode"].ToString();
                BiddingMoney money = new BiddingMoney();
                money.CostCode = dtl.CostCode;
                money.CostBudgetSetCode = dtl.CostBudgetSetCode;
                money.PBSCode = dtl.PBSCode;
                money.PBSType = dtl.PBSType;
                string text = "";
                Cash_Message message2 = new Cash_Message();
                message2.CashMessageType = "回标";
                message2.CashMessageTypeCode = row["BiddingReturnCode"].ToString();
                DataTable table2 = message2.GetCash_Messages();
                if (table2.Rows.Count > 0)
                {
                    text = table2.Rows[table2.Rows.Count - 1]["CashMessageCode"].ToString();
                }
                Cash_Detail detail = new Cash_Detail();
                detail.Cash_MessageCode = text;
                DataTable table3 = detail.GetCash_Details();
                CashMoney[] moneyArray2 = new CashMoney[table3.Rows.Count];
                int num3 = 0;
                decimal num4 = 0M;
                foreach (DataRow row2 in table3.Select())
                {
                    CashMoney money2 = new CashMoney();
                    money2.MoneyCash = row2["Cash"].ToString();
                    money2.MoneyType = row2["MoneyType"].ToString();
                    money2.ExchangeRate = row2["ExchangeRate"].ToString();
                    money2.RMBTypeCash = row2["RMB"].ToString();
                    moneyArray2[num3] = money2;
                    num4 += ConvertRule.ToDecimal(money2.RMBTypeCash);
                    num3++;
                }
                money.CashMoneys = moneyArray2;
                money.SumCashMoney = num4;
                moneyArray[index] = money;
                num2 += money.SumCashMoney;
                index++;
            }
            value2.BiddingMoneys = moneyArray;
            value2.ContractMoney = num2;
            return value2;
        }

        public string GetLastBiddingEmitCode()
        {
            string text2;
            try
            {
                if (base.BiddingCode == null)
                {
                    throw new Exception("招投标计划代码未实利化！");
                }
                string text = "";
                BiddingEmit emit = new BiddingEmit();
                emit.BiddingCode = base.BiddingCode;
                DataTable biddingEmits = emit.GetBiddingEmits();
                biddingEmits.Select("", "BiddingEmitCode desc");
                if (biddingEmits.Rows.Count != 0)
                {
                    text = biddingEmits.Rows[0]["BiddingEmitCode"].ToString();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }
    }
}

