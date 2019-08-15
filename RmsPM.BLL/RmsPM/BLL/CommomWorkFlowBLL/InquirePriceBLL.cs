namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class InquirePriceBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Transaction);
            return edal.Delete(Code);
        }

        public InquirePriceModel GetModel(int Code, SqlConnection Connection)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Connection);
            return edal.GetModel(Code);
        }

        public InquirePriceModel GetModel(int Code, SqlTransaction Transaction)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<InquirePriceModel> GetModels(SqlConnection Connection)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Connection);
            return edal.Select();
        }

        public List<InquirePriceModel> GetModels(SqlTransaction Transaction)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Transaction);
            return edal.Select();
        }

        public List<InquirePriceModel> GetModels(InquirePriceQueryModel ObjQueryModel, SqlConnection Connection)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<InquirePriceModel> GetModels(InquirePriceQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(InquirePriceModel ObjModel, SqlTransaction Transaction)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            InquirePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Aduiting = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            InquirePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Aduiting = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            InquirePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Aduiting = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            InquirePriceModel objModel = this.GetModel(Code, Transaction);
            objModel.Aduiting = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(InquirePriceModel ObjModel, SqlTransaction Transaction)
        {
            InquirePriceDAL edal = new InquirePriceDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

