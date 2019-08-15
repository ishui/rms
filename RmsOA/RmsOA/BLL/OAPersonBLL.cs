namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class OAPersonBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            OAPersonDAL ndal = new OAPersonDAL(Transaction);
            return ndal.Delete(Code);
        }

        public OAPersonModel GetModel(int Code, SqlConnection Connection)
        {
            OAPersonDAL ndal = new OAPersonDAL(Connection);
            return ndal.GetModel(Code);
        }

        public OAPersonModel GetModel(int Code, SqlTransaction Transaction)
        {
            OAPersonDAL ndal = new OAPersonDAL(Transaction);
            return ndal.GetModel(Code);
        }

        public List<OAPersonModel> GetModels(SqlConnection Connection)
        {
            OAPersonDAL ndal = new OAPersonDAL(Connection);
            return ndal.Select();
        }

        public List<OAPersonModel> GetModels(SqlTransaction Transaction)
        {
            OAPersonDAL ndal = new OAPersonDAL(Transaction);
            return ndal.Select();
        }

        public List<OAPersonModel> GetModels(OAPersonQueryModel ObjQueryModel, SqlConnection Connection)
        {
            OAPersonDAL ndal = new OAPersonDAL(Connection);
            return ndal.Select(ObjQueryModel);
        }

        public List<OAPersonModel> GetModels(OAPersonQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            OAPersonDAL ndal = new OAPersonDAL(Transaction);
            return ndal.Select(ObjQueryModel);
        }

        public int Insert(OAPersonModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonDAL ndal = new OAPersonDAL(Transaction);
            ObjModel.photosize = 0;
            ObjModel.phototype = "";
            ObjModel.Status = "";
            return ndal.Insert(ObjModel);
        }

        public int Update(OAPersonModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonDAL ndal = new OAPersonDAL(Transaction);
            return ndal.Update(ObjModel);
        }
    }
}

