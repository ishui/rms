namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;

    public class StampDutyDAO
    {
        private StampDutyDAO()
        {
        }

        public static void DeleteStampDuty(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
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

        public static EntityData GetAllStampDuty()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
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

        public static EntityData GetStampDutyByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
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

        public static void InsertStampDuty(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllStampDuty(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
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

        public static void UpdateStampDuty(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
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

