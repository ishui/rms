namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class ResourceRule
    {
        public static string GetAccessRange(string code, string classCode, string operationCode, ref string userCodes, ref string userNames, ref string stationCodes, ref string stationNames)
        {
            string text3;
            try
            {
                userCodes = "";
                userNames = "";
                stationCodes = "";
                stationNames = "";
                string resourceCode = GetResourceCode(code, classCode);
                if (resourceCode != "")
                {
                    AccessRangeStrategyBuilder builder = new AccessRangeStrategyBuilder();
                    builder.AddStrategy(new Strategy(AccessRangeStrategyName.ResourceCode, resourceCode));
                    builder.AddStrategy(new Strategy(AccessRangeStrategyName.OperationCode, operationCode));
                    QueryAgent agent = new QueryAgent();
                    EntityData data = agent.FillEntityData("AccessRange", builder.BuildMainQueryString());
                    agent.Dispose();
                    int count = data.CurrentTable.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        data.SetCurrentRow(i);
                        int @int = data.GetInt("AccessRangeType");
                        string userCode = data.GetString("RelationCode");
                        if (@int == 0)
                        {
                            if (userCodes != "")
                            {
                                userCodes = userCodes + ",";
                                userNames = userNames + ",";
                            }
                            userCodes = userCodes + userCode;
                            userNames = userNames + SystemRule.GetUserName(userCode);
                        }
                        else
                        {
                            if (stationCodes != "")
                            {
                                stationCodes = stationCodes + ",";
                                stationNames = stationNames + ",";
                            }
                            stationCodes = stationCodes + userCode;
                            stationNames = stationNames + SystemRule.GetStationName(userCode);
                        }
                    }
                    data.Dispose();
                }
                text3 = resourceCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetAccessRangeRoleLevelName(object objRoleLevel)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToInt(objRoleLevel) == 1)
                {
                    text = "个人";
                }
                else
                {
                    text = "所有";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetResourceCode(string code, string classCode)
        {
            string text2;
            try
            {
                string text = "";
                ResourceStrategyBuilder builder = new ResourceStrategyBuilder();
                ArrayList pas = new ArrayList();
                pas.Add(classCode);
                pas.Add(code);
                builder.AddStrategy(new Strategy(ResourceStrategyName.ClassRelationCode, pas));
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Resource", builder.BuildMainQueryString());
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("ResourceCode");
                }
                data.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static bool RemoveUserRule(string strUser, string strOperationCode, string strRelationCode)
        {
            return true;
        }

        public static string SetResourceAccessRange(string code, string classCode, string unitCode)
        {
            return SetResourceAccessRange(code, classCode, unitCode, true);
        }

        public static string SetResourceAccessRange(string code, string classCode, string unitCode, bool isRefreshRight)
        {
            string text5;
            try
            {
                string resourceCode = GetResourceCode(code, classCode);
                bool flag = resourceCode == "";
                EntityData entity = null;
                if (flag)
                {
                    resourceCode = SystemManageDAO.GetNewSysCode("ResourceCode");
                    entity = new EntityData("Standard_Resource");
                    DataRow newRecord = entity.GetNewRecord();
                    newRecord["ResourceCode"] = resourceCode;
                    newRecord["ClassCode"] = classCode;
                    newRecord["UnitCode"] = unitCode;
                    newRecord["RelationCode"] = code;
                    entity.AddNewRecord(newRecord);
                }
                else
                {
                    entity = ResourceDAO.GetStandard_ResourceByCode(resourceCode);
                    entity.CurrentRow["unitCode"] = unitCode;
                }
                if (isRefreshRight)
                {
                    entity.DeleteAllTableRow("AccessRange");
                }
                EntityData stationByUnitAccess = OBSDAO.GetStationByUnitAccess(unitCode);
                entity.SetCurrentTable("AccessRange");
                foreach (DataRow row2 in stationByUnitAccess.CurrentTable.Rows)
                {
                    string text2 = row2["RoleCode"].ToString();
                    string text3 = row2["StationCode"].ToString();
                    EntityData data3 = SystemManageDAO.GetStandard_RoleByCode(text2);
                    foreach (DataRow row3 in data3.Tables["RoleOperation"].Select(string.Format(" SubString(OperationCode,1,4)='{0}' ", classCode)))
                    {
                        string text4 = (string) row3["OperationCode"];
                        DataRow row = entity.GetNewRecord();
                        row["AccessRangeCode"] = SystemManageDAO.GetNewSysCode("AccessRangeCode");
                        row["ResourceCode"] = resourceCode;
                        row["UnitCode"] = unitCode;
                        row["OperationCode"] = text4;
                        row["AccessRangeType"] = 1;
                        row["RelationCode"] = text3;
                        entity.AddNewRecord(row);
                    }
                    data3.Dispose();
                }
                stationByUnitAccess.Dispose();
                ResourceDAO.SubmitAllStandard_Resource(entity);
                entity.Dispose();
                text5 = resourceCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text5;
        }

        public static string SetResourceAccessRange(string code, string classCode, string unitCode, ArrayList ar)
        {
            return SetResourceAccessRange(code, classCode, unitCode, ar, true);
        }

        public static string SetResourceAccessRange(string code, string classCode, string unitCode, ArrayList ar, bool isRefreshRight)
        {
            string text3;
            try
            {
                string resourceCode = GetResourceCode(code, classCode);
                bool flag = resourceCode == "";
                EntityData entity = null;
                if (flag)
                {
                    resourceCode = SystemManageDAO.GetNewSysCode("ResourceCode");
                    entity = new EntityData("Standard_Resource");
                    DataRow newRecord = entity.GetNewRecord();
                    newRecord["ResourceCode"] = resourceCode;
                    newRecord["ClassCode"] = classCode;
                    newRecord["UnitCode"] = unitCode;
                    newRecord["RelationCode"] = code;
                    entity.AddNewRecord(newRecord);
                }
                else
                {
                    entity = ResourceDAO.GetStandard_ResourceByCode(resourceCode);
                    entity.CurrentRow["unitCode"] = unitCode;
                }
                if (isRefreshRight)
                {
                    entity.DeleteAllTableRow("AccessRange");
                }
                entity.SetCurrentTable("AccessRange");
                int count = ar.Count;
                for (int i = 0; i < count; i++)
                {
                    AccessRange range = (AccessRange) ar[i];
                    foreach (string text2 in range.Operations.Split(new char[] { ',' }))
                    {
                        DataRow row = entity.GetNewRecord();
                        row["AccessRangeCode"] = SystemManageDAO.GetNewSysCode("AccessRangeCode");
                        row["ResourceCode"] = resourceCode;
                        row["UnitCode"] = unitCode;
                        row["OperationCode"] = text2;
                        row["AccessRangeType"] = range.AccessRangeType;
                        row["RelationCode"] = range.RelationCode;
                        entity.AddNewRecord(row);
                    }
                }
                ResourceDAO.SubmitAllStandard_Resource(entity);
                entity.Dispose();
                text3 = resourceCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }
    }
}

