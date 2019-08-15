namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_BigGoodsDetailBFL
    {
        public int Delete(TC_OA_BigGoodsDetailModel ObjModel)
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
                        num = new TC_OA_BigGoodsDetailBLL().Delete(ObjModel.Code, transaction);
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

        public TC_OA_BigGoodsDetailModel GetTC_OA_BigGoodsDetail(int Code)
        {
            TC_OA_BigGoodsDetailModel model = new TC_OA_BigGoodsDetailModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new TC_OA_BigGoodsDetailBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TC_OA_BigGoodsDetailModel> GetTC_OA_BigGoodsDetailList(TC_OA_BigGoodsDetailQueryModel QueryModel)
        {
            List<TC_OA_BigGoodsDetailModel> models = new List<TC_OA_BigGoodsDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new TC_OA_BigGoodsDetailQueryModel();
                    }
                    models = new TC_OA_BigGoodsDetailBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_BigGoodsDetailModel> GetTC_OA_BigGoodsDetailList(string SortColumns, int StartRecord, int MaxRecords, string MastCodeEqual, int IsSubmitEqual)
        {
            List<TC_OA_BigGoodsDetailModel> models = new List<TC_OA_BigGoodsDetailModel>();
            TC_OA_BigGoodsDetailQueryModel objQueryModel = new TC_OA_BigGoodsDetailQueryModel();
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
                    models = new TC_OA_BigGoodsDetailBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_BigGoodsDetailModel> GetTC_OA_BigGoodsDetailListOne(int Code)
        {
            List<TC_OA_BigGoodsDetailModel> list = new List<TC_OA_BigGoodsDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TC_OA_BigGoodsDetailBLL lbll = new TC_OA_BigGoodsDetailBLL();
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

        public int Insert(TC_OA_BigGoodsDetailModel ObjModel)
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
                        num = new TC_OA_BigGoodsDetailBLL().Insert(ObjModel, transaction);
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

        public int Update(TC_OA_BigGoodsDetailModel ObjModel)
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
                        num = new TC_OA_BigGoodsDetailBLL().Update(ObjModel, transaction);
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

