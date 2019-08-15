namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_CarDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_CarDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_CarDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_CarModel _DataBind(int Code)
        {
            GK_OA_CarModel model = new GK_OA_CarModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_Car ");
            builder.Append(" where Car_Code=@Car_Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Car_Code", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from GK_OA_Car ");
            builder.Append(" where Car_Code=@Car_Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Car_Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_CarModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_Car(");
            builder.Append("Car_Id,Car_Type,Buy_Date,Chejia_Id,Fadongji_Id,Index_num)");
            builder.Append(" values (");
            builder.Append("@Car_Id,@Car_Type,@Buy_Date,@Chejia_Id,@Fadongji_Id,@Index_num) ");
            builder.Append("SELECT @Car_Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Car_Id", SqlDbType.VarChar, 50, mObj.Car_Id);
            this._DataProcess.ProcessParametersAdd("@Car_Type", SqlDbType.VarChar, 50, mObj.Car_Type);
            this._DataProcess.ProcessParametersAdd("@Buy_Date", SqlDbType.DateTime, 8, mObj.Buy_Date);
            this._DataProcess.ProcessParametersAdd("@Chejia_Id", SqlDbType.VarChar, 50, mObj.Chejia_Id);
            this._DataProcess.ProcessParametersAdd("@Fadongji_Id", SqlDbType.VarChar, 50, mObj.Fadongji_Id);
            this._DataProcess.ProcessParametersAdd("@Index_num", SqlDbType.VarChar, 50, mObj.Index_num);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Car_Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Car_Code = (int) parameter.Value;
            return mObj.Car_Code;
        }

        private List<GK_OA_CarModel> _Select(GK_OA_CarQueryModel qmObj)
        {
            List<GK_OA_CarModel> list = new List<GK_OA_CarModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_Car ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY Car_Code desc");
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
                            GK_OA_CarModel model = new GK_OA_CarModel();
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

        private int _Update(GK_OA_CarModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_Car set ");
            if (mObj.Car_Id != null)
            {
                builder.Append("Car_Id=@Car_Id,");
                this._DataProcess.ProcessParametersAdd("@Car_Id", SqlDbType.VarChar, 50, mObj.Car_Id);
            }
            if (mObj.Car_Type != null)
            {
                builder.Append("Car_Type=@Car_Type,");
                this._DataProcess.ProcessParametersAdd("@Car_Type", SqlDbType.VarChar, 50, mObj.Car_Type);
            }
            if (mObj.Buy_Date != DateTime.MinValue)
            {
                builder.Append("Buy_Date=@Buy_Date,");
                this._DataProcess.ProcessParametersAdd("@Buy_Date", SqlDbType.DateTime, 8, mObj.Buy_Date);
            }
            if (mObj.Chejia_Id != null)
            {
                builder.Append("Chejia_Id=@Chejia_Id,");
                this._DataProcess.ProcessParametersAdd("@Chejia_Id", SqlDbType.VarChar, 50, mObj.Chejia_Id);
            }
            if (mObj.Fadongji_Id != null)
            {
                builder.Append("Fadongji_Id=@Fadongji_Id,");
                this._DataProcess.ProcessParametersAdd("@Fadongji_Id", SqlDbType.VarChar, 50, mObj.Fadongji_Id);
            }
            if (mObj.Index_num != null)
            {
                builder.Append("Index_num=@Index_num,");
                this._DataProcess.ProcessParametersAdd("@Index_num", SqlDbType.VarChar, 50, mObj.Index_num);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where Car_Code=@Car_Code");
            this._DataProcess.ProcessParametersAdd("@Car_Code", SqlDbType.Int, 4, mObj.Car_Code);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(GK_OA_CarModel mObj)
        {
            return this._Delete(mObj.Car_Code);
        }

        public GK_OA_CarModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_CarModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Car_Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Car_Id = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Car_Type = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Buy_Date = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Chejia_Id = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Fadongji_Id = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Index_num = reader.GetString(6);
            }
        }

        public int Insert(GK_OA_CarModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_CarModel> Select()
        {
            GK_OA_CarQueryModel qmObj = new GK_OA_CarQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_CarModel> Select(GK_OA_CarQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_CarModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

