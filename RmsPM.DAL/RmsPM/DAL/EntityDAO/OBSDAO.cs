namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class OBSDAO
    {
        public static void DeleteStandard_Station(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Station"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_Unit(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Unit"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteUnit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Unit"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteUnitSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UnitSubjectSet"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAllStation()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllUnit()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Unit"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllUnitSubjectSet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("UnitSubjectSet"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetOBSUnitByCode(string unitCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.UnitCode, unitCode));
                string queryString = builder.BuildQueryOBSString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetOBSUnitByParent(string parentCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.ParentUnitCode, parentCode));
                string queryString = builder.BuildQueryOBSString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetOBSUnitOnlyHasUserByCode(string unitCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.UnitCode, unitCode));
                string queryString = builder.BuildQueryOBSOnlyHasUserString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetOBSUnitOnlyHasUserByParent(string parentCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.ParentUnitCode, parentCode));
                string queryString = builder.BuildQueryOBSOnlyHasUserString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_StationByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Station");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Station"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Station", "Select").SqlString, "@StationCode", code, entitydata, "Station");
                    ydao.FillEntity(SqlManager.GetSqlStruct("UserRole", "SelectByStationCode").SqlString, "@StationCode", code, entitydata, "UserRole");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_UnitByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Unit");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Unit"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Unit", "Select").SqlString, "@UnitCode", code, entitydata, "Unit");
                    ydao.FillEntity(SqlManager.GetSqlStruct("UnitSubjectSet", "SelectByUnitCode").SqlString, "@UnitCode", code, entitydata, "UnitSubjectSet");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStationByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStationByRoleCode(string roleCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Station");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Station", "SelectByRoleCode").SqlString, "@RoleCode", roleCode, entitydata, "Station");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStationByUnitAccess(string unitCode)
        {
            EntityData data2;
            try
            {
                string unitFullCode = GetUnitFullCode(unitCode);
                EntityData entitydata = new EntityData("Station");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Station", "SelectByUnitAccess").SqlString, "@FullCode", unitFullCode, entitydata, "Station");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStationByUnitCode(string unitCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Station");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Station", "SelectByUnitCode").SqlString, "@UnitCode", unitCode, entitydata, "Station");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStationByUserCode(string userCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Station");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Station", "SelectByUserCode").SqlString, "@UserCode", userCode, entitydata, "Station");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitAllChildAndSelf(string parentCode)
        {
            EntityData data3;
            try
            {
                string text = "";
                EntityData unitByCode = GetUnitByCode(parentCode);
                if (unitByCode.HasRecord())
                {
                    text = unitByCode.GetString("FullCode");
                }
                else
                {
                    text = "null";
                }
                unitByCode.Dispose();
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.UnderUnitCode, text));
                string queryString = builder.BuildQueryChildCountString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data2 = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data3 = data2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data3;
        }

        public static EntityData GetUnitByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Unit"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitByParent(string parentCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.ParentUnitCode, parentCode));
                string queryString = builder.BuildQueryChildCountString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitByProjectCode(string projectCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.RelaCode, projectCode));
                string queryString = builder.BuildMainQueryString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitBySortID(string SortID)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.SortID, SortID));
                string queryString = builder.BuildQueryChildCountString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitByUnitName(string UnitName)
        {
            return GetUnitByUnitName(UnitName, "");
        }

        public static EntityData GetUnitByUnitName(string UnitName, string projectCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.UnitName, UnitName));
                if (projectCode != "")
                {
                    builder.AddStrategy(new Strategy(UnitStrategyName.ProjectCode, projectCode));
                }
                string queryString = builder.BuildQueryChildCountString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetUnitFullCode(string unitCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData unitByCode = GetUnitByCode(unitCode);
                if (unitByCode.HasRecord())
                {
                    text = unitByCode.GetString("FullCode");
                }
                unitByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetUnitFullNameByUnitCode(string unitCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.UnitCode, unitCode));
                string queryString = builder.BuildQueryFullNameString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitOnlyHasUserByParent(string parentCode)
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.ParentUnitCode, parentCode));
                string queryString = builder.BuildQueryChildCountOnlyHasUserString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitSubjectSetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("UnitSubjectSet"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitSubjectSetBySubjectSet(string SubjectSetCode)
        {
            EntityData data2;
            try
            {
                UnitSubjectSetStrategyBuilder builder = new UnitSubjectSetStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitSubjectSetStrategyName.SubjectSetCode, SubjectSetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("UnitSubjectSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitSubjectSetByUnit(string UnitCode, string SubjectSetCode)
        {
            EntityData data2;
            try
            {
                UnitSubjectSetStrategyBuilder builder = new UnitSubjectSetStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitSubjectSetStrategyName.UnitCode, UnitCode));
                builder.AddStrategy(new Strategy(UnitSubjectSetStrategyName.SubjectSetCode, SubjectSetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("UnitSubjectSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertStandard_Station(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Station"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Unit(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Unit"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertUnit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Unit"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertUnitSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UnitSubjectSet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllStandard_Station(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Station"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_Unit(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Unit"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStation(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllUnitSubjectSet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UnitSubjectSet"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void UpdateStandard_Station(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Station"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Unit(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Unit"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Station"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateUnit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Unit"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateUnitSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UnitSubjectSet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

