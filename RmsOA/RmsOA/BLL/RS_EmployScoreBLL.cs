namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class RS_EmployScoreBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Transaction);
            return edal.Delete(Code);
        }

        public RS_EmployScoreModel GetModel(int Code, SqlConnection Connection)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Connection);
            return edal.GetModel(Code);
        }

        public RS_EmployScoreModel GetModel(int Code, SqlTransaction Transaction)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<RS_EmployScoreModel> GetModels(SqlConnection Connection)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Connection);
            return edal.Select();
        }

        public List<RS_EmployScoreModel> GetModels(SqlTransaction Transaction)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Transaction);
            return edal.Select();
        }

        public List<RS_EmployScoreModel> GetModels(RS_EmployScoreQueryModel ObjQueryModel, SqlConnection Connection)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<RS_EmployScoreModel> GetModels(RS_EmployScoreQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(RS_EmployScoreModel ObjModel, SqlTransaction Transaction)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(RS_EmployScoreModel ObjModel, SqlTransaction Transaction)
        {
            RS_EmployScoreDAL edal = new RS_EmployScoreDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

