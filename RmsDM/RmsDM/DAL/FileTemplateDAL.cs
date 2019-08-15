namespace RmsDM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsDM.MODEL;

    public class FileTemplateDAL
    {
        private SqlDataProcess _DataProcess;

        public FileTemplateDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public FileTemplateDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private FileTemplateModel _DataBind(int Code)
        {
            FileTemplateModel model = new FileTemplateModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FileTemplate ");
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
            builder.Append("delete from FileTemplate ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(FileTemplateModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileTemplate(");
            builder.Append("FileTemplateTypeCode,FileTemplateName,SortCode)");
            builder.Append(" values (");
            builder.Append("@FileTemplateTypeCode,@FileTemplateName,@SortCode) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@FileTemplateTypeCode", SqlDbType.Int, 4, mObj.FileTemplateTypeCode);
            this._DataProcess.ProcessParametersAdd("@FileTemplateName", SqlDbType.VarChar, 100, mObj.FileTemplateName);
            this._DataProcess.ProcessParametersAdd("@SortCode", SqlDbType.VarChar, 500, mObj.SortCode);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<FileTemplateModel> _Select(FileTemplateQueryModel qmObj)
        {
            List<FileTemplateModel> list = new List<FileTemplateModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FileTemplate ");
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
                            FileTemplateModel model = new FileTemplateModel();
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

        private int _Update(FileTemplateModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileTemplate set ");
            if (mObj.FileTemplateTypeCode != 0)
            {
                builder.Append("FileTemplateTypeCode=@FileTemplateTypeCode,");
                this._DataProcess.ProcessParametersAdd("@FileTemplateTypeCode", SqlDbType.Int, 4, mObj.FileTemplateTypeCode);
            }
            if (mObj.FileTemplateName != null)
            {
                builder.Append("FileTemplateName=@FileTemplateName,");
                this._DataProcess.ProcessParametersAdd("@FileTemplateName", SqlDbType.VarChar, 100, mObj.FileTemplateName);
            }
            if (mObj.SortCode != null)
            {
                builder.Append("SortCode=@SortCode,");
                this._DataProcess.ProcessParametersAdd("@SortCode", SqlDbType.VarChar, 500, mObj.SortCode);
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

        public int Delete(FileTemplateModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public FileTemplateModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, FileTemplateModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.FileTemplateTypeCode = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.FileTemplateName = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.SortCode = reader.GetString(3);
            }
        }

        public int Insert(FileTemplateModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<FileTemplateModel> Select()
        {
            FileTemplateQueryModel qmObj = new FileTemplateQueryModel();
            return this._Select(qmObj);
        }

        public List<FileTemplateModel> Select(FileTemplateQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(FileTemplateModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

