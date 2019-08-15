namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Bidding", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingStrategyName) strategy.Name))
            {
                case BiddingStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case BiddingStrategyName.Content:
                    strategy.RelationFieldName = "Content";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.Accessory:
                    strategy.RelationFieldName = "Accessory";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.ArrangedDate:
                    strategy.RelationFieldName = "ArrangedDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.PrejudicationDate:
                    strategy.RelationFieldName = "PrejudicationDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case BiddingStrategyName.EmitDate:
                    strategy.RelationFieldName = "EmitDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case BiddingStrategyName.ReturnDate:
                    strategy.RelationFieldName = "ReturnDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case BiddingStrategyName.ConfirmDate:
                    strategy.RelationFieldName = "ConfirmDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case BiddingStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case BiddingStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case BiddingStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.Money:
                    strategy.RelationFieldName = "Money";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.ObligateMoney:
                    strategy.RelationFieldName = "ObligateMoney";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.BiddingRemark1:
                    strategy.RelationFieldName = "BiddingRemark1";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.BiddingRemark2:
                    strategy.RelationFieldName = "BiddingRemark2";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.ArrangedDateEx:
                    strategy.RelationFieldName = "ArrangedDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case BiddingStrategyName.ConfirmDateEx:
                    strategy.RelationFieldName = "ConfirmDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case BiddingStrategyName.BiddingType:
                    strategy.RelationFieldName = "BiddingType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.BiddingAddress:
                    strategy.RelationFieldName = "BiddingAddress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingStrategyName name = (BiddingStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case BiddingStrategyName.CanBeginContract:
                        return "";
                }
                return "State='41' or State='42'";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

