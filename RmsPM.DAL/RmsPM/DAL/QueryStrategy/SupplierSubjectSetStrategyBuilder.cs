namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SupplierSubjectSetStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public SupplierSubjectSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SupplierSubjectSet", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("SupplierSubjectSet", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SupplierSubjectSetStrategyName) strategy.Name))
            {
                case SupplierSubjectSetStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSubjectSetStrategyName.SupplierName:
                    strategy.RelationFieldName = "SupplierName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierSubjectSetStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSubjectSetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierSubjectSetStrategyName.U8Code:
                    strategy.RelationFieldName = "U8Code";
                    strategy.Type = StrategyType.StringLike;
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
            SupplierSubjectSetStrategyName name = (SupplierSubjectSetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SupplierSubjectSetStrategyName.ProjectNotNull:
                        return "";
                }
                return "ProjectCode <> ''";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

