namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public sealed class AccessRanggeQuery
    {
        /// <summary>
        /// 原先的到点

        /// </summary>
        /// <param name="operationCode"></param>
        /// <param name="userCode"></param>
        /// <param name="stationCodes"></param>
        /// <param name="tableName"></param>
        /// <param name="keyColumnName"></param>
        /// <returns></returns>
        public static string BuildAccessRangeString(string operationCode, string userCode, string stationCodes, string tableName, string keyColumnName)
        {
            string classCode = SystemManageDAO.GetFunctionStructureParentCode(operationCode);
            string codes = BuildStationCodeString(stationCodes);
            return BuildAccessResourceString(operationCode, classCode, userCode, codes, tableName, keyColumnName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationCode"></param>
        /// <param name="userCode"></param>
        /// <param name="stationCodes"></param>
        /// <param name="tableName"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="createUserColumnName">创建人在表中的列，如果为空就忽略</param>
        /// <returns></returns>
        public static string BuildAccessRangeString(string operationCode, string userCode, string stationCodes, string tableName, string keyColumnName, string createUserColumnName)
        {
            string classCode = SystemManageDAO.GetFunctionStructureParentCode(operationCode);
            string codes = BuildStationCodeString(stationCodes);
            string text3 = BuildAccessResourceString(operationCode, classCode, userCode, codes, tableName, keyColumnName, createUserColumnName);
            return string.Format("( {0}  )", text3);
        }

        /// <summary>
        /// 到类型

        /// </summary>
        /// <param name="operationCode"></param>
        /// <param name="userCode"></param>
        /// <param name="stationCodes"></param>
        /// <param name="tableName"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="typeColumnName"></param>
        /// <param name="createUserColumnName">创建人在表中的列，如果为空就忽略</param>
        /// <returns></returns>
        public static string BuildAccessRangeString(string operationCode, string userCode, string stationCodes, string tableName, string keyColumnName, string typeColumnName, string createUserColumnName)
        {
            string classCode = SystemManageDAO.GetFunctionStructureParentCode(operationCode);
            string codes = BuildStationCodeString(stationCodes);
            string text3 = BuildAccessResourceString(operationCode, classCode, userCode, codes, tableName, keyColumnName, createUserColumnName);
            string text4 = BuildAccessTypeString(operationCode, classCode, userCode, codes, tableName, keyColumnName, typeColumnName, createUserColumnName);
            return string.Format("( {0}  {1}   )", text3, text4);
        }

        public static string BuildAccessRangeStringNoGroupCode(string operationCode, string userCode, string stationCodes, string tableName, string keyColumnName, string typeColumnName, string createUserColumnName)
        {
            string classCode = SystemManageDAO.GetFunctionStructureParentCode(operationCode);
            string codes = BuildStationCodeString(stationCodes);
            string text3 = BuildAccessResourceString(operationCode, classCode, userCode, codes, tableName, keyColumnName, createUserColumnName);
            string text4 = BuildAccessTypeStringNoGroupCode(operationCode, classCode, userCode, codes, tableName, keyColumnName, typeColumnName, createUserColumnName);
            return string.Format("( {0}  {1}   )", text3, text4);
        }

        public static string BuildAccessResourceString(string operationCode, string classCode, string userCode, string codes, string tableName, string keyColumnName)
        {
            return string.Format(" exists ( select 1 from resource where resource.relationCode={0}.{1} and classCode='{2}' and exists (  select 1 from AccessRange where  AccessRange.ResourceCode = Resource.ResourceCode  and ( ( AccessRangeType=0 and relationCode = '{3}' ) or ( AccessRangeType=1 and relationCode in ( {4} ) and (RoleLevel <> 1 or RoleLevel is null )  )) and  OperationCode =  '{5}'  ))  ", new object[] { tableName, keyColumnName, classCode, userCode, codes, operationCode });
        }

        public static string BuildAccessResourceString(string operationCode, string classCode, string userCode, string codes, string tableName, string keyColumnName, string createUserColumnName)
        {
            return string.Format(" exists ( select 1 from resource where resource.relationCode={0}.{1} and classCode='{2}' and exists (  select 1 from AccessRange where  AccessRange.ResourceCode = Resource.ResourceCode  and ( ( AccessRangeType=0 and relationCode = '{3}' ) or ( AccessRangeType=1 and relationCode in ( {4} ) and (RoleLevel <> 1 or RoleLevel is null )  )  or ( AccessRangeType=1 and relationCode in ( {4} ) and RoleLevel =1 and {0}.{6}='{3}'  ))   and  OperationCode =  '{5}'  ))  ", new object[] { tableName, keyColumnName, classCode, userCode, codes, operationCode, createUserColumnName });
        }

        public static string BuildAccessTypeString(string operationCode, string classCode, string userCode, string codes, string tableName, string keyColumnName, string typeColumnName, string createUserColumnName)
        {
            string text = "";
            string queryString = string.Format(" select accessRange.groupCode,SystemGroup.FullID,RoleLevel from accessRange left join SystemGroup on accessRange.GroupCode=SystemGroup.GroupCode where OperationCode = '{0}' and (( AccessRangeType=0 and relationCode = '{1}' ) or ( AccessRangeType=1 and relationCode in ( {2} ) )  ) ", new object[] { operationCode, userCode, codes });
            QueryAgent agent = new QueryAgent();
            DataSet set = agent.ExecSqlForDataSet(queryString);
            agent.Dispose();
            foreach (DataRow row in set.Tables[0].Rows)
            {
                if (!row.IsNull("FullID"))
                {
                    string text3 = row["FullID"].ToString();
                    int num = 0;
                    if (!row.IsNull("RoleLevel"))
                    {
                        num = (int) row["RoleLevel"];
                    }
                    if (num == 0)
                    {
                        text = text + string.Format("or ( dbo.GetSystemGroupFullID({0}.{1}) like '{2}%' ) ", tableName, typeColumnName, text3);
                    }
                    else
                    {
                        text = text + string.Format("or ( dbo.GetSystemGroupFullID({0}.{1}) like '{2}%' and {0}.{3}='{4}'  ) ", new object[] { tableName, typeColumnName, text3, createUserColumnName, userCode });
                    }
                }
            }
            return text;
        }

        public static string BuildAccessTypeStringNoGroupCode(string operationCode, string classCode, string userCode, string codes, string tableName, string keyColumnName, string typeColumnName, string createUserColumnName)
        {
            string text = "";
            string queryString = string.Format(" select accessRange.groupCode,SystemGroup.FullID,RoleLevel from accessRange left join SystemGroup on accessRange.GroupCode=SystemGroup.GroupCode where OperationCode = '{0}' and (( AccessRangeType=0 and relationCode = '{1}' ) or ( AccessRangeType=1 and relationCode in ( {2} ) )  ) ", new object[] { operationCode, userCode, codes });
            QueryAgent agent = new QueryAgent();
            DataSet set = agent.ExecSqlForDataSet(queryString);
            agent.Dispose();
            text = text + string.Format("or isnull({0}.{1}, '') = '' ", tableName, typeColumnName);
            foreach (DataRow row in set.Tables[0].Rows)
            {
                if (!row.IsNull("FullID"))
                {
                    string text3 = row["FullID"].ToString();
                    int num = 0;
                    if (!row.IsNull("RoleLevel"))
                    {
                        num = (int) row["RoleLevel"];
                    }
                    if (num == 0)
                    {
                        text = text + string.Format(" or ( dbo.GetSystemGroupFullID({0}.{1}) like '{2}%' ) ", tableName, typeColumnName, text3);
                    }
                    else
                    {
                        text = text + string.Format(" or ( dbo.GetSystemGroupFullID({0}.{1}) like '{2}%' and {0}.{3}='{4}'  ) ", new object[] { tableName, typeColumnName, text3, createUserColumnName, userCode });
                    }
                }
            }
            return text;
        }

        public static string BuildContractAccessRangeString(string operationCode, string userCode, string stationCodes, string tableName, string keyColumnName, string typeColumnName, string createUserColumnName)
        {
            string classCode = SystemManageDAO.GetFunctionStructureParentCode(operationCode);
            string codes = BuildStationCodeString(stationCodes);
            string text3 = BuildContractAccessResourceString(operationCode, classCode, userCode, codes, tableName, keyColumnName, createUserColumnName);
            string text4 = BuildAccessTypeString(operationCode, classCode, userCode, codes, tableName, keyColumnName, typeColumnName, createUserColumnName);
            return string.Format("( {0}  {1}   )", text3, text4);
        }

        public static string BuildContractAccessResourceString(string operationCode, string classCode, string userCode, string codes, string tableName, string keyColumnName, string createUserColumnName)
        {
            return ("(" + string.Format(" exists ( select 1 from resource where resource.relationCode={0}.{1} and classCode='{2}' and exists (  select 1 from AccessRange where  AccessRange.ResourceCode = Resource.ResourceCode  and ( ( AccessRangeType=0 and relationCode = '{3}' ) or ( AccessRangeType=1 and relationCode in ( {4} ) and (RoleLevel <> 1 or RoleLevel is null )  )  or ( AccessRangeType=1 and relationCode in ( {4} ) and RoleLevel =1 and {0}.{6}='{3}'  ))   and  OperationCode =  '{5}'  ))  ", new object[] { tableName, keyColumnName, classCode, userCode, codes, operationCode, createUserColumnName }) + " and Contract.Status in (0, 2))");
        }

        public static string BuildStationCodeString(string stationCodes)
        {
            string text = "";
            foreach (string text2 in stationCodes.Split(new char[] { ',' }))
            {
                if (text != "")
                {
                    text = text + ",";
                }
                text = text + "'" + text2 + "'";
            }
            return text;
        }
    }
}

