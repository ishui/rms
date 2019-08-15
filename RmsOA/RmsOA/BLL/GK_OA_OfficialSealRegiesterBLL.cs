namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_OfficialSealRegiesterBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Transaction);
            return rdal.Delete(Code);
        }

        public GK_OA_OfficialSealRegiesterModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Connection);
            return rdal.GetModel(Code);
        }

        public GK_OA_OfficialSealRegiesterModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Transaction);
            return rdal.GetModel(Code);
        }

        public List<GK_OA_OfficialSealRegiesterModel> GetModels(SqlConnection Connection)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Connection);
            return rdal.Select();
        }

        public List<GK_OA_OfficialSealRegiesterModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Transaction);
            return rdal.Select();
        }

        public List<GK_OA_OfficialSealRegiesterModel> GetModels(GK_OA_OfficialSealRegiesterQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Connection);
            return rdal.Select(ObjQueryModel);
        }

        public List<GK_OA_OfficialSealRegiesterModel> GetModels(GK_OA_OfficialSealRegiesterQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Transaction);
            return rdal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_OfficialSealRegiesterModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Transaction);
            return rdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_OfficialSealRegiesterModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OfficialSealRegiesterDAL rdal = new GK_OA_OfficialSealRegiesterDAL(Transaction);
            return rdal.Update(ObjModel);
        }
    }
}

