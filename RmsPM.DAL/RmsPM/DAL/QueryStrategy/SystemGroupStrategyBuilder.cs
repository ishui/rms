namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class SystemGroupStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public SystemGroupStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SystemGroup", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("SystemGroup", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SystemGroupStrategyName) strategy.Name))
            {
                case SystemGroupStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemGroupStrategyName.ClassCode:
                    strategy.RelationFieldName = "ClassCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemGroupStrategyName.GroupName:
                    strategy.RelationFieldName = "GroupName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemGroupStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemGroupStrategyName.Deep:
                    strategy.RelationFieldName = "Deep";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case SystemGroupStrategyName.SortID:
                    strategy.RelationFieldName = "SortID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemGroupStrategyName.FullID:
                    strategy.RelationFieldName = "FullID";
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
            SystemGroupStrategyName name = (SystemGroupStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SystemGroupStrategyName.AllChild:
                        return string.Format("FullID like '{0}-%'", strategy.GetParameter(0).Trim());

                    case SystemGroupStrategyName.IncludeAllChild:
                        return string.Format("(FullID = '{0}' or FullID like '{0}-%')", strategy.GetParameter(0).Trim());

                    case SystemGroupStrategyName.ProjectName:
                        return string.Format("(FullID like (select fullid from systemgroup where groupname='{0}' and classcode ='{1}' and deep =1) + '%')", strategy.GetParameter(0).Trim(), strategy.GetParameter(1).Trim());

                    case SystemGroupStrategyName.False:
                        return "1=2";
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }

        public static string BuildTreeNodeSearchString(string systemGroupCode, TreeNodeSearchType searchType, string typeColumnName)
        {
            string systemGroupFullID = SystemManageDAO.GetSystemGroupFullID(systemGroupCode);
            switch (searchType)
            {
                case TreeNodeSearchType.AllSubNodeIncludeSelf:
                    return string.Format(" {1} in ( select GroupCode from SystemGroup where FullID  like '{0}%'  ) ", systemGroupFullID, typeColumnName);

                case TreeNodeSearchType.AllSubNodeNotIncludeSelf:
                    return string.Format(" {1} in ( select GroupCode from SystemGroup where FullID  FullCode like '{0}%' and FullID <> {'0'}  ) ", systemGroupFullID, typeColumnName);

                case TreeNodeSearchType.FirstChildNode:
                    return string.Format(" {1} in ( select GroupCode from SystemGroup where FullID = '{0}'  ) ", systemGroupFullID, typeColumnName);

                case TreeNodeSearchType.AllSubLeafNode:
                    return string.Format("  {1} in ( select GroupCode from SystemGroup c where FullID like '{0}%' and Not Exists ( select * from SystemGroup e where e.ParentCode = c.systemGroupCode  ) ) ", systemGroupFullID, typeColumnName);

                case TreeNodeSearchType.AllSubNotLeafNode:
                    return string.Format("  {1} in ( select GroupCode from SystemGroup c where FullID like '{0}%' and Exists ( select * from SystemGroup e where e.ParentCode = c.systemGroupCode  ) ) ", systemGroupFullID, typeColumnName);

                case TreeNodeSearchType.OnlySelfNode:
                    return string.Format(" {1}  = '{0}'  ) ", systemGroupCode, typeColumnName);
            }
            return "";
        }

        public string GetDefaultOrder()
        {
            return " order by case isnull(sortid, '') when '' then '左' else sortid end, GroupName, GroupCode";
        }
    }
}

