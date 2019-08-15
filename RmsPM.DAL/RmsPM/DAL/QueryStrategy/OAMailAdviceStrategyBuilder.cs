namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAMailAdviceStrategyBuilder : StandardQueryStringBuilder
    {
        public OAMailAdviceStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAMailAdvice", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAMailAdviceName) strategy.Name))
            {
                case OAMailAdviceName.OAMailAdviceCode:
                    strategy.RelationFieldName = "OAMailAdviceCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAMailAdviceName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAMailAdviceName.UserName:
                    strategy.RelationFieldName = "UserName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAMailAdviceName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAMailAdviceName.LeadCode:
                    strategy.RelationFieldName = "LeadCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAMailAdviceName.DjDate:
                    strategy.RelationFieldName = "DjDate";
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
            OAMailAdviceName name = (OAMailAdviceName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

