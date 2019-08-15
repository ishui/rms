namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TCheckPaymentBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Transaction);
            return tdal.Delete(Code);
        }

        public TCheckPaymentModel GetModel(int Code, SqlConnection Connection)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Connection);
            return tdal.GetModel(Code);
        }

        public TCheckPaymentModel GetModel(int Code, SqlTransaction Transaction)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Transaction);
            return tdal.GetModel(Code);
        }

        public List<TCheckPaymentModel> GetModels(SqlConnection Connection)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Connection);
            return tdal.Select();
        }

        public List<TCheckPaymentModel> GetModels(SqlTransaction Transaction)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Transaction);
            return tdal.Select();
        }

        public List<TCheckPaymentModel> GetModels(TCheckPaymentQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Connection);
            return tdal.Select(ObjQueryModel);
        }

        public List<TCheckPaymentModel> GetModels(TCheckPaymentQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Transaction);
            return tdal.Select(ObjQueryModel);
        }

        public int Update(TCheckPaymentModel ObjModel, SqlTransaction Transaction)
        {
            TCheckPaymentDAL tdal = new TCheckPaymentDAL(Transaction);
            return tdal.Update(ObjModel);
        }
    }
}

