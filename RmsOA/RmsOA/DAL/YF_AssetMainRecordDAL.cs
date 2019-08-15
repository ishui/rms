namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class YF_AssetMainRecordDAL
    {
        private SqlDataProcess _DataProcess;

        public YF_AssetMainRecordDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public YF_AssetMainRecordDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private YF_AssetMainRecordModel _DataBind(int Code)
        {
            YF_AssetMainRecordModel model = new YF_AssetMainRecordModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetMainRecord ");
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
            builder.Append("delete from YF_AssetMainRecord ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(YF_AssetMainRecordModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO YF_AssetMainRecord (");
            builder2.Append("VALUES(");
            if (mObj.Code != 0)
            {
                builder.Append("Code,");
                builder2.Append("@Code,");
                this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, mObj.Code);
            }
            if (mObj.FKCode != 0)
            {
                builder.Append("FKCode,");
                builder2.Append("@FKCode,");
                this._DataProcess.ProcessParametersAdd("@FKCode", SqlDbType.Int, 4, mObj.FKCode);
            }
            if (mObj.Content != null)
            {
                builder.Append("Content,");
                builder2.Append("@Content,");
                this._DataProcess.ProcessParametersAdd("@Content", SqlDbType.VarChar, 0x7d0, mObj.Content);
            }
            if (mObj.CostTime != 0)
            {
                builder.Append("CostTime,");
                builder2.Append("@CostTime,");
                this._DataProcess.ProcessParametersAdd("@CostTime", SqlDbType.Int, 4, mObj.CostTime);
            }
            if (mObj.CostMoney != 0M)
            {
                builder.Append("CostMoney,");
                builder2.Append("@CostMoney,");
                this._DataProcess.ProcessParametersAdd("@CostMoney", SqlDbType.Decimal, 9, mObj.CostMoney);
            }
            if (mObj.ChangeDetail != null)
            {
                builder.Append("ChangeDetail,");
                builder2.Append("@ChangeDetail,");
                this._DataProcess.ProcessParametersAdd("@ChangeDetail", SqlDbType.VarChar, 0x7d0, mObj.ChangeDetail);
            }
            if (mObj.UserSign != null)
            {
                builder.Append("UserSign,");
                builder2.Append("@UserSign,");
                this._DataProcess.ProcessParametersAdd("@UserSign", SqlDbType.VarChar, 50, mObj.UserSign);
            }
            if (mObj.MainUser != null)
            {
                builder.Append("MainUser,");
                builder2.Append("@MainUser,");
                this._DataProcess.ProcessParametersAdd("@MainUser", SqlDbType.VarChar, 50, mObj.MainUser);
            }
            if (mObj.MainTime != DateTime.MinValue)
            {
                builder.Append("MainTime,");
                builder2.Append("@MainTime,");
                this._DataProcess.ProcessParametersAdd("@MainTime", SqlDbType.DateTime, 8, mObj.MainTime);
            }
            if (mObj.Result != null)
            {
                builder.Append("Result,");
                builder2.Append("@Result,");
                this._DataProcess.ProcessParametersAdd("@Result", SqlDbType.VarChar, 50, mObj.Result);
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            builder.Append(") ");
            builder2.Append(") ");
            builder.Append(builder2.ToString());
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.RunSql();
            return mObj.Code;
        }

        private List<YF_AssetMainRecordModel> _Select(YF_AssetMainRecordQueryModel qmObj)
        {
            List<YF_AssetMainRecordModel> list = new List<YF_AssetMainRecordModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetMainRecord ");
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
                            YF_AssetMainRecordModel model = new YF_AssetMainRecordModel();
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

        private int _Update(YF_AssetMainRecordModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update YF_AssetMainRecord set ");
            if (mObj.FKCode != 0)
            {
                builder.Append("FKCode=@FKCode,");
                this._DataProcess.ProcessParametersAdd("@FKCode", SqlDbType.Int, 4, mObj.FKCode);
            }
            if (mObj.Content != null)
            {
                builder.Append("Content=@Content,");
                this._DataProcess.ProcessParametersAdd("@Content", SqlDbType.VarChar, 0x7d0, mObj.Content);
            }
            if (mObj.CostTime != 0)
            {
                builder.Append("CostTime=@CostTime,");
                this._DataProcess.ProcessParametersAdd("@CostTime", SqlDbType.Int, 4, mObj.CostTime);
            }
            if (mObj.CostMoney != 0M)
            {
                builder.Append("CostMoney=@CostMoney,");
                this._DataProcess.ProcessParametersAdd("@CostMoney", SqlDbType.Decimal, 9, mObj.CostMoney);
            }
            if (mObj.ChangeDetail != null)
            {
                builder.Append("ChangeDetail=@ChangeDetail,");
                this._DataProcess.ProcessParametersAdd("@ChangeDetail", SqlDbType.VarChar, 0x7d0, mObj.ChangeDetail);
            }
            if (mObj.UserSign != null)
            {
                builder.Append("UserSign=@UserSign,");
                this._DataProcess.ProcessParametersAdd("@UserSign", SqlDbType.VarChar, 50, mObj.UserSign);
            }
            if (mObj.MainUser != null)
            {
                builder.Append("MainUser=@MainUser,");
                this._DataProcess.ProcessParametersAdd("@MainUser", SqlDbType.VarChar, 50, mObj.MainUser);
            }
            if (mObj.MainTime != DateTime.MinValue)
            {
                builder.Append("MainTime=@MainTime,");
                this._DataProcess.ProcessParametersAdd("@MainTime", SqlDbType.DateTime, 8, mObj.MainTime);
            }
            if (mObj.Result != null)
            {
                builder.Append("Result=@Result,");
                this._DataProcess.ProcessParametersAdd("@Result", SqlDbType.VarChar, 50, mObj.Result);
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

        public int Delete(YF_AssetMainRecordModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public YF_AssetMainRecordModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, YF_AssetMainRecordModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.FKCode = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Content = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.CostTime = reader.GetInt32(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.CostMoney = reader.GetDecimal(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.ChangeDetail = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.UserSign = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.MainUser = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.MainTime = reader.GetDateTime(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Result = reader.GetString(9);
            }
        }

        public int Insert(YF_AssetMainRecordModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<YF_AssetMainRecordModel> Select()
        {
            YF_AssetMainRecordQueryModel qmObj = new YF_AssetMainRecordQueryModel();
            return this._Select(qmObj);
        }

        public List<YF_AssetMainRecordModel> Select(YF_AssetMainRecordQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(YF_AssetMainRecordModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

