namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_WorkLogBFL
    {
        public List<GK_OA_WorkLogModel> ChangeWorkLogModel(GK_OA_WorkLogQueryModel queryModel)
        {
            List<GK_OA_WorkLogModel> list = this.GetGK_OA_WorkLogList(queryModel);
            foreach (GK_OA_WorkLogModel model in list)
            {
                string context = model.Context;
                if (context.Length >= 100)
                {
                    model.Context = context.Substring(0, 50) + "  ... ...";
                }
            }
            return list;
        }

        public int Delete(GK_OA_WorkLogModel ObjModel)
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
                        num = new GK_OA_WorkLogBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_WorkLogModel GetGK_OA_WorkLog(int Code)
        {
            GK_OA_WorkLogModel model = new GK_OA_WorkLogModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new GK_OA_WorkLogBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_WorkLogModel> GetGK_OA_WorkLogList(GK_OA_WorkLogQueryModel QueryModel)
        {
            List<GK_OA_WorkLogModel> models = new List<GK_OA_WorkLogModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_WorkLogQueryModel();
                    }
                    models = new GK_OA_WorkLogBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_WorkLogModel> GetGK_OA_WorkLogList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, DateTime DayWritedEqual, string UserIdEqual, string WeatherEqual, string MoodEqual, string ContextEqual, string WaiterEqual)
        {
            List<GK_OA_WorkLogModel> models = new List<GK_OA_WorkLogModel>();
            GK_OA_WorkLogQueryModel objQueryModel = new GK_OA_WorkLogQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.DayWritedEqual = DayWritedEqual;
            objQueryModel.UserIdEqual = UserIdEqual;
            objQueryModel.WeatherEqual = WeatherEqual;
            objQueryModel.MoodEqual = MoodEqual;
            objQueryModel.ContextEqual = ContextEqual;
            objQueryModel.WaiterEqual = WaiterEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new GK_OA_WorkLogBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_WorkLogModel> GetGK_OA_WorkLogListOne(int Code)
        {
            List<GK_OA_WorkLogModel> list = new List<GK_OA_WorkLogModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_WorkLogBLL gbll = new GK_OA_WorkLogBLL();
                    list.Add(gbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public string[] GetMoodType()
        {
            return new string[] { "", "特别愉快", "愉快", "一般", "不好", "很差" };
        }

        public DateTime GetUsefulTime(string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                if (result.Year < 0x708)
                {
                    return DateTime.Now;
                }
                return result;
            }
            return DateTime.Now;
        }

        public string[] GetWeatherType()
        {
            return new string[] { "", "晴朗", "少云", "多云", "阴", "阵雨", "小雨", "中雨", "大雨", "暴雨", "雪", "雾" };
        }

        public int Insert(GK_OA_WorkLogModel ObjModel)
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
                        num = new GK_OA_WorkLogBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_WorkLogModel ObjModel)
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
                        num = new GK_OA_WorkLogBLL().Update(ObjModel, transaction);
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

