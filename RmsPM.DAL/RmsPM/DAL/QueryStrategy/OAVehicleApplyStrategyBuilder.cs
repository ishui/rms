namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAVehicleApplyStrategyBuilder : StandardQueryStringBuilder
    {
        public OAVehicleApplyStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAVehicleApply", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAVehicleApplyStrategyName) strategy.Name))
            {
                case OAVehicleApplyStrategyName.OAVehicleApplyCode:
                    strategy.RelationFieldName = "OAVehicleApplyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAVehicleApplyStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAVehicleApplyStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAVehicleApplyStrategyName.OAVehicleInfoCode:
                    strategy.RelationFieldName = "OAVehicleInfoCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAVehicleApplyStrategyName.ApplyDate:
                    strategy.RelationFieldName = "ApplyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OAVehicleApplyStrategyName name = (OAVehicleApplyStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OAVehicleApplyStrategyName.AccessRange:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

