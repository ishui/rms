namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TCheckPaymentSumBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Transaction);
            return mdal.Delete(Code);
        }

        public TCheckPaymentSumModel GetModel(int Code, SqlConnection Connection)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Connection);
            return mdal.GetModel(Code);
        }

        public TCheckPaymentSumModel GetModel(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Transaction);
            return mdal.GetModel(Code);
        }

        public List<TCheckPaymentSumModel> GetModels(SqlConnection Connection)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Connection);
            return mdal.Select();
        }

        public List<TCheckPaymentSumModel> GetModels(SqlTransaction Transaction)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Transaction);
            return mdal.Select();
        }

        public List<TCheckPaymentSumModel> GetModels(TCheckPaymentSumQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Connection);
            return mdal.Select(ObjQueryModel);
        }

        public List<TCheckPaymentSumModel> GetModels(TCheckPaymentSumQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Transaction);
            return mdal.Select(ObjQueryModel);
        }

        public int Insert(TCheckPaymentSumModel ObjModel, SqlTransaction Transaction)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Transaction);
            return mdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "2";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentSumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "3";
            return this.Update(objModel, Transaction);
        }

        public int Update(TCheckPaymentSumModel ObjModel, SqlTransaction Transaction)
        {
            TCheckPaymentSumDAL mdal = new TCheckPaymentSumDAL(Transaction);
            return mdal.Update(ObjModel);
        }
    }
}

