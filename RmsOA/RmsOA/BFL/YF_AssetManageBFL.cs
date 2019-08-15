namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class YF_AssetManageBFL
    {
        public int Delete(YF_AssetManageModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().Delete(ObjModel.Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public static string GetManpowerNeedStatusName(string str)
        {
            switch (str)
            {
                case "0":
                    return "申请";

                case "1":
                    return "审核中";

                case "2":
                    return "通过";

                case "3":
                    return "未通过";

                case "4":
                    return "作废";
            }
            return "";
        }

        public YF_AssetManageModel GetYF_AssetManage(int Code)
        {
            YF_AssetManageModel model = new YF_AssetManageModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new YF_AssetManageBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<YF_AssetManageModel> GetYF_AssetManageList(YF_AssetManageQueryModel QueryModel)
        {
            List<YF_AssetManageModel> models = new List<YF_AssetManageModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new YF_AssetManageQueryModel();
                    }
                    models = new YF_AssetManageBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<YF_AssetManageModel> GetYF_AssetManageList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string FacilityNameEqual, string AreaEqual, string ProducerEqual, string BuyCorpEqual, string ModelNOEqual, string LayCorpEqual, string TypeEqual, string CountsEqual, string UseDeptEqual, string LayPlaceEqual, string CountUnitEqual, string ProdAreaEqual, string SortNOEqual, string SortTypeEqual, string FreeMainEqual, string EquiTypeEqual, string ProviderEqual, DateTime BuyDateEqual, decimal PriceEqual, string RegisterEqual, string MainCardPlaceEqual, string StoreStatusEqual, string RemarkEqual, string StatusEqual, string DeptUnitEqual, DateTime BookINTimeEqual, string BuyTypeEqual, string CodeNOEqual, DateTime StartTimeEqual, DateTime EndTimeEqual)
        {
            List<YF_AssetManageModel> models = new List<YF_AssetManageModel>();
            YF_AssetManageQueryModel objQueryModel = new YF_AssetManageQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.FacilityNameEqual = FacilityNameEqual;
            objQueryModel.AreaEqual = AreaEqual;
            objQueryModel.ProducerEqual = ProducerEqual;
            objQueryModel.BuyCorpEqual = BuyCorpEqual;
            objQueryModel.ModelNOEqual = ModelNOEqual;
            objQueryModel.LayCorpEqual = LayCorpEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.CountsEqual = CountsEqual;
            objQueryModel.UseDeptEqual = UseDeptEqual;
            objQueryModel.LayPlaceEqual = LayPlaceEqual;
            objQueryModel.CountUnitEqual = CountUnitEqual;
            objQueryModel.ProdAreaEqual = ProdAreaEqual;
            objQueryModel.SortNOEqual = SortNOEqual;
            objQueryModel.SortTypeEqual = SortTypeEqual;
            objQueryModel.FreeMainEqual = FreeMainEqual;
            objQueryModel.EquiTypeEqual = EquiTypeEqual;
            objQueryModel.ProviderEqual = ProviderEqual;
            objQueryModel.BuyDateEqual = BuyDateEqual;
            objQueryModel.PriceEqual = PriceEqual;
            objQueryModel.RegisterEqual = RegisterEqual;
            objQueryModel.MainCardPlaceEqual = MainCardPlaceEqual;
            objQueryModel.StoreStatusEqual = StoreStatusEqual;
            objQueryModel.RemarkEqual = RemarkEqual;
            objQueryModel.StatusEqual = StatusEqual;
            objQueryModel.DeptUnitEqual = DeptUnitEqual;
            objQueryModel.BookINTimeEqual = BookINTimeEqual;
            objQueryModel.BuyTypeEqual = BuyTypeEqual;
            objQueryModel.CodeNOEqual = CodeNOEqual;
            objQueryModel.QueryStartTimeEqual = StartTimeEqual;
            objQueryModel.QueryEndTimeEqual = EndTimeEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new YF_AssetManageBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<YF_AssetManageModel> GetYF_AssetManageListOne(int Code)
        {
            List<YF_AssetManageModel> list = new List<YF_AssetManageModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    YF_AssetManageBLL ebll = new YF_AssetManageBLL();
                    list.Add(ebll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(YF_AssetManageModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().Insert(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int ModifyAlreadyAuditing(int Code)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().ModifyAlreadyAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int ModifyBankOutAuditing(int Code)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().ModifyBankOutAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int ModifyNotAuditing(int Code)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().ModifyNotAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int ModifyNotPassAuditing(int Code)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().ModifyNotPassAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int ModifyPassAuditing(int Code)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().ModifyPassAuditing(Code, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public int Update(YF_AssetManageModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        num = new YF_AssetManageBLL().Update(ObjModel, transaction);
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw exception;
                    }
                    return num;
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

