namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class V_StyleInfoStrategyBuilder : StandardQueryStringBuilder
    {
        public V_StyleInfoStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_StyleInfo", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((V_StyleInfoStrategyName) strategy.Name))
            {
                case V_StyleInfoStrategyName.ControlID:
                    strategy.RelationFieldName = "ControlID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_StyleInfoStrategyName.StyleID:
                    strategy.RelationFieldName = "StyleID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_StyleInfoStrategyName.ControlTitle:
                    strategy.RelationFieldName = "ControlTitle";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_StyleInfoStrategyName.parentID:
                    strategy.RelationFieldName = "parentID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_StyleInfoStrategyName.TableID:
                    strategy.RelationFieldName = "TableID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_StyleInfoStrategyName.ControlOrder:
                    strategy.RelationFieldName = "ControlOrder";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_StyleInfoStrategyName.ControlState:
                    strategy.RelationFieldName = "ControlState";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_StyleInfoStrategyName.StyleName:
                    strategy.RelationFieldName = "StyleName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            V_StyleInfoStrategyName name = (V_StyleInfoStrategyName) strategy.Name;
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

