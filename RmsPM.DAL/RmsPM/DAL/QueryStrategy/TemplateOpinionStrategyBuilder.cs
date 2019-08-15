namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TemplateOpinionStrategyBuilder : StandardQueryStringBuilder
    {
        public TemplateOpinionStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TemplateOpinion", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TemplateOpinionStrategyName) strategy.Name))
            {
                case TemplateOpinionStrategyName.TemplateOpinionCode:
                    strategy.RelationFieldName = "TemplateOpinionCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TemplateOpinionStrategyName.Number:
                    strategy.RelationFieldName = "Number";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TemplateOpinionStrategyName.Name:
                    strategy.RelationFieldName = "Name";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TemplateOpinionStrategyName.Center:
                    strategy.RelationFieldName = "Center";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TemplateOpinionStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TemplateOpinionStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TemplateOpinionStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TemplateOpinionStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            TemplateOpinionStrategyName name = (TemplateOpinionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

