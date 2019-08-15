namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_OilDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_OilDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_OilDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_OilModel _DataBind(int Code)
        {
            GK_OA_OilModel model = new GK_OA_OilModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_Oil ");
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
            builder.Append("delete from GK_OA_Oil ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_OilModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_Oil(");
            builder.Append("Car_id,GetDate,GetNum,FactMil,FirstMil,ThisMil,GetMan,IndexNum)");
            builder.Append(" values (");
            builder.Append("@Car_id,@GetDate,@GetNum,@FactMil,@FirstMil,@ThisMil,@GetMan,@IndexNum) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Car_id", SqlDbType.VarChar, 50, mObj.Car_id);
            this._DataProcess.ProcessParametersAdd("@GetDate", SqlDbType.DateTime, 8, mObj.GetDate);
            this._DataProcess.ProcessParametersAdd("@GetNum", SqlDbType.Decimal, 9, mObj.GetNum);
            this._DataProcess.ProcessParametersAdd("@FactMil", SqlDbType.Decimal, 9, mObj.FactMil);
            this._DataProcess.ProcessParametersAdd("@FirstMil", SqlDbType.Decimal, 9, mObj.FirstMil);
            this._DataProcess.ProcessParametersAdd("@ThisMil", SqlDbType.Decimal, 9, mObj.ThisMil);
            this._DataProcess.ProcessParametersAdd("@GetMan", SqlDbType.VarChar, 50, mObj.GetMan);
            this._DataProcess.ProcessParametersAdd("@IndexNum", SqlDbType.VarChar, 50, mObj.IndexNum);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_OilModel> _Select(GK_OA_OilQueryModel qmObj)
        {
            List<GK_OA_OilModel> list = new List<GK_OA_OilModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_Oil ");
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
                            GK_OA_OilModel model = new GK_OA_OilModel();
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

        private int _Update(GK_OA_OilModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_Oil set ");
            if (mObj.Car_id != null)
            {
                builder.Append("Car_id=@Car_id,");
                this._DataProcess.ProcessParametersAdd("@Car_id", SqlDbType.VarChar, 50, mObj.Car_id);
            }
            if (mObj.GetDate != DateTime.MinValue)
            {
                builder.Append("GetDate=@GetDate,");
                this._DataProcess.ProcessParametersAdd("@GetDate", SqlDbType.DateTime, 8, mObj.GetDate);
            }
            if (mObj.GetNum != 0M)
            {
                builder.Append("GetNum=@GetNum,");
                this._DataProcess.ProcessParametersAdd("@GetNum", SqlDbType.Decimal, 9, mObj.GetNum);
            }
            if (mObj.FactMil != 0M)
            {
                builder.Append("FactMil=@FactMil,");
                this._DataProcess.ProcessParametersAdd("@FactMil", SqlDbType.Decimal, 9, mObj.FactMil);
            }
            if (mObj.FirstMil != 0M)
            {
                builder.Append("FirstMil=@FirstMil,");
                this._DataProcess.ProcessParametersAdd("@FirstMil", SqlDbType.Decimal, 9, mObj.FirstMil);
            }
            if (mObj.ThisMil != 0M)
            {
                builder.Append("ThisMil=@ThisMil,");
                this._DataProcess.ProcessParametersAdd("@ThisMil", SqlDbType.Decimal, 9, mObj.ThisMil);
            }
            if (mObj.GetMan != null)
            {
                builder.Append("GetMan=@GetMan,");
                this._DataProcess.ProcessParametersAdd("@GetMan", SqlDbType.VarChar, 50, mObj.GetMan);
            }
            if (mObj.IndexNum != null)
            {
                builder.Append("IndexNum=@IndexNum,");
                this._DataProcess.ProcessParametersAdd("@IndexNum", SqlDbType.VarChar, 50, mObj.IndexNum);
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

        public int Delete(GK_OA_OilModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_OilModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_OilModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Car_id = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.GetDate = reader.GetDateTime(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.GetNum = reader.GetDecimal(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.FactMil = reader.GetDecimal(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.FirstMil = reader.GetDecimal(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.ThisMil = reader.GetDecimal(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.GetMan = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.IndexNum = reader.GetString(8);
            }
        }

        public int Insert(GK_OA_OilModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_OilModel> Select()
        {
            GK_OA_OilQueryModel qmObj = new GK_OA_OilQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_OilModel> Select(GK_OA_OilQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_OilModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

