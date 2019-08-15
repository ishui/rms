namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PBSUnitStrategyBuilder : StandardQueryStringBuilder
    {
        public PBSUnitStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PBSUnit", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public PBSUnitStrategyBuilder(string ViewName)
        {
            base.QueryMainString = SqlManager.GetSqlStruct(ViewName, "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PBSUnitStrategyName) strategy.Name))
            {
                case PBSUnitStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSUnitStrategyName.PBSUnitCode:
                    strategy.RelationFieldName = "PBSUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSUnitStrategyName.PBSUnitName:
                    strategy.RelationFieldName = "PBSUnitName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSUnitStrategyName.PBSUnitNameLike:
                    strategy.RelationFieldName = "PBSUnitName";
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
            PBSUnitStrategyName name = (PBSUnitStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PBSUnitStrategyName.PBSUnitCodeNot:
                        return "";
                }
                return string.Format("PBSUnitCode <> '{0}'", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

