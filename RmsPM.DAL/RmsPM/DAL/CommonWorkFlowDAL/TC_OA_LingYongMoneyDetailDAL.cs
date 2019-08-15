namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_LingYongMoneyDetailDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_LingYongMoneyDetailDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_LingYongMoneyDetailDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_LingYongMoneyDetailModel _DataBind(int Code)
        {
            TC_OA_LingYongMoneyDetailModel model = new TC_OA_LingYongMoneyDetailModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_LingYongMoneyDetail ");
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
            builder.Append("delete from TC_OA_LingYongMoneyDetail ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_LingYongMoneyDetailModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_LingYongMoneyDetail(");
            builder.Append("MastCode,Name,Price,Number,TotalMoney,IsSubmit)");
            builder.Append(" values (");
            builder.Append("@MastCode,@Name,@Price,@Number,@TotalMoney,@IsSubmit) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            this._DataProcess.ProcessParametersAdd("@TotalMoney", SqlDbType.Decimal, 9, mObj.TotalMoney);
            this._DataProcess.ProcessParametersAdd("@IsSubmit", SqlDbType.Int, 4, mObj.IsSubmit);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_LingYongMoneyDetailModel> _Select(TC_OA_LingYongMoneyDetailQueryModel qmObj)
        {
            List<TC_OA_LingYongMoneyDetailModel> list = new List<TC_OA_LingYongMoneyDetailModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_LingYongMoneyDetail ");
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
                            TC_OA_LingYongMoneyDetailModel model = new TC_OA_LingYongMoneyDetailModel();
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

        private int _Update(TC_OA_LingYongMoneyDetailModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_LingYongMoneyDetail set ");
            if (mObj.MastCode != null)
            {
                builder.Append("MastCode=@MastCode,");
                this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            }
            if (mObj.Name != null)
            {
                builder.Append("Name=@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            }
            if (mObj.Price != 0M)
            {
                builder.Append("Price=@Price,");
                this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number=@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            }
            if (mObj.TotalMoney != 0M)
            {
                builder.Append("TotalMoney=@TotalMoney,");
                this._DataProcess.ProcessParametersAdd("@TotalMoney", SqlDbType.Decimal, 9, mObj.TotalMoney);
            }
            if (mObj.IsSubmit != 0)
            {
                builder.Append("IsSubmit=@IsSubmit,");
                this._DataProcess.ProcessParametersAdd("@IsSubmit", SqlDbType.Int, 4, mObj.IsSubmit);
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

        public int Delete(TC_OA_LingYongMoneyDetailModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_LingYongMoneyDetailModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_LingYongMoneyDetailModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.MastCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Name = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Price = reader.GetDecimal(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Number = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.TotalMoney = reader.GetDecimal(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.IsSubmit = reader.GetInt32(6);
            }
        }

        public int Insert(TC_OA_LingYongMoneyDetailModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_LingYongMoneyDetailModel> Select()
        {
            TC_OA_LingYongMoneyDetailQueryModel qmObj = new TC_OA_LingYongMoneyDetailQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_LingYongMoneyDetailModel> Select(TC_OA_LingYongMoneyDetailQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_LingYongMoneyDetailModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

