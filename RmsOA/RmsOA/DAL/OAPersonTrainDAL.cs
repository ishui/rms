namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class OAPersonTrainDAL
    {
        private SqlDataProcess _DataProcess;

        public OAPersonTrainDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public OAPersonTrainDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private OAPersonTrainModel _DataBind(int Code)
        {
            OAPersonTrainModel model = new OAPersonTrainModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonTrain ");
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
            builder.Append("delete from GK_OAPersonTrain ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(OAPersonTrainModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPersonTrain(");
            builder.Append("personid,BEGIN_DATE,END_DATE,TRAIN_CONTENT,TRAIN_HOUR,TRAIN_METHOD)");
            builder.Append(" values (");
            builder.Append("@personid,@BEGIN_DATE,@END_DATE,@TRAIN_CONTENT,@TRAIN_HOUR,@TRAIN_METHOD) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@personid", SqlDbType.VarChar, 50, mObj.personid);
            this._DataProcess.ProcessParametersAdd("@BEGIN_DATE", SqlDbType.VarChar, 50, mObj.BEGIN_DATE);
            this._DataProcess.ProcessParametersAdd("@END_DATE", SqlDbType.VarChar, 50, mObj.END_DATE);
            this._DataProcess.ProcessParametersAdd("@TRAIN_CONTENT", SqlDbType.VarChar, 0x10, mObj.TRAIN_CONTENT);
            this._DataProcess.ProcessParametersAdd("@TRAIN_HOUR", SqlDbType.VarChar, 50, mObj.TRAIN_HOUR);
            this._DataProcess.ProcessParametersAdd("@TRAIN_METHOD", SqlDbType.VarChar, 50, mObj.TRAIN_METHOD);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<OAPersonTrainModel> _Select(OAPersonTrainQueryModel qmObj)
        {
            List<OAPersonTrainModel> list = new List<OAPersonTrainModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonTrain ");
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
                            OAPersonTrainModel model = new OAPersonTrainModel();
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

        private int _Update(OAPersonTrainModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPersonTrain set ");
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
            if (mObj.TRAIN_CONTENT != null)
            {
                builder.Append("TRAIN_CONTENT=@TRAIN_CONTENT,");
                this._DataProcess.ProcessParametersAdd("@TRAIN_CONTENT", SqlDbType.VarChar, 0x10, mObj.TRAIN_CONTENT);
            }
            if (mObj.TRAIN_HOUR != null)
            {
                builder.Append("TRAIN_HOUR=@TRAIN_HOUR,");
                this._DataProcess.ProcessParametersAdd("@TRAIN_HOUR", SqlDbType.VarChar, 50, mObj.TRAIN_HOUR);
            }
            if (mObj.TRAIN_METHOD != null)
            {
                builder.Append("TRAIN_METHOD=@TRAIN_METHOD,");
                this._DataProcess.ProcessParametersAdd("@TRAIN_METHOD", SqlDbType.VarChar, 50, mObj.TRAIN_METHOD);
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

        public int Delete(OAPersonTrainModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public OAPersonTrainModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, OAPersonTrainModel obj)
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
                obj.TRAIN_CONTENT = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.TRAIN_HOUR = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.TRAIN_METHOD = reader.GetString(6);
            }
        }

        public int Insert(OAPersonTrainModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<OAPersonTrainModel> Select()
        {
            OAPersonTrainQueryModel qmObj = new OAPersonTrainQueryModel();
            return this._Select(qmObj);
        }

        public List<OAPersonTrainModel> Select(OAPersonTrainQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(OAPersonTrainModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

