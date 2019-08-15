namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_SmallGoodsDetailBFL
    {
        public int Delete(TC_OA_SmallGoodsDetailModel ObjModel)
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
                        num = new TC_OA_SmallGoodsDetailBLL().Delete(ObjModel.Code, transaction);
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

        public TC_OA_SmallGoodsDetailModel GetTC_OA_SmallGoodsDetail(int Code)
        {
            TC_OA_SmallGoodsDetailModel model = new TC_OA_SmallGoodsDetailModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new TC_OA_SmallGoodsDetailBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TC_OA_SmallGoodsDetailModel> GetTC_OA_SmallGoodsDetailList(TC_OA_SmallGoodsDetailQueryModel QueryModel)
        {
            List<TC_OA_SmallGoodsDetailModel> models = new List<TC_OA_SmallGoodsDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new TC_OA_SmallGoodsDetailQueryModel();
                    }
                    models = new TC_OA_SmallGoodsDetailBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_SmallGoodsDetailModel> GetTC_OA_SmallGoodsDetailList(string SortColumns, int StartRecord, int MaxRecords, string MastCodeEqual, int IsSubmitEqual)
        {
            List<TC_OA_SmallGoodsDetailModel> models = new List<TC_OA_SmallGoodsDetailModel>();
            TC_OA_SmallGoodsDetailQueryModel objQueryModel = new TC_OA_SmallGoodsDetailQueryModel();
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
                    models = new TC_OA_SmallGoodsDetailBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_SmallGoodsDetailModel> GetTC_OA_SmallGoodsDetailListOne(int Code)
        {
            List<TC_OA_SmallGoodsDetailModel> list = new List<TC_OA_SmallGoodsDetailModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TC_OA_SmallGoodsDetailBLL lbll = new TC_OA_SmallGoodsDetailBLL();
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

        public int Insert(TC_OA_SmallGoodsDetailModel ObjModel)
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
                        num = new TC_OA_SmallGoodsDetailBLL().Insert(ObjModel, transaction);
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

        public int Update(TC_OA_SmallGoodsDetailModel ObjModel)
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
                        num = new TC_OA_SmallGoodsDetailBLL().Update(ObjModel, transaction);
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

