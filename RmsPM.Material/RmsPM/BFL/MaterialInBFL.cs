namespace RmsPM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.BLL;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class MaterialInBFL
    {
        public int Delete(MaterialInModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialInBLL().Delete(ObjModel, transaction);
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

        public MaterialInModel GetMaterialIn(int Code)
        {
            MaterialInModel model = new MaterialInModel();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                model = new MaterialInBLL().GetModel(Code, connection);
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

        public static MaterialInDtlModel GetMaterialInDtl(int Code)
        {
            MaterialInDtlModel model;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                model = new MaterialInDtlBLL().GetModel(Code, connection);
            }
            catch (Exception exception)
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

        public List<MaterialInDtlModel> GetMaterialInDtlList(int MaterialInCode)
        {
            List<MaterialInDtlModel> materialInDtlListByMaterialInCode;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    materialInDtlListByMaterialInCode = new MaterialInDtlDAL(connection).GetMaterialInDtlListByMaterialInCode(MaterialInCode);
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
            return materialInDtlListByMaterialInCode;
        }

        public static List<MaterialInDtlModel> GetMaterialInDtlListByContract(string ContractCode)
        {
            List<MaterialInDtlModel> materialInDtlListByContract;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    materialInDtlListByContract = new MaterialInDtlDAL(connection).GetMaterialInDtlListByContract(ContractCode);
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
            return materialInDtlListByContract;
        }

        public static List<MaterialInDtlModel> GetMaterialInDtlListByContract(string ContractCode, int MaterialCode)
        {
            List<MaterialInDtlModel> materialInDtlListByContract;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    materialInDtlListByContract = new MaterialInDtlDAL(connection).GetMaterialInDtlListByContract(ContractCode, MaterialCode);
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
            return materialInDtlListByContract;
        }

        public List<MaterialInModel> GetMaterialInList(MaterialInQueryModel QueryModel)
        {
            List<MaterialInModel> models = new List<MaterialInModel>();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                if (QueryModel == null)
                {
                    QueryModel = new MaterialInQueryModel();
                }
                models = new MaterialInBLL().GetModels(QueryModel, connection);
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

        public List<MaterialInModel> GetMaterialInList(string SortColumns, int StartRecord, int MaxRecords, string AccessRange, string MaterialInCodeEqual, string MaterialInIDEqual, string ProjectCodeEqual, string GroupCodeEqual, string InDateRange1, string InDateRange2, string InPersonEqual, string StatusEqual, string InputPersonEqual, string InputDateRange1, string InputDateRange2, string CheckPersonEqual, string CheckDateRange1, string CheckDateRange2, string ContractCodeEqual, string RemarkEqual)
        {
            List<MaterialInModel> models = new List<MaterialInModel>();
            MaterialInQueryModel objQueryModel = new MaterialInQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.AccessRange = AccessRange;
            objQueryModel.MaterialInCodeEqual = MaterialInCodeEqual;
            objQueryModel.MaterialInIDEqual = MaterialInIDEqual;
            objQueryModel.ProjectCodeEqual = ProjectCodeEqual;
            objQueryModel.GroupCodeEqual = GroupCodeEqual;
            objQueryModel.InDateRange1 = InDateRange1;
            objQueryModel.InDateRange2 = InDateRange2;
            objQueryModel.InPersonEqual = InPersonEqual;
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
                models = new MaterialInBLL().GetModels(objQueryModel, connection);
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

        public List<MaterialInModel> GetMaterialInListOne(int Code)
        {
            List<MaterialInModel> list = new List<MaterialInModel>();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                MaterialInBLL nbll = new MaterialInBLL();
                list.Add(nbll.GetModel(Code, connection));
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

        public static decimal GetMaterialInQtyByContract(string ContractCode, int MaterialCode)
        {
            decimal num2;
            try
            {
                List<MaterialInDtlModel> materialInDtlListByContract = GetMaterialInDtlListByContract(ContractCode, MaterialCode);
                decimal num = 0M;
                foreach (MaterialInDtlModel model in materialInDtlListByContract)
                {
                    num += model.InQty;
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public int Insert(MaterialInModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialInBLL().Insert(ObjModel, transaction);
                    transaction.Commit();
                    return num;
                }
                catch (Exception exception)
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

        public List<MaterialInDtlModel> SelectMaterialInDtlList(string SortColumns, int StartRecord, int MaxRecords, string AccessRange, string MaterialInDtlCodeEqual, string MaterialInCodeEqual, string MaterialCodeEqual, string InQtyEqual, string InPriceEqual, string InMoneyEqual, string OutQtyEqual, string MaterialNameEqual, string SpecEqual, string UnitEqual, string GroupCodeEqual, string GroupNameEqual, string GroupFullIDEqual, string GroupSortIDEqual, string InvQtyEqual, string InvMoneyEqual, string MaterialInIDEqual, string InDateRange1, string InDateRange2, string ContractCodeEqual, string InGroupCodeEqual, string InGroupNameEqual, string ProjectCodeEqual)
        {
            List<MaterialInDtlModel> models;
            MaterialInDtlQueryModel objQueryModel = new MaterialInDtlQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.AccessRange = AccessRange;
            objQueryModel.MaterialInDtlCodeEqual = MaterialInDtlCodeEqual;
            objQueryModel.MaterialInCodeEqual = MaterialInCodeEqual;
            objQueryModel.MaterialCodeEqual = MaterialCodeEqual;
            objQueryModel.InQtyEqual = InQtyEqual;
            objQueryModel.InPriceEqual = InPriceEqual;
            objQueryModel.InMoneyEqual = InMoneyEqual;
            objQueryModel.OutQtyEqual = OutQtyEqual;
            objQueryModel.MaterialNameEqual = MaterialNameEqual;
            objQueryModel.SpecEqual = SpecEqual;
            objQueryModel.UnitEqual = UnitEqual;
            objQueryModel.GroupCodeEqual = GroupCodeEqual;
            objQueryModel.GroupNameEqual = GroupNameEqual;
            objQueryModel.GroupFullIDEqual = GroupFullIDEqual;
            objQueryModel.GroupSortIDEqual = GroupSortIDEqual;
            objQueryModel.InvQtyEqual = InvQtyEqual;
            objQueryModel.InvMoneyEqual = InvMoneyEqual;
            objQueryModel.MaterialInIDEqual = MaterialInIDEqual;
            objQueryModel.InDateRange1 = InDateRange1;
            objQueryModel.InDateRange2 = InDateRange2;
            objQueryModel.ContractCodeEqual = ContractCodeEqual;
            objQueryModel.InGroupCodeEqual = InGroupCodeEqual;
            objQueryModel.InGroupNameEqual = InGroupNameEqual;
            objQueryModel.ProjectCodeEqual = ProjectCodeEqual;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                models = new MaterialInDtlBLL().GetModels(objQueryModel, connection);
            }
            catch (Exception exception)
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

        public int Update(MaterialInModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialInBLL().Update(ObjModel, transaction);
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

        public int UpdateMaterialInDtlList(List<MaterialInDtlModel> ObjModelDtls, int MaterialInCode)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialInBLL().UpdateMaterialInDtlList(transaction, ObjModelDtls, MaterialInCode);
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

