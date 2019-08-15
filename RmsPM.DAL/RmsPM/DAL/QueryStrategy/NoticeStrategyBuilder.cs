namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class NoticeStrategyBuilder : StandardQueryStringBuilder
    {
        public NoticeStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Notice", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((NoticeStrategyName) strategy.Name))
            {
                case NoticeStrategyName.NoticeCode:
                    strategy.RelationFieldName = "NoticeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case NoticeStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case NoticeStrategyName.UserName:
                    strategy.RelationFieldName = "UserName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case NoticeStrategyName.NoticeClass:
                    strategy.RelationFieldName = "NoticeClass";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case NoticeStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case NoticeStrategyName.SubmitDate:
                    strategy.RelationFieldName = "SubmitDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case NoticeStrategyName.status:
                    strategy.RelationFieldName = "status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case NoticeStrategyName.SubmitPerson:
                    strategy.RelationFieldName = "SubmitPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case NoticeStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            NoticeStrategyName name = (NoticeStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                NoticeStrategyName name2 = name;
                if (name2 != NoticeStrategyName.UserName)
                {
                    if (name2 != NoticeStrategyName.AccessRange)
                    {
                        return "";
                    }
                }
                else
                {
                    return string.Format("NoticeCode in ( select NoticeCode from notice where submitperson in (select * from systemuser where username like '%{0}%') )", strategy.GetParameter(0));
                }
                return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "Notice", "NoticeCode", "SubmitPerson");
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

