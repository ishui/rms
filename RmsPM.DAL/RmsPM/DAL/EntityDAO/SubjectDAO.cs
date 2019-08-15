namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;

    public sealed class SubjectDAO
    {
        public static void DeleteSubject(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
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

        public static void DeleteSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SubjectSet"))
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

        public static EntityData GetAllSubject()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
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

        public static EntityData GetAllSubjectSet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SubjectSet"))
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

        public static EntityData GetChildSubject(string subjectSetCode, string subjectCode, int layer)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Subject");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
                {
                    string[] Params = new string[] { "@SubjectSetCode", "@SubjectCode", "@Layer" };
                    object[] values = new object[] { subjectSetCode, subjectCode, layer };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Subject", "SelectChildSubject").GetSqlStringWithOrder(), Params, values, entitydata, "Subject");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSubjectByCode(string subjectCode, string subjectSetCode)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
                {
                    object[] keyvalues = new object[] { subjectCode, subjectSetCode };
                    data = ydao.SelectbyPrimaryKey(keyvalues);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSubjectByLayer(string subjectSetCode, int layer)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Subject");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
                {
                    string[] Params = new string[] { "@SubjectSetCode", "@Layer" };
                    object[] values = new object[] { subjectSetCode, layer };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Subject", "SelectByLayer").GetSqlStringWithOrder(), Params, values, entitydata, "Subject");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSubjectBySubjectSet(string subjectSetCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Subject");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Subject", "SelectBySubjectSet").GetSqlStringWithOrder(), "@SubjectSetCode", subjectSetCode, entitydata, "Subject");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSubjectSetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SubjectSet"))
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

        public static void InsertSubject(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SubjectSet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllSubject(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
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

        public static void UpdateSubject(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Subject"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SubjectSet"))
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

