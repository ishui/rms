namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class DesignDocumentStrategyBuilder : StandardQueryStringBuilder
    {
        public DesignDocumentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("DesignDocument", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((DesignDocumentStrategyName) strategy.Name))
            {
                case DesignDocumentStrategyName.DesignDocumentCode:
                    strategy.RelationFieldName = "DesignDocumentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case DesignDocumentStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.Context:
                    strategy.RelationFieldName = "Context";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.CreateUser:
                    strategy.RelationFieldName = "CreateUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DesignDocumentStrategyName.type:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringLike;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            DesignDocumentStrategyName name = (DesignDocumentStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

