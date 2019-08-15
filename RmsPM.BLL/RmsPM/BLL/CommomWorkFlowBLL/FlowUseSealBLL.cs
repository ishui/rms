namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class FlowUseSealBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Transaction);
            return ldal.Delete(Code);
        }

        public FlowUseSealModel GetModel(int Code, SqlConnection Connection)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Connection);
            return ldal.GetModel(Code);
        }

        public FlowUseSealModel GetModel(int Code, SqlTransaction Transaction)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<FlowUseSealModel> GetModels(SqlConnection Connection)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Connection);
            return ldal.Select();
        }

        public List<FlowUseSealModel> GetModels(SqlTransaction Transaction)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Transaction);
            return ldal.Select();
        }

        public List<FlowUseSealModel> GetModels(FlowUseSealQueryModel ObjQueryModel, SqlConnection Connection)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<FlowUseSealModel> GetModels(FlowUseSealQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(FlowUseSealModel ObjModel, SqlTransaction Transaction)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Transaction);
            ObjModel.Auditing = "1";
            return ldal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            FlowUseSealModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "2";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            FlowUseSealModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            FlowUseSealModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            FlowUseSealModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = "3";
            return this.Update(objModel, Transaction);
        }

        public int Update(FlowUseSealModel ObjModel, SqlTransaction Transaction)
        {
            FlowUseSealDAL ldal = new FlowUseSealDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

