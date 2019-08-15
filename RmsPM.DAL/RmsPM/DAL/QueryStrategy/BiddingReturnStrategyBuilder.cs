namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingReturnStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingReturnStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingReturn", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingReturnStrategyName) strategy.Name))
            {
                case BiddingReturnStrategyName.BiddingReturnCode:
                    strategy.RelationFieldName = "BiddingReturnCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.BiddingDtlCode:
                    strategy.RelationFieldName = "BiddingDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.BiddingEmitCode:
                    strategy.RelationFieldName = "BiddingEmitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.Money:
                    strategy.RelationFieldName = "Money";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.Design:
                    strategy.RelationFieldName = "Design";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.Project:
                    strategy.RelationFieldName = "Project";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.Consultant:
                    strategy.RelationFieldName = "Consultant";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.OrderCode:
                    strategy.RelationFieldName = "OrderCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.ReturnDate:
                    strategy.RelationFieldName = "ReturnDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnStrategyName.Abnegate:
                    strategy.RelationFieldName = "Abnegate";
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
            BiddingReturnStrategyName name = (BiddingReturnStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                //switch (name)
                //{
                //    case BiddingReturnStrategyName.BiddingCode:
                //        return "";
                //}
                return string.Format(" BiddingEmitCode in (select BiddingEmitCode from BiddingEmit where BiddingCode='{0}')", strategy.GetParameter(0).Trim());
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

