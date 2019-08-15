namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_WorkLogDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_WorkLogDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_WorkLogDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_WorkLogModel _DataBind(int Code)
        {
            GK_OA_WorkLogModel model = new GK_OA_WorkLogModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_WorkLog ");
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
            builder.Append("delete from GK_OA_WorkLog ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_WorkLogModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            if (mObj.Waiter == null)
            {
                mObj.Waiter = "";
            }
            builder.Append("insert into GK_OA_WorkLog(");
            builder.Append("DayWrited,UserId,Weather,Mood,Context,Waiter)");
            builder.Append(" values (");
            builder.Append("@DayWrited,@UserId,@Weather,@Mood,@Context,@Waiter) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@DayWrited", SqlDbType.DateTime, 8, mObj.DayWrited);
            this._DataProcess.ProcessParametersAdd("@UserId", SqlDbType.VarChar, 50, mObj.UserId);
            this._DataProcess.ProcessParametersAdd("@Weather", SqlDbType.VarChar, 100, mObj.Weather);
            this._DataProcess.ProcessParametersAdd("@Mood", SqlDbType.VarChar, 100, mObj.Mood);
            this._DataProcess.ProcessParametersAdd("@Context", SqlDbType.Text, 0xffff, mObj.Context);
            this._DataProcess.ProcessParametersAdd("@Waiter", SqlDbType.VarChar, 50, mObj.Waiter);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_WorkLogModel> _Select(GK_OA_WorkLogQueryModel qmObj)
        {
            List<GK_OA_WorkLogModel> list = new List<GK_OA_WorkLogModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_WorkLog ");
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
                            GK_OA_WorkLogModel model = new GK_OA_WorkLogModel();
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

        private int _Update(GK_OA_WorkLogModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_WorkLog set ");
            if (mObj.DayWrited != DateTime.MinValue)
            {
                builder.Append("DayWrited=@DayWrited,");
                this._DataProcess.ProcessParametersAdd("@DayWrited", SqlDbType.DateTime, 8, mObj.DayWrited);
            }
            if (mObj.UserId != null)
            {
                builder.Append("UserId=@UserId,");
                this._DataProcess.ProcessParametersAdd("@UserId", SqlDbType.VarChar, 50, mObj.UserId);
            }
            if (mObj.Weather != null)
            {
                builder.Append("Weather=@Weather,");
                this._DataProcess.ProcessParametersAdd("@Weather", SqlDbType.VarChar, 100, mObj.Weather);
            }
            if (mObj.Mood != null)
            {
                builder.Append("Mood=@Mood,");
                this._DataProcess.ProcessParametersAdd("@Mood", SqlDbType.VarChar, 100, mObj.Mood);
            }
            if (mObj.Context != null)
            {
                builder.Append("Context=@Context,");
                this._DataProcess.ProcessParametersAdd("@Context", SqlDbType.VarChar, 0xffff, mObj.Context);
            }
            if (mObj.Waiter != null)
            {
                builder.Append("Waiter=@Waiter,");
                this._DataProcess.ProcessParametersAdd("@Waiter", SqlDbType.VarChar, 50, mObj.Waiter);
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

        public int Delete(GK_OA_WorkLogModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_WorkLogModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_WorkLogModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.DayWrited = reader.GetDateTime(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UserId = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Weather = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Mood = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Context = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Waiter = reader.GetString(6);
            }
        }

        public int Insert(GK_OA_WorkLogModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_WorkLogModel> Select()
        {
            GK_OA_WorkLogQueryModel qmObj = new GK_OA_WorkLogQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_WorkLogModel> Select(GK_OA_WorkLogQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_WorkLogModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

