namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class YF_AssetTransferBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Transaction);
            return rdal.Delete(Code);
        }

        public YF_AssetTransferModel GetModel(int Code, SqlConnection Connection)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Connection);
            return rdal.GetModel(Code);
        }

        public YF_AssetTransferModel GetModel(int Code, SqlTransaction Transaction)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<YF_AssetTransferModel> GetModels(SqlConnection Connection)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Connection);
            return rdal.Select();
        }

        public List<YF_AssetTransferModel> GetModels(SqlTransaction Transaction)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Transaction);
            return rdal.Select();
        }

        public List<YF_AssetTransferModel> GetModels(YF_AssetTransferQueryModel ObjQueryModel, SqlConnection Connection)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<YF_AssetTransferModel> GetModels(YF_AssetTransferQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(YF_AssetTransferModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetTransferModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(YF_AssetTransferModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetTransferDAL rdal = new YF_AssetTransferDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

