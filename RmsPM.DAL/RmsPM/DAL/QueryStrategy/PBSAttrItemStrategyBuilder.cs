namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PBSAttrItemStrategyBuilder : StandardQueryStringBuilder
    {
        public PBSAttrItemStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PBSAttrItem", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PBSAttrItemStrategyName) strategy.Name))
            {
                case PBSAttrItemStrategyName.PBSAttrItemCodeEq:
                    strategy.RelationFieldName = "PBSAttrItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrItemStrategyName.AttrSetItemCodeEq:
                    strategy.RelationFieldName = "AttrSetItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrItemStrategyName.MasterTypeEq:
                    strategy.RelationFieldName = "MasterType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrItemStrategyName.MasterCodeEq:
                    strategy.RelationFieldName = "MasterCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrItemStrategyName.StageEq:
                    strategy.RelationFieldName = "Stage";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrItemStrategyName.StringValueEq:
                    strategy.RelationFieldName = "StringValue";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PBSAttrItemStrategyName.DateValueEq:
                    strategy.RelationFieldName = "DateValue";
                    strategy.Type = StrategyType.DateTimeEqual;
                    break;

                case PBSAttrItemStrategyName.IntValueEq:
                    strategy.RelationFieldName = "IntValue";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PBSAttrItemStrategyName name = (PBSAttrItemStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PBSAttrItemStrategyName.PBSAttrItemCodeIn:
                        return string.Format(" PBSAttrItemCode in ( {0} )", strategy.GetParameter(0));

                    case PBSAttrItemStrategyName.AttrSetItemCodeEq:
                    case PBSAttrItemStrategyName.MasterTypeEq:
                    case PBSAttrItemStrategyName.MasterCodeEq:
                    case PBSAttrItemStrategyName.StageEq:
                        return text;

                    case PBSAttrItemStrategyName.AttrSetItemCodeIn:
                        return string.Format(" AttrSetItemCode in ( {0} )", strategy.GetParameter(0));

                    case PBSAttrItemStrategyName.MasterTypeIn:
                        return string.Format(" MasterType in ( {0} )", strategy.GetParameter(0));

                    case PBSAttrItemStrategyName.MasterCodeIn:
                        return string.Format(" MasterCode in ( {0} )", strategy.GetParameter(0));

                    case PBSAttrItemStrategyName.StageIn:
                        return string.Format(" Stage in ( {0} )", strategy.GetParameter(0));

                    case PBSAttrItemStrategyName.FloatValueEq:
                        return string.Format(" FloatValue = '{0}'", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

