namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_SubmitAccountMainDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_SubmitAccountMainDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_SubmitAccountMainDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_SubmitAccountMainModel _DataBind(int Code)
        {
            GK_OA_SubmitAccountMainModel model = new GK_OA_SubmitAccountMainModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_SubmitAccountMain ");
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
            builder.Append("delete from GK_OA_SubmitAccountMain ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_SubmitAccountMainModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_SubmitAccountMain(");
            builder.Append("SystemCode,FileCode,RegiesterDate,Name,Duties)");
            builder.Append(" values (");
            builder.Append("@SystemCode,@FileCode,@RegiesterDate,@Name,@Duties) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@RegiesterDate", SqlDbType.DateTime, 8, mObj.RegiesterDate);
            this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            this._DataProcess.ProcessParametersAdd("@Duties", SqlDbType.VarChar, 50, mObj.Duties);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_SubmitAccountMainModel> _Select(GK_OA_SubmitAccountMainQueryModel qmObj)
        {
            List<GK_OA_SubmitAccountMainModel> list = new List<GK_OA_SubmitAccountMainModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_SubmitAccountMain ");
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
                            GK_OA_SubmitAccountMainModel model = new GK_OA_SubmitAccountMainModel();
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

        private int _Update(GK_OA_SubmitAccountMainModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_SubmitAccountMain set ");
            if (mObj.SystemCode != null)
            {
                builder.Append("SystemCode=@SystemCode,");
                this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            }
            if (mObj.FileCode != null)
            {
                builder.Append("FileCode=@FileCode,");
                this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            }
            if (mObj.RegiesterDate != DateTime.MinValue)
            {
                builder.Append("RegiesterDate=@RegiesterDate,");
                this._DataProcess.ProcessParametersAdd("@RegiesterDate", SqlDbType.DateTime, 8, mObj.RegiesterDate);
            }
            if (mObj.Name != null)
            {
                builder.Append("Name=@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            }
            if (mObj.Duties != null)
            {
                builder.Append("Duties=@Duties,");
                this._DataProcess.ProcessParametersAdd("@Duties", SqlDbType.VarChar, 50, mObj.Duties);
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

        public int Delete(GK_OA_SubmitAccountMainModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_SubmitAccountMainModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_SubmitAccountMainModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.SystemCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.FileCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.RegiesterDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Name = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Duties = reader.GetString(5);
            }
        }

        public int Insert(GK_OA_SubmitAccountMainModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_SubmitAccountMainModel> Select()
        {
            GK_OA_SubmitAccountMainQueryModel qmObj = new GK_OA_SubmitAccountMainQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_SubmitAccountMainModel> Select(GK_OA_SubmitAccountMainQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_SubmitAccountMainModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

