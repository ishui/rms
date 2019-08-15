namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowProcedureStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryWorkFlowCommonString = "";

        public WorkFlowProcedureStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowProcedure", "SelectAll").SqlString;
            this.QueryWorkFlowCommonString = SqlManager.GetSqlStruct("WorkFlowProcedure", "SelectCommon").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowProcedureStrategyName) strategy.Name))
            {
                case WorkFlowProcedureStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "ProcedureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedureStrategyName.ProcedureName:
                    strategy.RelationFieldName = "ProcedureName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedureStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case WorkFlowProcedureStrategyName.SysType:
                    strategy.RelationFieldName = "SysType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedureStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedureStrategyName.Activity:
                    strategy.RelationFieldName = "Activity";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedureStrategyName.ProcedureNameLike:
                    strategy.RelationFieldName = "ProcedureName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case WorkFlowProcedureStrategyName.DescriptionLike:
                    strategy.RelationFieldName = "Description";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case WorkFlowProcedureStrategyName.VersionNumber:
                    strategy.RelationFieldName = "VersionNumber";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryWorkFlowCommonString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            WorkFlowProcedureStrategyName name = (WorkFlowProcedureStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                //switch (name)
                //{
                //    case WorkFlowProcedureStrategyName.AccessRange:
                //        return "";
                //}
                return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("WorkFlowProcedure"), SystemClassDescription.GetItemKeyColumnName("WorkFlowProcedure"), SystemClassDescription.GetItemTypeColumnName("WorkFlowProcedure"), SystemClassDescription.GetItemCreateUserColumnName("WorkFlowProcedure"));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

