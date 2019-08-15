namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetDtlHisStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudgetDtlHis", "SelectView").SqlString;

        public CostBudgetDtlHisStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetDtlHis", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetDtlHisStrategyName) strategy.Name))
            {
                case CostBudgetDtlHisStrategyName.CostBudgetDtlHisCode:
                    strategy.RelationFieldName = "CostBudgetDtlHisCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlHisStrategyName.CostBudgetDtlCode:
                    strategy.RelationFieldName = "CostBudgetDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlHisStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlHisStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlHisStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlHisStrategyName.VerID:
                    strategy.RelationFieldName = "VerID";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetDtlHisStrategyName.TargetFlag:
                    strategy.RelationFieldName = "TargetFlag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetDtlHisStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlHisStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CostBudgetDtlHisStrategyName name = (CostBudgetDtlHisStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case CostBudgetDtlHisStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

