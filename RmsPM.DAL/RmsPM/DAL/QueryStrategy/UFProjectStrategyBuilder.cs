namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class UFProjectStrategyBuilder : StandardQueryStringBuilder
    {
        public UFProjectStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("UFProject", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((UFProjectStrategyName) strategy.Name))
            {
                case UFProjectStrategyName.UFProjectCode:
                    strategy.RelationFieldName = "UFProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UFProjectStrategyName.UFProjectName:
                    strategy.RelationFieldName = "UFProjectName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            UFProjectStrategyName name = (UFProjectStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case UFProjectStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

