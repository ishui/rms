namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_MeetSummaryDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_MeetSummaryDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_MeetSummaryDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_MeetSummaryModel _DataBind(int Code)
        {
            GK_OA_MeetSummaryModel model = new GK_OA_MeetSummaryModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_MeetSummary ");
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
            builder.Append("delete from GK_OA_MeetSummary ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_MeetSummaryModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO GK_OA_MeetSummary (");
            builder2.Append("VALUES(");
            if (mObj.SortCode != null)
            {
                builder.Append("SortCode,");
                builder2.Append("@SortCode,");
                this._DataProcess.ProcessParametersAdd("@SortCode", SqlDbType.VarChar, 200, mObj.SortCode);
            }
            if (mObj.CodeRule != null)
            {
                builder.Append("CodeRule,");
                builder2.Append("@CodeRule,");
                this._DataProcess.ProcessParametersAdd("@CodeRule", SqlDbType.VarChar, 200, mObj.CodeRule);
            }
            if (mObj.MeetStartTime != DateTime.MinValue)
            {
                builder.Append("MeetStartTime,");
                builder2.Append("@MeetStartTime,");
                this._DataProcess.ProcessParametersAdd("@MeetStartTime", SqlDbType.DateTime, 8, mObj.MeetStartTime);
            }
            if (mObj.Place != null)
            {
                builder.Append("Place,");
                builder2.Append("@Place,");
                this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 200, mObj.Place);
            }
            if (mObj.Title != null)
            {
                builder.Append("Title,");
                builder2.Append("@Title,");
                this._DataProcess.ProcessParametersAdd("@Title", SqlDbType.VarChar, 200, mObj.Title);
            }
            if (mObj.AttendPersons != null)
            {
                builder.Append("AttendPersons,");
                builder2.Append("@AttendPersons,");
                this._DataProcess.ProcessParametersAdd("@AttendPersons", SqlDbType.VarChar, 800, mObj.AttendPersons);
            }
            if (mObj.Recoder != null)
            {
                builder.Append("Recoder,");
                builder2.Append("@Recoder,");
                this._DataProcess.ProcessParametersAdd("@Recoder", SqlDbType.VarChar, 50, mObj.Recoder);
            }
            if (mObj.Context != null)
            {
                builder.Append("Context,");
                builder2.Append("@Context,");
                this._DataProcess.ProcessParametersAdd("@Context", SqlDbType.Text, 0x186a0, mObj.Context);
            }
            if (mObj.OtherContext != null)
            {
                builder.Append("OtherContext,");
                builder2.Append("@OtherContext,");
                this._DataProcess.ProcessParametersAdd("@OtherContext", SqlDbType.VarChar, 0x1f40, mObj.OtherContext);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type,");
                builder2.Append("@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 200, mObj.Type);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept,");
                builder2.Append("@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 200, mObj.Status);
            }
            if (mObj.CharterMember != null)
            {
                builder.Append("CharterMember,");
                builder2.Append("@CharterMember,");
                this._DataProcess.ProcessParametersAdd("@CharterMember", SqlDbType.VarChar, 50, mObj.CharterMember);
            }
            if (mObj.OtherPerson != null)
            {
                builder.Append("OtherPerson,");
                builder2.Append("@OtherPerson,");
                this._DataProcess.ProcessParametersAdd("@OtherPerson", SqlDbType.VarChar, 500, mObj.OtherPerson);
            }
            if (mObj.MeetEndTime != DateTime.MinValue)
            {
                builder.Append("MeetEndTime,");
                builder2.Append("@MeetEndTime,");
                this._DataProcess.ProcessParametersAdd("@MeetEndTime", SqlDbType.DateTime, 8, mObj.MeetEndTime);
            }
            if (mObj.SmallTitle != null)
            {
                builder.Append("SmallTitle,");
                builder2.Append("@SmallTitle,");
                this._DataProcess.ProcessParametersAdd("@SmallTitle", SqlDbType.VarChar, 50, mObj.SmallTitle);
            }
            if (mObj.SendStatus != null)
            {
                builder.Append("SendStatus,");
                builder2.Append("@SendStatus,");
                this._DataProcess.ProcessParametersAdd("@SendStatus", SqlDbType.VarChar, 50, mObj.SendStatus);
            }
            if (mObj.PreLeave != null)
            {
                builder.Append("PreLeave,");
                builder2.Append("@PreLeave,");
                this._DataProcess.ProcessParametersAdd("@PreLeave", SqlDbType.VarChar, 200, mObj.PreLeave);
            }
            if (mObj.SubmitTime != DateTime.MinValue)
            {
                builder.Append("SubmitTime,");
                builder2.Append("@SubmitTime,");
                this._DataProcess.ProcessParametersAdd("@SubmitTime", SqlDbType.DateTime, 8, mObj.SubmitTime);
            }
            if (mObj.Submiter != null)
            {
                builder.Append("Submiter,");
                builder2.Append("@Submiter,");
                this._DataProcess.ProcessParametersAdd("@Submiter", SqlDbType.VarChar, 50, mObj.Submiter);
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

        private List<GK_OA_MeetSummaryModel> _Select(GK_OA_MeetSummaryQueryModel qmObj)
        {
            List<GK_OA_MeetSummaryModel> list = new List<GK_OA_MeetSummaryModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_MeetSummary ");
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
                            GK_OA_MeetSummaryModel model = new GK_OA_MeetSummaryModel();
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

        private int _Update(GK_OA_MeetSummaryModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_MeetSummary set ");
            if (mObj.SortCode != null)
            {
                builder.Append("SortCode=@SortCode,");
                this._DataProcess.ProcessParametersAdd("@SortCode", SqlDbType.VarChar, 200, mObj.SortCode);
            }
            if (mObj.CodeRule != null)
            {
                builder.Append("CodeRule=@CodeRule,");
                this._DataProcess.ProcessParametersAdd("@CodeRule", SqlDbType.VarChar, 200, mObj.CodeRule);
            }
            if (mObj.MeetStartTime != DateTime.MinValue)
            {
                builder.Append("MeetStartTime=@MeetStartTime,");
                this._DataProcess.ProcessParametersAdd("@MeetStartTime", SqlDbType.DateTime, 8, mObj.MeetStartTime);
            }
            if (mObj.Place != null)
            {
                builder.Append("Place=@Place,");
                this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 200, mObj.Place);
            }
            if (mObj.Title != null)
            {
                builder.Append("Title=@Title,");
                this._DataProcess.ProcessParametersAdd("@Title", SqlDbType.VarChar, 200, mObj.Title);
            }
            if (mObj.AttendPersons != null)
            {
                builder.Append("AttendPersons=@AttendPersons,");
                this._DataProcess.ProcessParametersAdd("@AttendPersons", SqlDbType.VarChar, 800, mObj.AttendPersons);
            }
            if (mObj.Recoder != null)
            {
                builder.Append("Recoder=@Recoder,");
                this._DataProcess.ProcessParametersAdd("@Recoder", SqlDbType.VarChar, 50, mObj.Recoder);
            }
            if (mObj.Context != null)
            {
                builder.Append("Context=@Context,");
                this._DataProcess.ProcessParametersAdd("@Context", SqlDbType.Text, 0x186a0, mObj.Context);
            }
            if (mObj.OtherContext != null)
            {
                builder.Append("OtherContext=@OtherContext,");
                this._DataProcess.ProcessParametersAdd("@OtherContext", SqlDbType.VarChar, 0x1f40, mObj.OtherContext);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 200, mObj.Type);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept=@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 200, mObj.Status);
            }
            if (mObj.CharterMember != null)
            {
                builder.Append("CharterMember=@CharterMember,");
                this._DataProcess.ProcessParametersAdd("@CharterMember", SqlDbType.VarChar, 50, mObj.CharterMember);
            }
            if (mObj.OtherPerson != null)
            {
                builder.Append("OtherPerson=@OtherPerson,");
                this._DataProcess.ProcessParametersAdd("@OtherPerson", SqlDbType.VarChar, 500, mObj.OtherPerson);
            }
            if (mObj.MeetEndTime != DateTime.MinValue)
            {
                builder.Append("MeetEndTime=@MeetEndTime,");
                this._DataProcess.ProcessParametersAdd("@MeetEndTime", SqlDbType.DateTime, 8, mObj.MeetEndTime);
            }
            if (mObj.SmallTitle != null)
            {
                builder.Append("SmallTitle=@SmallTitle,");
                this._DataProcess.ProcessParametersAdd("@SmallTitle", SqlDbType.VarChar, 50, mObj.SmallTitle);
            }
            if (mObj.SendStatus != null)
            {
                builder.Append("SendStatus=@SendStatus,");
                this._DataProcess.ProcessParametersAdd("@SendStatus", SqlDbType.VarChar, 50, mObj.SendStatus);
            }
            if (mObj.PreLeave != null)
            {
                builder.Append("PreLeave=@PreLeave,");
                this._DataProcess.ProcessParametersAdd("@PreLeave", SqlDbType.VarChar, 200, mObj.PreLeave);
            }
            if (mObj.SubmitTime != DateTime.MinValue)
            {
                builder.Append("SubmitTime=@SubmitTime,");
                this._DataProcess.ProcessParametersAdd("@SubmitTime", SqlDbType.DateTime, 8, mObj.SubmitTime);
            }
            if (mObj.Submiter != null)
            {
                builder.Append("Submiter=@Submiter,");
                this._DataProcess.ProcessParametersAdd("@Submiter", SqlDbType.VarChar, 50, mObj.Submiter);
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

        public int Delete(GK_OA_MeetSummaryModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_MeetSummaryModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_MeetSummaryModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.SortCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.CodeRule = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.MeetStartTime = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Place = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Title = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.AttendPersons = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Recoder = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Context = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.OtherContext = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Type = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Dept = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Status = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.CharterMember = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.OtherPerson = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.MeetEndTime = reader.GetDateTime(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.SmallTitle = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.SendStatus = reader.GetString(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.PreLeave = reader.GetString(0x12);
            }
            if (reader.GetValue(0x13) != DBNull.Value)
            {
                obj.SubmitTime = reader.GetDateTime(0x13);
            }
            if (reader.GetValue(20) != DBNull.Value)
            {
                obj.Submiter = reader.GetString(20);
            }
        }

        public int Insert(GK_OA_MeetSummaryModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_MeetSummaryModel> Select()
        {
            GK_OA_MeetSummaryQueryModel qmObj = new GK_OA_MeetSummaryQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_MeetSummaryModel> Select(GK_OA_MeetSummaryQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_MeetSummaryModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

