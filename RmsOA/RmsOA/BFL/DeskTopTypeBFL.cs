namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class DeskTopTypeBFL
    {
        public int Delete(DeskTopTypeModel ObjModel)
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
                        num = new DeskTopTypeBLL().Delete(ObjModel.Code, transaction);
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

        public DeskTopTypeModel GetDeskTopType(int Code)
        {
            DeskTopTypeModel model = new DeskTopTypeModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new DeskTopTypeBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<DeskTopTypeModel> GetDeskTopTypeList(DeskTopTypeQueryModel QueryModel)
        {
            List<DeskTopTypeModel> models = new List<DeskTopTypeModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    if (QueryModel == null)
                    {
                        QueryModel = new DeskTopTypeQueryModel();
                    }
                    models = new DeskTopTypeBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DeskTopTypeModel> GetDeskTopTypeList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, int ControldIDEqual, string DeskTypeEqual)
        {
            List<DeskTopTypeModel> models = new List<DeskTopTypeModel>();
            DeskTopTypeQueryModel objQueryModel = new DeskTopTypeQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.ControldIDEqual = ControldIDEqual;
            objQueryModel.DeskTypeEqual = DeskTypeEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new DeskTopTypeBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DeskTopTypeModel> GetDeskTopTypeListOne(int Code)
        {
            List<DeskTopTypeModel> list = new List<DeskTopTypeModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    DeskTopTypeBLL ebll = new DeskTopTypeBLL();
                    list.Add(ebll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public static List<string> GetIDCollectionByDesktopType(DesktopType deskType)
        {
            List<string> list = new List<string>();
            DeskTopTypeQueryModel queryModel = new DeskTopTypeQueryModel();
            queryModel.DeskTypeEqual = deskType.ToString("d");
            List<DeskTopTypeModel> deskTopTypeList = new DeskTopTypeBFL().GetDeskTopTypeList(queryModel);
            foreach (DeskTopTypeModel model2 in deskTopTypeList)
            {
                list.Add(model2.ControldID.ToString());
            }
            return list;
        }

        public int Insert(DeskTopTypeModel ObjModel)
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
                        num = new DeskTopTypeBLL().Insert(ObjModel, transaction);
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

        public int Update(DeskTopTypeModel ObjModel)
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
                        num = new DeskTopTypeBLL().Update(ObjModel, transaction);
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

