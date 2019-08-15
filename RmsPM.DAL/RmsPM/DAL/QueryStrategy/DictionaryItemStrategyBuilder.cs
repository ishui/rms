namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class DictionaryItemStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public DictionaryItemStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("DictionaryItem", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("DictionaryItem", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((DictionaryItemStrategyName) strategy.Name))
            {
                case DictionaryItemStrategyName.DictionaryItemCode:
                    strategy.RelationFieldName = "DictionaryItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DictionaryItemStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DictionaryItemStrategyName.DictionaryNameCode:
                    strategy.RelationFieldName = "DictionaryNameCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DictionaryItemStrategyName.DictionaryName:
                    strategy.RelationFieldName = "DictionaryName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DictionaryItemStrategyName.NameLike:
                    strategy.RelationFieldName = "Name";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case DictionaryItemStrategyName.Name:
                    strategy.RelationFieldName = "Name";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            DictionaryItemStrategyName name = (DictionaryItemStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case DictionaryItemStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

