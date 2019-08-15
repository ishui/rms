namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class PayoutItemStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumCashString;
        public string QuerySumMoneyString;
        public string QuerySumString;

        public PayoutItemStrategyBuilder()
        {
            this.QuerySumString = "";
            this.QuerySumMoneyString = "";
            this.QuerySumCashString = "";
            base.QueryMainString = SqlManager.GetSqlStruct("PayoutItem", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public PayoutItemStrategyBuilder(string ViewName)
        {
            this.QuerySumString = "";
            this.QuerySumMoneyString = "";
            this.QuerySumCashString = "";
            base.QueryMainString = SqlManager.GetSqlStruct(ViewName, "SelectAll").SqlString;
            this.QuerySumMoneyString = SqlManager.GetSqlStruct(ViewName, "SelectSumMoney").SqlString;
            this.QuerySumCashString = SqlManager.GetSqlStruct(ViewName, "SelectSumCash").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PayoutItemStrategyName) strategy.Name))
            {
                case PayoutItemStrategyName.PayoutItemCode:
                    strategy.RelationFieldName = "PayoutItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.PayoutCode:
                    strategy.RelationFieldName = "PayoutCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case PayoutItemStrategyName.PayoutDateRange:
                    strategy.RelationFieldName = "PayoutDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case PayoutItemStrategyName.PaymentCode:
                    strategy.RelationFieldName = "PaymentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.PaymentID:
                    strategy.RelationFieldName = "PaymentID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.IsContract:
                    strategy.RelationFieldName = "IsContract";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PayoutItemStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.AllocateCode:
                    strategy.RelationFieldName = "AllocateCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.CostCodeIncludeAllChild:
                    strategy.RelationFieldName = "CostFullCode";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, CBSDAO.GetCBSFullCode(strategy.GetParameter(0)) + "%");
                    break;

                case PayoutItemStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutItemStrategyName.ContractCostCashCode:
                    strategy.RelationFieldName = "ContractCostCashCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumCashString()
        {
            return (this.QuerySumCashString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public string BuildQuerySumMoneyString()
        {
            return (this.QuerySumMoneyString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PayoutItemStrategyName name = (PayoutItemStrategyName) strategy.Name;
            string format = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PayoutItemStrategyName.PBSTypeAndCode:
                        format = "exists (select 1 from PaymentItem i where i.PaymentItemCode = a.PaymentItemCode and i.PBSType = '{0}' and i.PBSCode = '{1}')";
                        return string.Format(format, strategy.GetParameter(0), strategy.GetParameter(1));

                    case PayoutItemStrategyName.SubjectCode:
                        return string.Format("exists (select * from CBS c where c.CostCode = a.CostCode and c.SubjectCode = '{0}')", strategy.GetParameter(0));

                    case PayoutItemStrategyName.SubjectCodeIncludeAllChild:
                        return string.Format("exists (select * from CBS c where c.CostCode = a.CostCode and c.SubjectCode like '{0}%')", strategy.GetParameter(0));
                }
                return format;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

