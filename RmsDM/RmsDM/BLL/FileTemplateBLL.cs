namespace RmsDM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.DAL;
    using RmsDM.MODEL;

    public class FileTemplateBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Transaction);
            return edal.Delete(Code);
        }

        public FileTemplateModel GetModel(int Code, SqlConnection Connection)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Connection);
            return edal.GetModel(Code);
        }

        public FileTemplateModel GetModel(int Code, SqlTransaction Transaction)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<FileTemplateModel> GetModels(SqlConnection Connection)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Connection);
            return edal.Select();
        }

        public List<FileTemplateModel> GetModels(SqlTransaction Transaction)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Transaction);
            return edal.Select();
        }

        public List<FileTemplateModel> GetModels(FileTemplateQueryModel ObjQueryModel, SqlConnection Connection)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<FileTemplateModel> GetModels(FileTemplateQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(FileTemplateModel ObjModel, SqlTransaction Transaction)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(FileTemplateModel ObjModel, SqlTransaction Transaction)
        {
            FileTemplateDAL edal = new FileTemplateDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

