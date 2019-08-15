namespace RmsDM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsDM.MODEL;

    public class FileTemplateTypeDAL
    {
        private SqlDataProcess _DataProcess;

        public FileTemplateTypeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public FileTemplateTypeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private FileTemplateTypeModel _DataBind(int Code)
        {
            FileTemplateTypeModel model = new FileTemplateTypeModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FileTemplateType ");
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
            builder.Append("delete from FileTemplateType ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(FileTemplateTypeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileTemplateType(");
            builder.Append("FileTemplateTypeName,ParentCode,FullID,Deep,OrderByID)");
            builder.Append(" values (");
            builder.Append("@FileTemplateTypeName,@ParentCode,@FullID,@Deep,@OrderByID) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@FileTemplateTypeName", SqlDbType.VarChar, 50, mObj.FileTemplateTypeName);
            this._DataProcess.ProcessParametersAdd("@ParentCode", SqlDbType.Int, 4, mObj.ParentCode);
            this._DataProcess.ProcessParametersAdd("@FullID", SqlDbType.VarChar, 50, mObj.FullID);
            this._DataProcess.ProcessParametersAdd("@Deep", SqlDbType.Int, 4, mObj.Deep);
            this._DataProcess.ProcessParametersAdd("@OrderByID", SqlDbType.Int, 4, mObj.OrderByID);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<FileTemplateTypeModel> _Select(FileTemplateTypeQueryModel qmObj)
        {
            List<FileTemplateTypeModel> list = new List<FileTemplateTypeModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FileTemplateType ");
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
                            FileTemplateTypeModel model = new FileTemplateTypeModel();
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

        private int _Update(FileTemplateTypeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileTemplateType set ");
            if (mObj.FileTemplateTypeName != null)
            {
                builder.Append("FileTemplateTypeName=@FileTemplateTypeName,");
                this._DataProcess.ProcessParametersAdd("@FileTemplateTypeName", SqlDbType.VarChar, 50, mObj.FileTemplateTypeName);
            }
            if (mObj.ParentCode != 0)
            {
                builder.Append("ParentCode=@ParentCode,");
                this._DataProcess.ProcessParametersAdd("@ParentCode", SqlDbType.Int, 4, mObj.ParentCode);
            }
            if (mObj.FullID != null)
            {
                builder.Append("FullID=@FullID,");
                this._DataProcess.ProcessParametersAdd("@FullID", SqlDbType.VarChar, 50, mObj.FullID);
            }
            if (mObj.Deep != 0)
            {
                builder.Append("Deep=@Deep,");
                this._DataProcess.ProcessParametersAdd("@Deep", SqlDbType.Int, 4, mObj.Deep);
            }
            if (mObj.OrderByID != 0)
            {
                builder.Append("OrderByID=@OrderByID,");
                this._DataProcess.ProcessParametersAdd("@OrderByID", SqlDbType.Int, 4, mObj.OrderByID);
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

        public int Delete(FileTemplateTypeModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public FileTemplateTypeModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, FileTemplateTypeModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.FileTemplateTypeName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.ParentCode = reader.GetInt32(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.FullID = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Deep = reader.GetInt32(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.OrderByID = reader.GetInt32(5);
            }
        }

        public int Insert(FileTemplateTypeModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<FileTemplateTypeModel> Select()
        {
            FileTemplateTypeQueryModel qmObj = new FileTemplateTypeQueryModel();
            return this._Select(qmObj);
        }

        public List<FileTemplateTypeModel> Select(FileTemplateTypeQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(FileTemplateTypeModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

