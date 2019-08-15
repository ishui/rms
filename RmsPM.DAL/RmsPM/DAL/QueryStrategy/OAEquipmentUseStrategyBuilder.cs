namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAEquipmentUseStrategyBuilder : StandardQueryStringBuilder
    {
        public OAEquipmentUseStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAEquipmentUse", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAEquipmentUseStrategyName) strategy.Name))
            {
                case OAEquipmentUseStrategyName.OARequipmentUseCode:
                    strategy.RelationFieldName = "OARequipmentUseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentUseStrategyName.EquipmentID:
                    strategy.RelationFieldName = "EquipmentID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentUseStrategyName.UseStaff:
                    strategy.RelationFieldName = "UseStaff";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentUseStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentUseStrategyName.UseTime:
                    strategy.RelationFieldName = "UseTime";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case OAEquipmentUseStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
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
            OAEquipmentUseStrategyName name = (OAEquipmentUseStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OAEquipmentUseStrategyName.OARequipmentUseCode:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

