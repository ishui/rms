namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_PaymentSumDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_PaymentSumDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_PaymentSumDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_PaymentSumModel _DataBind(int Code)
        {
            TC_PaymentSumModel model = new TC_PaymentSumModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_PaymentSum ");
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
            builder.Append("delete from TC_PaymentSum ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_PaymentSumModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_PaymentSum(");
            builder.Append("CollectBillCode,SumUserCode,SendUnitCode,SumDateTime,Status)");
            builder.Append(" values (");
            builder.Append("@CollectBillCode,@SumUserCode,@SendUnitCode,@SumDateTime,@Status) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@CollectBillCode", SqlDbType.VarChar, 50, mObj.CollectBillCode);
            this._DataProcess.ProcessParametersAdd("@SumUserCode", SqlDbType.VarChar, 50, mObj.SumUserCode);
            this._DataProcess.ProcessParametersAdd("@SendUnitCode", SqlDbType.VarChar, 50, mObj.SendUnitCode);
            this._DataProcess.ProcessParametersAdd("@SumDateTime", SqlDbType.DateTime, 8, mObj.SumDateTime);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_PaymentSumModel> _Select(TC_PaymentSumQueryModel qmObj)
        {
            List<TC_PaymentSumModel> list = new List<TC_PaymentSumModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_PaymentSum ");
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
                            TC_PaymentSumModel model = new TC_PaymentSumModel();
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

        private int _Update(TC_PaymentSumModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_PaymentSum set ");
            if (mObj.CollectBillCode != null)
            {
                builder.Append("CollectBillCode=@CollectBillCode,");
                this._DataProcess.ProcessParametersAdd("@CollectBillCode", SqlDbType.VarChar, 50, mObj.CollectBillCode);
            }
            if (mObj.SumUserCode != null)
            {
                builder.Append("SumUserCode=@SumUserCode,");
                this._DataProcess.ProcessParametersAdd("@SumUserCode", SqlDbType.VarChar, 50, mObj.SumUserCode);
            }
            if (mObj.SendUnitCode != null)
            {
                builder.Append("SendUnitCode=@SendUnitCode,");
                this._DataProcess.ProcessParametersAdd("@SendUnitCode", SqlDbType.VarChar, 50, mObj.SendUnitCode);
            }
            if (mObj.SumDateTime != DateTime.MinValue)
            {
                builder.Append("SumDateTime=@SumDateTime,");
                this._DataProcess.ProcessParametersAdd("@SumDateTime", SqlDbType.DateTime, 8, mObj.SumDateTime);
            }
            if (mObj.AuditDateTime != DateTime.MinValue)
            {
                builder.Append("AuditDateTime=@AuditDateTime,");
                this._DataProcess.ProcessParametersAdd("@AuditDateTime", SqlDbType.DateTime, 8, mObj.AuditDateTime);
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

        public int Delete(TC_PaymentSumModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_PaymentSumModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_PaymentSumModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.CollectBillCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.SumUserCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.SendUnitCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.SumDateTime = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.AuditDateTime = reader.GetDateTime(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Status = reader.GetString(6);
            }
        }

        public int Insert(TC_PaymentSumModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_PaymentSumModel> Select()
        {
            TC_PaymentSumQueryModel qmObj = new TC_PaymentSumQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_PaymentSumModel> Select(TC_PaymentSumQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_PaymentSumModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

