namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class MaterialInventoryBLL
    {
        public List<V_MaterialInventoryModel> GetModels(SqlConnection Connection)
        {
            List<V_MaterialInventoryModel> list;
            try
            {
                list = new V_MaterialInventoryDAL(Connection).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryModel> GetModels(SqlTransaction Transaction)
        {
            List<V_MaterialInventoryModel> list;
            try
            {
                list = new V_MaterialInventoryDAL(Transaction).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryModel> GetModels(V_MaterialInventoryQueryModel ObjQueryModel, SqlConnection Connection)
        {
            List<V_MaterialInventoryModel> list;
            try
            {
                list = new V_MaterialInventoryDAL(Connection).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryModel> GetModels(V_MaterialInventoryQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            List<V_MaterialInventoryModel> list;
            try
            {
                list = new V_MaterialInventoryDAL(Transaction).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }
    }
}

