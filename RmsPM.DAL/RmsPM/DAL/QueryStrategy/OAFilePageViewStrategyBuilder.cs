namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OAFilePageViewStrategyBuilder : StandardQueryStringBuilder
    {
        public OAFilePageViewStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OAFilePage", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OAFilePageViewStrategyName) strategy.Name))
            {
                case OAFilePageViewStrategyName.OAFilePageCode:
                    strategy.RelationFieldName = "OAFilePageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAFilePageViewStrategyName.Keeping_Date:
                    strategy.RelationFieldName = "Keeping_Date";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAFilePageViewStrategyName.Page_Title:
                    strategy.RelationFieldName = "Page_Title";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAFilePageViewStrategyName.SerialNumber:
                    strategy.RelationFieldName = "SerialNumber";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAFilePageViewStrategyName.FileTypeCode:
                    strategy.RelationFieldName = "FileTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAFilePageViewStrategyName.KeepUnit:
                    strategy.RelationFieldName = "KeepUnit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OAFilePageViewStrategyName.keepDate:
                    strategy.RelationFieldName = "keepDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OAFilePageViewStrategyName name = (OAFilePageViewStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

