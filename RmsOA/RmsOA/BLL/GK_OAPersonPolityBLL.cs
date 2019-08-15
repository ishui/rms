namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OAPersonPolityBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Transaction);
            return ydal.Delete(Code);
        }

        public GK_OAPersonPolityModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Connection);
            return ydal.GetModel(Code);
        }

        public GK_OAPersonPolityModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Transaction);
            return ydal.GetModel(Code);
        }

        public List<GK_OAPersonPolityModel> GetModels(SqlConnection Connection)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Connection);
            return ydal.Select();
        }

        public List<GK_OAPersonPolityModel> GetModels(SqlTransaction Transaction)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Transaction);
            return ydal.Select();
        }

        public List<GK_OAPersonPolityModel> GetModels(GK_OAPersonPolityQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Connection);
            return ydal.Select(ObjQueryModel);
        }

        public List<GK_OAPersonPolityModel> GetModels(GK_OAPersonPolityQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Transaction);
            return ydal.Select(ObjQueryModel);
        }

        public int Insert(GK_OAPersonPolityModel ObjModel, SqlTransaction Transaction)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Transaction);
            return ydal.Insert(ObjModel);
        }

        public int Update(GK_OAPersonPolityModel ObjModel, SqlTransaction Transaction)
        {
            GK_OAPersonPolityDAL ydal = new GK_OAPersonPolityDAL(Transaction);
            return ydal.Update(ObjModel);
        }
    }
}

