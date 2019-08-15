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

    public class GK_OA_EquipmentMaintainApplyBFL
    {
        public int Delete(GK_OA_EquipmentMaintainApplyModel ObjModel)
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().Delete(ObjModel.Code, transaction);
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

        public List<string> GetEquipmentType()
        {
            List<string> list = new List<string>();
            EntityData dictionaryItemByName = SystemManageDAO.GetDictionaryItemByName("设备类型");
            foreach (DataRow row in dictionaryItemByName.Tables[0].Rows)
            {
                list.Add(row["Name"].ToString());
            }
            return list;
        }

        public GK_OA_EquipmentMaintainApplyModel GetGK_OA_EquipmentMaintainApply(int Code)
        {
            GK_OA_EquipmentMaintainApplyModel model = new GK_OA_EquipmentMaintainApplyModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new GK_OA_EquipmentMaintainApplyBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<GK_OA_EquipmentMaintainApplyModel> GetGK_OA_EquipmentMaintainApplyList(GK_OA_EquipmentMaintainApplyQueryModel QueryModel)
        {
            List<GK_OA_EquipmentMaintainApplyModel> models = new List<GK_OA_EquipmentMaintainApplyModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new GK_OA_EquipmentMaintainApplyQueryModel();
                    }
                    models = new GK_OA_EquipmentMaintainApplyBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_EquipmentMaintainApplyModel> GetGK_OA_EquipmentMaintainApplyList(string SortColumns, int StartRecord, int MaxRecords, int CodeEqual, string NameEqual, string DeptEqual, string ModelNOEqual, string TypeEqual, string NumberEqual, decimal BudgetMoneyEqual, string UserNameEqual, DateTime ApplyDateEqual, string ReasonEqual, string StateEqual, string QualityNOEqual, string SNRuleEqual, DateTime StartDate, DateTime EndDate)
        {
            List<GK_OA_EquipmentMaintainApplyModel> models = new List<GK_OA_EquipmentMaintainApplyModel>();
            GK_OA_EquipmentMaintainApplyQueryModel objQueryModel = new GK_OA_EquipmentMaintainApplyQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.CodeEqual = CodeEqual;
            objQueryModel.NameEqual = NameEqual;
            objQueryModel.DeptEqual = DeptEqual;
            objQueryModel.ModelNOEqual = ModelNOEqual;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.NumberEqual = NumberEqual;
            objQueryModel.BudgetMoneyEqual = BudgetMoneyEqual;
            objQueryModel.UserNameEqual = UserNameEqual;
            objQueryModel.ApplyDateEqual = ApplyDateEqual;
            objQueryModel.ReasonEqual = ReasonEqual;
            objQueryModel.StateEqual = StateEqual;
            objQueryModel.QualityNOEqual = QualityNOEqual;
            objQueryModel.SNRuleEqual = SNRuleEqual;
            objQueryModel.StartDateEqual = StartDate;
            objQueryModel.EndDateEqual = EndDate;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new GK_OA_EquipmentMaintainApplyBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<GK_OA_EquipmentMaintainApplyModel> GetGK_OA_EquipmentMaintainApplyListOne(int Code)
        {
            List<GK_OA_EquipmentMaintainApplyModel> list = new List<GK_OA_EquipmentMaintainApplyModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    GK_OA_EquipmentMaintainApplyBLL ybll = new GK_OA_EquipmentMaintainApplyBLL();
                    list.Add(ybll.GetModel(Code, connection));
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

        public int Insert(GK_OA_EquipmentMaintainApplyModel ObjModel)
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().Insert(ObjModel, transaction);
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().ModifyAlreadyAuditing(Code, transaction);
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().ModifyBankOutAuditing(Code, transaction);
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().ModifyNotAuditing(Code, transaction);
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().ModifyNotPassAuditing(Code, transaction);
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().ModifyPassAuditing(Code, transaction);
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

        public int Update(GK_OA_EquipmentMaintainApplyModel ObjModel)
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
                        num = new GK_OA_EquipmentMaintainApplyBLL().Update(ObjModel, transaction);
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

