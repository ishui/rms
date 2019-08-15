namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TempRoomOutStrategyBuilder : StandardQueryStringBuilder
    {
        public TempRoomOutStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TempRoomOut", "SelectAllIncludeDtl").SqlString;
            base.IsNeedWhere = true;
        }

        public TempRoomOutStrategyBuilder(string SqlSelect)
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TempRoomOut", SqlSelect).SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TempRoomOutStrategyName) strategy.Name))
            {
                case TempRoomOutStrategyName.OutListCode:
                    strategy.RelationFieldName = "OutListCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TempRoomOutStrategyName.OutListName:
                    strategy.RelationFieldName = "OutListName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TempRoomOutStrategyName.CurYear:
                    strategy.RelationFieldName = "CurYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case TempRoomOutStrategyName.SumNo:
                    strategy.RelationFieldName = "SumNo";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case TempRoomOutStrategyName.CodeName:
                    strategy.RelationFieldName = "CodeName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TempRoomOutStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TempRoomOutStrategyName.OutAspect:
                    strategy.RelationFieldName = "OutAspect";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TempRoomOutStrategyName.Out_State:
                    strategy.RelationFieldName = "Out_State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TempRoomOutStrategyName.OutDateRange:
                    strategy.RelationFieldName = "Out_Date";
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
            TempRoomOutStrategyName name = (TempRoomOutStrategyName) strategy.Name;
            string text2 = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case TempRoomOutStrategyName.CheckState:
                        return string.Format("isnull(CheckState, 0) = '{0}'", strategy.GetParameter(0));

                    case TempRoomOutStrategyName.InBuildingCode:
                        text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text2 != "")
                        {
                            text3 = string.Format("exists(select * from TempRoomStructure s where s.TempBuildingCode in ({0}) and s.OutListCode = a.OutListCode)", text2);
                        }
                        return text3;

                    case TempRoomOutStrategyName.InBuildingName:
                        text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text2 != "")
                        {
                            text3 = string.Format("exists(select * from Building b, TempRoomStructure s where b.BuildingName in ({0}) and s.OutListCode = a.OutListCode and b.BuildingCode = s.TempBuildingCode)", text2);
                        }
                        return text3;

                    case TempRoomOutStrategyName.InChamberName:
                        text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text2 != "")
                        {
                            text3 = string.Format("exists(select * from Chamber c, TempRoomStructure s where c.ChamberName in ({0}) and s.OutListCode = a.OutListCode and c.ChamberCode = s.TempChamberCode)", text2);
                        }
                        return text3;

                    case TempRoomOutStrategyName.InRoomName:
                        text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text2 != "")
                        {
                            text3 = string.Format("exists(select * from Room r, TempRoomStructure s where r.RoomName in ({0}) and s.OutListCode = a.OutListCode and r.RoomCode = s.TempRoomCode)", text2);
                        }
                        return text3;

                    case TempRoomOutStrategyName.InRoomCode:
                        text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text2 != "")
                        {
                            text3 = string.Format("exists(select * from TempRoomStructure s where s.TempRoomCode in ({0}) and s.OutListCode = a.OutListCode)", text2);
                        }
                        return text3;
                }
                return text3;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

