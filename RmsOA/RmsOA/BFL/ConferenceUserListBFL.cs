namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class ConferenceUserListBFL
    {
        public int Delete(ConferenceUserListModel ObjModel)
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
                        num = new ConferenceUserListBLL().Delete(ObjModel.Code, transaction);
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

        public ConferenceUserListModel GetConferenceUserList(int Code)
        {
            ConferenceUserListModel model = new ConferenceUserListModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new ConferenceUserListBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<ConferenceUserListModel> GetConferenceUserListList(ConferenceUserListQueryModel QueryModel)
        {
            List<ConferenceUserListModel> models = new List<ConferenceUserListModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new ConferenceUserListQueryModel();
                    }
                    models = new ConferenceUserListBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ConferenceUserListModel> GetConferenceUserListList(string SortColumns, int StartRecord, int MaxRecords, int ConferenceCodeEqual, string UserCodeEqual, string TypeEqual, string UserNameEqual, int CodeEqual)
        {
            List<ConferenceUserListModel> models = new List<ConferenceUserListModel>();
            ConferenceUserListQueryModel objQueryModel = new ConferenceUserListQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.ConferenceCodeEqual = ConferenceCodeEqual;
            objQueryModel.UserCodeEqual = UserCodeEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.UserNameEqual = UserNameEqual;
            objQueryModel.CodeEqual = CodeEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new ConferenceUserListBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ConferenceUserListModel> GetConferenceUserListListOne(int Code)
        {
            List<ConferenceUserListModel> list = new List<ConferenceUserListModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    ConferenceUserListBLL tbll = new ConferenceUserListBLL();
                    list.Add(tbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(ConferenceUserListModel ObjModel)
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
                        num = new ConferenceUserListBLL().Insert(ObjModel, transaction);
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

        public int Update(ConferenceUserListModel ObjModel)
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
                        num = new ConferenceUserListBLL().Update(ObjModel, transaction);
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

