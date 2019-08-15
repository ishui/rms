namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_PurchasePriceDetailDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_PurchasePriceDetailDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_PurchasePriceDetailDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_PurchasePriceDetailModel _DataBind(int Code)
        {
            TC_OA_PurchasePriceDetailModel model = new TC_OA_PurchasePriceDetailModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_PurchasePriceDetail ");
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
            builder.Append("delete from TC_OA_PurchasePriceDetail ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_PurchasePriceDetailModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_PurchasePriceDetail(");
            builder.Append("MastCode,SupplyName,ConcertUser,ConcertTelephone,Price,Detail,IsSubmit)");
            builder.Append(" values (");
            builder.Append("@MastCode,@SupplyName,@ConcertUser,@ConcertTelephone,@Price,@Detail,@IsSubmit) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            this._DataProcess.ProcessParametersAdd("@SupplyName", SqlDbType.VarChar, 50, mObj.SupplyName);
            this._DataProcess.ProcessParametersAdd("@ConcertUser", SqlDbType.VarChar, 50, mObj.ConcertUser);
            this._DataProcess.ProcessParametersAdd("@ConcertTelephone", SqlDbType.VarChar, 50, mObj.ConcertTelephone);
            this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            this._DataProcess.ProcessParametersAdd("@Detail", SqlDbType.VarChar, 500, mObj.Detail);
            this._DataProcess.ProcessParametersAdd("@IsSubmit", SqlDbType.Int, 4, mObj.IsSubmit);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_PurchasePriceDetailModel> _Select(TC_OA_PurchasePriceDetailQueryModel qmObj)
        {
            List<TC_OA_PurchasePriceDetailModel> list = new List<TC_OA_PurchasePriceDetailModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_PurchasePriceDetail ");
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
                            TC_OA_PurchasePriceDetailModel model = new TC_OA_PurchasePriceDetailModel();
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

        private int _Update(TC_OA_PurchasePriceDetailModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_PurchasePriceDetail set ");
            if (mObj.MastCode != null)
            {
                builder.Append("MastCode=@MastCode,");
                this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            }
            if (mObj.SupplyName != null)
            {
                builder.Append("SupplyName=@SupplyName,");
                this._DataProcess.ProcessParametersAdd("@SupplyName", SqlDbType.VarChar, 50, mObj.SupplyName);
            }
            if (mObj.ConcertUser != null)
            {
                builder.Append("ConcertUser=@ConcertUser,");
                this._DataProcess.ProcessParametersAdd("@ConcertUser", SqlDbType.VarChar, 50, mObj.ConcertUser);
            }
            if (mObj.ConcertTelephone != null)
            {
                builder.Append("ConcertTelephone=@ConcertTelephone,");
                this._DataProcess.ProcessParametersAdd("@ConcertTelephone", SqlDbType.VarChar, 50, mObj.ConcertTelephone);
            }
            if (mObj.Price != 0M)
            {
                builder.Append("Price=@Price,");
                this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            }
            if (mObj.Detail != null)
            {
                builder.Append("Detail=@Detail,");
                this._DataProcess.ProcessParametersAdd("@Detail", SqlDbType.VarChar, 500, mObj.Detail);
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

        public int Delete(TC_OA_PurchasePriceDetailModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_PurchasePriceDetailModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_PurchasePriceDetailModel obj)
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
                obj.SupplyName = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.ConcertUser = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.ConcertTelephone = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Price = reader.GetDecimal(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Detail = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.IsSubmit = reader.GetInt32(7);
            }
        }

        public int Insert(TC_OA_PurchasePriceDetailModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_PurchasePriceDetailModel> Select()
        {
            TC_OA_PurchasePriceDetailQueryModel qmObj = new TC_OA_PurchasePriceDetailQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_PurchasePriceDetailModel> Select(TC_OA_PurchasePriceDetailQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_PurchasePriceDetailModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

