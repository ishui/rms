namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_BigGoodsBFL
    {
        public int Delete(TC_OA_BigGoodsModel ObjModel)
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
                        num = new TC_OA_BigGoodsBLL().Delete(ObjModel.Code, transaction);
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

        public TC_OA_BigGoodsModel GetTC_OA_BigGoods(int Code)
        {
            TC_OA_BigGoodsModel model = new TC_OA_BigGoodsModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new TC_OA_BigGoodsBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<TC_OA_BigGoodsModel> GetTC_OA_BigGoodsList(TC_OA_BigGoodsQueryModel QueryModel)
        {
            List<TC_OA_BigGoodsModel> models = new List<TC_OA_BigGoodsModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    if (QueryModel == null)
                    {
                        QueryModel = new TC_OA_BigGoodsQueryModel();
                    }
                    models = new TC_OA_BigGoodsBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_BigGoodsModel> GetTC_OA_BigGoodsList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string UserCodeEqual, string UnitCodeEqual, DateTime ApplayDateEqual, string NameEqual, string UseWayEqual, string TypeEqual, string GoodsCodeEqual, int AuditingEqual)
        {
            List<TC_OA_BigGoodsModel> models = new List<TC_OA_BigGoodsModel>();
            TC_OA_BigGoodsQueryModel objQueryModel = new TC_OA_BigGoodsQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.UserCodeEqual = UserCodeEqual;
            objQueryModel.UnitCodeEqual = UnitCodeEqual;
            objQueryModel.ApplayDateEqual = ApplayDateEqual;
            objQueryModel.NameEqual = NameEqual;
            objQueryModel.UseWayEqual = UseWayEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.GoodsCodeEqual = GoodsCodeEqual;
            objQueryModel.AuditingEqual = AuditingEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new TC_OA_BigGoodsBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<TC_OA_BigGoodsModel> GetTC_OA_BigGoodsListOne(int Code)
        {
            List<TC_OA_BigGoodsModel> list = new List<TC_OA_BigGoodsModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    TC_OA_BigGoodsBLL sbll = new TC_OA_BigGoodsBLL();
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

        public int Insert(TC_OA_BigGoodsModel ObjModel)
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
                        num = new TC_OA_BigGoodsBLL().Insert(ObjModel, transaction);
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

        public int ModifyAlreadyAuditing(int Code)
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
                        num = new TC_OA_BigGoodsBLL().ModifyAlreadyAuditing(Code, transaction);
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

        public int ModifyNotAuditing(int Code)
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
                        num = new TC_OA_BigGoodsBLL().ModifyNotAuditing(Code, transaction);
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

        public int ModifyNotPassAuditing(int Code)
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
                        num = new TC_OA_BigGoodsBLL().ModifyNotPassAuditing(Code, transaction);
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

        public int ModifyPassAuditing(int Code)
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
                        num = new TC_OA_BigGoodsBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(TC_OA_BigGoodsModel ObjModel)
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
                        num = new TC_OA_BigGoodsBLL().Update(ObjModel, transaction);
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

