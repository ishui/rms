namespace RmsOA.BFL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Rms.ORMap;
    using RmsOA.BLL;
    using RmsOA.MODEL;
    using RmsPM.DAL.EntityDAO;

    public class ConferenceManageBFL
    {
        public string BuildQueryString(string topic, string startTime, string endTime, string dept, string place, string chaterMember)
        {
            return ConferenceManageBLL.BuildQueryString(topic, startTime, endTime, dept, place, chaterMember);
        }

        public int DayOfWeekToInt(DateTime dtTime)
        {
            switch (dtTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 6;

                case DayOfWeek.Monday:
                    return 0;

                case DayOfWeek.Tuesday:
                    return 1;

                case DayOfWeek.Wednesday:
                    return 2;

                case DayOfWeek.Thursday:
                    return 3;

                case DayOfWeek.Friday:
                    return 4;

                case DayOfWeek.Saturday:
                    return 5;
            }
            return -1;
        }

        public int Delete(ConferenceManageModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new ConferenceManageBLL().Delete(ObjModel.Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public ConferenceManageModel GetConferenceManage(int Code)
        {
            ConferenceManageModel model = new ConferenceManageModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new ConferenceManageBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<ConferenceManageModel> GetConferenceManageList(ConferenceManageQueryModel QueryModel)
        {
            List<ConferenceManageModel> models = new List<ConferenceManageModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new ConferenceManageQueryModel();
                    }
                    models = new ConferenceManageBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ConferenceManageModel> GetConferenceManageList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string TopicEqual, string ChaterMemberEqual, string TypeEqual, string PlaceEqual, string DeptEqual, DateTime StartTimeEqual, DateTime EndTimeEqual, string RemarkEqual, string StateEqual)
        {
            List<ConferenceManageModel> models = new List<ConferenceManageModel>();
            ConferenceManageQueryModel objQueryModel = new ConferenceManageQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.TopicEqual = TopicEqual;
            objQueryModel.ChaterMemberEqual = ChaterMemberEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.PlaceEqual = PlaceEqual;
            objQueryModel.DeptEqual = DeptEqual;
            objQueryModel.StartTimeEqual = StartTimeEqual;
            objQueryModel.EndTimeEqual = EndTimeEqual;
            objQueryModel.RemarkEqual = RemarkEqual;
            objQueryModel.StateEqual = StateEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new ConferenceManageBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ConferenceManageModel> GetConferenceManageListOne(int Code)
        {
            List<ConferenceManageModel> list = new List<ConferenceManageModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    ConferenceManageBLL ebll = new ConferenceManageBLL();
                    list.Add(ebll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public ArrayList GetConferenceType()
        {
            ArrayList list = new ArrayList();
            EntityData dictionaryItemByName = SystemManageDAO.GetDictionaryItemByName("会议类型");
            foreach (DataRow row in dictionaryItemByName.Tables[0].Rows)
            {
                list.Add(row["Name"].ToString());
            }
            return list;
        }

        public List<ConferenceManageModel> GetSearchResultConferenceList(string strQuery)
        {
            List<ConferenceManageModel> list = new List<ConferenceManageModel>();
            ConferenceManageQueryModel queryModel = new ConferenceManageQueryModel();
            queryModel.QueryConditionStr = strQuery;
            return this.GetConferenceManageList(queryModel);
        }

        public DateTime GetUsefulDate(string getTime)
        {
            DateTime result;
            if (DateTime.TryParse(getTime, out result))
            {
                if (result.Year <= 0x708)
                {
                    return DateTime.Now;
                }
                return result;
            }
            return DateTime.Now;
        }

        public DateTime[] GetWeekBeginAndEndDate(DateTime dtTime)
        {
            DateTime[] timeArray = new DateTime[2];
            int num = this.DayOfWeekToInt(dtTime);
            DateTime time = dtTime.AddDays((double) -num);
            DateTime time2 = dtTime.AddDays((double) (6 - num));
            timeArray[0] = time;
            timeArray[1] = time2;
            return timeArray;
        }

        public List<ConferenceManageModel> GetWeekConferenceList(DateTime dtTime)
        {
            List<ConferenceManageModel> models = new List<ConferenceManageModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                ConferenceManageQueryModel objQueryModel = this.SetConferenceQueryModel(dtTime);
                try
                {
                    connection.Open();
                    models = new ConferenceManageBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public int Insert(ConferenceManageModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new ConferenceManageBLL().Insert(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public ConferenceManageQueryModel SetConferenceQueryModel(DateTime dtTime)
        {
            ConferenceManageQueryModel model = new ConferenceManageQueryModel();
            DateTime[] weekBeginAndEndDate = this.GetWeekBeginAndEndDate(dtTime.Date);
            DateTime time = weekBeginAndEndDate[0];
            DateTime time2 = weekBeginAndEndDate[1];
            model.WeekStartTimeEqual = time;
            model.WeekEndTimeEqual = time2;
            model.StateEqual = MeetStateType.Authored.ToString();
            return model;
        }

        public int Update(ConferenceManageModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new ConferenceManageBLL().Update(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }
    }
}

