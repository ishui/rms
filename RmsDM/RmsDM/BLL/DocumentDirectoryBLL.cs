namespace RmsDM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using RmsDM.DAL;
    using RmsDM.MODEL;

    public class DocumentDirectoryBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Transaction);
            return ydal.Delete(Code);
        }

        public DocumentDirectoryModel GetModel(int Code, SqlConnection Connection)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Connection);
            return ydal.GetModel(Code);
        }

        public DocumentDirectoryModel GetModel(int Code, SqlTransaction Transaction)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Transaction);
            return ydal.GetModel(Code);
        }

        public List<DocumentDirectoryModel> GetModels(SqlConnection Connection)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Connection);
            return ydal.Select();
        }

        public List<DocumentDirectoryModel> GetModels(SqlTransaction Transaction)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Transaction);
            return ydal.Select();
        }

        public List<DocumentDirectoryModel> GetModels(DocumentDirectoryQueryModel ObjQueryModel, SqlConnection Connection)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Connection);
            return ydal.Select(ObjQueryModel);
        }

        public List<DocumentDirectoryModel> GetModels(DocumentDirectoryQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Transaction);
            return ydal.Select(ObjQueryModel);
        }

        public DataSet GetStartupProcedureDS(SqlConnection Connection)
        {
            HandMadeDAL edal = new HandMadeDAL(Connection);
            return edal.GetProcedureDS();
        }

        public int Insert(DocumentDirectoryModel ObjModel, SqlTransaction Transaction)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Transaction);
            return ydal.Insert(ObjModel);
        }

        public int Update(DocumentDirectoryModel ObjModel, SqlTransaction Transaction)
        {
            DocumentDirectoryDAL ydal = new DocumentDirectoryDAL(Transaction);
            return ydal.Update(ObjModel);
        }
    }
}

