namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_MakeMarksBFL
    {
        public int Delete(GK_OA_MakeMarksModel ObjModel)
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
                        num = new GK_OA_MakeMarksBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_MakeMarksModel GetGK_OA_MakeMarks(int Code)
        {
            GK_OA_MakeMarksModel model = new GK_OA_MakeMarksModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_MakeMarksBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_MakeMarksModel> GetGK_OA_MakeMarksList(GK_OA_MakeMarksQueryModel QueryModel)
        {
            List<GK_OA_MakeMarksModel> models = new List<GK_OA_MakeMarksModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_MakeMarksQueryModel();
                    }
                    models = new GK_OA_MakeMarksBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_MakeMarksModel> GetGK_OA_MakeMarksList(string SortColumns, int StartRecord, int MaxRecords, string UnitCodeEqual, string MarkTypeEqual, DateTime RegisterDateEqualStart, DateTime RegisterDateEqualEnd, string TitleEqual)
        {
            List<GK_OA_MakeMarksModel> models = new List<GK_OA_MakeMarksModel>();
            GK_OA_MakeMarksQueryModel objQueryModel = new GK_OA_MakeMarksQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.UnitCodeEqual = UnitCodeEqual;
            objQueryModel.MarkTypeEqual = MarkTypeEqual;
            objQueryModel.RegisterDateEqualStart = RegisterDateEqualStart;
            objQueryModel.RegisterDateEqualEnd = RegisterDateEqualEnd;
            objQueryModel.TitleEqual = TitleEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_MakeMarksBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_MakeMarksModel> GetGK_OA_MakeMarksListOne(int Code)
        {
            List<GK_OA_MakeMarksModel> list = new List<GK_OA_MakeMarksModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_MakeMarksBLL sbll = new GK_OA_MakeMarksBLL();
                    list.Add(sbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(GK_OA_MakeMarksModel ObjModel)
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
                        num = new GK_OA_MakeMarksBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_MakeMarksModel ObjModel)
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
                        num = new GK_OA_MakeMarksBLL().Update(ObjModel, transaction);
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

