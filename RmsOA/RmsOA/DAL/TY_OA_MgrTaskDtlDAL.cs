namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class TY_OA_MgrTaskDtlDAL
    {
        private SqlDataProcess _DataProcess;

        public TY_OA_MgrTaskDtlDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TY_OA_MgrTaskDtlDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TY_OA_MgrTaskDtlModel _DataBind(int Code)
        {
            TY_OA_MgrTaskDtlModel model = new TY_OA_MgrTaskDtlModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TY_OA_MgrTaskDtl ");
            builder.Append(" where MgrDtlCode=@MgrDtlCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MgrDtlCode", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from TY_OA_MgrTaskDtl ");
            builder.Append(" where MgrDtlCode=@MgrDtlCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MgrDtlCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TY_OA_MgrTaskDtlModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TY_OA_MgrTaskDtl(");
            builder.Append("MgrDtlCode,MgrDtlInfo,DeadLine,ResponsePerson,MgrCodeID,Assistpersons,TrancRevert,ManagerRevert,Isfinish,State,Bak)");
            builder.Append(" values (");
            builder.Append("@MgrDtlCode,@MgrDtlInfo,@DeadLine,@ResponsePerson,@MgrCodeID,@Assistpersons,@TrancRevert,@ManagerRevert,@Isfinish,@State,@Bak) ");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MgrDtlCode", SqlDbType.Int, 4, mObj.MgrDtlCode);
            this._DataProcess.ProcessParametersAdd("@MgrDtlInfo", SqlDbType.VarChar, 800, mObj.MgrDtlInfo);
            this._DataProcess.ProcessParametersAdd("@DeadLine", SqlDbType.DateTime, 8, mObj.DeadLine);
            this._DataProcess.ProcessParametersAdd("@ResponsePerson", SqlDbType.VarChar, 50, mObj.ResponsePerson);
            this._DataProcess.ProcessParametersAdd("@MgrCodeID", SqlDbType.Int, 4, mObj.MgrCodeID);
            this._DataProcess.ProcessParametersAdd("@Assistpersons", SqlDbType.VarChar, 500, mObj.Assistpersons);
            this._DataProcess.ProcessParametersAdd("@TrancRevert", SqlDbType.VarChar, 0x3e8, (mObj.TrancRevert == null) ? "" : mObj.TrancRevert);
            this._DataProcess.ProcessParametersAdd("@ManagerRevert", SqlDbType.VarChar, 0x3e8, (mObj.ManagerRevert == null) ? "" : mObj.ManagerRevert);
            this._DataProcess.ProcessParametersAdd("@Isfinish", SqlDbType.VarChar, 50, (mObj.Isfinish == null) ? "0" : mObj.Isfinish);
            this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, (mObj.State == null) ? "1" : mObj.State);
            this._DataProcess.ProcessParametersAdd("@Bak", SqlDbType.VarChar, 50, (mObj.Bak == null) ? "" : mObj.Bak);
            this._DataProcess.RunSql();
            return mObj.MgrDtlCode;
        }

        private List<TY_OA_MgrTaskDtlModel> _Select(TY_OA_MgrTaskDtlQueryModel qmObj)
        {
            List<TY_OA_MgrTaskDtlModel> list = new List<TY_OA_MgrTaskDtlModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TY_OA_MgrTaskDtl ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY MgrDtlCode desc");
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
                            TY_OA_MgrTaskDtlModel model = new TY_OA_MgrTaskDtlModel();
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

        private int _Update(TY_OA_MgrTaskDtlModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TY_OA_MgrTaskDtl set ");
            if (mObj.MgrDtlInfo != null)
            {
                builder.Append("MgrDtlInfo=@MgrDtlInfo,");
                this._DataProcess.ProcessParametersAdd("@MgrDtlInfo", SqlDbType.VarChar, 800, mObj.MgrDtlInfo);
            }
            if (mObj.DeadLine != DateTime.MinValue)
            {
                builder.Append("DeadLine=@DeadLine,");
                this._DataProcess.ProcessParametersAdd("@DeadLine", SqlDbType.DateTime, 8, mObj.DeadLine);
            }
            if (mObj.ResponsePerson != null)
            {
                builder.Append("ResponsePerson=@ResponsePerson,");
                this._DataProcess.ProcessParametersAdd("@ResponsePerson", SqlDbType.VarChar, 50, mObj.ResponsePerson);
            }
            if (mObj.MgrCodeID != 0)
            {
                builder.Append("MgrCodeID=@MgrCodeID,");
                this._DataProcess.ProcessParametersAdd("@MgrCodeID", SqlDbType.Int, 4, mObj.MgrCodeID);
            }
            if (mObj.Assistpersons != null)
            {
                builder.Append("Assistpersons=@Assistpersons,");
                this._DataProcess.ProcessParametersAdd("@Assistpersons", SqlDbType.VarChar, 500, mObj.Assistpersons);
            }
            if (mObj.TrancRevert != null)
            {
                builder.Append("TrancRevert=@TrancRevert,");
                this._DataProcess.ProcessParametersAdd("@TrancRevert", SqlDbType.VarChar, 0x3e8, mObj.TrancRevert);
            }
            if (mObj.ManagerRevert != null)
            {
                builder.Append("ManagerRevert=@ManagerRevert,");
                this._DataProcess.ProcessParametersAdd("@ManagerRevert", SqlDbType.VarChar, 0x3e8, mObj.ManagerRevert);
            }
            if (mObj.Isfinish != null)
            {
                builder.Append("Isfinish=@Isfinish,");
                this._DataProcess.ProcessParametersAdd("@Isfinish", SqlDbType.VarChar, 50, mObj.Isfinish);
            }
            if (mObj.State != null)
            {
                builder.Append("State=@State,");
                this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            }
            if (mObj.Bak != null)
            {
                builder.Append("Bak=@Bak,");
                this._DataProcess.ProcessParametersAdd("@Bak", SqlDbType.VarChar, 50, mObj.Bak);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where MgrDtlCode=@MgrDtlCode");
            this._DataProcess.ProcessParametersAdd("@MgrDtlCode", SqlDbType.Int, 4, mObj.MgrDtlCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(TY_OA_MgrTaskDtlModel mObj)
        {
            return this._Delete(mObj.MgrDtlCode);
        }

        public TY_OA_MgrTaskDtlModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        public List<TY_OA_MgrTaskDtlModel> GetTY_OA_MgrTaskDtlListByMgrCodeID(int MgrCodeID)
        {
            TY_OA_MgrTaskDtlQueryModel qmObj = new TY_OA_MgrTaskDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MgrDtlCode";
            qmObj.MgrCodeIDEqual = (MgrCodeID == 0) ? -1 : MgrCodeID;
            return this._Select(qmObj);
        }

        private void Initialize(SqlDataReader reader, TY_OA_MgrTaskDtlModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.MgrDtlCode = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.MgrDtlInfo = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.DeadLine = reader.GetDateTime(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.ResponsePerson = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.MgrCodeID = reader.GetInt32(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Assistpersons = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.TrancRevert = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.ManagerRevert = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Isfinish = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.State = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Bak = reader.GetString(10);
            }
        }

        public int Insert(TY_OA_MgrTaskDtlModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TY_OA_MgrTaskDtlModel> Select()
        {
            TY_OA_MgrTaskDtlQueryModel qmObj = new TY_OA_MgrTaskDtlQueryModel();
            return this._Select(qmObj);
        }

        public List<TY_OA_MgrTaskDtlModel> Select(TY_OA_MgrTaskDtlQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TY_OA_MgrTaskDtlModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

