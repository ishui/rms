namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class YF_AssetMainApplyDAL
    {
        private SqlDataProcess _DataProcess;

        public YF_AssetMainApplyDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public YF_AssetMainApplyDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private YF_AssetMainApplyModel _DataBind(int Code)
        {
            YF_AssetMainApplyModel model = new YF_AssetMainApplyModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetMainApply ");
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
            builder.Append("delete from YF_AssetMainApply ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(YF_AssetMainApplyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO YF_AssetMainApply (");
            builder2.Append("VALUES(");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode,");
                builder2.Append("@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode,");
                builder2.Append("@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept,");
                builder2.Append("@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            }
            if (mObj.ApplyDate != null)
            {
                builder.Append("ApplyDate,");
                builder2.Append("@ApplyDate,");
                this._DataProcess.ProcessParametersAdd("@ApplyDate", SqlDbType.VarChar, 50, mObj.ApplyDate);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason,");
                builder2.Append("@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 50, mObj.Reason);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark,");
                builder2.Append("@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            builder.Append(") ");
            builder2.Append(") ");
            builder.Append(builder2.ToString());
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<YF_AssetMainApplyModel> _Select(YF_AssetMainApplyQueryModel qmObj)
        {
            List<YF_AssetMainApplyModel> list = new List<YF_AssetMainApplyModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetMainApply ");
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
                            YF_AssetMainApplyModel model = new YF_AssetMainApplyModel();
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

        private int _Update(YF_AssetMainApplyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update YF_AssetMainApply set ");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode=@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept=@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            }
            if (mObj.ApplyDate != null)
            {
                builder.Append("ApplyDate=@ApplyDate,");
                this._DataProcess.ProcessParametersAdd("@ApplyDate", SqlDbType.VarChar, 50, mObj.ApplyDate);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 50, mObj.Reason);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
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

        public int Delete(YF_AssetMainApplyModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public YF_AssetMainApplyModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, YF_AssetMainApplyModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ManageCode = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Dept = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.ApplyDate = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Reason = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Remark = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Status = reader.GetString(7);
            }
        }

        public int Insert(YF_AssetMainApplyModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<YF_AssetMainApplyModel> Select()
        {
            YF_AssetMainApplyQueryModel qmObj = new YF_AssetMainApplyQueryModel();
            return this._Select(qmObj);
        }

        public List<YF_AssetMainApplyModel> Select(YF_AssetMainApplyQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(YF_AssetMainApplyModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

