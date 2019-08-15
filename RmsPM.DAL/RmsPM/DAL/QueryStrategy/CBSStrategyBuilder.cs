namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class CBSStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryChildCountString = "";

        public CBSStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CBS", "SelectAll").SqlString;
            this.QueryChildCountString = SqlManager.GetSqlStruct("CBS", "SelectChildCount").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CBSStrategyName) strategy.Name))
            {
                case CBSStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CBSStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CBSStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CBSStrategyName.Deep:
                    strategy.RelationFieldName = "Deep";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CBSStrategyName.CostName:
                    strategy.RelationFieldName = "CostName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CBSStrategyName.CostCodeStr:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringRange;
                    break;

                case CBSStrategyName.SortID:
                    strategy.RelationFieldName = "SortID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CBSStrategyName.BudgetType:
                    strategy.RelationFieldName = "BudgetType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CBSStrategyName.ParentCodeIn:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case CBSStrategyName.FullCodeInLike:
                    strategy.RelationFieldName = "FullCode";
                    strategy.Type = StrategyType.StringLikeEx1;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryChildCountString()
        {
            return (this.QueryChildCountString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CBSStrategyName name = (CBSStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            switch (name)
            {
                case CBSStrategyName.UserAccess:
                    return string.Format(" CostCode in ( select CostCode from CBSPerson where UserCode='{0}' ) ", strategy.GetParameter(0));

                case CBSStrategyName.CostCodeStr:
                case CBSStrategyName.SortID:
                    return text;

                case CBSStrategyName.AllSubNodeIncludeSelf:
                    return BuildTreeNodeSearchString(strategy.GetParameter(0), TreeNodeSearchType.AllSubNodeIncludeSelf);

                case CBSStrategyName.AllSubNodeNotIncludeSelf:
                    return BuildTreeNodeSearchString(strategy.GetParameter(0), TreeNodeSearchType.AllSubNodeNotIncludeSelf);

                case CBSStrategyName.FirstChildNode:
                    return BuildTreeNodeSearchString(strategy.GetParameter(0), TreeNodeSearchType.FirstChildNode);

                case CBSStrategyName.AllSubLeafNode:
                    return BuildTreeNodeSearchString(strategy.GetParameter(0), TreeNodeSearchType.AllSubLeafNode);

                case CBSStrategyName.AllSubNotLeafNode:
                    return BuildTreeNodeSearchString(strategy.GetParameter(0), TreeNodeSearchType.AllSubNotLeafNode);

                case CBSStrategyName.AccessRange:
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

        public static string BuildTreeNodeSearchString(string costCode, TreeNodeSearchType searchType)
        {
            string cBSFullCode = CBSDAO.GetCBSFullCode(costCode);
            switch (searchType)
            {
                case TreeNodeSearchType.AllSubNodeIncludeSelf:
                    return string.Format(" costCode in ( select costCode from CBS where FullCode  like '{0}%'  ) ", cBSFullCode);

                case TreeNodeSearchType.AllSubNodeNotIncludeSelf:
                    return string.Format(" costCode in ( select costCode from CBS where FullCode  FullCode like '{0}%' and FullCode <> {'0'}  ) ", cBSFullCode);

                case TreeNodeSearchType.FirstChildNode:
                    return string.Format(" CostCode in ( select CostCode from CBS where ParentCode = '{0}'  ) ", cBSFullCode);

                case TreeNodeSearchType.AllSubLeafNode:
                    return string.Format("  costCode in ( select costCode from CBS c where FullCode like '{0}%' and Not Exists ( select * from CBS e where e.ParentCode = c.CostCode  ) ) ", cBSFullCode);

                case TreeNodeSearchType.AllSubNotLeafNode:
                    return string.Format("  costCode in ( select costCode from CBS c where FullCode like '{0}%' and Exists ( select * from CBS e where e.ParentCode = c.CostCode  ) ) ", cBSFullCode);

                case TreeNodeSearchType.OnlySelfNode:
                    return string.Format(" CostCode  = '{0}'  ) ", costCode);
            }
            return "";
        }
    }
}

