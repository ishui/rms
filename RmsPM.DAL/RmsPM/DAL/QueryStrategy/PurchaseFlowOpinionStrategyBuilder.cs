namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowOpinionStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowOpinionStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlowOpinion", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PurchaseFlowOpinionStrategyName) strategy.Name))
            {
                case PurchaseFlowOpinionStrategyName.PurchaseFlowOpinionCode:
                    strategy.RelationFieldName = "PurchaseFlowOpinionCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowOpinionStrategyName.ObjectCode:
                    strategy.RelationFieldName = "ObjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowOpinionStrategyName.OpinionType:
                    strategy.RelationFieldName = "OpinionType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowOpinionStrategyName.OpinionText:
                    strategy.RelationFieldName = "OpinionText";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowOpinionStrategyName.OpinionUserCode:
                    strategy.RelationFieldName = "OpinionUserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowOpinionStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowOpinionStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
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
            PurchaseFlowOpinionStrategyName name = (PurchaseFlowOpinionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                //switch (name)
                //{
                //    case PurchaseFlowOpinionStrategyName.StateIn:
                //        return "";
                //}
                return string.Format(" isnull(State,'') in ('','{0}')", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

