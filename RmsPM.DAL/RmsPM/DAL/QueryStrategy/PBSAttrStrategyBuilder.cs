namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PBSAttrStrategyBuilder : StandardQueryStringBuilder
    {
        public PBSAttrStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PBSAttr", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PBSAttrStrategyName) strategy.Name))
            {
                case PBSAttrStrategyName.PBSAttrCodeEq:
                    strategy.RelationFieldName = "PBSAttrCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrStrategyName.AttrSetCodeEq:
                    strategy.RelationFieldName = "AttrSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrStrategyName.MasterTypeEq:
                    strategy.RelationFieldName = "MasterType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrStrategyName.MasterCodeEq:
                    strategy.RelationFieldName = "MasterCode";
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
            PBSAttrStrategyName name = (PBSAttrStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PBSAttrStrategyName.AttrSetCodeIn:
                        return string.Format(" AttrSetCode in ( {0} )", strategy.GetParameter(0));

                    case PBSAttrStrategyName.MasterTypeEq:
                    case PBSAttrStrategyName.MasterCodeEq:
                        return text;

                    case PBSAttrStrategyName.MasterTypeIn:
                        return string.Format(" MasterType in ( {0} )", strategy.GetParameter(0));

                    case PBSAttrStrategyName.MasterCodeIn:
                        return string.Format(" MasterCode in ( {0} )", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

