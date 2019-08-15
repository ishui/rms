namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetContractStrategyBuilder : StandardQueryStringBuilder
    {
        public CostBudgetContractStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetContract", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetContractStrategyName) strategy.Name))
            {
                case CostBudgetContractStrategyName.CostBudgetContractCode:
                    strategy.RelationFieldName = "CostBudgetContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetContractStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetContractStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetContractStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetContractStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetContractStrategyName.RelationType:
                    strategy.RelationFieldName = "RelationType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetContractStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetContractStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CostBudgetContractStrategyName name = (CostBudgetContractStrategyName) strategy.Name;
            string text2 = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                CostBudgetContractStrategyName name2 = name;
                if (name2 != CostBudgetContractStrategyName.False)
                {
                    if (name2 != CostBudgetContractStrategyName.CostCodeIn)
                    {
                        return text3;
                    }
                }
                else
                {
                    return "1=2";
                }
                text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                if (text2 != "")
                {
                    text3 = string.Format(" CostCode in ({0}) ", text2);
                }
                return text3;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

