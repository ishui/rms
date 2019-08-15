namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class UserHelpUserDAL
    {
        private SqlDataProcess _DataProcess;

        public UserHelpUserDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public UserHelpUserDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private UserHelpUserModel _DataBind(int Code)
        {
            UserHelpUserModel model = new UserHelpUserModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from UserHelpUser ");
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
            builder.Append("delete from UserHelpUser ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(UserHelpUserModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO UserHelpUser (");
            builder2.Append("VALUES(");
            if (mObj.GroupCode != 0)
            {
                builder.Append("GroupCode,");
                builder2.Append("@GroupCode,");
                this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.Int, 4, mObj.GroupCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode,");
                builder2.Append("@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.AddDate != DateTime.MinValue)
            {
                builder.Append("AddDate,");
                builder2.Append("@AddDate,");
                this._DataProcess.ProcessParametersAdd("@AddDate", SqlDbType.DateTime, 8, mObj.AddDate);
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

        private List<UserHelpUserModel> _Select(UserHelpUserQueryModel qmObj)
        {
            List<UserHelpUserModel> list = new List<UserHelpUserModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from UserHelpUser ");
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
                            UserHelpUserModel model = new UserHelpUserModel();
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

        private int _Update(UserHelpUserModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update UserHelpUser set ");
            if (mObj.GroupCode != 0)
            {
                builder.Append("GroupCode=@GroupCode,");
                this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.Int, 4, mObj.GroupCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.AddDate != DateTime.MinValue)
            {
                builder.Append("AddDate=@AddDate,");
                this._DataProcess.ProcessParametersAdd("@AddDate", SqlDbType.DateTime, 8, mObj.AddDate);
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

        public int Delete(UserHelpUserModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public UserHelpUserModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, UserHelpUserModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.GroupCode = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.AddDate = reader.GetDateTime(3);
            }
        }

        public int Insert(UserHelpUserModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<UserHelpUserModel> Select()
        {
            UserHelpUserQueryModel qmObj = new UserHelpUserQueryModel();
            return this._Select(qmObj);
        }

        public List<UserHelpUserModel> Select(UserHelpUserQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(UserHelpUserModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

