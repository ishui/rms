namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class DynamicCostStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumString = "";

        public DynamicCostStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("DynamicCost", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("DynamicCost", "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((DynamicCostStrategyName) strategy.Name))
            {
                case DynamicCostStrategyName.DynamicCostCode:
                    strategy.RelationFieldName = "DynamicCostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DynamicCostStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DynamicCostStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DynamicCostStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case DynamicCostStrategyName.ModifyDate:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeEqualOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + base.BuildStrategysString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            DynamicCostStrategyName name = (DynamicCostStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case DynamicCostStrategyName.CostCodeIncludeSubNodeAndLeaf:
                        return text;
                }
                string costCode = strategy.GetParameter(0);
                string parameter = strategy.GetParameter(1);
                if (parameter == null)
                {
                    return text;
                }
                if (parameter != "0")
                {
                    if (parameter != "1")
                    {
                        if (parameter != "2")
                        {
                            return text;
                        }
                        return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNotLeafNode);
                    }
                }
                else
                {
                    return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNodeIncludeSelf);
                }
                return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubLeafNode);
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

