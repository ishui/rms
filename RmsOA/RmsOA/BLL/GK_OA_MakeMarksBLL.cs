namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_MakeMarksBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Transaction);
            return sdal.Delete(Code);
        }

        public GK_OA_MakeMarksModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Connection);
            return sdal.GetModel(Code);
        }

        public GK_OA_MakeMarksModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Transaction);
            return sdal.GetModel(Code);
        }

        public List<GK_OA_MakeMarksModel> GetModels(SqlConnection Connection)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Connection);
            return sdal.Select();
        }

        public List<GK_OA_MakeMarksModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Transaction);
            return sdal.Select();
        }

        public List<GK_OA_MakeMarksModel> GetModels(GK_OA_MakeMarksQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Connection);
            return sdal.Select(ObjQueryModel);
        }

        public List<GK_OA_MakeMarksModel> GetModels(GK_OA_MakeMarksQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Transaction);
            return sdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_MakeMarksModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Transaction);
            return sdal.Insert(ObjModel);
        }

        public int Update(GK_OA_MakeMarksModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_MakeMarksDAL sdal = new GK_OA_MakeMarksDAL(Transaction);
            return sdal.Update(ObjModel);
        }
    }
}

