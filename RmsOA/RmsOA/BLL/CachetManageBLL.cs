namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class CachetManageBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            CachetManageDAL edal = new CachetManageDAL(Transaction);
            return edal.Delete(Code);
        }

        public CachetManageModel GetModel(int Code, SqlConnection Connection)
        {
            CachetManageDAL edal = new CachetManageDAL(Connection);
            return edal.GetModel(Code);
        }

        public CachetManageModel GetModel(int Code, SqlTransaction Transaction)
        {
            CachetManageDAL edal = new CachetManageDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<CachetManageModel> GetModels(SqlConnection Connection)
        {
            CachetManageDAL edal = new CachetManageDAL(Connection);
            return edal.Select();
        }

        public List<CachetManageModel> GetModels(SqlTransaction Transaction)
        {
            CachetManageDAL edal = new CachetManageDAL(Transaction);
            return edal.Select();
        }

        public List<CachetManageModel> GetModels(CachetManageQueryModel ObjQueryModel, SqlConnection Connection)
        {
            CachetManageDAL edal = new CachetManageDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<CachetManageModel> GetModels(CachetManageQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            CachetManageDAL edal = new CachetManageDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(CachetManageModel ObjModel, SqlTransaction Transaction)
        {
            CachetManageDAL edal = new CachetManageDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(CachetManageModel ObjModel, SqlTransaction Transaction)
        {
            CachetManageDAL edal = new CachetManageDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

