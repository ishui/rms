namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class YF_UseCarFlowBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Transaction);
            return wdal.Delete(Code);
        }

        public YF_UseCarFlowModel GetModel(int Code, SqlConnection Connection)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Connection);
            return wdal.GetModel(Code);
        }

        public YF_UseCarFlowModel GetModel(int Code, SqlTransaction Transaction)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Transaction);
            return wdal.GetModel(Code);
        }

        public List<YF_UseCarFlowModel> GetModels(SqlConnection Connection)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Connection);
            return wdal.Select();
        }

        public List<YF_UseCarFlowModel> GetModels(SqlTransaction Transaction)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Transaction);
            return wdal.Select();
        }

        public List<YF_UseCarFlowModel> GetModels(YF_UseCarFlowQueryModel ObjQueryModel, SqlConnection Connection)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Connection);
            return wdal.Select(ObjQueryModel);
        }

        public List<YF_UseCarFlowModel> GetModels(YF_UseCarFlowQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Transaction);
            return wdal.Select(ObjQueryModel);
        }

        public int Insert(YF_UseCarFlowModel ObjModel, SqlTransaction Transaction)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Transaction);
            return wdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            YF_UseCarFlowModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            YF_UseCarFlowModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            YF_UseCarFlowModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_UseCarFlowModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            YF_UseCarFlowModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(YF_UseCarFlowModel ObjModel, SqlTransaction Transaction)
        {
            YF_UseCarFlowDAL wdal = new YF_UseCarFlowDAL(Transaction);
            return wdal.Update(ObjModel);
        }
    }
}

