namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;

    public class SupplierGradeTypeDao
    {
        public static EntityData GetAllSupplierGradeType()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierGradeType"))
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
    }
}

