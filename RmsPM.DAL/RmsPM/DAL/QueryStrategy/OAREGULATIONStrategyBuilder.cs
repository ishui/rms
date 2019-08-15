namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAREGULATIONStrategyBuilder : StandardQueryStringBuilder
    {
        public OAREGULATIONStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAREGULATION", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAREGULATIONStrategyName) strategy.Name))
            {
                case OAREGULATIONStrategyName.OAREGULATIONCODE:
                    strategy.RelationFieldName = "NoticeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAREGULATIONStrategyName.TITLE:
                    strategy.RelationFieldName = "TITLE";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAREGULATIONStrategyName.REG_CLASS:
                    strategy.RelationFieldName = "REG_CLASS";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAREGULATIONStrategyName.DEPARTMENT:
                    strategy.RelationFieldName = "DEPARTMENT";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAREGULATIONStrategyName.PUBLISH_TIME:
                    strategy.RelationFieldName = "PUBLISH_TIME";
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
            OAREGULATIONStrategyName name = (OAREGULATIONStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OAREGULATIONStrategyName.OAREGULATIONCODE:
                        return "";
                }
                return string.Format(" ( (select count(*) from noticeUser where NoticeUser.NoticeCode=Notice.NoticeCode)  = 0 or ( exists ( select * from noticeUser where NoticeUser.NoticeCode=Notice.NoticeCode and NoticeObject='{0}' ))) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

