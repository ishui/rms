namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_ArchivesCopyDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_ArchivesCopyDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_ArchivesCopyDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_ArchivesCopyModel _DataBind(int Code)
        {
            GK_OA_ArchivesCopyModel model = new GK_OA_ArchivesCopyModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ArchivesCopy ");
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
            builder.Append("delete from GK_OA_ArchivesCopy ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_ArchivesCopyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_ArchivesCopy(");
            builder.Append("UnitCode,RegiesterDate,FileName,FileCode,UsePerson,ReturnDate,Reason,ArchivesType,Status,SystemCode,FileSystemCode)");
            builder.Append(" values (");
            builder.Append("@UnitCode,@RegiesterDate,@FileName,@FileCode,@UsePerson,@ReturnDate,@Reason,@ArchivesType,@Status,@SystemCode,@FileSystemCode) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@RegiesterDate", SqlDbType.DateTime, 8, mObj.RegiesterDate);
            this._DataProcess.ProcessParametersAdd("@FileName", SqlDbType.VarChar, 50, mObj.FileName);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@UsePerson", SqlDbType.VarChar, 50, mObj.UsePerson);
            this._DataProcess.ProcessParametersAdd("@ReturnDate", SqlDbType.DateTime, 8, mObj.ReturnDate);
            this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 500, mObj.Reason);
            this._DataProcess.ProcessParametersAdd("@ArchivesType", SqlDbType.VarChar, 50, mObj.ArchivesType);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            this._DataProcess.ProcessParametersAdd("@FileSystemCode", SqlDbType.VarChar, 50, mObj.FileSystemCode);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_ArchivesCopyModel> _Select(GK_OA_ArchivesCopyQueryModel qmObj)
        {
            List<GK_OA_ArchivesCopyModel> list = new List<GK_OA_ArchivesCopyModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ArchivesCopy ");
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
                            GK_OA_ArchivesCopyModel model = new GK_OA_ArchivesCopyModel();
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

        private int _Update(GK_OA_ArchivesCopyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_ArchivesCopy set ");
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.RegiesterDate != DateTime.MinValue)
            {
                builder.Append("RegiesterDate=@RegiesterDate,");
                this._DataProcess.ProcessParametersAdd("@RegiesterDate", SqlDbType.DateTime, 8, mObj.RegiesterDate);
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
            if (mObj.UsePerson != null)
            {
                builder.Append("UsePerson=@UsePerson,");
                this._DataProcess.ProcessParametersAdd("@UsePerson", SqlDbType.VarChar, 50, mObj.UsePerson);
            }
            if (mObj.ReturnDate != DateTime.MinValue)
            {
                builder.Append("ReturnDate=@ReturnDate,");
                this._DataProcess.ProcessParametersAdd("@ReturnDate", SqlDbType.DateTime, 8, mObj.ReturnDate);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 500, mObj.Reason);
            }
            if (mObj.ArchivesType != null)
            {
                builder.Append("ArchivesType=@ArchivesType,");
                this._DataProcess.ProcessParametersAdd("@ArchivesType", SqlDbType.VarChar, 50, mObj.ArchivesType);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
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

        public int Delete(GK_OA_ArchivesCopyModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_ArchivesCopyModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_ArchivesCopyModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.RegiesterDate = reader.GetDateTime(2);
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
                obj.UsePerson = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.ReturnDate = reader.GetDateTime(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Reason = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.ArchivesType = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Status = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.SystemCode = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.FileSystemCode = reader.GetString(11);
            }
        }

        public int Insert(GK_OA_ArchivesCopyModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_ArchivesCopyModel> Select()
        {
            GK_OA_ArchivesCopyQueryModel qmObj = new GK_OA_ArchivesCopyQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_ArchivesCopyModel> Select(GK_OA_ArchivesCopyQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_ArchivesCopyModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

