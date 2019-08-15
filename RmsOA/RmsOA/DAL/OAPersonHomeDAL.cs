namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class OAPersonHomeDAL
    {
        private SqlDataProcess _DataProcess;

        public OAPersonHomeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public OAPersonHomeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private OAPersonHomeModel _DataBind(int Code)
        {
            OAPersonHomeModel model = new OAPersonHomeModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonHome ");
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
            builder.Append("delete from GK_OAPersonHome ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(OAPersonHomeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPersonHome(");
            builder.Append("personid,appname,polity,cname,idcard,workplace,duty,yesno,monthearn,phone,educational)");
            builder.Append(" values (");
            builder.Append("@personid,@appname,@polity,@cname,@idcard,@workplace,@duty,@yesno,@monthearn,@phone,@educational) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@personid", SqlDbType.VarChar, 50, mObj.personid);
            this._DataProcess.ProcessParametersAdd("@appname", SqlDbType.VarChar, 50, mObj.appname);
            this._DataProcess.ProcessParametersAdd("@polity", SqlDbType.VarChar, 50, mObj.polity);
            this._DataProcess.ProcessParametersAdd("@cname", SqlDbType.VarChar, 50, mObj.cname);
            this._DataProcess.ProcessParametersAdd("@idcard", SqlDbType.VarChar, 50, mObj.idcard);
            this._DataProcess.ProcessParametersAdd("@workplace", SqlDbType.VarChar, 50, mObj.workplace);
            this._DataProcess.ProcessParametersAdd("@duty", SqlDbType.VarChar, 50, mObj.duty);
            this._DataProcess.ProcessParametersAdd("@yesno", SqlDbType.VarChar, 50, mObj.yesno);
            this._DataProcess.ProcessParametersAdd("@monthearn", SqlDbType.VarChar, 50, mObj.monthearn);
            this._DataProcess.ProcessParametersAdd("@phone", SqlDbType.VarChar, 50, mObj.phone);
            this._DataProcess.ProcessParametersAdd("@educational", SqlDbType.VarChar, 50, mObj.educational);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<OAPersonHomeModel> _Select(OAPersonHomeQueryModel qmObj)
        {
            List<OAPersonHomeModel> list = new List<OAPersonHomeModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonHome ");
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
                            OAPersonHomeModel model = new OAPersonHomeModel();
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

        private int _Update(OAPersonHomeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPersonHome set ");
            if (mObj.personid != null)
            {
                builder.Append("personid=@personid,");
                this._DataProcess.ProcessParametersAdd("@personid", SqlDbType.VarChar, 50, mObj.personid);
            }
            if (mObj.appname != null)
            {
                builder.Append("appname=@appname,");
                this._DataProcess.ProcessParametersAdd("@appname", SqlDbType.VarChar, 50, mObj.appname);
            }
            if (mObj.polity != null)
            {
                builder.Append("polity=@polity,");
                this._DataProcess.ProcessParametersAdd("@polity", SqlDbType.VarChar, 50, mObj.polity);
            }
            if (mObj.cname != null)
            {
                builder.Append("cname=@cname,");
                this._DataProcess.ProcessParametersAdd("@cname", SqlDbType.VarChar, 50, mObj.cname);
            }
            if (mObj.idcard != null)
            {
                builder.Append("idcard=@idcard,");
                this._DataProcess.ProcessParametersAdd("@idcard", SqlDbType.VarChar, 50, mObj.idcard);
            }
            if (mObj.workplace != null)
            {
                builder.Append("workplace=@workplace,");
                this._DataProcess.ProcessParametersAdd("@workplace", SqlDbType.VarChar, 50, mObj.workplace);
            }
            if (mObj.duty != null)
            {
                builder.Append("duty=@duty,");
                this._DataProcess.ProcessParametersAdd("@duty", SqlDbType.VarChar, 50, mObj.duty);
            }
            if (mObj.yesno != null)
            {
                builder.Append("yesno=@yesno,");
                this._DataProcess.ProcessParametersAdd("@yesno", SqlDbType.VarChar, 50, mObj.yesno);
            }
            if (mObj.monthearn != null)
            {
                builder.Append("monthearn=@monthearn,");
                this._DataProcess.ProcessParametersAdd("@monthearn", SqlDbType.VarChar, 50, mObj.monthearn);
            }
            if (mObj.phone != null)
            {
                builder.Append("phone=@phone,");
                this._DataProcess.ProcessParametersAdd("@phone", SqlDbType.VarChar, 50, mObj.phone);
            }
            if (mObj.educational != null)
            {
                builder.Append("educational=@educational,");
                this._DataProcess.ProcessParametersAdd("@educational", SqlDbType.VarChar, 50, mObj.educational);
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

        public int Delete(OAPersonHomeModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public OAPersonHomeModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, OAPersonHomeModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.personid = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.appname = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.polity = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.cname = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.idcard = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.workplace = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.duty = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.yesno = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.monthearn = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.phone = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.educational = reader.GetString(11);
            }
        }

        public int Insert(OAPersonHomeModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<OAPersonHomeModel> Select()
        {
            OAPersonHomeQueryModel qmObj = new OAPersonHomeQueryModel();
            return this._Select(qmObj);
        }

        public List<OAPersonHomeModel> Select(OAPersonHomeQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(OAPersonHomeModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

