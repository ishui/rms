namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ContractAllocationStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumString = "";

        public ContractAllocationStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ContractAllocation", "SelectView").SqlString;
            base.QueryKeyString = SqlManager.GetSqlStruct("ContractAllocation", "SelectKey").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("ContractAllocation", "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ContractAllocationStrategyName) strategy.Name))
            {
                case ContractAllocationStrategyName.AllocateCode:
                    strategy.RelationFieldName = "AllocateCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractAllocationStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractAllocationStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractAllocationStrategyName.ContractDate:
                    strategy.RelationFieldName = "ContractDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case ContractAllocationStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractAllocationStrategyName.PlanningPayDate:
                    strategy.RelationFieldName = "PlanningPayDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryKeyString()
        {
            return (base.QueryKeyString + this.BuildStrategysString());
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + this.BuildStrategysString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ContractAllocationStrategyName name = (ContractAllocationStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ContractAllocationStrategyName.CostCodeIncludeSubNodeAndLeaf:
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
                    case ContractAllocationStrategyName.ContractStatus:
                        return string.Format(" ContractCode in  ( select ContractCode from Contract where status in ({0})  ) ", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

