namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingMessageStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingMessageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingMessage", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingMessageStrategyName) strategy.Name))
            {
                case BiddingMessageStrategyName.BiddingMessageCode:
                    strategy.RelationFieldName = "BiddingMessageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.ContractNember:
                    strategy.RelationFieldName = "ContractNember";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.ContractName:
                    strategy.RelationFieldName = "ContractName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.ContractType:
                    strategy.RelationFieldName = "ContractType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.Supplier:
                    strategy.RelationFieldName = "Supplier";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.ContractDate:
                    strategy.RelationFieldName = "ContractDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.CreateUser:
                    strategy.RelationFieldName = "CreateUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.BiddingReturnCode:
                    strategy.RelationFieldName = "BiddingReturnCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingMessageStrategyName.BiddingDtlCode:
                    strategy.RelationFieldName = "BiddingDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingMessageStrategyName name = (BiddingMessageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

