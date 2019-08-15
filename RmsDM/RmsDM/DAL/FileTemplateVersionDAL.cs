namespace RmsDM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsDM.MODEL;

    public class FileTemplateVersionDAL
    {
        private SqlDataProcess _DataProcess;

        public FileTemplateVersionDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public FileTemplateVersionDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private FileTemplateVersionModel _DataBind(int Code)
        {
            FileTemplateVersionModel model = new FileTemplateVersionModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FileTemplateVersion ");
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
            builder.Append("delete from FileTemplateVersion ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(FileTemplateVersionModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FileTemplateVersion(");
            builder.Append("FileTemplateCode,WorkFlowProcedureName,VersionNumber,IsPigeonhole,PigeonholeTime,SaveTerm,RecordKind,MarkingSNRule,IsAvailability)");
            builder.Append(" values (");
            builder.Append("@FileTemplateCode,@WorkFlowProcedureName,@VersionNumber,@IsPigeonhole,@PigeonholeTime,@SaveTerm,@RecordKind,@MarkingSNRule,@IsAvailability) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@FileTemplateCode", SqlDbType.Int, 4, mObj.FileTemplateCode);
            this._DataProcess.ProcessParametersAdd("@WorkFlowProcedureName", SqlDbType.VarChar, 500, mObj.WorkFlowProcedureName);
            this._DataProcess.ProcessParametersAdd("@VersionNumber", SqlDbType.VarChar, 100, mObj.VersionNumber);
            this._DataProcess.ProcessParametersAdd("@IsPigeonhole", SqlDbType.VarChar, 50, mObj.IsPigeonhole);
            this._DataProcess.ProcessParametersAdd("@PigeonholeTime", SqlDbType.VarChar, 50, mObj.PigeonholeTime);
            this._DataProcess.ProcessParametersAdd("@SaveTerm", SqlDbType.VarChar, 50, mObj.SaveTerm);
            this._DataProcess.ProcessParametersAdd("@RecordKind", SqlDbType.VarChar, 100, mObj.RecordKind);
            this._DataProcess.ProcessParametersAdd("@MarkingSNRule", SqlDbType.VarChar, 100, mObj.MarkingSNRule);
            this._DataProcess.ProcessParametersAdd("@IsAvailability", SqlDbType.VarChar, 50, mObj.IsAvailability);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<FileTemplateVersionModel> _Select(FileTemplateVersionQueryModel qmObj)
        {
            List<FileTemplateVersionModel> list = new List<FileTemplateVersionModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FileTemplateVersion ");
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
                            FileTemplateVersionModel model = new FileTemplateVersionModel();
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

        private int _Update(FileTemplateVersionModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FileTemplateVersion set ");
            if (mObj.FileTemplateCode != 0)
            {
                builder.Append("FileTemplateCode=@FileTemplateCode,");
                this._DataProcess.ProcessParametersAdd("@FileTemplateCode", SqlDbType.Int, 4, mObj.FileTemplateCode);
            }
            if (mObj.WorkFlowProcedureName != null)
            {
                builder.Append("WorkFlowProcedureName=@WorkFlowProcedureName,");
                this._DataProcess.ProcessParametersAdd("@WorkFlowProcedureName", SqlDbType.VarChar, 500, mObj.WorkFlowProcedureName);
            }
            if (mObj.VersionNumber != null)
            {
                builder.Append("VersionNumber=@VersionNumber,");
                this._DataProcess.ProcessParametersAdd("@VersionNumber", SqlDbType.VarChar, 100, mObj.VersionNumber);
            }
            if (mObj.IsPigeonhole != null)
            {
                builder.Append("IsPigeonhole=@IsPigeonhole,");
                this._DataProcess.ProcessParametersAdd("@IsPigeonhole", SqlDbType.VarChar, 50, mObj.IsPigeonhole);
            }
            if (mObj.PigeonholeTime != null)
            {
                builder.Append("PigeonholeTime=@PigeonholeTime,");
                this._DataProcess.ProcessParametersAdd("@PigeonholeTime", SqlDbType.VarChar, 50, mObj.PigeonholeTime);
            }
            if (mObj.SaveTerm != null)
            {
                builder.Append("SaveTerm=@SaveTerm,");
                this._DataProcess.ProcessParametersAdd("@SaveTerm", SqlDbType.VarChar, 50, mObj.SaveTerm);
            }
            if (mObj.RecordKind != null)
            {
                builder.Append("RecordKind=@RecordKind,");
                this._DataProcess.ProcessParametersAdd("@RecordKind", SqlDbType.VarChar, 100, mObj.RecordKind);
            }
            if (mObj.MarkingSNRule != null)
            {
                builder.Append("MarkingSNRule=@MarkingSNRule,");
                this._DataProcess.ProcessParametersAdd("@MarkingSNRule", SqlDbType.VarChar, 100, mObj.MarkingSNRule);
            }
            if (mObj.IsAvailability != null)
            {
                builder.Append("IsAvailability=@IsAvailability,");
                this._DataProcess.ProcessParametersAdd("@IsAvailability", SqlDbType.VarChar, 50, mObj.IsAvailability);
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

        public int Delete(FileTemplateVersionModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public FileTemplateVersionModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, FileTemplateVersionModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.FileTemplateCode = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.WorkFlowProcedureName = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.VersionNumber = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.IsPigeonhole = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.PigeonholeTime = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.SaveTerm = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.RecordKind = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.MarkingSNRule = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.IsAvailability = reader.GetString(9);
            }
        }

        public int Insert(FileTemplateVersionModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<FileTemplateVersionModel> Select()
        {
            FileTemplateVersionQueryModel qmObj = new FileTemplateVersionQueryModel();
            return this._Select(qmObj);
        }

        public List<FileTemplateVersionModel> Select(FileTemplateVersionQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(FileTemplateVersionModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

