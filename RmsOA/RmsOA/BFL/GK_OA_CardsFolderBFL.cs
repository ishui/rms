namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class GK_OA_CardsFolderBFL
    {
        public int Delete(GK_OA_CardsFolderModel ObjModel)
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
                        num = new GK_OA_CardsFolderBLL().Delete(ObjModel.Code, transaction);
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

        public static string[] GetCardType()
        {
            return new string[] { "", "客户", "同事", "朋友", "同学" };
        }

        public GK_OA_CardsFolderModel GetGK_OA_CardsFolder(int Code)
        {
            GK_OA_CardsFolderModel model = new GK_OA_CardsFolderModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_CardsFolderBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_CardsFolderModel> GetGK_OA_CardsFolderList(GK_OA_CardsFolderQueryModel QueryModel)
        {
            List<GK_OA_CardsFolderModel> models = new List<GK_OA_CardsFolderModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_CardsFolderQueryModel();
                    }
                    models = new GK_OA_CardsFolderBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_CardsFolderModel> GetGK_OA_CardsFolderList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string UserIdEqual, string UserNameEqual, string SexEqual, int AgeEqual, string CompanyNameEqual, string CompanyAddressEqual, string DeptEqual, string HeadshipEqual, string PostalcodeEqual, string PhoneEqual, string FaxEqual, string MobileEqual, string EmailEqual, string NetAddressEqual, string HobbyEqual, string HomeAddressEqual, string WedlockEqual, string CardTypeEqual, string NativePlaceEqual, DateTime BirthdayEqual, DateTime ContactTimeEqual, string RemarkEqual, string HomePhoneEqual)
        {
            List<GK_OA_CardsFolderModel> models = new List<GK_OA_CardsFolderModel>();
            GK_OA_CardsFolderQueryModel objQueryModel = new GK_OA_CardsFolderQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.UserIdEqual = UserIdEqual;
            objQueryModel.UserNameEqual = UserNameEqual;
            objQueryModel.SexEqual = SexEqual;
            objQueryModel.AgeEqual = AgeEqual;
            objQueryModel.CompanyNameEqual = CompanyNameEqual;
            objQueryModel.CompanyAddressEqual = CompanyAddressEqual;
            objQueryModel.DeptEqual = DeptEqual;
            objQueryModel.HeadshipEqual = HeadshipEqual;
            objQueryModel.PostalcodeEqual = PostalcodeEqual;
            objQueryModel.PhoneEqual = PhoneEqual;
            objQueryModel.FaxEqual = FaxEqual;
            objQueryModel.MobileEqual = MobileEqual;
            objQueryModel.EmailEqual = EmailEqual;
            objQueryModel.NetAddressEqual = NetAddressEqual;
            objQueryModel.HobbyEqual = HobbyEqual;
            objQueryModel.HomeAddressEqual = HomeAddressEqual;
            objQueryModel.WedlockEqual = WedlockEqual;
            objQueryModel.CardTypeEqual = CardTypeEqual;
            objQueryModel.NativePlaceEqual = NativePlaceEqual;
            objQueryModel.BirthdayEqual = BirthdayEqual;
            objQueryModel.ContactTimeEqual = ContactTimeEqual;
            objQueryModel.RemarkEqual = RemarkEqual;
            objQueryModel.HomePhoneEqual = HomePhoneEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_CardsFolderBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_CardsFolderModel> GetGK_OA_CardsFolderListOne(int Code)
        {
            List<GK_OA_CardsFolderModel> list = new List<GK_OA_CardsFolderModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_CardsFolderBLL rbll = new GK_OA_CardsFolderBLL();
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

        public DateTime GetUsefulTime(string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                if (result.Year < 0x708)
                {
                    return DateTime.Now;
                }
                return result;
            }
            return DateTime.Now;
        }

        public static string[] GetWedLockType()
        {
            return new string[] { "", "未婚", "已婚", "离异", "丧偶" };
        }

        public int Insert(GK_OA_CardsFolderModel ObjModel)
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
                        num = new GK_OA_CardsFolderBLL().Insert(ObjModel, transaction);
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

        public int Update(GK_OA_CardsFolderModel ObjModel)
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
                        num = new GK_OA_CardsFolderBLL().Update(ObjModel, transaction);
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

