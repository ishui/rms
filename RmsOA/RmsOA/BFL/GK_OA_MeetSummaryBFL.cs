namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_MeetSummaryBFL
    {
        public int Delete(GK_OA_MeetSummaryModel ObjModel)
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
                        num = new GK_OA_MeetSummaryBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_MeetSummaryModel GetGK_OA_MeetSummary(int Code)
        {
            GK_OA_MeetSummaryModel model = new GK_OA_MeetSummaryModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_MeetSummaryBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_MeetSummaryModel> GetGK_OA_MeetSummaryList(GK_OA_MeetSummaryQueryModel QueryModel)
        {
            List<GK_OA_MeetSummaryModel> models = new List<GK_OA_MeetSummaryModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_MeetSummaryQueryModel();
                    }
                    models = new GK_OA_MeetSummaryBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_MeetSummaryModel> GetGK_OA_MeetSummaryList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string SortCodeEqual, string CodeRuleEqual, DateTime MeetStartTimeEqual, string PlaceEqual, string TitleEqual, string AttendPersonsEqual, string RecoderEqual, string ContextEqual, string OtherContextEqual, string TypeEqual, string DeptEqual, string StatusEqual, string CharterMemberEqual, string OtherPersonEqual, DateTime MeetEndTimeEqual, string SmallTitleEqual, string SendStatusEqual, string PreLeaveEqual, DateTime SubmitTimeEqual, string SubmiterEqual, DateTime startTime, DateTime endTime)
        {
            List<GK_OA_MeetSummaryModel> models = new List<GK_OA_MeetSummaryModel>();
            GK_OA_MeetSummaryQueryModel objQueryModel = new GK_OA_MeetSummaryQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.SortCodeEqual = SortCodeEqual;
            objQueryModel.CodeRuleEqual = CodeRuleEqual;
            objQueryModel.MeetStartTimeEqual = MeetStartTimeEqual;
            objQueryModel.PlaceEqual = PlaceEqual;
            objQueryModel.TitleEqual = TitleEqual;
            objQueryModel.AttendPersonsEqual = AttendPersonsEqual;
            objQueryModel.RecoderEqual = RecoderEqual;
            objQueryModel.ContextEqual = ContextEqual;
            objQueryModel.OtherContextEqual = OtherContextEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.DeptEqual = DeptEqual;
            objQueryModel.StatusEqual = StatusEqual;
            objQueryModel.CharterMemberEqual = CharterMemberEqual;
            objQueryModel.OtherPersonEqual = OtherPersonEqual;
            objQueryModel.MeetEndTimeEqual = MeetEndTimeEqual;
            objQueryModel.SmallTitleEqual = SmallTitleEqual;
            objQueryModel.SendStatusEqual = SendStatusEqual;
            objQueryModel.PreLeaveEqual = PreLeaveEqual;
            objQueryModel.SubmitTimeEqual = SubmitTimeEqual;
            objQueryModel.SubmiterEqual = SubmiterEqual;
            objQueryModel.StartTimeEqual = startTime;
            objQueryModel.EndTimeEqual = endTime;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_MeetSummaryBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_MeetSummaryModel> GetGK_OA_MeetSummaryListOne(int Code)
        {
            List<GK_OA_MeetSummaryModel> list = new List<GK_OA_MeetSummaryModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_MeetSummaryBLL ybll = new GK_OA_MeetSummaryBLL();
                    list.Add(ybll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
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

        public int Insert(GK_OA_MeetSummaryModel ObjModel)
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
                        num = new GK_OA_MeetSummaryBLL().Insert(ObjModel, transaction);
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
                        num = new GK_OA_MeetSummaryBLL().ModifyAlreadyAuditing(Code, transaction);
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
                        num = new GK_OA_MeetSummaryBLL().ModifyBankOutAuditing(Code, transaction);
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
                        num = new GK_OA_MeetSummaryBLL().ModifyNotAuditing(Code, transaction);
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
                        num = new GK_OA_MeetSummaryBLL().ModifyNotPassAuditing(Code, transaction);
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
                        num = new GK_OA_MeetSummaryBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(GK_OA_MeetSummaryModel ObjModel)
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
                        num = new GK_OA_MeetSummaryBLL().Update(ObjModel, transaction);
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

