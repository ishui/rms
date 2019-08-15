namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_MaterialTransferBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Transaction);
            return rdal.Delete(Code);
        }

        public GK_OA_MaterialTransferModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Connection);
            return rdal.GetModel(Code);
        }

        public GK_OA_MaterialTransferModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<GK_OA_MaterialTransferModel> GetModels(SqlConnection Connection)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Connection);
            return rdal.Select();
        }

        public List<GK_OA_MaterialTransferModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Transaction);
            return rdal.Select();
        }

        public List<GK_OA_MaterialTransferModel> GetModels(GK_OA_MaterialTransferQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<GK_OA_MaterialTransferModel> GetModels(GK_OA_MaterialTransferQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_MaterialTransferModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_MaterialTransferModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_MaterialTransferDAL rdal = new GK_OA_MaterialTransferDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

