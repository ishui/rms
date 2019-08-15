namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_PurchasePriceBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Transaction);
            return edal.Delete(Code);
        }

        public TC_OA_PurchasePriceModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Connection);
            return edal.GetModel(Code);
        }

        public TC_OA_PurchasePriceModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<TC_OA_PurchasePriceModel> GetModels(SqlConnection Connection)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Connection);
            return edal.Select();
        }

        public List<TC_OA_PurchasePriceModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Transaction);
            return edal.Select();
        }

        public List<TC_OA_PurchasePriceModel> GetModels(TC_OA_PurchasePriceQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<TC_OA_PurchasePriceModel> GetModels(TC_OA_PurchasePriceQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_PurchasePriceModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_PurchasePriceModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_PurchasePriceDAL edal = new TC_OA_PurchasePriceDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

