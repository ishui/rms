namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_GoodsBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Transaction);
            return sdal.Delete(Code);
        }

        public GK_OA_GoodsModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Connection);
            return sdal.GetModel(Code);
        }

        public GK_OA_GoodsModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Transaction);
            return sdal.GetModel(Code);
        }

        public List<GK_OA_GoodsModel> GetModels(SqlConnection Connection)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Connection);
            return sdal.Select();
        }

        public List<GK_OA_GoodsModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Transaction);
            return sdal.Select();
        }

        public List<GK_OA_GoodsModel> GetModels(GK_OA_GoodsQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Connection);
            return sdal.Select(ObjQueryModel);
        }

        public List<GK_OA_GoodsModel> GetModels(GK_OA_GoodsQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Transaction);
            return sdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_GoodsModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Transaction);
            return sdal.Insert(ObjModel);
        }

        public int Update(GK_OA_GoodsModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_GoodsDAL sdal = new GK_OA_GoodsDAL(Transaction);
            return sdal.Update(ObjModel);
        }
    }
}

