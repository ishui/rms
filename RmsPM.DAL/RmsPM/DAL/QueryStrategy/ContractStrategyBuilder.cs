namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ContractStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumString = "";
        public string QueryViewPayMoneyString = "";
        public string QueryViewString = "";

        public ContractStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Contract", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("Contract", "SelectSum").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("Contract", "SelectView").SqlString;
            this.QueryViewPayMoneyString = SqlManager.GetSqlStruct("Contract", "SelectViewPayMoney").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ContractStrategyName) strategy.Name))
            {
                case ContractStrategyName.ProjectCode:
                    strategy.RelationFieldName = "Contract.ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.ContractID:
                    strategy.RelationFieldName = "ContractID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case ContractStrategyName.ContractName:
                    strategy.RelationFieldName = "ContractName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case ContractStrategyName.ContractModelCode:
                    strategy.RelationFieldName = "ContractModelCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case ContractStrategyName.TotalMoney:
                    strategy.RelationFieldName = "TotalMoney";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case ContractStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.ContractPerson:
                    strategy.RelationFieldName = "ContractPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.ContractDate:
                    strategy.RelationFieldName = "ContractDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case ContractStrategyName.CheckDate:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case ContractStrategyName.ContractLabel:
                    strategy.RelationFieldName = "ContractLabel";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractStrategyName.ChangeCount:
                    strategy.RelationFieldName = "ChangeCount";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                case ContractStrategyName.ChangeStatus:
                    strategy.RelationFieldName = "ChangeStatus";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case ContractStrategyName.AdIssueDate:
                    strategy.RelationFieldName = "AdIssueDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + base.BuildStrategysString());
        }

        public string BuildQueryViewPayMoneyString()
        {
            return (this.QueryViewPayMoneyString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ContractStrategyName name = (ContractStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ContractStrategyName.TypeEx:
                    {
                        string systemGroupCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("Contract"));

                            case "1":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("Contract"));

                            case "2":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("Contract"));
                        }
                        return text;
                    }
                    case ContractStrategyName.Status:
                    case ContractStrategyName.TotalMoney:
                    case ContractStrategyName.SupplierCode:
                    case ContractStrategyName.ContractPerson:
                    case ContractStrategyName.ContractDate:
                    case ContractStrategyName.CheckDate:
                    case ContractStrategyName.ContractLabel:
                    case ContractStrategyName.UnitCode:
                        return text;

                    case ContractStrategyName.OriginalContract:
                        return "ContractLabel = ContractCode";

                    case ContractStrategyName.NotOriginalContract:
                        return "ContractLabel <> ContractCode";

                    case ContractStrategyName.WBSCode:
                        return string.Format(" ContractCode in ( select ContractCode from TaskContract where WBSCode='{0}' ) ", strategy.GetParameter(0));

                    case ContractStrategyName.AccessRange:
                        return AccessRanggeQuery.BuildContractAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("Contract"), SystemClassDescription.GetItemKeyColumnName("Contract"), SystemClassDescription.GetItemTypeColumnName("Contract"), SystemClassDescription.GetItemCreateUserColumnName("Contract"));

                    case ContractStrategyName.CostBudgetSetCode:
                        return string.Format("exists(select 1 from ContractCost left join CBS on CBS.FullCode like '%'+ContractCost.CostCode+'%' where ContractCost.CostBudgetSetCode = '{0}'and ContractCost.ContractCode = Contract.ContractCode)", strategy.GetParameter(0).Trim());

                    case ContractStrategyName.CostCode:
                        return string.Format("exists(select 1 from ContractCost left join CBS on CBS.FullCode like '%'+ContractCost.CostCode+'%' where ContractCost.CostCode = '{0}'and ContractCost.ContractCode = Contract.ContractCode)", strategy.GetParameter(0).Trim());

                    case ContractStrategyName.FullCode:
                        return string.Format("exists(select 1 from ContractCost left join CBS on CBS.FullCode like '%'+ContractCost.CostCode+'%' where CBS.FullCode like '{0}'and ContractCost.ContractCode = Contract.ContractCode)", strategy.GetParameter(0).Trim());

                    case ContractStrategyName.SupplierName:
                        return string.Format("SupplierCode in (select SupplierCode from Supplier where Supplier.SupplierName like '%{0}%')", strategy.GetParameter(0).Trim());
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

