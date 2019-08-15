namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_WorkEvaluationBFL
    {
        public int Delete(GK_OA_WorkEvaluationModel ObjModel)
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
                        num = new GK_OA_WorkEvaluationBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_WorkEvaluationModel GetGK_OA_WorkEvaluation(int Code)
        {
            GK_OA_WorkEvaluationModel model = new GK_OA_WorkEvaluationModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_WorkEvaluationBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_WorkEvaluationModel> GetGK_OA_WorkEvaluationList(GK_OA_WorkEvaluationQueryModel QueryModel)
        {
            List<GK_OA_WorkEvaluationModel> models = new List<GK_OA_WorkEvaluationModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_WorkEvaluationQueryModel();
                    }
                    models = new GK_OA_WorkEvaluationBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_WorkEvaluationModel> GetGK_OA_WorkEvaluationList(string SortColumns, int StartRecord, int MaxRecords, string SystemCodeEqual, string FileCodeEqual, string PersonNameEqual, string UnitCodeEqual, DateTime InComDateStart, DateTime InComDateEnd, string StatusEqual)
        {
            List<GK_OA_WorkEvaluationModel> models = new List<GK_OA_WorkEvaluationModel>();
            GK_OA_WorkEvaluationQueryModel objQueryModel = new GK_OA_WorkEvaluationQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.SystemCodeEqual = SystemCodeEqual;
            objQueryModel.FileCodeEqual = FileCodeEqual;
            objQueryModel.PersonNameEqual = PersonNameEqual;
            objQueryModel.UnitCodeEqual = UnitCodeEqual;
            objQueryModel.StatusEqual = StatusEqual;
            objQueryModel.InComDateEqualStart = InComDateStart;
            objQueryModel.InComDateEqualEnd = InComDateEnd;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_WorkEvaluationBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_WorkEvaluationModel> GetGK_OA_WorkEvaluationListOne(int Code)
        {
            List<GK_OA_WorkEvaluationModel> list = new List<GK_OA_WorkEvaluationModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_WorkEvaluationBLL nbll = new GK_OA_WorkEvaluationBLL();
                    list.Add(nbll.GetModel(Code, connection));
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

        public int Insert(GK_OA_WorkEvaluationModel ObjModel)
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
                        num = new GK_OA_WorkEvaluationBLL().Insert(ObjModel, transaction);
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
                        num = new GK_OA_WorkEvaluationBLL().ModifyAlreadyAuditing(Code, transaction);
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
                        num = new GK_OA_WorkEvaluationBLL().ModifyBankOutAuditing(Code, transaction);
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
                        num = new GK_OA_WorkEvaluationBLL().ModifyNotAuditing(Code, transaction);
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
                        num = new GK_OA_WorkEvaluationBLL().ModifyNotPassAuditing(Code, transaction);
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
                        num = new GK_OA_WorkEvaluationBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(GK_OA_WorkEvaluationModel ObjModel)
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
                        num = new GK_OA_WorkEvaluationBLL().Update(ObjModel, transaction);
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

