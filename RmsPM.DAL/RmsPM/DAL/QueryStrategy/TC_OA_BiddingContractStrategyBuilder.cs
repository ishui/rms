namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TC_OA_BiddingContractStrategyBuilder : StandardQueryStringBuilder
    {
        public TC_OA_BiddingContractStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TC_OA_BiddingContract", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TC_OA_BiddingContractStrategyName) strategy.Name))
            {
                case TC_OA_BiddingContractStrategyName.TC_OA_BiddingContractCode:
                    strategy.RelationFieldName = "TC_OA_BiddingContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.WorkName:
                    strategy.RelationFieldName = "WorkName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.ApplyDate:
                    strategy.RelationFieldName = "ApplyDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.CompleteDate:
                    strategy.RelationFieldName = "CompleteDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.CommendSupplier:
                    strategy.RelationFieldName = "CommendSupplier";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case TC_OA_BiddingContractStrategyName.Opinion:
                    strategy.RelationFieldName = "Opinion";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.ContractDate:
                    strategy.RelationFieldName = "ContractDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.ContractType:
                    strategy.RelationFieldName = "ContractType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.ContractID:
                    strategy.RelationFieldName = "ContractID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.ContractContent:
                    strategy.RelationFieldName = "ContractContent";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TC_OA_BiddingContractStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            TC_OA_BiddingContractStrategyName name = (TC_OA_BiddingContractStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

