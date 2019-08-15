namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class OAPersonHomeBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Transaction);
            return edal.Delete(Code);
        }

        public OAPersonHomeModel GetModel(int Code, SqlConnection Connection)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Connection);
            return edal.GetModel(Code);
        }

        public OAPersonHomeModel GetModel(int Code, SqlTransaction Transaction)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<OAPersonHomeModel> GetModels(SqlConnection Connection)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Connection);
            return edal.Select();
        }

        public List<OAPersonHomeModel> GetModels(SqlTransaction Transaction)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Transaction);
            return edal.Select();
        }

        public List<OAPersonHomeModel> GetModels(OAPersonHomeQueryModel ObjQueryModel, SqlConnection Connection)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<OAPersonHomeModel> GetModels(OAPersonHomeQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(OAPersonHomeModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(OAPersonHomeModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonHomeDAL edal = new OAPersonHomeDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

