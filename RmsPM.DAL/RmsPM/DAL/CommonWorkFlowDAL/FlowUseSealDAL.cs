namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class FlowUseSealDAL
    {
        private SqlDataProcess _DataProcess;

        public FlowUseSealDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public FlowUseSealDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private FlowUseSealModel _DataBind(int Code)
        {
            FlowUseSealModel model = new FlowUseSealModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FlowUseSeal ");
            builder.Append(" where UseSealCode=@UseSealCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UseSealCode", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from FlowUseSeal ");
            builder.Append(" where UseSealCode=@UseSealCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UseSealCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(FlowUseSealModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into FlowUseSeal(");
            builder.Append("ApplySealDate,UseSealDate,QianShouDate,UserCode,UnitCode,DepartmentCode,FileName,ArchivesCode,Remark,KeptFlag,Auditing)");
            builder.Append(" values (");
            builder.Append("@ApplySealDate,@UseSealDate,@QianShouDate,@UserCode,@UnitCode,@DepartmentCode,@FileName,@ArchivesCode,@Remark,@KeptFlag,@Auditing) ");
            builder.Append("SELECT @UseSealCode = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ApplySealDate", SqlDbType.DateTime, 8, mObj.ApplySealDate);
            this._DataProcess.ProcessParametersAdd("@UseSealDate", SqlDbType.DateTime, 8, mObj.UseSealDate);
            this._DataProcess.ProcessParametersAdd("@QianShouDate", SqlDbType.DateTime, 8, mObj.QianShouDate);
            this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@DepartmentCode", SqlDbType.VarChar, 50, mObj.DepartmentCode);
            this._DataProcess.ProcessParametersAdd("@FileName", SqlDbType.VarChar, 50, mObj.FileName);
            this._DataProcess.ProcessParametersAdd("@ArchivesCode", SqlDbType.VarChar, 50, mObj.ArchivesCode);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@KeptFlag", SqlDbType.VarChar, 50, mObj.KeptFlag);
            this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.VarChar, 50, mObj.Auditing);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@UseSealCode", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.UseSealCode = (int) parameter.Value;
            return mObj.UseSealCode;
        }

        private List<FlowUseSealModel> _Select(FlowUseSealQueryModel qmObj)
        {
            List<FlowUseSealModel> list = new List<FlowUseSealModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from FlowUseSeal ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY UseSealCode desc");
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
                            FlowUseSealModel model = new FlowUseSealModel();
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

        private int _Update(FlowUseSealModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update FlowUseSeal set ");
            if (mObj.ApplySealDate != DateTime.MinValue)
            {
                builder.Append("ApplySealDate=@ApplySealDate,");
                this._DataProcess.ProcessParametersAdd("@ApplySealDate", SqlDbType.DateTime, 8, mObj.ApplySealDate);
            }
            if (mObj.UseSealDate != DateTime.MinValue)
            {
                builder.Append("UseSealDate=@UseSealDate,");
                this._DataProcess.ProcessParametersAdd("@UseSealDate", SqlDbType.DateTime, 8, mObj.UseSealDate);
            }
            if (mObj.QianShouDate != DateTime.MinValue)
            {
                builder.Append("QianShouDate=@QianShouDate,");
                this._DataProcess.ProcessParametersAdd("@QianShouDate", SqlDbType.DateTime, 8, mObj.QianShouDate);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.DepartmentCode != null)
            {
                builder.Append("DepartmentCode=@DepartmentCode,");
                this._DataProcess.ProcessParametersAdd("@DepartmentCode", SqlDbType.VarChar, 50, mObj.DepartmentCode);
            }
            if (mObj.FileName != null)
            {
                builder.Append("FileName=@FileName,");
                this._DataProcess.ProcessParametersAdd("@FileName", SqlDbType.VarChar, 50, mObj.FileName);
            }
            if (mObj.ArchivesCode != null)
            {
                builder.Append("ArchivesCode=@ArchivesCode,");
                this._DataProcess.ProcessParametersAdd("@ArchivesCode", SqlDbType.VarChar, 50, mObj.ArchivesCode);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            }
            if (mObj.KeptFlag != null)
            {
                builder.Append("KeptFlag=@KeptFlag,");
                this._DataProcess.ProcessParametersAdd("@KeptFlag", SqlDbType.VarChar, 50, mObj.KeptFlag);
            }
            if (mObj.Auditing != null)
            {
                builder.Append("Auditing=@Auditing,");
                this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.VarChar, 50, mObj.Auditing);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where UseSealCode=@UseSealCode");
            this._DataProcess.ProcessParametersAdd("@UseSealCode", SqlDbType.Int, 4, mObj.UseSealCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(FlowUseSealModel mObj)
        {
            return this._Delete(mObj.UseSealCode);
        }

        public FlowUseSealModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, FlowUseSealModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.UseSealCode = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ApplySealDate = reader.GetDateTime(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UseSealDate = reader.GetDateTime(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.QianShouDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.DepartmentCode = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.FileName = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.ArchivesCode = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Remark = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.KeptFlag = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Auditing = reader.GetString(11);
            }
        }

        public int Insert(FlowUseSealModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<FlowUseSealModel> Select()
        {
            FlowUseSealQueryModel qmObj = new FlowUseSealQueryModel();
            return this._Select(qmObj);
        }

        public List<FlowUseSealModel> Select(FlowUseSealQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(FlowUseSealModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

