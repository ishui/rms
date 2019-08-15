namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAMailLeadsStrategyBuilder : StandardQueryStringBuilder
    {
        public OAMailLeadsStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAMailLeads", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAMailLeadsName) strategy.Name))
            {
                case OAMailLeadsName.OAMailLeadsCode:
                    strategy.RelationFieldName = "OAMailLeadsCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAMailLeadsName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAMailLeadsName.UserName:
                    strategy.RelationFieldName = "UserName";
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
            OAMailLeadsName name = (OAMailLeadsName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

