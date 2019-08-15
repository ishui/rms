namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class MaterialInventoryIOBLL
    {
        public List<V_MaterialInventoryIOModel> GetModels(SqlConnection Connection)
        {
            List<V_MaterialInventoryIOModel> list;
            try
            {
                list = new V_MaterialInventoryIODAL(Connection).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryIOModel> GetModels(SqlTransaction Transaction)
        {
            List<V_MaterialInventoryIOModel> list;
            try
            {
                list = new V_MaterialInventoryIODAL(Transaction).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryIOModel> GetModels(V_MaterialInventoryIOQueryModel ObjQueryModel, SqlConnection Connection)
        {
            List<V_MaterialInventoryIOModel> list;
            try
            {
                list = new V_MaterialInventoryIODAL(Connection).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryIOModel> GetModels(V_MaterialInventoryIOQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            List<V_MaterialInventoryIOModel> list;
            try
            {
                list = new V_MaterialInventoryIODAL(Transaction).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }
    }
}

