namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_OilBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Transaction);
            return ldal.Delete(Code);
        }

        public GK_OA_OilModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Connection);
            return ldal.GetModel(Code);
        }

        public GK_OA_OilModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<GK_OA_OilModel> GetModels(SqlConnection Connection)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Connection);
            return ldal.Select();
        }

        public List<GK_OA_OilModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Transaction);
            return ldal.Select();
        }

        public List<GK_OA_OilModel> GetModels(GK_OA_OilQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<GK_OA_OilModel> GetModels(GK_OA_OilQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_OilModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int Update(GK_OA_OilModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OilDAL ldal = new GK_OA_OilDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

