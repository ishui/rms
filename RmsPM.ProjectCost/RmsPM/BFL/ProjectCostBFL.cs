namespace RmsPM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using RmsPM.BLL;
    using TiannuoPM.MODEL;

    public class ProjectCostBFL
    {
        public int Delete(ProjectCostModel ObjModel)
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
                        num = new ProjectCostBLL().Delete(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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

        public static void DeleteAllProjectCost()
        {
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();
                try
                {
                    try
                    {
                        new ProjectCostBLL().DeleteAll(tran);
                        tran.Commit();
                    }
                    catch (SqlException exception)
                    {
                        tran.Rollback();
                        throw exception;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static string GetImportProjectCostFieldDesc()
        {
            ProjectCostImport import = new ProjectCostImport();
            return import.GetDefineFieldDesc();
        }

        public ProjectCostModel GetProjectCost(int Code)
        {
            ProjectCostModel model;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new ProjectCostBLL().GetModel(Code, connection);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<ProjectCostModel> GetProjectCostList(ProjectCostQueryModel QueryModel)
        {
            List<ProjectCostModel> models;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    if (QueryModel == null)
                    {
                        QueryModel = new ProjectCostQueryModel();
                    }
                    models = new ProjectCostBLL().GetModels(QueryModel, connection);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ProjectCostModel> GetProjectCostList(string SortColumns, int StartRecord, int MaxRecords, string AccessRange, string ProjectCostCodeEqual, string ProjectNameEqual, string GroupCodeEqual, string AreaRange1, string AreaRange2, string PriceRange1, string PriceRange2, string MoneyRange1, string MoneyRange2, string QtyRange1, string QtyRange2, string UnitEqual, string InputPersonEqual, string InputDateRange1, string InputDateRange2, string RemarkEqual)
        {
            List<ProjectCostModel> models;
            ProjectCostQueryModel objQueryModel = new ProjectCostQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.AccessRange = AccessRange;
            objQueryModel.ProjectCostCodeEqual = ProjectCostCodeEqual;
            objQueryModel.ProjectNameEqual = ProjectNameEqual;
            objQueryModel.GroupCodeEqual = GroupCodeEqual;
            objQueryModel.AreaRange1 = AreaRange1;
            objQueryModel.AreaRange2 = AreaRange2;
            objQueryModel.PriceRange1 = PriceRange1;
            objQueryModel.PriceRange2 = PriceRange2;
            objQueryModel.MoneyRange1 = MoneyRange1;
            objQueryModel.MoneyRange2 = MoneyRange2;
            objQueryModel.QtyRange1 = QtyRange1;
            objQueryModel.QtyRange2 = QtyRange2;
            objQueryModel.UnitEqual = UnitEqual;
            objQueryModel.InputPersonEqual = InputPersonEqual;
            objQueryModel.InputDateRange1 = InputDateRange1;
            objQueryModel.InputDateRange2 = InputDateRange2;
            objQueryModel.RemarkEqual = RemarkEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new ProjectCostBLL().GetModels(objQueryModel, connection);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<ProjectCostModel> GetProjectCostListOne(int Code)
        {
            List<ProjectCostModel> list = new List<ProjectCostModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    ProjectCostBLL tbll = new ProjectCostBLL();
                    list.Add(tbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public static string ImportProjectCost(Stream stream, string InputPerson)
        {
            string text;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                try
                {
                    ProjectCostImport import = new ProjectCostImport();
                    import.conn = connection;
                    text = import.Import(stream, InputPerson);
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return text;
        }

        public int Insert(ProjectCostModel ObjModel)
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
                        num = new ProjectCostBLL().Insert(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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

        public int Update(ProjectCostModel ObjModel)
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
                        num = new ProjectCostBLL().Update(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
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

