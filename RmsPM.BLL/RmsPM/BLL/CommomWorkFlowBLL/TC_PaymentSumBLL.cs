namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_PaymentSumBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Transaction);
            return mdal.Delete(Code);
        }

        public TC_PaymentSumModel GetModel(int Code, SqlConnection Connection)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Connection);
            return mdal.GetModel(Code);
        }

        public TC_PaymentSumModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Transaction);
            return mdal.GetModel(Code);
        }

        public List<TC_PaymentSumModel> GetModels(SqlConnection Connection)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Connection);
            return mdal.Select();
        }

        public List<TC_PaymentSumModel> GetModels(SqlTransaction Transaction)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Transaction);
            return mdal.Select();
        }

        public List<TC_PaymentSumModel> GetModels(TC_PaymentSumQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Connection);
            return mdal.Select(ObjQueryModel);
        }

        public List<TC_PaymentSumModel> GetModels(TC_PaymentSumQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Transaction);
            return mdal.Select(ObjQueryModel);
        }

        public int Insert(TC_PaymentSumModel ObjModel, SqlTransaction Transaction)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Transaction);
            return mdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            objModel.AuditDateTime = DateTime.Now;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_PaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            objModel.AuditDateTime = DateTime.Now;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_PaymentSumModel ObjModel, SqlTransaction Transaction)
        {
            TC_PaymentSumDAL mdal = new TC_PaymentSumDAL(Transaction);
            return mdal.Update(ObjModel);
        }
    }
}

