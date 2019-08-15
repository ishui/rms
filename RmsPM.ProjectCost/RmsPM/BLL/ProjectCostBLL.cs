namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class ProjectCostBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = new ProjectCostDAL(Transaction).Delete(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Delete(ProjectCostModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = this.Delete(ObjModel.ProjectCostCode, Transaction);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public void DeleteAll(SqlTransaction tran)
        {
            try
            {
                int num2 = new SqlDataProcess(tran).RunSql("delete ProjectCost");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public ProjectCostModel GetModel(int Code, SqlConnection Connection)
        {
            ProjectCostModel model;
            try
            {
                model = new ProjectCostDAL(Connection).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public ProjectCostModel GetModel(int Code, SqlTransaction Transaction)
        {
            ProjectCostModel model;
            try
            {
                model = new ProjectCostDAL(Transaction).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public List<ProjectCostModel> GetModels(SqlConnection Connection)
        {
            List<ProjectCostModel> list;
            try
            {
                list = new ProjectCostDAL(Connection).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<ProjectCostModel> GetModels(SqlTransaction Transaction)
        {
            List<ProjectCostModel> list;
            try
            {
                list = new ProjectCostDAL(Transaction).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<ProjectCostModel> GetModels(ProjectCostQueryModel ObjQueryModel, SqlConnection Connection)
        {
            List<ProjectCostModel> list;
            try
            {
                list = new ProjectCostDAL(Connection).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<ProjectCostModel> GetModels(ProjectCostQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            List<ProjectCostModel> list;
            try
            {
                list = new ProjectCostDAL(Transaction).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public int Insert(ProjectCostModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                ProjectCostDAL tdal = new ProjectCostDAL(Transaction);
                ObjModel.InputDate = DateTime.Now;
                num = tdal.Insert(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Update(ProjectCostModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                ProjectCostDAL tdal = new ProjectCostDAL(Transaction);
                ObjModel.InputDate = DateTime.Now;
                num = tdal.Update(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }
    }
}

