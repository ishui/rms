namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_GoodsBFL
    {
        public int Delete(GK_OA_GoodsModel ObjModel)
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
                        num = new GK_OA_GoodsBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_GoodsModel GetGK_OA_Goods(int Code)
        {
            GK_OA_GoodsModel model = new GK_OA_GoodsModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_GoodsBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_GoodsModel> GetGK_OA_GoodsList(GK_OA_GoodsQueryModel QueryModel)
        {
            List<GK_OA_GoodsModel> models = new List<GK_OA_GoodsModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_GoodsQueryModel();
                    }
                    models = new GK_OA_GoodsBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_GoodsModel> GetGK_OA_GoodsList(string SortColumns, int StartRecord, int MaxRecords, string GoodsNameEqual, string GoodsCodeEqual, string UnitCodeEqual, DateTime InputDateEqualStart, DateTime InputDateEqualEnd)
        {
            List<GK_OA_GoodsModel> models = new List<GK_OA_GoodsModel>();
            GK_OA_GoodsQueryModel objQueryModel = new GK_OA_GoodsQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.GoodsNameEqual = GoodsNameEqual;
            objQueryModel.GoodsCodeEqual = GoodsCodeEqual;
            objQueryModel.UnitCodeEqual = UnitCodeEqual;
            objQueryModel.InputDateEqualStart = InputDateEqualStart;
            objQueryModel.InputDateEqualEnd = InputDateEqualEnd;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_GoodsBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_GoodsModel> GetGK_OA_GoodsListOne(int Code)
        {
            List<GK_OA_GoodsModel> list = new List<GK_OA_GoodsModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_GoodsBLL sbll = new GK_OA_GoodsBLL();
                    list.Add(sbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(GK_OA_GoodsModel ObjModel)
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
                        num = new GK_OA_GoodsBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_GoodsModel ObjModel)
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
                        num = new GK_OA_GoodsBLL().Update(ObjModel, transaction);
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

