namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OASalaryStrategyBuilder : StandardQueryStringBuilder
    {
        public OASalaryStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OASalary", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OASalaryStrategyName) strategy.Name))
            {
                case OASalaryStrategyName.OASalaryCode:
                    strategy.RelationFieldName = "OASalaryCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OASalaryStrategyName.usercode:
                    strategy.RelationFieldName = "usercode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OASalaryStrategyName.username:
                    strategy.RelationFieldName = "username";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case OASalaryStrategyName.CURYEAR:
                    strategy.RelationFieldName = "CURYEAR";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OASalaryStrategyName.CURMONTH:
                    strategy.RelationFieldName = "CURMONTH";
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
            OASalaryStrategyName name = (OASalaryStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OASalaryStrategyName.OASalaryCode:
                        return "";
                }
                return string.Format("{0}", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

