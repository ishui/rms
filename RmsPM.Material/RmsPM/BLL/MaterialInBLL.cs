namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using RmsPM.DAL.EntityDAO;
    using TiannuoPM.MODEL;

    public class MaterialInBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            MaterialInDAL ndal = new MaterialInDAL(Transaction);
            MaterialInDtlDAL ldal = new MaterialInDtlDAL(Transaction);
            MaterialInDtlQueryModel qmObj = new MaterialInDtlQueryModel();
            qmObj.MaterialInCodeEqual = Code.ToString();
            foreach (MaterialInDtlModel model2 in ldal.Select(qmObj))
            {
                ldal.Delete(model2.MaterialInDtlCode);
            }
            return ndal.Delete(Code);
        }

        public int Delete(MaterialInModel ObjModel, SqlTransaction Transaction)
        {
            return this.Delete(ObjModel.MaterialInCode, Transaction);
        }

        public static MaterialInDtlModel FindModel(List<MaterialInDtlModel> mObjs, string MaterialInDtlCode)
        {
            foreach (MaterialInDtlModel model in mObjs)
            {
                if (model.MaterialInDtlCode.ToString() == MaterialInDtlCode)
                {
                    return model;
                }
            }
            return null;
        }

        public MaterialInModel GetModel(int Code, SqlConnection Connection)
        {
            MaterialInDAL ndal = new MaterialInDAL(Connection);
            return ndal.GetModel(Code);
        }

        public MaterialInModel GetModel(int Code, SqlTransaction Transaction)
        {
            MaterialInDAL ndal = new MaterialInDAL(Transaction);
            return ndal.GetModel(Code);
        }

        public List<MaterialInModel> GetModels(SqlConnection Connection)
        {
            MaterialInDAL ndal = new MaterialInDAL(Connection);
            return ndal.Select();
        }

        public List<MaterialInModel> GetModels(SqlTransaction Transaction)
        {
            MaterialInDAL ndal = new MaterialInDAL(Transaction);
            return ndal.Select();
        }

        public List<MaterialInModel> GetModels(MaterialInQueryModel ObjQueryModel, SqlConnection Connection)
        {
            MaterialInDAL ndal = new MaterialInDAL(Connection);
            return ndal.Select(ObjQueryModel);
        }

        public List<MaterialInModel> GetModels(MaterialInQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            MaterialInDAL ndal = new MaterialInDAL(Transaction);
            return ndal.Select(ObjQueryModel);
        }

        public int Insert(MaterialInModel ObjModel, SqlTransaction Transaction)
        {
            MaterialInDAL ndal = new MaterialInDAL(Transaction);
            ObjModel.InputDate = DateTime.Now;
            return ndal.Insert(ObjModel);
        }

        public int Update(MaterialInModel ObjModel, SqlTransaction Transaction)
        {
            MaterialInDAL ndal = new MaterialInDAL(Transaction);
            ObjModel.InputDate = DateTime.Now;
            return ndal.Update(ObjModel);
        }

        public int UpdateMaterialInDtlList(SqlTransaction Transaction, List<MaterialInDtlModel> mObjs, int MaterialInCode)
        {
            try
            {
                MaterialInDtlDAL ldal = new MaterialInDtlDAL(Transaction);
                foreach (MaterialInDtlModel model in ldal.GetMaterialInDtlListByMaterialInCode(MaterialInCode))
                {
                    if (FindModel(mObjs, model.MaterialInDtlCode.ToString()) == null)
                    {
                        ldal.Delete(model.MaterialInDtlCode);
                    }
                }
                foreach (MaterialInDtlModel model2 in mObjs)
                {
                    if (model2.MaterialInDtlCode <= 0)
                    {
                        model2.MaterialInDtlCode = int.Parse(SystemManageDAO.GetNewSysCode("MaterialInDtlCode"));
                        model2.MaterialInCode = MaterialInCode;
                        ldal.Insert(model2);
                        continue;
                    }
                    ldal.Update(model2);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return 1;
        }
    }
}

