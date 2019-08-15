namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SalPayStrategyBuilder : StandardQueryStringBuilder
    {
        public SalPayStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SalPay", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SalPayStrategyName) strategy.Name))
            {
                case SalPayStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalPayStrategyName.ContractID:
                    strategy.RelationFieldName = "ContractID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalPayStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalPayStrategyName.PayDateRange:
                    strategy.RelationFieldName = "PayDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case SalPayStrategyName.CheckDateRange:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case SalPayStrategyName.BuildingName:
                    strategy.RelationFieldName = "BuildingName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalPayStrategyName.ClientName:
                    strategy.RelationFieldName = "ClientName";
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
            SalPayStrategyName name = (SalPayStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SalPayStrategyName.Status:
                        if (strategy.GetParameter(0) != "")
                        {
                            text = string.Format(" dbo.GetSalPayStatus(PayCode) = {0}", strategy.GetParameter(0));
                        }
                        return text;

                    case SalPayStrategyName.NotBalance:
                        return " not exists(select 1 from SalPayPlan p, SalPayRela r where p.PayPlanCode = r.PayPlanCode and r.PayCode = a.PayCode and p.itemname like '价格补差%')";

                    case SalPayStrategyName.OnlyBalance:
                        return " exists(select 1 from SalPayPlan p, SalPayRela r where p.PayPlanCode = r.PayPlanCode and r.PayCode = a.PayCode and p.itemname like '价格补差%')";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

