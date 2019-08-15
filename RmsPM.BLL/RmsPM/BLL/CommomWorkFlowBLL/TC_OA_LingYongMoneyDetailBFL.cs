namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_LingYongMoneyDetailBFL
    {
        public int Delete(TC_OA_LingYongMoneyDetailModel ObjModel)
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
                        num = new TC_OA_LingYongMoneyDetailBLL().Delete(ObjModel.Code, transaction);
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

        public TC_OA_LingYongMoneyDetailModel GetTC_OA_LingYongMoneyDetail(int Code)
        {
            TC_OA_LingYongMoneyDetailModel model = new TC_OA_LingYongMoneyDetailModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new TC_OA_LingYongMoneyDetailBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TC_OA_LingYongMoneyDetailModel> GetTC_OA_LingYongMoneyDetailList(TC_OA_LingYongMoneyDetailQueryModel QueryModel)
        {
            List<TC_OA_LingYongMoneyDetailModel> models = new List<TC_OA_LingYongMoneyDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new TC_OA_LingYongMoneyDetailQueryModel();
                    }
                    models = new TC_OA_LingYongMoneyDetailBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_LingYongMoneyDetailModel> GetTC_OA_LingYongMoneyDetailList(string SortColumns, int StartRecord, int MaxRecords, string MastCodeEqual, int IsSubmitEqual)
        {
            List<TC_OA_LingYongMoneyDetailModel> models = new List<TC_OA_LingYongMoneyDetailModel>();
            TC_OA_LingYongMoneyDetailQueryModel objQueryModel = new TC_OA_LingYongMoneyDetailQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            if (SortColumns == null)
            {
                SortColumns = "";
            }
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.MastCodeEqual = MastCodeEqual;
            objQueryModel.IsSubmitEqual = IsSubmitEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new TC_OA_LingYongMoneyDetailBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_LingYongMoneyDetailModel> GetTC_OA_LingYongMoneyDetailListOne(int Code)
        {
            List<TC_OA_LingYongMoneyDetailModel> list = new List<TC_OA_LingYongMoneyDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TC_OA_LingYongMoneyDetailBLL lbll = new TC_OA_LingYongMoneyDetailBLL();
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

        public int Insert(TC_OA_LingYongMoneyDetailModel ObjModel)
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
                        num = new TC_OA_LingYongMoneyDetailBLL().Insert(ObjModel, transaction);
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

        public int Update(TC_OA_LingYongMoneyDetailModel ObjModel)
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
                        num = new TC_OA_LingYongMoneyDetailBLL().Update(ObjModel, transaction);
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

