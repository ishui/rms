namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class CachetTypeManageDAL
    {
        private SqlDataProcess _DataProcess;

        public CachetTypeManageDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public CachetTypeManageDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private CachetTypeManageModel _DataBind(int Code)
        {
            CachetTypeManageModel model = new CachetTypeManageModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from CachetTypeManage ");
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
            builder.Append("delete from CachetTypeManage ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(CachetTypeManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CachetTypeManage(");
            builder.Append("CachetName,Type)");
            builder.Append(" values (");
            builder.Append("@CachetName,@Type) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@CachetName", SqlDbType.VarChar, 100, mObj.CachetName);
            this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<CachetTypeManageModel> _Select(CachetTypeManageQueryModel qmObj)
        {
            List<CachetTypeManageModel> list = new List<CachetTypeManageModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from CachetTypeManage ");
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
                            CachetTypeManageModel model = new CachetTypeManageModel();
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

        private int _Update(CachetTypeManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CachetTypeManage set ");
            if (mObj.CachetName != null)
            {
                builder.Append("CachetName=@CachetName,");
                this._DataProcess.ProcessParametersAdd("@CachetName", SqlDbType.VarChar, 100, mObj.CachetName);
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

        public int Delete(CachetTypeManageModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public CachetTypeManageModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, CachetTypeManageModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.CachetName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Type = reader.GetString(2);
            }
        }

        public int Insert(CachetTypeManageModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<CachetTypeManageModel> Select()
        {
            CachetTypeManageQueryModel qmObj = new CachetTypeManageQueryModel();
            return this._Select(qmObj);
        }

        public List<CachetTypeManageModel> Select(CachetTypeManageQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(CachetTypeManageModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

