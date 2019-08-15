namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OAPersonPolityBFL
    {
        public int Delete(GK_OAPersonPolityModel ObjModel)
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
                        num = new GK_OAPersonPolityBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OAPersonPolityModel GetGK_OAPersonPolity(int Code)
        {
            GK_OAPersonPolityModel model = new GK_OAPersonPolityModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OAPersonPolityBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OAPersonPolityModel> GetGK_OAPersonPolityList(GK_OAPersonPolityQueryModel QueryModel)
        {
            List<GK_OAPersonPolityModel> models = new List<GK_OAPersonPolityModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OAPersonPolityQueryModel();
                    }
                    models = new GK_OAPersonPolityBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OAPersonPolityModel> GetGK_OAPersonPolityList(string SortColumns, int StartRecord, int MaxRecords, string PersonIDEqual)
        {
            List<GK_OAPersonPolityModel> models = new List<GK_OAPersonPolityModel>();
            GK_OAPersonPolityQueryModel objQueryModel = new GK_OAPersonPolityQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.PersonIDEqual = PersonIDEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OAPersonPolityBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OAPersonPolityModel> GetGK_OAPersonPolityListOne(int Code)
        {
            List<GK_OAPersonPolityModel> list = new List<GK_OAPersonPolityModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OAPersonPolityBLL ybll = new GK_OAPersonPolityBLL();
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

        public int Insert(GK_OAPersonPolityModel ObjModel)
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
                        num = new GK_OAPersonPolityBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OAPersonPolityModel ObjModel)
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
                        num = new GK_OAPersonPolityBLL().Update(ObjModel, transaction);
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

