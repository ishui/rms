namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OAPersonWorkBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Transaction);
            return kdal.Delete(Code);
        }

        public GK_OAPersonWorkModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Connection);
            return kdal.GetModel(Code);
        }

        public GK_OAPersonWorkModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Transaction);
            return kdal.GetModel(Code);
        }

        public List<GK_OAPersonWorkModel> GetModels(SqlConnection Connection)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Connection);
            return kdal.Select();
        }

        public List<GK_OAPersonWorkModel> GetModels(SqlTransaction Transaction)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Transaction);
            return kdal.Select();
        }

        public List<GK_OAPersonWorkModel> GetModels(GK_OAPersonWorkQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Connection);
            return kdal.Select(ObjQueryModel);
        }

        public List<GK_OAPersonWorkModel> GetModels(GK_OAPersonWorkQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Transaction);
            return kdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OAPersonWorkModel ObjModel, SqlTransaction Transaction)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Transaction);
            return kdal.Insert(ObjModel);
        }

        public int Update(GK_OAPersonWorkModel ObjModel, SqlTransaction Transaction)
        {
            GK_OAPersonWorkDAL kdal = new GK_OAPersonWorkDAL(Transaction);
            return kdal.Update(ObjModel);
        }
    }
}

