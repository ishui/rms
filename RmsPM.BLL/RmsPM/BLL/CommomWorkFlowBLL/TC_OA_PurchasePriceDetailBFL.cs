namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_PurchasePriceDetailBFL
    {
        public int Delete(TC_OA_PurchasePriceDetailModel ObjModel)
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
                        num = new TC_OA_PurchasePriceDetailBLL().Delete(ObjModel.Code, transaction);
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

        public TC_OA_PurchasePriceDetailModel GetTC_OA_PurchasePriceDetail(int Code)
        {
            TC_OA_PurchasePriceDetailModel model = new TC_OA_PurchasePriceDetailModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new TC_OA_PurchasePriceDetailBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TC_OA_PurchasePriceDetailModel> GetTC_OA_PurchasePriceDetailList(TC_OA_PurchasePriceDetailQueryModel QueryModel)
        {
            List<TC_OA_PurchasePriceDetailModel> models = new List<TC_OA_PurchasePriceDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new TC_OA_PurchasePriceDetailQueryModel();
                    }
                    models = new TC_OA_PurchasePriceDetailBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_PurchasePriceDetailModel> GetTC_OA_PurchasePriceDetailList(string SortColumns, int StartRecord, int MaxRecords, string MastCodeEqual, int IsSubmitEqual)
        {
            List<TC_OA_PurchasePriceDetailModel> models = new List<TC_OA_PurchasePriceDetailModel>();
            TC_OA_PurchasePriceDetailQueryModel objQueryModel = new TC_OA_PurchasePriceDetailQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
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
                    models = new TC_OA_PurchasePriceDetailBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_PurchasePriceDetailModel> GetTC_OA_PurchasePriceDetailListOne(int Code)
        {
            List<TC_OA_PurchasePriceDetailModel> list = new List<TC_OA_PurchasePriceDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TC_OA_PurchasePriceDetailBLL lbll = new TC_OA_PurchasePriceDetailBLL();
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

        public int Insert(TC_OA_PurchasePriceDetailModel ObjModel)
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
                        num = new TC_OA_PurchasePriceDetailBLL().Insert(ObjModel, transaction);
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

        public int Update(TC_OA_PurchasePriceDetailModel ObjModel)
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
                        num = new TC_OA_PurchasePriceDetailBLL().Update(ObjModel, transaction);
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

