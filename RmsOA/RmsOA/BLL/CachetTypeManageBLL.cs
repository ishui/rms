namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class CachetTypeManageBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Transaction);
            return edal.Delete(Code);
        }

        public CachetTypeManageModel GetModel(int Code, SqlConnection Connection)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Connection);
            return edal.GetModel(Code);
        }

        public CachetTypeManageModel GetModel(int Code, SqlTransaction Transaction)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<CachetTypeManageModel> GetModels(SqlConnection Connection)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Connection);
            return edal.Select();
        }

        public List<CachetTypeManageModel> GetModels(SqlTransaction Transaction)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Transaction);
            return edal.Select();
        }

        public List<CachetTypeManageModel> GetModels(CachetTypeManageQueryModel ObjQueryModel, SqlConnection Connection)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<CachetTypeManageModel> GetModels(CachetTypeManageQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(CachetTypeManageModel ObjModel, SqlTransaction Transaction)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(CachetTypeManageModel ObjModel, SqlTransaction Transaction)
        {
            CachetTypeManageDAL edal = new CachetTypeManageDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

