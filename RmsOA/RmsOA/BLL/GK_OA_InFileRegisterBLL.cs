namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_InFileRegisterBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Transaction);
            return rdal.Delete(Code);
        }

        public GK_OA_InFileRegisterModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Connection);
            return rdal.GetModel(Code);
        }

        public GK_OA_InFileRegisterModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<GK_OA_InFileRegisterModel> GetModels(SqlConnection Connection)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Connection);
            return rdal.Select();
        }

        public List<GK_OA_InFileRegisterModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Transaction);
            return rdal.Select();
        }

        public List<GK_OA_InFileRegisterModel> GetModels(GK_OA_InFileRegisterQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<GK_OA_InFileRegisterModel> GetModels(GK_OA_InFileRegisterQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_InFileRegisterModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int Update(GK_OA_InFileRegisterModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterDAL rdal = new GK_OA_InFileRegisterDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

