namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class RS_EmployScoreBFL
    {
        public int Delete(RS_EmployScoreModel ObjModel)
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
                        num = new RS_EmployScoreBLL().Delete(ObjModel.Code, transaction);
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

        public RS_EmployScoreModel GetRS_EmployScore(int Code)
        {
            RS_EmployScoreModel model = new RS_EmployScoreModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new RS_EmployScoreBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<RS_EmployScoreModel> GetRS_EmployScoreList(RS_EmployScoreQueryModel QueryModel)
        {
            List<RS_EmployScoreModel> models = new List<RS_EmployScoreModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new RS_EmployScoreQueryModel();
                    }
                    models = new RS_EmployScoreBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<RS_EmployScoreModel> GetRS_EmployScoreList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, int ManageCodeEqual, string UserCodeEqual, int ScoreEqual)
        {
            List<RS_EmployScoreModel> models = new List<RS_EmployScoreModel>();
            RS_EmployScoreQueryModel objQueryModel = new RS_EmployScoreQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.ManageCodeEqual = ManageCodeEqual;
            objQueryModel.UserCodeEqual = UserCodeEqual;
            objQueryModel.ScoreEqual = ScoreEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new RS_EmployScoreBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<RS_EmployScoreModel> GetRS_EmployScoreList(string SortColumns, int StartRecord, int MaxRecords, string CodeEqual, string ManageCodeEqual, string UserCodeEqual, string ScoreEqual)
        {
            List<RS_EmployScoreModel> models = new List<RS_EmployScoreModel>();
            RS_EmployScoreQueryModel objQueryModel = new RS_EmployScoreQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            if (!string.IsNullOrEmpty(CodeEqual))
            {
                objQueryModel.CodeEqual = int.Parse(CodeEqual);
            }
            if (!string.IsNullOrEmpty(ManageCodeEqual))
            {
                objQueryModel.ManageCodeEqual = int.Parse(ManageCodeEqual);
            }
            if (!string.IsNullOrEmpty(UserCodeEqual))
            {
                objQueryModel.UserCodeEqual = UserCodeEqual;
            }
            if (!string.IsNullOrEmpty(ScoreEqual))
            {
                objQueryModel.ScoreEqual = int.Parse(ScoreEqual);
            }
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    try
                    {
                        connection.Open();
                        models = new RS_EmployScoreBLL().GetModels(objQueryModel, connection);
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

        public int Insert(RS_EmployScoreModel ObjModel)
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
                        num = new RS_EmployScoreBLL().Insert(ObjModel, transaction);
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

        public int Update(RS_EmployScoreModel ObjModel)
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
                        num = new RS_EmployScoreBLL().Update(ObjModel, transaction);
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

