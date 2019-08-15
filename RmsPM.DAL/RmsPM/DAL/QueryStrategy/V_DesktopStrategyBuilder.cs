namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class V_DesktopStrategyBuilder : StandardQueryStringBuilder
    {
        public V_DesktopStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_Desktop", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((V_DesktopStrategyName) strategy.Name))
            {
                case V_DesktopStrategyName.CacheTime:
                    strategy.RelationFieldName = "CacheTime";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_DesktopStrategyName.AccessRoles:
                    strategy.RelationFieldName = "AccessRoles";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_DesktopStrategyName.ControlSrc:
                    strategy.RelationFieldName = "ControlSrc";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_DesktopStrategyName.StationID:
                    strategy.RelationFieldName = "StationID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_DesktopStrategyName.StyleID:
                    strategy.RelationFieldName = "StyleID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_DesktopStrategyName.TableID:
                    strategy.RelationFieldName = "TableID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_DesktopStrategyName.parentID:
                    strategy.RelationFieldName = "parentID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_DesktopStrategyName.ControlState:
                    strategy.RelationFieldName = "ControlState";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_DesktopStrategyName.ControlOrder:
                    strategy.RelationFieldName = "ControlOrder";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case V_DesktopStrategyName.StationDesktopID:
                    strategy.RelationFieldName = "StationDesktopID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            V_DesktopStrategyName name = (V_DesktopStrategyName) strategy.Name;
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

