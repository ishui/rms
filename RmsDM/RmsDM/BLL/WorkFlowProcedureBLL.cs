namespace RmsDM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.DAL;
    using RmsDM.MODEL;

    public class WorkFlowProcedureBLL
    {
        public WorkFlowProcedureModel GetModel(int Code, SqlConnection Connection)
        {
            WorkFlowProcedureDAL edal = new WorkFlowProcedureDAL(Connection);
            return edal.GetModel(Code);
        }

        public WorkFlowProcedureModel GetModel(int Code, SqlTransaction Transaction)
        {
            WorkFlowProcedureDAL edal = new WorkFlowProcedureDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<WorkFlowProcedureModel> GetModels(SqlConnection Connection)
        {
            WorkFlowProcedureDAL edal = new WorkFlowProcedureDAL(Connection);
            return edal.Select();
        }

        public List<WorkFlowProcedureModel> GetModels(SqlTransaction Transaction)
        {
            WorkFlowProcedureDAL edal = new WorkFlowProcedureDAL(Transaction);
            return edal.Select();
        }

        public List<WorkFlowProcedureModel> GetModels(WorkFlowProcedureQueryModel ObjQueryModel, SqlConnection Connection)
        {
            WorkFlowProcedureDAL edal = new WorkFlowProcedureDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<WorkFlowProcedureModel> GetModels(WorkFlowProcedureQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            WorkFlowProcedureDAL edal = new WorkFlowProcedureDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }
    }
}

