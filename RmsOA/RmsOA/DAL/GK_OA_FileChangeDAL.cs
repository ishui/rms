namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_FileChangeDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_FileChangeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_FileChangeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_FileChangeModel _DataBind(int Code)
        {
            GK_OA_FileChangeModel model = new GK_OA_FileChangeModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_FileChange ");
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
            builder.Append("delete from GK_OA_FileChange ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_FileChangeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_FileChange(");
            builder.Append("SystemCode,FileSystemCode,FileName,FileCode,OldVersionNumber,NewVersionNumber,UnitCode,SubmitDate,ChangeReason,OldContext,NewContext,Status)");
            builder.Append(" values (");
            builder.Append("@SystemCode,@FileSystemCode,@FileName,@FileCode,@OldVersionNumber,@NewVersionNumber,@UnitCode,@SubmitDate,@ChangeReason,@OldContext,@NewContext,@Status) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            this._DataProcess.ProcessParametersAdd("@FileSystemCode", SqlDbType.VarChar, 50, mObj.FileSystemCode);
            this._DataProcess.ProcessParametersAdd("@FileName", SqlDbType.VarChar, 50, mObj.FileName);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@OldVersionNumber", SqlDbType.VarChar, 50, mObj.OldVersionNumber);
            this._DataProcess.ProcessParametersAdd("@NewVersionNumber", SqlDbType.VarChar, 50, mObj.NewVersionNumber);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@SubmitDate", SqlDbType.DateTime, 8, mObj.SubmitDate);
            this._DataProcess.ProcessParametersAdd("@ChangeReason", SqlDbType.VarChar, 500, mObj.ChangeReason);
            this._DataProcess.ProcessParametersAdd("@OldContext", SqlDbType.VarChar, 500, mObj.OldContext);
            this._DataProcess.ProcessParametersAdd("@NewContext", SqlDbType.VarChar, 500, mObj.NewContext);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_FileChangeModel> _Select(GK_OA_FileChangeQueryModel qmObj)
        {
            List<GK_OA_FileChangeModel> list = new List<GK_OA_FileChangeModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_FileChange ");
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
                            GK_OA_FileChangeModel model = new GK_OA_FileChangeModel();
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

        private int _Update(GK_OA_FileChangeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_FileChange set ");
            if (mObj.SystemCode != null)
            {
                builder.Append("SystemCode=@SystemCode,");
                this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            }
            if (mObj.FileSystemCode != null)
            {
                builder.Append("FileSystemCode=@FileSystemCode,");
                this._DataProcess.ProcessParametersAdd("@FileSystemCode", SqlDbType.VarChar, 50, mObj.FileSystemCode);
            }
            if (mObj.FileName != null)
            {
                builder.Append("FileName=@FileName,");
                this._DataProcess.ProcessParametersAdd("@FileName", SqlDbType.VarChar, 50, mObj.FileName);
            }
            if (mObj.FileCode != null)
            {
                builder.Append("FileCode=@FileCode,");
                this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            }
            if (mObj.OldVersionNumber != null)
            {
                builder.Append("OldVersionNumber=@OldVersionNumber,");
                this._DataProcess.ProcessParametersAdd("@OldVersionNumber", SqlDbType.VarChar, 50, mObj.OldVersionNumber);
            }
            if (mObj.NewVersionNumber != null)
            {
                builder.Append("NewVersionNumber=@NewVersionNumber,");
                this._DataProcess.ProcessParametersAdd("@NewVersionNumber", SqlDbType.VarChar, 50, mObj.NewVersionNumber);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.SubmitDate != DateTime.MinValue)
            {
                builder.Append("SubmitDate=@SubmitDate,");
                this._DataProcess.ProcessParametersAdd("@SubmitDate", SqlDbType.DateTime, 8, mObj.SubmitDate);
            }
            if (mObj.ChangeReason != null)
            {
                builder.Append("ChangeReason=@ChangeReason,");
                this._DataProcess.ProcessParametersAdd("@ChangeReason", SqlDbType.VarChar, 500, mObj.ChangeReason);
            }
            if (mObj.OldContext != null)
            {
                builder.Append("OldContext=@OldContext,");
                this._DataProcess.ProcessParametersAdd("@OldContext", SqlDbType.VarChar, 500, mObj.OldContext);
            }
            if (mObj.NewContext != null)
            {
                builder.Append("NewContext=@NewContext,");
                this._DataProcess.ProcessParametersAdd("@NewContext", SqlDbType.VarChar, 500, mObj.NewContext);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
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

        public int Delete(GK_OA_FileChangeModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_FileChangeModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_FileChangeModel obj)
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
                obj.FileSystemCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.FileName = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.FileCode = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.OldVersionNumber = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.NewVersionNumber = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.SubmitDate = reader.GetDateTime(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.ChangeReason = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.OldContext = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.NewContext = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Status = reader.GetString(12);
            }
        }

        public int Insert(GK_OA_FileChangeModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_FileChangeModel> Select()
        {
            GK_OA_FileChangeQueryModel qmObj = new GK_OA_FileChangeQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_FileChangeModel> Select(GK_OA_FileChangeQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_FileChangeModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

