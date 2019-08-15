namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;

    internal class GradeDAO
    {
        public static void DeleteStandard_Grade(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Grade"))
                {
                    ydao.SubmitEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_GradeMessage(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("GradeMessage"))
                {
                    ydao.SubmitEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

