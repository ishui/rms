namespace RmsDM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.DAL;
    using RmsDM.MODEL;

    public class DocumentFileBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Transaction);
            return edal.Delete(Code);
        }

        public DocumentFileModel GetModel(int Code, SqlConnection Connection)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Connection);
            return edal.GetModel(Code);
        }

        public DocumentFileModel GetModel(int Code, SqlTransaction Transaction)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<DocumentFileModel> GetModels(SqlConnection Connection)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Connection);
            return edal.Select();
        }

        public List<DocumentFileModel> GetModels(SqlTransaction Transaction)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Transaction);
            return edal.Select();
        }

        public List<DocumentFileModel> GetModels(DocumentFileQueryModel ObjQueryModel, SqlConnection Connection)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<DocumentFileModel> GetModels(DocumentFileQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(DocumentFileModel ObjModel, SqlTransaction Transaction)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            DocumentFileModel objModel = this.GetModel(Code, Transaction);
            objModel.AuditingState = "申请";
            return this.Update(objModel, Transaction);
        }

        public int Update(DocumentFileModel ObjModel, SqlTransaction Transaction)
        {
            DocumentFileDAL edal = new DocumentFileDAL(Transaction);
            return edal.Update(ObjModel);
        }

        public int UpdateAlreadyAuditing(int AppCode, SqlTransaction Transaction)
        {
            DocumentFileModel objModel = this.GetModel(AppCode, Transaction);
            objModel.AuditingState = "审批中";
            objModel.AuditingDatetime = DateTime.Now;
            return this.Update(objModel, Transaction);
        }

        public int UpdateAuditingAgree(int AppCode, SqlTransaction Transaction)
        {
            DocumentFileModel objModel = this.GetModel(AppCode, Transaction);
            objModel.AuditingState = "同意";
            objModel.AuditingDatetime = DateTime.Now;
            objModel.ArchiveDatetime = DateTime.Now;
            return this.Update(objModel, Transaction);
        }

        public int UpdateAuditingNoAgree(int AppCode, SqlTransaction Transaction)
        {
            DocumentFileModel objModel = this.GetModel(AppCode, Transaction);
            objModel.AuditingState = "否决";
            objModel.AuditingDatetime = DateTime.Now;
            objModel.ArchiveDatetime = DateTime.Now;
            return this.Update(objModel, Transaction);
        }
    }
}

