namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class YF_AssetDrawBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Transaction);
            return wdal.Delete(Code);
        }

        public YF_AssetDrawModel GetModel(int Code, SqlConnection Connection)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Connection);
            return wdal.GetModel(Code);
        }

        public YF_AssetDrawModel GetModel(int Code, SqlTransaction Transaction)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Transaction);
            return wdal.GetModel(Code);
        }

        public List<YF_AssetDrawModel> GetModels(SqlConnection Connection)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Connection);
            return wdal.Select();
        }

        public List<YF_AssetDrawModel> GetModels(SqlTransaction Transaction)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Transaction);
            return wdal.Select();
        }

        public List<YF_AssetDrawModel> GetModels(YF_AssetDrawQueryModel ObjQueryModel, SqlConnection Connection)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Connection);
            return wdal.Select(ObjQueryModel);
        }

        public List<YF_AssetDrawModel> GetModels(YF_AssetDrawQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Transaction);
            return wdal.Select(ObjQueryModel);
        }

        public int Insert(YF_AssetDrawModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Transaction);
            return wdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetDrawModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetDrawModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetDrawModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetDrawModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetDrawModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(YF_AssetDrawModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetDrawDAL wdal = new YF_AssetDrawDAL(Transaction);
            return wdal.Update(ObjModel);
        }
    }
}

