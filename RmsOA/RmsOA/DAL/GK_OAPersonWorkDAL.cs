namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OAPersonWorkDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OAPersonWorkDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OAPersonWorkDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OAPersonWorkModel _DataBind(int Code)
        {
            GK_OAPersonWorkModel model = new GK_OAPersonWorkModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonWork ");
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
            builder.Append("delete from GK_OAPersonWork ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OAPersonWorkModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPersonWork(");
            builder.Append("PersonID,BeginDate,EndDate,WorkUnit,JobPost,Duty,Certifier)");
            builder.Append(" values (");
            builder.Append("@PersonID,@BeginDate,@EndDate,@WorkUnit,@JobPost,@Duty,@Certifier) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@PersonID", SqlDbType.VarChar, 50, mObj.PersonID);
            this._DataProcess.ProcessParametersAdd("@BeginDate", SqlDbType.VarChar, 50, mObj.BeginDate);
            this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.VarChar, 50, mObj.EndDate);
            this._DataProcess.ProcessParametersAdd("@WorkUnit", SqlDbType.VarChar, 50, mObj.WorkUnit);
            this._DataProcess.ProcessParametersAdd("@JobPost", SqlDbType.VarChar, 50, mObj.JobPost);
            this._DataProcess.ProcessParametersAdd("@Duty", SqlDbType.VarChar, 50, mObj.Duty);
            this._DataProcess.ProcessParametersAdd("@Certifier", SqlDbType.VarChar, 50, mObj.Certifier);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OAPersonWorkModel> _Select(GK_OAPersonWorkQueryModel qmObj)
        {
            List<GK_OAPersonWorkModel> list = new List<GK_OAPersonWorkModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonWork ");
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
                            GK_OAPersonWorkModel model = new GK_OAPersonWorkModel();
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

        private int _Update(GK_OAPersonWorkModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPersonWork set ");
            if (mObj.PersonID != null)
            {
                builder.Append("PersonID=@PersonID,");
                this._DataProcess.ProcessParametersAdd("@PersonID", SqlDbType.VarChar, 50, mObj.PersonID);
            }
            if (mObj.BeginDate != null)
            {
                builder.Append("BeginDate=@BeginDate,");
                this._DataProcess.ProcessParametersAdd("@BeginDate", SqlDbType.VarChar, 50, mObj.BeginDate);
            }
            if (mObj.EndDate != null)
            {
                builder.Append("EndDate=@EndDate,");
                this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.VarChar, 50, mObj.EndDate);
            }
            if (mObj.WorkUnit != null)
            {
                builder.Append("WorkUnit=@WorkUnit,");
                this._DataProcess.ProcessParametersAdd("@WorkUnit", SqlDbType.VarChar, 50, mObj.WorkUnit);
            }
            if (mObj.JobPost != null)
            {
                builder.Append("JobPost=@JobPost,");
                this._DataProcess.ProcessParametersAdd("@JobPost", SqlDbType.VarChar, 50, mObj.JobPost);
            }
            if (mObj.Duty != null)
            {
                builder.Append("Duty=@Duty,");
                this._DataProcess.ProcessParametersAdd("@Duty", SqlDbType.VarChar, 50, mObj.Duty);
            }
            if (mObj.Certifier != null)
            {
                builder.Append("Certifier=@Certifier,");
                this._DataProcess.ProcessParametersAdd("@Certifier", SqlDbType.VarChar, 50, mObj.Certifier);
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

        public int Delete(GK_OAPersonWorkModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OAPersonWorkModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OAPersonWorkModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.PersonID = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.BeginDate = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.EndDate = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.WorkUnit = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.JobPost = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Duty = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Certifier = reader.GetString(7);
            }
        }

        public int Insert(GK_OAPersonWorkModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OAPersonWorkModel> Select()
        {
            GK_OAPersonWorkQueryModel qmObj = new GK_OAPersonWorkQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OAPersonWorkModel> Select(GK_OAPersonWorkQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OAPersonWorkModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

