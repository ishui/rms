namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class AttrSetItemStrategyBuilder : StandardQueryStringBuilder
    {
        public AttrSetItemStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("AttrSetItem", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((AttrSetItemStrategyName) strategy.Name))
            {
                case AttrSetItemStrategyName.AttrSetItemCodeEq:
                    strategy.RelationFieldName = "AttrSetItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetItemStrategyName.FieldTypeEq:
                    strategy.RelationFieldName = "FieldType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetItemStrategyName.FieldLengthEq:
                    strategy.RelationFieldName = "FieldLength";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case AttrSetItemStrategyName.FieldDecEq:
                    strategy.RelationFieldName = "FieldDec";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case AttrSetItemStrategyName.UnitEq:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetItemStrategyName.CalcFlagEq:
                    strategy.RelationFieldName = "CalcFlag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case AttrSetItemStrategyName.CalcSourceEq:
                    strategy.RelationFieldName = "CalcSource";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetItemStrategyName.SpecIDEq:
                    strategy.RelationFieldName = "SpecID";
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
            AttrSetItemStrategyName name = (AttrSetItemStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case AttrSetItemStrategyName.AttrSetItemCodeIn:
                        return "";
                }
                return string.Format(" AttrSetItemCode in ( {0} )", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

