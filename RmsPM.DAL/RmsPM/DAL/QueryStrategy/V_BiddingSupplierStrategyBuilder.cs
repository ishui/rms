namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class V_BiddingSupplierStrategyBuilder : StandardQueryStringBuilder
    {
        public V_BiddingSupplierStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_BiddingSupplier", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((V_BiddingSupplierStrategyName) strategy.Name))
            {
                case V_BiddingSupplierStrategyName.BiddingSupplierCode:
                    strategy.RelationFieldName = "BiddingSupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.BiddingPrejudicationCode:
                    strategy.RelationFieldName = "BiddingPrejudicationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.NominateUser:
                    strategy.RelationFieldName = "NominateUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.NominateDate:
                    strategy.RelationFieldName = "NominateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case V_BiddingSupplierStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.OrderCode:
                    strategy.RelationFieldName = "OrderCode";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case V_BiddingSupplierStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.U8Code:
                    strategy.RelationFieldName = "U8Code";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.SupplierName:
                    strategy.RelationFieldName = "SupplierName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.Abbreviation:
                    strategy.RelationFieldName = "Abbreviation";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.Quality:
                    strategy.RelationFieldName = "Quality";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.CreditLevel:
                    strategy.RelationFieldName = "CreditLevel";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.AreaCode:
                    strategy.RelationFieldName = "AreaCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.ContactDate:
                    strategy.RelationFieldName = "ContactDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case V_BiddingSupplierStrategyName.Mobile:
                    strategy.RelationFieldName = "Mobile";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.EarlyArrearDate:
                    strategy.RelationFieldName = "EarlyArrearDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case V_BiddingSupplierStrategyName.RegisteredCapital:
                    strategy.RelationFieldName = "RegisteredCapital";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.Product:
                    strategy.RelationFieldName = "Product";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.CheckOpinion:
                    strategy.RelationFieldName = "CheckOpinion";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.SupplierTypeCode:
                    strategy.RelationFieldName = "SupplierTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.ArtificialPerson:
                    strategy.RelationFieldName = "ArtificialPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.ContractPerson:
                    strategy.RelationFieldName = "ContractPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.OfficePhone:
                    strategy.RelationFieldName = "OfficePhone";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.RegisteredAddress:
                    strategy.RelationFieldName = "RegisteredAddress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.IndustryType:
                    strategy.RelationFieldName = "IndustryType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.IndustrySort:
                    strategy.RelationFieldName = "IndustrySort";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.SJHG:
                    strategy.RelationFieldName = "SJHG";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.LicenseID:
                    strategy.RelationFieldName = "LicenseID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.TaxID:
                    strategy.RelationFieldName = "TaxID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.TaxNo:
                    strategy.RelationFieldName = "TaxNo";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.WorkAddress:
                    strategy.RelationFieldName = "WorkAddress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.WorkTimeLimit:
                    strategy.RelationFieldName = "WorkTimeLimit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.PostCode:
                    strategy.RelationFieldName = "PostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.EMail:
                    strategy.RelationFieldName = "EMail";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.WebAddress:
                    strategy.RelationFieldName = "WebAddress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BiddingSupplierStrategyName.CreatePerson:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            V_BiddingSupplierStrategyName name = (V_BiddingSupplierStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

