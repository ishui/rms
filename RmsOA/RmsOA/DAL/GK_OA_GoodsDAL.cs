namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_GoodsDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_GoodsDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_GoodsDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_GoodsModel _DataBind(int Code)
        {
            GK_OA_GoodsModel model = new GK_OA_GoodsModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_Goods ");
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
            builder.Append("delete from GK_OA_Goods ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_GoodsModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_Goods(");
            builder.Append("GoodsName,GoodsCode,GoodsType,GoodsPart,GoodsNumber,UnitCode,GoodsPrice,InputDate,DepartmentCode,UsePersonCode)");
            builder.Append(" values (");
            builder.Append("@GoodsName,@GoodsCode,@GoodsType,@GoodsPart,@GoodsNumber,@UnitCode,@GoodsPrice,@InputDate,@DepartmentCode,@UsePersonCode) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@GoodsName", SqlDbType.VarChar, 50, mObj.GoodsName);
            this._DataProcess.ProcessParametersAdd("@GoodsCode", SqlDbType.VarChar, 50, mObj.GoodsCode);
            this._DataProcess.ProcessParametersAdd("@GoodsType", SqlDbType.VarChar, 50, mObj.GoodsType);
            this._DataProcess.ProcessParametersAdd("@GoodsPart", SqlDbType.VarChar, 50, mObj.GoodsPart);
            this._DataProcess.ProcessParametersAdd("@GoodsNumber", SqlDbType.VarChar, 50, mObj.GoodsNumber);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@GoodsPrice", SqlDbType.VarChar, 50, mObj.GoodsPrice);
            this._DataProcess.ProcessParametersAdd("@InputDate", SqlDbType.DateTime, 8, mObj.InputDate);
            this._DataProcess.ProcessParametersAdd("@DepartmentCode", SqlDbType.VarChar, 50, mObj.DepartmentCode);
            this._DataProcess.ProcessParametersAdd("@UsePersonCode", SqlDbType.VarChar, 50, mObj.UsePersonCode);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_GoodsModel> _Select(GK_OA_GoodsQueryModel qmObj)
        {
            List<GK_OA_GoodsModel> list = new List<GK_OA_GoodsModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_Goods ");
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
                            GK_OA_GoodsModel model = new GK_OA_GoodsModel();
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

        private int _Update(GK_OA_GoodsModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_Goods set ");
            if (mObj.GoodsName != null)
            {
                builder.Append("GoodsName=@GoodsName,");
                this._DataProcess.ProcessParametersAdd("@GoodsName", SqlDbType.VarChar, 50, mObj.GoodsName);
            }
            if (mObj.GoodsCode != null)
            {
                builder.Append("GoodsCode=@GoodsCode,");
                this._DataProcess.ProcessParametersAdd("@GoodsCode", SqlDbType.VarChar, 50, mObj.GoodsCode);
            }
            if (mObj.GoodsType != null)
            {
                builder.Append("GoodsType=@GoodsType,");
                this._DataProcess.ProcessParametersAdd("@GoodsType", SqlDbType.VarChar, 50, mObj.GoodsType);
            }
            if (mObj.GoodsPart != null)
            {
                builder.Append("GoodsPart=@GoodsPart,");
                this._DataProcess.ProcessParametersAdd("@GoodsPart", SqlDbType.VarChar, 50, mObj.GoodsPart);
            }
            if (mObj.GoodsNumber != null)
            {
                builder.Append("GoodsNumber=@GoodsNumber,");
                this._DataProcess.ProcessParametersAdd("@GoodsNumber", SqlDbType.VarChar, 50, mObj.GoodsNumber);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.GoodsPrice != null)
            {
                builder.Append("GoodsPrice=@GoodsPrice,");
                this._DataProcess.ProcessParametersAdd("@GoodsPrice", SqlDbType.VarChar, 50, mObj.GoodsPrice);
            }
            if (mObj.InputDate != DateTime.MinValue)
            {
                builder.Append("InputDate=@InputDate,");
                this._DataProcess.ProcessParametersAdd("@InputDate", SqlDbType.DateTime, 8, mObj.InputDate);
            }
            if (mObj.DepartmentCode != null)
            {
                builder.Append("DepartmentCode=@DepartmentCode,");
                this._DataProcess.ProcessParametersAdd("@DepartmentCode", SqlDbType.VarChar, 50, mObj.DepartmentCode);
            }
            if (mObj.UsePersonCode != null)
            {
                builder.Append("UsePersonCode=@UsePersonCode,");
                this._DataProcess.ProcessParametersAdd("@UsePersonCode", SqlDbType.VarChar, 50, mObj.UsePersonCode);
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

        public int Delete(GK_OA_GoodsModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_GoodsModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_GoodsModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.GoodsName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.GoodsCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.GoodsType = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.GoodsPart = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.GoodsNumber = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.GoodsPrice = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.InputDate = reader.GetDateTime(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.DepartmentCode = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.UsePersonCode = reader.GetString(10);
            }
        }

        public int Insert(GK_OA_GoodsModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_GoodsModel> Select()
        {
            GK_OA_GoodsQueryModel qmObj = new GK_OA_GoodsQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_GoodsModel> Select(GK_OA_GoodsQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_GoodsModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

