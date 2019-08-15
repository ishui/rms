namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Rms.ORMap;
    using RmsOA.BLL;
    using RmsOA.MODEL;
    using RmsPM.DAL.EntityDAO;

    public class GK_OA_AssetTransferBFL
    {
        public int Delete(GK_OA_AssetTransferModel ObjModel)
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
                        num = new GK_OA_AssetTransferBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_AssetTransferModel GetGK_OA_AssetTransfer(int Code)
        {
            GK_OA_AssetTransferModel model = new GK_OA_AssetTransferModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_AssetTransferBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_AssetTransferModel> GetGK_OA_AssetTransferList(GK_OA_AssetTransferQueryModel QueryModel)
        {
            List<GK_OA_AssetTransferModel> models = new List<GK_OA_AssetTransferModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_AssetTransferQueryModel();
                    }
                    models = new GK_OA_AssetTransferBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_AssetTransferModel> GetGK_OA_AssetTransferList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string NameEqual, string SortEqual, string NumberEqual, string NumUnitEqual, string ReasonEqual, string PreUserEqual, string PreDeptEqual, string PostUserEqual, string PostDeptEqual, string QualityNOEqual, string SNRuleEqual, string SubmiterEqual, DateTime SubTimeEqual, string StatusEqual, decimal OriginalPriceEqual, DateTime StartEqual, DateTime EndEqual)
        {
            List<GK_OA_AssetTransferModel> models = new List<GK_OA_AssetTransferModel>();
            GK_OA_AssetTransferQueryModel objQueryModel = new GK_OA_AssetTransferQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.NameEqual = NameEqual;
            objQueryModel.SortEqual = SortEqual;
            objQueryModel.NumberEqual = NumberEqual;
            objQueryModel.NumUnitEqual = NumUnitEqual;
            objQueryModel.ReasonEqual = ReasonEqual;
            objQueryModel.PreUserEqual = PreUserEqual;
            objQueryModel.PreDeptEqual = PreDeptEqual;
            objQueryModel.PostUserEqual = PostUserEqual;
            objQueryModel.PostDeptEqual = PostDeptEqual;
            objQueryModel.QualityNOEqual = QualityNOEqual;
            objQueryModel.SNRuleEqual = SNRuleEqual;
            objQueryModel.SubmiterEqual = SubmiterEqual;
            objQueryModel.SubTimeEqual = SubTimeEqual;
            objQueryModel.StatusEqual = StatusEqual;
            objQueryModel.OriginalPriceEqual = OriginalPriceEqual;
            objQueryModel.StartSubTimeEqual = StartEqual;
            objQueryModel.EndSubTimeEqual = EndEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_AssetTransferBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_AssetTransferModel> GetGK_OA_AssetTransferListOne(int Code)
        {
            List<GK_OA_AssetTransferModel> list = new List<GK_OA_AssetTransferModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_AssetTransferBLL rbll = new GK_OA_AssetTransferBLL();
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

        public List<string> GetSortType()
        {
            List<string> list = new List<string>();
            EntityData dictionaryItemByName = SystemManageDAO.GetDictionaryItemByName("资产类别");
            foreach (DataRow row in dictionaryItemByName.Tables[0].Rows)
            {
                list.Add(row["Name"].ToString());
            }
            return list;
        }

        public int Insert(GK_OA_AssetTransferModel ObjModel)
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
                        num = new GK_OA_AssetTransferBLL().Insert(ObjModel, transaction);
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
                        num = new GK_OA_AssetTransferBLL().ModifyAlreadyAuditing(Code, transaction);
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
                        num = new GK_OA_AssetTransferBLL().ModifyBankOutAuditing(Code, transaction);
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
                        num = new GK_OA_AssetTransferBLL().ModifyNotAuditing(Code, transaction);
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
                        num = new GK_OA_AssetTransferBLL().ModifyNotPassAuditing(Code, transaction);
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
                        num = new GK_OA_AssetTransferBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(GK_OA_AssetTransferModel ObjModel)
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
                        num = new GK_OA_AssetTransferBLL().Update(ObjModel, transaction);
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

