namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class EmailHistoryStrategyBuilder : StandardQueryStringBuilder
    {
        public EmailHistoryStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("EmailHistory", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((EmailHistoryStrategyName) strategy.Name))
            {
                case EmailHistoryStrategyName.EmailHistoryCode:
                    strategy.RelationFieldName = "EmailHistoryCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EmailHistoryStrategyName.EmailType:
                    strategy.RelationFieldName = "EmailType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EmailHistoryStrategyName.MasterCode:
                    strategy.RelationFieldName = "MasterCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EmailHistoryStrategyName.EmailTitle:
                    strategy.RelationFieldName = "EmailTitle";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EmailHistoryStrategyName.Receiver:
                    strategy.RelationFieldName = "Receiver";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EmailHistoryStrategyName.Sender:
                    strategy.RelationFieldName = "Sender";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EmailHistoryStrategyName.EmailContent:
                    strategy.RelationFieldName = "EmailContent";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EmailHistoryStrategyName.SendDate:
                    strategy.RelationFieldName = "SendDate";
                    strategy.Type = StrategyType.DateTimeEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }
    }
}

