namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAComputerMaintenanceStrategyBuilder : StandardQueryStringBuilder
    {
        public OAComputerMaintenanceStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAComputerMaintenance", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAComputerMaintenanceStrategyName) strategy.Name))
            {
                case OAComputerMaintenanceStrategyName.ComputerMaintenanceCode:
                    strategy.RelationFieldName = "ComputerMaintenanceCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAComputerMaintenanceStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAComputerMaintenanceStrategyName.ApplyUser:
                    strategy.RelationFieldName = "ApplyUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAComputerMaintenanceStrategyName.MaintenanceItem:
                    strategy.RelationFieldName = "MaintenanceItem";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAComputerMaintenanceStrategyName.MaintenanceContext:
                    strategy.RelationFieldName = "MaintenanceContext";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAComputerMaintenanceStrategyName.ConkOutText:
                    strategy.RelationFieldName = "ConkOutText";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OAComputerMaintenanceStrategyName name = (OAComputerMaintenanceStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

