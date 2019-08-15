namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_WorkLogBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Transaction);
            return gdal.Delete(Code);
        }

        public GK_OA_WorkLogModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Connection);
            return gdal.GetModel(Code);
        }

        public GK_OA_WorkLogModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Transaction);
            return gdal.GetModel(Code);
        }

        public List<GK_OA_WorkLogModel> GetModels(SqlConnection Connection)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Connection);
            return gdal.Select();
        }

        public List<GK_OA_WorkLogModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Transaction);
            return gdal.Select();
        }

        public List<GK_OA_WorkLogModel> GetModels(GK_OA_WorkLogQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Connection);
            return gdal.Select(ObjQueryModel);
        }

        public List<GK_OA_WorkLogModel> GetModels(GK_OA_WorkLogQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Transaction);
            return gdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_WorkLogModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Transaction);
            return gdal.Insert(ObjModel);
        }

        public int Update(GK_OA_WorkLogModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_WorkLogDAL gdal = new GK_OA_WorkLogDAL(Transaction);
            return gdal.Update(ObjModel);
        }
    }
}

