namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class OAPersonRewardBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Transaction);
            return ddal.Delete(Code);
        }

        public OAPersonRewardModel GetModel(int Code, SqlConnection Connection)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Connection);
            return ddal.GetModel(Code);
        }

        public OAPersonRewardModel GetModel(int Code, SqlTransaction Transaction)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Transaction);
            return ddal.GetModel(Code);
        }

        public List<OAPersonRewardModel> GetModels(SqlConnection Connection)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Connection);
            return ddal.Select();
        }

        public List<OAPersonRewardModel> GetModels(SqlTransaction Transaction)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Transaction);
            return ddal.Select();
        }

        public List<OAPersonRewardModel> GetModels(OAPersonRewardQueryModel ObjQueryModel, SqlConnection Connection)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Connection);
            return ddal.Select(ObjQueryModel);
        }

        public List<OAPersonRewardModel> GetModels(OAPersonRewardQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Transaction);
            return ddal.Select(ObjQueryModel);
        }

        public int Insert(OAPersonRewardModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Transaction);
            return ddal.Insert(ObjModel);
        }

        public int Update(OAPersonRewardModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonRewardDAL ddal = new OAPersonRewardDAL(Transaction);
            return ddal.Update(ObjModel);
        }
    }
}

