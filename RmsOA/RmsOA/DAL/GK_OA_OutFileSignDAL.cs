namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_OutFileSignDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_OutFileSignDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_OutFileSignDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_OutFileSignModel _DataBind(int Code)
        {
            GK_OA_OutFileSignModel model = new GK_OA_OutFileSignModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_OutFileSign ");
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
            builder.Append("delete from GK_OA_OutFileSign ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_OutFileSignModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_OutFileSign(");
            builder.Append("SystemCode,FileCode,RegisterDate,FileTitle,OutFileCode,Secret,Urgent,NB_UnitCode,NB_UserCode,JB_UnitCode,JB_UserCode,HG_UserCode,Status,Number)");
            builder.Append(" values (");
            builder.Append("@SystemCode,@FileCode,@RegisterDate,@FileTitle,@OutFileCode,@Secret,@Urgent,@NB_UnitCode,@NB_UserCode,@JB_UnitCode,@JB_UserCode,@HG_UserCode,@Status,@Number) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@RegisterDate", SqlDbType.DateTime, 8, mObj.RegisterDate);
            this._DataProcess.ProcessParametersAdd("@FileTitle", SqlDbType.VarChar, 50, mObj.FileTitle);
            this._DataProcess.ProcessParametersAdd("@OutFileCode", SqlDbType.VarChar, 50, mObj.OutFileCode);
            this._DataProcess.ProcessParametersAdd("@Secret", SqlDbType.VarChar, 50, mObj.Secret);
            this._DataProcess.ProcessParametersAdd("@Urgent", SqlDbType.VarChar, 50, mObj.Urgent);
            this._DataProcess.ProcessParametersAdd("@NB_UnitCode", SqlDbType.VarChar, 50, mObj.NB_UnitCode);
            this._DataProcess.ProcessParametersAdd("@NB_UserCode", SqlDbType.VarChar, 50, mObj.NB_UserCode);
            this._DataProcess.ProcessParametersAdd("@JB_UnitCode", SqlDbType.VarChar, 50, mObj.JB_UnitCode);
            this._DataProcess.ProcessParametersAdd("@JB_UserCode", SqlDbType.VarChar, 50, mObj.JB_UserCode);
            this._DataProcess.ProcessParametersAdd("@HG_UserCode", SqlDbType.VarChar, 50, mObj.HG_UserCode);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.Decimal, 50, mObj.Number);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_OutFileSignModel> _Select(GK_OA_OutFileSignQueryModel qmObj)
        {
            List<GK_OA_OutFileSignModel> list = new List<GK_OA_OutFileSignModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_OutFileSign ");
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
                            GK_OA_OutFileSignModel model = new GK_OA_OutFileSignModel();
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

        private int _Update(GK_OA_OutFileSignModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_OutFileSign set ");
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
            if (mObj.RegisterDate != DateTime.MinValue)
            {
                builder.Append("RegisterDate=@RegisterDate,");
                this._DataProcess.ProcessParametersAdd("@RegisterDate", SqlDbType.DateTime, 8, mObj.RegisterDate);
            }
            if (mObj.FileTitle != null)
            {
                builder.Append("FileTitle=@FileTitle,");
                this._DataProcess.ProcessParametersAdd("@FileTitle", SqlDbType.VarChar, 50, mObj.FileTitle);
            }
            if (mObj.OutFileCode != null)
            {
                builder.Append("OutFileCode=@OutFileCode,");
                this._DataProcess.ProcessParametersAdd("@OutFileCode", SqlDbType.VarChar, 50, mObj.OutFileCode);
            }
            if (mObj.Secret != null)
            {
                builder.Append("Secret=@Secret,");
                this._DataProcess.ProcessParametersAdd("@Secret", SqlDbType.VarChar, 50, mObj.Secret);
            }
            if (mObj.Urgent != null)
            {
                builder.Append("Urgent=@Urgent,");
                this._DataProcess.ProcessParametersAdd("@Urgent", SqlDbType.VarChar, 50, mObj.Urgent);
            }
            if (mObj.NB_UnitCode != null)
            {
                builder.Append("NB_UnitCode=@NB_UnitCode,");
                this._DataProcess.ProcessParametersAdd("@NB_UnitCode", SqlDbType.VarChar, 50, mObj.NB_UnitCode);
            }
            if (mObj.NB_UserCode != null)
            {
                builder.Append("NB_UserCode=@NB_UserCode,");
                this._DataProcess.ProcessParametersAdd("@NB_UserCode", SqlDbType.VarChar, 50, mObj.NB_UserCode);
            }
            if (mObj.JB_UnitCode != null)
            {
                builder.Append("JB_UnitCode=@JB_UnitCode,");
                this._DataProcess.ProcessParametersAdd("@JB_UnitCode", SqlDbType.VarChar, 50, mObj.JB_UnitCode);
            }
            if (mObj.JB_UserCode != null)
            {
                builder.Append("JB_UserCode=@JB_UserCode,");
                this._DataProcess.ProcessParametersAdd("@JB_UserCode", SqlDbType.VarChar, 50, mObj.JB_UserCode);
            }
            if (mObj.HG_UserCode != null)
            {
                builder.Append("HG_UserCode=@HG_UserCode,");
                this._DataProcess.ProcessParametersAdd("@HG_UserCode", SqlDbType.VarChar, 50, mObj.HG_UserCode);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            decimal number = mObj.Number;
            builder.Append("Number=@Number,");
            this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.Decimal, 50, mObj.Number);
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

        public int Delete(GK_OA_OutFileSignModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_OutFileSignModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_OutFileSignModel obj)
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
                obj.RegisterDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.FileTitle = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.OutFileCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Secret = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Urgent = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.NB_UnitCode = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.NB_UserCode = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.JB_UnitCode = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.JB_UserCode = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.HG_UserCode = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.Status = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.Number = reader.GetDecimal(14);
            }
        }

        public int Insert(GK_OA_OutFileSignModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_OutFileSignModel> Select()
        {
            GK_OA_OutFileSignQueryModel qmObj = new GK_OA_OutFileSignQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_OutFileSignModel> Select(GK_OA_OutFileSignQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_OutFileSignModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

