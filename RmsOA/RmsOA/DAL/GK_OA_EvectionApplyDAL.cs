namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_EvectionApplyDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_EvectionApplyDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_EvectionApplyDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_EvectionApplyModel _DataBind(int Code)
        {
            GK_OA_EvectionApplyModel model = new GK_OA_EvectionApplyModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_EvectionApply ");
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
            builder.Append("delete from GK_OA_EvectionApply ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_EvectionApplyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO GK_OA_EvectionApply (");
            builder2.Append("VALUES(");
            if (mObj.Dept != null)
            {
                builder.Append("Dept,");
                builder2.Append("@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 200, mObj.Dept);
            }
            if (mObj.Users != null)
            {
                builder.Append("Users,");
                builder2.Append("@Users,");
                this._DataProcess.ProcessParametersAdd("@Users", SqlDbType.VarChar, 500, mObj.Users);
            }
            if (mObj.UserCount != 0)
            {
                builder.Append("UserCount,");
                builder2.Append("@UserCount,");
                this._DataProcess.ProcessParametersAdd("@UserCount", SqlDbType.Int, 4, mObj.UserCount);
            }
            if (mObj.ApplyDate != DateTime.MinValue)
            {
                builder.Append("ApplyDate,");
                builder2.Append("@ApplyDate,");
                this._DataProcess.ProcessParametersAdd("@ApplyDate", SqlDbType.DateTime, 8, mObj.ApplyDate);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason,");
                builder2.Append("@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 0x7d0, mObj.Reason);
            }
            if (mObj.Rount != null)
            {
                builder.Append("Rount,");
                builder2.Append("@Rount,");
                this._DataProcess.ProcessParametersAdd("@Rount", SqlDbType.VarChar, 200, mObj.Rount);
            }
            if (mObj.StartData != DateTime.MinValue)
            {
                builder.Append("StartData,");
                builder2.Append("@StartData,");
                this._DataProcess.ProcessParametersAdd("@StartData", SqlDbType.DateTime, 8, mObj.StartData);
            }
            if (mObj.EndDate != DateTime.MinValue)
            {
                builder.Append("EndDate,");
                builder2.Append("@EndDate,");
                this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.DateTime, 8, mObj.EndDate);
            }
            if (mObj.Vehicle != null)
            {
                builder.Append("Vehicle,");
                builder2.Append("@Vehicle,");
                this._DataProcess.ProcessParametersAdd("@Vehicle", SqlDbType.VarChar, 200, mObj.Vehicle);
            }
            if (mObj.LiveLevel != null)
            {
                builder.Append("LiveLevel,");
                builder2.Append("@LiveLevel,");
                this._DataProcess.ProcessParametersAdd("@LiveLevel", SqlDbType.VarChar, 50, mObj.LiveLevel);
            }
            if (mObj.BudgetMoney != 0M)
            {
                builder.Append("BudgetMoney,");
                builder2.Append("@BudgetMoney,");
                this._DataProcess.ProcessParametersAdd("@BudgetMoney", SqlDbType.Decimal, 9, mObj.BudgetMoney);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO,");
                builder2.Append("@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 100, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule,");
                builder2.Append("@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 100, mObj.SNRule);
            }
            if (mObj.Applyer != null)
            {
                builder.Append("Applyer,");
                builder2.Append("@Applyer,");
                this._DataProcess.ProcessParametersAdd("@Applyer", SqlDbType.VarChar, 20, mObj.Applyer);
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

        private List<GK_OA_EvectionApplyModel> _Select(GK_OA_EvectionApplyQueryModel qmObj)
        {
            List<GK_OA_EvectionApplyModel> list = new List<GK_OA_EvectionApplyModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_EvectionApply ");
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
                            GK_OA_EvectionApplyModel model = new GK_OA_EvectionApplyModel();
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

        private int _Update(GK_OA_EvectionApplyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_EvectionApply set ");
            if (mObj.Dept != null)
            {
                builder.Append("Dept=@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 200, mObj.Dept);
            }
            if (mObj.Users != null)
            {
                builder.Append("Users=@Users,");
                this._DataProcess.ProcessParametersAdd("@Users", SqlDbType.VarChar, 500, mObj.Users);
            }
            if (mObj.UserCount != 0)
            {
                builder.Append("UserCount=@UserCount,");
                this._DataProcess.ProcessParametersAdd("@UserCount", SqlDbType.Int, 4, mObj.UserCount);
            }
            if (mObj.ApplyDate != DateTime.MinValue)
            {
                builder.Append("ApplyDate=@ApplyDate,");
                this._DataProcess.ProcessParametersAdd("@ApplyDate", SqlDbType.DateTime, 8, mObj.ApplyDate);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 0x7d0, mObj.Reason);
            }
            if (mObj.Rount != null)
            {
                builder.Append("Rount=@Rount,");
                this._DataProcess.ProcessParametersAdd("@Rount", SqlDbType.VarChar, 200, mObj.Rount);
            }
            if (mObj.StartData != DateTime.MinValue)
            {
                builder.Append("StartData=@StartData,");
                this._DataProcess.ProcessParametersAdd("@StartData", SqlDbType.DateTime, 8, mObj.StartData);
            }
            if (mObj.EndDate != DateTime.MinValue)
            {
                builder.Append("EndDate=@EndDate,");
                this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.DateTime, 8, mObj.EndDate);
            }
            if (mObj.Vehicle != null)
            {
                builder.Append("Vehicle=@Vehicle,");
                this._DataProcess.ProcessParametersAdd("@Vehicle", SqlDbType.VarChar, 200, mObj.Vehicle);
            }
            if (mObj.LiveLevel != null)
            {
                builder.Append("LiveLevel=@LiveLevel,");
                this._DataProcess.ProcessParametersAdd("@LiveLevel", SqlDbType.VarChar, 50, mObj.LiveLevel);
            }
            if (mObj.BudgetMoney != 0M)
            {
                builder.Append("BudgetMoney=@BudgetMoney,");
                this._DataProcess.ProcessParametersAdd("@BudgetMoney", SqlDbType.Decimal, 9, mObj.BudgetMoney);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO=@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 100, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule=@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 100, mObj.SNRule);
            }
            if (mObj.Applyer != null)
            {
                builder.Append("Applyer=@Applyer,");
                this._DataProcess.ProcessParametersAdd("@Applyer", SqlDbType.VarChar, 20, mObj.Applyer);
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

        public int Delete(GK_OA_EvectionApplyModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_EvectionApplyModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_EvectionApplyModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Dept = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Users = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.UserCount = reader.GetInt32(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.ApplyDate = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Reason = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Rount = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.StartData = reader.GetDateTime(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.EndDate = reader.GetDateTime(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Vehicle = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.LiveLevel = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.BudgetMoney = reader.GetDecimal(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Status = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.QualityNO = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.SNRule = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.Applyer = reader.GetString(15);
            }
        }

        public int Insert(GK_OA_EvectionApplyModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_EvectionApplyModel> Select()
        {
            GK_OA_EvectionApplyQueryModel qmObj = new GK_OA_EvectionApplyQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_EvectionApplyModel> Select(GK_OA_EvectionApplyQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_EvectionApplyModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

