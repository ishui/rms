namespace RmsPM.BLL
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class MaterialRule
    { 
        public static void DeleteAllMaterialCost()
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    agent.ExecuteSql("delete MaterialCost");
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteAllSupplierMaterial()
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    agent.ExecuteSql("delete SupplierMaterial");
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteMaterialCost(string MaterialCostCode)
        {
            try
            {
                EntityData entity = MaterialDAO.GetMaterialCostByCode(MaterialCostCode);
                MaterialDAO.DeleteMaterialCost(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteSupplierMaterial(string SupplierMaterialCode)
        {
            try
            {
                EntityData entity = MaterialDAO.GetSupplierMaterialByCode(SupplierMaterialCode);
                MaterialDAO.DeleteSupplierMaterial(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

