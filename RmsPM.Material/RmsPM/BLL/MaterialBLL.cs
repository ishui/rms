namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class MaterialBLL
    {
        public void CheckBeforeDelete(int MaterialCode, SqlTransaction tran)
        {
            try
            {
                SqlDataProcess process = new SqlDataProcess(tran);
                string commandText = string.Format("select top 1 * from MaterialInDtl where MaterialCode = '{0}'", MaterialCode);
                if (process.GetDataSet(commandText).Tables[0].Rows.Count > 0)
                {
                    throw new Exception("材料已入库，不能删除");
                }
                commandText = string.Format("select top 1 * from MaterialOutDtl where MaterialCode = '{0}'", MaterialCode);
                if (process.GetDataSet(commandText).Tables[0].Rows.Count > 0)
                {
                    throw new Exception("材料已领用，不能删除");
                }
                commandText = string.Format("select top 1 * from ContractMaterial where MaterialCode = '{0}'", MaterialCode);
                if (process.GetDataSet(commandText).Tables[0].Rows.Count > 0)
                {
                    throw new Exception("材料已制定合同材料需求，不能删除");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int Delete(int Code, SqlTransaction Transaction)
        {
            MaterialDAL ldal = new MaterialDAL(Transaction);
            this.CheckBeforeDelete(Code, Transaction);
            return ldal.Delete(Code);
        }

        public int Delete(MaterialModel MaterialModel, SqlTransaction Transaction)
        {
            return this.Delete(MaterialModel.MaterialCode, Transaction);
        }

        public void DeleteAll(SqlTransaction tran)
        {
            try
            {
                int num = new SqlDataProcess(tran).RunSql("delete Material");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public MaterialModel GetModel(int Code, SqlTransaction Transaction)
        {
            MaterialDAL ldal = new MaterialDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public MaterialModel GetModel(object Code, SqlConnection Connection)
        {
            MaterialDAL ldal = new MaterialDAL(Connection);
            return ldal.GetModel(Code);
        }

        public List<MaterialModel> GetModels(SqlConnection Connection)
        {
            MaterialDAL ldal = new MaterialDAL(Connection);
            return ldal.Select();
        }

        public List<MaterialModel> GetModels(SqlTransaction Transaction)
        {
            MaterialDAL ldal = new MaterialDAL(Transaction);
            return ldal.Select();
        }

        public List<MaterialModel> GetModels(MaterialQueryModel ObjQueryModel, SqlConnection Connection)
        {
            MaterialDAL ldal = new MaterialDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<MaterialModel> GetModels(MaterialQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            MaterialDAL ldal = new MaterialDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(MaterialModel ObjModel, SqlTransaction Transaction)
        {
            MaterialDAL ldal = new MaterialDAL(Transaction);
            ObjModel.InputDate = DateTime.Now;
            return ldal.Insert(ObjModel);
        }

        public int Update(MaterialModel ObjModel, SqlTransaction Transaction)
        {
            MaterialDAL ldal = new MaterialDAL(Transaction);
            ObjModel.InputDate = DateTime.Now;
            return ldal.Update(ObjModel);
        }
    }
}

