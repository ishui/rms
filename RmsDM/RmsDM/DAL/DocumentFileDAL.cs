namespace RmsDM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsDM.MODEL;

    public class DocumentFileDAL
    {
        private SqlDataProcess _DataProcess;

        public DocumentFileDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public DocumentFileDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private DocumentFileModel _DataBind(int Code)
        {
            DocumentFileModel model = new DocumentFileModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DocumentFile ");
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
            builder.Append("delete from DocumentFile ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(DocumentFileModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DocumentFile(");
            builder.Append("OperationType,ApplyUserCode,ApplyDepartmentCode,ApplyDateTime,FileTemplateCode,VersionNumber,SortCode,DoucmentMarkingSN,Subject,Content,Remark,ArchiveType,ArchiveState,ArchiveDatetime,AuditingState,AuditingDatetime,CreateDate,CreateUserCode,LastModifyDatetime,LastModifyByUserCode,DeleteFlag,FileCode,Counts,Leaves)");
            builder.Append(" values (");
            builder.Append("@OperationType,@ApplyUserCode,@ApplyDepartmentCode,@ApplyDateTime,@FileTemplateCode,@VersionNumber,@SortCode,@DoucmentMarkingSN,@Subject,@Content,@Remark,@ArchiveType,@ArchiveState,@ArchiveDatetime,@AuditingState,@AuditingDatetime,@CreateDate,@CreateUserCode,@LastModifyDatetime,@LastModifyByUserCode,@DeleteFlag,@FileCode,@Counts,@Leaves) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@OperationType", SqlDbType.VarChar, 50, mObj.OperationType);
            this._DataProcess.ProcessParametersAdd("@ApplyUserCode", SqlDbType.VarChar, 50, mObj.ApplyUserCode);
            this._DataProcess.ProcessParametersAdd("@ApplyDepartmentCode", SqlDbType.VarChar, 50, mObj.ApplyDepartmentCode);
            this._DataProcess.ProcessParametersAdd("@ApplyDateTime", SqlDbType.DateTime, 8, mObj.ApplyDateTime);
            this._DataProcess.ProcessParametersAdd("@FileTemplateCode", SqlDbType.Int, 4, mObj.FileTemplateCode);
            this._DataProcess.ProcessParametersAdd("@VersionNumber", SqlDbType.VarChar, 100, mObj.VersionNumber);
            this._DataProcess.ProcessParametersAdd("@SortCode", SqlDbType.VarChar, 0x3e8, mObj.SortCode);
            this._DataProcess.ProcessParametersAdd("@DoucmentMarkingSN", SqlDbType.VarChar, 100, mObj.DoucmentMarkingSN);
            this._DataProcess.ProcessParametersAdd("@Subject", SqlDbType.VarChar, 100, mObj.Subject);
            this._DataProcess.ProcessParametersAdd("@Content", SqlDbType.VarChar, 0x10, mObj.Content);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x1f40, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@ArchiveType", SqlDbType.VarChar, 200, mObj.ArchiveType);
            this._DataProcess.ProcessParametersAdd("@ArchiveState", SqlDbType.VarChar, 100, mObj.ArchiveState);
            this._DataProcess.ProcessParametersAdd("@ArchiveDatetime", SqlDbType.DateTime, 8, mObj.ArchiveDatetime);
            this._DataProcess.ProcessParametersAdd("@AuditingState", SqlDbType.VarChar, 100, mObj.AuditingState);
            this._DataProcess.ProcessParametersAdd("@AuditingDatetime", SqlDbType.DateTime, 8, mObj.AuditingDatetime);
            this._DataProcess.ProcessParametersAdd("@CreateDate", SqlDbType.DateTime, 8, mObj.CreateDate);
            this._DataProcess.ProcessParametersAdd("@CreateUserCode", SqlDbType.VarChar, 50, mObj.CreateUserCode);
            this._DataProcess.ProcessParametersAdd("@LastModifyDatetime", SqlDbType.DateTime, 8, mObj.LastModifyDatetime);
            this._DataProcess.ProcessParametersAdd("@LastModifyByUserCode", SqlDbType.VarChar, 50, mObj.LastModifyByUserCode);
            this._DataProcess.ProcessParametersAdd("@DeleteFlag", SqlDbType.VarChar, 50, mObj.DeleteFlag);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 100, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@Counts", SqlDbType.Int, 4, mObj.Counts);
            this._DataProcess.ProcessParametersAdd("@Leaves", SqlDbType.Int, 4, mObj.Leaves);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<DocumentFileModel> _Select(DocumentFileQueryModel qmObj)
        {
            List<DocumentFileModel> list = new List<DocumentFileModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DocumentFile ");
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
                            DocumentFileModel model = new DocumentFileModel();
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

        private int _Update(DocumentFileModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DocumentFile set ");
            if (mObj.OperationType != null)
            {
                builder.Append("OperationType=@OperationType,");
                this._DataProcess.ProcessParametersAdd("@OperationType", SqlDbType.VarChar, 50, mObj.OperationType);
            }
            if (mObj.ApplyUserCode != null)
            {
                builder.Append("ApplyUserCode=@ApplyUserCode,");
                this._DataProcess.ProcessParametersAdd("@ApplyUserCode", SqlDbType.VarChar, 50, mObj.ApplyUserCode);
            }
            if (mObj.ApplyDepartmentCode != null)
            {
                builder.Append("ApplyDepartmentCode=@ApplyDepartmentCode,");
                this._DataProcess.ProcessParametersAdd("@ApplyDepartmentCode", SqlDbType.VarChar, 50, mObj.ApplyDepartmentCode);
            }
            if (mObj.ApplyDateTime != DateTime.MinValue)
            {
                builder.Append("ApplyDateTime=@ApplyDateTime,");
                this._DataProcess.ProcessParametersAdd("@ApplyDateTime", SqlDbType.DateTime, 8, mObj.ApplyDateTime);
            }
            if (mObj.FileTemplateCode != 0)
            {
                builder.Append("FileTemplateCode=@FileTemplateCode,");
                this._DataProcess.ProcessParametersAdd("@FileTemplateCode", SqlDbType.Int, 4, mObj.FileTemplateCode);
            }
            if (mObj.VersionNumber != null)
            {
                builder.Append("VersionNumber=@VersionNumber,");
                this._DataProcess.ProcessParametersAdd("@VersionNumber", SqlDbType.VarChar, 100, mObj.VersionNumber);
            }
            if (mObj.SortCode != null)
            {
                builder.Append("SortCode=@SortCode,");
                this._DataProcess.ProcessParametersAdd("@SortCode", SqlDbType.VarChar, 0x3e8, mObj.SortCode);
            }
            if (mObj.DoucmentMarkingSN != null)
            {
                builder.Append("DoucmentMarkingSN=@DoucmentMarkingSN,");
                this._DataProcess.ProcessParametersAdd("@DoucmentMarkingSN", SqlDbType.VarChar, 100, mObj.DoucmentMarkingSN);
            }
            if (mObj.Subject != null)
            {
                builder.Append("Subject=@Subject,");
                this._DataProcess.ProcessParametersAdd("@Subject", SqlDbType.VarChar, 100, mObj.Subject);
            }
            if (mObj.Content != null)
            {
                builder.Append("Content=@Content,");
                this._DataProcess.ProcessParametersAdd("@Content", SqlDbType.VarChar, 0x10, mObj.Content);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x1f40, mObj.Remark);
            }
            if (mObj.ArchiveType != null)
            {
                builder.Append("ArchiveType=@ArchiveType,");
                this._DataProcess.ProcessParametersAdd("@ArchiveType", SqlDbType.VarChar, 200, mObj.ArchiveType);
            }
            if (mObj.ArchiveState != null)
            {
                builder.Append("ArchiveState=@ArchiveState,");
                this._DataProcess.ProcessParametersAdd("@ArchiveState", SqlDbType.VarChar, 100, mObj.ArchiveState);
            }
            if (mObj.ArchiveDatetime != DateTime.MinValue)
            {
                builder.Append("ArchiveDatetime=@ArchiveDatetime,");
                this._DataProcess.ProcessParametersAdd("@ArchiveDatetime", SqlDbType.DateTime, 8, mObj.ArchiveDatetime);
            }
            if (mObj.AuditingState != null)
            {
                builder.Append("AuditingState=@AuditingState,");
                this._DataProcess.ProcessParametersAdd("@AuditingState", SqlDbType.VarChar, 100, mObj.AuditingState);
            }
            if (mObj.AuditingDatetime != DateTime.MinValue)
            {
                builder.Append("AuditingDatetime=@AuditingDatetime,");
                this._DataProcess.ProcessParametersAdd("@AuditingDatetime", SqlDbType.DateTime, 8, mObj.AuditingDatetime);
            }
            if (mObj.CreateDate != DateTime.MinValue)
            {
                builder.Append("CreateDate=@CreateDate,");
                this._DataProcess.ProcessParametersAdd("@CreateDate", SqlDbType.DateTime, 8, mObj.CreateDate);
            }
            if (mObj.CreateUserCode != null)
            {
                builder.Append("CreateUserCode=@CreateUserCode,");
                this._DataProcess.ProcessParametersAdd("@CreateUserCode", SqlDbType.VarChar, 50, mObj.CreateUserCode);
            }
            if (mObj.LastModifyDatetime != DateTime.MinValue)
            {
                builder.Append("LastModifyDatetime=@LastModifyDatetime,");
                this._DataProcess.ProcessParametersAdd("@LastModifyDatetime", SqlDbType.DateTime, 8, mObj.LastModifyDatetime);
            }
            if (mObj.LastModifyByUserCode != null)
            {
                builder.Append("LastModifyByUserCode=@LastModifyByUserCode,");
                this._DataProcess.ProcessParametersAdd("@LastModifyByUserCode", SqlDbType.VarChar, 50, mObj.LastModifyByUserCode);
            }
            if (mObj.DeleteFlag != null)
            {
                builder.Append("DeleteFlag=@DeleteFlag,");
                this._DataProcess.ProcessParametersAdd("@DeleteFlag", SqlDbType.VarChar, 50, mObj.DeleteFlag);
            }
            if (mObj.FileCode != null)
            {
                builder.Append("FileCode=@FileCode,");
                this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 100, mObj.FileCode);
            }
            if (mObj.Counts != 0)
            {
                builder.Append("Counts=@Counts,");
                this._DataProcess.ProcessParametersAdd("@Counts", SqlDbType.Int, 4, mObj.Counts);
            }
            if (mObj.Leaves != 0)
            {
                builder.Append("Leaves=@Leaves,");
                this._DataProcess.ProcessParametersAdd("@Leaves", SqlDbType.Int, 4, mObj.Leaves);
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

        public int Delete(DocumentFileModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public DocumentFileModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, DocumentFileModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.OperationType = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.ApplyUserCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.ApplyDepartmentCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.ApplyDateTime = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.FileCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.FileTemplateCode = reader.GetInt32(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.VersionNumber = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.SortCode = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.DoucmentMarkingSN = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Subject = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Content = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Remark = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.ArchiveType = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.ArchiveState = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.ArchiveDatetime = reader.GetDateTime(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.AuditingState = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.AuditingDatetime = reader.GetDateTime(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.CreateDate = reader.GetDateTime(0x12);
            }
            if (reader.GetValue(0x13) != DBNull.Value)
            {
                obj.CreateUserCode = reader.GetString(0x13);
            }
            if (reader.GetValue(20) != DBNull.Value)
            {
                obj.LastModifyDatetime = reader.GetDateTime(20);
            }
            if (reader.GetValue(0x15) != DBNull.Value)
            {
                obj.LastModifyByUserCode = reader.GetString(0x15);
            }
            if (reader.GetValue(0x16) != DBNull.Value)
            {
                obj.DeleteFlag = reader.GetString(0x16);
            }
            if (reader.GetValue(0x17) != DBNull.Value)
            {
                obj.Counts = reader.GetInt32(0x17);
            }
            if (reader.GetValue(0x18) != DBNull.Value)
            {
                obj.Leaves = reader.GetInt32(0x18);
            }
        }

        public int Insert(DocumentFileModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<DocumentFileModel> Select()
        {
            DocumentFileQueryModel qmObj = new DocumentFileQueryModel();
            return this._Select(qmObj);
        }

        public List<DocumentFileModel> Select(DocumentFileQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(DocumentFileModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

