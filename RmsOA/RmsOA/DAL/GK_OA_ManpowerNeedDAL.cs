namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_ManpowerNeedDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_ManpowerNeedDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_ManpowerNeedDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_ManpowerNeedModel _DataBind(int Code)
        {
            GK_OA_ManpowerNeedModel model = new GK_OA_ManpowerNeedModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ManpowerNeed ");
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
            builder.Append("delete from GK_OA_ManpowerNeed ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_ManpowerNeedModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_ManpowerNeed(");
            builder.Append("SystemCode,FileCode,UnitCode,RegistDate,StationCode,PersonNumber,Education,Seniority,YearOfWork,Treatment,Remark,Status,Field1)");
            builder.Append(" values (");
            builder.Append("@SystemCode,@FileCode,@UnitCode,@RegistDate,@StationCode,@PersonNumber,@Education,@Seniority,@YearOfWork,@Treatment,@Remark,@Status,@Field1) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@RegistDate", SqlDbType.DateTime, 8, mObj.RegistDate);
            this._DataProcess.ProcessParametersAdd("@StationCode", SqlDbType.VarChar, 50, mObj.StationCode);
            this._DataProcess.ProcessParametersAdd("@PersonNumber", SqlDbType.VarChar, 50, mObj.PersonNumber);
            this._DataProcess.ProcessParametersAdd("@Education", SqlDbType.VarChar, 50, mObj.Education);
            this._DataProcess.ProcessParametersAdd("@Seniority", SqlDbType.VarChar, 50, mObj.Seniority);
            this._DataProcess.ProcessParametersAdd("@YearOfWork", SqlDbType.VarChar, 50, mObj.YearOfWork);
            this._DataProcess.ProcessParametersAdd("@Treatment", SqlDbType.VarChar, 50, mObj.Treatment);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 500, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_ManpowerNeedModel> _Select(GK_OA_ManpowerNeedQueryModel qmObj)
        {
            List<GK_OA_ManpowerNeedModel> list = new List<GK_OA_ManpowerNeedModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ManpowerNeed ");
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
                            GK_OA_ManpowerNeedModel model = new GK_OA_ManpowerNeedModel();
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

        private int _Update(GK_OA_ManpowerNeedModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_ManpowerNeed set ");
            if (mObj.SystemCode != null)
            {
                builder.Append("SystemCode=@SystemCode,");
                this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            }
            if (mObj.FileCode != null)
            {
                builder.Append("FileCode=@FileCode,");
                this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.RegistDate != DateTime.MinValue)
            {
                builder.Append("RegistDate=@RegistDate,");
                this._DataProcess.ProcessParametersAdd("@RegistDate", SqlDbType.DateTime, 8, mObj.RegistDate);
            }
            if (mObj.StationCode != null)
            {
                builder.Append("StationCode=@StationCode,");
                this._DataProcess.ProcessParametersAdd("@StationCode", SqlDbType.VarChar, 50, mObj.StationCode);
            }
            if (mObj.PersonNumber != null)
            {
                builder.Append("PersonNumber=@PersonNumber,");
                this._DataProcess.ProcessParametersAdd("@PersonNumber", SqlDbType.VarChar, 50, mObj.PersonNumber);
            }
            if (mObj.Education != null)
            {
                builder.Append("Education=@Education,");
                this._DataProcess.ProcessParametersAdd("@Education", SqlDbType.VarChar, 50, mObj.Education);
            }
            if (mObj.Seniority != null)
            {
                builder.Append("Seniority=@Seniority,");
                this._DataProcess.ProcessParametersAdd("@Seniority", SqlDbType.VarChar, 50, mObj.Seniority);
            }
            if (mObj.YearOfWork != null)
            {
                builder.Append("YearOfWork=@YearOfWork,");
                this._DataProcess.ProcessParametersAdd("@YearOfWork", SqlDbType.VarChar, 50, mObj.YearOfWork);
            }
            if (mObj.Treatment != null)
            {
                builder.Append("Treatment=@Treatment,");
                this._DataProcess.ProcessParametersAdd("@Treatment", SqlDbType.VarChar, 50, mObj.Treatment);
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

        public int Delete(GK_OA_ManpowerNeedModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_ManpowerNeedModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_ManpowerNeedModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.SystemCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.FileCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.RegistDate = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.StationCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.PersonNumber = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Education = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Seniority = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.YearOfWork = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Treatment = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Remark = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Status = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.Field1 = reader.GetString(13);
            }
        }

        public int Insert(GK_OA_ManpowerNeedModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_ManpowerNeedModel> Select()
        {
            GK_OA_ManpowerNeedQueryModel qmObj = new GK_OA_ManpowerNeedQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_ManpowerNeedModel> Select(GK_OA_ManpowerNeedQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_ManpowerNeedModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

