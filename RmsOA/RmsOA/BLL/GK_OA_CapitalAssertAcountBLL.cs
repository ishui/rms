namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_CapitalAssertAcountBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Transaction);
            return tdal.Delete(Code);
        }

        public GK_OA_CapitalAssertAcountModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Connection);
            return tdal.GetModel(Code);
        }

        public GK_OA_CapitalAssertAcountModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Transaction);
            return tdal.GetModel(Code);
        }

        public List<GK_OA_CapitalAssertAcountModel> GetModels(SqlConnection Connection)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Connection);
            return tdal.Select();
        }

        public List<GK_OA_CapitalAssertAcountModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Transaction);
            return tdal.Select();
        }

        public List<GK_OA_CapitalAssertAcountModel> GetModels(GK_OA_CapitalAssertAcountQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Connection);
            return tdal.Select(ObjQueryModel);
        }

        public List<GK_OA_CapitalAssertAcountModel> GetModels(GK_OA_CapitalAssertAcountQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Transaction);
            return tdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_CapitalAssertAcountModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Transaction);
            return tdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_CapitalAssertAcountModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_CapitalAssertAcountDAL tdal = new GK_OA_CapitalAssertAcountDAL(Transaction);
            return tdal.Update(ObjModel);
        }
    }
}

