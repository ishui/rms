namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_SubmitAccountMainBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Transaction);
            return ndal.Delete(Code);
        }

        public GK_OA_SubmitAccountMainModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Connection);
            return ndal.GetModel(Code);
        }

        public GK_OA_SubmitAccountMainModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Transaction);
            return ndal.GetModel(Code);
        }

        public List<GK_OA_SubmitAccountMainModel> GetModels(SqlConnection Connection)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Connection);
            return ndal.Select();
        }

        public List<GK_OA_SubmitAccountMainModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Transaction);
            return ndal.Select();
        }

        public List<GK_OA_SubmitAccountMainModel> GetModels(GK_OA_SubmitAccountMainQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Connection);
            return ndal.Select(ObjQueryModel);
        }

        public List<GK_OA_SubmitAccountMainModel> GetModels(GK_OA_SubmitAccountMainQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Transaction);
            return ndal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_SubmitAccountMainModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Transaction);
            return ndal.Insert(ObjModel);
        }

        public int Update(GK_OA_SubmitAccountMainModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountMainDAL ndal = new GK_OA_SubmitAccountMainDAL(Transaction);
            return ndal.Update(ObjModel);
        }
    }
}

