namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TCheckPaymentSumDAL
    {
        private SqlDataProcess _DataProcess;

        public TCheckPaymentSumDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TCheckPaymentSumDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TCheckPaymentSumModel _DataBind(int Code)
        {
            TCheckPaymentSumModel model = new TCheckPaymentSumModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TCheckPaymentSum ");
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
            builder.Append("delete from TCheckPaymentSum ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TCheckPaymentSumModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TCheckPaymentSum(");
            builder.Append("SendUnit,SumDate,Auditing)");
            builder.Append(" values (");
            builder.Append("@SendUnit,@SumDate,@Auditing) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SendUnit", SqlDbType.VarChar, 50, mObj.SendUnit);
            this._DataProcess.ProcessParametersAdd("@SumDate", SqlDbType.DateTime, 8, mObj.SumDate);
            this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.VarChar, 50, mObj.Auditing);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TCheckPaymentSumModel> _Select(TCheckPaymentSumQueryModel qmObj)
        {
            List<TCheckPaymentSumModel> list = new List<TCheckPaymentSumModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TCheckPaymentSum ");
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
                            TCheckPaymentSumModel model = new TCheckPaymentSumModel();
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

        private int _Update(TCheckPaymentSumModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TCheckPaymentSum set ");
            if (mObj.SendUnit != null)
            {
                builder.Append("SendUnit=@SendUnit,");
                this._DataProcess.ProcessParametersAdd("@SendUnit", SqlDbType.VarChar, 50, mObj.SendUnit);
            }
            if (mObj.SumDate != DateTime.MinValue)
            {
                builder.Append("SumDate=@SumDate,");
                this._DataProcess.ProcessParametersAdd("@SumDate", SqlDbType.DateTime, 8, mObj.SumDate);
            }
            if (mObj.Auditing != null)
            {
                builder.Append("Auditing=@Auditing,");
                this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.VarChar, 50, mObj.Auditing);
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

        public int Delete(TCheckPaymentSumModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TCheckPaymentSumModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TCheckPaymentSumModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.SendUnit = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.SumDate = reader.GetDateTime(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Auditing = reader.GetString(3);
            }
        }

        public int Insert(TCheckPaymentSumModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TCheckPaymentSumModel> Select()
        {
            TCheckPaymentSumQueryModel qmObj = new TCheckPaymentSumQueryModel();
            return this._Select(qmObj);
        }

        public List<TCheckPaymentSumModel> Select(TCheckPaymentSumQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TCheckPaymentSumModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

