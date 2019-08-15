namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_PurchasePriceDetailBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Transaction);
            return ldal.Delete(Code);
        }

        public TC_OA_PurchasePriceDetailModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Connection);
            return ldal.GetModel(Code);
        }

        public TC_OA_PurchasePriceDetailModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<TC_OA_PurchasePriceDetailModel> GetModels(SqlConnection Connection)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Connection);
            return ldal.Select();
        }

        public List<TC_OA_PurchasePriceDetailModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Transaction);
            return ldal.Select();
        }

        public List<TC_OA_PurchasePriceDetailModel> GetModels(TC_OA_PurchasePriceDetailQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<TC_OA_PurchasePriceDetailModel> GetModels(TC_OA_PurchasePriceDetailQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_PurchasePriceDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int Update(TC_OA_PurchasePriceDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDetailDAL ldal = new TC_OA_PurchasePriceDetailDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

