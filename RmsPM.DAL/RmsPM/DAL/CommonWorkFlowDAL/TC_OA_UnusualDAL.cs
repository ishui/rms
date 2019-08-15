namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_UnusualDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_UnusualDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_UnusualDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_UnusualModel _DataBind(int Code)
        {
            TC_OA_UnusualModel model = new TC_OA_UnusualModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_Unusual ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from TC_OA_Unusual ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_UnusualModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_Unusual(");
            builder.Append("ProjectCode,ProjectName,UserCode,ApplayDate,CoustomType,CoustomName,Telephone,Email,Remark,Reason,suggest,Auditing)");
            builder.Append(" values (");
            builder.Append("@ProjectCode,@ProjectName,@UserCode,@ApplayDate,@CoustomType,@CoustomName,@Telephone,@Email,@Remark,@Reason,@suggest,@Auditing) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ProjectCode", SqlDbType.VarChar, 50, mObj.ProjectCode);
            this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 50, mObj.ProjectName);
            this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            this._DataProcess.ProcessParametersAdd("@ApplayDate", SqlDbType.DateTime, 8, mObj.ApplayDate);
            this._DataProcess.ProcessParametersAdd("@CoustomType", SqlDbType.VarChar, 50, mObj.CoustomType);
            this._DataProcess.ProcessParametersAdd("@CoustomName", SqlDbType.VarChar, 50, mObj.CoustomName);
            this._DataProcess.ProcessParametersAdd("@Telephone", SqlDbType.VarChar, 50, mObj.Telephone);
            this._DataProcess.ProcessParametersAdd("@Email", SqlDbType.VarChar, 50, mObj.Email);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 500, mObj.Reason);
            this._DataProcess.ProcessParametersAdd("@suggest", SqlDbType.VarChar, 500, mObj.suggest);
            this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_UnusualModel> _Select(TC_OA_UnusualQueryModel qmObj)
        {
            List<TC_OA_UnusualModel> list = new List<TC_OA_UnusualModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_Unusual ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY Code desc");
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
                            TC_OA_UnusualModel model = new TC_OA_UnusualModel();
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

        private int _Update(TC_OA_UnusualModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_Unusual set ");
            if (mObj.ProjectCode != null)
            {
                builder.Append("ProjectCode=@ProjectCode,");
                this._DataProcess.ProcessParametersAdd("@ProjectCode", SqlDbType.VarChar, 50, mObj.ProjectCode);
            }
            if (mObj.ProjectName != null)
            {
                builder.Append("ProjectName=@ProjectName,");
                this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 50, mObj.ProjectName);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.ApplayDate != DateTime.MinValue)
            {
                builder.Append("ApplayDate=@ApplayDate,");
                this._DataProcess.ProcessParametersAdd("@ApplayDate", SqlDbType.DateTime, 8, mObj.ApplayDate);
            }
            if (mObj.CoustomType != null)
            {
                builder.Append("CoustomType=@CoustomType,");
                this._DataProcess.ProcessParametersAdd("@CoustomType", SqlDbType.VarChar, 50, mObj.CoustomType);
            }
            if (mObj.CoustomName != null)
            {
                builder.Append("CoustomName=@CoustomName,");
                this._DataProcess.ProcessParametersAdd("@CoustomName", SqlDbType.VarChar, 50, mObj.CoustomName);
            }
            if (mObj.Telephone != null)
            {
                builder.Append("Telephone=@Telephone,");
                this._DataProcess.ProcessParametersAdd("@Telephone", SqlDbType.VarChar, 50, mObj.Telephone);
            }
            if (mObj.Email != null)
            {
                builder.Append("Email=@Email,");
                this._DataProcess.ProcessParametersAdd("@Email", SqlDbType.VarChar, 50, mObj.Email);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 500, mObj.Reason);
            }
            if (mObj.suggest != null)
            {
                builder.Append("suggest=@suggest,");
                this._DataProcess.ProcessParametersAdd("@suggest", SqlDbType.VarChar, 500, mObj.suggest);
            }
            if (mObj.Auditing != 0)
            {
                builder.Append("Auditing=@Auditing,");
                this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where Code=@Code");
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, mObj.Code);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(TC_OA_UnusualModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_UnusualModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_UnusualModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ProjectCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.ProjectName = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.ApplayDate = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.CoustomType = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.CoustomName = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Telephone = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Email = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Remark = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Reason = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.suggest = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Auditing = reader.GetInt32(12);
            }
        }

        public int Insert(TC_OA_UnusualModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_UnusualModel> Select()
        {
            TC_OA_UnusualQueryModel qmObj = new TC_OA_UnusualQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_UnusualModel> Select(TC_OA_UnusualQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_UnusualModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

