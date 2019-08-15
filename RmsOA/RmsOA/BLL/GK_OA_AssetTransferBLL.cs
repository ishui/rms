namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_AssetTransferBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Transaction);
            return rdal.Delete(Code);
        }

        public GK_OA_AssetTransferModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Connection);
            return rdal.GetModel(Code);
        }

        public GK_OA_AssetTransferModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<GK_OA_AssetTransferModel> GetModels(SqlConnection Connection)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Connection);
            return rdal.Select();
        }

        public List<GK_OA_AssetTransferModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Transaction);
            return rdal.Select();
        }

        public List<GK_OA_AssetTransferModel> GetModels(GK_OA_AssetTransferQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<GK_OA_AssetTransferModel> GetModels(GK_OA_AssetTransferQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_AssetTransferModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_AssetTransferModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_AssetTransferDAL rdal = new GK_OA_AssetTransferDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

