namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class DocumentStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public DocumentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Document", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("Document", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((DocumentStrategyName) strategy.Name))
            {
                case DocumentStrategyName.DocumentCode:
                    strategy.RelationFieldName = "DocumentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DocumentStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case DocumentStrategyName.Author:
                    strategy.RelationFieldName = "Author";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case DocumentStrategyName.DocumentID:
                    strategy.RelationFieldName = "DocumentID";
                    strategy.SetParameter(0, strategy.GetParameter(0));
                    strategy.Type = StrategyType.StringLike;
                    break;

                case DocumentStrategyName.MainText:
                    strategy.RelationFieldName = "MainText";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case DocumentStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case DocumentStrategyName.CreateDateRange:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case DocumentStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case DocumentStrategyName.CheckDateRange:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case DocumentStrategyName.CreatePerson:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DocumentStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DocumentStrategyName.CheckPerson:
                    strategy.RelationFieldName = "CheckPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case DocumentStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
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
            DocumentStrategyName name = (DocumentStrategyName) strategy.Name;
            string text = "";
            string parameter = "";
            string text3 = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            switch (name)
            {
                case DocumentStrategyName.DocumentTypeCode:
                    return string.Format(" exists ( select * from DocumentConfig where DocumentTypeCode='{0}' and DocumentCode = Document.DocumentCode ) ", strategy.GetParameter(0));

                case DocumentStrategyName.DocumentTypeCodeFull:
                    return string.Format(" exists ( select * from DocumentConfig c, DocumentType t where c.DocumentTypeCode = t.DocumentTypeCode and t.FullCode like (select FullCode + '%' from DocumentType where DocumentTypeCode = '{0}') and c.DocumentCode = Document.DocumentCode ) ", strategy.GetParameter(0));

                case DocumentStrategyName.Code:
                    switch (strategy.GetParameterCount())
                    {
                        case 1:
                            return string.Format(" exists ( select * from DocumentConfig where Code='{0}' and DocumentCode = Document.DocumentCode ) ", strategy.GetParameter(0));

                        case 2:
                            return string.Format(" exists ( select * from DocumentConfig where DocumentTypeCode='{0}' and Code='{1}' and DocumentCode = Document.DocumentCode ) ", strategy.GetParameter(0), strategy.GetParameter(1));
                    }
                    return text;

                case DocumentStrategyName.RelationKey:
                    parameter = strategy.GetParameter(0);
                    text3 = strategy.GetParameter(1);
                    switch (parameter)
                    {
                        case "000001":
                            return string.Format(" exists ( select * from DocumentConfig where DocumentTypeCode='{0}' and Code in (select ContractCode from Contract where ContractID = '{1}' or ContractName like '%{1}%') and DocumentCode = Document.DocumentCode ) ", parameter, text3);

                        case "000006":
                            return string.Format(" exists ( select * from DocumentConfig where DocumentTypeCode='{0}' and Code in (select DocumentCode from Document where DocumentCode = '{1}' or Title like '%{1}%') and DocumentCode = Document.DocumentCode ) ", parameter, text3);
                    }
                    return string.Format(" exists ( select * from DocumentConfig where DocumentTypeCode='{0}' and Code='{1}' and DocumentCode = Document.DocumentCode ) ", parameter, text3);

                case DocumentStrategyName.GroupCode:
                    return text;

                case DocumentStrategyName.GroupFullID:
                    return string.Format(" (GroupFullID = '{0}' or GroupFullID like '{0}-%')", strategy.GetParameter(0));

                case DocumentStrategyName.GroupFullIDs:
                {
                    text = "";
                    text = text + " (";
                    string[] textArray = strategy.GetParameter(0).Split(new char[] { ","[0] });
                    int num = 0;
                    foreach (string text4 in textArray)
                    {
                        if (text4 != "")
                        {
                            num++;
                            if (num > 1)
                            {
                                text = text + " or";
                            }
                            text = text + string.Format(" (GroupFullID = '{0}' or GroupFullID like '{0}-%')", text4);
                        }
                    }
                    return (text + ")");
                }
                case DocumentStrategyName.AccessRange:
                    return ("(isnull(GroupCode, '') = '' or " + AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("Document"), SystemClassDescription.GetItemKeyColumnName("Document"), SystemClassDescription.GetItemTypeColumnName("Document"), SystemClassDescription.GetItemCreateUserColumnName("Document")) + ")");
            }
            return text;
        }
    }
}

