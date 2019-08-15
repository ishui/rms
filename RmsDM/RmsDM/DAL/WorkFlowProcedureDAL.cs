namespace RmsDM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsDM.MODEL;

    public class WorkFlowProcedureDAL
    {
        private SqlDataProcess _DataProcess;

        public WorkFlowProcedureDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public WorkFlowProcedureDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private WorkFlowProcedureModel _DataBind(int Code)
        {
            WorkFlowProcedureModel model = new WorkFlowProcedureModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from WorkFlowProcedure ");
            builder.Append(" where ProcedureCode=@ProcedureCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ProcedureCode", SqlDbType.Int, 4, Code);
            SqlDataReader sqlDataReader = null;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        this.Initialize(sqlDataReader, model);
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return model;
        }

        private List<WorkFlowProcedureModel> _Select(WorkFlowProcedureQueryModel qmObj)
        {
            List<WorkFlowProcedureModel> list = new List<WorkFlowProcedureModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from WorkFlowProcedure ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY ProcedureCode desc");
            }
            else
            {
                builder.Append(" ORDER BY " + qmObj.SortColumns);
            }
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.SqlParameters = qmObj.Parameters;
            SqlDataReader sqlDataReader = null;
            int num = 0;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        if ((num >= qmObj.StartRecord) && ((list.Count < qmObj.MaxRecords) || (qmObj.MaxRecords == -1)))
                        {
                            WorkFlowProcedureModel model = new WorkFlowProcedureModel();
                            this.Initialize(sqlDataReader, model);
                            list.Add(model);
                        }
                        num++;
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return list;
        }

        public WorkFlowProcedureModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, WorkFlowProcedureModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.ProcedureCode = reader.GetString(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ProcedureName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Description = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.ApplicationPath = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.ApplicationInfoPath = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Type = reader.GetInt32(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Remark = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.SysType = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Creator = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.VersionNumber = reader.GetDecimal(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.ProjectCode = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.CreateUser = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.CreateDate = reader.GetDateTime(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.ModifyUser = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.ModifyDate = reader.GetDateTime(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.Activity = reader.GetInt32(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.VersionDescription = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.ProcedureRemark = reader.GetString(0x11);
            }
        }

        public List<WorkFlowProcedureModel> Select()
        {
            WorkFlowProcedureQueryModel qmObj = new WorkFlowProcedureQueryModel();
            return this._Select(qmObj);
        }

        public List<WorkFlowProcedureModel> Select(WorkFlowProcedureQueryModel qmObj)
        {
            return this._Select(qmObj);
        }
    }
}

