namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class MaterialCostStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("MaterialCost", "SelectView").SqlString;

        public MaterialCostStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("MaterialCost", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((MaterialCostStrategyName) strategy.Name))
            {
                case MaterialCostStrategyName.MaterialCostCode:
                    strategy.RelationFieldName = "MaterialCostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialCostStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case MaterialCostStrategyName.PriceRange:
                    strategy.RelationFieldName = "Price";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case MaterialCostStrategyName.Project:
                    strategy.RelationFieldName = "Project";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case MaterialCostStrategyName.BiddingDateRange:
                    strategy.RelationFieldName = "BiddingDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case MaterialCostStrategyName.Description:
                    strategy.RelationFieldName = "Description";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case MaterialCostStrategyName.DescriptionEn:
                    strategy.RelationFieldName = "DescriptionEn";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case MaterialCostStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialCostStrategyName.AreaCode:
                    strategy.RelationFieldName = "AreaCode";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case MaterialCostStrategyName.CreatePerson:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialCostStrategyName.CreateDateRange:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case MaterialCostStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialCostStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case MaterialCostStrategyName.Category:
                    strategy.RelationFieldName = "Category";
                    strategy.Type = StrategyType.StringLike;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public static string BuildAccessRangeString(string UserCode, string StationCodes)
        {
            return AccessRanggeQuery.BuildAccessRangeString("141101", UserCode, StationCodes, SystemClassDescription.GetItemTableName("MaterialCost"), SystemClassDescription.GetItemKeyColumnName("MaterialCost"), SystemClassDescription.GetItemTypeColumnName("MaterialCost"), SystemClassDescription.GetItemCreateUserColumnName("MaterialCost"));
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            MaterialCostStrategyName name = (MaterialCostStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case MaterialCostStrategyName.GroupCodeEx:
                    {
                        string systemGroupCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("MaterialCost"));

                            case "1":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("MaterialCost"));

                            case "2":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("MaterialCost"));
                        }
                        return text;
                    }
                    case MaterialCostStrategyName.AccessRange:
                        return BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1));

                    case MaterialCostStrategyName.False:
                        return "1=2";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

