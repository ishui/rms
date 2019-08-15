namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OADocumentBorrowStrategyBuilder : StandardQueryStringBuilder
    {
        public OADocumentBorrowStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAFileBorrow", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OADocumentBorrowStrategyName) strategy.Name))
            {
                case OADocumentBorrowStrategyName.OAFileBorrowCode:
                    strategy.RelationFieldName = "OAFileBorrowCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentBorrowStrategyName.OAFileWritCode:
                    strategy.RelationFieldName = "OAFileWritCode";
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
            OADocumentBorrowStrategyName name = (OADocumentBorrowStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

