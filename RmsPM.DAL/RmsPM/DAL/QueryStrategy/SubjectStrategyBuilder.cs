namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SubjectStrategyBuilder : StandardQueryStringBuilder
    {
        public SubjectStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Subject", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SubjectStrategyName) strategy.Name))
            {
                case SubjectStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SubjectStrategyName.SubjectCode:
                    strategy.RelationFieldName = "SubjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SubjectStrategyName.SubjectName:
                    strategy.RelationFieldName = "SubjectName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SubjectStrategyName.Layer:
                    strategy.RelationFieldName = "Layer";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case SubjectStrategyName.IsDebit:
                    strategy.RelationFieldName = "IsDebit";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case SubjectStrategyName.IsCrebit:
                    strategy.RelationFieldName = "IsCrebit";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case SubjectStrategyName.ParentCode:
                    strategy.RelationFieldName = "SubjectCode";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, strategy.GetParameter(0) + "%");
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SubjectStrategyName name = (SubjectStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

