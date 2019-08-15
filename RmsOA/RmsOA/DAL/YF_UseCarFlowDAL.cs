namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class YF_UseCarFlowDAL
    {
        private SqlDataProcess _DataProcess;

        public YF_UseCarFlowDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public YF_UseCarFlowDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private YF_UseCarFlowModel _DataBind(int Code)
        {
            YF_UseCarFlowModel model = new YF_UseCarFlowModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_UseCarFlow ");
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
            builder.Append("delete from YF_UseCarFlow ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(YF_UseCarFlowModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into YF_UseCarFlow(");
            builder.Append("ApplayDateTime,BillCode,DepartCode,UsePerson,GusetPerson,UserCarDateTime,AdressTo,Content,CarCode,RunKilometres,Status,Driver)");
            builder.Append(" values (");
            builder.Append("@ApplayDateTime,@BillCode,@DepartCode,@UsePerson,@GusetPerson,@UserCarDateTime,@AdressTo,@Content,@CarCode,@RunKilometres,@Status,@Driver) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ApplayDateTime", SqlDbType.VarChar, 50, mObj.ApplayDateTime);
            this._DataProcess.ProcessParametersAdd("@BillCode", SqlDbType.VarChar, 50, mObj.BillCode);
            this._DataProcess.ProcessParametersAdd("@DepartCode", SqlDbType.VarChar, 50, mObj.DepartCode);
            this._DataProcess.ProcessParametersAdd("@UsePerson", SqlDbType.VarChar, 50, mObj.UsePerson);
            this._DataProcess.ProcessParametersAdd("@GusetPerson", SqlDbType.VarChar, 50, mObj.GusetPerson);
            this._DataProcess.ProcessParametersAdd("@UserCarDateTime", SqlDbType.DateTime, 8, mObj.UserCarDateTime);
            this._DataProcess.ProcessParametersAdd("@AdressTo", SqlDbType.VarChar, 50, mObj.AdressTo);
            this._DataProcess.ProcessParametersAdd("@Content", SqlDbType.Text, 0x7fffffff, mObj.Content);
            this._DataProcess.ProcessParametersAdd("@CarCode", SqlDbType.VarChar, 50, mObj.CarCode);
            this._DataProcess.ProcessParametersAdd("@RunKilometres", SqlDbType.VarChar, 50, mObj.RunKilometres);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@Driver", SqlDbType.VarChar, 50, mObj.Driver);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<YF_UseCarFlowModel> _Select(YF_UseCarFlowQueryModel qmObj)
        {
            List<YF_UseCarFlowModel> list = new List<YF_UseCarFlowModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_UseCarFlow ");
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
                            YF_UseCarFlowModel model = new YF_UseCarFlowModel();
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

        private int _Update(YF_UseCarFlowModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update YF_UseCarFlow set ");
            if (mObj.ApplayDateTime != null)
            {
                builder.Append("ApplayDateTime=@ApplayDateTime,");
                this._DataProcess.ProcessParametersAdd("@ApplayDateTime", SqlDbType.VarChar, 50, mObj.ApplayDateTime);
            }
            if (mObj.BillCode != null)
            {
                builder.Append("BillCode=@BillCode,");
                this._DataProcess.ProcessParametersAdd("@BillCode", SqlDbType.VarChar, 50, mObj.BillCode);
            }
            if (mObj.DepartCode != null)
            {
                builder.Append("DepartCode=@DepartCode,");
                this._DataProcess.ProcessParametersAdd("@DepartCode", SqlDbType.VarChar, 50, mObj.DepartCode);
            }
            if (mObj.UsePerson != null)
            {
                builder.Append("UsePerson=@UsePerson,");
                this._DataProcess.ProcessParametersAdd("@UsePerson", SqlDbType.VarChar, 50, mObj.UsePerson);
            }
            if (mObj.GusetPerson != null)
            {
                builder.Append("GusetPerson=@GusetPerson,");
                this._DataProcess.ProcessParametersAdd("@GusetPerson", SqlDbType.VarChar, 50, mObj.GusetPerson);
            }
            if (mObj.UserCarDateTime != DateTime.MinValue)
            {
                builder.Append("UserCarDateTime=@UserCarDateTime,");
                this._DataProcess.ProcessParametersAdd("@UserCarDateTime", SqlDbType.DateTime, 8, mObj.UserCarDateTime);
            }
            if (mObj.AdressTo != null)
            {
                builder.Append("AdressTo=@AdressTo,");
                this._DataProcess.ProcessParametersAdd("@AdressTo", SqlDbType.VarChar, 50, mObj.AdressTo);
            }
            if (mObj.Content != null)
            {
                builder.Append("Content=@Content,");
                this._DataProcess.ProcessParametersAdd("@Content", SqlDbType.Text, 0x7fffffff, mObj.Content);
            }
            if (mObj.StartDateTime != DateTime.MinValue)
            {
                builder.Append("StartDateTime=@StartDateTime,");
                this._DataProcess.ProcessParametersAdd("@StartDateTime", SqlDbType.DateTime, 8, mObj.StartDateTime);
            }
            if (mObj.EndDateTime != DateTime.MinValue)
            {
                builder.Append("EndDateTime=@EndDateTime,");
                this._DataProcess.ProcessParametersAdd("@EndDateTime", SqlDbType.DateTime, 8, mObj.EndDateTime);
            }
            if (mObj.CarCode != null)
            {
                builder.Append("CarCode=@CarCode,");
                this._DataProcess.ProcessParametersAdd("@CarCode", SqlDbType.VarChar, 50, mObj.CarCode);
            }
            if (mObj.RunKilometres != null)
            {
                builder.Append("RunKilometres=@RunKilometres,");
                this._DataProcess.ProcessParametersAdd("@RunKilometres", SqlDbType.VarChar, 50, mObj.RunKilometres);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.Driver != null)
            {
                builder.Append("Driver=@Driver,");
                this._DataProcess.ProcessParametersAdd("@Driver", SqlDbType.VarChar, 50, mObj.Driver);
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

        public int Delete(YF_UseCarFlowModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public YF_UseCarFlowModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, YF_UseCarFlowModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ApplayDateTime = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.BillCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.DepartCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.UsePerson = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.GusetPerson = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.UserCarDateTime = reader.GetDateTime(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.AdressTo = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Content = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.StartDateTime = reader.GetDateTime(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.EndDateTime = reader.GetDateTime(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.CarCode = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.RunKilometres = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.Status = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.Driver = reader.GetString(14);
            }
        }

        public int Insert(YF_UseCarFlowModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<YF_UseCarFlowModel> Select()
        {
            YF_UseCarFlowQueryModel qmObj = new YF_UseCarFlowQueryModel();
            return this._Select(qmObj);
        }

        public List<YF_UseCarFlowModel> Select(YF_UseCarFlowQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(YF_UseCarFlowModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

