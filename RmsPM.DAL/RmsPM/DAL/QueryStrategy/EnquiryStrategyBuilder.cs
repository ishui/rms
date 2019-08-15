namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class EnquiryStrategyBuilder : StandardQueryStringBuilder
    {
        public EnquiryStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Enquiry", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((EnquiryStrategyName) strategy.Name))
            {
                case EnquiryStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case EnquiryStrategyName.ReplayPerson:
                    strategy.RelationFieldName = "ReplayPerson";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case EnquiryStrategyName.EnquiryDate:
                    strategy.RelationFieldName = "EnquiryDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            EnquiryStrategyName name = (EnquiryStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case EnquiryStrategyName.SupplierName:
                    {
                        string parameter = strategy.GetParameter(0);
                        return string.Format(" suppliercode in  ( select suppliercode  from supplier where SupplierName like '%{0}%')", parameter);
                    }
                    case EnquiryStrategyName.SupplierCode:
                    case EnquiryStrategyName.ReplayPerson:
                    case EnquiryStrategyName.EnquiryDate:
                        return text;

                    case EnquiryStrategyName.EnquiryPerson:
                    {
                        string text4 = strategy.GetParameter(0);
                        return string.Format(" EnquiryPerson in  ( select userid from systemuser where UserName like '%{0}%')", text4);
                    }
                    case EnquiryStrategyName.Status:
                        if (strategy.GetParameter(0) != "0")
                        {
                            return " ReplayDate is not null";
                        }
                        return " ReplayDate is null ";

                    case EnquiryStrategyName.AccessRange:
                    {
                        string text2 = " select purchaseCode from purchase where ";
                        text2 = text2 + AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "Purchase", "PurchaseCode", "ClassType", "CreatePerson");
                        return (" PurchaseCode in (" + text2 + ")");
                    }
                    case EnquiryStrategyName.SingleAccess:
                    {
                        string text6 = strategy.GetParameter(0);
                        return string.Format(" EnquiryPerson = '{0}'", text6);
                    }
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

