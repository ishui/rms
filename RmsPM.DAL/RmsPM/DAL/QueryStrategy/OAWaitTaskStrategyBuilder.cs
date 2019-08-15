namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAWaitTaskStrategyBuilder : StandardQueryStringBuilder
    {
        public OAWaitTaskStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAWaitTask", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAWaitTaskStrategyName) strategy.Name))
            {
                case OAWaitTaskStrategyName.OAWaitTaskCode:
                    strategy.RelationFieldName = "NoticeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAWaitTaskStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAWaitTaskStrategyName.BeginDate:
                    strategy.RelationFieldName = "BeginDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case OAWaitTaskStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAWaitTaskStrategyName.PRI:
                    strategy.RelationFieldName = "PRI";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAWaitTaskStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAWaitTaskStrategyName.Type:
                    strategy.RelationFieldName = "Type";
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
            OAWaitTaskStrategyName name = (OAWaitTaskStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OAWaitTaskStrategyName.OAWaitTaskCode:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

