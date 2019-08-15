namespace RmsDM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.BLL;
    using RmsDM.MODEL;

    public class WorkFlowProcedureBFL
    {
        public WorkFlowProcedureModel GetWorkFlowProcedure(int Code)
        {
            WorkFlowProcedureModel model = new WorkFlowProcedureModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new WorkFlowProcedureBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<WorkFlowProcedureModel> GetWorkFlowProcedureList(WorkFlowProcedureQueryModel QueryModel)
        {
            List<WorkFlowProcedureModel> models = new List<WorkFlowProcedureModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new WorkFlowProcedureQueryModel();
                    }
                    models = new WorkFlowProcedureBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<WorkFlowProcedureModel> GetWorkFlowProcedureList(string SortColumns, int StartRecord, int MaxRecords, string ProcedureCodeEqual, string ProcedureNameEqual, string DescriptionEqual, string ApplicationPathEqual, string ApplicationInfoPathEqual, int TypeEqual, string RemarkEqual, string SysTypeEqual, string CreatorEqual, decimal VersionNumberEqual, string ProjectCodeEqual, string CreateUserEqual, DateTime CreateDateEqual, string ModifyUserEqual, DateTime ModifyDateEqual, int ActivityEqual, string VersionDescriptionEqual, string ProcedureRemarkEqual)
        {
            List<WorkFlowProcedureModel> models = new List<WorkFlowProcedureModel>();
            WorkFlowProcedureQueryModel objQueryModel = new WorkFlowProcedureQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.ProcedureCodeEqual = ProcedureCodeEqual;
            objQueryModel.ProcedureNameEqual = ProcedureNameEqual;
            objQueryModel.DescriptionEqual = DescriptionEqual;
            objQueryModel.ApplicationPathEqual = ApplicationPathEqual;
            objQueryModel.ApplicationInfoPathEqual = ApplicationInfoPathEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.RemarkEqual = RemarkEqual;
            objQueryModel.SysTypeEqual = SysTypeEqual;
            objQueryModel.CreatorEqual = CreatorEqual;
            objQueryModel.VersionNumberEqual = VersionNumberEqual;
            objQueryModel.ProjectCodeEqual = ProjectCodeEqual;
            objQueryModel.CreateUserEqual = CreateUserEqual;
            objQueryModel.CreateDateEqual = CreateDateEqual;
            objQueryModel.ModifyUserEqual = ModifyUserEqual;
            objQueryModel.ModifyDateEqual = ModifyDateEqual;
            objQueryModel.ActivityEqual = ActivityEqual;
            objQueryModel.VersionDescriptionEqual = VersionDescriptionEqual;
            objQueryModel.ProcedureRemarkEqual = ProcedureRemarkEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new WorkFlowProcedureBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<WorkFlowProcedureModel> GetWorkFlowProcedureListOne(int Code)
        {
            List<WorkFlowProcedureModel> list = new List<WorkFlowProcedureModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    WorkFlowProcedureBLL ebll = new WorkFlowProcedureBLL();
                    list.Add(ebll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }
    }
}

