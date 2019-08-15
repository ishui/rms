namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_CarBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Transaction);
            return rdal.Delete(Code);
        }

        public GK_OA_CarModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Connection);
            return rdal.GetModel(Code);
        }

        public GK_OA_CarModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<GK_OA_CarModel> GetModels(SqlConnection Connection)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Connection);
            return rdal.Select();
        }

        public List<GK_OA_CarModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Transaction);
            return rdal.Select();
        }

        public List<GK_OA_CarModel> GetModels(GK_OA_CarQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<GK_OA_CarModel> GetModels(GK_OA_CarQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_CarModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int Update(GK_OA_CarModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CarDAL rdal = new GK_OA_CarDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

