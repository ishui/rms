namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_WorkConcertDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_WorkConcertDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_WorkConcertDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_WorkConcertModel _DataBind(int Code)
        {
            TC_OA_WorkConcertModel model = new TC_OA_WorkConcertModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_WorkConcert ");
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
            builder.Append("delete from TC_OA_WorkConcert ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_WorkConcertModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_WorkConcert(");
            builder.Append("SendUserCode,SendUnitCode,SendDate,GetUnitCode,GetUserCode,Auditing)");
            builder.Append(" values (");
            builder.Append("@SendUserCode,@SendUnitCode,@SendDate,@GetUnitCode,@GetUserCode,@Auditing) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SendUserCode", SqlDbType.VarChar, 50, mObj.SendUserCode);
            this._DataProcess.ProcessParametersAdd("@SendUnitCode", SqlDbType.VarChar, 50, mObj.SendUnitCode);
            this._DataProcess.ProcessParametersAdd("@SendDate", SqlDbType.DateTime, 8, mObj.SendDate);
            this._DataProcess.ProcessParametersAdd("@GetUnitCode", SqlDbType.VarChar, 50, mObj.GetUnitCode);
            this._DataProcess.ProcessParametersAdd("@GetUserCode", SqlDbType.VarChar, 50, mObj.GetUserCode);
            this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_WorkConcertModel> _Select(TC_OA_WorkConcertQueryModel qmObj)
        {
            List<TC_OA_WorkConcertModel> list = new List<TC_OA_WorkConcertModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_WorkConcert ");
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
                            TC_OA_WorkConcertModel model = new TC_OA_WorkConcertModel();
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

        private int _Update(TC_OA_WorkConcertModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_WorkConcert set ");
            if (mObj.SendUserCode != null)
            {
                builder.Append("SendUserCode=@SendUserCode,");
                this._DataProcess.ProcessParametersAdd("@SendUserCode", SqlDbType.VarChar, 50, mObj.SendUserCode);
            }
            if (mObj.SendUnitCode != null)
            {
                builder.Append("SendUnitCode=@SendUnitCode,");
                this._DataProcess.ProcessParametersAdd("@SendUnitCode", SqlDbType.VarChar, 50, mObj.SendUnitCode);
            }
            if (mObj.SendDate != DateTime.MinValue)
            {
                builder.Append("SendDate=@SendDate,");
                this._DataProcess.ProcessParametersAdd("@SendDate", SqlDbType.DateTime, 8, mObj.SendDate);
            }
            if (mObj.GetUnitCode != null)
            {
                builder.Append("GetUnitCode=@GetUnitCode,");
                this._DataProcess.ProcessParametersAdd("@GetUnitCode", SqlDbType.VarChar, 50, mObj.GetUnitCode);
            }
            if (mObj.GetUserCode != null)
            {
                builder.Append("GetUserCode=@GetUserCode,");
                this._DataProcess.ProcessParametersAdd("@GetUserCode", SqlDbType.VarChar, 50, mObj.GetUserCode);
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

        public int Delete(TC_OA_WorkConcertModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_WorkConcertModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_WorkConcertModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.SendUserCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.SendUnitCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.SendDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.GetUnitCode = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.GetUserCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Auditing = reader.GetInt32(6);
            }
        }

        public int Insert(TC_OA_WorkConcertModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_WorkConcertModel> Select()
        {
            TC_OA_WorkConcertQueryModel qmObj = new TC_OA_WorkConcertQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_WorkConcertModel> Select(TC_OA_WorkConcertQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_WorkConcertModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

