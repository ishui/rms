namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SupplierLinkmanStrategyBuilder : StandardQueryStringBuilder
    {
        public SupplierLinkmanStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SupplierLinkman", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SupplierLinkmanStrategyName) strategy.Name))
            {
                case SupplierLinkmanStrategyName.SupplierLinkmanCode:
                    strategy.RelationFieldName = "SupplierLinkmanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.ContractPerson:
                    strategy.RelationFieldName = "ContractPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.OfficePhone:
                    strategy.RelationFieldName = "OfficePhone";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.PostCode:
                    strategy.RelationFieldName = "PostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.Mobile:
                    strategy.RelationFieldName = "Mobile";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.Fax:
                    strategy.RelationFieldName = "Fax";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.EMail:
                    strategy.RelationFieldName = "EMail";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.AreaName:
                    strategy.RelationFieldName = "AreaName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.ProjectName:
                    strategy.RelationFieldName = "ProjectName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierLinkmanStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SupplierLinkmanStrategyName name = (SupplierLinkmanStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

