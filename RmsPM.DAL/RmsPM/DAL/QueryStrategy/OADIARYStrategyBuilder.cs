namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OADIARYStrategyBuilder : StandardQueryStringBuilder
    {
        public OADIARYStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OADIARY", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OADIARYStrategyName) strategy.Name))
            {
                case OADIARYStrategyName.OADIARYCODE:
                    strategy.RelationFieldName = "NoticeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADIARYStrategyName.USERCODE:
                    strategy.RelationFieldName = "USERCODE";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADIARYStrategyName.CONTEXT:
                    strategy.RelationFieldName = "CONTEXT";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OADIARYStrategyName.curdate:
                    strategy.RelationFieldName = "curdate";
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
            OADIARYStrategyName name = (OADIARYStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OADIARYStrategyName.AccessRange:
                        return "";
                }
                return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "OADIARY", "MasterCode", "UserCode");
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

