namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SupplierMaterialStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("SupplierMaterial", "SelectView").SqlString;

        public SupplierMaterialStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SupplierMaterial", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SupplierMaterialStrategyName) strategy.Name))
            {
                case SupplierMaterialStrategyName.SupplierMaterialCode:
                    strategy.RelationFieldName = "SupplierMaterialCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierMaterialStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierMaterialStrategyName.Brand:
                    strategy.RelationFieldName = "Brand";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.Model:
                    strategy.RelationFieldName = "Model";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.Spec:
                    strategy.RelationFieldName = "Spec";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.Nation:
                    strategy.RelationFieldName = "Nation";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.AreaCode:
                    strategy.RelationFieldName = "AreaCode";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.SampleID:
                    strategy.RelationFieldName = "SampleID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.PriceRange:
                    strategy.RelationFieldName = "Price";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case SupplierMaterialStrategyName.Description:
                    strategy.RelationFieldName = "Description";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierMaterialStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierMaterialStrategyName.CreatePerson:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierMaterialStrategyName.CreateDateRange:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case SupplierMaterialStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierMaterialStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case SupplierMaterialStrategyName.SupplierName:
                    strategy.RelationFieldName = "SupplierName";
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
            return AccessRanggeQuery.BuildAccessRangeString("141301", UserCode, StationCodes, SystemClassDescription.GetItemTableName("SupplierMaterial"), SystemClassDescription.GetItemKeyColumnName("SupplierMaterial"), SystemClassDescription.GetItemTypeColumnName("SupplierMaterial"), SystemClassDescription.GetItemCreateUserColumnName("SupplierMaterial"));
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SupplierMaterialStrategyName name = (SupplierMaterialStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SupplierMaterialStrategyName.GroupCodeEx:
                    {
                        string systemGroupCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("SupplierMaterial"));

                            case "1":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("SupplierMaterial"));

                            case "2":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("SupplierMaterial"));
                        }
                        return text;
                    }
                    case SupplierMaterialStrategyName.AccessRange:
                        return BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1));

                    case SupplierMaterialStrategyName.False:
                        return "1=2";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

