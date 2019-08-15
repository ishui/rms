namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public class PBSDAO
    {
        public static void DeletePBSPic(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPic"))
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

        public static void DeletePBSPicGroup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPicGroup"))
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

        public static void DeletePBSType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
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

        public static void DeletePBSTypeLayout(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
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

        public static void DeletePBSUnit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSUnit"))
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

        public static EntityData GetAllPBSPic()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPic"))
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

        public static EntityData GetAllPBSPicGroup()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPicGroup"))
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

        public static EntityData GetAllPBSType()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
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

        public static EntityData GetAllPBSTypeLayout()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
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

        public static EntityData GetAllPBSUnit()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSUnit"))
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

        public static EntityData GetPBSPicByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPic"))
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

        public static EntityData GetPBSPicGroupByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPicGroup"))
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

        public static EntityData GetPBSTypeAllChildByParentCode(string parentCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("PBSType");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
                {
                    string text = "%" + parentCode + "%";
                    ydao.FillEntity(SqlManager.GetSqlStruct("PBSType", "SelectAllChildByParentCode").SqlString, "@ParentCode", parentCode, entitydata, "PBSType");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPBSTypeByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
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

        public static EntityData GetPBSTypeByParentCode(string parentCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("PBSType");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("PBSType", "SelectByParentCode").SqlString, "@ParentCode", parentCode, entitydata, "PBSType");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPBSTypeByProject(string aProjectCode)
        {
            EntityData data2;
            try
            {
                string text = "0";
                EntityData entitydata = new EntityData("PBSType");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("PBSType", "SelectByProjectCode").SqlString, "@ProjectCode", text, entitydata, "PBSType");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPBSTypeLayoutByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
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

        public static EntityData GetPBSTypeLayoutByProject(string strProjectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("PBSTypeLayout");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("PBSTypeLayout", "SelectByProjectCode").SqlString, "@ProjectCode", strProjectCode, entitydata, "PBSTypeLayout");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPBSTypeLayoutByProjectPBSType(string ProjectCode, string PBSTypeCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("PBSTypeLayout");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@PBSTypeCode" };
                    object[] values = new object[] { ProjectCode, PBSTypeCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("PBSTypeLayout", "SelectByProjectPBSType").SqlString, Params, values, entitydata, "PBSTypeLayout");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static int GetPBSTypeMaxSortID(string projectCode, string parentCode)
        {
            int num2;
            try
            {
                int @int;
                EntityData entitydata = new EntityData("PBSType");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@ParentCode" };
                    object[] values = new object[] { projectCode, parentCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("PBSType", "SelectMaxSortID").SqlString, Params, values, entitydata, "PBSType");
                }
                if (entitydata.Tables[0].Rows.Count > 0)
                {
                    @int = entitydata.GetInt("SortID");
                }
                else
                {
                    @int = 0;
                }
                entitydata.Dispose();
                num2 = @int;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static EntityData GetPBSUnitByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSUnit"))
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

        public static EntityData GetPBSUnitByProject(string ProjectCode)
        {
            EntityData data2;
            try
            {
                PBSUnitStrategyBuilder builder = new PBSUnitStrategyBuilder("PBSUnit");
                builder.AddStrategy(new Strategy(PBSUnitStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("PBSUnitName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("PBSUnit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_PBSUnitByCode(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                PBSUnitStrategyBuilder builder = new PBSUnitStrategyBuilder("V_PBSUnit");
                builder.AddStrategy(new Strategy(PBSUnitStrategyName.PBSUnitCode, PBSUnitCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("PBSUnit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_PBSUnitByProject(string ProjectCode)
        {
            EntityData data2;
            try
            {
                PBSUnitStrategyBuilder builder = new PBSUnitStrategyBuilder("V_PBSUnit");
                builder.AddStrategy(new Strategy(PBSUnitStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("PBSUnitName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("PBSUnit", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertPBSPic(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPic"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPBSPicGroup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPicGroup"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPBSType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPBSTypeLayout(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPBSUnit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSUnit"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllPBSPic(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPic"))
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

        public static void SubmitAllPBSPicGroup(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPicGroup"))
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

        public static void SubmitAllPBSTypeLayout(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
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

        public static void SubmitAllPBSUnit(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSUnit"))
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

        public static void UpdatePBSPic(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPic"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePBSPicGroup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSPicGroup"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePBSType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSType"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePBSTypeLayout(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSTypeLayout"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePBSUnit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBSUnit"))
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

