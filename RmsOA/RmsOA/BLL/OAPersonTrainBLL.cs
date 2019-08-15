namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class OAPersonTrainBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Transaction);
            return ndal.Delete(Code);
        }

        public OAPersonTrainModel GetModel(int Code, SqlConnection Connection)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Connection);
            return ndal.GetModel(Code);
        }

        public OAPersonTrainModel GetModel(int Code, SqlTransaction Transaction)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Transaction);
            return ndal.GetModel(Code);
        }

        public List<OAPersonTrainModel> GetModels(SqlConnection Connection)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Connection);
            return ndal.Select();
        }

        public List<OAPersonTrainModel> GetModels(SqlTransaction Transaction)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Transaction);
            return ndal.Select();
        }

        public List<OAPersonTrainModel> GetModels(OAPersonTrainQueryModel ObjQueryModel, SqlConnection Connection)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Connection);
            return ndal.Select(ObjQueryModel);
        }

        public List<OAPersonTrainModel> GetModels(OAPersonTrainQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Transaction);
            return ndal.Select(ObjQueryModel);
        }

        public int Insert(OAPersonTrainModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Transaction);
            return ndal.Insert(ObjModel);
        }

        public int Update(OAPersonTrainModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonTrainDAL ndal = new OAPersonTrainDAL(Transaction);
            return ndal.Update(ObjModel);
        }
    }
}

