namespace RmsPM.DAL.EntityDAO
{
    using System;
    using System.Collections;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class ResourceDAO
    {
        public static void DeleteAccessRange(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("AccessRange"))
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

        public static void DeleteResource(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Resource"))
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

        public static void DeleteStandard_Resource(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Resource"))
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

        public static EntityData GetAccessRangeByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("AccessRange"))
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

        public static EntityData GetAccessRangeByGroupCode(string GroupCode)
        {
            EntityData data2;
            try
            {
                AccessRangeStrategyBuilder builder = new AccessRangeStrategyBuilder();
                builder.AddStrategy(new Strategy(AccessRangeStrategyName.GroupCode, GroupCode));
                builder.AddOrder("OperationCode", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("AccessRange", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAccessRangeByGroupRelation(string GroupCode, string AccessRangeType, string RelationCode)
        {
            EntityData data2;
            try
            {
                AccessRangeStrategyBuilder builder = new AccessRangeStrategyBuilder();
                builder.AddStrategy(new Strategy(AccessRangeStrategyName.GroupCode, GroupCode));
                ArrayList pas = new ArrayList();
                pas.Add(AccessRangeType);
                pas.Add(RelationCode);
                builder.AddStrategy(new Strategy(AccessRangeStrategyName.AccessRelation0, pas));
                builder.AddOrder("OperationCode", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("AccessRange", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAccessRangeByRelation(string AccessRangeType, string RelationCode)
        {
            EntityData data2;
            try
            {
                AccessRangeStrategyBuilder builder = new AccessRangeStrategyBuilder();
                ArrayList pas = new ArrayList();
                pas.Add(AccessRangeType);
                pas.Add(RelationCode);
                builder.AddStrategy(new Strategy(AccessRangeStrategyName.AccessRelation0, pas));
                builder.AddOrder("OperationCode", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("AccessRange", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAccessRangeByResourceCode(string ResourceCode)
        {
            EntityData data2;
            try
            {
                AccessRangeStrategyBuilder builder = new AccessRangeStrategyBuilder();
                builder.AddStrategy(new Strategy(AccessRangeStrategyName.ResourceCode, ResourceCode));
                builder.AddOrder("OperationCode", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("AccessRange", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllAccessRange()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("AccessRange"))
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

        public static EntityData GetAllResource()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Resource"))
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

        public static EntityData GetResourceByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Resource"))
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

        public static EntityData GetStandard_ResourceByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Resource");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Resource"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Resource", "Select").SqlString, "@ResourceCode", code, entitydata, "Resource");
                    ydao.FillEntity(SqlManager.GetSqlStruct("AccessRange", "SelectByResourceCode").SqlString, "@ResourceCode", code, entitydata, "AccessRange");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertAccessRange(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("AccessRange"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertResource(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Resource"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Resource(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Resource"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllAccessRange(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("AccessRange"))
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

        public static void SubmitAllResource(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Resource"))
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

        public static void SubmitAllStandard_Resource(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Resource"))
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

        public static void UpdateAccessRange(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("AccessRange"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateResource(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Resource"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Resource(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Resource"))
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

