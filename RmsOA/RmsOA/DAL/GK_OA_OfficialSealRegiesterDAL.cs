namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_OfficialSealRegiesterDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_OfficialSealRegiesterDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_OfficialSealRegiesterDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_OfficialSealRegiesterModel _DataBind(int Code)
        {
            GK_OA_OfficialSealRegiesterModel model = new GK_OA_OfficialSealRegiesterModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_OfficialSealRegiester ");
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
            builder.Append("delete from GK_OA_OfficialSealRegiester ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_OfficialSealRegiesterModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_OfficialSealRegiester(");
            builder.Append("RegiesterDate,UnitCode,Detail,Remark,Status,CollectCode,Field1)");
            builder.Append(" values (");
            builder.Append("@RegiesterDate,@UnitCode,@Detail,@Remark,@Status,@CollectCode,@Field1) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@RegiesterDate", SqlDbType.DateTime, 8, mObj.RegiesterDate);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@Detail", SqlDbType.VarChar, 500, mObj.Detail);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@CollectCode", SqlDbType.VarChar, 50, mObj.CollectCode);
            this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_OfficialSealRegiesterModel> _Select(GK_OA_OfficialSealRegiesterQueryModel qmObj)
        {
            List<GK_OA_OfficialSealRegiesterModel> list = new List<GK_OA_OfficialSealRegiesterModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_OfficialSealRegiester ");
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
                            GK_OA_OfficialSealRegiesterModel model = new GK_OA_OfficialSealRegiesterModel();
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

        private int _Update(GK_OA_OfficialSealRegiesterModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_OfficialSealRegiester set ");
            if (mObj.RegiesterDate != DateTime.MinValue)
            {
                builder.Append("RegiesterDate=@RegiesterDate,");
                this._DataProcess.ProcessParametersAdd("@RegiesterDate", SqlDbType.DateTime, 8, mObj.RegiesterDate);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.Detail != null)
            {
                builder.Append("Detail=@Detail,");
                this._DataProcess.ProcessParametersAdd("@Detail", SqlDbType.VarChar, 500, mObj.Detail);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.CollectCode != null)
            {
                builder.Append("CollectCode=@CollectCode,");
                this._DataProcess.ProcessParametersAdd("@CollectCode", SqlDbType.VarChar, 50, mObj.CollectCode);
            }
            if (mObj.Field1 != null)
            {
                builder.Append("Field1=@Field1,");
                this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
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

        public int Delete(GK_OA_OfficialSealRegiesterModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_OfficialSealRegiesterModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_OfficialSealRegiesterModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.RegiesterDate = reader.GetDateTime(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Detail = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Remark = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Status = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.CollectCode = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Field1 = reader.GetString(7);
            }
        }

        public int Insert(GK_OA_OfficialSealRegiesterModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_OfficialSealRegiesterModel> Select()
        {
            GK_OA_OfficialSealRegiesterQueryModel qmObj = new GK_OA_OfficialSealRegiesterQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_OfficialSealRegiesterModel> Select(GK_OA_OfficialSealRegiesterQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_OfficialSealRegiesterModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

