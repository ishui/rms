namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class OAPersonContractBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Transaction);
            return tdal.Delete(Code);
        }

        public OAPersonContractModel GetModel(int Code, SqlConnection Connection)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Connection);
            return tdal.GetModel(Code);
        }

        public OAPersonContractModel GetModel(int Code, SqlTransaction Transaction)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Transaction);
            return tdal.GetModel(Code);
        }

        public List<OAPersonContractModel> GetModels(SqlConnection Connection)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Connection);
            return tdal.Select();
        }

        public List<OAPersonContractModel> GetModels(SqlTransaction Transaction)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Transaction);
            return tdal.Select();
        }

        public List<OAPersonContractModel> GetModels(OAPersonContractQueryModel ObjQueryModel, SqlConnection Connection)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Connection);
            return tdal.Select(ObjQueryModel);
        }

        public List<OAPersonContractModel> GetModels(OAPersonContractQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Transaction);
            return tdal.Select(ObjQueryModel);
        }

        public int Insert(OAPersonContractModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Transaction);
            return tdal.Insert(ObjModel);
        }

        public int Update(OAPersonContractModel ObjModel, SqlTransaction Transaction)
        {
            OAPersonContractDAL tdal = new OAPersonContractDAL(Transaction);
            return tdal.Update(ObjModel);
        }
    }
}

