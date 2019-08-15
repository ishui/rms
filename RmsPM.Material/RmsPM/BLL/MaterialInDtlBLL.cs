namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class MaterialInDtlBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = new MaterialInDtlDAL(Transaction).Delete(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Delete(MaterialInDtlModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = this.Delete(ObjModel.MaterialInDtlCode, Transaction);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public MaterialInDtlModel GetModel(int Code, SqlConnection Connection)
        {
            MaterialInDtlModel model;
            try
            {
                model = new MaterialInDtlDAL(Connection).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public MaterialInDtlModel GetModel(int Code, SqlTransaction Transaction)
        {
            MaterialInDtlModel model;
            try
            {
                model = new MaterialInDtlDAL(Transaction).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public List<MaterialInDtlModel> GetModels(SqlConnection Connection)
        {
            List<MaterialInDtlModel> list;
            try
            {
                list = new MaterialInDtlDAL(Connection).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialInDtlModel> GetModels(SqlTransaction Transaction)
        {
            List<MaterialInDtlModel> list;
            try
            {
                list = new MaterialInDtlDAL(Transaction).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialInDtlModel> GetModels(MaterialInDtlQueryModel ObjQueryModel, SqlConnection Connection)
        {
            List<MaterialInDtlModel> list;
            try
            {
                list = new MaterialInDtlDAL(Connection).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialInDtlModel> GetModels(MaterialInDtlQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            List<MaterialInDtlModel> list;
            try
            {
                list = new MaterialInDtlDAL(Transaction).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public int Insert(MaterialInDtlModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = new MaterialInDtlDAL(Transaction).Insert(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Update(MaterialInDtlModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = new MaterialInDtlDAL(Transaction).Update(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }
    }
}

