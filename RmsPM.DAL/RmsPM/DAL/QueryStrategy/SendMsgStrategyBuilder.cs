namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SendMsgStrategyBuilder : StandardQueryStringBuilder
    {
        public SendMsgStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SendMsg", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SendMsgStrategyName) strategy.Name))
            {
                case SendMsgStrategyName.SendMsgCode:
                    strategy.RelationFieldName = "SendMsgCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.SendUsercode:
                    strategy.RelationFieldName = "SendUsercode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.ToUsercode:
                    strategy.RelationFieldName = "ToUsercode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.Msg:
                    strategy.RelationFieldName = "Msg";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.Sendtime:
                    strategy.RelationFieldName = "Sendtime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.senddel:
                    strategy.RelationFieldName = "senddel";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.todel:
                    strategy.RelationFieldName = "todel";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SendMsgStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SendMsgStrategyName name = (SendMsgStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

