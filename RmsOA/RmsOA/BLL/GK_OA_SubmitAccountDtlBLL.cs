namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_SubmitAccountDtlBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Transaction);
            return ldal.Delete(Code);
        }

        public GK_OA_SubmitAccountDtlModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Connection);
            return ldal.GetModel(Code);
        }

        public GK_OA_SubmitAccountDtlModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<GK_OA_SubmitAccountDtlModel> GetModels(SqlConnection Connection)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Connection);
            return ldal.Select();
        }

        public List<GK_OA_SubmitAccountDtlModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Transaction);
            return ldal.Select();
        }

        public List<GK_OA_SubmitAccountDtlModel> GetModels(GK_OA_SubmitAccountDtlQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<GK_OA_SubmitAccountDtlModel> GetModels(GK_OA_SubmitAccountDtlQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_SubmitAccountDtlModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int Update(GK_OA_SubmitAccountDtlModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_SubmitAccountDtlDAL ldal = new GK_OA_SubmitAccountDtlDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

