namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class YF_AssetMainApplyBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Transaction);
            return ydal.Delete(Code);
        }

        public YF_AssetMainApplyModel GetModel(int Code, SqlConnection Connection)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Connection);
            return ydal.GetModel(Code);
        }

        public YF_AssetMainApplyModel GetModel(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Transaction);
            return ydal.GetModel(Code);
        }

        public List<YF_AssetMainApplyModel> GetModels(SqlConnection Connection)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Connection);
            return ydal.Select();
        }

        public List<YF_AssetMainApplyModel> GetModels(SqlTransaction Transaction)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Transaction);
            return ydal.Select();
        }

        public List<YF_AssetMainApplyModel> GetModels(YF_AssetMainApplyQueryModel ObjQueryModel, SqlConnection Connection)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Connection);
            return ydal.Select(ObjQueryModel);
        }

        public List<YF_AssetMainApplyModel> GetModels(YF_AssetMainApplyQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Transaction);
            return ydal.Select(ObjQueryModel);
        }

        public int Insert(YF_AssetMainApplyModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Transaction);
            return ydal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetMainApplyModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(YF_AssetMainApplyModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetMainApplyDAL ydal = new YF_AssetMainApplyDAL(Transaction);
            return ydal.Update(ObjModel);
        }
    }
}

