namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class UserHelpUserBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Transaction);
            return rdal.Delete(Code);
        }

        public UserHelpUserModel GetModel(int Code, SqlConnection Connection)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Connection);
            return rdal.GetModel(Code);
        }

        public UserHelpUserModel GetModel(int Code, SqlTransaction Transaction)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<UserHelpUserModel> GetModels(SqlConnection Connection)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Connection);
            return rdal.Select();
        }

        public List<UserHelpUserModel> GetModels(SqlTransaction Transaction)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Transaction);
            return rdal.Select();
        }

        public List<UserHelpUserModel> GetModels(UserHelpUserQueryModel ObjQueryModel, SqlConnection Connection)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<UserHelpUserModel> GetModels(UserHelpUserQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(UserHelpUserModel ObjModel, SqlTransaction Transaction)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int Update(UserHelpUserModel ObjModel, SqlTransaction Transaction)
        {
            UserHelpUserDAL rdal = new UserHelpUserDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

