namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_SmallGoodsDetailBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Transaction);
            return ldal.Delete(Code);
        }

        public TC_OA_SmallGoodsDetailModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Connection);
            return ldal.GetModel(Code);
        }

        public TC_OA_SmallGoodsDetailModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<TC_OA_SmallGoodsDetailModel> GetModels(SqlConnection Connection)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Connection);
            return ldal.Select();
        }

        public List<TC_OA_SmallGoodsDetailModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Transaction);
            return ldal.Select();
        }

        public List<TC_OA_SmallGoodsDetailModel> GetModels(TC_OA_SmallGoodsDetailQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<TC_OA_SmallGoodsDetailModel> GetModels(TC_OA_SmallGoodsDetailQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_SmallGoodsDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int Update(TC_OA_SmallGoodsDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDetailDAL ldal = new TC_OA_SmallGoodsDetailDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

