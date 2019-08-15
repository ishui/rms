namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlow", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PurchaseFlowStrategyName) strategy.Name))
            {
                case PurchaseFlowStrategyName.PurchaseFlowCode:
                    strategy.RelationFieldName = "PurchaseFlowCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowStrategyName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowStrategyName.Purpose:
                    strategy.RelationFieldName = "Purpose";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
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
            PurchaseFlowStrategyName name = (PurchaseFlowStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PurchaseFlowStrategyName.ContractCode:
                        return string.Format("isnull(ContractCode,'')='{0}'", strategy.GetParameter(0));

                    case PurchaseFlowStrategyName.Approve:
                        return "PurchaseFlowCode not in (select distinct PurchaseCode from PurchaseFlowSelSupply where ConfirmUnit='1')";
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

