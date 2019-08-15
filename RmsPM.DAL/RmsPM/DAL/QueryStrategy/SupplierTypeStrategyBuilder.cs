namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class SupplierTypeStrategyBuilder : StandardQueryStringBuilder
    {
        public SupplierTypeStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SupplierType", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SupplierTypeStrategyName) strategy.Name))
            {
                case SupplierTypeStrategyName.SupplierTypeCode:
                    strategy.RelationFieldName = "SupplierTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierTypeStrategyName.TypeName:
                    strategy.RelationFieldName = "TypeName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SupplierTypeStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SupplierTypeStrategyName.Deep:
                    strategy.RelationFieldName = "Deep";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SupplierTypeStrategyName name = (SupplierTypeStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SupplierTypeStrategyName.ChildNodesIncludeSelf:
                        return "";
                }
                string code = strategy.GetParameter(0);
                string text3 = "";
                EntityData supplierTypeByCode = ProjectDAO.GetSupplierTypeByCode(code);
                if (supplierTypeByCode.HasRecord())
                {
                    text3 = supplierTypeByCode.GetString("FullCode");
                }
                supplierTypeByCode.Dispose();
                return string.Format(" FullCode like '{0}%' ", text3);
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

