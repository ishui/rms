namespace RmsDM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.BLL;
    using RmsDM.MODEL;

    public class FileTemplateVersionBFL
    {
        public int Delete(FileTemplateVersionModel ObjModel)
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
                        num = new FileTemplateVersionBLL().Delete(ObjModel.Code, transaction);
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

        public FileTemplateVersionModel GetFileTemplateVersion(int Code)
        {
            FileTemplateVersionModel model = new FileTemplateVersionModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new FileTemplateVersionBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<FileTemplateVersionModel> GetFileTemplateVersionList(FileTemplateVersionQueryModel QueryModel)
        {
            List<FileTemplateVersionModel> models = new List<FileTemplateVersionModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new FileTemplateVersionQueryModel();
                    }
                    models = new FileTemplateVersionBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<FileTemplateVersionModel> GetFileTemplateVersionList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, int FileTemplateCodeEqual, string WorkFlowProcedureNameEqual, string VersionNumberEqual, string IsPigeonholeEqual, string PigeonholeTimeEqual, string SaveTermEqual, string RecordKindEqual, string MarkingSNRuleEqual, string IsAvailabilityEqual)
        {
            List<FileTemplateVersionModel> models = new List<FileTemplateVersionModel>();
            FileTemplateVersionQueryModel objQueryModel = new FileTemplateVersionQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.FileTemplateCodeEqual = FileTemplateCodeEqual;
            objQueryModel.WorkFlowProcedureNameEqual = WorkFlowProcedureNameEqual;
            objQueryModel.VersionNumberEqual = VersionNumberEqual;
            objQueryModel.IsPigeonholeEqual = IsPigeonholeEqual;
            objQueryModel.PigeonholeTimeEqual = PigeonholeTimeEqual;
            objQueryModel.SaveTermEqual = SaveTermEqual;
            objQueryModel.RecordKindEqual = RecordKindEqual;
            objQueryModel.MarkingSNRuleEqual = MarkingSNRuleEqual;
            objQueryModel.IsAvailabilityEqual = IsAvailabilityEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new FileTemplateVersionBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<FileTemplateVersionModel> GetFileTemplateVersionListOne(int Code)
        {
            List<FileTemplateVersionModel> list = new List<FileTemplateVersionModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    FileTemplateVersionBLL nbll = new FileTemplateVersionBLL();
                    list.Add(nbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(FileTemplateVersionModel ObjModel)
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
                        num = new FileTemplateVersionBLL().Insert(ObjModel, transaction);
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

        public int Update(FileTemplateVersionModel ObjModel)
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
                        num = new FileTemplateVersionBLL().Update(ObjModel, transaction);
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

