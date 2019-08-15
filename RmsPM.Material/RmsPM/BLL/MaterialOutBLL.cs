namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using RmsPM.DAL.EntityDAO;
    using TiannuoPM.MODEL;

    public class MaterialOutBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            int num;
            try
            {
                MaterialOutDAL tdal = new MaterialOutDAL(Transaction);
                MaterialOutDtlDAL ldal = new MaterialOutDtlDAL(Transaction);
                MaterialOutDtlQueryModel qmObj = new MaterialOutDtlQueryModel();
                qmObj.MaterialOutCodeEqual = Code.ToString();
                foreach (MaterialOutDtlModel model2 in ldal.Select(qmObj))
                {
                    ldal.Delete(model2.MaterialOutDtlCode);
                }
                num = tdal.Delete(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Delete(MaterialOutModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                num = this.Delete(ObjModel.MaterialOutCode, Transaction);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static MaterialOutDtlModel FindModel(List<MaterialOutDtlModel> mObjs, string MaterialOutDtlCode)
        {
            foreach (MaterialOutDtlModel model in mObjs)
            {
                if (model.MaterialOutDtlCode.ToString() == MaterialOutDtlCode)
                {
                    return model;
                }
            }
            return null;
        }

        public MaterialOutModel GetModel(int Code, SqlConnection Connection)
        {
            MaterialOutModel model;
            try
            {
                model = new MaterialOutDAL(Connection).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public MaterialOutModel GetModel(int Code, SqlTransaction Transaction)
        {
            MaterialOutModel model;
            try
            {
                model = new MaterialOutDAL(Transaction).GetModel(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        public List<MaterialOutModel> GetModels(SqlConnection Connection)
        {
            List<MaterialOutModel> list;
            try
            {
                list = new MaterialOutDAL(Connection).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutModel> GetModels(SqlTransaction Transaction)
        {
            List<MaterialOutModel> list;
            try
            {
                list = new MaterialOutDAL(Transaction).Select();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutModel> GetModels(MaterialOutQueryModel ObjQueryModel, SqlConnection Connection)
        {
            List<MaterialOutModel> list;
            try
            {
                list = new MaterialOutDAL(Connection).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutModel> GetModels(MaterialOutQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            List<MaterialOutModel> list;
            try
            {
                list = new MaterialOutDAL(Transaction).Select(ObjQueryModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public int Insert(MaterialOutModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                MaterialOutDAL tdal = new MaterialOutDAL(Transaction);
                ObjModel.InputDate = DateTime.Now;
                num = tdal.Insert(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Update(MaterialOutModel ObjModel, SqlTransaction Transaction)
        {
            int num;
            try
            {
                MaterialOutDAL tdal = new MaterialOutDAL(Transaction);
                ObjModel.InputDate = DateTime.Now;
                num = tdal.Update(ObjModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int UpdateMaterialOutDtlList(SqlTransaction Transaction, List<MaterialOutDtlModel> mObjs, int MaterialOutCode)
        {
            try
            {
                MaterialOutDtlDAL ldal = new MaterialOutDtlDAL(Transaction);
                Hashtable hashtable = new Hashtable();
                foreach (MaterialOutDtlModel model in ldal.GetMaterialOutDtlListByMaterialOutCode(MaterialOutCode))
                {
                    if (!hashtable.Contains(model.MaterialInDtlCode))
                    {
                        hashtable.Add(model.MaterialInDtlCode, model.MaterialInDtlCode);
                    }
                    if (FindModel(mObjs, model.MaterialOutDtlCode.ToString()) == null)
                    {
                        ldal.Delete(model.MaterialOutDtlCode);
                    }
                }
                foreach (MaterialOutDtlModel model2 in mObjs)
                {
                    if (!hashtable.Contains(model2.MaterialInDtlCode))
                    {
                        hashtable.Add(model2.MaterialInDtlCode, model2.MaterialInDtlCode);
                    }
                    if (model2.MaterialOutDtlCode <= 0)
                    {
                        model2.MaterialOutDtlCode = int.Parse(SystemManageDAO.GetNewSysCode("MaterialOutDtlCode"));
                        model2.MaterialOutCode = MaterialOutCode;
                        ldal.Insert(model2);
                        continue;
                    }
                    ldal.Update(model2);
                }
                IEnumerator enumerator = hashtable.Keys.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        int current = (int) enumerator.Current;
                        string commandText = "update MaterialInDtl set OutQty = (select sum(isnull(OutQty, 0)) from MaterialOutDtl where MaterialInDtlCode = MaterialInDtl.MaterialInDtlCode)";
                        ldal._DataProcess.RunSql(commandText);
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
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

