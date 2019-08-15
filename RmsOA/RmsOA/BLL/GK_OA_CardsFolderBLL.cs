namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_CardsFolderBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Transaction);
            return rdal.Delete(Code);
        }

        public GK_OA_CardsFolderModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Connection);
            return rdal.GetModel(Code);
        }

        public GK_OA_CardsFolderModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<GK_OA_CardsFolderModel> GetModels(SqlConnection Connection)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Connection);
            return rdal.Select();
        }

        public List<GK_OA_CardsFolderModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Transaction);
            return rdal.Select();
        }

        public List<GK_OA_CardsFolderModel> GetModels(GK_OA_CardsFolderQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<GK_OA_CardsFolderModel> GetModels(GK_OA_CardsFolderQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_CardsFolderModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int Update(GK_OA_CardsFolderModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CardsFolderDAL rdal = new GK_OA_CardsFolderDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

