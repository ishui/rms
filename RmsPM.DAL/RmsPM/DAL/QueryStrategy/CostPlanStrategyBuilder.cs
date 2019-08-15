namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostPlanStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumString = "";

        public CostPlanStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostPlan", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("CostPlan", "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostPlanStrategyName) strategy.Name))
            {
                case CostPlanStrategyName.CostPlanCode:
                    strategy.RelationFieldName = "CostPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostPlanStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostPlanStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostPlanStrategyName.IMonth:
                    strategy.RelationFieldName = "IMonth";
                    strategy.Type = StrategyType.IntegerEqual;
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
            CostPlanStrategyName name = (CostPlanStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case CostPlanStrategyName.ProjectCode:
                        return string.Format(" CostCode in ( Select CostCode from CBS where ProjectCode = '{0}' )   ", strategy.GetParameter(0));

                    case CostPlanStrategyName.CostCodeIncludeSubNodeAndLeaf:
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
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

