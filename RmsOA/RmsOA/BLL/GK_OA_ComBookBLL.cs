namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_ComBookBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Transaction);
            return kdal.Delete(Code);
        }

        public GK_OA_ComBookModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Connection);
            return kdal.GetModel(Code);
        }

        public GK_OA_ComBookModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Transaction);
            return kdal.GetModel(Code);
        }

        public List<GK_OA_ComBookModel> GetModels(SqlConnection Connection)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Connection);
            return kdal.Select();
        }

        public List<GK_OA_ComBookModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Transaction);
            return kdal.Select();
        }

        public List<GK_OA_ComBookModel> GetModels(GK_OA_ComBookQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Connection);
            return kdal.Select(ObjQueryModel);
        }

        public List<GK_OA_ComBookModel> GetModels(GK_OA_ComBookQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Transaction);
            return kdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_ComBookModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Transaction);
            return kdal.Insert(ObjModel);
        }

        public int Update(GK_OA_ComBookModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_ComBookDAL kdal = new GK_OA_ComBookDAL(Transaction);
            return kdal.Update(ObjModel);
        }
    }
}

