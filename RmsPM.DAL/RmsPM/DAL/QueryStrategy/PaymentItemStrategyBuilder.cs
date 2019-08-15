namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class PaymentItemStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumCashString = "";
        public string QuerySumMoneyString = "";
        public string QueryViewString = "";

        public PaymentItemStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PaymentItem", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("PaymentItem", "SelectView").SqlString;
            this.QuerySumMoneyString = SqlManager.GetSqlStruct("PaymentItem", "SelectSumMoney").SqlString;
            this.QuerySumCashString = SqlManager.GetSqlStruct("PaymentItem", "SelectSumCash").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PaymentItemStrategyName) strategy.Name))
            {
                case PaymentItemStrategyName.PaymentItemCode:
                    strategy.RelationFieldName = "PaymentItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.PaymentCode:
                    strategy.RelationFieldName = "PaymentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.PaymentType:
                    strategy.RelationFieldName = "PaymentType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case PaymentItemStrategyName.IsContract:
                    strategy.RelationFieldName = "IsContract";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PaymentItemStrategyName.AllocateCode:
                    strategy.RelationFieldName = "AllocateCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.CostCodeIncludeAllChild:
                    strategy.RelationFieldName = "CostFullCode";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, CBSDAO.GetCBSFullCode(strategy.GetParameter(0)) + "%");
                    break;

                case PaymentItemStrategyName.PayDate:
                    strategy.RelationFieldName = "PayDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case PaymentItemStrategyName.IsApportion:
                    strategy.RelationFieldName = "IsApportion";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PaymentItemStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.ContractCostPlanCode:
                    strategy.RelationFieldName = "ContractCostPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.ContractCostCashCode:
                    strategy.RelationFieldName = "ContractCostCashCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
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

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PaymentItemStrategyName name = (PaymentItemStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PaymentItemStrategyName.AllocateCodeKeyIn:
                        return string.Format(" AllocateCode in ( {0} ) ", strategy.GetParameter(0));

                    case PaymentItemStrategyName.CostCodeIncludeSubNodeAndLeaf:
                    {
                        string costCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNodeIncludeSelf);

                            case "1":
                                return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubLeafNode);

                            case "2":
                                return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNotLeafNode);
                        }
                        return text;
                    }
                    case PaymentItemStrategyName.IsPayout:
                        switch (strategy.GetParameter(0))
                        {
                            case "0":
                                return " isnull(TotalPayoutMoney, 0) = 0";

                            case "1":
                                return " isnull(TotalPayoutMoney, 0) > 0 and isnull(TotalPayoutMoney, 0) < isnull(Money, 0)";

                            case "2":
                                return " isnull(TotalPayoutMoney, 0) >= isnull(Money, 0)";

                            case "1,2":
                                return " isnull(TotalPayoutMoney, 0) > 0";

                            case "0,1":
                                return " isnull(TotalPayoutMoney, 0) < isnull(Money, 0)";
                        }
                        return text;

                    case PaymentItemStrategyName.ContractCostCashCode:
                    case PaymentItemStrategyName.PBSType:
                    case PaymentItemStrategyName.PBSCode:
                        return text;

                    case PaymentItemStrategyName.MaxIssue:
                        return string.Format("Issue <= {0}", strategy.GetParameter(0).Trim());

                    case PaymentItemStrategyName.MinIssue:
                        return string.Format("Issue >= {0}", strategy.GetParameter(0).Trim());

                    case PaymentItemStrategyName.SubjectCode:
                        return string.Format("exists (select * from CBS c where c.CostCode = PaymentItem.CostCode and c.SubjectCode = '{0}')", strategy.GetParameter(0));

                    case PaymentItemStrategyName.SubjectCodeIncludeAllChild:
                        return string.Format("exists (select * from CBS c where c.CostCode = PaymentItem.CostCode and c.SubjectCode like '{0}%')", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

