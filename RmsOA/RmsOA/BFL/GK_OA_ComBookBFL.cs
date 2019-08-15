namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_ComBookBFL
    {
        public int Delete(GK_OA_ComBookModel ObjModel)
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
                        num = new GK_OA_ComBookBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_ComBookModel GetGK_OA_ComBook(int Code)
        {
            GK_OA_ComBookModel model = new GK_OA_ComBookModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new GK_OA_ComBookBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_ComBookModel> GetGK_OA_ComBookList(GK_OA_ComBookQueryModel QueryModel)
        {
            List<GK_OA_ComBookModel> models = new List<GK_OA_ComBookModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_ComBookQueryModel();
                    }
                    models = new GK_OA_ComBookBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_ComBookModel> GetGK_OA_ComBookList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string CompanyEqual, string UnitCodeEqual, string UserCodeEqual, string TelephoneEqual, string HandleTelephoneEqual, string MSNEqual, string QQEqual, string EmailEqual, string UrgePhoneEqual, string PrepField1Equal, string PrepField2Equal, string PrepField3Equal)
        {
            List<GK_OA_ComBookModel> models = new List<GK_OA_ComBookModel>();
            GK_OA_ComBookQueryModel objQueryModel = new GK_OA_ComBookQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.CompanyEqual = CompanyEqual;
            objQueryModel.UnitCodeEqual = UnitCodeEqual;
            objQueryModel.UserCodeEqual = UserCodeEqual;
            objQueryModel.TelephoneEqual = TelephoneEqual;
            objQueryModel.HandleTelephoneEqual = HandleTelephoneEqual;
            objQueryModel.MSNEqual = MSNEqual;
            objQueryModel.QQEqual = QQEqual;
            objQueryModel.EmailEqual = EmailEqual;
            objQueryModel.UrgePhoneEqual = UrgePhoneEqual;
            objQueryModel.PrepField1Equal = PrepField1Equal;
            objQueryModel.PrepField2Equal = PrepField2Equal;
            objQueryModel.PrepField3Equal = PrepField3Equal;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new GK_OA_ComBookBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_ComBookModel> GetGK_OA_ComBookListOne(int Code)
        {
            List<GK_OA_ComBookModel> list = new List<GK_OA_ComBookModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    GK_OA_ComBookBLL kbll = new GK_OA_ComBookBLL();
                    list.Add(kbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(GK_OA_ComBookModel ObjModel)
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
                        num = new GK_OA_ComBookBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_ComBookModel ObjModel)
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
                        num = new GK_OA_ComBookBLL().Update(ObjModel, transaction);
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

