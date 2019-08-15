namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TaskBudgetStrategyBuilder : StandardQueryStringBuilder
    {
        private string QuerySumString = "";

        public TaskBudgetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TaskBudget", "SelectView").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("TaskBudget", "QuerySum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TaskBudgetStrategyName) strategy.Name))
            {
                case TaskBudgetStrategyName.TaskBudgetCode:
                    strategy.RelationFieldName = "TaskBudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskBudgetStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskBudgetStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskBudgetStrategyName.PlanningPayDate:
                    strategy.RelationFieldName = "PlanningPayDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            TaskBudgetStrategyName name = (TaskBudgetStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case TaskBudgetStrategyName.WBSCodeEx:
                    {
                        string wbsCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return WBSStrategyBuilder.BuildTreeNodeSearchString(wbsCode, TreeNodeSearchType.AllSubNodeIncludeSelf);

                            case "1":
                                return WBSStrategyBuilder.BuildTreeNodeSearchString(wbsCode, TreeNodeSearchType.AllSubLeafNode);

                            case "2":
                                return WBSStrategyBuilder.BuildTreeNodeSearchString(wbsCode, TreeNodeSearchType.AllSubNotLeafNode);
                        }
                        return text;
                    }
                    case TaskBudgetStrategyName.CostCode:
                        return text;

                    case TaskBudgetStrategyName.CostCodeEx:
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

        public string BuildSumQueryString()
        {
            return (this.QuerySumString + base.BuildStrategysString());
        }
    }
}

