namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class DIStrategyBuilder : StandardQueryStringBuilder
    {
        public DIStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("DictionaryItem", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((DIStrategyName) strategy.Name))
            {
                case DIStrategyName.DictionaryItemCode:
                    strategy.RelationFieldName = "DictionaryItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DIStrategyName.DictionaryNameCode:
                    strategy.RelationFieldName = "DictionaryNameCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DIStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
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
            DIStrategyName name = (DIStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case DIStrategyName.DictionaryNameName:
                        return "";
                }
                return string.Format(" DictionaryNameCode in ( select DictionaryNameCode from DictionaryName where Name='{0}' ) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

