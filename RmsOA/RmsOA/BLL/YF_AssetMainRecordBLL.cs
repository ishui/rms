namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class YF_AssetMainRecordBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Transaction);
            return ddal.Delete(Code);
        }

        public YF_AssetMainRecordModel GetModel(int Code, SqlConnection Connection)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Connection);
            return ddal.GetModel(Code);
        }

        public YF_AssetMainRecordModel GetModel(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Transaction);
            return ddal.GetModel(Code);
        }

        public List<YF_AssetMainRecordModel> GetModels(SqlConnection Connection)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Connection);
            return ddal.Select();
        }

        public List<YF_AssetMainRecordModel> GetModels(SqlTransaction Transaction)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Transaction);
            return ddal.Select();
        }

        public List<YF_AssetMainRecordModel> GetModels(YF_AssetMainRecordQueryModel ObjQueryModel, SqlConnection Connection)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Connection);
            return ddal.Select(ObjQueryModel);
        }

        public List<YF_AssetMainRecordModel> GetModels(YF_AssetMainRecordQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Transaction);
            return ddal.Select(ObjQueryModel);
        }

        public int Insert(YF_AssetMainRecordModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Transaction);
            return ddal.Insert(ObjModel);
        }

        public int Update(YF_AssetMainRecordModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetMainRecordDAL ddal = new YF_AssetMainRecordDAL(Transaction);
            return ddal.Update(ObjModel);
        }
    }
}

