namespace RmsDM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.BLL;
    using RmsDM.MODEL;

    public class DocumentFileBFL
    {
        public int AlreadyAuditing(int AppCode)
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
                        num = new DocumentFileBLL().UpdateAlreadyAuditing(AppCode, transaction);
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

        public int AuditingAgree(int AppCode)
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
                        num = new DocumentFileBLL().UpdateAuditingAgree(AppCode, transaction);
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

        public int AuditingNoAgree(int AppCode)
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
                        num = new DocumentFileBLL().UpdateAuditingNoAgree(AppCode, transaction);
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

        public int Delete(DocumentFileModel ObjModel)
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
                        num = new DocumentFileBLL().Delete(ObjModel.Code, transaction);
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

        public DocumentFileModel GetDocumentFile(int Code)
        {
            DocumentFileModel model = new DocumentFileModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new DocumentFileBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<DocumentFileModel> GetDocumentFileList(DocumentFileQueryModel QueryModel)
        {
            List<DocumentFileModel> models = new List<DocumentFileModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new DocumentFileQueryModel();
                    }
                    models = new DocumentFileBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DocumentFileModel> GetDocumentFileList(string SortColumns, int StartRecord, int MaxRecords, string NodeCode)
        {
            DocumentDirectoryBFL ybfl = new DocumentDirectoryBFL();
            DocumentDirectoryQueryModel queryModel = new DocumentDirectoryQueryModel();
            DocumentDirectoryModel model2 = new DocumentDirectoryModel();
            queryModel.CodeEqual = int.Parse(NodeCode);
            List<DocumentDirectoryModel> documentDirectoryList = ybfl.GetDocumentDirectoryList(queryModel);
            string departmentCode = "";
            int fileTemplateCode = 0;
            if (documentDirectoryList.Count > 0)
            {
                departmentCode = documentDirectoryList[0].DepartmentCode;
                fileTemplateCode = documentDirectoryList[0].FileTemplateCode;
            }
            DocumentFileQueryModel model3 = new DocumentFileQueryModel();
            model3.FileTemplateCodeEqual = fileTemplateCode;
            model3.ApplyDepartmentCodeEqual = departmentCode;
            model3.SortColumns = SortColumns;
            model3.StartRecord = StartRecord;
            model3.MaxRecords = MaxRecords;
            return this.GetDocumentFileList(model3);
        }

        public List<DocumentFileModel> GetDocumentFileList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string OperationTypeEqual, string ApplyUserCodeEqual, string ApplyDepartmentCodeEqual, DateTime ApplyDateTimeEqual, string FileCodeEqual, int FileTemplateCodeEqual, string FileTemplateCodeStrEqual, string SubjectEqual, string ContentEqual, string RemarkEqual, string ArchiveTypeEqual, string ArchiveStateEqual, DateTime ArchiveDatetimeEqual, string AuditingStateEqual, DateTime AuditingDatetimeEqual, DateTime CreateDateEqual, string CreateUserCodeEqual, DateTime LastModifyDatetimeEqual, string LastModifyByUserCodeEqual, string DeleteFlagEqual, int CountsEqual, int LeavesEqual)
        {
            List<DocumentFileModel> models = new List<DocumentFileModel>();
            DocumentFileQueryModel objQueryModel = new DocumentFileQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.OperationTypeEqual = OperationTypeEqual;
            objQueryModel.ApplyUserCodeEqual = ApplyUserCodeEqual;
            objQueryModel.ApplyDepartmentCodeEqual = ApplyDepartmentCodeEqual;
            objQueryModel.ApplyDateTimeEqual = ApplyDateTimeEqual;
            objQueryModel.FileCodeEqul = FileCodeEqual;
            objQueryModel.FileTemplateCodeEqual = FileTemplateCodeEqual;
            objQueryModel.FileTemplateCodeIn = FileTemplateCodeStrEqual;
            objQueryModel.SubjectEqual = SubjectEqual;
            objQueryModel.ContentEqual = ContentEqual;
            objQueryModel.RemarkEqual = RemarkEqual;
            objQueryModel.ArchiveTypeEqual = ArchiveTypeEqual;
            objQueryModel.ArchiveStateEqual = ArchiveStateEqual;
            objQueryModel.AuditingStateEqual = AuditingStateEqual;
            objQueryModel.AuditingDatetimeEqual = AuditingDatetimeEqual;
            objQueryModel.CreateDateEqual = CreateDateEqual;
            objQueryModel.CreateUserCodeEqual = CreateUserCodeEqual;
            objQueryModel.LastModifyDatetimeEqual = LastModifyDatetimeEqual;
            objQueryModel.LastModifyByUserCodeEqual = LastModifyByUserCodeEqual;
            objQueryModel.DeleteFlagEqual = DeleteFlagEqual;
            objQueryModel.CountsEqual = CountsEqual;
            objQueryModel.LeavesEqual = LeavesEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new DocumentFileBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DocumentFileModel> GetDocumentFileListByCondition(string SortColumns, int StartRecord, int MaxRecords, string NodeCode, string FileCodeEqual, string DoucmentMarkingSNEqual, string SubjectEqual, DateTime ArchiveDateTimeEqualStart, DateTime ArchiveDateTimeEqualEnd)
        {
            DocumentDirectoryBFL ybfl = new DocumentDirectoryBFL();
            DocumentDirectoryQueryModel queryModel = new DocumentDirectoryQueryModel();
            DocumentDirectoryModel model2 = new DocumentDirectoryModel();
            queryModel.CodeEqual = int.Parse(NodeCode);
            List<DocumentDirectoryModel> documentDirectoryList = ybfl.GetDocumentDirectoryList(queryModel);
            string departmentCode = "";
            int fileTemplateCode = 0;
            if (documentDirectoryList.Count > 0)
            {
                departmentCode = documentDirectoryList[0].DepartmentCode;
                fileTemplateCode = documentDirectoryList[0].FileTemplateCode;
            }
            DocumentFileQueryModel model3 = new DocumentFileQueryModel();
            model3.FileTemplateCodeEqual = fileTemplateCode;
            model3.ApplyDepartmentCodeEqual = departmentCode;
            model3.SortColumns = SortColumns;
            model3.StartRecord = StartRecord;
            model3.MaxRecords = MaxRecords;
            model3.DoucmentMarkingSNEqual = DoucmentMarkingSNEqual;
            model3.SubjectEqual = SubjectEqual;
            model3.FileCodeEqul = FileCodeEqual;
            model3.ArchiveDatetimeEqualStart = ArchiveDateTimeEqualStart;
            model3.ArchiveDatetimeEqualEnd = ArchiveDateTimeEqualEnd;
            return this.GetDocumentFileList(model3);
        }

        public List<DocumentFileModel> GetDocumentFileListOne(int Code)
        {
            List<DocumentFileModel> list = new List<DocumentFileModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    DocumentFileBLL ebll = new DocumentFileBLL();
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

        public int Insert(DocumentFileModel ObjModel)
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
                        num = new DocumentFileBLL().Insert(ObjModel, transaction);
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
                        num = new DocumentFileBLL().ModifyNotAuditing(Code, transaction);
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

        public int Update(DocumentFileModel ObjModel)
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
                        num = new DocumentFileBLL().Update(ObjModel, transaction);
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

