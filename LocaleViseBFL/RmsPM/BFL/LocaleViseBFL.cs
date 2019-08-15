namespace RmsPM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using RmsPM.BLL;
    using TiannuoPM.MODEL;

    public class LocaleViseBFL
    {
        public int Balance(int ViseCode)
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
                        num = new LocaleViseBLL().Balance(ViseCode, transaction);
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

        public int Delete(LocaleViseModel ViseModel)
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
                        num = new LocaleViseBLL().Delete(ViseModel, transaction);
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

        public int DeleteCost(LocaleViseCostModel ViseCostModel)
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
                        num = new LocaleViseBLL().DeleteCost(ViseCostModel, transaction);
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

        public static ViseBalanceStatusEnum GetBalanceStatus(int ViseCode)
        {
            LocaleViseBLL ebll = new LocaleViseBLL();
            ViseBalanceStatusEnum unknown = ViseBalanceStatusEnum.unknown;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    switch (ebll.GetModel(ViseCode, connection).ViseBalanceStatus)
                    {
                        case 1:
                            unknown = ViseBalanceStatusEnum.nobalance;
                            break;

                        case 2:
                            unknown = ViseBalanceStatusEnum.isbalance;
                            break;

                        default:
                            unknown = ViseBalanceStatusEnum.unknown;
                            break;
                    }
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return unknown;
        }

        public List<LocaleViseModel> GetLocalVise(int Code)
        {
            List<LocaleViseModel> list = new List<LocaleViseModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    LocaleViseQueryModel viseQueryModel = new LocaleViseQueryModel();
                    viseQueryModel.ViseCode = Code;
                    list = new LocaleViseBLL().Select(viseQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public List<LocaleViseCostModel> GetLocalViseCost(int Code)
        {
            List<LocaleViseCostModel> list = new List<LocaleViseCostModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    LocaleViseCostQueryModel viseCostQueryModel = new LocaleViseCostQueryModel();
                    viseCostQueryModel.ViseCostCode = Code;
                    list = new LocaleViseBLL().SelectCost(viseCostQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public List<LocaleViseCostModel> GetLocalViseCosts(int Code)
        {
            List<LocaleViseCostModel> list = new List<LocaleViseCostModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    LocaleViseCostQueryModel viseCostQueryModel = new LocaleViseCostQueryModel();
                    viseCostQueryModel.ViseCode = Code;
                    list = new LocaleViseBLL().SelectCost(viseCostQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public List<LocaleViseModel> GetLocalVises(LocaleViseQueryModel QueryModel)
        {
            List<LocaleViseModel> list = new List<LocaleViseModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    if (QueryModel == null)
                    {
                        QueryModel = new LocaleViseQueryModel();
                    }
                    list = new LocaleViseBLL().Select(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public List<LocaleViseModel> GetLocalVises(string SortColumns, int StartRecord, int MaxRecords, string ViseId, string ViseName, string VisePerson, string ViseSupplier, string ViseContractCode, string ViseUnit, DateTime ViseDateStart, DateTime ViseDateEnd, DateTime ViseEndDateStart, DateTime ViseEndDateEnd, string ViseProject, string ViseType, string ViseBalanceStatusInStr, string ViseStatusInStr)
        {
            return this.GetLocalVises(SortColumns, StartRecord, MaxRecords, ViseId, ViseName, VisePerson, ViseSupplier, ViseContractCode, ViseUnit, ViseDateStart, ViseDateEnd, ViseEndDateStart, ViseEndDateEnd, ViseProject, ViseType, ViseBalanceStatusInStr, ViseStatusInStr, 0);
        }

        public List<LocaleViseModel> GetLocalVises(string SortColumns, int StartRecord, int MaxRecords, string ViseId, string ViseName, string VisePerson, string ViseSupplier, string ViseContractCode, string ViseUnit, DateTime ViseDateStart, DateTime ViseDateEnd, DateTime ViseEndDateStart, DateTime ViseEndDateEnd, string ViseProject, string ViseType, string ViseBalanceStatusInStr, string ViseStatusInStr, int ViseReferCode)
        {
            List<LocaleViseModel> list = new List<LocaleViseModel>();
            LocaleViseQueryModel viseQueryModel = new LocaleViseQueryModel();
            viseQueryModel.StartRecord = StartRecord;
            viseQueryModel.MaxRecords = MaxRecords;
            viseQueryModel.SortColumns = SortColumns;
            viseQueryModel.ViseId = ViseId;
            viseQueryModel.ViseName = ViseName;
            viseQueryModel.VisePerson = VisePerson;
            viseQueryModel.ViseProject = ViseProject;
            viseQueryModel.ViseStatusInStr = ViseStatusInStr;
            viseQueryModel.ViseSupplier = ViseSupplier;
            viseQueryModel.ViseType = ViseType;
            viseQueryModel.ViseUnit = ViseUnit;
            viseQueryModel.ViseBalanceStatusInStr = ViseBalanceStatusInStr;
            viseQueryModel.ViseContractCode = ViseContractCode;
            viseQueryModel.ViseDateEnd = ViseDateEnd;
            viseQueryModel.ViseDateStart = ViseDateStart;
            viseQueryModel.ViseEndDateStart = ViseEndDateStart;
            viseQueryModel.ViseEndDateEnd = ViseEndDateEnd;
            viseQueryModel.ViseReferCode = ViseReferCode;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    list = new LocaleViseBLL().Select(viseQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public static ViseStatusEnum GetStatus(int ViseCode)
        {
            LocaleViseBLL ebll = new LocaleViseBLL();
            ViseStatusEnum unknown = ViseStatusEnum.unknown;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    switch (ebll.GetModel(ViseCode, connection).ViseStatus)
                    {
                        case 1:
                            unknown = ViseStatusEnum.wait;
                            break;

                        case 2:
                            unknown = ViseStatusEnum.process;
                            break;

                        case 3:
                            unknown = ViseStatusEnum.ispass;
                            break;

                        case 4:
                            unknown = ViseStatusEnum.nopass;
                            break;

                        default:
                            unknown = ViseStatusEnum.unknown;
                            break;
                    }
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return unknown;
        }

        public static string GetViseContractCode(int Code)
        {
            string viseContractCode = "";
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    LocaleViseBLL ebll = new LocaleViseBLL();
                    viseContractCode = ebll.GetModel(Code, connection).ViseContractCode;
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return viseContractCode;
        }

        public static string GetViseName(int Code)
        {
            string viseName = "";
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    LocaleViseBLL ebll = new LocaleViseBLL();
                    viseName = ebll.GetModel(Code, connection).ViseName;
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return viseName;
        }

        public static string GetViseStatusName(int Status)
        {
            switch (Status)
            {
                case 1:
                    return "待审";

                case 2:
                    return "审核中";

                case 3:
                    return "已审核";

                case 4:
                    return "作废";
            }
            return "";
        }

        public static decimal GetViseSumCheckMoney(int ViseCode)
        {
            decimal viseSumCheckMoney = 0M;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    viseSumCheckMoney = new LocaleViseBLL().GetViseSumCheckMoney(ViseCode, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return viseSumCheckMoney;
        }

        public static decimal GetViseSumCheckMoneyByContract(string ContractCode)
        {
            decimal sumCheckMoneyByContract = 0M;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    sumCheckMoneyByContract = new LocaleViseBLL().GetSumCheckMoneyByContract(ContractCode, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return sumCheckMoneyByContract;
        }

        public static decimal GetViseSumMoney(int ViseCode)
        {
            decimal viseSumMoney = 0M;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    viseSumMoney = new LocaleViseBLL().GetViseSumMoney(ViseCode, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return viseSumMoney;
        }

        public int Insert(LocaleViseModel ViseModel)
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
                        num = new LocaleViseBLL().Insert(ViseModel, transaction);
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

        public int InsertCost(LocaleViseCostModel ViseCostModel)
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
                        num = new LocaleViseBLL().InsertCost(ViseCostModel, transaction);
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

        public int NoPassAudit(int ViseCode)
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
                        num = new LocaleViseBLL().NoPassAudit(ViseCode, transaction);
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

        public int PassAudit(int ViseCode, List<LocaleViseCostModel> CostList)
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
                        LocaleViseBLL ebll = new LocaleViseBLL();
                        foreach (LocaleViseCostModel model in CostList)
                        {
                            ebll.UpdateCost(model, transaction);
                        }
                        num = ebll.PassAudit(ViseCode, transaction);
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

        public int ReturnWait(int ViseCode)
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
                        num = new LocaleViseBLL().ReturnWait(ViseCode, transaction);
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

        public int StartAudit(int ViseCode)
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
                        num = new LocaleViseBLL().StartAudit(ViseCode, transaction);
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

        public int Update(LocaleViseModel ViseModel)
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
                        num = new LocaleViseBLL().Update(ViseModel, transaction);
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

        public int UpdateComeToMoney(int ViseCode, decimal ComeToMoney)
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
                        LocaleViseBLL ebll = new LocaleViseBLL();
                        LocaleViseModel viseModel = ebll.GetModel(ViseCode, transaction);
                        viseModel.ViseComeToMoney = ComeToMoney;
                        num = ebll.Update(viseModel, transaction);
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

        public int UpdateCost(LocaleViseCostModel ViseCostModel)
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
                        num = new LocaleViseBLL().UpdateCost(ViseCostModel, transaction);
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

        public static int ViseAuditForCreateContractChange(int Code, string UserCode, List<LocaleViseCostModel> CostList)
        {
            int num = 0;
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("Code", Type.GetType("System.String"));
            DataColumn column2 = new DataColumn("Type", Type.GetType("System.String"));
            table.Columns.Add(column);
            table.Columns.Add(column2);
            DataRow row = table.NewRow();
            row["Code"] = Code;
            row["Type"] = "Vise";
            table.Rows.Add(row);
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        LocaleViseBLL ebll = new LocaleViseBLL();
                        foreach (LocaleViseCostModel model in CostList)
                        {
                            ebll.UpdateCost(model, transaction);
                        }
                        num = ebll.PassAudit(Code, transaction);
                        ebll.Balance(Code, transaction);
                        string viseContractCode = ebll.GetModel(Code, transaction).ViseContractCode;
                        if (viseContractCode != "")
                        {
                            ContractRule.BuildContractChangeByNexusCodeType(viseContractCode, table, transaction, UserCode);
                        }
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

