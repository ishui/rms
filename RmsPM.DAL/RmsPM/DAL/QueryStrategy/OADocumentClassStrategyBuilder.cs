namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OADocumentClassStrategyBuilder : StandardQueryStringBuilder
    {
        public OADocumentClassStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAFileType", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            OADocumentClassStrategyName name = (OADocumentClassStrategyName) strategy.Name;
            if (name == OADocumentClassStrategyName.OAFileTypeCode)
            {
                strategy.RelationFieldName = "OAFileTypeCode";
                strategy.Type = StrategyType.StringEqual;
            }
            else
            {
                strategy.Type = StrategyType.Other;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OADocumentClassStrategyName name = (OADocumentClassStrategyName) strategy.Name;
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

