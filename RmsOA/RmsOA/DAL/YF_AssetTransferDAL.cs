namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class YF_AssetTransferDAL
    {
        private SqlDataProcess _DataProcess;

        public YF_AssetTransferDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public YF_AssetTransferDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private YF_AssetTransferModel _DataBind(int Code)
        {
            YF_AssetTransferModel model = new YF_AssetTransferModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetTransfer ");
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
            builder.Append("delete from YF_AssetTransfer ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(YF_AssetTransferModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO YF_AssetTransfer (");
            builder2.Append("VALUES(");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode,");
                builder2.Append("@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.PreDept != null)
            {
                builder.Append("PreDept,");
                builder2.Append("@PreDept,");
                this._DataProcess.ProcessParametersAdd("@PreDept", SqlDbType.VarChar, 50, mObj.PreDept);
            }
            if (mObj.PostDept != null)
            {
                builder.Append("PostDept,");
                builder2.Append("@PostDept,");
                this._DataProcess.ProcessParametersAdd("@PostDept", SqlDbType.VarChar, 50, mObj.PostDept);
            }
            if (mObj.TransferTime != null)
            {
                builder.Append("TransferTime,");
                builder2.Append("@TransferTime,");
                this._DataProcess.ProcessParametersAdd("@TransferTime", SqlDbType.VarChar, 50, mObj.TransferTime);
            }
            if (mObj.Applyer != null)
            {
                builder.Append("Applyer,");
                builder2.Append("@Applyer,");
                this._DataProcess.ProcessParametersAdd("@Applyer", SqlDbType.VarChar, 50, mObj.Applyer);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode,");
                builder2.Append("@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
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

        private List<YF_AssetTransferModel> _Select(YF_AssetTransferQueryModel qmObj)
        {
            List<YF_AssetTransferModel> list = new List<YF_AssetTransferModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetTransfer ");
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
                            YF_AssetTransferModel model = new YF_AssetTransferModel();
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

        private int _Update(YF_AssetTransferModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update YF_AssetTransfer set ");
            if (mObj.ManageCode != 0)
            {
                builder.Append("ManageCode=@ManageCode,");
                this._DataProcess.ProcessParametersAdd("@ManageCode", SqlDbType.Int, 4, mObj.ManageCode);
            }
            if (mObj.PreDept != null)
            {
                builder.Append("PreDept=@PreDept,");
                this._DataProcess.ProcessParametersAdd("@PreDept", SqlDbType.VarChar, 50, mObj.PreDept);
            }
            if (mObj.PostDept != null)
            {
                builder.Append("PostDept=@PostDept,");
                this._DataProcess.ProcessParametersAdd("@PostDept", SqlDbType.VarChar, 50, mObj.PostDept);
            }
            if (mObj.TransferTime != null)
            {
                builder.Append("TransferTime=@TransferTime,");
                this._DataProcess.ProcessParametersAdd("@TransferTime", SqlDbType.VarChar, 50, mObj.TransferTime);
            }
            if (mObj.Applyer != null)
            {
                builder.Append("Applyer=@Applyer,");
                this._DataProcess.ProcessParametersAdd("@Applyer", SqlDbType.VarChar, 50, mObj.Applyer);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
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

        public int Delete(YF_AssetTransferModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public YF_AssetTransferModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, YF_AssetTransferModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ManageCode = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.PreDept = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.PostDept = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.TransferTime = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Applyer = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Status = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(7);
            }
        }

        public int Insert(YF_AssetTransferModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<YF_AssetTransferModel> Select()
        {
            YF_AssetTransferQueryModel qmObj = new YF_AssetTransferQueryModel();
            return this._Select(qmObj);
        }

        public List<YF_AssetTransferModel> Select(YF_AssetTransferQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(YF_AssetTransferModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

