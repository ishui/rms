namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BuildingStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryChildCountString;
        public string QueryFullNameString;
        public string QueryRoomCountString;
        public string QuerySumBuildAreaString;
        public string QueryViewString;

        public BuildingStrategyBuilder()
        {
            this.QueryViewString = "";
            this.QuerySumBuildAreaString = "";
            this.QueryRoomCountString = "";
            this.QueryChildCountString = "";
            this.QueryFullNameString = "";
            base.QueryMainString = SqlManager.GetSqlStruct("Building", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("Building", "SelectView").SqlString;
            this.QuerySumBuildAreaString = SqlManager.GetSqlStruct("Building", "SelectSumBuildArea").SqlString;
            this.QueryRoomCountString = SqlManager.GetSqlStruct("Building", "SelectRoomCount").SqlString;
            this.QueryChildCountString = SqlManager.GetSqlStruct("Building", "SelectChildCount").SqlString;
            this.QueryFullNameString = SqlManager.GetSqlStruct("Building", "SelectFullName").SqlString;
            base.IsNeedWhere = true;
        }

        public BuildingStrategyBuilder(string ViewName)
        {
            this.QueryViewString = "";
            this.QuerySumBuildAreaString = "";
            this.QueryRoomCountString = "";
            this.QueryChildCountString = "";
            this.QueryFullNameString = "";
            base.QueryMainString = SqlManager.GetSqlStruct(ViewName, "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BuildingStrategyName) strategy.Name))
            {
                case BuildingStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.InProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case BuildingStrategyName.PBSTypeCode:
                    strategy.RelationFieldName = "PBSTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.BuildingName:
                    strategy.RelationFieldName = "BuildingName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.BuildingNameLike:
                    strategy.RelationFieldName = "BuildingName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case BuildingStrategyName.InBuildingName:
                    strategy.RelationFieldName = "BuildingName";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case BuildingStrategyName.IsArea:
                    strategy.RelationFieldName = "IsArea";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case BuildingStrategyName.UseType:
                    strategy.RelationFieldName = "UseType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.Direction:
                    strategy.RelationFieldName = "Direction";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.VisualProgress:
                    strategy.RelationFieldName = "VisualProgress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.PBSUnitCode:
                    strategy.RelationFieldName = "PBSUnitCode";
                    strategy.Type = StrategyType.StringEqualEx;
                    break;

                case BuildingStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.Layer:
                    strategy.RelationFieldName = "Layer";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.InvestType:
                    strategy.RelationFieldName = "InvestType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStrategyName.InInvestType:
                    strategy.RelationFieldName = "InvestType";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case BuildingStrategyName.InUseType:
                    strategy.RelationFieldName = "UseType";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case BuildingStrategyName.ProjectStatus:
                    strategy.RelationFieldName = "ProjectStatus";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryChildCountString()
        {
            return (this.QueryChildCountString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryFullNameString()
        {
            return (this.QueryFullNameString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryRoomCountString()
        {
            return (this.QueryRoomCountString + base.BuildStrategysString());
        }

        public string BuildQuerySumBuildAreaString()
        {
            return (this.QuerySumBuildAreaString + base.BuildStrategysString());
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BuildingStrategyName name = (BuildingStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case BuildingStrategyName.False:
                        return "1=2";

                    case BuildingStrategyName.BuildingCode:
                        return text;

                    case BuildingStrategyName.BuildingCodeNot:
                        return string.Format("BuildingCode <> '{0}'", strategy.GetParameter(0));

                    case BuildingStrategyName.PBSTypeCodeAllChild:
                        return string.Format("(PBSTypeCode = '{0}' or PBSTypeFullID like '{0}-%')", strategy.GetParameter(0).Trim());

                    case BuildingStrategyName.OutListCode:
                        return string.Format("exists(select * from TempRoomStructure where TempBuildingCode = a.BuildingCode and OutListCode = '{0}')", strategy.GetParameter(0).Trim());

                    case BuildingStrategyName.PaymentItemCodeB:
                        return string.Format("exists(select * from PaymentItemBuilding b where b.BuildingCode = a.BuildingCode and b.PaymentItemCode = '{0}')", strategy.GetParameter(0).Trim());

                    case BuildingStrategyName.PaymentItemCodeU:
                        return string.Format("exists(select * from PaymentItemBuilding b where b.PBSUnitCode = a.PBSUnitCode and b.PaymentItemCode = '{0}')", strategy.GetParameter(0).Trim());

                    case BuildingStrategyName.NullPBSUnit:
                        return "not exists(select * from PBSUnit b where b.PBSUnitCode = a.PBSUnitCode)";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

