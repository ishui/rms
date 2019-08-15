namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_SubmitAccountDtlDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_SubmitAccountDtlDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_SubmitAccountDtlDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_SubmitAccountDtlModel _DataBind(int Code)
        {
            GK_OA_SubmitAccountDtlModel model = new GK_OA_SubmitAccountDtlModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_SubmitAccountDtl ");
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
            builder.Append("delete from GK_OA_SubmitAccountDtl ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_SubmitAccountDtlModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_SubmitAccountDtl(");
            builder.Append("MastCode,Month,StandardCost,RealityCost,RemainCost,SumCost,Remark)");
            builder.Append(" values (");
            builder.Append("@MastCode,@Month,@StandardCost,@RealityCost,@RemainCost,@SumCost,@Remark) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            this._DataProcess.ProcessParametersAdd("@Month", SqlDbType.DateTime, 50, mObj.Month);
            this._DataProcess.ProcessParametersAdd("@StandardCost", SqlDbType.Decimal, 9, mObj.StandardCost);
            this._DataProcess.ProcessParametersAdd("@RealityCost", SqlDbType.Decimal, 9, mObj.RealityCost);
            this._DataProcess.ProcessParametersAdd("@RemainCost", SqlDbType.Decimal, 9, mObj.RemainCost);
            this._DataProcess.ProcessParametersAdd("@SumCost", SqlDbType.Decimal, 9, mObj.SumCost);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_SubmitAccountDtlModel> _Select(GK_OA_SubmitAccountDtlQueryModel qmObj)
        {
            List<GK_OA_SubmitAccountDtlModel> list = new List<GK_OA_SubmitAccountDtlModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_SubmitAccountDtl ");
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
                            GK_OA_SubmitAccountDtlModel model = new GK_OA_SubmitAccountDtlModel();
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

        private int _Update(GK_OA_SubmitAccountDtlModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_SubmitAccountDtl set ");
            if (mObj.MastCode != null)
            {
                builder.Append("MastCode=@MastCode,");
                this._DataProcess.ProcessParametersAdd("@MastCode", SqlDbType.VarChar, 50, mObj.MastCode);
            }
            if (mObj.Month != DateTime.MinValue)
            {
                builder.Append("Month=@Month,");
                this._DataProcess.ProcessParametersAdd("@Month", SqlDbType.DateTime, 50, mObj.Month);
            }
            if (mObj.StandardCost != 0M)
            {
                builder.Append("StandardCost=@StandardCost,");
                this._DataProcess.ProcessParametersAdd("@StandardCost", SqlDbType.Decimal, 9, mObj.StandardCost);
            }
            if (mObj.RealityCost != 0M)
            {
                builder.Append("RealityCost=@RealityCost,");
                this._DataProcess.ProcessParametersAdd("@RealityCost", SqlDbType.Decimal, 9, mObj.RealityCost);
            }
            if (mObj.RemainCost != 0M)
            {
                builder.Append("RemainCost=@RemainCost,");
                this._DataProcess.ProcessParametersAdd("@RemainCost", SqlDbType.Decimal, 9, mObj.RemainCost);
            }
            if (mObj.SumCost != 0M)
            {
                builder.Append("SumCost=@SumCost,");
                this._DataProcess.ProcessParametersAdd("@SumCost", SqlDbType.Decimal, 9, mObj.SumCost);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
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

        public int Delete(GK_OA_SubmitAccountDtlModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_SubmitAccountDtlModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_SubmitAccountDtlModel obj)
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
                obj.Month = reader.GetDateTime(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.StandardCost = reader.GetDecimal(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.RealityCost = reader.GetDecimal(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.RemainCost = reader.GetDecimal(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.SumCost = reader.GetDecimal(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Remark = reader.GetString(7);
            }
        }

        public int Insert(GK_OA_SubmitAccountDtlModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_SubmitAccountDtlModel> Select()
        {
            GK_OA_SubmitAccountDtlQueryModel qmObj = new GK_OA_SubmitAccountDtlQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_SubmitAccountDtlModel> Select(GK_OA_SubmitAccountDtlQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_SubmitAccountDtlModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

