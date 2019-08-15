namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class ContractBillBFL
    {
        public int Delete(ContractBillModel ObjModel)
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
                        num = new ContractBillBLL().Delete(ObjModel.Code, transaction);
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

        public ContractBillModel GetContractBill(int Code)
        {
            ContractBillModel model = new ContractBillModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new ContractBillBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<ContractBillModel> GetContractBillList(ContractBillQueryModel QueryModel)
        {
            List<ContractBillModel> models = new List<ContractBillModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new ContractBillQueryModel();
                    }
                    models = new ContractBillBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ContractBillModel> GetContractBillList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string ProjectCodeEqual, string ContractCodeEqual, string BillNoEqual, decimal BillMoneyEqual)
        {
            List<ContractBillModel> models = new List<ContractBillModel>();
            ContractBillQueryModel objQueryModel = new ContractBillQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.ProjectCodeEqual = ProjectCodeEqual;
            objQueryModel.ContractCodeEqual = ContractCodeEqual;
            objQueryModel.BillNoEqual = BillNoEqual;
            objQueryModel.BillMoneyEqual = BillMoneyEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new ContractBillBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ContractBillModel> GetContractBillListOne(int Code)
        {
            List<ContractBillModel> list = new List<ContractBillModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    ContractBillBLL lbll = new ContractBillBLL();
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

        public int Insert(ContractBillModel ObjModel)
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
                        num = new ContractBillBLL().Insert(ObjModel, transaction);
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

        public int Update(ContractBillModel ObjModel)
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
                        num = new ContractBillBLL().Update(ObjModel, transaction);
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

