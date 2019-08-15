namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_OutFileRegiesterBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Transaction);
            return rdal.Delete(Code);
        }

        public GK_OA_OutFileRegiesterModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Connection);
            return rdal.GetModel(Code);
        }

        public GK_OA_OutFileRegiesterModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<GK_OA_OutFileRegiesterModel> GetModels(SqlConnection Connection)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Connection);
            return rdal.Select();
        }

        public List<GK_OA_OutFileRegiesterModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Transaction);
            return rdal.Select();
        }

        public List<GK_OA_OutFileRegiesterModel> GetModels(GK_OA_OutFileRegiesterQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<GK_OA_OutFileRegiesterModel> GetModels(GK_OA_OutFileRegiesterQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_OutFileRegiesterModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int Update(GK_OA_OutFileRegiesterModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OutFileRegiesterDAL rdal = new GK_OA_OutFileRegiesterDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

