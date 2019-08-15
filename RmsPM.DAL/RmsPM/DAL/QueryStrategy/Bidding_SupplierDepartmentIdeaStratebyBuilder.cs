namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class Bidding_SupplierDepartmentIdeaStratebyBuilder : StandardQueryStringBuilder
    {
        public Bidding_SupplierDepartmentIdeaStratebyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Bidding_SupplierDepartmentIdea", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((Bidding_SupplierDepartmentIdeaStrategyName) strategy.Name))
            {
                case Bidding_SupplierDepartmentIdeaStrategyName.DepartmentIdearID:
                    strategy.RelationFieldName = "DepartmentIdearID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.BiddingSupplierCode:
                    strategy.RelationFieldName = "BiddingSupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Depart_Build:
                    strategy.RelationFieldName = "Depart_Build";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Depart_Project:
                    strategy.RelationFieldName = "Depart_Project";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Depart_Agreement:
                    strategy.RelationFieldName = "Depart_Agreement";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Md_Item:
                    strategy.RelationFieldName = "Md_Item";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Md_Project:
                    strategy.RelationFieldName = "Md_Project";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Md_Agreement:
                    strategy.RelationFieldName = "Md_Agreement";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Director_Project:
                    strategy.RelationFieldName = "Director_Project";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Director_Agreement:
                    strategy.RelationFieldName = "Director_Agreement";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.Director_Finance:
                    strategy.RelationFieldName = "Director_Finance";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark:
                    strategy.RelationFieldName = "DepartmentRemark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark1:
                    strategy.RelationFieldName = "DepartmentRemark1";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark2:
                    strategy.RelationFieldName = "DepartmentRemark2";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark3:
                    strategy.RelationFieldName = "DepartmentRemark3";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Bidding_SupplierDepartmentIdeaStrategyName.BiddingPrejudicationCode:
                    strategy.RelationFieldName = "BiddingPrejudicationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            Bidding_SupplierDepartmentIdeaStrategyName name = (Bidding_SupplierDepartmentIdeaStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

