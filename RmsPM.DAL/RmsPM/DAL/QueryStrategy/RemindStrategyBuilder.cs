namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class RemindStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public RemindStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("RemindStrategy", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((RemindStrategyName) strategy.Name))
            {
                case RemindStrategyName.RemindStrategyCode:
                    strategy.RelationFieldName = "RemindStrategyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RemindStrategyName.ObjectCode:
                    strategy.RelationFieldName = "ObjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RemindStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case RemindStrategyName.IsActive:
                    strategy.RelationFieldName = "IsActive";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            RemindStrategyName name = (RemindStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case RemindStrategyName.CurUserRemind:
                        return "";
                }
                string parameter = strategy.GetParameter(1);
                parameter = "'" + parameter.Replace(",", "','") + "'";
                return string.Format(" ((type='1' or type='2') and objectcode in (" + parameter + ")) or ((type='0' or type='3') and exists (select 1 from taskperson where type in ('0','3') and usercode='{0}')) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

