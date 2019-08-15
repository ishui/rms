namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_SmallGoodsDetailDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_SmallGoodsDetailDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_SmallGoodsDetailDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_SmallGoodsDetailModel _DataBind(int Code)
        {
            TC_OA_SmallGoodsDetailModel model = new TC_OA_SmallGoodsDetailModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_SmallGoodsDetail ");
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
            builder.Append("delete from TC_OA_SmallGoodsDetail ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_SmallGoodsDetailModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_SmallGoodsDetail(");
            builder.Append("MastCode,GoodName,Type,Numeber,UseWay,UseDate,IsSubmit)");
            builder.Append(" values (");
            builder.Append("@MastCode,@GoodName,@Type,@Numeber,@UseWay,@UseDate,@IsSubmit) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            this._DataProcess.ProcessParametersAdd("@GoodName", SqlDbType.VarChar, 50, mObj.GoodName);
            this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            this._DataProcess.ProcessParametersAdd("@Numeber", SqlDbType.Decimal, 9, mObj.Numeber);
            this._DataProcess.ProcessParametersAdd("@UseWay", SqlDbType.VarChar, 500, mObj.UseWay);
            this._DataProcess.ProcessParametersAdd("@UseDate", SqlDbType.DateTime, 8, mObj.UseDate);
            this._DataProcess.ProcessParametersAdd("@IsSubmit", SqlDbType.Int, 4, mObj.IsSubmit);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_SmallGoodsDetailModel> _Select(TC_OA_SmallGoodsDetailQueryModel qmObj)
        {
            List<TC_OA_SmallGoodsDetailModel> list = new List<TC_OA_SmallGoodsDetailModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_SmallGoodsDetail ");
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
                            TC_OA_SmallGoodsDetailModel model = new TC_OA_SmallGoodsDetailModel();
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

        private int _Update(TC_OA_SmallGoodsDetailModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_SmallGoodsDetail set ");
            if (mObj.MastCode != null)
            {
                builder.Append("MastCode=@MastCode,");
                this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            }
            if (mObj.GoodName != null)
            {
                builder.Append("GoodName=@GoodName,");
                this._DataProcess.ProcessParametersAdd("@GoodName", SqlDbType.VarChar, 50, mObj.GoodName);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.Numeber != 0M)
            {
                builder.Append("Numeber=@Numeber,");
                this._DataProcess.ProcessParametersAdd("@Numeber", SqlDbType.Decimal, 9, mObj.Numeber);
            }
            if (mObj.UseWay != null)
            {
                builder.Append("UseWay=@UseWay,");
                this._DataProcess.ProcessParametersAdd("@UseWay", SqlDbType.VarChar, 500, mObj.UseWay);
            }
            if (mObj.UseDate != DateTime.MinValue)
            {
                builder.Append("UseDate=@UseDate,");
                this._DataProcess.ProcessParametersAdd("@UseDate", SqlDbType.DateTime, 8, mObj.UseDate);
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

        public int Delete(TC_OA_SmallGoodsDetailModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_SmallGoodsDetailModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_SmallGoodsDetailModel obj)
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
                obj.GoodName = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Type = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Numeber = reader.GetDecimal(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.UseWay = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.UseDate = reader.GetDateTime(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.IsSubmit = reader.GetInt32(7);
            }
        }

        public int Insert(TC_OA_SmallGoodsDetailModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_SmallGoodsDetailModel> Select()
        {
            TC_OA_SmallGoodsDetailQueryModel qmObj = new TC_OA_SmallGoodsDetailQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_SmallGoodsDetailModel> Select(TC_OA_SmallGoodsDetailQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_SmallGoodsDetailModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

