namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_OutFileRegiesterBFL
    {
        public int Delete(GK_OA_OutFileRegiesterModel ObjModel)
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
                        num = new GK_OA_OutFileRegiesterBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_OutFileRegiesterModel GetGK_OA_OutFileRegiester(int Code)
        {
            GK_OA_OutFileRegiesterModel model = new GK_OA_OutFileRegiesterModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_OutFileRegiesterBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_OutFileRegiesterModel> GetGK_OA_OutFileRegiesterList(GK_OA_OutFileRegiesterQueryModel QueryModel)
        {
            List<GK_OA_OutFileRegiesterModel> models = new List<GK_OA_OutFileRegiesterModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_OutFileRegiesterQueryModel();
                    }
                    models = new GK_OA_OutFileRegiesterBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_OutFileRegiesterModel> GetGK_OA_OutFileRegiesterList(string SortColumns, int StartRecord, int MaxRecords, string FileCodeEqual, DateTime RegiesterDateEqualStart, DateTime RegiesterDateEqualEnd, string UnitCodeEqual, string UserCodeEqual)
        {
            List<GK_OA_OutFileRegiesterModel> models = new List<GK_OA_OutFileRegiesterModel>();
            GK_OA_OutFileRegiesterQueryModel objQueryModel = new GK_OA_OutFileRegiesterQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.FileCodeEqual = FileCodeEqual;
            objQueryModel.RegiesterDateEqualStart = RegiesterDateEqualStart;
            objQueryModel.RegiesterDateEqualEnd = RegiesterDateEqualEnd;
            objQueryModel.UnitCodeEqual = UnitCodeEqual;
            objQueryModel.UserCodeEqual = UserCodeEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_OutFileRegiesterBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_OutFileRegiesterModel> GetGK_OA_OutFileRegiesterListOne(int Code)
        {
            List<GK_OA_OutFileRegiesterModel> list = new List<GK_OA_OutFileRegiesterModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_OutFileRegiesterBLL rbll = new GK_OA_OutFileRegiesterBLL();
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

        public int Insert(GK_OA_OutFileRegiesterModel ObjModel)
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
                        num = new GK_OA_OutFileRegiesterBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_OutFileRegiesterModel ObjModel)
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
                        num = new GK_OA_OutFileRegiesterBLL().Update(ObjModel, transaction);
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

