namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class YF_AssetDrawDAL
    {
        private SqlDataProcess _DataProcess;

        public YF_AssetDrawDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public YF_AssetDrawDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private YF_AssetDrawModel _DataBind(int Code)
        {
            YF_AssetDrawModel model = new YF_AssetDrawModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetDraw ");
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
            builder.Append("delete from YF_AssetDraw ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(YF_AssetDrawModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO YF_AssetDraw (");
            builder2.Append("VALUES(");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode,");
                builder2.Append("@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.DrawUnit != null)
            {
                builder.Append("DrawUnit,");
                builder2.Append("@DrawUnit,");
                this._DataProcess.ProcessParametersAdd("@DrawUnit", SqlDbType.VarChar, 50, mObj.DrawUnit);
            }
            if (mObj.DrawDate != DateTime.MinValue)
            {
                builder.Append("DrawDate,");
                builder2.Append("@DrawDate,");
                this._DataProcess.ProcessParametersAdd("@DrawDate", SqlDbType.DateTime, 8, mObj.DrawDate);
            }
            if (mObj.DrawPerson != null)
            {
                builder.Append("DrawPerson,");
                builder2.Append("@DrawPerson,");
                this._DataProcess.ProcessParametersAdd("@DrawPerson", SqlDbType.VarChar, 50, mObj.DrawPerson);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode,");
                builder2.Append("@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.Unit != null)
            {
                builder.Append("Unit,");
                builder2.Append("@Unit,");
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            }
            if (mObj.BackTime != DateTime.MinValue)
            {
                builder.Append("BackTime,");
                builder2.Append("@BackTime,");
                this._DataProcess.ProcessParametersAdd("@BackTime", SqlDbType.DateTime, 8, mObj.BackTime);
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

        private List<YF_AssetDrawModel> _Select(YF_AssetDrawQueryModel qmObj)
        {
            List<YF_AssetDrawModel> list = new List<YF_AssetDrawModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetDraw ");
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
                            YF_AssetDrawModel model = new YF_AssetDrawModel();
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

        private int _Update(YF_AssetDrawModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update YF_AssetDraw set ");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode=@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.DrawUnit != null)
            {
                builder.Append("DrawUnit=@DrawUnit,");
                this._DataProcess.ProcessParametersAdd("@DrawUnit", SqlDbType.VarChar, 50, mObj.DrawUnit);
            }
            if (mObj.DrawDate != DateTime.MinValue)
            {
                builder.Append("DrawDate=@DrawDate,");
                this._DataProcess.ProcessParametersAdd("@DrawDate", SqlDbType.DateTime, 8, mObj.DrawDate);
            }
            if (mObj.DrawPerson != null)
            {
                builder.Append("DrawPerson=@DrawPerson,");
                this._DataProcess.ProcessParametersAdd("@DrawPerson", SqlDbType.VarChar, 50, mObj.DrawPerson);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.Unit != null)
            {
                builder.Append("Unit=@Unit,");
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            }
            if (mObj.BackTime != DateTime.MinValue)
            {
                builder.Append("BackTime=@BackTime,");
                this._DataProcess.ProcessParametersAdd("@BackTime", SqlDbType.DateTime, 8, mObj.BackTime);
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

        public int Delete(YF_AssetDrawModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public YF_AssetDrawModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, YF_AssetDrawModel obj)
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
                obj.DrawUnit = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.DrawDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.DrawPerson = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Status = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Unit = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.BackTime = reader.GetDateTime(8);
            }
        }

        public int Insert(YF_AssetDrawModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<YF_AssetDrawModel> Select()
        {
            YF_AssetDrawQueryModel qmObj = new YF_AssetDrawQueryModel();
            return this._Select(qmObj);
        }

        public List<YF_AssetDrawModel> Select(YF_AssetDrawQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(YF_AssetDrawModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

