namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class OAPersonStudyBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Transaction);
            return ydal.Delete(Code);
        }

        public OAPersonStudyModel GetModel(int Code, SqlConnection Connection)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Connection);
            return ydal.GetModel(Code);
        }

        public OAPersonStudyModel GetModel(int Code, SqlTransaction Transaction)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Transaction);
            return ydal.GetModel(Code);
        }

        public List<OAPersonStudyModel> GetModels(SqlConnection Connection)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Connection);
            return ydal.Select();
        }

        public List<OAPersonStudyModel> GetModels(SqlTransaction Transaction)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Transaction);
            return ydal.Select();
        }

        public List<OAPersonStudyModel> GetModels(OAPersonStudyQueryModel ObjQueryModel, SqlConnection Connection)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Connection);
            return ydal.Select(ObjQueryModel);
        }

        public List<OAPersonStudyModel> GetModels(OAPersonStudyQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Transaction);
            return ydal.Select(ObjQueryModel);
        }

        public int Insert(OAPersonStudyModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Transaction);
            return ydal.Insert(ObjModel);
        }

        public int Update(OAPersonStudyModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonStudyDAL ydal = new OAPersonStudyDAL(Transaction);
            return ydal.Update(ObjModel);
        }
    }
}

