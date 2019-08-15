namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_BigGoodsDetailBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Transaction);
            return ldal.Delete(Code);
        }

        public TC_OA_BigGoodsDetailModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Connection);
            return ldal.GetModel(Code);
        }

        public TC_OA_BigGoodsDetailModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<TC_OA_BigGoodsDetailModel> GetModels(SqlConnection Connection)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Connection);
            return ldal.Select();
        }

        public List<TC_OA_BigGoodsDetailModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Transaction);
            return ldal.Select();
        }

        public List<TC_OA_BigGoodsDetailModel> GetModels(TC_OA_BigGoodsDetailQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<TC_OA_BigGoodsDetailModel> GetModels(TC_OA_BigGoodsDetailQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_BigGoodsDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int Update(TC_OA_BigGoodsDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_BigGoodsDetailDAL ldal = new TC_OA_BigGoodsDetailDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

