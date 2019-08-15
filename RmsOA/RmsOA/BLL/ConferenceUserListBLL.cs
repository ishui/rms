namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class ConferenceUserListBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Transaction);
            return tdal.Delete(Code);
        }

        public ConferenceUserListModel GetModel(int Code, SqlConnection Connection)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Connection);
            return tdal.GetModel(Code);
        }

        public ConferenceUserListModel GetModel(int Code, SqlTransaction Transaction)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Transaction);
            return tdal.GetModel(Code);
        }

        public List<ConferenceUserListModel> GetModels(SqlConnection Connection)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Connection);
            return tdal.Select();
        }

        public List<ConferenceUserListModel> GetModels(SqlTransaction Transaction)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Transaction);
            return tdal.Select();
        }

        public List<ConferenceUserListModel> GetModels(ConferenceUserListQueryModel ObjQueryModel, SqlConnection Connection)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Connection);
            return tdal.Select(ObjQueryModel);
        }

        public List<ConferenceUserListModel> GetModels(ConferenceUserListQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Transaction);
            return tdal.Select(ObjQueryModel);
        }

        public int Insert(ConferenceUserListModel ObjModel, SqlTransaction Transaction)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Transaction);
            return tdal.Insert(ObjModel);
        }

        public int Update(ConferenceUserListModel ObjModel, SqlTransaction Transaction)
        {
            ConferenceUserListDAL tdal = new ConferenceUserListDAL(Transaction);
            return tdal.Update(ObjModel);
        }
    }
}

