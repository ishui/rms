namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ChamberStrategyBuilder : StandardQueryStringBuilder
    {
        public ChamberStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Chamber", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ChamberStrategyName) strategy.Name))
            {
                case ChamberStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ChamberStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ChamberStrategyName.ChamberCode:
                    strategy.RelationFieldName = "ChamberCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ChamberStrategyName.ChamberName:
                    strategy.RelationFieldName = "ChamberName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ChamberStrategyName.ChamberNameLike:
                    strategy.RelationFieldName = "ChamberName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ChamberStrategyName name = (ChamberStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ChamberStrategyName.ChamberCodeNot:
                        return "";
                }
                return string.Format("ChamberCode <> '{0}'", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

