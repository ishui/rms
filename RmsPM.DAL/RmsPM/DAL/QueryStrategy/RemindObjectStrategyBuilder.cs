namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class RemindObjectStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public RemindObjectStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("RemindObject", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((RemindObjectName) strategy.Name))
            {
                case RemindObjectName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case RemindObjectName.Message:
                    strategy.RelationFieldName = "Message";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case RemindObjectName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case RemindObjectName.EndDate:
                    strategy.RelationFieldName = "EndDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case RemindObjectName.User:
                    strategy.RelationFieldName = "[User]";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RemindObjectName.IsDesk:
                    strategy.RelationFieldName = "IsDesk";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }
    }
}

