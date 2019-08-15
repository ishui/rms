namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_ManpowerNeedBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Transaction);
            return ddal.Delete(Code);
        }

        public GK_OA_ManpowerNeedModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Connection);
            return ddal.GetModel(Code);
        }

        public GK_OA_ManpowerNeedModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Transaction);
            return ddal.GetModel(Code);
        }

        public List<GK_OA_ManpowerNeedModel> GetModels(SqlConnection Connection)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Connection);
            return ddal.Select();
        }

        public List<GK_OA_ManpowerNeedModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Transaction);
            return ddal.Select();
        }

        public List<GK_OA_ManpowerNeedModel> GetModels(GK_OA_ManpowerNeedQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Connection);
            return ddal.Select(ObjQueryModel);
        }

        public List<GK_OA_ManpowerNeedModel> GetModels(GK_OA_ManpowerNeedQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Transaction);
            return ddal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_ManpowerNeedModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Transaction);
            return ddal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_ManpowerNeedModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_ManpowerNeedDAL ddal = new GK_OA_ManpowerNeedDAL(Transaction);
            return ddal.Update(ObjModel);
        }
    }
}

