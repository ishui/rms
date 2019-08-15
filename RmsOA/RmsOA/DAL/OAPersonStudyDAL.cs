namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class OAPersonStudyDAL
    {
        private SqlDataProcess _DataProcess;

        public OAPersonStudyDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public OAPersonStudyDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private OAPersonStudyModel _DataBind(int Code)
        {
            OAPersonStudyModel model = new OAPersonStudyModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonStudy ");
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
            builder.Append("delete from GK_OAPersonStudy ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(OAPersonStudyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPersonStudy(");
            builder.Append("personid,BEGIN_DATE,END_DATE,SCHOOL_NAME,SPECIALITY,DEGREE,LETTER_NAME,Certifier)");
            builder.Append(" values (");
            builder.Append("@personid,@BEGIN_DATE,@END_DATE,@SCHOOL_NAME,@SPECIALITY,@DEGREE,@LETTER_NAME,@Certifier) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@personid", SqlDbType.VarChar, 50, mObj.personid);
            this._DataProcess.ProcessParametersAdd("@BEGIN_DATE", SqlDbType.VarChar, 50, mObj.BEGIN_DATE);
            this._DataProcess.ProcessParametersAdd("@END_DATE", SqlDbType.VarChar, 50, mObj.END_DATE);
            this._DataProcess.ProcessParametersAdd("@SCHOOL_NAME", SqlDbType.VarChar, 100, mObj.SCHOOL_NAME);
            this._DataProcess.ProcessParametersAdd("@SPECIALITY", SqlDbType.VarChar, 100, mObj.SPECIALITY);
            this._DataProcess.ProcessParametersAdd("@DEGREE", SqlDbType.VarChar, 50, mObj.DEGREE);
            this._DataProcess.ProcessParametersAdd("@LETTER_NAME", SqlDbType.VarChar, 50, mObj.LETTER_NAME);
            this._DataProcess.ProcessParametersAdd("@Certifier", SqlDbType.VarChar, 50, mObj.Certifier);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<OAPersonStudyModel> _Select(OAPersonStudyQueryModel qmObj)
        {
            List<OAPersonStudyModel> list = new List<OAPersonStudyModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonStudy ");
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
                            OAPersonStudyModel model = new OAPersonStudyModel();
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

        private int _Update(OAPersonStudyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPersonStudy set ");
            if (mObj.personid != null)
            {
                builder.Append("personid=@personid,");
                this._DataProcess.ProcessParametersAdd("@personid", SqlDbType.VarChar, 50, mObj.personid);
            }
            if (mObj.BEGIN_DATE != null)
            {
                builder.Append("BEGIN_DATE=@BEGIN_DATE,");
                this._DataProcess.ProcessParametersAdd("@BEGIN_DATE", SqlDbType.VarChar, 50, mObj.BEGIN_DATE);
            }
            if (mObj.END_DATE != null)
            {
                builder.Append("END_DATE=@END_DATE,");
                this._DataProcess.ProcessParametersAdd("@END_DATE", SqlDbType.VarChar, 50, mObj.END_DATE);
            }
            if (mObj.SCHOOL_NAME != null)
            {
                builder.Append("SCHOOL_NAME=@SCHOOL_NAME,");
                this._DataProcess.ProcessParametersAdd("@SCHOOL_NAME", SqlDbType.VarChar, 100, mObj.SCHOOL_NAME);
            }
            if (mObj.SPECIALITY != null)
            {
                builder.Append("SPECIALITY=@SPECIALITY,");
                this._DataProcess.ProcessParametersAdd("@SPECIALITY", SqlDbType.VarChar, 100, mObj.SPECIALITY);
            }
            if (mObj.DEGREE != null)
            {
                builder.Append("DEGREE=@DEGREE,");
                this._DataProcess.ProcessParametersAdd("@DEGREE", SqlDbType.VarChar, 50, mObj.DEGREE);
            }
            if (mObj.LETTER_NAME != null)
            {
                builder.Append("LETTER_NAME=@LETTER_NAME,");
                this._DataProcess.ProcessParametersAdd("@LETTER_NAME", SqlDbType.VarChar, 50, mObj.LETTER_NAME);
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

        public int Delete(OAPersonStudyModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public OAPersonStudyModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, OAPersonStudyModel obj)
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
                obj.BEGIN_DATE = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.END_DATE = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.SCHOOL_NAME = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.SPECIALITY = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.DEGREE = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.LETTER_NAME = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Certifier = reader.GetString(8);
            }
        }

        public int Insert(OAPersonStudyModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<OAPersonStudyModel> Select()
        {
            OAPersonStudyQueryModel qmObj = new OAPersonStudyQueryModel();
            return this._Select(qmObj);
        }

        public List<OAPersonStudyModel> Select(OAPersonStudyQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(OAPersonStudyModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

