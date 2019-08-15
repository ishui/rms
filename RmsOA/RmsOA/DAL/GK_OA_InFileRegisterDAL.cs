namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_InFileRegisterDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_InFileRegisterDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_InFileRegisterDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_InFileRegisterModel _DataBind(int Code)
        {
            GK_OA_InFileRegisterModel model = new GK_OA_InFileRegisterModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_InFileRegister ");
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
            builder.Append("delete from GK_OA_InFileRegister ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_InFileRegisterModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_InFileRegister(");
            builder.Append("RegisterMainCode,AuditingMailCode,InFileDate,InFileCode,OriginalFileCode,FileType,FileNumber,Remark,Field1)");
            builder.Append(" values (");
            builder.Append("@RegisterMainCode,@AuditingMailCode,@InFileDate,@InFileCode,@OriginalFileCode,@FileType,@FileNumber,@Remark,@Field1) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@RegisterMainCode", SqlDbType.VarChar, 50, mObj.RegisterMainCode);
            this._DataProcess.ProcessParametersAdd("@AuditingMailCode", SqlDbType.VarChar, 50, mObj.AuditingMailCode);
            this._DataProcess.ProcessParametersAdd("@InFileDate", SqlDbType.DateTime, 8, mObj.InFileDate);
            this._DataProcess.ProcessParametersAdd("@InFileCode", SqlDbType.VarChar, 50, mObj.InFileCode);
            this._DataProcess.ProcessParametersAdd("@OriginalFileCode", SqlDbType.VarChar, 50, mObj.OriginalFileCode);
            this._DataProcess.ProcessParametersAdd("@FileType", SqlDbType.VarChar, 50, mObj.FileType);
            this._DataProcess.ProcessParametersAdd("@FileNumber", SqlDbType.VarChar, 50, mObj.FileNumber);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_InFileRegisterModel> _Select(GK_OA_InFileRegisterQueryModel qmObj)
        {
            List<GK_OA_InFileRegisterModel> list = new List<GK_OA_InFileRegisterModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_InFileRegister ");
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
                            GK_OA_InFileRegisterModel model = new GK_OA_InFileRegisterModel();
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

        private int _Update(GK_OA_InFileRegisterModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_InFileRegister set ");
            if (mObj.RegisterMainCode != null)
            {
                builder.Append("RegisterMainCode=@RegisterMainCode,");
                this._DataProcess.ProcessParametersAdd("@RegisterMainCode", SqlDbType.VarChar, 50, mObj.RegisterMainCode);
            }
            if (mObj.AuditingMailCode != null)
            {
                builder.Append("AuditingMailCode=@AuditingMailCode,");
                this._DataProcess.ProcessParametersAdd("@AuditingMailCode", SqlDbType.VarChar, 50, mObj.AuditingMailCode);
            }
            if (mObj.InFileDate != DateTime.MinValue)
            {
                builder.Append("InFileDate=@InFileDate,");
                this._DataProcess.ProcessParametersAdd("@InFileDate", SqlDbType.DateTime, 8, mObj.InFileDate);
            }
            if (mObj.InFileCode != null)
            {
                builder.Append("InFileCode=@InFileCode,");
                this._DataProcess.ProcessParametersAdd("@InFileCode", SqlDbType.VarChar, 50, mObj.InFileCode);
            }
            if (mObj.OriginalFileCode != null)
            {
                builder.Append("OriginalFileCode=@OriginalFileCode,");
                this._DataProcess.ProcessParametersAdd("@OriginalFileCode", SqlDbType.VarChar, 50, mObj.OriginalFileCode);
            }
            if (mObj.FileType != null)
            {
                builder.Append("FileType=@FileType,");
                this._DataProcess.ProcessParametersAdd("@FileType", SqlDbType.VarChar, 50, mObj.FileType);
            }
            if (mObj.FileNumber != null)
            {
                builder.Append("FileNumber=@FileNumber,");
                this._DataProcess.ProcessParametersAdd("@FileNumber", SqlDbType.VarChar, 50, mObj.FileNumber);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            }
            if (mObj.Field1 != null)
            {
                builder.Append("Field1=@Field1,");
                this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
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

        public int Delete(GK_OA_InFileRegisterModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_InFileRegisterModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_InFileRegisterModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.RegisterMainCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.AuditingMailCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.InFileDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.InFileCode = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.OriginalFileCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.FileType = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.FileNumber = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Remark = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Field1 = reader.GetString(9);
            }
        }

        public int Insert(GK_OA_InFileRegisterModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_InFileRegisterModel> Select()
        {
            GK_OA_InFileRegisterQueryModel qmObj = new GK_OA_InFileRegisterQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_InFileRegisterModel> Select(GK_OA_InFileRegisterQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_InFileRegisterModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

