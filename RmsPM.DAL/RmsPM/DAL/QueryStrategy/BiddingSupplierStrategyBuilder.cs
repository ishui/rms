namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingSupplierStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingSupplierStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingSupplier", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingSupplierStrategyName) strategy.Name))
            {
                case BiddingSupplierStrategyName.BiddingSupplierCode:
                    strategy.RelationFieldName = "BiddingSupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.BiddingPrejudicationCode:
                    strategy.RelationFieldName = "BiddingPrejudicationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.NominateUser:
                    strategy.RelationFieldName = "NominateUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.NominateDate:
                    strategy.RelationFieldName = "NominateDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.OrderCode:
                    strategy.RelationFieldName = "OrderCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingSupplierStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingSupplierStrategyName name = (BiddingSupplierStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

