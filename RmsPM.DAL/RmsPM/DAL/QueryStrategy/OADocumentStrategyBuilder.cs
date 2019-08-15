namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OADocumentStrategyBuilder : StandardQueryStringBuilder
    {
        public OADocumentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAFileWrit", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OADocumentStrategyName) strategy.Name))
            {
                case OADocumentStrategyName.fileNo:
                    strategy.RelationFieldName = "fileNo";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentStrategyName.abstract1:
                    strategy.RelationFieldName = "abstract1";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentStrategyName.author:
                    strategy.RelationFieldName = "author";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentStrategyName.file_date:
                    strategy.RelationFieldName = "file_date";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case OADocumentStrategyName.TomeId:
                    strategy.RelationFieldName = "TomeId";
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
            OADocumentStrategyName name = (OADocumentStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case OADocumentStrategyName.OAFileWritCode:
                        return "";
                }
                return string.Format("OAFileWritCode in ( {0} )", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

