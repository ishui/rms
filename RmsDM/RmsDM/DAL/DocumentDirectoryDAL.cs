namespace RmsDM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsDM.MODEL;

    public class DocumentDirectoryDAL
    {
        private SqlDataProcess _DataProcess;

        public DocumentDirectoryDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public DocumentDirectoryDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private DocumentDirectoryModel _DataBind(int Code)
        {
            DocumentDirectoryModel model = new DocumentDirectoryModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DocumentDirectory ");
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
            builder.Append("delete from DocumentDirectory ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(DocumentDirectoryModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DocumentDirectory(");
            builder.Append("DirectoryName,DepartmentCode,DirectoryNodeCode,FileTemplateCode,ParentCode,FullID,Deep,CreateDate,OrderByID)");
            builder.Append(" values (");
            builder.Append("@DirectoryName,@DepartmentCode,@DirectoryNodeCode,@FileTemplateCode,@ParentCode,@FullID,@Deep,@CreateDate,@OrderByID) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@DirectoryName", SqlDbType.VarChar, 50, mObj.DirectoryName);
            this._DataProcess.ProcessParametersAdd("@DepartmentCode", SqlDbType.VarChar, 50, mObj.DepartmentCode);
            this._DataProcess.ProcessParametersAdd("@DirectoryNodeCode", SqlDbType.VarChar, 50, mObj.DirectoryNodeCode);
            this._DataProcess.ProcessParametersAdd("@FileTemplateCode", SqlDbType.Int, 4, mObj.FileTemplateCode);
            this._DataProcess.ProcessParametersAdd("@ParentCode", SqlDbType.Int, 4, mObj.ParentCode);
            this._DataProcess.ProcessParametersAdd("@FullID", SqlDbType.VarChar, 50, mObj.FullID);
            this._DataProcess.ProcessParametersAdd("@Deep", SqlDbType.Int, 4, mObj.Deep);
            this._DataProcess.ProcessParametersAdd("@CreateDate", SqlDbType.DateTime, 8, mObj.CreateDate);
            this._DataProcess.ProcessParametersAdd("@OrderByID", SqlDbType.Int, 4, mObj.OrderByID);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<DocumentDirectoryModel> _Select(DocumentDirectoryQueryModel qmObj)
        {
            List<DocumentDirectoryModel> list = new List<DocumentDirectoryModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DocumentDirectory ");
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
                            DocumentDirectoryModel model = new DocumentDirectoryModel();
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

        private int _Update(DocumentDirectoryModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DocumentDirectory set ");
            if (mObj.DirectoryName != null)
            {
                builder.Append("DirectoryName=@DirectoryName,");
                this._DataProcess.ProcessParametersAdd("@DirectoryName", SqlDbType.VarChar, 50, mObj.DirectoryName);
            }
            if (mObj.DepartmentCode != null)
            {
                builder.Append("DepartmentCode=@DepartmentCode,");
                this._DataProcess.ProcessParametersAdd("@DepartmentCode", SqlDbType.VarChar, 50, mObj.DepartmentCode);
            }
            if (mObj.DirectoryNodeCode != null)
            {
                builder.Append("DirectoryNodeCode=@DirectoryNodeCode,");
                this._DataProcess.ProcessParametersAdd("@DirectoryNodeCode", SqlDbType.VarChar, 50, mObj.DirectoryNodeCode);
            }
            if (mObj.FileTemplateCode != 0)
            {
                builder.Append("FileTemplateCode=@FileTemplateCode,");
                this._DataProcess.ProcessParametersAdd("@FileTemplateCode", SqlDbType.Int, 4, mObj.FileTemplateCode);
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
            if (mObj.CreateDate != DateTime.MinValue)
            {
                builder.Append("CreateDate=@CreateDate,");
                this._DataProcess.ProcessParametersAdd("@CreateDate", SqlDbType.DateTime, 8, mObj.CreateDate);
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

        public int Delete(DocumentDirectoryModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public DocumentDirectoryModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, DocumentDirectoryModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.DirectoryName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.DepartmentCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.DirectoryNodeCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.FileTemplateCode = reader.GetInt32(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.ParentCode = reader.GetInt32(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.FullID = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Deep = reader.GetInt32(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.CreateDate = reader.GetDateTime(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.OrderByID = reader.GetInt32(9);
            }
        }

        public int Insert(DocumentDirectoryModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<DocumentDirectoryModel> Select()
        {
            DocumentDirectoryQueryModel qmObj = new DocumentDirectoryQueryModel();
            return this._Select(qmObj);
        }

        public List<DocumentDirectoryModel> Select(DocumentDirectoryQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(DocumentDirectoryModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

