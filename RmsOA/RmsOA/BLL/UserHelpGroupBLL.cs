namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class UserHelpGroupBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Transaction);
            return pdal.Delete(Code);
        }

        public UserHelpGroupModel GetModel(int Code, SqlConnection Connection)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Connection);
            return pdal.GetModel(Code);
        }

        public UserHelpGroupModel GetModel(int Code, SqlTransaction Transaction)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Transaction);
            return pdal.GetModel(Code);
        }

        public List<UserHelpGroupModel> GetModels(SqlConnection Connection)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Connection);
            return pdal.Select();
        }

        public List<UserHelpGroupModel> GetModels(SqlTransaction Transaction)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Transaction);
            return pdal.Select();
        }

        public List<UserHelpGroupModel> GetModels(UserHelpGroupQueryModel ObjQueryModel, SqlConnection Connection)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Connection);
            return pdal.Select(ObjQueryModel);
        }

        public List<UserHelpGroupModel> GetModels(UserHelpGroupQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Transaction);
            return pdal.Select(ObjQueryModel);
        }

        public int Insert(UserHelpGroupModel ObjModel, SqlTransaction Transaction)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Transaction);
            return pdal.Insert(ObjModel);
        }

        public int Update(UserHelpGroupModel ObjModel, SqlTransaction Transaction)
        {
            UserHelpGroupDAL pdal = new UserHelpGroupDAL(Transaction);
            return pdal.Update(ObjModel);
        }
    }
}

