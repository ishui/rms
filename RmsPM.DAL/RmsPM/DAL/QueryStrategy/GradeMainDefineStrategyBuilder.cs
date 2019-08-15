namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeMainDefineStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeMainDefineStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GradeMainDefine", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeMainDefineStrategyName) strategy.Name))
            {
                case GradeMainDefineStrategyName.MainDefineCode:
                    strategy.RelationFieldName = "MainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeMainDefineStrategyName.Name:
                    strategy.RelationFieldName = "Name";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeMainDefineStrategyName.state:
                    strategy.RelationFieldName = "state";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GradeMainDefineStrategyName name = (GradeMainDefineStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

