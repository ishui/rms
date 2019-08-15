namespace RmsPM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.BLL;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class MaterialOutBFL
    {
        public int Delete(MaterialOutModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialOutBLL().Delete(ObjModel, transaction);
                    transaction.Commit();
                    return num;
                }
                catch (SqlException exception)
                {
                    transaction.Rollback();
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public MaterialOutModel GetMaterialOut(int Code)
        {
            MaterialOutModel model = new MaterialOutModel();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                model = new MaterialOutBLL().GetModel(Code, connection);
                connection.Close();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return model;
        }

        public List<MaterialOutDtlModel> GetMaterialOutDtlList(int MaterialOutCode)
        {
            List<MaterialOutDtlModel> materialOutDtlListByMaterialOutCode;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    materialOutDtlListByMaterialOutCode = new MaterialOutDtlDAL(connection).GetMaterialOutDtlListByMaterialOutCode(MaterialOutCode);
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return materialOutDtlListByMaterialOutCode;
        }

        public static List<MaterialOutDtlModel> GetMaterialOutDtlListByContract(string ContractCode)
        {
            List<MaterialOutDtlModel> materialOutDtlListByContract;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    materialOutDtlListByContract = new MaterialOutDtlDAL(connection).GetMaterialOutDtlListByContract(ContractCode);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return materialOutDtlListByContract;
        }

        public static List<MaterialOutDtlModel> GetMaterialOutDtlListByContract(string ContractCode, int MaterialCode)
        {
            List<MaterialOutDtlModel> materialOutDtlListByContract;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    materialOutDtlListByContract = new MaterialOutDtlDAL(connection).GetMaterialOutDtlListByContract(ContractCode, MaterialCode);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return materialOutDtlListByContract;
        }

        public List<MaterialOutDtlModel> GetMaterialOutDtlListByMaterialInCode(int MaterialInCode)
        {
            List<MaterialOutDtlModel> materialOutDtlListByMaterialInCode;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    materialOutDtlListByMaterialInCode = new MaterialOutDtlDAL(connection).GetMaterialOutDtlListByMaterialInCode(MaterialInCode);
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return materialOutDtlListByMaterialInCode;
        }

        public List<MaterialOutModel> GetMaterialOutList(MaterialOutQueryModel QueryModel)
        {
            List<MaterialOutModel> models = new List<MaterialOutModel>();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                if (QueryModel == null)
                {
                    QueryModel = new MaterialOutQueryModel();
                }
                models = new MaterialOutBLL().GetModels(QueryModel, connection);
                connection.Close();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return models;
        }

        public List<MaterialOutModel> GetMaterialOutList(string SortColumns, int StartRecord, int MaxRecords, string AccessRange, string MaterialOutCodeEqual, string MaterialOutIDEqual, string ProjectCodeEqual, string GroupCodeEqual, string OutDateRange1, string OutDateRange2, string OutPersonEqual, string StatusEqual, string InputPersonEqual, string InputDateRange1, string InputDateRange2, string CheckPersonEqual, string CheckDateRange1, string CheckDateRange2, string ContractCodeEqual, string RemarkEqual)
        {
            List<MaterialOutModel> models = new List<MaterialOutModel>();
            MaterialOutQueryModel objQueryModel = new MaterialOutQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.AccessRange = AccessRange;
            objQueryModel.MaterialOutCodeEqual = MaterialOutCodeEqual;
            objQueryModel.MaterialOutIDEqual = MaterialOutIDEqual;
            objQueryModel.ProjectCodeEqual = ProjectCodeEqual;
            objQueryModel.GroupCodeEqual = GroupCodeEqual;
            objQueryModel.OutDateRange1 = OutDateRange1;
            objQueryModel.OutDateRange2 = OutDateRange2;
            objQueryModel.OutPersonEqual = OutPersonEqual;
            objQueryModel.StatusEqual = StatusEqual;
            objQueryModel.InputPersonEqual = InputPersonEqual;
            objQueryModel.InputDateRange1 = InputDateRange1;
            objQueryModel.InputDateRange2 = InputDateRange2;
            objQueryModel.CheckPersonEqual = CheckPersonEqual;
            objQueryModel.CheckDateRange1 = CheckDateRange1;
            objQueryModel.CheckDateRange2 = CheckDateRange2;
            objQueryModel.ContractCodeEqual = ContractCodeEqual;
            objQueryModel.RemarkEqual = RemarkEqual;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                models = new MaterialOutBLL().GetModels(objQueryModel, connection);
                connection.Close();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return models;
        }

        public List<MaterialOutModel> GetMaterialOutListOne(int Code)
        {
            List<MaterialOutModel> list = new List<MaterialOutModel>();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                MaterialOutBLL tbll = new MaterialOutBLL();
                list.Add(tbll.GetModel(Code, connection));
                connection.Close();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return list;
        }

        public static decimal GetMaterialOutQtyByContract(string ContractCode, int MaterialCode)
        {
            decimal num2;
            try
            {
                List<MaterialOutDtlModel> materialOutDtlListByContract = GetMaterialOutDtlListByContract(ContractCode, MaterialCode);
                decimal num = 0M;
                foreach (MaterialOutDtlModel model in materialOutDtlListByContract)
                {
                    num += model.OutQty;
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public int Insert(MaterialOutModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialOutBLL().Insert(ObjModel, transaction);
                    transaction.Commit();
                    return num;
                }
                catch (SqlException exception)
                {
                    transaction.Rollback();
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int Update(MaterialOutModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialOutBLL().Update(ObjModel, transaction);
                    transaction.Commit();
                    return num;
                }
                catch (SqlException exception)
                {
                    transaction.Rollback();
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int UpdateMaterialOutDtlList(List<MaterialOutDtlModel> ObjModelDtls, int MaterialOutCode)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialOutBLL().UpdateMaterialOutDtlList(transaction, ObjModelDtls, MaterialOutCode);
                    transaction.Commit();
                    return num;
                }
                catch (SqlException exception)
                {
                    transaction.Rollback();
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }
    }
}

