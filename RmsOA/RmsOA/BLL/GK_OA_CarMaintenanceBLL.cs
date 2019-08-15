namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_CarMaintenanceBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Transaction);
            return edal.Delete(Code);
        }

        public GK_OA_CarMaintenanceModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Connection);
            return edal.GetModel(Code);
        }

        public GK_OA_CarMaintenanceModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<GK_OA_CarMaintenanceModel> GetModels(SqlConnection Connection)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Connection);
            return edal.Select();
        }

        public List<GK_OA_CarMaintenanceModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Transaction);
            return edal.Select();
        }

        public List<GK_OA_CarMaintenanceModel> GetModels(GK_OA_CarMaintenanceQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<GK_OA_CarMaintenanceModel> GetModels(GK_OA_CarMaintenanceQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_CarMaintenanceModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(GK_OA_CarMaintenanceModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CarMaintenanceDAL edal = new GK_OA_CarMaintenanceDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

