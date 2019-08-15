namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TY_ReferendumReportDAL
    {
        private SqlDataProcess _DataProcess;

        public TY_ReferendumReportDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TY_ReferendumReportDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TY_ReferendumReportModel _DataBind(int Code)
        {
            TY_ReferendumReportModel model = new TY_ReferendumReportModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TY_ReferendumReport ");
            builder.Append(" where ReferendumReportCode=@ReferendumReportCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ReferendumReportCode", SqlDbType.Int, 4, Code);
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

        private int _Delete(int Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TY_ReferendumReport ");
            builder.Append(" where ReferendumReportCode=@ReferendumReportCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ReferendumReportCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TY_ReferendumReportModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TY_ReferendumReport(");
            builder.Append("Number,StartDate,StartPerson,TextTitle,TextContent,Audit,Field1)");
            builder.Append(" values (");
            builder.Append("@Number,@StartDate,@StartPerson,@TextTitle,@TextContent,@Audit,@Field1) ");
            builder.Append("SELECT @ReferendumReportCode = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            this._DataProcess.ProcessParametersAdd("@StartDate", SqlDbType.DateTime, 8, mObj.StartDate);
            this._DataProcess.ProcessParametersAdd("@StartPerson", SqlDbType.VarChar, 50, mObj.StartPerson);
            this._DataProcess.ProcessParametersAdd("@TextTitle", SqlDbType.VarChar, 50, mObj.TextTitle);
            this._DataProcess.ProcessParametersAdd("@TextContent", SqlDbType.VarChar, 800, mObj.TextContent);
            this._DataProcess.ProcessParametersAdd("@Audit", SqlDbType.VarChar, 50, mObj.Audit);
            this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@ReferendumReportCode", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.ReferendumReportCode = (int) parameter.Value;
            return mObj.ReferendumReportCode;
        }

        private List<TY_ReferendumReportModel> _Select(TY_ReferendumReportQueryModel qmObj)
        {
            List<TY_ReferendumReportModel> list = new List<TY_ReferendumReportModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TY_ReferendumReport ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY ReferendumReportCode desc");
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
                            TY_ReferendumReportModel model = new TY_ReferendumReportModel();
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

        private int _Update(TY_ReferendumReportModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TY_ReferendumReport set ");
            if (mObj.Number != null)
            {
                builder.Append("Number=@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            }
            if (mObj.StartDate != DateTime.MinValue)
            {
                builder.Append("StartDate=@StartDate,");
                this._DataProcess.ProcessParametersAdd("@StartDate", SqlDbType.DateTime, 8, mObj.StartDate);
            }
            if (mObj.StartPerson != null)
            {
                builder.Append("StartPerson=@StartPerson,");
                this._DataProcess.ProcessParametersAdd("@StartPerson", SqlDbType.VarChar, 50, mObj.StartPerson);
            }
            if (mObj.TextTitle != null)
            {
                builder.Append("TextTitle=@TextTitle,");
                this._DataProcess.ProcessParametersAdd("@TextTitle", SqlDbType.VarChar, 50, mObj.TextTitle);
            }
            if (mObj.TextContent != null)
            {
                builder.Append("TextContent=@TextContent,");
                this._DataProcess.ProcessParametersAdd("@TextContent", SqlDbType.VarChar, 800, mObj.TextContent);
            }
            if (mObj.Audit != null)
            {
                builder.Append("Audit=@Audit,");
                this._DataProcess.ProcessParametersAdd("@Audit", SqlDbType.VarChar, 50, mObj.Audit);
            }
            if (mObj.Field1 != null)
            {
                builder.Append("Field1=@Field1,");
                this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where ReferendumReportCode=@ReferendumReportCode");
            this._DataProcess.ProcessParametersAdd("@ReferendumReportCode", SqlDbType.Int, 4, mObj.ReferendumReportCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(TY_ReferendumReportModel mObj)
        {
            return this._Delete(mObj.ReferendumReportCode);
        }

        public TY_ReferendumReportModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TY_ReferendumReportModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.ReferendumReportCode = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Number = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.StartDate = reader.GetDateTime(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.StartPerson = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.TextTitle = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.TextContent = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Audit = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Field1 = reader.GetString(7);
            }
        }

        public int Insert(TY_ReferendumReportModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TY_ReferendumReportModel> Select()
        {
            TY_ReferendumReportQueryModel qmObj = new TY_ReferendumReportQueryModel();
            return this._Select(qmObj);
        }

        public List<TY_ReferendumReportModel> Select(TY_ReferendumReportQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TY_ReferendumReportModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

