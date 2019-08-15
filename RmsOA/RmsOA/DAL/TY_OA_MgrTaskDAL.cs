namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class TY_OA_MgrTaskDAL
    {
        private SqlDataProcess _DataProcess;

        public TY_OA_MgrTaskDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TY_OA_MgrTaskDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TY_OA_MgrTaskModel _DataBind(int Code)
        {
            TY_OA_MgrTaskModel model = new TY_OA_MgrTaskModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TY_OA_MgrTask ");
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
            builder.Append("delete from TY_OA_MgrTask ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TY_OA_MgrTaskModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TY_OA_MgrTask(");
            builder.Append("MgrTaskID,State,TaskName,TaskDetail,IsFinish,TaskTail,CreateDate,CreateMan,ReferLink,DeadLine)");
            builder.Append(" values (");
            builder.Append("@MgrTaskID,@State,@TaskName,@TaskDetail,@IsFinish,@TaskTail,@CreateDate,@CreateMan,@ReferLink,@DeadLine) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MgrTaskID", SqlDbType.VarChar, 50, mObj.MgrTaskID);
            this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            this._DataProcess.ProcessParametersAdd("@TaskName", SqlDbType.VarChar, 500, mObj.TaskName);
            this._DataProcess.ProcessParametersAdd("@TaskDetail", SqlDbType.VarChar, 500, mObj.TaskDetail);
            this._DataProcess.ProcessParametersAdd("@IsFinish", SqlDbType.VarChar, 50, mObj.IsFinish);
            this._DataProcess.ProcessParametersAdd("@TaskTail", SqlDbType.VarChar, 0x3e8, mObj.TaskTail);
            this._DataProcess.ProcessParametersAdd("@CreateDate", SqlDbType.DateTime, 8, mObj.CreateDate);
            this._DataProcess.ProcessParametersAdd("@CreateMan", SqlDbType.VarChar, 50, mObj.CreateMan);
            this._DataProcess.ProcessParametersAdd("@ReferLink", SqlDbType.VarChar, 500, (mObj.ReferLink == null) ? "" : mObj.ReferLink);
            DateTime deadLine = mObj.DeadLine;
            this._DataProcess.ProcessParametersAdd("@DeadLine", SqlDbType.DateTime, 8, mObj.DeadLine);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TY_OA_MgrTaskModel> _Select(TY_OA_MgrTaskQueryModel qmObj)
        {
            List<TY_OA_MgrTaskModel> list = new List<TY_OA_MgrTaskModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TY_OA_MgrTask ");
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
                            TY_OA_MgrTaskModel model = new TY_OA_MgrTaskModel();
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

        private int _Update(TY_OA_MgrTaskModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TY_OA_MgrTask set ");
            if (mObj.MgrTaskID != null)
            {
                builder.Append("MgrTaskID=@MgrTaskID,");
                this._DataProcess.ProcessParametersAdd("@MgrTaskID", SqlDbType.VarChar, 50, mObj.MgrTaskID);
            }
            if (mObj.State != null)
            {
                builder.Append("State=@State,");
                this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            }
            if (mObj.TaskName != null)
            {
                builder.Append("TaskName=@TaskName,");
                this._DataProcess.ProcessParametersAdd("@TaskName", SqlDbType.VarChar, 500, mObj.TaskName);
            }
            if (mObj.TaskDetail != null)
            {
                builder.Append("TaskDetail=@TaskDetail,");
                this._DataProcess.ProcessParametersAdd("@TaskDetail", SqlDbType.VarChar, 500, mObj.TaskDetail);
            }
            if (mObj.IsFinish != null)
            {
                builder.Append("IsFinish=@IsFinish,");
                this._DataProcess.ProcessParametersAdd("@IsFinish", SqlDbType.VarChar, 50, mObj.IsFinish);
            }
            if (mObj.TaskTail != null)
            {
                builder.Append("TaskTail=@TaskTail,");
                this._DataProcess.ProcessParametersAdd("@TaskTail", SqlDbType.VarChar, 0x3e8, mObj.TaskTail);
            }
            if (mObj.CreateDate != DateTime.MinValue)
            {
                builder.Append("CreateDate=@CreateDate,");
                this._DataProcess.ProcessParametersAdd("@CreateDate", SqlDbType.DateTime, 8, mObj.CreateDate);
            }
            if (mObj.CreateMan != null)
            {
                builder.Append("CreateMan=@CreateMan,");
                this._DataProcess.ProcessParametersAdd("@CreateMan", SqlDbType.VarChar, 50, mObj.CreateMan);
            }
            if (mObj.ReferLink != null)
            {
                builder.Append("ReferLink=@ReferLink,");
                this._DataProcess.ProcessParametersAdd("@ReferLink", SqlDbType.VarChar, 500, mObj.ReferLink);
            }
            if (mObj.DeadLine != DateTime.MinValue)
            {
                builder.Append("DeadLine=@DeadLine,");
                this._DataProcess.ProcessParametersAdd("@DeadLine", SqlDbType.DateTime, 8, mObj.DeadLine);
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

        public int Delete(TY_OA_MgrTaskModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TY_OA_MgrTaskModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TY_OA_MgrTaskModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.MgrTaskID = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.State = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.TaskName = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.TaskDetail = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.IsFinish = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.TaskTail = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.CreateDate = reader.GetDateTime(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.CreateMan = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.ReferLink = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.DeadLine = reader.GetDateTime(10);
            }
        }

        public int Insert(TY_OA_MgrTaskModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TY_OA_MgrTaskModel> Select()
        {
            TY_OA_MgrTaskQueryModel qmObj = new TY_OA_MgrTaskQueryModel();
            return this._Select(qmObj);
        }

        public List<TY_OA_MgrTaskModel> Select(TY_OA_MgrTaskQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TY_OA_MgrTaskModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

