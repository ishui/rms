namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAPersonContractStrategyBuilder : StandardQueryStringBuilder
    {
        public OAPersonContractStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAPersonContract", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAPersonContractName) strategy.Name))
            {
                case OAPersonContractName.OAPersonContractCode:
                    strategy.RelationFieldName = "OAPersonContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonContractName.OAPersonCode:
                    strategy.RelationFieldName = "OAPersonCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAPersonContractName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
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
            OAPersonContractName name = (OAPersonContractName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

