namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class AttrSetStrategyBuilder : StandardQueryStringBuilder
    {
        public AttrSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("AttrSet", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((AttrSetStrategyName) strategy.Name))
            {
                case AttrSetStrategyName.AttrSetCodeEq:
                    strategy.RelationFieldName = "AttrSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetStrategyName.SetNameEq:
                    strategy.RelationFieldName = "SetName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case AttrSetStrategyName.SetTypeEq:
                    strategy.RelationFieldName = "SetType";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case AttrSetStrategyName.SetTypeIn:
                    strategy.RelationFieldName = "SetType";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case AttrSetStrategyName.SetStyleLike:
                    strategy.RelationFieldName = "SetStyle";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case AttrSetStrategyName.DescriptionLike:
                    strategy.RelationFieldName = "Description";
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
            AttrSetStrategyName name = (AttrSetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case AttrSetStrategyName.AttrSetCodeIn:
                        return "";
                }
                return string.Format(" AttrSetCode in ( {0} )", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

