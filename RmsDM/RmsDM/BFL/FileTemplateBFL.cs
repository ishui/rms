namespace RmsDM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.BLL;
    using RmsDM.DAL;
    using RmsDM.MODEL;

    public class FileTemplateBFL
    {
        public int Delete(FileTemplateModel ObjModel)
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
                        num = new FileTemplateBLL().Delete(ObjModel.Code, transaction);
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

        public FileTemplateModel GetFileTemplate(int Code)
        {
            FileTemplateModel model = new FileTemplateModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new FileTemplateBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public FileTemplateModel GetFileTemplateByProcCode(string ProcCode)
        {
            FileTemplateModel fileTemplate = null;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    try
                    {
                        connection.Open();
                        int code = new HandMadeDAL(connection).GetFileTemplateCodeByProcCode(ProcCode);
                        fileTemplate = this.GetFileTemplate(code);
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    return fileTemplate;
                }
                finally
                {
                    connection.Close();
                }
            }
            return fileTemplate;
        }

        public List<FileTemplateModel> GetFileTemplateList(FileTemplateQueryModel QueryModel)
        {
            List<FileTemplateModel> models = new List<FileTemplateModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new FileTemplateQueryModel();
                    }
                    models = new FileTemplateBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<FileTemplateModel> GetFileTemplateList(string SortColumns, int StartRecord, int MaxRecords, int NodeCode)
        {
            FileTemplateQueryModel queryModel = new FileTemplateQueryModel();
            queryModel.FileTemplateTypeCodeEqual = NodeCode;
            queryModel.SortColumns = SortColumns;
            queryModel.StartRecord = StartRecord;
            queryModel.MaxRecords = MaxRecords;
            return this.GetFileTemplateList(queryModel);
        }

        public List<FileTemplateModel> GetFileTemplateList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, int FileTemplateTypeCodeEqual, string FileTemplateNameEqual)
        {
            List<FileTemplateModel> models = new List<FileTemplateModel>();
            FileTemplateQueryModel objQueryModel = new FileTemplateQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.FileTemplateTypeCodeEqual = FileTemplateTypeCodeEqual;
            objQueryModel.FileTemplateNameEqual = FileTemplateNameEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new FileTemplateBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<FileTemplateModel> GetFileTemplateListOne(int Code)
        {
            List<FileTemplateModel> list = new List<FileTemplateModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    FileTemplateBLL ebll = new FileTemplateBLL();
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

        public static string GetSortCode(string TemplateCode)
        {
            string sortCode = "";
            if (!TemplateCode.Equals(string.Empty))
            {
                FileTemplateBFL ebfl = new FileTemplateBFL();
                sortCode = ebfl.GetFileTemplate(int.Parse(TemplateCode)).SortCode;
            }
            return sortCode;
        }

        public static string GetTemplateName(string TemplateCode)
        {
            string fileTemplateName = "";
            if (TemplateCode != "")
            {
                FileTemplateBFL ebfl = new FileTemplateBFL();
                fileTemplateName = ebfl.GetFileTemplate(int.Parse(TemplateCode)).FileTemplateName;
            }
            return fileTemplateName;
        }

        public int Insert(FileTemplateModel ObjModel)
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
                        num = new FileTemplateBLL().Insert(ObjModel, transaction);
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

        public int Update(FileTemplateModel ObjModel)
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
                        num = new FileTemplateBLL().Update(ObjModel, transaction);
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

