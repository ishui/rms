namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_MaterialTransferDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_MaterialTransferDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_MaterialTransferDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_MaterialTransferModel _DataBind(int Code)
        {
            GK_OA_MaterialTransferModel model = new GK_OA_MaterialTransferModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_MaterialTransfer ");
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
            builder.Append("delete from GK_OA_MaterialTransfer ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_MaterialTransferModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO GK_OA_MaterialTransfer (");
            builder2.Append("VALUES(");
            if (mObj.Name != null)
            {
                builder.Append("Name,");
                builder2.Append("@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 100, mObj.Name);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type,");
                builder2.Append("@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 100, mObj.Type);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number,");
                builder2.Append("@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 100, mObj.Number);
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
            if (mObj.OriginalPrice != 0M)
            {
                builder.Append("OriginalPrice,");
                builder2.Append("@OriginalPrice,");
                this._DataProcess.ProcessParametersAdd("@OriginalPrice", SqlDbType.Decimal, 9, mObj.OriginalPrice);
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
                this._DataProcess.ProcessParametersAdd("@PreDept", SqlDbType.VarChar, 100, mObj.PreDept);
            }
            if (mObj.LaterUser != null)
            {
                builder.Append("LaterUser,");
                builder2.Append("@LaterUser,");
                this._DataProcess.ProcessParametersAdd("@LaterUser", SqlDbType.VarChar, 50, mObj.LaterUser);
            }
            if (mObj.LaterDept != null)
            {
                builder.Append("LaterDept,");
                builder2.Append("@LaterDept,");
                this._DataProcess.ProcessParametersAdd("@LaterDept", SqlDbType.VarChar, 50, mObj.LaterDept);
            }
            if (mObj.TransferHander != null)
            {
                builder.Append("TransferHander,");
                builder2.Append("@TransferHander,");
                this._DataProcess.ProcessParametersAdd("@TransferHander", SqlDbType.VarChar, 50, mObj.TransferHander);
            }
            if (mObj.ReciveHander != null)
            {
                builder.Append("ReciveHander,");
                builder2.Append("@ReciveHander,");
                this._DataProcess.ProcessParametersAdd("@ReciveHander", SqlDbType.VarChar, 50, mObj.ReciveHander);
            }
            if (mObj.TransferMaster != null)
            {
                builder.Append("TransferMaster,");
                builder2.Append("@TransferMaster,");
                this._DataProcess.ProcessParametersAdd("@TransferMaster", SqlDbType.VarChar, 50, mObj.TransferMaster);
            }
            if (mObj.ReciveMaster != null)
            {
                builder.Append("ReciveMaster,");
                builder2.Append("@ReciveMaster,");
                this._DataProcess.ProcessParametersAdd("@ReciveMaster", SqlDbType.VarChar, 50, mObj.ReciveMaster);
            }
            if (mObj.TransferDate != DateTime.MinValue)
            {
                builder.Append("TransferDate,");
                builder2.Append("@TransferDate,");
                this._DataProcess.ProcessParametersAdd("@TransferDate", SqlDbType.DateTime, 8, mObj.TransferDate);
            }
            if (mObj.ReciveDate != DateTime.MinValue)
            {
                builder.Append("ReciveDate,");
                builder2.Append("@ReciveDate,");
                this._DataProcess.ProcessParametersAdd("@ReciveDate", SqlDbType.DateTime, 8, mObj.ReciveDate);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO,");
                builder2.Append("@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 100, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule,");
                builder2.Append("@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 100, mObj.SNRule);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
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

        private List<GK_OA_MaterialTransferModel> _Select(GK_OA_MaterialTransferQueryModel qmObj)
        {
            List<GK_OA_MaterialTransferModel> list = new List<GK_OA_MaterialTransferModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_MaterialTransfer ");
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
                            GK_OA_MaterialTransferModel model = new GK_OA_MaterialTransferModel();
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

        private int _Update(GK_OA_MaterialTransferModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_MaterialTransfer set ");
            if (mObj.Name != null)
            {
                builder.Append("Name=@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 100, mObj.Name);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 100, mObj.Type);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number=@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 100, mObj.Number);
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
            if (mObj.OriginalPrice != 0M)
            {
                builder.Append("OriginalPrice=@OriginalPrice,");
                this._DataProcess.ProcessParametersAdd("@OriginalPrice", SqlDbType.Decimal, 9, mObj.OriginalPrice);
            }
            if (mObj.PreUser != null)
            {
                builder.Append("PreUser=@PreUser,");
                this._DataProcess.ProcessParametersAdd("@PreUser", SqlDbType.VarChar, 50, mObj.PreUser);
            }
            if (mObj.PreDept != null)
            {
                builder.Append("PreDept=@PreDept,");
                this._DataProcess.ProcessParametersAdd("@PreDept", SqlDbType.VarChar, 100, mObj.PreDept);
            }
            if (mObj.LaterUser != null)
            {
                builder.Append("LaterUser=@LaterUser,");
                this._DataProcess.ProcessParametersAdd("@LaterUser", SqlDbType.VarChar, 50, mObj.LaterUser);
            }
            if (mObj.LaterDept != null)
            {
                builder.Append("LaterDept=@LaterDept,");
                this._DataProcess.ProcessParametersAdd("@LaterDept", SqlDbType.VarChar, 50, mObj.LaterDept);
            }
            if (mObj.TransferHander != null)
            {
                builder.Append("TransferHander=@TransferHander,");
                this._DataProcess.ProcessParametersAdd("@TransferHander", SqlDbType.VarChar, 50, mObj.TransferHander);
            }
            if (mObj.ReciveHander != null)
            {
                builder.Append("ReciveHander=@ReciveHander,");
                this._DataProcess.ProcessParametersAdd("@ReciveHander", SqlDbType.VarChar, 50, mObj.ReciveHander);
            }
            if (mObj.TransferMaster != null)
            {
                builder.Append("TransferMaster=@TransferMaster,");
                this._DataProcess.ProcessParametersAdd("@TransferMaster", SqlDbType.VarChar, 50, mObj.TransferMaster);
            }
            if (mObj.ReciveMaster != null)
            {
                builder.Append("ReciveMaster=@ReciveMaster,");
                this._DataProcess.ProcessParametersAdd("@ReciveMaster", SqlDbType.VarChar, 50, mObj.ReciveMaster);
            }
            if (mObj.TransferDate != DateTime.MinValue)
            {
                builder.Append("TransferDate=@TransferDate,");
                this._DataProcess.ProcessParametersAdd("@TransferDate", SqlDbType.DateTime, 8, mObj.TransferDate);
            }
            if (mObj.ReciveDate != DateTime.MinValue)
            {
                builder.Append("ReciveDate=@ReciveDate,");
                this._DataProcess.ProcessParametersAdd("@ReciveDate", SqlDbType.DateTime, 8, mObj.ReciveDate);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO=@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 100, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule=@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 100, mObj.SNRule);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
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

        public int Delete(GK_OA_MaterialTransferModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_MaterialTransferModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_MaterialTransferModel obj)
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
                obj.Type = reader.GetString(2);
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
                obj.OriginalPrice = reader.GetDecimal(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.PreUser = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.PreDept = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.LaterUser = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.LaterDept = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.TransferHander = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.ReciveHander = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.TransferMaster = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.ReciveMaster = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.TransferDate = reader.GetDateTime(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.ReciveDate = reader.GetDateTime(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.QualityNO = reader.GetString(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.SNRule = reader.GetString(0x12);
            }
            if (reader.GetValue(0x13) != DBNull.Value)
            {
                obj.Status = reader.GetString(0x13);
            }
        }

        public int Insert(GK_OA_MaterialTransferModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_MaterialTransferModel> Select()
        {
            GK_OA_MaterialTransferQueryModel qmObj = new GK_OA_MaterialTransferQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_MaterialTransferModel> Select(GK_OA_MaterialTransferQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_MaterialTransferModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

