namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_FileChangeBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Transaction);
            return edal.Delete(Code);
        }

        public GK_OA_FileChangeModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Connection);
            return edal.GetModel(Code);
        }

        public GK_OA_FileChangeModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<GK_OA_FileChangeModel> GetModels(SqlConnection Connection)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Connection);
            return edal.Select();
        }

        public List<GK_OA_FileChangeModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Transaction);
            return edal.Select();
        }

        public List<GK_OA_FileChangeModel> GetModels(GK_OA_FileChangeQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<GK_OA_FileChangeModel> GetModels(GK_OA_FileChangeQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_FileChangeModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_FileChangeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_FileChangeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_FileChangeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_FileChangeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_FileChangeModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_FileChangeModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_FileChangeDAL edal = new GK_OA_FileChangeDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

