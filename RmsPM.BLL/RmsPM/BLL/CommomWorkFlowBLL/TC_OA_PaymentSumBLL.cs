namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_PaymentSumBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Transaction);
            return mdal.Delete(Code);
        }

        public TC_OA_PaymentSumModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Connection);
            return mdal.GetModel(Code);
        }

        public TC_OA_PaymentSumModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Transaction);
            return mdal.GetModel(Code);
        }

        public List<TC_OA_PaymentSumModel> GetModels(SqlConnection Connection)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Connection);
            return mdal.Select();
        }

        public List<TC_OA_PaymentSumModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Transaction);
            return mdal.Select();
        }

        public List<TC_OA_PaymentSumModel> GetModels(TC_OA_PaymentSumQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Connection);
            return mdal.Select(ObjQueryModel);
        }

        public List<TC_OA_PaymentSumModel> GetModels(TC_OA_PaymentSumQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Transaction);
            return mdal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_PaymentSumModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Transaction);
            return mdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_PaymentSumModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_PaymentSumDAL mdal = new TC_OA_PaymentSumDAL(Transaction);
            return mdal.Update(ObjModel);
        }
    }
}

