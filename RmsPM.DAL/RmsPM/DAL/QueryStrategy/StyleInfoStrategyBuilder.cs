namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class StyleInfoStrategyBuilder : StandardQueryStringBuilder
    {
        public StyleInfoStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("StyleInfo", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((StyleInfoStrategyName) strategy.Name))
            {
                case StyleInfoStrategyName.StyleID:
                    strategy.RelationFieldName = "StyleID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case StyleInfoStrategyName.ControlID:
                    strategy.RelationFieldName = "ControlID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case StyleInfoStrategyName.TableID:
                    strategy.RelationFieldName = "TableID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case StyleInfoStrategyName.parentID:
                    strategy.RelationFieldName = "parentID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case StyleInfoStrategyName.ControlOrder:
                    strategy.RelationFieldName = "ControlOrder";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case StyleInfoStrategyName.ControlState:
                    strategy.RelationFieldName = "ControlState";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case StyleInfoStrategyName.SytleName:
                    strategy.RelationFieldName = "SytleName";
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
            StyleInfoStrategyName name = (StyleInfoStrategyName) strategy.Name;
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

