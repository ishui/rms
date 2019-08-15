namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PaymentPlanStrategyBuilder : StandardQueryStringBuilder
    {
        public PaymentPlanStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PaymentPlan", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PaymentPlanStrategyName) strategy.Name))
            {
                case PaymentPlanStrategyName.PaymentPlanCode:
                    strategy.RelationFieldName = "PaymentPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentPlanStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentPlanStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PaymentPlanStrategyName.IMonth:
                    strategy.RelationFieldName = "IMonth";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PaymentPlanStrategyName.IsCheck:
                    strategy.RelationFieldName = "IsCheck";
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
            PaymentPlanStrategyName name = (PaymentPlanStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

