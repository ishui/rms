namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_InFileRegisterBFL
    {
        public int Delete(GK_OA_InFileRegisterModel ObjModel)
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
                        num = new GK_OA_InFileRegisterBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_InFileRegisterModel GetGK_OA_InFileRegister(int Code)
        {
            GK_OA_InFileRegisterModel model = new GK_OA_InFileRegisterModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_InFileRegisterBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_InFileRegisterModel> GetGK_OA_InFileRegisterByAuditing(string RegisterAuditingMainCodeEqual)
        {
            List<GK_OA_InFileRegisterModel> models = new List<GK_OA_InFileRegisterModel>();
            GK_OA_InFileRegisterQueryModel objQueryModel = new GK_OA_InFileRegisterQueryModel();
            objQueryModel.AuditingMailCodeEqual = RegisterAuditingMainCodeEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_InFileRegisterBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_InFileRegisterModel> GetGK_OA_InFileRegisterList(GK_OA_InFileRegisterQueryModel QueryModel)
        {
            List<GK_OA_InFileRegisterModel> models = new List<GK_OA_InFileRegisterModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_InFileRegisterQueryModel();
                    }
                    models = new GK_OA_InFileRegisterBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_InFileRegisterModel> GetGK_OA_InFileRegisterList(string SortColumns, int StartRecord, int MaxRecords, string RegisterMainCodeEqual)
        {
            List<GK_OA_InFileRegisterModel> models = new List<GK_OA_InFileRegisterModel>();
            GK_OA_InFileRegisterQueryModel objQueryModel = new GK_OA_InFileRegisterQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.RegisterMainCodeEqual = RegisterMainCodeEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_InFileRegisterBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_InFileRegisterModel> GetGK_OA_InFileRegisterListOne(int Code)
        {
            List<GK_OA_InFileRegisterModel> list = new List<GK_OA_InFileRegisterModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_InFileRegisterBLL rbll = new GK_OA_InFileRegisterBLL();
                    list.Add(rbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(GK_OA_InFileRegisterModel ObjModel)
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
                        num = new GK_OA_InFileRegisterBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_InFileRegisterModel ObjModel)
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
                        num = new GK_OA_InFileRegisterBLL().Update(ObjModel, transaction);
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

