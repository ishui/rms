namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_InFileRegisterMainBFL
    {
        public int Delete(GK_OA_InFileRegisterMainModel ObjModel)
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
                        num = new GK_OA_InFileRegisterMainBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_InFileRegisterMainModel GetGK_OA_InFileRegisterMain(int Code)
        {
            GK_OA_InFileRegisterMainModel model = new GK_OA_InFileRegisterMainModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_InFileRegisterMainBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_InFileRegisterMainModel> GetGK_OA_InFileRegisterMainList(GK_OA_InFileRegisterMainQueryModel QueryModel)
        {
            List<GK_OA_InFileRegisterMainModel> models = new List<GK_OA_InFileRegisterMainModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_InFileRegisterMainQueryModel();
                    }
                    models = new GK_OA_InFileRegisterMainBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_InFileRegisterMainModel> GetGK_OA_InFileRegisterMainList(string SortColumns, int StartRecord, int MaxRecords, string SystemCodeEqual, string FileCodeEqual, DateTime InFileDateEqualStart, DateTime InFileDateEqualEnd)
        {
            List<GK_OA_InFileRegisterMainModel> models = new List<GK_OA_InFileRegisterMainModel>();
            GK_OA_InFileRegisterMainQueryModel objQueryModel = new GK_OA_InFileRegisterMainQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.SystemCodeEqual = SystemCodeEqual;
            objQueryModel.FileCodeEqual = FileCodeEqual;
            objQueryModel.InFileDateEqualStart = InFileDateEqualStart;
            objQueryModel.InFileDateEqualEnd = InFileDateEqualEnd;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_InFileRegisterMainBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_InFileRegisterMainModel> GetGK_OA_InFileRegisterMainListOne(int Code)
        {
            List<GK_OA_InFileRegisterMainModel> list = new List<GK_OA_InFileRegisterMainModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_InFileRegisterMainBLL nbll = new GK_OA_InFileRegisterMainBLL();
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

        public int Insert(GK_OA_InFileRegisterMainModel ObjModel)
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
                        num = new GK_OA_InFileRegisterMainBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_InFileRegisterMainModel ObjModel)
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
                        num = new GK_OA_InFileRegisterMainBLL().Update(ObjModel, transaction);
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

