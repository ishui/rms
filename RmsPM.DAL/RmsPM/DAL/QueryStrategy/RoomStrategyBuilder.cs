namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class RoomStrategyBuilder : StandardQueryStringBuilder
    {
        private string QuerySumBuildingAreaPreString;
        private string QuerySumBuildingAreaString;

        public RoomStrategyBuilder()
        {
            this.QuerySumBuildingAreaString = SqlManager.GetSqlStruct("Room", "SumBuildingArea").SqlString;
            this.QuerySumBuildingAreaPreString = SqlManager.GetSqlStruct("Room", "SumBuildingAreaPre").SqlString;
            base.QueryMainString = SqlManager.GetSqlStruct("Room", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public RoomStrategyBuilder(string EntityName)
        {
            this.QuerySumBuildingAreaString = SqlManager.GetSqlStruct("Room", "SumBuildingArea").SqlString;
            this.QuerySumBuildingAreaPreString = SqlManager.GetSqlStruct("Room", "SumBuildingAreaPre").SqlString;
            base.QueryMainString = SqlManager.GetSqlStruct(EntityName, "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((RoomStrategyName) strategy.Name))
            {
                case RoomStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.RoomCode:
                    strategy.RelationFieldName = "RoomCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.RoomName:
                    strategy.RelationFieldName = "RoomName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InRoomName:
                    strategy.RelationFieldName = "RoomName";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case RoomStrategyName.ChamberCode:
                    strategy.RelationFieldName = "ChamberCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.ModelCode:
                    strategy.RelationFieldName = "ModelCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.FloorIndex:
                    strategy.RelationFieldName = "FloorIndex";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case RoomStrategyName.BuildArea:
                    strategy.RelationFieldName = "BuildArea";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case RoomStrategyName.OutAspect:
                    strategy.RelationFieldName = "OutAspect";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InOutAspect:
                    strategy.RelationFieldName = "OutAspect";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case RoomStrategyName.InDateRange:
                    strategy.RelationFieldName = "InDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case RoomStrategyName.OutDateRange:
                    strategy.RelationFieldName = "OutDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case RoomStrategyName.BofangDateRange:
                    strategy.RelationFieldName = "BofangDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case RoomStrategyName.BofangName:
                    strategy.RelationFieldName = "BofangName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case RoomStrategyName.KgYear:
                    strategy.RelationFieldName = "kgDate";
                    strategy.Type = StrategyType.DateTimeEqualYear;
                    break;

                case RoomStrategyName.JgYear:
                    strategy.RelationFieldName = "jgDate";
                    strategy.Type = StrategyType.DateTimeEqualYear;
                    break;

                case RoomStrategyName.IFloorCount:
                    strategy.RelationFieldName = "IFloorCount";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                case RoomStrategyName.BuildingName:
                    strategy.RelationFieldName = "BuildingName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InBuildingName:
                    strategy.RelationFieldName = "BuildingName";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case RoomStrategyName.ChamberName:
                    strategy.RelationFieldName = "ChamberName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InChamberName:
                    strategy.RelationFieldName = "ChamberName";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case RoomStrategyName.ChamberNameLike:
                    strategy.RelationFieldName = "ChamberName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case RoomStrategyName.PBSTypeCode:
                    strategy.RelationFieldName = "PBSTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InvestType:
                    strategy.RelationFieldName = "InvestType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InInvestType:
                    strategy.RelationFieldName = "InvestType";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case RoomStrategyName.UseType:
                    strategy.RelationFieldName = "UseType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RoomStrategyName.InUseType:
                    strategy.RelationFieldName = "UseType";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                case RoomStrategyName.BofangType:
                    strategy.RelationFieldName = "BofangName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case RoomStrategyName.BofangYear:
                    strategy.RelationFieldName = "BofangYear";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case RoomStrategyName.BofangSnoRange:
                    strategy.RelationFieldName = "BofangSno";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumBuildAreaPreString()
        {
            return (this.QuerySumBuildingAreaPreString + base.BuildStrategysString());
        }

        public string BuildQuerySumBuildAreaString()
        {
            return (this.QuerySumBuildingAreaString + base.BuildStrategysString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            RoomStrategyName name = (RoomStrategyName) strategy.Name;
            string text2 = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case RoomStrategyName.InBuildingCode:
                        text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text2 != "")
                        {
                            text3 = string.Format(" BuildingCode in ({0}) ", text2);
                        }
                        return text3;

                    case RoomStrategyName.OutAspect:
                    case RoomStrategyName.InOutAspect:
                        return text3;

                    case RoomStrategyName.InvState:
                        return string.Format("isnull(InvState,'') ='{0}' ", strategy.GetParameter(0).Trim());

                    case RoomStrategyName.InInvState:
                        text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text2 != "")
                        {
                            text3 = string.Format(" isnull(InvState, '') in ({0}) ", text2);
                        }
                        return text3;

                    case RoomStrategyName.OutState:
                        return string.Format("isnull(OutState,'') ='{0}' ", strategy.GetParameter(0).Trim());

                    case RoomStrategyName.SalState:
                        return string.Format("isnull(SalState,'') ='{0}' ", strategy.GetParameter(0).Trim());

                    case RoomStrategyName.False:
                        return "1=2";

                    case RoomStrategyName.PBSTypeCodeAllChild:
                        return string.Format("(PBSTypeCode = '{0}' or PBSTypeFullID like '{0}-%')", strategy.GetParameter(0).Trim());

                    case RoomStrategyName.OutListCode:
                        return string.Format("exists(select 1 from TempRoomStructure t where t.TempRoomCode = a.RoomCode and t.OutListCode = '{0}')", strategy.GetParameter(0).Trim());
                }
                return text3;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

