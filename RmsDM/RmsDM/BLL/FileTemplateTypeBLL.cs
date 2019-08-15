namespace RmsDM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.DAL;
    using RmsDM.MODEL;

    public class FileTemplateTypeBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Transaction);
            return edal.Delete(Code);
        }

        public FileTemplateTypeModel GetModel(int Code, SqlConnection Connection)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Connection);
            return edal.GetModel(Code);
        }

        public FileTemplateTypeModel GetModel(int Code, SqlTransaction Transaction)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Transaction);
            return edal.GetModel(Code);
        }

        public List<FileTemplateTypeModel> GetModels(SqlConnection Connection)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Connection);
            return edal.Select();
        }

        public List<FileTemplateTypeModel> GetModels(SqlTransaction Transaction)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Transaction);
            return edal.Select();
        }

        public List<FileTemplateTypeModel> GetModels(FileTemplateTypeQueryModel ObjQueryModel, SqlConnection Connection)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Connection);
            return edal.Select(ObjQueryModel);
        }

        public List<FileTemplateTypeModel> GetModels(FileTemplateTypeQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Transaction);
            return edal.Select(ObjQueryModel);
        }

        public int Insert(FileTemplateTypeModel ObjModel, SqlTransaction Transaction)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Transaction);
            return edal.Insert(ObjModel);
        }

        public int Update(FileTemplateTypeModel ObjModel, SqlTransaction Transaction)
        {
            FileTemplateTypeDAL edal = new FileTemplateTypeDAL(Transaction);
            return edal.Update(ObjModel);
        }
    }
}

