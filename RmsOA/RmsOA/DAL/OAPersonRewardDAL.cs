namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class OAPersonRewardDAL
    {
        private SqlDataProcess _DataProcess;

        public OAPersonRewardDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public OAPersonRewardDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private OAPersonRewardModel _DataBind(int Code)
        {
            OAPersonRewardModel model = new OAPersonRewardModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonReward ");
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
            builder.Append("delete from GK_OAPersonReward ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(OAPersonRewardModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPersonReward(");
            builder.Append("personid,dj_date,content,cause,remark)");
            builder.Append(" values (");
            builder.Append("@personid,@dj_date,@content,@cause,@remark) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@personid", SqlDbType.VarChar, 50, mObj.personid);
            this._DataProcess.ProcessParametersAdd("@dj_date", SqlDbType.DateTime, 8, mObj.dj_date);
            this._DataProcess.ProcessParametersAdd("@content", SqlDbType.VarChar, 500, mObj.content);
            this._DataProcess.ProcessParametersAdd("@cause", SqlDbType.VarChar, 500, mObj.cause);
            this._DataProcess.ProcessParametersAdd("@remark", SqlDbType.VarChar, 500, mObj.remark);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<OAPersonRewardModel> _Select(OAPersonRewardQueryModel qmObj)
        {
            List<OAPersonRewardModel> list = new List<OAPersonRewardModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonReward ");
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
                            OAPersonRewardModel model = new OAPersonRewardModel();
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

        private int _Update(OAPersonRewardModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPersonReward set ");
            if (mObj.personid != null)
            {
                builder.Append("personid=@personid,");
                this._DataProcess.ProcessParametersAdd("@personid", SqlDbType.VarChar, 50, mObj.personid);
            }
            if (mObj.dj_date != null)
            {
                builder.Append("dj_date=@dj_date,");
                this._DataProcess.ProcessParametersAdd("@dj_date", SqlDbType.VarChar, 50, mObj.dj_date);
            }
            if (mObj.content != null)
            {
                builder.Append("content=@content,");
                this._DataProcess.ProcessParametersAdd("@content", SqlDbType.VarChar, 500, mObj.content);
            }
            if (mObj.cause != null)
            {
                builder.Append("cause=@cause,");
                this._DataProcess.ProcessParametersAdd("@cause", SqlDbType.VarChar, 500, mObj.cause);
            }
            if (mObj.remark != null)
            {
                builder.Append("remark=@remark,");
                this._DataProcess.ProcessParametersAdd("@remark", SqlDbType.VarChar, 500, mObj.remark);
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

        public int Delete(OAPersonRewardModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public OAPersonRewardModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, OAPersonRewardModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.personid = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.dj_date = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.content = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.cause = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.remark = reader.GetString(5);
            }
        }

        public int Insert(OAPersonRewardModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<OAPersonRewardModel> Select()
        {
            OAPersonRewardQueryModel qmObj = new OAPersonRewardQueryModel();
            return this._Select(qmObj);
        }

        public List<OAPersonRewardModel> Select(OAPersonRewardQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(OAPersonRewardModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

