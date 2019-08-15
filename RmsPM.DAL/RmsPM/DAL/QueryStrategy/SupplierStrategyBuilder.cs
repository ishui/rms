namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SupplierStrategyBuilder : StandardQueryStringBuilder
    {
        public SupplierStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Supplier", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SupplierStrategyName) strategy.Name))
            {
                case SupplierStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.SupplierName:
                    strategy.RelationFieldName = "SupplierName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.Abbreviation:
                    strategy.RelationFieldName = "Abbreviation";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierStrategyName.SupplierTypeCode:
                    strategy.RelationFieldName = "SupplierTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierStrategyName.AreaCode:
                    strategy.RelationFieldName = "AreaCode";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.Quality:
                    strategy.RelationFieldName = "Quality";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.RegisteredAddress:
                    strategy.RelationFieldName = "RegisteredAddress";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.WorkAddress:
                    strategy.RelationFieldName = "WorkAddress";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.ContractPerson:
                    strategy.RelationFieldName = "ContractPerson";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.IndustryType:
                    strategy.RelationFieldName = "IndustryType";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.CheckOpinion:
                    strategy.RelationFieldName = "CheckOpinion";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.Achievement:
                    strategy.RelationFieldName = "Achievement";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.SaleType:
                    strategy.RelationFieldName = "saleType";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.CharacterType:
                    strategy.RelationFieldName = "characterType";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.IsCCC:
                    strategy.RelationFieldName = "IsCCC";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.IsISO:
                    strategy.RelationFieldName = "IsISO";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.CreditLevel:
                    strategy.RelationFieldName = "CreditLevel";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.QualityGrade:
                    strategy.RelationFieldName = "QualityGrade";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.SupplierGrade:
                    strategy.RelationFieldName = "SupplierGrade";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SupplierStrategyName name = (SupplierStrategyName) strategy.Name;
            string text = "";
            string text2 = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            switch (name)
            {
                case SupplierStrategyName.SupplierTypeCodeEx:
                {
                    string systemGroupCode = strategy.GetParameter(0);
                    switch (strategy.GetParameter(1))
                    {
                        case "0":
                            return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("Supplier"));

                        case "1":
                            return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("Supplier"));

                        case "2":
                            return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("Supplier"));
                    }
                    return text;
                }
                case SupplierStrategyName.AccessRange:
                    return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("Supplier"), SystemClassDescription.GetItemKeyColumnName("Supplier"), SystemClassDescription.GetItemTypeColumnName("Supplier"), SystemClassDescription.GetItemCreateUserColumnName("Supplier"));

                case SupplierStrategyName.CheckName:
                {
                    string parameter = strategy.GetParameter(0);
                    string text4 = strategy.GetParameter(1);
                    string text5 = strategy.GetParameter(2);
                    if (parameter == "")
                    {
                        if (text5 != "")
                        {
                            return string.Format(" ( SupplierName='{0}'  or  Abbreviation = '{1}' ) ", text4, text5, parameter);
                        }
                        return string.Format(" ( SupplierName='{0}'   ) ", text4, text5, parameter);
                    }
                    if (text5 == "")
                    {
                        return string.Format(" ( SupplierCode<>'{2}' )  and ( SupplierName='{0}'   ) ", text4, text5, parameter);
                    }
                    return string.Format(" ( SupplierCode<>'{2}' )  and ( SupplierName='{0}'  or  Abbreviation = '{1}' ) ", text4, text5, parameter);
                }
                case SupplierStrategyName.False:
                    return "1=2";

                case SupplierStrategyName.IsExistsContract:
                    if (strategy.GetParameter(0) != "1")
                    {
                        return " not exists (select * from Contract where Contract.SupplierCode = Supplier.SupplierCode)";
                    }
                    return " exists (select * from Contract where Contract.SupplierCode = Supplier.SupplierCode)";

                case SupplierStrategyName.Grade:
                    text2 = strategy.GetParameter(0);
                    return string.Format("exists (select * from SupplierSurveyOpinion o where o.SupplierCode = Supplier.SupplierCode and o.SupplierSurveyOpinionCode = (select min(SupplierSurveyOpinionCode) from SupplierSurveyOpinion o2 where o2.SupplierCode = o.SupplierCode) and o.Grade like '{0}')", text2);
            }
            return text;
        }
    }
}

