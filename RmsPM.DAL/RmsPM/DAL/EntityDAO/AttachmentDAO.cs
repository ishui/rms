namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;

    public class AttachmentDAO : IAttachmentDAO
    {
        private AttachmentDAO()
        {
        }

        public void DeleteAttachMent(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
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

        public EntityData GetAllAttachMent()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
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

        public EntityData GetAttachMentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
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

        public EntityData GetAttachMentByMasterCode(string type, string code)
        {
            EntityData data2;
            try
            {
                string[] Params = new string[] { "@AttachMentType", "@MasterCode" };
                object[] values = new object[] { type, code };
                EntityData entitydata = new EntityData();
                using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("AttachMent", "SelectByMasterCode").GetSqlStringWithOrder(), Params, values, entitydata, "AttachMent");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public EntityData GetAttachMentByTypeAndMasterCode(string AttachMentType, string MasterCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("AttachMent");
                using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
                {
                    string format = "select * from AttachMent where AttachMentType = '{0}' and MasterCode = '{1}'";
                    ydao.FillEntity(string.Format(format, AttachMentType, MasterCode), entitydata);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static AttachmentDAO getAttachmentDAO()
        {
            return new AttachmentDAO();
        }

        public void SubmitAllAttachMent(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
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

        public void UpdateAttachMent(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("AttachMent"))
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

