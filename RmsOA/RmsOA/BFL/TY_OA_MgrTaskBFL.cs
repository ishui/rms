namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class TY_OA_MgrTaskBFL
    {
        public int Delete(TY_OA_MgrTaskModel ObjModel)
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
                        num = new TY_OA_MgrTaskBLL().Delete(ObjModel.Code, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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

        public TY_OA_MgrTaskModel GetTY_OA_MgrTask(int Code)
        {
            TY_OA_MgrTaskModel model = new TY_OA_MgrTaskModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new TY_OA_MgrTaskBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TY_OA_MgrTaskModel> GetTY_OA_MgrTaskList(TY_OA_MgrTaskQueryModel QueryModel)
        {
            List<TY_OA_MgrTaskModel> models = new List<TY_OA_MgrTaskModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new TY_OA_MgrTaskQueryModel();
                    }
                    models = new TY_OA_MgrTaskBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TY_OA_MgrTaskModel> GetTY_OA_MgrTaskList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string MgrTaskIDEqual, string StateEqual, string TaskNameEqual, string TaskDetailEqual, string IsFinishEqual, string TaskTailEqual, DateTime CreateDateEqual, string CreateDateRange1, string CreateDateRange2, string CreateManEqual, string ReferLinkEqual)
        {
            List<TY_OA_MgrTaskModel> models = new List<TY_OA_MgrTaskModel>();
            TY_OA_MgrTaskQueryModel objQueryModel = new TY_OA_MgrTaskQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.MgrTaskIDEqual = MgrTaskIDEqual;
            objQueryModel.StateEqual = StateEqual;
            objQueryModel.TaskNameEqual = TaskNameEqual;
            objQueryModel.TaskDetailEqual = TaskDetailEqual;
            objQueryModel.IsFinishEqual = IsFinishEqual;
            objQueryModel.TaskTailEqual = TaskTailEqual;
            objQueryModel.CreateDateEqual = CreateDateEqual;
            objQueryModel.CreateManEqual = CreateManEqual;
            objQueryModel.ReferLinkEqual = ReferLinkEqual;
            objQueryModel.CreateDateRange1 = CreateDateRange1;
            objQueryModel.CreateDateRange2 = CreateDateRange2;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new TY_OA_MgrTaskBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TY_OA_MgrTaskModel> GetTY_OA_MgrTaskListOne(int Code)
        {
            List<TY_OA_MgrTaskModel> list = new List<TY_OA_MgrTaskModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TY_OA_MgrTaskBLL kbll = new TY_OA_MgrTaskBLL();
                    list.Add(kbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(TY_OA_MgrTaskModel ObjModel)
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
                        num = new TY_OA_MgrTaskBLL().Insert(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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
                        num = new TY_OA_MgrTaskBLL().ModifyAlreadyAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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
                        num = new TY_OA_MgrTaskBLL().ModifyNotAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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
                        num = new TY_OA_MgrTaskBLL().ModifyNotPassAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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
                        num = new TY_OA_MgrTaskBLL().ModifyPassAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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

        public int Update(TY_OA_MgrTaskModel ObjModel)
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
                        num = new TY_OA_MgrTaskBLL().Update(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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

