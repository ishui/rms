namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAEquipmentReqStrategyBuilder : StandardQueryStringBuilder
    {
        public OAEquipmentReqStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAEquipmentReq", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAEquipmentReqStrategyName) strategy.Name))
            {
                case OAEquipmentReqStrategyName.OAEquipmentReqCode:
                    strategy.RelationFieldName = "OAEquipmentReqCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentReqStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentReqStrategyName.Stuff:
                    strategy.RelationFieldName = "Stuff";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAEquipmentReqStrategyName.Requirement:
                    strategy.RelationFieldName = "Requirement";
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
            OAEquipmentReqStrategyName name = (OAEquipmentReqStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OAEquipmentReqStrategyName.OAEquipmentReqCode:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

