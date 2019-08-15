namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SalSuplStrategyBuilder : StandardQueryStringBuilder
    {
        public SalSuplStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SalSupl", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SalSuplStrategyName) strategy.Name))
            {
                case SalSuplStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalSuplStrategyName.SuplCode:
                    strategy.RelationFieldName = "SuplCode";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case SalSuplStrategyName.SuplName:
                    strategy.RelationFieldName = "SuplName";
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
            SalSuplStrategyName name = (SalSuplStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SalSuplStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

