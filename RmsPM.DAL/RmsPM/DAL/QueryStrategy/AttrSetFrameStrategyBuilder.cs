namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class AttrSetFrameStrategyBuilder : StandardQueryStringBuilder
    {
        public AttrSetFrameStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("AttrSetFrame", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((AttrSetFrameStrategyName) strategy.Name))
            {
                case AttrSetFrameStrategyName.AttrSetFrameCodeEq:
                    strategy.RelationFieldName = "AttrSetFrameCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetFrameStrategyName.AttrSetCodeEq:
                    strategy.RelationFieldName = "AttrSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetFrameStrategyName.FrameTypeEq:
                    strategy.RelationFieldName = "FrameType";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case AttrSetFrameStrategyName.FrameTypeIn:
                    strategy.RelationFieldName = "FrameType";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case AttrSetFrameStrategyName.AttrTypeEq:
                    strategy.RelationFieldName = "AttrType";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case AttrSetFrameStrategyName.AttrTypeIn:
                    strategy.RelationFieldName = "AttrType";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case AttrSetFrameStrategyName.AttrNameEq:
                    strategy.RelationFieldName = "AttrName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetFrameStrategyName.SortIDEq:
                    strategy.RelationFieldName = "SortID";
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
            AttrSetFrameStrategyName name = (AttrSetFrameStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case AttrSetFrameStrategyName.AttrSetCodeIn:
                        return "";
                }
                return string.Format(" AttrSetCode in ( {0} )", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

