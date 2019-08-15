﻿namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class CachetManageDAL
    {
        private SqlDataProcess _DataProcess;

        public CachetManageDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public CachetManageDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private CachetManageModel _DataBind(int Code)
        {
            CachetManageModel model = new CachetManageModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from CachetManage ");
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
            builder.Append("delete from CachetManage ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(CachetManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CachetManage(");
            builder.Append("UserName,Dept,CachetName,StartData,EndData,State,Type)");
            builder.Append(" values (");
            builder.Append("@UserName,@Dept,@CachetName,@StartData,@EndData,@State,@Type) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            this._DataProcess.ProcessParametersAdd("@CachetName", SqlDbType.VarChar, 500, mObj.CachetName);
            this._DataProcess.ProcessParametersAdd("@StartData", SqlDbType.DateTime, 8, mObj.StartData);
            this._DataProcess.ProcessParametersAdd("@EndData", SqlDbType.DateTime, 8, mObj.EndData);
            this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<CachetManageModel> _Select(CachetManageQueryModel qmObj)
        {
            List<CachetManageModel> list = new List<CachetManageModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from CachetManage ");
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
                            CachetManageModel model = new CachetManageModel();
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

        private int _Update(CachetManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CachetManage set ");
            if (mObj.UserName != null)
            {
                builder.Append("UserName=@UserName,");
                this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept=@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            }
            if (mObj.CachetName != null)
            {
                builder.Append("CachetName=@CachetName,");
                this._DataProcess.ProcessParametersAdd("@CachetName", SqlDbType.VarChar, 500, mObj.CachetName);
            }
            if (mObj.StartData != DateTime.MinValue)
            {
                builder.Append("StartData=@StartData,");
                this._DataProcess.ProcessParametersAdd("@StartData", SqlDbType.DateTime, 8, mObj.StartData);
            }
            if (mObj.EndData != DateTime.MinValue)
            {
                builder.Append("EndData=@EndData,");
                this._DataProcess.ProcessParametersAdd("@EndData", SqlDbType.DateTime, 8, mObj.EndData);
            }
            if (mObj.State != null)
            {
                builder.Append("State=@State,");
                this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
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

        public int Delete(CachetManageModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public CachetManageModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, CachetManageModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.UserName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Dept = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.CachetName = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.StartData = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.EndData = reader.GetDateTime(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.State = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Type = reader.GetString(7);
            }
        }

        public int Insert(CachetManageModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<CachetManageModel> Select()
        {
            CachetManageQueryModel qmObj = new CachetManageQueryModel();
            return this._Select(qmObj);
        }

        public List<CachetManageModel> Select(CachetManageQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(CachetManageModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

