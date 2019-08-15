namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SupplierSurveyOpinionStrategyBuilder : StandardQueryStringBuilder
    {
        public SupplierSurveyOpinionStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SupplierSurveyOpinion", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SupplierSurveyOpinionStrategyName) strategy.Name))
            {
                case SupplierSurveyOpinionStrategyName.SupplierSurveyOpinionCode:
                    strategy.RelationFieldName = "SupplierSurveyOpinionCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.WorkName:
                    strategy.RelationFieldName = "WorkName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.ZYName:
                    strategy.RelationFieldName = "ZYName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.SurveyDate:
                    strategy.RelationFieldName = "SurveyDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.Grade:
                    strategy.RelationFieldName = "Grade";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.AdviceGrade:
                    strategy.RelationFieldName = "AdviceGrade";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSurveyOpinionStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SupplierSurveyOpinionStrategyName name = (SupplierSurveyOpinionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

