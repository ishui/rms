namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAGsdtSB : StandardQueryStringBuilder
    {
        public OAGsdtSB()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAGsdt", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GsdtStrategyName) strategy.Name))
            {
                case GsdtStrategyName.OAGsdtCode:
                    strategy.RelationFieldName = "OAGsdtCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GsdtStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case GsdtStrategyName.Content:
                    strategy.RelationFieldName = "Content";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case GsdtStrategyName.djDate:
                    strategy.RelationFieldName = "djDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case GsdtStrategyName.djPerson:
                    strategy.RelationFieldName = "djPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GsdtStrategyName name = (GsdtStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

