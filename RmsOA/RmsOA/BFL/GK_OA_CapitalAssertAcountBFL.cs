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

    public class GK_OA_CapitalAssertAcountBFL
    {
        public int Delete(GK_OA_CapitalAssertAcountModel ObjModel)
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
                        num = new GK_OA_CapitalAssertAcountBLL().Delete(ObjModel.Code, transaction);
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

        public GK_OA_CapitalAssertAcountModel GetGK_OA_CapitalAssertAcount(int Code)
        {
            GK_OA_CapitalAssertAcountModel model = new GK_OA_CapitalAssertAcountModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_CapitalAssertAcountBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_CapitalAssertAcountModel> GetGK_OA_CapitalAssertAcountList(GK_OA_CapitalAssertAcountQueryModel QueryModel)
        {
            List<GK_OA_CapitalAssertAcountModel> models = new List<GK_OA_CapitalAssertAcountModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_CapitalAssertAcountQueryModel();
                    }
                    models = new GK_OA_CapitalAssertAcountBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_CapitalAssertAcountModel> GetGK_OA_CapitalAssertAcountList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string NameEqual, string TypeEqual, string NumberEqual, DateTime BuydateEqual, int BuyCountEqual, decimal PriceEqual, string UnitEqual, string DeptEqual, string TransferRecordEqual, string UserNameEqual, string PlaceEqual, string RemarkEqual, string QualityNOEqual, string SNRuleEqual, string StatusEqual, DateTime StartDay, DateTime EndDay)
        {
            List<GK_OA_CapitalAssertAcountModel> models = new List<GK_OA_CapitalAssertAcountModel>();
            GK_OA_CapitalAssertAcountQueryModel objQueryModel = new GK_OA_CapitalAssertAcountQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.NameEqual = NameEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.NumberEqual = NumberEqual;
            objQueryModel.BuydateEqual = BuydateEqual;
            objQueryModel.BuyCountEqual = BuyCountEqual;
            objQueryModel.PriceEqual = PriceEqual;
            objQueryModel.UnitEqual = UnitEqual;
            objQueryModel.DeptEqual = DeptEqual;
            objQueryModel.TransferRecordEqual = TransferRecordEqual;
            objQueryModel.UserNameEqual = UserNameEqual;
            objQueryModel.PlaceEqual = PlaceEqual;
            objQueryModel.RemarkEqual = RemarkEqual;
            objQueryModel.QualityNOEqual = QualityNOEqual;
            objQueryModel.SNRuleEqual = SNRuleEqual;
            objQueryModel.StatusEqual = StatusEqual;
            objQueryModel.StartDayEqual = StartDay;
            objQueryModel.EndDayEqual = EndDay;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_CapitalAssertAcountBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_CapitalAssertAcountModel> GetGK_OA_CapitalAssertAcountListOne(int Code)
        {
            List<GK_OA_CapitalAssertAcountModel> list = new List<GK_OA_CapitalAssertAcountModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_CapitalAssertAcountBLL tbll = new GK_OA_CapitalAssertAcountBLL();
                    list.Add(tbll.GetModel(Code, connection));
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

        public List<string> GetMaterialType()
        {
            List<string> list = new List<string>();
            EntityData dictionaryItemByName = SystemManageDAO.GetDictionaryItemByName("资产类别");
            foreach (DataRow row in dictionaryItemByName.Tables[0].Rows)
            {
                list.Add(row["Name"].ToString());
            }
            return list;
        }

        public int Insert(GK_OA_CapitalAssertAcountModel ObjModel)
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
                        num = new GK_OA_CapitalAssertAcountBLL().Insert(ObjModel, transaction);
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
                        num = new GK_OA_CapitalAssertAcountBLL().ModifyAlreadyAuditing(Code, transaction);
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
                        num = new GK_OA_CapitalAssertAcountBLL().ModifyBankOutAuditing(Code, transaction);
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
                        num = new GK_OA_CapitalAssertAcountBLL().ModifyNotAuditing(Code, transaction);
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
                        num = new GK_OA_CapitalAssertAcountBLL().ModifyNotPassAuditing(Code, transaction);
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
                        num = new GK_OA_CapitalAssertAcountBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(GK_OA_CapitalAssertAcountModel ObjModel)
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
                        num = new GK_OA_CapitalAssertAcountBLL().Update(ObjModel, transaction);
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

