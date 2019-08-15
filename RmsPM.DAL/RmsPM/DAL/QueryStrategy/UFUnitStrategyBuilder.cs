namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class UFUnitStrategyBuilder : StandardQueryStringBuilder
    {
        public UFUnitStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("UFUnit", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((UFUnitStrategyName) strategy.Name))
            {
                case UFUnitStrategyName.UFUnitCode:
                    strategy.RelationFieldName = "UFUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UFUnitStrategyName.UFUnitName:
                    strategy.RelationFieldName = "UFUnitName";
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
            UFUnitStrategyName name = (UFUnitStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case UFUnitStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

