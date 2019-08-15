namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using Rms.ORMap;
    using RmsOA.BLL;
    using RmsOA.DAL;
    using RmsOA.MODEL;
    using RmsPM.BLL;

    public class RS_ScoreManageBFL
    {
        public DateTime ConvertTimeToMonth(string time)
        {
            DateTime time2 = new DateTime();
            DateTime result = new DateTime();
            if (DateTime.TryParse(time.ToString(), out result))
            {
                int year = result.Year;
                return new DateTime(year, result.Month, 1);
            }
            return new DateTime(0x7d0, 1, 1);
        }

        public int Delete(RS_ScoreManageModel ObjModel)
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
                        num = new RS_ScoreManageBLL().Delete(ObjModel.Code, transaction);
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

        public List<UnitModel> GetAllDepts()
        {
            List<UnitModel> list = new List<UnitModel>();
            SqlConnection sqlConn = new SqlConnection(FunctionRule.GetConnectionString());
            return RS_ScoreManageDAL.GetAllDepts(sqlConn);
        }

        public AuditTimeType GetAuditType()
        {
            AuditTimeType lastMonth = AuditTimeType.LastMonth;
            string text = ConfigurationManager.AppSettings["AduitTimeType"];
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Equals(AuditTimeType.LastMonth.ToString()))
                {
                    lastMonth = AuditTimeType.LastMonth;
                }
                if (text.Equals(AuditTimeType.ThisMonth.ToString()))
                {
                    lastMonth = AuditTimeType.ThisMonth;
                }
            }
            return lastMonth;
        }

        public DataTable GetDeptListByUserCode(string userCode)
        {
            return SystemRule.GetNewUnitListByUserCode(userCode);
        }

        public string GetDeptNameByDeptID(string deptID)
        {
            return SystemRule.GetUnitName(deptID);
        }

        public List<EmployScoreMode> GetDeptScores(string userCode, DateTime dt, string type)
        {
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            List<RS_EmployScoreModel> list2 = new List<RS_EmployScoreModel>();
            RS_ScoreManageQueryModel queryModel = new RS_ScoreManageQueryModel();
            queryModel.MarkDateEqual = this.GetMonthFirstDate(dt);
            queryModel.MarkerEqual = userCode;
            queryModel.TypeEqual = int.Parse(ScoreType.Dept.ToString("d"));
            list = this.GetRS_ScoreManageList(queryModel);
            if (list.Count == 0)
            {
                if (!type.Equals("Load"))
                {
                    return null;
                }
                for (int i = 2; (list.Count == 0) && (i > 0); i--)
                {
                    queryModel = new RS_ScoreManageQueryModel();
                    dt = dt.AddMonths(-1);
                    queryModel.MarkerEqual = userCode;
                    queryModel.MarkDateEqual = dt;
                    queryModel.TypeEqual = int.Parse(ScoreType.Dept.ToString("d"));
                    list = this.GetRS_ScoreManageList(queryModel);
                }
                if (list.Count == 0)
                {
                    return null;
                }
            }
            List<RS_EmployScoreModel> userScoreListByManageCode = new List<RS_EmployScoreModel>();
            userScoreListByManageCode = this.GetUserScoreListByManageCode(list[0].Code);
            string userNameByUserID = this.GetUserNameByUserID(list[0].Marker);
            EmployScoreMode item = new EmployScoreMode();
            List<EmployScoreMode> list4 = new List<EmployScoreMode>();
            foreach (RS_EmployScoreModel model2 in userScoreListByManageCode)
            {
                item = new EmployScoreMode();
                item.Score = model2.Score.ToString();
                item.DeptCode = model2.UserCode;
                item.DeptName = this.GetDeptNameByDeptID(model2.UserCode);
                item.Marker = userNameByUserID;
                item.ScoreCode = model2.Code;
                item.MarkTime = list[0].MarkDate.ToString();
                list4.Add(item);
            }
            return list4;
        }

        public List<EmployScoreMode> GetEmployScore(string deptCode)
        {
            int num;
            RS_ScoreManageQueryModel model = new RS_ScoreManageQueryModel();
            RS_ScoreManageModel model2 = new RS_ScoreManageModel();
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            DateTime dt = this.GetMonthFirstDate(RS_ScoreExtend.CheckMonth);
            list = this.GetManageCode(deptCode, dt, ScoreType.Employ);
            if (list.Count == 0)
            {
                for (num = 12; (list.Count == 0) && (num > 0); num--)
                {
                    dt = dt.AddMonths(-1);
                    list = this.GetManageCode(deptCode, dt, ScoreType.Employ);
                }
                if (list.Count == 0)
                {
                    return null;
                }
            }
            else
            {
                int code = list[0].Code;
                if (int.Parse(list[0].Status) < int.Parse(WorkFlowStatus.Audited.ToString("d")))
                {
                    for (num = 12; (list.Count == 0) && (num > 0); num--)
                    {
                        dt = dt.AddMinutes(-1);
                        list = this.GetManageCode(deptCode, dt, ScoreType.Employ);
                    }
                    if (list.Count == 0)
                    {
                        return null;
                    }
                }
            }
            List<RS_EmployScoreModel> userScoreListByManageCode = new List<RS_EmployScoreModel>();
            userScoreListByManageCode = this.GetUserScoreListByManageCode(list[0].Code);
            string userNameByUserID = this.GetUserNameByUserID(list[0].Marker);
            string deptNameByDeptID = this.GetDeptNameByDeptID(list[0].DeptCode);
            EmployScoreMode item = new EmployScoreMode();
            List<EmployScoreMode> list3 = new List<EmployScoreMode>();
            RS_ScoreExtend extend = new RS_ScoreExtend();
            foreach (RS_EmployScoreModel model3 in userScoreListByManageCode)
            {
                item = new EmployScoreMode();
                item.UserCode = model3.UserCode;
                item.UserName = extend.GetUserNameByCode(model3.UserCode);
                item.Score = model3.Score.ToString();
                item.DeptCode = list[0].DeptCode;
                item.DeptName = deptNameByDeptID;
                item.Marker = userNameByUserID;
                item.ScoreCode = model3.Code;
                item.MarkTime = list[0].MarkDate.ToString();
                item.Status = list[0].Status;
                list3.Add(item);
            }
            return list3;
        }

        public List<EmployScoreMode> GetEmployScore(string deptCode, DateTime dtNow, SearchType st)
        {
            int num;
            RS_ScoreManageQueryModel model = new RS_ScoreManageQueryModel();
            RS_ScoreManageModel model2 = new RS_ScoreManageModel();
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            DateTime dt = this.GetMonthFirstDate(dtNow);
            list = this.GetManageCode(deptCode, dt, ScoreType.Employ);
            if (list.Count == 0)
            {
                if (st == SearchType.Search)
                {
                    return null;
                }
                for (num = 12; (list.Count == 0) && (num > 0); num--)
                {
                    dt = dt.AddMonths(-1);
                    list = this.GetManageCode(deptCode, dt, ScoreType.Employ);
                }
                if (list.Count == 0)
                {
                    return null;
                }
            }
            else
            {
                int code = list[0].Code;
                if (int.Parse(list[0].Status) < int.Parse(WorkFlowStatus.Audited.ToString("d")))
                {
                    for (num = 12; (list.Count == 0) && (num > 0); num--)
                    {
                        if (st == SearchType.Search)
                        {
                            return null;
                        }
                        dt = dt.AddMinutes(-1);
                        list = this.GetManageCode(deptCode, dt, ScoreType.Employ);
                    }
                    if (list.Count == 0)
                    {
                        return null;
                    }
                }
            }
            List<RS_EmployScoreModel> userScoreListByManageCode = new List<RS_EmployScoreModel>();
            userScoreListByManageCode = this.GetUserScoreListByManageCode(list[0].Code);
            string userNameByUserID = this.GetUserNameByUserID(list[0].Marker);
            string deptNameByDeptID = this.GetDeptNameByDeptID(list[0].DeptCode);
            EmployScoreMode item = new EmployScoreMode();
            List<EmployScoreMode> list3 = new List<EmployScoreMode>();
            RS_ScoreExtend extend = new RS_ScoreExtend();
            foreach (RS_EmployScoreModel model3 in userScoreListByManageCode)
            {
                item = new EmployScoreMode();
                item.UserCode = model3.UserCode;
                item.UserName = extend.GetUserNameByCode(model3.UserCode);
                item.Score = model3.Score.ToString();
                item.DeptCode = list[0].DeptCode;
                item.DeptName = deptNameByDeptID;
                item.Marker = userNameByUserID;
                item.ScoreCode = model3.Code;
                item.MarkTime = list[0].MarkDate.ToString();
                item.Status = list[0].Status;
                list3.Add(item);
            }
            return list3;
        }

        public List<UserModel> GetLeadedUsersByUserID(string userID)
        {
            List<UserModel> list = new List<UserModel>();
            SqlConnection sqlConn = new SqlConnection(FunctionRule.GetConnectionString());
            return RS_ScoreManageDAL.GetLeadedUser(userID, sqlConn);
        }

        public List<RS_ScoreManageModel> GetManageCode(string deptCode, DateTime dt, ScoreType sorceType)
        {
            RS_ScoreManageQueryModel queryModel = new RS_ScoreManageQueryModel();
            queryModel.DeptCodeEqual = deptCode;
            queryModel.MarkDateEqual = dt;
            queryModel.TypeEqual = int.Parse(sorceType.ToString("d"));
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            return this.GetRS_ScoreManageList(queryModel);
        }

        public List<EmployViewModel> GetManageScoresByFKCode(int FKCode)
        {
            RS_EmployScoreBFL ebfl = new RS_EmployScoreBFL();
            RS_EmployScoreModel model2 = new RS_EmployScoreModel();
            RS_EmployScoreQueryModel queryModel = new RS_EmployScoreQueryModel();
            List<EmployViewModel> list = new List<EmployViewModel>();
            List<RS_EmployScoreModel> list2 = new List<RS_EmployScoreModel>();
            queryModel.ManageCodeEqual = FKCode;
            list2 = ebfl.GetRS_EmployScoreList(queryModel);
            if (list2.Count > 0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    EmployViewModel item = new EmployViewModel();
                    item.Index = i + 1;
                    item.Code = list2[i].Code;
                    item.UserName = list2[i].UserCode;
                    item.Score = list2[i].Score;
                    list.Add(item);
                }
                return list;
            }
            return null;
        }

        public List<UnitModel> GetMangeDeptListByUserCode(string userCode)
        {
            List<UnitModel> list = new List<UnitModel>();
            SqlConnection sqlConn = new SqlConnection(FunctionRule.GetConnectionString());
            return RS_ScoreManageDAL.GetManageListByUserCode(userCode, sqlConn);
        }

        public static string GetManpowerNeedStatusName(string str)
        {
            switch (str)
            {
                case "0":
                    return "申请";

                case "1":
                    return "审核中";

                case "2":
                    return "通过";

                case "3":
                    return "未通过";

                case "4":
                    return "作废";
            }
            return "";
        }

        public DateTime GetMonthFirstDate(DateTime dtNow)
        {
            int month = dtNow.Month;
            return new DateTime(dtNow.Year, month, 1);
        }

        public RS_ScoreManageModel GetRS_ScoreManage(int Code)
        {
            RS_ScoreManageModel model = new RS_ScoreManageModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new RS_ScoreManageBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<RS_ScoreManageModel> GetRS_ScoreManageList(RS_ScoreManageQueryModel QueryModel)
        {
            List<RS_ScoreManageModel> models = new List<RS_ScoreManageModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new RS_ScoreManageQueryModel();
                    }
                    models = new RS_ScoreManageBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<RS_ScoreManageModel> GetRS_ScoreManageList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string DeptCodeEqual, string MarkerEqual, DateTime MarkDateEqual, int TypeEqual, string StatusEqual)
        {
            List<RS_ScoreManageModel> models = new List<RS_ScoreManageModel>();
            RS_ScoreManageQueryModel objQueryModel = new RS_ScoreManageQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.DeptCodeEqual = DeptCodeEqual;
            objQueryModel.MarkerEqual = MarkerEqual;
            objQueryModel.MarkDateEqual = MarkDateEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.StatusEqual = StatusEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new RS_ScoreManageBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<RS_ScoreManageModel> GetRS_ScoreManageListOne(int Code)
        {
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    RS_ScoreManageBLL ebll = new RS_ScoreManageBLL();
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

        public List<EmployScoreMode> GetScores(string userCode, ScoreType st, DateTime dt)
        {
            DateTime monthFirstDate;
            int num;
            if (dt > DateTime.Now)
            {
                monthFirstDate = this.GetMonthFirstDate(DateTime.Now);
            }
            else
            {
                monthFirstDate = this.GetMonthFirstDate(dt);
            }
            RS_ScoreManageQueryModel queryModel = new RS_ScoreManageQueryModel();
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            queryModel.MarkerEqual = userCode;
            queryModel.MarkDateEqual = monthFirstDate;
            queryModel.TypeEqual = int.Parse(st.ToString("d"));
            list = this.GetRS_ScoreManageList(queryModel);
            if ((list == null) || (list.Count == 0))
            {
                for (num = 12; (list.Count == 0) && (num > 0); num--)
                {
                    queryModel = new RS_ScoreManageQueryModel();
                    monthFirstDate = monthFirstDate.AddMonths(-1);
                    queryModel.MarkerEqual = userCode;
                    queryModel.MarkDateEqual = monthFirstDate;
                    queryModel.TypeEqual = int.Parse(st.ToString("d"));
                    list = this.GetRS_ScoreManageList(queryModel);
                }
                if ((list == null) || (list.Count == 0))
                {
                    return null;
                }
            }
            else
            {
                int code = list[0].Code;
                if (int.Parse(list[0].Status) < int.Parse(WorkFlowStatus.Audited.ToString("d")))
                {
                    for (num = 12; (list.Count == 0) && (num > 0); num--)
                    {
                        queryModel = new RS_ScoreManageQueryModel();
                        monthFirstDate = monthFirstDate.AddMonths(-1);
                        queryModel.MarkerEqual = userCode;
                        queryModel.MarkDateEqual = monthFirstDate;
                        queryModel.TypeEqual = int.Parse(st.ToString("d"));
                        list = this.GetRS_ScoreManageList(queryModel);
                    }
                    if ((list.Count == 0) || (list == null))
                    {
                        return null;
                    }
                }
            }
            List<RS_EmployScoreModel> userScoreListByManageCode = new List<RS_EmployScoreModel>();
            userScoreListByManageCode = this.GetUserScoreListByManageCode(list[0].Code);
            string userNameByUserID = this.GetUserNameByUserID(list[0].Marker);
            EmployScoreMode item = new EmployScoreMode();
            List<EmployScoreMode> list3 = new List<EmployScoreMode>();
            foreach (RS_EmployScoreModel model2 in userScoreListByManageCode)
            {
                item = new EmployScoreMode();
                item.UserCode = model2.UserCode;
                item.Score = model2.Score.ToString();
                item.Marker = userNameByUserID;
                item.ScoreCode = model2.Code;
                item.MarkTime = list[0].MarkDate.ToString();
                item.Status = list[0].Status;
                list3.Add(item);
            }
            return list3;
        }

        public List<UnitScoreModel> GetUnitScoreModel(DateTime dt)
        {
            dt = this.GetMonthFirstDate(dt);
            List<UnitScoreModel> deptScoreTotal = new List<UnitScoreModel>();
            SqlConnection sqlConn = new SqlConnection(FunctionRule.GetConnectionString());
            deptScoreTotal = RS_ScoreManageDAL.GetDeptScoreTotal(dt, sqlConn);
            List<UnitScoreModel> list2 = new List<UnitScoreModel>();
            foreach (UnitScoreModel model2 in deptScoreTotal)
            {
                SqlConnection connection2 = new SqlConnection(FunctionRule.GetConnectionString());
                UnitScoreModel item = new UnitScoreModel();
                item.DeptCode = model2.DeptCode;
                item.DeptName = this.GetDeptNameByDeptID(model2.DeptCode);
                item.Score = model2.Score;
                string text = RS_ScoreManageDAL.GetDeptEmployMarker(dt, model2.DeptCode, connection2);
                if (!string.IsNullOrEmpty(text))
                {
                    item.Marker = this.GetUserNameByUserID(text);
                }
                list2.Add(item);
            }
            return list2;
        }

        public List<EmployScoreMode> GetUserByUnitCode(string unitCode)
        {
            List<EmployScoreMode> list = new List<EmployScoreMode>();
            EmployScoreMode item = new EmployScoreMode();
            EntityData usersByUnit = SystemRule.GetUsersByUnit(unitCode);
            foreach (DataRow row in usersByUnit.Tables[0].Rows)
            {
                item = new EmployScoreMode();
                item.UserName = row["UserName"].ToString();
                item.UserCode = row["UserCode"].ToString();
                list.Add(item);
            }
            return list;
        }

        public string GetUserNameByUserID(string userID)
        {
            return SystemRule.GetUserName(userID);
        }

        public List<RS_EmployScoreModel> GetUserScoreListByManageCode(int mainCode)
        {
            RS_EmployScoreQueryModel queryModel = new RS_EmployScoreQueryModel();
            List<RS_EmployScoreModel> list = new List<RS_EmployScoreModel>();
            queryModel.ManageCodeEqual = mainCode;
            RS_EmployScoreBFL ebfl = new RS_EmployScoreBFL();
            return ebfl.GetRS_EmployScoreList(queryModel);
        }

        public List<EmployViewModel> GetUserScoresByFKCode(int FKCode)
        {
            RS_ScoreExtend extend = new RS_ScoreExtend();
            RS_EmployScoreBFL ebfl = new RS_EmployScoreBFL();
            RS_EmployScoreModel model2 = new RS_EmployScoreModel();
            RS_EmployScoreQueryModel queryModel = new RS_EmployScoreQueryModel();
            List<EmployViewModel> list = new List<EmployViewModel>();
            List<RS_EmployScoreModel> list2 = new List<RS_EmployScoreModel>();
            queryModel.ManageCodeEqual = FKCode;
            list2 = ebfl.GetRS_EmployScoreList(queryModel);
            if (list2.Count > 0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    EmployViewModel item = new EmployViewModel();
                    item.Index = i + 1;
                    item.Code = list2[i].Code;
                    item.UserCode = list2[i].UserCode;
                    item.UserName = extend.GetUserNameByCode(list2[i].UserCode);
                    item.Score = list2[i].Score;
                    list.Add(item);
                }
                return list;
            }
            return null;
        }

        public int HasManagerScoredORAudit(string userCode, out bool hasScore, out bool hasAudit)
        {
            int code = -1;
            DateTime dtNow = RS_ScoreExtend.CheckMonth;
            RS_ScoreManageQueryModel queryModel = new RS_ScoreManageQueryModel();
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            queryModel.MarkDateEqual = this.GetMonthFirstDate(dtNow);
            queryModel.MarkerEqual = userCode;
            queryModel.TypeEqual = int.Parse(ScoreType.Manager.ToString("d"));
            list = this.GetRS_ScoreManageList(queryModel);
            if ((list == null) || (list.Count == 0))
            {
                hasAudit = false;
                hasScore = false;
                return code;
            }
            code = list[0].Code;
            if (int.Parse(list[0].Status) < int.Parse(WorkFlowStatus.Audited.ToString("d")))
            {
                hasScore = true;
                hasAudit = false;
                return code;
            }
            hasScore = true;
            hasAudit = true;
            return code;
        }

        public int HasScoreORAudit(string deptID, out bool hasAudit)
        {
            int code = -1;
            DateTime dtNow = RS_ScoreExtend.CheckMonth;
            RS_ScoreManageQueryModel queryModel = new RS_ScoreManageQueryModel();
            List<RS_ScoreManageModel> list = new List<RS_ScoreManageModel>();
            queryModel.DeptCodeEqual = deptID;
            queryModel.MarkDateEqual = this.GetMonthFirstDate(dtNow);
            queryModel.TypeEqual = int.Parse(ScoreType.Employ.ToString("d"));
            list = this.GetRS_ScoreManageList(queryModel);
            if (list.Count > 0)
            {
                code = list[0].Code;
                if (int.Parse(list[0].Status) > int.Parse(WorkFlowStatus.Audited.ToString("d")))
                {
                    hasAudit = true;
                    return code;
                }
                hasAudit = false;
                return code;
            }
            hasAudit = false;
            return code;
        }

        public int Insert(RS_ScoreManageModel ObjModel)
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
                        num = new RS_ScoreManageBLL().Insert(ObjModel, transaction);
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

        public int ModifyAlreadyAuditing(int Code)
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
                        num = new RS_ScoreManageBLL().ModifyAlreadyAuditing(Code, transaction);
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

        public int ModifyBankOutAuditing(int Code)
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
                        num = new RS_ScoreManageBLL().ModifyBankOutAuditing(Code, transaction);
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

        public int ModifyNotAuditing(int Code)
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
                        num = new RS_ScoreManageBLL().ModifyNotAuditing(Code, transaction);
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

        public int ModifyNotPassAuditing(int Code)
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
                        num = new RS_ScoreManageBLL().ModifyNotPassAuditing(Code, transaction);
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

        public int ModifyPassAuditing(int Code)
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
                        num = new RS_ScoreManageBLL().ModifyPassAuditing(Code, transaction);
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

        public DateTime ScoreTime()
        {
            if (this.GetAuditType().Equals(AuditTimeType.LastMonth))
            {
                DateTime dtNow = DateTime.Now.AddMonths(-1);
                return this.GetMonthFirstDate(dtNow);
            }
            return this.GetMonthFirstDate(DateTime.Now);
        }

        public int Update(RS_ScoreManageModel ObjModel)
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
                        num = new RS_ScoreManageBLL().Update(ObjModel, transaction);
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

        public int UpDateScores(EmployViewModel evModel)
        {
            RS_EmployScoreModel objModel = new RS_EmployScoreModel();
            objModel.Code = evModel.Code;
            objModel.Score = evModel.Score;
            RS_EmployScoreBFL ebfl = new RS_EmployScoreBFL();
            return ebfl.Update(objModel);
        }
    }
}

