namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class LocaleViseCostDAL
    {
        private SqlDataProcess _DataProcess;

        public LocaleViseCostDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public LocaleViseCostDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private LocaleViseCostModel _DataBind(int Code)
        {
            LocaleViseCostModel model = new LocaleViseCostModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from LocaleViseCost ");
            builder.Append(" where ViseCostCode=@ViseCostCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ViseCostCode", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from LocaleViseCost ");
            builder.Append(" where ViseCostCode=@ViseCostCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ViseCostCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(LocaleViseCostModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into LocaleViseCost(");
            builder.Append("CostCode,CostBudgetSetCode,PBSCode,PBSType,Money,OtherMoney,OtherMoneyType,OtherMoneyRate,Remark,State,Flag,ViseCode,CheckMoney)");
            builder.Append(" values (");
            builder.Append("@CostCode,@CostBudgetSetCode,@PBSCode,@PBSType,@Money,@OtherMoney,@OtherMoneyType,@OtherMoneyRate,@Remark,@State,@Flag,@ViseCode,@CheckMoney) ");
            builder.Append("SELECT @ViseCostCode = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@CostCode", SqlDbType.VarChar, 50, mObj.CostCode);
            this._DataProcess.ProcessParametersAdd("@CostBudgetSetCode", SqlDbType.VarChar, 50, mObj.CostBudgetSetCode);
            this._DataProcess.ProcessParametersAdd("@PBSCode", SqlDbType.VarChar, 50, mObj.PBSCode);
            this._DataProcess.ProcessParametersAdd("@PBSType", SqlDbType.VarChar, 50, mObj.PBSType);
            this._DataProcess.ProcessParametersAdd("@Money", SqlDbType.Decimal, 9, mObj.Money);
            this._DataProcess.ProcessParametersAdd("@OtherMoney", SqlDbType.Decimal, 9, mObj.OtherMoney);
            this._DataProcess.ProcessParametersAdd("@OtherMoneyType", SqlDbType.VarChar, 50, mObj.OtherMoneyType);
            this._DataProcess.ProcessParametersAdd("@OtherMoneyRate", SqlDbType.Decimal, 9, mObj.OtherMoneyRate);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@State", SqlDbType.Int, 4, mObj.State);
            this._DataProcess.ProcessParametersAdd("@Flag", SqlDbType.Int, 4, mObj.Flag);
            this._DataProcess.ProcessParametersAdd("@ViseCode", SqlDbType.Int, 4, mObj.ViseCode);
            this._DataProcess.ProcessParametersAdd("@CheckMoney", SqlDbType.Decimal, 9, mObj.CheckMoney);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@ViseCostCode", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.ViseCostCode = (int) parameter.Value;
            return mObj.ViseCostCode;
        }

        private List<LocaleViseCostModel> _Select(LocaleViseCostQueryModel qmObj)
        {
            List<LocaleViseCostModel> list = new List<LocaleViseCostModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from LocaleViseCost ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY ViseCostCode desc");
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
                            LocaleViseCostModel model = new LocaleViseCostModel();
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

        private int _Update(LocaleViseCostModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update LocaleViseCost set ");
            if (mObj.CostCode != null)
            {
                builder.Append("CostCode=@CostCode,");
                this._DataProcess.ProcessParametersAdd("@CostCode", SqlDbType.VarChar, 50, mObj.CostCode);
            }
            if (mObj.CostBudgetSetCode != null)
            {
                builder.Append("CostBudgetSetCode=@CostBudgetSetCode,");
                this._DataProcess.ProcessParametersAdd("@CostBudgetSetCode", SqlDbType.VarChar, 50, mObj.CostBudgetSetCode);
            }
            if (mObj.PBSCode != null)
            {
                builder.Append("PBSCode=@PBSCode,");
                this._DataProcess.ProcessParametersAdd("@PBSCode", SqlDbType.VarChar, 50, mObj.PBSCode);
            }
            if (mObj.PBSType != null)
            {
                builder.Append("PBSType=@PBSType,");
                this._DataProcess.ProcessParametersAdd("@PBSType", SqlDbType.VarChar, 50, mObj.PBSType);
            }
            if (mObj.Money != 0M)
            {
                builder.Append("Money=@Money,");
                this._DataProcess.ProcessParametersAdd("@Money", SqlDbType.Decimal, 9, mObj.Money);
            }
            if (mObj.OtherMoney != 0M)
            {
                builder.Append("OtherMoney=@OtherMoney,");
                this._DataProcess.ProcessParametersAdd("@OtherMoney", SqlDbType.Decimal, 9, mObj.OtherMoney);
            }
            if (mObj.OtherMoneyType != null)
            {
                builder.Append("OtherMoneyType=@OtherMoneyType,");
                this._DataProcess.ProcessParametersAdd("@OtherMoneyType", SqlDbType.VarChar, 50, mObj.OtherMoneyType);
            }
            if (mObj.OtherMoneyRate != 0M)
            {
                builder.Append("OtherMoneyRate=@OtherMoneyRate,");
                this._DataProcess.ProcessParametersAdd("@OtherMoneyRate", SqlDbType.Decimal, 9, mObj.OtherMoneyRate);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            }
            if (mObj.State != 0)
            {
                builder.Append("State=@State,");
                this._DataProcess.ProcessParametersAdd("@State", SqlDbType.Int, 4, mObj.State);
            }
            if (mObj.Flag != 0)
            {
                builder.Append("Flag=@Flag,");
                this._DataProcess.ProcessParametersAdd("@Flag", SqlDbType.Int, 4, mObj.Flag);
            }
            if (mObj.ViseCode != 0)
            {
                builder.Append("ViseCode=@ViseCode,");
                this._DataProcess.ProcessParametersAdd("@ViseCode", SqlDbType.Int, 4, mObj.ViseCode);
            }
            if (mObj.CheckMoney != 0M)
            {
                builder.Append("CheckMoney=@CheckMoney,");
                this._DataProcess.ProcessParametersAdd("@CheckMoney", SqlDbType.Decimal, 9, mObj.CheckMoney);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where ViseCostCode=@ViseCostCode");
            this._DataProcess.ProcessParametersAdd("@ViseCostCode", SqlDbType.Int, 4, mObj.ViseCostCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(LocaleViseCostModel mObj)
        {
            return this._Delete(mObj.ViseCostCode);
        }

        public LocaleViseCostModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, LocaleViseCostModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.ViseCostCode = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.CostCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.CostBudgetSetCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.PBSCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.PBSType = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Money = reader.GetDecimal(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.OtherMoney = reader.GetDecimal(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.OtherMoneyType = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.OtherMoneyRate = reader.GetDecimal(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Remark = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.State = reader.GetInt32(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Flag = reader.GetInt32(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.ViseCode = reader.GetInt32(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.CheckMoney = reader.GetDecimal(13);
            }
        }

        public int Insert(LocaleViseCostModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<LocaleViseCostModel> Select()
        {
            LocaleViseCostQueryModel qmObj = new LocaleViseCostQueryModel();
            return this._Select(qmObj);
        }

        public List<LocaleViseCostModel> Select(LocaleViseCostQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(LocaleViseCostModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

