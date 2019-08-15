namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_MaterialTransferBFL
    {
        public int Delete(GK_OA_MaterialTransferModel ObjModel)
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
                        num = new GK_OA_MaterialTransferBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_MaterialTransferModel GetGK_OA_MaterialTransfer(int Code)
        {
            GK_OA_MaterialTransferModel model = new GK_OA_MaterialTransferModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_MaterialTransferBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_MaterialTransferModel> GetGK_OA_MaterialTransferList(GK_OA_MaterialTransferQueryModel QueryModel)
        {
            List<GK_OA_MaterialTransferModel> models = new List<GK_OA_MaterialTransferModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_MaterialTransferQueryModel();
                    }
                    models = new GK_OA_MaterialTransferBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_MaterialTransferModel> GetGK_OA_MaterialTransferList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string NameEqual, string TypeEqual, string NumberEqual, string NumUnitEqual, string ReasonEqual, decimal OriginalPriceEqual, string PreUserEqual, string PreDeptEqual, string LaterUserEqual, string LaterDeptEqual, string TransferHanderEqual, string ReciveHanderEqual, string TransferMasterEqual, string ReciveMasterEqual, DateTime TransferDateEqual, DateTime ReciveDateEqual, string QualityNOEqual, string SNRuleEqual, string StatusEqual, DateTime StartDate, DateTime EndDate)
        {
            List<GK_OA_MaterialTransferModel> models = new List<GK_OA_MaterialTransferModel>();
            GK_OA_MaterialTransferQueryModel objQueryModel = new GK_OA_MaterialTransferQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.NameEqual = NameEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.NumberEqual = NumberEqual;
            objQueryModel.NumUnitEqual = NumUnitEqual;
            objQueryModel.ReasonEqual = ReasonEqual;
            objQueryModel.OriginalPriceEqual = OriginalPriceEqual;
            objQueryModel.PreUserEqual = PreUserEqual;
            objQueryModel.PreDeptEqual = PreDeptEqual;
            objQueryModel.LaterUserEqual = LaterUserEqual;
            objQueryModel.LaterDeptEqual = LaterDeptEqual;
            objQueryModel.TransferHanderEqual = TransferHanderEqual;
            objQueryModel.ReciveHanderEqual = ReciveHanderEqual;
            objQueryModel.TransferMasterEqual = TransferMasterEqual;
            objQueryModel.ReciveMasterEqual = ReciveMasterEqual;
            objQueryModel.TransferDateEqual = TransferDateEqual;
            objQueryModel.ReciveDateEqual = ReciveDateEqual;
            objQueryModel.QualityNOEqual = QualityNOEqual;
            objQueryModel.SNRuleEqual = SNRuleEqual;
            objQueryModel.StatusEqual = StatusEqual;
            objQueryModel.StartDateEqual = StartDate;
            objQueryModel.EndDateEqual = EndDate;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_MaterialTransferBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_MaterialTransferModel> GetGK_OA_MaterialTransferListOne(int Code)
        {
            List<GK_OA_MaterialTransferModel> list = new List<GK_OA_MaterialTransferModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_MaterialTransferBLL rbll = new GK_OA_MaterialTransferBLL();
                    list.Add(rbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
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

        public string[] GetMaterialType()
        {
            return new string[] { "|--请选择--|", "固定资产", "低耗品" };
        }

        public int Insert(GK_OA_MaterialTransferModel ObjModel)
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
                        num = new GK_OA_MaterialTransferBLL().Insert(ObjModel, transaction);
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
                        num = new GK_OA_MaterialTransferBLL().ModifyAlreadyAuditing(Code, transaction);
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
                        num = new GK_OA_MaterialTransferBLL().ModifyBankOutAuditing(Code, transaction);
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
                        num = new GK_OA_MaterialTransferBLL().ModifyNotAuditing(Code, transaction);
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
                        num = new GK_OA_MaterialTransferBLL().ModifyNotPassAuditing(Code, transaction);
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
                        num = new GK_OA_MaterialTransferBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(GK_OA_MaterialTransferModel ObjModel)
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
                        num = new GK_OA_MaterialTransferBLL().Update(ObjModel, transaction);
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

