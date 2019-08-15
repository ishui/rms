namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ResourceStrategyBuilder : StandardQueryStringBuilder
    {
        public ResourceStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Resource", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ResourceStrategyName) strategy.Name))
            {
                case ResourceStrategyName.ResourceCode:
                    strategy.RelationFieldName = "ResourceCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ResourceStrategyName.ClassCode:
                    strategy.RelationFieldName = "ClassCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ResourceStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
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
            ResourceStrategyName name = (ResourceStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                //switch (name)
                //{
                //    case ResourceStrategyName.ClassRelationCode:
                //        return "";
                //}
                string parameter = strategy.GetParameter(0);
                string text3 = strategy.GetParameter(1);
                return string.Format(" classCode='{0}' and RelationCode='{1}' ", parameter, text3);
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

