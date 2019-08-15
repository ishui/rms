namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_ChangeStationNoticeDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_ChangeStationNoticeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_ChangeStationNoticeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_ChangeStationNoticeModel _DataBind(int Code)
        {
            GK_OA_ChangeStationNoticeModel model = new GK_OA_ChangeStationNoticeModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ChangeStationNotice ");
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
            builder.Append("delete from GK_OA_ChangeStationNotice ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_ChangeStationNoticeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_ChangeStationNotice(");
            builder.Append("SystemCode,FileCode,PersonName,Sex,InComDate,ChangeStationDate,OldUnitCode,NewUnitCode,OldStation,NewStation,OldStationLeavel,NewStationLeavel,Remark1,Remark2,Remark3,Reason,Status,Field1)");
            builder.Append(" values (");
            builder.Append("@SystemCode,@FileCode,@PersonName,@Sex,@InComDate,@ChangeStationDate,@OldUnitCode,@NewUnitCode,@OldStation,@NewStation,@OldStationLeavel,@NewStationLeavel,@Remark1,@Remark2,@Remark3,@Reason,@Status,@Field1) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@PersonName", SqlDbType.VarChar, 50, mObj.PersonName);
            this._DataProcess.ProcessParametersAdd("@Sex", SqlDbType.VarChar, 50, mObj.Sex);
            this._DataProcess.ProcessParametersAdd("@InComDate", SqlDbType.DateTime, 8, mObj.InComDate);
            this._DataProcess.ProcessParametersAdd("@ChangeStationDate", SqlDbType.DateTime, 8, mObj.ChangeStationDate);
            this._DataProcess.ProcessParametersAdd("@OldUnitCode", SqlDbType.VarChar, 50, mObj.OldUnitCode);
            this._DataProcess.ProcessParametersAdd("@NewUnitCode", SqlDbType.VarChar, 50, mObj.NewUnitCode);
            this._DataProcess.ProcessParametersAdd("@OldStation", SqlDbType.VarChar, 50, mObj.OldStation);
            this._DataProcess.ProcessParametersAdd("@NewStation", SqlDbType.VarChar, 50, mObj.NewStation);
            this._DataProcess.ProcessParametersAdd("@OldStationLeavel", SqlDbType.VarChar, 50, mObj.OldStationLeavel);
            this._DataProcess.ProcessParametersAdd("@NewStationLeavel", SqlDbType.VarChar, 50, mObj.NewStationLeavel);
            this._DataProcess.ProcessParametersAdd("@Remark1", SqlDbType.VarChar, 500, mObj.Remark1);
            this._DataProcess.ProcessParametersAdd("@Remark2", SqlDbType.VarChar, 500, mObj.Remark2);
            this._DataProcess.ProcessParametersAdd("@Remark3", SqlDbType.VarChar, 500, mObj.Remark3);
            this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 50, mObj.Reason);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_ChangeStationNoticeModel> _Select(GK_OA_ChangeStationNoticeQueryModel qmObj)
        {
            List<GK_OA_ChangeStationNoticeModel> list = new List<GK_OA_ChangeStationNoticeModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ChangeStationNotice ");
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
                            GK_OA_ChangeStationNoticeModel model = new GK_OA_ChangeStationNoticeModel();
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

        private int _Update(GK_OA_ChangeStationNoticeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_ChangeStationNotice set ");
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
            if (mObj.PersonName != null)
            {
                builder.Append("PersonName=@PersonName,");
                this._DataProcess.ProcessParametersAdd("@PersonName", SqlDbType.VarChar, 50, mObj.PersonName);
            }
            if (mObj.Sex != null)
            {
                builder.Append("Sex=@Sex,");
                this._DataProcess.ProcessParametersAdd("@Sex", SqlDbType.VarChar, 50, mObj.Sex);
            }
            if (mObj.InComDate != DateTime.MinValue)
            {
                builder.Append("InComDate=@InComDate,");
                this._DataProcess.ProcessParametersAdd("@InComDate", SqlDbType.DateTime, 8, mObj.InComDate);
            }
            if (mObj.ChangeStationDate != DateTime.MinValue)
            {
                builder.Append("ChangeStationDate=@ChangeStationDate,");
                this._DataProcess.ProcessParametersAdd("@ChangeStationDate", SqlDbType.DateTime, 8, mObj.ChangeStationDate);
            }
            if (mObj.OldUnitCode != null)
            {
                builder.Append("OldUnitCode=@OldUnitCode,");
                this._DataProcess.ProcessParametersAdd("@OldUnitCode", SqlDbType.VarChar, 50, mObj.OldUnitCode);
            }
            if (mObj.NewUnitCode != null)
            {
                builder.Append("NewUnitCode=@NewUnitCode,");
                this._DataProcess.ProcessParametersAdd("@NewUnitCode", SqlDbType.VarChar, 50, mObj.NewUnitCode);
            }
            if (mObj.OldStation != null)
            {
                builder.Append("OldStation=@OldStation,");
                this._DataProcess.ProcessParametersAdd("@OldStation", SqlDbType.VarChar, 50, mObj.OldStation);
            }
            if (mObj.NewStation != null)
            {
                builder.Append("NewStation=@NewStation,");
                this._DataProcess.ProcessParametersAdd("@NewStation", SqlDbType.VarChar, 50, mObj.NewStation);
            }
            if (mObj.OldStationLeavel != null)
            {
                builder.Append("OldStationLeavel=@OldStationLeavel,");
                this._DataProcess.ProcessParametersAdd("@OldStationLeavel", SqlDbType.VarChar, 50, mObj.OldStationLeavel);
            }
            if (mObj.NewStationLeavel != null)
            {
                builder.Append("NewStationLeavel=@NewStationLeavel,");
                this._DataProcess.ProcessParametersAdd("@NewStationLeavel", SqlDbType.VarChar, 50, mObj.NewStationLeavel);
            }
            if (mObj.Remark1 != null)
            {
                builder.Append("Remark1=@Remark1,");
                this._DataProcess.ProcessParametersAdd("@Remark1", SqlDbType.VarChar, 500, mObj.Remark1);
            }
            if (mObj.Remark2 != null)
            {
                builder.Append("Remark2=@Remark2,");
                this._DataProcess.ProcessParametersAdd("@Remark2", SqlDbType.VarChar, 500, mObj.Remark2);
            }
            if (mObj.Remark3 != null)
            {
                builder.Append("Remark3=@Remark3,");
                this._DataProcess.ProcessParametersAdd("@Remark3", SqlDbType.VarChar, 500, mObj.Remark3);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 50, mObj.Reason);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
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

        public int Delete(GK_OA_ChangeStationNoticeModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_ChangeStationNoticeModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_ChangeStationNoticeModel obj)
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
                obj.PersonName = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Sex = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.InComDate = reader.GetDateTime(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.ChangeStationDate = reader.GetDateTime(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.OldUnitCode = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.NewUnitCode = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.OldStation = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.NewStation = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.OldStationLeavel = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.NewStationLeavel = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.Remark1 = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.Remark2 = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.Remark3 = reader.GetString(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.Reason = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.Status = reader.GetString(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.Field1 = reader.GetString(0x12);
            }
        }

        public int Insert(GK_OA_ChangeStationNoticeModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_ChangeStationNoticeModel> Select()
        {
            GK_OA_ChangeStationNoticeQueryModel qmObj = new GK_OA_ChangeStationNoticeQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_ChangeStationNoticeModel> Select(GK_OA_ChangeStationNoticeQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_ChangeStationNoticeModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

