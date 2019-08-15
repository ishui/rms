namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_LingYongMoneyDetailBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Transaction);
            return ldal.Delete(Code);
        }

        public TC_OA_LingYongMoneyDetailModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Connection);
            return ldal.GetModel(Code);
        }

        public TC_OA_LingYongMoneyDetailModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<TC_OA_LingYongMoneyDetailModel> GetModels(SqlConnection Connection)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Connection);
            return ldal.Select();
        }

        public List<TC_OA_LingYongMoneyDetailModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Transaction);
            return ldal.Select();
        }

        public List<TC_OA_LingYongMoneyDetailModel> GetModels(TC_OA_LingYongMoneyDetailQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<TC_OA_LingYongMoneyDetailModel> GetModels(TC_OA_LingYongMoneyDetailQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_LingYongMoneyDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int Update(TC_OA_LingYongMoneyDetailModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMoneyDetailDAL ldal = new TC_OA_LingYongMoneyDetailDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

