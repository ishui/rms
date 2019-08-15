namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAContactStrategyBuilder : StandardQueryStringBuilder
    {
        public OAContactStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAContact", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAContactStrategyName) strategy.Name))
            {
                case OAContactStrategyName.OAContactCode:
                    strategy.RelationFieldName = "OAContactCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.title:
                    strategy.RelationFieldName = "title";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.phone:
                    strategy.RelationFieldName = "phone";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.CDate:
                    strategy.RelationFieldName = "CDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.UserSend:
                    strategy.RelationFieldName = "UserSend";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.SubmitReport:
                    strategy.RelationFieldName = "SubmitReport";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.takPerson:
                    strategy.RelationFieldName = "takPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.content:
                    strategy.RelationFieldName = "content";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.takTime:
                    strategy.RelationFieldName = "takTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.Author:
                    strategy.RelationFieldName = "Author";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAContactStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OAContactStrategyName name = (OAContactStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

