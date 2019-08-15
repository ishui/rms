namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TCheckPaymentDAL
    {
        private SqlDataProcess _DataProcess;

        public TCheckPaymentDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TCheckPaymentDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TCheckPaymentModel _DataBind(int Code)
        {
            TCheckPaymentModel model = new TCheckPaymentModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TCheckPayment ");
            builder.Append(" where TCheckPaymentcode=@TCheckPaymentcode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@TCheckPaymentcode", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from TCheckPayment ");
            builder.Append(" where TCheckPaymentcode=@TCheckPaymentcode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@TCheckPaymentcode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private List<TCheckPaymentModel> _Select(TCheckPaymentQueryModel qmObj)
        {
            List<TCheckPaymentModel> list = new List<TCheckPaymentModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TCheckPayment ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY TCheckPaymentcode desc");
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
                            TCheckPaymentModel model = new TCheckPaymentModel();
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

        private int _Update(TCheckPaymentModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TCheckPayment set ");
            if (mObj.PaymentUnit != null)
            {
                builder.Append("PaymentUnit=@PaymentUnit,");
                this._DataProcess.ProcessParametersAdd("@PaymentUnit", SqlDbType.VarChar, 100, mObj.PaymentUnit);
            }
            if (mObj.PaymentRemark != null)
            {
                builder.Append("PaymentRemark=@PaymentRemark,");
                this._DataProcess.ProcessParametersAdd("@PaymentRemark", SqlDbType.VarChar, 0x1f40, mObj.PaymentRemark);
            }
            if (mObj.AcceptUnit != null)
            {
                builder.Append("AcceptUnit=@AcceptUnit,");
                this._DataProcess.ProcessParametersAdd("@AcceptUnit", SqlDbType.VarChar, 100, mObj.AcceptUnit);
            }
            if (mObj.AcceptBank != null)
            {
                builder.Append("AcceptBank=@AcceptBank,");
                this._DataProcess.ProcessParametersAdd("@AcceptBank", SqlDbType.VarChar, 100, mObj.AcceptBank);
            }
            if (mObj.AcceptAccounts != null)
            {
                builder.Append("AcceptAccounts=@AcceptAccounts,");
                this._DataProcess.ProcessParametersAdd("@AcceptAccounts", SqlDbType.VarChar, 100, mObj.AcceptAccounts);
            }
            if (mObj.AcceptMoney != 0M)
            {
                builder.Append("AcceptMoney=@AcceptMoney,");
                this._DataProcess.ProcessParametersAdd("@AcceptMoney", SqlDbType.Decimal, 9, mObj.AcceptMoney);
            }
            if (mObj.AcceptMoneyType != null)
            {
                builder.Append("AcceptMoneyType=@AcceptMoneyType,");
                this._DataProcess.ProcessParametersAdd("@AcceptMoneyType", SqlDbType.VarChar, 10, mObj.AcceptMoneyType);
            }
            if (mObj.PaymentBank != null)
            {
                builder.Append("PaymentBank=@PaymentBank,");
                this._DataProcess.ProcessParametersAdd("@PaymentBank", SqlDbType.VarChar, 100, mObj.PaymentBank);
            }
            if (mObj.PaymentAccounts != null)
            {
                builder.Append("PaymentAccounts=@PaymentAccounts,");
                this._DataProcess.ProcessParametersAdd("@PaymentAccounts", SqlDbType.VarChar, 100, mObj.PaymentAccounts);
            }
            if (mObj.PaymentTicketMark != null)
            {
                builder.Append("PaymentTicketMark=@PaymentTicketMark,");
                this._DataProcess.ProcessParametersAdd("@PaymentTicketMark", SqlDbType.VarChar, 100, mObj.PaymentTicketMark);
            }
            if (mObj.PaymentTicketDate != null)
            {
                builder.Append("PaymentTicketDate=@PaymentTicketDate,");
                this._DataProcess.ProcessParametersAdd("@PaymentTicketDate", SqlDbType.VarChar, 50, mObj.PaymentTicketDate);
            }
            if (mObj.PaymentMoney != 0M)
            {
                builder.Append("PaymentMoney=@PaymentMoney,");
                this._DataProcess.ProcessParametersAdd("@PaymentMoney", SqlDbType.Decimal, 9, mObj.PaymentMoney);
            }
            if (mObj.PaymentMoneyType != null)
            {
                builder.Append("PaymentMoneyType=@PaymentMoneyType,");
                this._DataProcess.ProcessParametersAdd("@PaymentMoneyType", SqlDbType.VarChar, 10, mObj.PaymentMoneyType);
            }
            if (mObj.PaymentCodition != null)
            {
                builder.Append("PaymentCodition=@PaymentCodition,");
                this._DataProcess.ProcessParametersAdd("@PaymentCodition", SqlDbType.VarChar, 0xfa0, mObj.PaymentCodition);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0xfa0, mObj.Remark);
            }
            if (mObj.State != null)
            {
                builder.Append("State=@State,");
                this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 10, mObj.State);
            }
            if (mObj.Flag != null)
            {
                builder.Append("Flag=@Flag,");
                this._DataProcess.ProcessParametersAdd("@Flag", SqlDbType.VarChar, 10, mObj.Flag);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where TCheckPaymentcode=@TCheckPaymentcode");
            this._DataProcess.ProcessParametersAdd("@TCheckPaymentcode", SqlDbType.Int, 4, mObj.TCheckPaymentcode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(TCheckPaymentModel mObj)
        {
            return this._Delete(Convert.ToInt32(mObj.TCheckPaymentcode));
        }

        public TCheckPaymentModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TCheckPaymentModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.TCheckPaymentcode = reader.GetString(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.PaymentUnit = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.PaymentRemark = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.AcceptUnit = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.AcceptBank = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.AcceptAccounts = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.AcceptMoney = reader.GetDecimal(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.AcceptMoneyType = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.PaymentBank = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.PaymentAccounts = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.PaymentTicketMark = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.PaymentTicketDate = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.PaymentMoney = reader.GetDecimal(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.PaymentMoneyType = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.PaymentCodition = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.Remark = reader.GetString(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.State = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.Flag = reader.GetString(0x11);
            }
        }

        public List<TCheckPaymentModel> Select()
        {
            TCheckPaymentQueryModel qmObj = new TCheckPaymentQueryModel();
            return this._Select(qmObj);
        }

        public List<TCheckPaymentModel> Select(TCheckPaymentQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TCheckPaymentModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

