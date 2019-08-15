namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Data;
    using Rms.ORMap;

    public class V_CBSCostStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryMainString0 = "";
        public string QuerySumString = "";

        public V_CBSCostStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_CBSCost", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("V_CBSCost", "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((V_CBSCostStrategyName) strategy.Name))
            {
                case V_CBSCostStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_CBSCostStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_CBSCostStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_CBSCostStrategyName.Deep:
                    strategy.RelationFieldName = "Deep";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case V_CBSCostStrategyName.CostName:
                    strategy.RelationFieldName = "CostName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_CBSCostStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case V_CBSCostStrategyName.ReviseBudgetCheckCode:
                    strategy.RelationFieldName = "ReviseBudgetCheckCode";
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
            V_CBSCostStrategyName name = (V_CBSCostStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            switch (name)
            {
                case V_CBSCostStrategyName.UserAccess:
                    return string.Format(" CBS.CostCode in ( select CostCode from CBSPerson where UserCode='{0}' ) ", strategy.GetParameter(0));

                case V_CBSCostStrategyName.ReviseBudgetCheckCode:
                    return text;

                case V_CBSCostStrategyName.CostCodeIncludeSubNodeAndLeaf:
                {
                    string costCode = strategy.GetParameter(0);
                    switch (strategy.GetParameter(1))
                    {
                        case "0":
                            return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNodeIncludeSelf);

                        case "1":
                            return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubLeafNode);

                        case "2":
                            return CBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNotLeafNode);
                    }
                    return text;
                }
                case V_CBSCostStrategyName.AccessRange:
                {
                    string parameter = strategy.GetParameter(1);
                    string text3 = strategy.GetParameter(2);
                    string text4 = "";
                    foreach (string text5 in text3.Split(new char[] { ',' }))
                    {
                        if (text4 != "")
                        {
                            text4 = text4 + ",";
                        }
                        text4 = text4 + "'" + text5 + "'";
                    }
                    string text6 = "";
                    if (text4.Length == 0)
                    {
                        text6 = string.Format(" ( AccessRangeType=0 and relationCode = '{0}' )   ", parameter);
                    }
                    else
                    {
                        text6 = string.Format(" (( AccessRangeType=0 and relationCode = '{0}' ) or ( AccessRangeType=1 and relationCode in ( {1} ) ))  ", parameter, text4);
                    }
                    QueryAgent agent = new QueryAgent();
                    string queryString = string.Format("select distinct( cbs.FullCode ) from cbs,accessRange where operationCode = '{0}' and cbs.costCode = accessRange.ResourceCode and ( {1} )", strategy.GetParameter(0), text6);
                    DataSet set = agent.ExecSqlForDataSet(queryString);
                    agent.Dispose();
                    if (set.Tables[0].Rows.Count > 0)
                    {
                        text = "";
                        foreach (DataRow row in set.Tables[0].Rows)
                        {
                            string text8 = row["FullCode"].ToString();
                            if (text != "")
                            {
                                text = text + " or ";
                            }
                            text = text + string.Format("( FullCode like '{0}%' )", text8);
                        }
                        text = "( " + text + ")";
                    }
                    else
                    {
                        text = " 1=1 ";
                    }
                    set.Dispose();
                    return text;
                }
            }
            return text;
        }

        public string BuilSumQueryString()
        {
            return (this.QuerySumString + this.BuildStrategysString() + base.BuildOrderString());
        }
    }
}

