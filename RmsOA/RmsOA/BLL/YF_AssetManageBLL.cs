namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class YF_AssetManageBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Transaction);
            return edal.Delete(Code);
        }

        public YF_AssetManageModel GetModel(int Code, SqlConnection Connection)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Connection);
            return edal.GetModel(Code);
        }

        public YF_AssetManageModel GetModel(int Code, SqlTransaction Transaction)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<YF_AssetManageModel> GetModels(SqlConnection Connection)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Connection);
            return edal.Select();
        }

        public List<YF_AssetManageModel> GetModels(SqlTransaction Transaction)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Transaction);
            return edal.Select();
        }

        public List<YF_AssetManageModel> GetModels(YF_AssetManageQueryModel ObjQueryModel, SqlConnection Connection)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<YF_AssetManageModel> GetModels(YF_AssetManageQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(YF_AssetManageModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_AssetManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(YF_AssetManageModel ObjModel, SqlTransaction Transaction)
        {
            YF_AssetManageDAL edal = new YF_AssetManageDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

