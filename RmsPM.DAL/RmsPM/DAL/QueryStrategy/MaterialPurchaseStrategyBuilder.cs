namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class MaterialPurchaseStrategyBuilder : StandardQueryStringBuilder
    {
        public MaterialPurchaseStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Purchase", "SelectAll").SqlString;
            base.QueryKeyString = SqlManager.GetSqlStruct("Purchase", "SelectKey").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((MaterialPurchaseStrategyName) strategy.Name))
            {
                case MaterialPurchaseStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialPurchaseStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialPurchaseStrategyName.CreatePerson:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialPurchaseStrategyName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case MaterialPurchaseStrategyName.AuditPerson:
                    strategy.RelationFieldName = "AuditPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case MaterialPurchaseStrategyName.AuditDate:
                    strategy.RelationFieldName = "AuditDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case MaterialPurchaseStrategyName.ClassType:
                    strategy.RelationFieldName = "ClassType";
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
            MaterialPurchaseStrategyName name = (MaterialPurchaseStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case MaterialPurchaseStrategyName.AccessRange:
                        return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "Purchase", "PurchaseCode", "ClassType", "CreatePerson");

                    case MaterialPurchaseStrategyName.MTName:
                        return "";

                    case MaterialPurchaseStrategyName.Status:
                    {
                        string parameter = strategy.GetParameter(0);
                        parameter = "'" + parameter.Replace(",", "','") + "'";
                        return (" Status in (" + parameter + ")");
                    }
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

