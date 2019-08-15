namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class RS_ScoreManageBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Transaction);
            return edal.Delete(Code);
        }

        public RS_ScoreManageModel GetModel(int Code, SqlConnection Connection)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Connection);
            return edal.GetModel(Code);
        }

        public RS_ScoreManageModel GetModel(int Code, SqlTransaction Transaction)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<RS_ScoreManageModel> GetModels(SqlConnection Connection)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Connection);
            return edal.Select();
        }

        public List<RS_ScoreManageModel> GetModels(SqlTransaction Transaction)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Transaction);
            return edal.Select();
        }

        public List<RS_ScoreManageModel> GetModels(RS_ScoreManageQueryModel ObjQueryModel, SqlConnection Connection)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<RS_ScoreManageModel> GetModels(RS_ScoreManageQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(RS_ScoreManageModel ObjModel, SqlTransaction Transaction)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            RS_ScoreManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            RS_ScoreManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            RS_ScoreManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            RS_ScoreManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            RS_ScoreManageModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(RS_ScoreManageModel ObjModel, SqlTransaction Transaction)
        {
            RS_ScoreManageDAL edal = new RS_ScoreManageDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

