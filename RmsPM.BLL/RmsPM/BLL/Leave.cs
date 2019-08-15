namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class Leave
    {
        private StandardEntityDAO _dao;
        private string _LeaveCause = null;
        private string _LeaveCode = null;
        private string _LeaveEndTime = null;
        private string _LeaveFlag = null;
        private string _LeavePerson = null;
        private string _LeaveStartTime = null;
        private string _LeaveSumTime = null;
        private string _LeaveTime = null;
        private string _LeaveType = null;
        private string _LeaveUnit = null;
        private string _QueryDate = null;

        private void _GetLeaveByCode()
        {
            EntityData leaveByCode = LeaveDAO.GetLeaveByCode(this._LeaveCode);
            this._LeavePerson = leaveByCode.GetString("LeavePerson");
            this._LeaveUnit = leaveByCode.GetString("LeaveUnit");
            this._LeaveCause = leaveByCode.GetString("LeaveCause");
            this._LeaveTime = leaveByCode.GetDateTimeOnlyDate("LeaveTime").ToString();
            this._LeaveStartTime = leaveByCode.GetDateTime("LeaveStartTime").ToString();
            this._LeaveEndTime = leaveByCode.GetDateTime("LeaveEndTime").ToString();
            this._LeaveFlag = leaveByCode.GetString("LeaveFlag");
            this._LeaveType = leaveByCode.GetString("LeaveType");
            this._LeaveSumTime = leaveByCode.GetDecimalString("LeaveSumTime");
            leaveByCode.Dispose();
        }

        private EntityData _GetLeaves()
        {
            EntityData entitydata = new EntityData("Leave");
            LeaveStrategyBuilder builder = new LeaveStrategyBuilder();
            if (this._LeaveCode != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveCode, this._LeaveCode));
            }
            if (this._LeavePerson != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeavePerson, this._LeavePerson));
            }
            if (this._LeaveUnit != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveUnit, this._LeaveUnit));
            }
            if (this._LeaveCause != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveCause, this._LeaveCause));
            }
            if (this._LeaveTime != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveTime, this._LeaveTime));
            }
            if (this._LeaveStartTime != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveStartTime, this._LeaveStartTime));
            }
            if (this._LeaveEndTime != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveEndTime, this._LeaveEndTime));
            }
            if (this._LeaveFlag != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveFlag, this._LeaveFlag));
            }
            if (this._LeaveType != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveType, this._LeaveType));
            }
            if (this._LeaveSumTime != null)
            {
                builder.AddStrategy(new Strategy(LeaveStrategyName.LeaveSumTime, this._LeaveSumTime));
            }
            if (this._QueryDate != null)
            {
                ArrayList pas = new ArrayList();
                pas.Add("");
                pas.Add(this._QueryDate + " 23:59:00");
                ArrayList list2 = new ArrayList();
                list2.Add(this._QueryDate + " 00:00:00");
                list2.Add("");
                builder.AddStrategy(new Strategy(LeaveStrategyName.QueryDateStart, pas));
                builder.AddStrategy(new Strategy(LeaveStrategyName.QueryDateEnd, list2));
            }
            string queryString = builder.BuildMainQueryString() + " order by LeaveTime";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("Leave", queryString);
            }
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData leaveByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._LeaveCode == "")
            {
                flag = true;
                leaveByCode = LeaveDAO.GetLeaveByCode("");
                this._LeaveCode = SystemManageDAO.GetNewSysCode("Leave");
                newRecord = leaveByCode.GetNewRecord();
            }
            else
            {
                leaveByCode = LeaveDAO.GetLeaveByCode(this._LeaveCode);
                newRecord = leaveByCode.CurrentRow;
            }
            if (this._LeaveCode != null)
            {
                newRecord["LeaveCode"] = this._LeaveCode;
            }
            if (this._LeavePerson != null)
            {
                newRecord["LeavePerson"] = this._LeavePerson;
            }
            if (this._LeaveUnit != null)
            {
                newRecord["LeaveUnit"] = this._LeaveUnit;
            }
            if (this._LeaveCause != null)
            {
                newRecord["LeaveCause"] = this._LeaveCause;
            }
            if (this._LeaveTime != null)
            {
                newRecord["LeaveTime"] = this._LeaveTime;
            }
            if (this._LeaveStartTime != null)
            {
                newRecord["LeaveStartTime"] = this._LeaveStartTime;
            }
            if (this._LeaveEndTime != null)
            {
                newRecord["LeaveEndTime"] = this._LeaveEndTime;
            }
            if (this._LeaveFlag != null)
            {
                newRecord["LeaveFlag"] = this._LeaveFlag;
            }
            if (this._LeaveType != null)
            {
                newRecord["LeaveType"] = this._LeaveType;
            }
            if (this._LeaveSumTime != null)
            {
                newRecord["LeaveSumTime"] = this._LeaveSumTime;
            }
            if (flag)
            {
                leaveByCode.AddNewRecord(newRecord);
            }
            return leaveByCode;
        }

        private EntityData BuildPassData(string LeaveFlagValue)
        {
            EntityData leaveByCode = LeaveDAO.GetLeaveByCode(this._LeaveCode);
            leaveByCode.CurrentRow["LeaveFlag"] = LeaveFlagValue;
            return leaveByCode;
        }

        public static string GetLeavePassState(string LeavePassCode)
        {
            if (LeavePassCode == "1")
            {
                return "同意";
            }
            if (LeavePassCode == "0")
            {
                return "不同意";
            }
            return "未审";
        }

        public static DataTable GetLeaveReport(string Year, string Month)
        {
            DataTable table2;
            QueryAgent agent = new QueryAgent();
            try
            {
                string queryString = "sp_LeaveReportForUnit";
                string[] parameterNames = new string[] { "@Year", "@Month" };
                object[] values = new object[] { Year, Month };
                DataTable table = agent.ExecSPForDataSet(queryString, parameterNames, values).Tables[0];
                table2 = table;
            }
            finally
            {
                agent.Dispose();
            }
            return table2;
        }

        public static DataTable GetLeaveReportForUnit(string Year, string Month, string UnitCode)
        {
            DataTable table2;
            QueryAgent agent = new QueryAgent();
            try
            {
                string queryString = "sp_LeaveReportForPerson";
                string[] parameterNames = new string[] { "@Year", "@Month", "@UnitCode" };
                object[] values = new object[] { Year, Month, UnitCode };
                DataTable table = agent.ExecSPForDataSet(queryString, parameterNames, values).Tables[0];
                table2 = table;
            }
            finally
            {
                agent.Dispose();
            }
            return table2;
        }

        public DataTable GetLeaves()
        {
            return this._GetLeaves().CurrentTable;
        }

        public static string GetLeaveTypeName(string LeaveTypeCode)
        {
            switch (LeaveTypeCode)
            {
                case "0":
                    return "外出";

                case "1":
                    return "事假";

                case "2":
                    return "病假";

                case "3":
                    return "产假";

                case "4":
                    return "婚丧假";

                case "5":
                    return "工伤假";

                case "6":
                    return "其它";
            }
            return "";
        }

        public void LeaveAdd()
        {
            if (this._LeaveCode == null)
            {
                if (this._dao == null)
                {
                    LeaveDAO.SubmitAllLeave(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Leave";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void LeaveDelete()
        {
            if (this._dao == null)
            {
                LeaveDAO.DeleteLeave(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Leave";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void LeaveNoPass()
        {
            if (this._dao == null)
            {
                LeaveDAO.UpdateLeave(this.BuildPassData("0"));
            }
            else
            {
                this.dao.EntityName = "Leave";
                this.dao.UpdateEntity(this.BuildPassData("0"));
            }
        }

        public void LeavePass()
        {
            if (this._dao == null)
            {
                LeaveDAO.UpdateLeave(this.BuildPassData("1"));
            }
            else
            {
                this.dao.EntityName = "Leave";
                this.dao.UpdateEntity(this.BuildPassData("1"));
            }
        }

        public void LeaveSubmit()
        {
            if (this._dao == null)
            {
                LeaveDAO.SubmitAllLeave(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Leave";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void LeaveUpdate()
        {
            if (this._LeaveCode != null)
            {
                if (this._dao == null)
                {
                    LeaveDAO.SubmitAllLeave(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Leave";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public StandardEntityDAO dao
        {
            get
            {
                return this._dao;
            }
            set
            {
                this._dao = value;
            }
        }

        public string LeaveCause
        {
            get
            {
                if ((this._LeaveCause == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveCause;
            }
            set
            {
                this._LeaveCause = value;
            }
        }

        public string LeaveCode
        {
            get
            {
                return this._LeaveCode;
            }
            set
            {
                this._LeaveCode = value;
            }
        }

        public string LeaveEndTime
        {
            get
            {
                if ((this._LeaveEndTime == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveEndTime;
            }
            set
            {
                this._LeaveEndTime = value;
            }
        }

        public string LeaveFlag
        {
            get
            {
                if ((this._LeaveFlag == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveFlag;
            }
            set
            {
                this._LeaveFlag = value;
            }
        }

        public string LeavePerson
        {
            get
            {
                if ((this._LeavePerson == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeavePerson;
            }
            set
            {
                this._LeavePerson = value;
            }
        }

        public string LeaveStartTime
        {
            get
            {
                if ((this._LeaveStartTime == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveStartTime;
            }
            set
            {
                this._LeaveStartTime = value;
            }
        }

        public string LeaveSumTime
        {
            get
            {
                if ((this._LeaveSumTime == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveSumTime;
            }
            set
            {
                this._LeaveSumTime = value;
            }
        }

        public string LeaveTime
        {
            get
            {
                if ((this._LeaveTime == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveTime;
            }
            set
            {
                this._LeaveTime = value;
            }
        }

        public string LeaveType
        {
            get
            {
                if ((this._LeaveType == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveType;
            }
            set
            {
                this._LeaveType = value;
            }
        }

        public string LeaveUnit
        {
            get
            {
                if ((this._LeaveUnit == null) && (this._LeaveCode != null))
                {
                    this._GetLeaveByCode();
                }
                return this._LeaveUnit;
            }
            set
            {
                this._LeaveUnit = value;
            }
        }

        public string QueryDate
        {
            get
            {
                return this._QueryDate;
            }
            set
            {
                this._QueryDate = value;
            }
        }
    }
}

