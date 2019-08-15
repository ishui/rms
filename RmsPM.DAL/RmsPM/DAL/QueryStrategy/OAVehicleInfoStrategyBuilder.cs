namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAVehicleInfoStrategyBuilder : StandardQueryStringBuilder
    {
        public OAVehicleInfoStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAVehicleInfo", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAVehicleInfoStrategyName) strategy.Name))
            {
                case OAVehicleInfoStrategyName.OAVehicleInfoCode:
                    strategy.RelationFieldName = "OAVehicleInfoCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAVehicleInfoStrategyName.VehicleNO:
                    strategy.RelationFieldName = "VehicleNO";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAVehicleInfoStrategyName.VehicleType:
                    strategy.RelationFieldName = "VehicleType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAVehicleInfoStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAVehicleInfoStrategyName.BuyTime:
                    strategy.RelationFieldName = "Status";
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
            OAVehicleInfoStrategyName name = (OAVehicleInfoStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OAVehicleInfoStrategyName.AccessRange:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

