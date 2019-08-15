namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TY_ReferendumReportBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Transaction);
            return tdal.Delete(Code);
        }

        public TY_ReferendumReportModel GetModel(int Code, SqlConnection Connection)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Connection);
            return tdal.GetModel(Code);
        }

        public TY_ReferendumReportModel GetModel(int Code, SqlTransaction Transaction)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Transaction);
            return tdal.GetModel(Code);
        }

        public List<TY_ReferendumReportModel> GetModels(SqlConnection Connection)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Connection);
            return tdal.Select();
        }

        public List<TY_ReferendumReportModel> GetModels(SqlTransaction Transaction)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Transaction);
            return tdal.Select();
        }

        public List<TY_ReferendumReportModel> GetModels(TY_ReferendumReportQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Connection);
            return tdal.Select(ObjQueryModel);
        }

        public List<TY_ReferendumReportModel> GetModels(TY_ReferendumReportQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Transaction);
            return tdal.Select(ObjQueryModel);
        }

        public int Insert(TY_ReferendumReportModel ObjModel, SqlTransaction Transaction)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Transaction);
            return tdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TY_ReferendumReportModel objModel = this.GetModel(Code, Transaction);
            objModel.Audit = "2";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TY_ReferendumReportModel objModel = this.GetModel(Code, Transaction);
            objModel.Audit = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TY_ReferendumReportModel objModel = this.GetModel(Code, Transaction);
            objModel.Audit = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TY_ReferendumReportModel objModel = this.GetModel(Code, Transaction);
            objModel.Audit = "3";
            return this.Update(objModel, Transaction);
        }

        public int Update(TY_ReferendumReportModel ObjModel, SqlTransaction Transaction)
        {
            TY_ReferendumReportDAL tdal = new TY_ReferendumReportDAL(Transaction);
            return tdal.Update(ObjModel);
        }
    }
}

