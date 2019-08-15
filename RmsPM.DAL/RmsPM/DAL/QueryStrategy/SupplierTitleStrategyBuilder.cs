namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SupplierTitleStrategyBuilder : StandardQueryStringBuilder
    {
        public SupplierTitleStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SupplierTitle", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SupplierTitleStrategyName) strategy.Name))
            {
                case SupplierTitleStrategyName.SupplierTitleCode:
                    strategy.RelationFieldName = "SupplierTitleCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierTitleStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierTitleStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierTitleStrategyName.BankAccount:
                    strategy.RelationFieldName = "BankAccount";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierTitleStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SupplierTitleStrategyName name = (SupplierTitleStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

