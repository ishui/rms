namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class DesignChangeBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Transaction);
            return edal.Delete(Code);
        }

        public DesignChangeModel GetModel(int Code, SqlConnection Connection)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Connection);
            return edal.GetModel(Code);
        }

        public DesignChangeModel GetModel(int Code, SqlTransaction Transaction)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<DesignChangeModel> GetModels(SqlConnection Connection)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Connection);
            return edal.Select();
        }

        public List<DesignChangeModel> GetModels(SqlTransaction Transaction)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Transaction);
            return edal.Select();
        }

        public List<DesignChangeModel> GetModels(DesignChangeQueryModel ObjQueryModel, SqlConnection Connection)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<DesignChangeModel> GetModels(DesignChangeQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(DesignChangeModel ObjModel, SqlTransaction Transaction)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Transaction);
            ObjModel.Flag = "0";
            ObjModel.State = "0";
            ObjModel.ChangeMoney = "";
            ObjModel.TotalMoney = 0M;
            return edal.Insert(ObjModel);
        }

        public int Update(DesignChangeModel ObjModel, SqlTransaction Transaction)
        {
            DesignChangeDAL edal = new DesignChangeDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

