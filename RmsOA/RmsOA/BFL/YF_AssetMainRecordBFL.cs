namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class YF_AssetMainRecordBFL
    {
        public int Delete(YF_AssetMainRecordModel ObjModel)
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
                        num = new YF_AssetMainRecordBLL().Delete(ObjModel.Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
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

        public YF_AssetMainRecordModel GetYF_AssetMainRecord(int Code)
        {
            YF_AssetMainRecordModel model = new YF_AssetMainRecordModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new YF_AssetMainRecordBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<YF_AssetMainRecordModel> GetYF_AssetMainRecordList(YF_AssetMainRecordQueryModel QueryModel)
        {
            List<YF_AssetMainRecordModel> models = new List<YF_AssetMainRecordModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new YF_AssetMainRecordQueryModel();
                    }
                    models = new YF_AssetMainRecordBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<YF_AssetMainRecordModel> GetYF_AssetMainRecordList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, int FKCodeEqual, string ContentEqual, int CostTimeEqual, decimal CostMoneyEqual, string ChangeDetailEqual, string UserSignEqual, string MainUserEqual, DateTime MainTimeEqual, string ResultEqual)
        {
            List<YF_AssetMainRecordModel> models = new List<YF_AssetMainRecordModel>();
            YF_AssetMainRecordQueryModel objQueryModel = new YF_AssetMainRecordQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.FKCodeEqual = FKCodeEqual;
            objQueryModel.ContentEqual = ContentEqual;
            objQueryModel.CostTimeEqual = CostTimeEqual;
            objQueryModel.CostMoneyEqual = CostMoneyEqual;
            objQueryModel.ChangeDetailEqual = ChangeDetailEqual;
            objQueryModel.UserSignEqual = UserSignEqual;
            objQueryModel.MainUserEqual = MainUserEqual;
            objQueryModel.MainTimeEqual = MainTimeEqual;
            objQueryModel.ResultEqual = ResultEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new YF_AssetMainRecordBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<YF_AssetMainRecordModel> GetYF_AssetMainRecordList(string SortColumns, int StartRecord, int MaxRecords, string CodeEqual, string FKCodeEqual, string ContentEqual, string CostTimeEqual, string CostMoneyEqual, string ChangeDetailEqual, string UserSignEqual, string MainUserEqual, string MainTimeEqual, string ResultEqual)
        {
            List<YF_AssetMainRecordModel> models = new List<YF_AssetMainRecordModel>();
            YF_AssetMainRecordQueryModel objQueryModel = new YF_AssetMainRecordQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            if (!string.IsNullOrEmpty(CodeEqual))
            {
                objQueryModel.CodeEqual = int.Parse(CodeEqual);
            }
            if (!string.IsNullOrEmpty(FKCodeEqual))
            {
                objQueryModel.FKCodeEqual = int.Parse(FKCodeEqual);
            }
            if (!string.IsNullOrEmpty(ContentEqual))
            {
                objQueryModel.ContentEqual = ContentEqual;
            }
            if (!string.IsNullOrEmpty(CostTimeEqual))
            {
                objQueryModel.CostTimeEqual = int.Parse(CostTimeEqual);
            }
            if (!string.IsNullOrEmpty(CostMoneyEqual))
            {
                objQueryModel.CostMoneyEqual = decimal.Parse(CostMoneyEqual);
            }
            if (!string.IsNullOrEmpty(ChangeDetailEqual))
            {
                objQueryModel.ChangeDetailEqual = ChangeDetailEqual;
            }
            if (!string.IsNullOrEmpty(UserSignEqual))
            {
                objQueryModel.UserSignEqual = UserSignEqual;
            }
            if (!string.IsNullOrEmpty(MainUserEqual))
            {
                objQueryModel.MainUserEqual = MainUserEqual;
            }
            if (!string.IsNullOrEmpty(MainTimeEqual))
            {
                objQueryModel.MainTimeEqual = DateTime.Parse(MainTimeEqual);
            }
            if (!string.IsNullOrEmpty(ResultEqual))
            {
                objQueryModel.ResultEqual = ResultEqual;
            }
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    try
                    {
                        connection.Open();
                        models = new YF_AssetMainRecordBLL().GetModels(objQueryModel, connection);
                    }
                    catch (SqlException exception)
                    {
                        throw exception;
                    }
                    return models;
                }
                finally
                {
                    connection.Close();
                }
            }
            return models;
        }

        public int Insert(YF_AssetMainRecordModel ObjModel)
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
                        num = new YF_AssetMainRecordBLL().Insert(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
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

        public int Update(YF_AssetMainRecordModel ObjModel)
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
                        num = new YF_AssetMainRecordBLL().Update(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
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

