namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class RS_ScoreManageDAL
    {
        private SqlDataProcess _DataProcess;

        public RS_ScoreManageDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public RS_ScoreManageDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private RS_ScoreManageModel _DataBind(int Code)
        {
            RS_ScoreManageModel model = new RS_ScoreManageModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from RS_ScoreManage ");
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
            builder.Append("delete from RS_ScoreManage ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(RS_ScoreManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO RS_ScoreManage (");
            builder2.Append("VALUES(");
            if (mObj.DeptCode != null)
            {
                builder.Append("DeptCode,");
                builder2.Append("@DeptCode,");
                this._DataProcess.ProcessParametersAdd("@DeptCode", SqlDbType.VarChar, 10, mObj.DeptCode);
            }
            if (mObj.Marker != null)
            {
                builder.Append("Marker,");
                builder2.Append("@Marker,");
                this._DataProcess.ProcessParametersAdd("@Marker", SqlDbType.VarChar, 10, mObj.Marker);
            }
            if (mObj.MarkDate != DateTime.MinValue)
            {
                builder.Append("MarkDate,");
                builder2.Append("@MarkDate,");
                this._DataProcess.ProcessParametersAdd("@MarkDate", SqlDbType.DateTime, 8, mObj.MarkDate);
            }
            if (mObj.Type != -1)
            {
                builder.Append("Type,");
                builder2.Append("@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.Int, 4, mObj.Type);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 2, mObj.Status);
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

        private List<RS_ScoreManageModel> _Select(RS_ScoreManageQueryModel qmObj)
        {
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from RS_ScoreManage ");
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
                            RS_ScoreManageModel model = new RS_ScoreManageModel();
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

        private int _Update(RS_ScoreManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update RS_ScoreManage set ");
            if (mObj.DeptCode != null)
            {
                builder.Append("DeptCode=@DeptCode,");
                this._DataProcess.ProcessParametersAdd("@DeptCode", SqlDbType.VarChar, 10, mObj.DeptCode);
            }
            if (mObj.Marker != null)
            {
                builder.Append("Marker=@Marker,");
                this._DataProcess.ProcessParametersAdd("@Marker", SqlDbType.VarChar, 10, mObj.Marker);
            }
            if (mObj.MarkDate != DateTime.MinValue)
            {
                builder.Append("MarkDate=@MarkDate,");
                this._DataProcess.ProcessParametersAdd("@MarkDate", SqlDbType.DateTime, 8, mObj.MarkDate);
            }
            if (mObj.Type != 0)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.Int, 4, mObj.Type);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 2, mObj.Status);
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

        public int Delete(RS_ScoreManageModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public static List<UnitModel> GetAllDepts(SqlConnection sqlConn)
        {
            List<UnitModel> list = new List<UnitModel>();
            UnitModel item = new UnitModel();
            DataSet dataSet = new DataSet();
            ConferenceManageDAL edal = new ConferenceManageDAL(sqlConn);
            string cmdText = "Select UnitCode,UnitName from Unit";
            SqlCommand selectCommand = new SqlCommand(cmdText, sqlConn);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            using (SqlConnection connection = sqlConn)
            {
                try
                {
                    sqlConn.Open();
                    adapter.Fill(dataSet);
                }
                catch (SqlException)
                {
                }
            }
            if (dataSet != null)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    item = new UnitModel();
                    item.UnitCode = row["UnitCode"].ToString();
                    item.UnitName = row["UnitName"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public static string GetDeptEmployMarker(DateTime dt, string deptCode, SqlConnection sqlConn)
        {
            string text = null;
            string cmdText = "SELECT Marker FROM RS_ScoreManage WHERE MarkDate = @MarkDate AND Type=0 AND DeptCode=@DeptCode";
            SqlCommand command = new SqlCommand(cmdText, sqlConn);
            command.Parameters.Add("@MarkDate", SqlDbType.DateTime).Value = dt;
            command.Parameters.Add("@DeptCode", SqlDbType.VarChar, 50).Value = deptCode;
            using (SqlConnection connection = sqlConn)
            {
                sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    try
                    {
                        while (reader.Read())
                        {
                            if (reader.GetValue(0) != DBNull.Value)
                            {
                                text = reader.GetString(0);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    return text;
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }
            return text;
        }

        public static List<UnitScoreModel> GetDeptScoreTotal(DateTime dt, SqlConnection sqlConn)
        {
            List<UnitScoreModel> list = new List<UnitScoreModel>();
            UnitScoreModel item = new UnitScoreModel();
            string cmdText = "SELECT UserCode,Score FROM RS_EmployScore WHERE ManageCode = ( SELECT Code FROM RS_ScoreManage WHERE MarkDate = @MarkDate AND Type=2)";
            SqlCommand command = new SqlCommand(cmdText, sqlConn);
            command.Parameters.Add("@MarkDate", SqlDbType.DateTime).Value = dt;
            using (SqlConnection connection = sqlConn)
            {
                sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    try
                    {
                        while (reader.Read())
                        {
                            item = new UnitScoreModel();
                            if (reader.GetValue(0) != DBNull.Value)
                            {
                                item.DeptCode = reader.GetString(0);
                            }
                            if (reader.GetValue(1) != DBNull.Value)
                            {
                                item.Score = reader.GetInt32(1);
                            }
                            list.Add(item);
                        }
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    return list;
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }
            return list;
        }

        public static List<UserModel> GetLeadedUser(string userCode, SqlConnection sqlConn)
        {
            List<UserModel> list = new List<UserModel>();
            UserModel item = new UserModel();
            string cmdText = "SELECT cname FROM GK_OAPerson Where Leader=@UserCode";
            SqlCommand command = new SqlCommand(cmdText, sqlConn);
            command.Parameters.Add("@UserCode", SqlDbType.VarChar, 50).Value = userCode;
            using (SqlConnection connection = sqlConn)
            {
                sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    try
                    {
                        while (reader.Read())
                        {
                            item = new UserModel();
                            if (reader.GetValue(0) != DBNull.Value)
                            {
                                item.UserName = reader.GetString(0);
                            }
                            list.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    return list;
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }
            return list;
        }

        public static List<UnitModel> GetManageListByUserCode(string userCode, SqlConnection sqlConn)
        {
            List<UnitModel> list = new List<UnitModel>();
            UnitModel item = new UnitModel();
            DataSet dataSet = new DataSet();
            ConferenceManageDAL edal = new ConferenceManageDAL(sqlConn);
            string cmdText = "Select UnitCode,UnitName from Unit \r\n                                Where ParentUnitCode=\r\n                                (Select AccessRangeUnitCode from Station \r\n                                Where StationCode = (Select StationCode from UserRole Where UserCode=@UserCode))";
            SqlCommand selectCommand = new SqlCommand(cmdText, sqlConn);
            selectCommand.Parameters.Add("@UserCode", SqlDbType.VarChar, 50).Value = userCode;
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            using (SqlConnection connection = sqlConn)
            {
                try
                {
                    sqlConn.Open();
                    adapter.Fill(dataSet);
                }
                catch (SqlException)
                {
                }
            }
            if (dataSet != null)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    item = new UnitModel();
                    item.UnitCode = row["UnitCode"].ToString();
                    item.UnitName = row["UnitName"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public RS_ScoreManageModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, RS_ScoreManageModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.DeptCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Marker = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.MarkDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Type = reader.GetInt32(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Status = reader.GetString(5);
            }
        }

        public int Insert(RS_ScoreManageModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<RS_ScoreManageModel> Select()
        {
            RS_ScoreManageQueryModel qmObj = new RS_ScoreManageQueryModel();
            return this._Select(qmObj);
        }

        public List<RS_ScoreManageModel> Select(RS_ScoreManageQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(RS_ScoreManageModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

