namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_InFileRegisterMainBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Transaction);
            return ndal.Delete(Code);
        }

        public GK_OA_InFileRegisterMainModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Connection);
            return ndal.GetModel(Code);
        }

        public GK_OA_InFileRegisterMainModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Transaction);
            return ndal.GetModel(Code);
        }

        public List<GK_OA_InFileRegisterMainModel> GetModels(SqlConnection Connection)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Connection);
            return ndal.Select();
        }

        public List<GK_OA_InFileRegisterMainModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Transaction);
            return ndal.Select();
        }

        public List<GK_OA_InFileRegisterMainModel> GetModels(GK_OA_InFileRegisterMainQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Connection);
            return ndal.Select(ObjQueryModel);
        }

        public List<GK_OA_InFileRegisterMainModel> GetModels(GK_OA_InFileRegisterMainQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Transaction);
            return ndal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_InFileRegisterMainModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Transaction);
            return ndal.Insert(ObjModel);
        }

        public int Update(GK_OA_InFileRegisterMainModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_InFileRegisterMainDAL ndal = new GK_OA_InFileRegisterMainDAL(Transaction);
            return ndal.Update(ObjModel);
        }
    }
}

