namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAPersonInfoChangeStrategyBuilder : StandardQueryStringBuilder
    {
        public OAPersonInfoChangeStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAPersonInfoChange", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAPersonInfoChangeName) strategy.Name))
            {
                case OAPersonInfoChangeName.OAPersonInfoChangeCode:
                    strategy.RelationFieldName = "OAPersonInfoChangeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonInfoChangeName.UserName:
                    strategy.RelationFieldName = "UserName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAPersonInfoChangeName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonInfoChangeName.unitCode:
                    strategy.RelationFieldName = "unitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonInfoChangeName.DjDate:
                    strategy.RelationFieldName = "DjDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case OAPersonInfoChangeName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringLike;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OAPersonInfoChangeName name = (OAPersonInfoChangeName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

