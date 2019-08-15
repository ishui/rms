namespace RmsDM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsDM.DAL;
    using RmsDM.MODEL;

    public class FileTemplateVersionBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Transaction);
            return ndal.Delete(Code);
        }

        public FileTemplateVersionModel GetModel(int Code, SqlConnection Connection)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Connection);
            return ndal.GetModel(Code);
        }

        public FileTemplateVersionModel GetModel(int Code, SqlTransaction Transaction)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Transaction);
            return ndal.GetModel(Code);
        }

        public List<FileTemplateVersionModel> GetModels(SqlConnection Connection)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Connection);
            return ndal.Select();
        }

        public List<FileTemplateVersionModel> GetModels(SqlTransaction Transaction)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Transaction);
            return ndal.Select();
        }

        public List<FileTemplateVersionModel> GetModels(FileTemplateVersionQueryModel ObjQueryModel, SqlConnection Connection)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Connection);
            return ndal.Select(ObjQueryModel);
        }

        public List<FileTemplateVersionModel> GetModels(FileTemplateVersionQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Transaction);
            return ndal.Select(ObjQueryModel);
        }

        public int Insert(FileTemplateVersionModel ObjModel, SqlTransaction Transaction)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Transaction);
            return ndal.Insert(ObjModel);
        }

        public int Update(FileTemplateVersionModel ObjModel, SqlTransaction Transaction)
        {
            FileTemplateVersionDAL ndal = new FileTemplateVersionDAL(Transaction);
            return ndal.Update(ObjModel);
        }
    }
}

