namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class ConferenceUserListDAL
    {
        private SqlDataProcess _DataProcess;

        public ConferenceUserListDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public ConferenceUserListDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private ConferenceUserListModel _DataBind(int Code)
        {
            ConferenceUserListModel model = new ConferenceUserListModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from ConferenceUserList ");
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
            builder.Append("delete from ConferenceUserList ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(ConferenceUserListModel mObj)
        {
            if (mObj.UserCode == null)
            {
                mObj.UserCode = "";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ConferenceUserList(");
            builder.Append("ConferenceCode,UserCode,Type,UserName,State)");
            builder.Append(" values (");
            builder.Append("@ConferenceCode,@UserCode,@Type,@UserName,@State) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ConferenceCode", SqlDbType.Int, 4, mObj.ConferenceCode);
            this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<ConferenceUserListModel> _Select(ConferenceUserListQueryModel qmObj)
        {
            List<ConferenceUserListModel> list = new List<ConferenceUserListModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from ConferenceUserList ");
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
                            ConferenceUserListModel model = new ConferenceUserListModel();
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

        private int _Update(ConferenceUserListModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ConferenceUserList set ");
            if (mObj.ConferenceCode != 0)
            {
                builder.Append("ConferenceCode=@ConferenceCode,");
                this._DataProcess.ProcessParametersAdd("@ConferenceCode", SqlDbType.Int, 4, mObj.ConferenceCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.UserName != null)
            {
                builder.Append("UserName=@UserName,");
                this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            }
            if (mObj.State != null)
            {
                builder.Append("State=@State,");
                this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
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

        public int Delete(ConferenceUserListModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public ConferenceUserListModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, ConferenceUserListModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.ConferenceCode = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Type = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.UserName = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.State = reader.GetString(5);
            }
        }

        public int Insert(ConferenceUserListModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<ConferenceUserListModel> Select()
        {
            ConferenceUserListQueryModel qmObj = new ConferenceUserListQueryModel();
            return this._Select(qmObj);
        }

        public List<ConferenceUserListModel> Select(ConferenceUserListQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(ConferenceUserListModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

