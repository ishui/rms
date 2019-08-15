namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_SubmitAccountDtlBFL
    {
        public int Delete(GK_OA_SubmitAccountDtlModel ObjModel)
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
                        num = new GK_OA_SubmitAccountDtlBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_SubmitAccountDtlModel GetGK_OA_SubmitAccountDtl(int Code)
        {
            GK_OA_SubmitAccountDtlModel model = new GK_OA_SubmitAccountDtlModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_SubmitAccountDtlBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_SubmitAccountDtlModel> GetGK_OA_SubmitAccountDtlList(string MastCodeEqual)
        {
            List<GK_OA_SubmitAccountDtlModel> models = new List<GK_OA_SubmitAccountDtlModel>();
            GK_OA_SubmitAccountDtlQueryModel objQueryModel = new GK_OA_SubmitAccountDtlQueryModel();
            objQueryModel.MastCodeEqual = MastCodeEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_SubmitAccountDtlBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_SubmitAccountDtlModel> GetGK_OA_SubmitAccountDtlList(GK_OA_SubmitAccountDtlQueryModel QueryModel)
        {
            List<GK_OA_SubmitAccountDtlModel> models = new List<GK_OA_SubmitAccountDtlModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_SubmitAccountDtlQueryModel();
                    }
                    models = new GK_OA_SubmitAccountDtlBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_SubmitAccountDtlModel> GetGK_OA_SubmitAccountDtlListOne(int Code)
        {
            List<GK_OA_SubmitAccountDtlModel> list = new List<GK_OA_SubmitAccountDtlModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_SubmitAccountDtlBLL lbll = new GK_OA_SubmitAccountDtlBLL();
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

        public int Insert(GK_OA_SubmitAccountDtlModel ObjModel)
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
                        num = new GK_OA_SubmitAccountDtlBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_SubmitAccountDtlModel ObjModel)
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
                        num = new GK_OA_SubmitAccountDtlBLL().Update(ObjModel, transaction);
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

