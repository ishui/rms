namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.BFL;
    using RmsOA.MODEL;

    public class ConferenceManageDAL
    {
        private SqlDataProcess _DataProcess;

        public ConferenceManageDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public ConferenceManageDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private ConferenceManageModel _DataBind(int Code)
        {
            ConferenceManageModel model = new ConferenceManageModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from ConferenceManage ");
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
            builder.Append("delete from ConferenceManage ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(ConferenceManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ConferenceManage(");
            builder.Append("Topic,ChaterMember,Type,Place,Dept,StartTime,EndTime,Remark,State)");
            builder.Append(" values (");
            builder.Append("@Topic,@ChaterMember,@Type,@Place,@Dept,@StartTime,@EndTime,@Remark,@State) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Topic", SqlDbType.VarChar, 500, mObj.Topic);
            this._DataProcess.ProcessParametersAdd("@ChaterMember", SqlDbType.VarChar, 50, mObj.ChaterMember);
            this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 100, mObj.Type);
            this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 100, mObj.Place);
            this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 100, mObj.Dept);
            this._DataProcess.ProcessParametersAdd("@StartTime", SqlDbType.DateTime, 4, mObj.StartTime);
            this._DataProcess.ProcessParametersAdd("@EndTime", SqlDbType.DateTime, 4, mObj.EndTime);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<ConferenceManageModel> _Select(ConferenceManageQueryModel qmObj)
        {
            List<ConferenceManageModel> list = new List<ConferenceManageModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from ConferenceManage ");
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
                            ConferenceManageModel model = new ConferenceManageModel();
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

        private int _Update(ConferenceManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ConferenceManage set ");
            if (mObj.Topic != null)
            {
                builder.Append("Topic=@Topic,");
                this._DataProcess.ProcessParametersAdd("@Topic", SqlDbType.VarChar, 500, mObj.Topic);
            }
            if (mObj.ChaterMember != null)
            {
                builder.Append("ChaterMember=@ChaterMember,");
                this._DataProcess.ProcessParametersAdd("@ChaterMember", SqlDbType.VarChar, 50, mObj.ChaterMember);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 100, mObj.Type);
            }
            if (mObj.Place != null)
            {
                builder.Append("Place=@Place,");
                this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 100, mObj.Place);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept=@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 100, mObj.Dept);
            }
            if (mObj.StartTime != DateTime.MinValue)
            {
                builder.Append("StartTime=@StartTime,");
                this._DataProcess.ProcessParametersAdd("@StartTime", SqlDbType.DateTime, 4, mObj.StartTime);
            }
            if (mObj.EndTime != DateTime.MinValue)
            {
                builder.Append("EndTime=@EndTime,");
                this._DataProcess.ProcessParametersAdd("@EndTime", SqlDbType.DateTime, 4, mObj.EndTime);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
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

        public static string BuildQueryString(string topic, string startTime, string endTime, string dept, string place, string chaterMember)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("WHERE ");
            if (!topic.Equals(string.Empty))
            {
                builder.Append("Topic LIKE '%" + topic + "%' AND ");
            }
            if (!startTime.Equals(string.Empty))
            {
                builder.Append("StartTime >= '" + startTime + "' AND ");
            }
            if (!endTime.Equals(string.Empty))
            {
                builder.Append("StartTime <= '" + endTime + "' AND ");
            }
            if (!dept.Equals(string.Empty))
            {
                builder.Append("Dept = '" + dept + "' AND ");
            }
            if (!place.Equals(string.Empty))
            {
                builder.Append("Place = '" + place + "' AND ");
            }
            if (!chaterMember.Equals(string.Empty))
            {
                builder.Append("ChaterMember = '" + chaterMember + "' AND ");
            }
            builder.Append(" State='" + MeetStateType.Authored.ToString() + "' AND 1=1");
            return builder.ToString();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(ConferenceManageModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public ConferenceManageModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        public List<RoomModelQuery> GetRoomUseStatus(string roomCode, string begin, string end)
        {
            List<RoomModelQuery> list = new List<RoomModelQuery>();
            RoomModelQuery item = new RoomModelQuery();
            if ((string.IsNullOrEmpty(roomCode) || string.IsNullOrEmpty(begin)) || string.IsNullOrEmpty(end))
            {
                return null;
            }
            this._DataProcess.CommandText = "SELECT Code,Topic,ChaterMember,StartTime,EndTime FROM ConferenceManage\r\nWHERE\r\n(\r\nPlace=@CODE AND\r\n(\r\n(STARTTIME >=@STARTTIME AND StartTime <= @ENDTIME) OR \r\n(ENDTIME >=@STARTTIME AND ENDTIME<=@ENDTIME)\r\n)\r\n)";
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, int.Parse(roomCode));
            this._DataProcess.ProcessParametersAdd("@STARTTIME", SqlDbType.DateTime, 8, DateTime.Parse(begin));
            this._DataProcess.ProcessParametersAdd("@ENDTIME", SqlDbType.DateTime, 8, DateTime.Parse(end));
            SqlDataReader sqlDataReader = this._DataProcess.GetSqlDataReader();
            using (SqlDataReader reader2 = sqlDataReader)
            {
                try
                {
                    while (sqlDataReader.Read())
                    {
                        item = new RoomModelQuery();
                        if (!sqlDataReader.GetValue(0).Equals(DBNull.Value))
                        {
                            item.MeetCode = sqlDataReader.GetInt32(0);
                        }
                        if (!sqlDataReader.GetValue(1).Equals(DBNull.Value))
                        {
                            item.Topic = sqlDataReader.GetString(1);
                        }
                        if (!sqlDataReader.GetValue(2).Equals(DBNull.Value))
                        {
                            item.ChaterMember = sqlDataReader.GetString(2);
                        }
                        if (!sqlDataReader.GetValue(3).Equals(DBNull.Value))
                        {
                            item.TimeAge = string.Format("{0}", sqlDataReader.GetDateTime(3));
                        }
                        if (!sqlDataReader.GetValue(4).Equals(DBNull.Value))
                        {
                            item.TimeAge = string.Format("{0}+{1}", item.TimeAge, sqlDataReader.GetDateTime(4));
                        }
                        list.Add(item);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public List<ConferenceManageModel> GetUserMeet(string userCode, DateTime begin, DateTime end)
        {
            List<ConferenceManageModel> list = new List<ConferenceManageModel>();
            ConferenceManageModel item = new ConferenceManageModel();
            this._DataProcess.CommandText = "SELECT Code,Topic,ChaterMember,Type,Place,StartTime,EndTime,Dept\r\nFROM CONFERENCEMANAGE \r\nWHERE (STARTTIME >=@TimeBegin AND STARTTIME <=@TimeEnd)\r\nAND STATE ='AUTHORED'\r\nAND ( CODE IN\r\n(\r\nSELECT CONFERENCECODE  FROM CONFERENCEUSERLIST\r\nWHERE USERCODE =@UserCode\r\n) OR CHATERMEMBER =@UserCode\r\n)";
            this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, userCode);
            this._DataProcess.ProcessParametersAdd("@TimeBegin", SqlDbType.DateTime, 8, begin);
            this._DataProcess.ProcessParametersAdd("@TimeEnd", SqlDbType.DateTime, 8, end);
            SqlDataReader sqlDataReader = this._DataProcess.GetSqlDataReader();
            using (SqlDataReader reader2 = sqlDataReader)
            {
                try
                {
                    while (sqlDataReader.Read())
                    {
                        item = new ConferenceManageModel();
                        if (!sqlDataReader.GetValue(0).Equals(DBNull.Value))
                        {
                            item.Code = sqlDataReader.GetInt32(0);
                        }
                        if (!sqlDataReader.GetValue(1).Equals(DBNull.Value))
                        {
                            item.Topic = sqlDataReader.GetString(1);
                        }
                        if (!sqlDataReader.GetValue(2).Equals(DBNull.Value))
                        {
                            item.ChaterMember = sqlDataReader.GetString(2);
                        }
                        if (!sqlDataReader.GetValue(3).Equals(DBNull.Value))
                        {
                            item.Type = sqlDataReader.GetString(3);
                        }
                        if (!sqlDataReader.GetValue(4).Equals(DBNull.Value))
                        {
                            item.Place = sqlDataReader.GetString(4);
                        }
                        if (!sqlDataReader.GetValue(5).Equals(DBNull.Value))
                        {
                            item.StartTime = sqlDataReader.GetDateTime(5);
                        }
                        if (!sqlDataReader.GetValue(6).Equals(DBNull.Value))
                        {
                            item.EndTime = sqlDataReader.GetDateTime(6);
                        }
                        if (!sqlDataReader.GetValue(7).Equals(DBNull.Value))
                        {
                            item.Dept = sqlDataReader.GetString(7);
                        }
                        else
                        {
                            item.Dept = string.Empty;
                        }
                        list.Add(item);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        private void Initialize(SqlDataReader reader, ConferenceManageModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Topic = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.ChaterMember = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Type = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Place = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Dept = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.StartTime = reader.GetDateTime(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.EndTime = reader.GetDateTime(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Remark = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.State = reader.GetString(9);
            }
        }

        public int Insert(ConferenceManageModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<ConferenceManageModel> Select()
        {
            ConferenceManageQueryModel qmObj = new ConferenceManageQueryModel();
            return this._Select(qmObj);
        }

        public List<ConferenceManageModel> Select(ConferenceManageQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(ConferenceManageModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

