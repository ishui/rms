namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class RS_EmployScoreDAL
    {
        private SqlDataProcess _DataProcess;

        public RS_EmployScoreDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public RS_EmployScoreDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private RS_EmployScoreModel _DataBind(int Code)
        {
            RS_EmployScoreModel model = new RS_EmployScoreModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from RS_EmployScore ");
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
            builder.Append("delete from RS_EmployScore ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(RS_EmployScoreModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO RS_EmployScore (");
            builder2.Append("VALUES(");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode,");
                builder2.Append("@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode,");
                builder2.Append("@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 10, mObj.UserCode);
            }
            if (mObj.Score != 0)
            {
                builder.Append("Score,");
                builder2.Append("@Score,");
                this._DataProcess.ProcessParametersAdd("@Score", SqlDbType.Int, 4, mObj.Score);
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            builder.Append(") ");
            builder2.Append(") ");
            builder.Append(builder2.ToString());
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<RS_EmployScoreModel> _Select(RS_EmployScoreQueryModel qmObj)
        {
            List<RS_EmployScoreModel> list = new List<RS_EmployScoreModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from RS_EmployScore ");
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
                            RS_EmployScoreModel model = new RS_EmployScoreModel();
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

        private int _Update(RS_EmployScoreModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update RS_EmployScore set ");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode=@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 10, mObj.UserCode);
            }
            if (mObj.Score != 0)
            {
                builder.Append("Score=@Score,");
                this._DataProcess.ProcessParametersAdd("@Score", SqlDbType.Int, 4, mObj.Score);
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

        public int Delete(RS_EmployScoreModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public RS_EmployScoreModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, RS_EmployScoreModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ManageCode = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Score = reader.GetInt32(3);
            }
        }

        public int Insert(RS_EmployScoreModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<RS_EmployScoreModel> Select()
        {
            RS_EmployScoreQueryModel qmObj = new RS_EmployScoreQueryModel();
            return this._Select(qmObj);
        }

        public List<RS_EmployScoreModel> Select(RS_EmployScoreQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(RS_EmployScoreModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

