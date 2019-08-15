namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAPersonStrategyBuilder : StandardQueryStringBuilder
    {
        public OAPersonStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAPerson", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAPersonStrategyName) strategy.Name))
            {
                case OAPersonStrategyName.yard:
                    strategy.RelationFieldName = "yard";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonStrategyName.OAPersonCode:
                    strategy.RelationFieldName = "OAPersonCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonStrategyName.cname:
                    strategy.RelationFieldName = "cname";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAPersonStrategyName.birthday:
                    strategy.RelationFieldName = "birthday";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OAPersonStrategyName name = (OAPersonStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

