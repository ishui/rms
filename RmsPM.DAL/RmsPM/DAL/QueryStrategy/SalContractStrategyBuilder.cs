namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SalContractStrategyBuilder : StandardQueryStringBuilder
    {
        public SalContractStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SalContract", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SalContractStrategyName) strategy.Name))
            {
                case SalContractStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalContractStrategyName.ContractID:
                    strategy.RelationFieldName = "ContractID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalContractStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalContractStrategyName.ContractDateRange:
                    strategy.RelationFieldName = "ContractDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case SalContractStrategyName.BuildingName:
                    strategy.RelationFieldName = "BuildingName";
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
            SalContractStrategyName name = (SalContractStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                SalContractStrategyName name2 = name;
                if (name2 != SalContractStrategyName.False)
                {
                    if (name2 != SalContractStrategyName.Status)
                    {
                        return text;
                    }
                }
                else
                {
                    return "1=2";
                }
                if (strategy.GetParameter(0) != "")
                {
                    text = string.Format(" dbo.GetSalContractStatus(ContractCode) = {0}", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

