namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_BigGoodsDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_BigGoodsDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_BigGoodsDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_BigGoodsModel _DataBind(int Code)
        {
            TC_OA_BigGoodsModel model = new TC_OA_BigGoodsModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_BigGoods ");
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
            builder.Append("delete from TC_OA_BigGoods ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_BigGoodsModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_BigGoods(");
            builder.Append("UserCode,UnitCode,ApplayDate,Name,UseWay,Type,GoodsCode,Auditing)");
            builder.Append(" values (");
            builder.Append("@UserCode,@UnitCode,@ApplayDate,@Name,@UseWay,@Type,@GoodsCode,@Auditing) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@ApplayDate", SqlDbType.DateTime, 8, mObj.ApplayDate);
            this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            this._DataProcess.ProcessParametersAdd("@UseWay", SqlDbType.VarChar, 50, mObj.UseWay);
            this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            this._DataProcess.ProcessParametersAdd("@GoodsCode", SqlDbType.VarChar, 50, mObj.GoodsCode);
            this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_BigGoodsModel> _Select(TC_OA_BigGoodsQueryModel qmObj)
        {
            List<TC_OA_BigGoodsModel> list = new List<TC_OA_BigGoodsModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_BigGoods ");
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
                            TC_OA_BigGoodsModel model = new TC_OA_BigGoodsModel();
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

        private int _Update(TC_OA_BigGoodsModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_BigGoods set ");
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.ApplayDate != DateTime.MinValue)
            {
                builder.Append("ApplayDate=@ApplayDate,");
                this._DataProcess.ProcessParametersAdd("@ApplayDate", SqlDbType.DateTime, 8, mObj.ApplayDate);
            }
            if (mObj.Name != null)
            {
                builder.Append("Name=@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            }
            if (mObj.UseWay != null)
            {
                builder.Append("UseWay=@UseWay,");
                this._DataProcess.ProcessParametersAdd("@UseWay", SqlDbType.VarChar, 50, mObj.UseWay);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.GoodsCode != null)
            {
                builder.Append("GoodsCode=@GoodsCode,");
                this._DataProcess.ProcessParametersAdd("@GoodsCode", SqlDbType.VarChar, 50, mObj.GoodsCode);
            }
            if (mObj.Auditing != 0)
            {
                builder.Append("Auditing=@Auditing,");
                this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
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

        public int Delete(TC_OA_BigGoodsModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_BigGoodsModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_BigGoodsModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.ApplayDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Name = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.UseWay = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Type = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.GoodsCode = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Auditing = reader.GetInt32(8);
            }
        }

        public int Insert(TC_OA_BigGoodsModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_BigGoodsModel> Select()
        {
            TC_OA_BigGoodsQueryModel qmObj = new TC_OA_BigGoodsQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_BigGoodsModel> Select(TC_OA_BigGoodsQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_BigGoodsModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

