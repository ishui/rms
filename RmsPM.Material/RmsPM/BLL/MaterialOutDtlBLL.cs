namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class MaterialOutDtlBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = new MaterialOutDtlDAL(Transaction).Delete(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Delete(MaterialOutDtlModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = this.Delete(ObjModel.MaterialOutDtlCode, Transaction);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public MaterialOutDtlModel GetModel(int Code, SqlConnection Connection)
        {
            MaterialOutDtlModel model;
            try
            {
                model = new MaterialOutDtlDAL(Connection).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public MaterialOutDtlModel GetModel(int Code, SqlTransaction Transaction)
        {
            MaterialOutDtlModel model;
            try
            {
                model = new MaterialOutDtlDAL(Transaction).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public List<MaterialOutDtlModel> GetModels(SqlConnection Connection)
        {
            List<MaterialOutDtlModel> list;
            try
            {
                list = new MaterialOutDtlDAL(Connection).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutDtlModel> GetModels(SqlTransaction Transaction)
        {
            List<MaterialOutDtlModel> list;
            try
            {
                list = new MaterialOutDtlDAL(Transaction).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutDtlModel> GetModels(MaterialOutDtlQueryModel ObjQueryModel, SqlConnection Connection)
        {
            List<MaterialOutDtlModel> list;
            try
            {
                list = new MaterialOutDtlDAL(Connection).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutDtlModel> GetModels(MaterialOutDtlQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            List<MaterialOutDtlModel> list;
            try
            {
                list = new MaterialOutDtlDAL(Transaction).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public int Insert(MaterialOutDtlModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = new MaterialOutDtlDAL(Transaction).Insert(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Update(MaterialOutDtlModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = new MaterialOutDtlDAL(Transaction).Update(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }
    }
}

