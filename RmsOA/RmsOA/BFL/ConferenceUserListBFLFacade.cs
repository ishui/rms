namespace RmsOA.BFL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using Rms.ORMap;
    using RmsOA.DAL;
    using RmsOA.MODEL;
    using RmsPM.BLL;
    using RmsPM.DAL.EntityDAO;

    public class ConferenceUserListBFLFacade
    {
        private const string DateBegin = "DateBegin";
        private const string DateEnd = "DateEnd";

        public void AuditAndSendMeetMsg(string meetCode)
        {
            int num = int.Parse(meetCode);
            this.ChangeMeetAuditState(num);
        }

        public void ChangeMeetAuditState(int meetCode)
        {
            ConferenceManageBFL ebfl = new ConferenceManageBFL();
            ConferenceManageModel objModel = new ConferenceManageModel();
            objModel.Code = meetCode;
            objModel.State = MeetStateType.Authored.ToString();
            ebfl.Update(objModel);
        }

        private string ChangeTimeArea(string time)
        {
            string[] textArray = time.Split(new char[] { '+' });
            return string.Format("{0:yy-MM-dd HH:mm} -> {1:yy-MM-dd HH:mm}", textArray[0], textArray[1]);
        }

        public string ChangTimeTostring(DateTime begin, DateTime end)
        {
            return string.Format("{0:dd日 HH:mm} - {1:dd日 HH:mm}", begin, end);
        }

        public List<ConferenceManageModel> GetADayMeet(DateTime dt)
        {
            ConferenceManageBFL ebfl = new ConferenceManageBFL();
            Dictionary<string, DateTime> dayBeginAndEnd = this.GetDayBeginAndEnd(dt);
            ConferenceManageQueryModel queryModel = new ConferenceManageQueryModel();
            queryModel.WeekStartTimeEqual = dayBeginAndEnd["DateBegin"];
            queryModel.WeekEndTimeEqual = dayBeginAndEnd["DateEnd"];
            queryModel.StateEqual = MeetStateType.Authored.ToString();
            return ebfl.GetConferenceManageList(queryModel);
        }

        public List<string> GetAllMeetRoomCode()
        {
            List<string> list = new List<string>();
            List<MeetRoomModel> meetRoomList = new List<MeetRoomModel>();
            meetRoomList = new MeetRoomBFL().GetMeetRoomList(null);
            foreach (MeetRoomModel model in meetRoomList)
            {
                list.Add(model.Code.ToString());
            }
            return list;
        }

        public List<RoomModelQuery> GetAllRoomUseStatus(string begin, string end)
        {
            List<string> allMeetRoomCode = this.GetAllMeetRoomCode();
            List<RoomModelQuery> list2 = new List<RoomModelQuery>();
            List<RoomModelQuery> collection = new List<RoomModelQuery>();
            foreach (string text in allMeetRoomCode)
            {
                collection = this.GetMeetRoomUseStatus(text, begin, end, LoadType.Search);
                if ((collection == null) || (collection.Count == 0))
                {
                    RoomModelQuery item = new RoomModelQuery();
                    item.RoomCode = int.Parse(text);
                    item.RoomName = GetMeetRoomName(text);
                    list2.Add(item);
                }
                else
                {
                    list2.AddRange(collection);
                }
            }
            return list2;
        }

        private string GetAuserDayMeetContent(string userCode, DateTime dt)
        {
            StringBuilder builder = new StringBuilder();
            List<ConferenceManageModel> userAdayMeet = this.GetUserAdayMeet(dt, userCode);
            if (userAdayMeet.Equals(null) || (userAdayMeet.Count == 0))
            {
                builder.Append(string.Format("<tr><td>{0}</td><td></td><td></td><td></td><td></td><td></td></tr>", this.GetWeekandDate(dt)));
            }
            else
            {
                int count = userAdayMeet.Count;
                if (count.Equals(1))
                {
                    builder.Append(string.Format("<tr><td>{0}</td><td><a href=\"#\" onclick=\"OpenLargeWindow('../RmsOA/XZ_Conference.aspx?Code={6}&Type=Read','')\">{1}</a></td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", new object[] { this.GetWeekandDate(dt), userAdayMeet[0].Topic, this.GetUnitFullName(userAdayMeet[0].Dept), userAdayMeet[0].Type, GetMeetRoomName(userAdayMeet[0].Place), this.ChangTimeTostring(userAdayMeet[0].StartTime, userAdayMeet[0].EndTime), userAdayMeet[0].Code }));
                }
                else
                {
                    builder.Append(string.Format("<tr><td rowspan=\"{6}\">{0}</td><td><a href=\"#\" onclick=\"OpenLargeWindow('../RmsOA/XZ_Conference.aspx?Code={7}&Type=Read','')\">{1}</a></td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", new object[] { this.GetWeekandDate(dt), userAdayMeet[0].Topic, this.GetUnitFullName(userAdayMeet[0].Dept), userAdayMeet[0].Type, GetMeetRoomName(userAdayMeet[0].Place), this.ChangTimeTostring(userAdayMeet[0].StartTime, userAdayMeet[0].EndTime), count, userAdayMeet[0].Code }));
                    for (int i = 1; i < count; i++)
                    {
                        builder.Append(string.Format("<tr><td><a href=\"#\" onclick=\"OpenLargeWindow('../RmsOA/XZ_Conference.aspx?Code={5}&Type=Read','')\">{0}</a></td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", new object[] { userAdayMeet[i].Topic, this.GetUnitFullName(userAdayMeet[i].Dept), userAdayMeet[i].Type, GetMeetRoomName(userAdayMeet[0].Place), this.ChangTimeTostring(userAdayMeet[i].StartTime, userAdayMeet[i].EndTime), userAdayMeet[i].Code }));
                    }
                }
            }
            return builder.ToString();
        }

        public Dictionary<string, DateTime> GetDayBeginAndEnd(DateTime dt)
        {
            Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>();
            DateTime date = dt.Date;
            DateTime time2 = dt.Date.AddDays(1).AddSeconds(-1);
            dictionary.Add("DateBegin", date);
            dictionary.Add("DateEnd", time2);
            return dictionary;
        }

        public string GetDayMeetContent(DateTime dt)
        {
            StringBuilder builder = new StringBuilder();
            List<ConferenceManageModel> aDayMeet = this.GetADayMeet(dt);
            if (aDayMeet.Equals(null) || (aDayMeet.Count == 0))
            {
                builder.Append(string.Format("<tr><td>{0}</td><td></td><td></td><td></td><td></td><td></td></tr>", this.GetWeekandDate(dt)));
            }
            else
            {
                int count = aDayMeet.Count;
                if (count.Equals(1))
                {
                    builder.Append(string.Format("<tr><td>{0}</td><td><a href=\"#\" onclick=\"OpenLargeWindow('../RmsOA/XZ_Conference.aspx?Code={6}&Type=Read','')\">{1}</a></td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", new object[] { this.GetWeekandDate(dt), aDayMeet[0].Topic, this.GetUnitFullName(aDayMeet[0].Dept), aDayMeet[0].Type, GetMeetRoomName(aDayMeet[0].Place), this.ChangTimeTostring(aDayMeet[0].StartTime, aDayMeet[0].EndTime), aDayMeet[0].Code }));
                }
                else
                {
                    builder.Append(string.Format("<tr><td rowspan=\"{6}\">{0}</td><td><a href=\"#\" onclick=\"OpenLargeWindow('../RmsOA/XZ_Conference.aspx?Code={7}&Type=Read','')\">{1}</a></td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", new object[] { this.GetWeekandDate(dt), aDayMeet[0].Topic, this.GetUnitFullName(aDayMeet[0].Dept), aDayMeet[0].Type, GetMeetRoomName(aDayMeet[0].Place), this.ChangTimeTostring(aDayMeet[0].StartTime, aDayMeet[0].EndTime), count, aDayMeet[0].Code }));
                    for (int i = 1; i < count; i++)
                    {
                        builder.Append(string.Format("<tr><td><a href=\"#\" onclick=\"OpenLargeWindow('../RmsOA/XZ_Conference.aspx?Code={5}&Type=Read','')\">{0}</a></td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", new object[] { aDayMeet[i].Topic, this.GetUnitFullName(aDayMeet[i].Dept), aDayMeet[i].Type, GetMeetRoomName(aDayMeet[0].Place), this.ChangTimeTostring(aDayMeet[i].StartTime, aDayMeet[i].EndTime), aDayMeet[i].Code }));
                    }
                }
            }
            return builder.ToString();
        }

        public string GetMeetClashMessage(string meetCode, string roomCode, string begin, string end)
        {
            string text = string.Empty;
            int num = 0;
            StringBuilder builder = new StringBuilder();
            List<RoomModelQuery> list = this.GetMeetRoomUseStatus(roomCode, begin, end, LoadType.Insert);
            if ((list == null) || (list.Count == 0))
            {
                return text;
            }
            if (!string.IsNullOrEmpty(meetCode))
            {
                foreach (RoomModelQuery query in list)
                {
                    if (!query.MeetCode.ToString().Equals(meetCode))
                    {
                        num++;
                        builder.Append(string.Format(@"会议{0}: 主持人{1} 会议时间{2}\n", num, query.ChaterMember, query.TimeAge));
                    }
                }
                if (num > 0)
                {
                    builder.Insert(0, string.Format(@"会议室共有{0}个会议：\n", num));
                    text = builder.ToString();
                }
                return text;
            }
            foreach (RoomModelQuery query in list)
            {
                num++;
                builder.Append(string.Format(@"会议{0}: 主持人{1} 会议时间{2}\n", num, query.ChaterMember, query.TimeAge));
            }
            builder.Insert(0, string.Format(@"会议室共有{0}个会议：\n", num));
            return builder.ToString();
        }

        public static string GetMeetRoomName(string roomCode)
        {
            MeetRoomBFL mbfl = new MeetRoomBFL();
            return mbfl.GetMeetRoom(int.Parse(roomCode)).RoomName;
        }

        public List<RoomModelQuery> GetMeetRoomUseStatus(string roomCode, string begin, string end, LoadType lt)
        {
            List<RoomModelQuery> list = new List<RoomModelQuery>();
            List<RoomModelQuery> list2 = new List<RoomModelQuery>();
            list = new ConferenceManageDAL(new SqlConnection(FunctionRule.GetConnectionString())).GetRoomUseStatus(roomCode, begin, end);
            if ((list != null) && (list.Count != 0))
            {
                foreach (RoomModelQuery query in list)
                {
                    query.RoomCode = int.Parse(roomCode);
                    query.RoomName = GetMeetRoomName(roomCode);
                    query.TimeAge = this.ChangeTimeArea(query.TimeAge);
                    query.ChaterMember = this.GetUserName(query.ChaterMember);
                    if (lt.Equals(LoadType.Search))
                    {
                        query.Message = string.Format("<a href=\"#\" onclick=\"OpenLargeWindow('../RmsOA/XZ_Conference.aspx?Code={0}&Type=Read','')\">{1}</a>", query.MeetCode, query.ChaterMember);
                    }
                    else
                    {
                        query.Message = query.ChaterMember;
                    }
                    list2.Add(query);
                }
            }
            return list2;
        }

        public List<MeetRoomBOX> GetRoomList()
        {
            List<MeetRoomBOX> list = new List<MeetRoomBOX>();
            MeetRoomBOX item = new MeetRoomBOX();
            List<MeetRoomModel> meetRoomList = new MeetRoomBFL().GetMeetRoomList(new MeetRoomQueryModel());
            foreach (MeetRoomModel model in meetRoomList)
            {
                item = new MeetRoomBOX();
                item.RoomCode = model.Code;
                item.RoomName = model.RoomName;
                list.Add(item);
            }
            return list;
        }

        public string GetUnitFullName(string unitCode)
        {
            return SystemRule.GetUnitFullName(unitCode);
        }

        public List<ConferenceManageModel> GetUserAdayMeet(DateTime dt, string userCode)
        {
            List<ConferenceManageModel> list = new List<ConferenceManageModel>();
            ConferenceManageDAL edal = new ConferenceManageDAL(new SqlConnection(FunctionRule.GetConnectionString()));
            Dictionary<string, DateTime> dayBeginAndEnd = this.GetDayBeginAndEnd(dt);
            return edal.GetUserMeet(userCode, dayBeginAndEnd["DateBegin"], dayBeginAndEnd["DateEnd"]);
        }

        public string GetUserName(string userCode)
        {
            return SystemRule.GetUserName(userCode);
        }

        public List<ConferenceUserListModel> GetUsersAttendMeet(int meetCode)
        {
            ConferenceUserListQueryModel queryModel = new ConferenceUserListQueryModel();
            queryModel.ConferenceCodeEqual = meetCode;
            ConferenceUserListBFL tbfl = new ConferenceUserListBFL();
            return tbfl.GetConferenceUserListList(queryModel);
        }

        public string GetWeekandDate(DateTime dt)
        {
            return string.Format("{0:MM月dd日} {1}", dt, TimeExtend.GetChineseWeekName(dt));
        }

        public string GetWeekConference(DateTime dt)
        {
            DateTime weekBeginDate = TimeExtend.GetWeekBeginDate(dt);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 7; i++)
            {
                builder.Append(this.GetDayMeetContent(weekBeginDate.AddDays((double) i)));
            }
            return builder.ToString();
        }

        private void SaveRS(ArrayList arOperator, string strUser, string strStation, string strOption)
        {
            AccessRange range;
            if (strUser.Length > 0)
            {
                foreach (string text in strUser.Split(new char[] { ',' }))
                {
                    if (text != "")
                    {
                        range = new AccessRange();
                        range.AccessRangeType = 0;
                        range.RelationCode = text;
                        range.Operations = strOption;
                        arOperator.Add(range);
                    }
                }
            }
            if (strStation.Length > 0)
            {
                foreach (string text2 in strStation.Split(new char[] { ',' }))
                {
                    if (text2 != "")
                    {
                        range = new AccessRange();
                        range.AccessRangeType = 1;
                        range.RelationCode = text2;
                        range.Operations = strOption;
                        arOperator.Add(range);
                    }
                }
            }
        }

        public void SendMsgToUser(int meetCode)
        {
            ConferenceManageBFL ebfl = new ConferenceManageBFL();
            ConferenceManageModel conferenceManage = new ConferenceManageModel();
            conferenceManage = ebfl.GetConferenceManage(meetCode);
            string strUser = conferenceManage.ChaterMember;
            string text2 = string.Format("{0} 会议", conferenceManage.Topic);
            string text3 = string.Format("主办单位： <b>{0}</b>\r\n会议地点： <b>{1}</b>\r\n会议时间：<b>{2}</b>\r\n<a href='../RmsOA/XZ_Conference.aspx?Type=Read&Code={3}'>详细内容</a>\r\n<a href='../RmsOA/XZ_ConferenceWeek.aspx'>本周会议</a>", new object[] { conferenceManage.Dept, conferenceManage.Place, conferenceManage.StartTime, meetCode.ToString() });
            DateTime now = DateTime.Now;
            string code = SystemManageDAO.GetNewSysCode("Notice");
            EntityData entity = new EntityData("Standard_Notice");
            DataRow newRecord = entity.GetNewRecord();
            newRecord["NoticeCode"] = code;
            newRecord["Title"] = text2;
            newRecord["SubmitPerson"] = strUser;
            newRecord["SubmitDate"] = DateTime.Now;
            newRecord["UpdateDate"] = DateTime.Now;
            newRecord["UserCode"] = strUser;
            newRecord["Content"] = text3;
            newRecord["Type"] = "1";
            newRecord["IsAll"] = "0";
            newRecord["status"] = "1";
            entity.AddNewRecord(newRecord);
            string strTmp = this.UserListString(meetCode);
            ArrayList arOperator = new ArrayList();
            this.SaveRS(arOperator, StringRule.CutRepeat(strTmp), "", "080104");
            this.SaveRS(arOperator, strUser, "", "080102,080103,080104");
            if (arOperator.Count > 0)
            {
                ResourceRule.SetResourceAccessRange(code, "0801", "", arOperator, false);
            }
            RemindDAO.InsertNotice(entity);
            entity.Dispose();
        }

        public string ShowAUserWeekMeetMessage(string userCode, DateTime dt)
        {
            DateTime weekBeginDate = TimeExtend.GetWeekBeginDate(dt);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 7; i++)
            {
                builder.Append(this.GetAuserDayMeetContent(userCode, weekBeginDate.AddDays((double) i)));
            }
            return builder.ToString();
        }

        public string UserListString(int meetCode)
        {
            StringBuilder builder = new StringBuilder();
            ConferenceUserListQueryModel queryModel = new ConferenceUserListQueryModel();
            queryModel.ConferenceCodeEqual = meetCode;
            List<ConferenceUserListModel> conferenceUserListList = new ConferenceUserListBFL().GetConferenceUserListList(queryModel);
            foreach (ConferenceUserListModel model2 in conferenceUserListList)
            {
                if (!model2.UserCode.Equals(string.Empty))
                {
                    builder.Append(model2.UserCode + ",");
                }
            }
            int length = builder.Length;
            if (length > 0)
            {
                builder.Remove(length - 1, 1);
            }
            return builder.ToString();
        }

        public static string WeekAge(DateTime dt)
        {
            Dictionary<string, DateTime> dictionary = TimeExtend.WeekBeginAddEndDay(dt);
            return string.Format("{0:yyyy年MM月dd日} - {1:yyyy年MM月dd日}", dictionary["WeekBegin"], dictionary["WeekEnd"]);
        }
    }
}

