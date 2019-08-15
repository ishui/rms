namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class DeskTopTypeBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Transaction);
            return edal.Delete(Code);
        }

        public DeskTopTypeModel GetModel(int Code, SqlConnection Connection)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Connection);
            return edal.GetModel(Code);
        }

        public DeskTopTypeModel GetModel(int Code, SqlTransaction Transaction)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<DeskTopTypeModel> GetModels(SqlConnection Connection)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Connection);
            return edal.Select();
        }

        public List<DeskTopTypeModel> GetModels(SqlTransaction Transaction)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Transaction);
            return edal.Select();
        }

        public List<DeskTopTypeModel> GetModels(DeskTopTypeQueryModel ObjQueryModel, SqlConnection Connection)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<DeskTopTypeModel> GetModels(DeskTopTypeQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(DeskTopTypeModel ObjModel, SqlTransaction Transaction)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(DeskTopTypeModel ObjModel, SqlTransaction Transaction)
        {
            DeskTopTypeDAL edal = new DeskTopTypeDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

