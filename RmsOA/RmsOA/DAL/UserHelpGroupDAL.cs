namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class UserHelpGroupDAL
    {
        private SqlDataProcess _DataProcess;

        public UserHelpGroupDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public UserHelpGroupDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private UserHelpGroupModel _DataBind(int Code)
        {
            UserHelpGroupModel model = new UserHelpGroupModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from UserHelpGroup ");
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
            builder.Append("delete from UserHelpGroup ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(UserHelpGroupModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO UserHelpGroup (");
            builder2.Append("VALUES(");
            if (mObj.GroupName != null)
            {
                builder.Append("GroupName,");
                builder2.Append("@GroupName,");
                this._DataProcess.ProcessParametersAdd("@GroupName", SqlDbType.VarChar, 200, mObj.GroupName);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode,");
                builder2.Append("@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.CreateTime != DateTime.MinValue)
            {
                builder.Append("CreateTime,");
                builder2.Append("@CreateTime,");
                this._DataProcess.ProcessParametersAdd("@CreateTime", SqlDbType.DateTime, 8, mObj.CreateTime);
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

        private List<UserHelpGroupModel> _Select(UserHelpGroupQueryModel qmObj)
        {
            List<UserHelpGroupModel> list = new List<UserHelpGroupModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from UserHelpGroup ");
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
                            UserHelpGroupModel model = new UserHelpGroupModel();
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

        private int _Update(UserHelpGroupModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update UserHelpGroup set ");
            if (mObj.GroupName != null)
            {
                builder.Append("GroupName=@GroupName,");
                this._DataProcess.ProcessParametersAdd("@GroupName", SqlDbType.VarChar, 200, mObj.GroupName);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.CreateTime != DateTime.MinValue)
            {
                builder.Append("CreateTime=@CreateTime,");
                this._DataProcess.ProcessParametersAdd("@CreateTime", SqlDbType.DateTime, 8, mObj.CreateTime);
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

        public int Delete(UserHelpGroupModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public UserHelpGroupModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, UserHelpGroupModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.GroupName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.CreateTime = reader.GetDateTime(3);
            }
        }

        public int Insert(UserHelpGroupModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<UserHelpGroupModel> Select()
        {
            UserHelpGroupQueryModel qmObj = new UserHelpGroupQueryModel();
            return this._Select(qmObj);
        }

        public List<UserHelpGroupModel> Select(UserHelpGroupQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(UserHelpGroupModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

