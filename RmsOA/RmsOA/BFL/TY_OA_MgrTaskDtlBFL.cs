namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class TY_OA_MgrTaskDtlBFL
    {
        public int Delete(TY_OA_MgrTaskDtlModel ObjModel)
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
                        num = new TY_OA_MgrTaskDtlBLL().Delete(ObjModel.MgrDtlCode, transaction);
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

        public TY_OA_MgrTaskDtlModel GetTY_OA_MgrTaskDtl(int Code)
        {
            TY_OA_MgrTaskDtlModel model = new TY_OA_MgrTaskDtlModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new TY_OA_MgrTaskDtlBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TY_OA_MgrTaskDtlModel> GetTY_OA_MgrTaskDtlList(int MgrCodeID)
        {
            List<TY_OA_MgrTaskDtlModel> list;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                try
                {
                    list = new TY_OA_MgrTaskDtlDAL(connection).GetTY_OA_MgrTaskDtlListByMgrCodeID(MgrCodeID);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return list;
        }

        public List<TY_OA_MgrTaskDtlModel> GetTY_OA_MgrTaskDtlList(TY_OA_MgrTaskDtlQueryModel QueryModel)
        {
            List<TY_OA_MgrTaskDtlModel> models = new List<TY_OA_MgrTaskDtlModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new TY_OA_MgrTaskDtlQueryModel();
                    }
                    models = new TY_OA_MgrTaskDtlBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TY_OA_MgrTaskDtlModel> GetTY_OA_MgrTaskDtlList(string SortColumns, int StartRecord, int MaxRecords, int MgrDtlCodeEqual, string MgrDtlInfoEqual, DateTime DeadLineEqual, string ResponsePersonEqual, int MgrCodeIDEqual, string AssistpersonsEqual, string TrancRevertEqual, string ManagerRevertEqual, string IsfinishEqual, string StateEqual, string BakEqual)
        {
            List<TY_OA_MgrTaskDtlModel> models = new List<TY_OA_MgrTaskDtlModel>();
            TY_OA_MgrTaskDtlQueryModel objQueryModel = new TY_OA_MgrTaskDtlQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.MgrDtlCodeEqual = MgrDtlCodeEqual;
            objQueryModel.MgrDtlInfoEqual = MgrDtlInfoEqual;
            objQueryModel.DeadLineEqual = DeadLineEqual;
            objQueryModel.ResponsePersonEqual = ResponsePersonEqual;
            objQueryModel.MgrCodeIDEqual = MgrCodeIDEqual;
            objQueryModel.AssistpersonsEqual = AssistpersonsEqual;
            objQueryModel.TrancRevertEqual = TrancRevertEqual;
            objQueryModel.ManagerRevertEqual = ManagerRevertEqual;
            objQueryModel.IsfinishEqual = IsfinishEqual;
            objQueryModel.StateEqual = StateEqual;
            objQueryModel.BakEqual = BakEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new TY_OA_MgrTaskDtlBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TY_OA_MgrTaskDtlModel> GetTY_OA_MgrTaskDtlListOne(int Code)
        {
            List<TY_OA_MgrTaskDtlModel> list = new List<TY_OA_MgrTaskDtlModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TY_OA_MgrTaskDtlBLL lbll = new TY_OA_MgrTaskDtlBLL();
                    list.Add(lbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(TY_OA_MgrTaskDtlModel ObjModel)
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
                        num = new TY_OA_MgrTaskDtlBLL().Insert(ObjModel, transaction);
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
                        num = new TY_OA_MgrTaskDtlBLL().ModifyAlreadyAuditing(Code, transaction);
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
                        num = new TY_OA_MgrTaskDtlBLL().ModifyNotAuditing(Code, transaction);
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
                        num = new TY_OA_MgrTaskDtlBLL().ModifyNotPassAuditing(Code, transaction);
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
                        num = new TY_OA_MgrTaskDtlBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(TY_OA_MgrTaskDtlModel ObjModel)
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
                        num = new TY_OA_MgrTaskDtlBLL().Update(ObjModel, transaction);
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

        public int UpdateTY_OA_MgrTaskDtlList(List<TY_OA_MgrTaskDtlModel> ObjModelDtls, int MgrCodeID)
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
                        num = new TY_OA_MgrTaskDtlBLL().UpdateTY_OA_MgrTaskDtlList(transaction, ObjModelDtls, MgrCodeID);
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

