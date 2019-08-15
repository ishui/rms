namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeMessageStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeMessageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GradeMessage", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeMessageStrategyName) strategy.Name))
            {
                case GradeMessageStrategyName.GradeMessageCode:
                    strategy.RelationFieldName = "GradeMessageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeMessageStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeMessageStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeMessageStrategyName.MainDefineCode:
                    strategy.RelationFieldName = "MainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeMessageStrategyName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeMessageStrategyName.ProjectManage:
                    strategy.RelationFieldName = "ProjectManage";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case GradeMessageStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.NumberIn;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GradeMessageStrategyName name = (GradeMessageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

