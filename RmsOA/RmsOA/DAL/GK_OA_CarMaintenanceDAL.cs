namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_CarMaintenanceDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_CarMaintenanceDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_CarMaintenanceDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_CarMaintenanceModel _DataBind(int Code)
        {
            GK_OA_CarMaintenanceModel model = new GK_OA_CarMaintenanceModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_CarMaintenance ");
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
            builder.Append("delete from GK_OA_CarMaintenance ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_CarMaintenanceModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_CarMaintenance(");
            builder.Append("Car_code,MDate,Mil,MValue,MPrice)");
            builder.Append(" values (");
            builder.Append("@Car_code,@MDate,@Mil,@MValue,@MPrice) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Car_code", SqlDbType.VarChar, 4, mObj.Car_code);
            this._DataProcess.ProcessParametersAdd("@MDate", SqlDbType.DateTime, 8, mObj.MDate);
            this._DataProcess.ProcessParametersAdd("@Mil", SqlDbType.Decimal, 9, mObj.Mil);
            this._DataProcess.ProcessParametersAdd("@MValue", SqlDbType.VarChar, 0x3e8, mObj.MValue);
            this._DataProcess.ProcessParametersAdd("@MPrice", SqlDbType.Decimal, 9, mObj.MPrice);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_CarMaintenanceModel> _Select(GK_OA_CarMaintenanceQueryModel qmObj)
        {
            List<GK_OA_CarMaintenanceModel> list = new List<GK_OA_CarMaintenanceModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_CarMaintenance ");
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
                            GK_OA_CarMaintenanceModel model = new GK_OA_CarMaintenanceModel();
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

        private int _Update(GK_OA_CarMaintenanceModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_CarMaintenance set ");
            if (mObj.Car_code != null)
            {
                builder.Append("Car_code=@Car_code,");
                this._DataProcess.ProcessParametersAdd("@Car_code", SqlDbType.VarChar, 4, mObj.Car_code);
            }
            if (mObj.MDate != DateTime.MinValue)
            {
                builder.Append("MDate=@MDate,");
                this._DataProcess.ProcessParametersAdd("@MDate", SqlDbType.DateTime, 8, mObj.MDate);
            }
            if (mObj.Mil != 0M)
            {
                builder.Append("Mil=@Mil,");
                this._DataProcess.ProcessParametersAdd("@Mil", SqlDbType.Decimal, 9, mObj.Mil);
            }
            if (mObj.MValue != null)
            {
                builder.Append("MValue=@MValue,");
                this._DataProcess.ProcessParametersAdd("@MValue", SqlDbType.VarChar, 0x3e8, mObj.MValue);
            }
            if (mObj.MPrice != 0M)
            {
                builder.Append("MPrice=@MPrice,");
                this._DataProcess.ProcessParametersAdd("@MPrice", SqlDbType.Decimal, 9, mObj.MPrice);
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

        public int Delete(GK_OA_CarMaintenanceModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_CarMaintenanceModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_CarMaintenanceModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Car_code = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.MDate = reader.GetDateTime(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Mil = reader.GetDecimal(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.MValue = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.MPrice = reader.GetDecimal(5);
            }
        }

        public int Insert(GK_OA_CarMaintenanceModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_CarMaintenanceModel> Select()
        {
            GK_OA_CarMaintenanceQueryModel qmObj = new GK_OA_CarMaintenanceQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_CarMaintenanceModel> Select(GK_OA_CarMaintenanceQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_CarMaintenanceModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

