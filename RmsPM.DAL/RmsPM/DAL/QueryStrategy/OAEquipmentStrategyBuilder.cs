namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAEquipmentStrategyBuilder : StandardQueryStringBuilder
    {
        public OAEquipmentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAEquipment", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAEquipmentStrategyName) strategy.Name))
            {
                case OAEquipmentStrategyName.OAEquipmentCode:
                    strategy.RelationFieldName = "OAEquipmentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentStrategyName.EquipmentName:
                    strategy.RelationFieldName = "EquipmentName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAEquipmentStrategyName.cpu:
                    strategy.RelationFieldName = "cpu";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAEquipmentStrategyName.Ram:
                    strategy.RelationFieldName = "Ram";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAEquipmentStrategyName.DiskType:
                    strategy.RelationFieldName = "DiskType";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAEquipmentStrategyName.Systemer:
                    strategy.RelationFieldName = "Systemer";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OAEquipmentStrategyName.BuyTime:
                    strategy.RelationFieldName = "BuyTime";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case OAEquipmentStrategyName.IsUse:
                    strategy.RelationFieldName = "IsUse";
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
            OAEquipmentStrategyName name = (OAEquipmentStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OAEquipmentStrategyName.OAEquipmentCode:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

