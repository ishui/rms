namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OADocumentViewStrategyBuilder : StandardQueryStringBuilder
    {
        public OADocumentViewStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_OADocumentQuery", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OADocumentViewStrategyName) strategy.Name))
            {
                case OADocumentViewStrategyName.fileNo:
                    strategy.RelationFieldName = "fileNo";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentViewStrategyName.abstract1:
                    strategy.RelationFieldName = "abstract1";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentViewStrategyName.author:
                    strategy.RelationFieldName = "author";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentViewStrategyName.file_date:
                    strategy.RelationFieldName = "file_date";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case OADocumentViewStrategyName.wslb:
                    strategy.RelationFieldName = "wslb";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentViewStrategyName.filename:
                    strategy.RelationFieldName = "filename";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentViewStrategyName.DictionaryItemCode:
                    strategy.RelationFieldName = "DictionaryItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentViewStrategyName.borrow_person:
                    strategy.RelationFieldName = "borrow_person";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OADocumentViewStrategyName name = (OADocumentViewStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

