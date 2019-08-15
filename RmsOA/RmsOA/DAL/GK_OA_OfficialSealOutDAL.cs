namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_OfficialSealOutDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_OfficialSealOutDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_OfficialSealOutDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_OfficialSealOutModel _DataBind(int Code)
        {
            GK_OA_OfficialSealOutModel model = new GK_OA_OfficialSealOutModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_OfficialSealOut ");
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
            builder.Append("delete from GK_OA_OfficialSealOut ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_OfficialSealOutModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_OfficialSealOut(");
            builder.Append("RegiesterDate,UnitCode,PersonCode,OfficialSealName,OutDate,ReturnDate,Reason,Remark,Status,CollectCode,Fieled1)");
            builder.Append(" values (");
            builder.Append("@RegiesterDate,@UnitCode,@PersonCode,@OfficialSealName,@OutDate,@ReturnDate,@Reason,@Remark,@Status,@CollectCode,@Fieled1) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@RegiesterDate", SqlDbType.DateTime, 8, mObj.RegiesterDate);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@PersonCode", SqlDbType.VarChar, 50, mObj.PersonCode);
            this._DataProcess.ProcessParametersAdd("@OfficialSealName", SqlDbType.VarChar, 50, mObj.OfficialSealName);
            this._DataProcess.ProcessParametersAdd("@OutDate", SqlDbType.DateTime, 8, mObj.OutDate);
            this._DataProcess.ProcessParametersAdd("@ReturnDate", SqlDbType.DateTime, 8, mObj.ReturnDate);
            this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 500, mObj.Reason);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@CollectCode", SqlDbType.VarChar, 50, mObj.CollectCode);
            this._DataProcess.ProcessParametersAdd("@Fieled1", SqlDbType.VarChar, 50, mObj.Fieled1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_OfficialSealOutModel> _Select(GK_OA_OfficialSealOutQueryModel qmObj)
        {
            List<GK_OA_OfficialSealOutModel> list = new List<GK_OA_OfficialSealOutModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_OfficialSealOut ");
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
                            GK_OA_OfficialSealOutModel model = new GK_OA_OfficialSealOutModel();
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

        private int _Update(GK_OA_OfficialSealOutModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_OfficialSealOut set ");
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
            if (mObj.PersonCode != null)
            {
                builder.Append("PersonCode=@PersonCode,");
                this._DataProcess.ProcessParametersAdd("@PersonCode", SqlDbType.VarChar, 50, mObj.PersonCode);
            }
            if (mObj.OfficialSealName != null)
            {
                builder.Append("OfficialSealName=@OfficialSealName,");
                this._DataProcess.ProcessParametersAdd("@OfficialSealName", SqlDbType.VarChar, 50, mObj.OfficialSealName);
            }
            if (mObj.OutDate != DateTime.MinValue)
            {
                builder.Append("OutDate=@OutDate,");
                this._DataProcess.ProcessParametersAdd("@OutDate", SqlDbType.DateTime, 8, mObj.OutDate);
            }
            if (mObj.ReturnDate != DateTime.MinValue)
            {
                builder.Append("ReturnDate=@ReturnDate,");
                this._DataProcess.ProcessParametersAdd("@ReturnDate", SqlDbType.DateTime, 8, mObj.ReturnDate);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 500, mObj.Reason);
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
            if (mObj.Fieled1 != null)
            {
                builder.Append("Fieled1=@Fieled1,");
                this._DataProcess.ProcessParametersAdd("@Fieled1", SqlDbType.VarChar, 50, mObj.Fieled1);
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

        public int Delete(GK_OA_OfficialSealOutModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_OfficialSealOutModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_OfficialSealOutModel obj)
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
                obj.PersonCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.OfficialSealName = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.OutDate = reader.GetDateTime(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.ReturnDate = reader.GetDateTime(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Reason = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Remark = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Status = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.CollectCode = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Fieled1 = reader.GetString(11);
            }
        }

        public int Insert(GK_OA_OfficialSealOutModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_OfficialSealOutModel> Select()
        {
            GK_OA_OfficialSealOutQueryModel qmObj = new GK_OA_OfficialSealOutQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_OfficialSealOutModel> Select(GK_OA_OfficialSealOutQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_OfficialSealOutModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

