namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_AssetTransferDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_AssetTransferDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_AssetTransferDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_AssetTransferModel _DataBind(int Code)
        {
            GK_OA_AssetTransferModel model = new GK_OA_AssetTransferModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_AssetTransfer ");
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
            builder.Append("delete from GK_OA_AssetTransfer ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_AssetTransferModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO GK_OA_AssetTransfer (");
            builder2.Append("VALUES(");
            if (mObj.Name != null)
            {
                builder.Append("Name,");
                builder2.Append("@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 200, mObj.Name);
            }
            if (mObj.Sort != null)
            {
                builder.Append("Sort,");
                builder2.Append("@Sort,");
                this._DataProcess.ProcessParametersAdd("@Sort", SqlDbType.VarChar, 50, mObj.Sort);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number,");
                builder2.Append("@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            }
            if (mObj.NumUnit != null)
            {
                builder.Append("NumUnit,");
                builder2.Append("@NumUnit,");
                this._DataProcess.ProcessParametersAdd("@NumUnit", SqlDbType.VarChar, 50, mObj.NumUnit);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason,");
                builder2.Append("@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 0x7d0, mObj.Reason);
            }
            if (mObj.PreUser != null)
            {
                builder.Append("PreUser,");
                builder2.Append("@PreUser,");
                this._DataProcess.ProcessParametersAdd("@PreUser", SqlDbType.VarChar, 50, mObj.PreUser);
            }
            if (mObj.PreDept != null)
            {
                builder.Append("PreDept,");
                builder2.Append("@PreDept,");
                this._DataProcess.ProcessParametersAdd("@PreDept", SqlDbType.VarChar, 50, mObj.PreDept);
            }
            if (mObj.PostUser != null)
            {
                builder.Append("PostUser,");
                builder2.Append("@PostUser,");
                this._DataProcess.ProcessParametersAdd("@PostUser", SqlDbType.VarChar, 50, mObj.PostUser);
            }
            if (mObj.PostDept != null)
            {
                builder.Append("PostDept,");
                builder2.Append("@PostDept,");
                this._DataProcess.ProcessParametersAdd("@PostDept", SqlDbType.VarChar, 50, mObj.PostDept);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO,");
                builder2.Append("@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 50, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule,");
                builder2.Append("@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 50, mObj.SNRule);
            }
            if (mObj.Submiter != null)
            {
                builder.Append("Submiter,");
                builder2.Append("@Submiter,");
                this._DataProcess.ProcessParametersAdd("@Submiter", SqlDbType.VarChar, 50, mObj.Submiter);
            }
            if (mObj.SubTime != DateTime.MinValue)
            {
                builder.Append("SubTime,");
                builder2.Append("@SubTime,");
                this._DataProcess.ProcessParametersAdd("@SubTime", SqlDbType.DateTime, 8, mObj.SubTime);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.OriginalPrice != 0M)
            {
                builder.Append("OriginalPrice,");
                builder2.Append("@OriginalPrice,");
                this._DataProcess.ProcessParametersAdd("@OriginalPrice", SqlDbType.Decimal, 9, mObj.OriginalPrice);
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            builder.Append(") ");
            builder2.Append(") ");
            builder.Append(builder2.ToString());
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_AssetTransferModel> _Select(GK_OA_AssetTransferQueryModel qmObj)
        {
            List<GK_OA_AssetTransferModel> list = new List<GK_OA_AssetTransferModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_AssetTransfer ");
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
                            GK_OA_AssetTransferModel model = new GK_OA_AssetTransferModel();
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

        private int _Update(GK_OA_AssetTransferModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_AssetTransfer set ");
            if (mObj.Name != null)
            {
                builder.Append("Name=@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 200, mObj.Name);
            }
            if (mObj.Sort != null)
            {
                builder.Append("Sort=@Sort,");
                this._DataProcess.ProcessParametersAdd("@Sort", SqlDbType.VarChar, 50, mObj.Sort);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number=@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            }
            if (mObj.NumUnit != null)
            {
                builder.Append("NumUnit=@NumUnit,");
                this._DataProcess.ProcessParametersAdd("@NumUnit", SqlDbType.VarChar, 50, mObj.NumUnit);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 0x7d0, mObj.Reason);
            }
            if (mObj.PreUser != null)
            {
                builder.Append("PreUser=@PreUser,");
                this._DataProcess.ProcessParametersAdd("@PreUser", SqlDbType.VarChar, 50, mObj.PreUser);
            }
            if (mObj.PreDept != null)
            {
                builder.Append("PreDept=@PreDept,");
                this._DataProcess.ProcessParametersAdd("@PreDept", SqlDbType.VarChar, 50, mObj.PreDept);
            }
            if (mObj.PostUser != null)
            {
                builder.Append("PostUser=@PostUser,");
                this._DataProcess.ProcessParametersAdd("@PostUser", SqlDbType.VarChar, 50, mObj.PostUser);
            }
            if (mObj.PostDept != null)
            {
                builder.Append("PostDept=@PostDept,");
                this._DataProcess.ProcessParametersAdd("@PostDept", SqlDbType.VarChar, 50, mObj.PostDept);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO=@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 50, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule=@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 50, mObj.SNRule);
            }
            if (mObj.Submiter != null)
            {
                builder.Append("Submiter=@Submiter,");
                this._DataProcess.ProcessParametersAdd("@Submiter", SqlDbType.VarChar, 50, mObj.Submiter);
            }
            if (mObj.SubTime != DateTime.MinValue)
            {
                builder.Append("SubTime=@SubTime,");
                this._DataProcess.ProcessParametersAdd("@SubTime", SqlDbType.DateTime, 8, mObj.SubTime);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.OriginalPrice != 0M)
            {
                builder.Append("OriginalPrice=@OriginalPrice,");
                this._DataProcess.ProcessParametersAdd("@OriginalPrice", SqlDbType.Decimal, 9, mObj.OriginalPrice);
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

        public int Delete(GK_OA_AssetTransferModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_AssetTransferModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_AssetTransferModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Name = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Sort = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Number = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.NumUnit = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Reason = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.PreUser = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.PreDept = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.PostUser = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.PostDept = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.QualityNO = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.SNRule = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Submiter = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.SubTime = reader.GetDateTime(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.Status = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.OriginalPrice = reader.GetDecimal(15);
            }
        }

        public int Insert(GK_OA_AssetTransferModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_AssetTransferModel> Select()
        {
            GK_OA_AssetTransferQueryModel qmObj = new GK_OA_AssetTransferQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_AssetTransferModel> Select(GK_OA_AssetTransferQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_AssetTransferModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

