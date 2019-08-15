namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TCheckPaymentBFL
    {
        public int Delete(TCheckPaymentModel ObjModel)
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
                        TCheckPaymentBLL tbll = new TCheckPaymentBLL();
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

        public TCheckPaymentModel GetTCheckPayment(int Code)
        {
            TCheckPaymentModel model = new TCheckPaymentModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new TCheckPaymentBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TCheckPaymentModel> GetTCheckPaymentList(TCheckPaymentQueryModel QueryModel)
        {
            List<TCheckPaymentModel> models = new List<TCheckPaymentModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new TCheckPaymentQueryModel();
                    }
                    models = new TCheckPaymentBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TCheckPaymentModel> GetTCheckPaymentList(string SortColumns, int StartRecord, int MaxRecords, string StateEqual, string FlagEqual)
        {
            List<TCheckPaymentModel> models = new List<TCheckPaymentModel>();
            TCheckPaymentQueryModel objQueryModel = new TCheckPaymentQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            if (SortColumns == null)
            {
                SortColumns = "";
            }
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.StateEqual = StateEqual;
            objQueryModel.FlagEqual = FlagEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new TCheckPaymentBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TCheckPaymentModel> GetTCheckPaymentListOne(int Code)
        {
            List<TCheckPaymentModel> list = new List<TCheckPaymentModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TCheckPaymentBLL tbll = new TCheckPaymentBLL();
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

        public int Update(TCheckPaymentModel ObjModel)
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
                        num = new TCheckPaymentBLL().Update(ObjModel, transaction);
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

