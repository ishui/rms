namespace RmsPM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.BLL;
    using TiannuoPM.MODEL;

    public class DesignChangeBFL
    {
        public int Delete(DesignChangeModel ObjModel) 
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
                        num = new DesignChangeBLL().Delete(ObjModel.Code, transaction);
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

        public DesignChangeModel GetDesignChange(int Code)
        {
            DesignChangeModel model = new DesignChangeModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    model = new DesignChangeBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<DesignChangeModel> GetDesignChangeList(DesignChangeQueryModel QueryModel)
        {
            List<DesignChangeModel> models = new List<DesignChangeModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    if (QueryModel == null)
                    {
                        QueryModel = new DesignChangeQueryModel();
                    }
                    models = new DesignChangeBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DesignChangeModel> GetDesignChangeList(string SortColumns, int StartRecord, int MaxRecords, string SolutionNameLike, string ProjectCodeEqual, string NumberEqual, string PersonEqual, string UnitEqual, string SupplierEqual, DateTime DateStart, DateTime DateEnd, string TypeEqual, string StateInStr, string ChangeType)
        {
            return this.GetDesignChangeList(SortColumns, StartRecord, MaxRecords, SolutionNameLike, ProjectCodeEqual, NumberEqual, PersonEqual, UnitEqual, SupplierEqual, DateStart, DateEnd, TypeEqual, StateInStr, ChangeType, 0);
        }

        public List<DesignChangeModel> GetDesignChangeList(string SortColumns, int StartRecord, int MaxRecords, string SolutionNameLike, string ProjectCodeEqual, string NumberEqual, string PersonEqual, string UnitEqual, string SupplierEqual, DateTime DateStart, DateTime DateEnd, string TypeEqual, string StateInStr, string ChangeType, int ReferCode)
        {
            List<DesignChangeModel> models = new List<DesignChangeModel>();
            DesignChangeQueryModel objQueryModel = new DesignChangeQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.SolutionNameLike = SolutionNameLike;
            objQueryModel.ProjectNameEqual = ProjectCodeEqual;
            objQueryModel.NumberEqual = NumberEqual;
            objQueryModel.PersonEqual = PersonEqual;
            objQueryModel.UnitEqual = UnitEqual;
            objQueryModel.SupplierEqual = SupplierEqual;
            objQueryModel.DateStartCheck = DateStart;
            objQueryModel.DateEndCheck = DateEnd;
            objQueryModel.TypeEqual = TypeEqual;
            objQueryModel.StateIn = StateInStr;
            objQueryModel.ChangeType = ChangeType;
            objQueryModel.ReferCode = ReferCode;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    models = new DesignChangeBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<DesignChangeModel> GetDesignChangeListOne(int Code)
        {
            List<DesignChangeModel> list = new List<DesignChangeModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    DesignChangeBLL ebll = new DesignChangeBLL();
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

        public static string GetDesignChangeName(int Code)
        {
            string solutionName = "";
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    DesignChangeBLL ebll = new DesignChangeBLL();
                    solutionName = ebll.GetModel(Code, connection).SolutionName;
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return solutionName;
        }

        public static string GetStateNameByCode(string StateCode)
        {
            switch (StateCode)
            {
                case "0":
                    return "待审";

                case "1":
                    return "审核中";

                case "2":
                    return "已审";

                case "3":
                    return "未通过";
            }
            return "待审";
        }

        public int Insert(DesignChangeModel ObjModel)
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
                        num = new DesignChangeBLL().Insert(ObjModel, transaction);
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

        public int NoPass(int Code)
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
                        DesignChangeBLL ebll = new DesignChangeBLL();
                        DesignChangeModel objModel = ebll.GetModel(Code, transaction);
                        objModel.State = "3";
                        objModel.ChangeMoney = "";
                        objModel.TotalMoney = 0M;
                        num = ebll.Update(objModel, transaction);
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

        public int PassAudit(int Code)
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
                        DesignChangeBLL ebll = new DesignChangeBLL();
                        DesignChangeModel objModel = ebll.GetModel(Code, transaction);
                        objModel.State = "2";
                        num = ebll.Update(objModel, transaction);
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

        public int ReturnWait(int Code)
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
                        DesignChangeBLL ebll = new DesignChangeBLL();
                        DesignChangeModel objModel = ebll.GetModel(Code, transaction);
                        objModel.State = "0";
                        num = ebll.Update(objModel, transaction);
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

        public int StartAudit(int Code)
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
                        DesignChangeBLL ebll = new DesignChangeBLL();
                        DesignChangeModel objModel = ebll.GetModel(Code, transaction);
                        objModel.State = "1";
                        num = ebll.Update(objModel, transaction);
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

        public int Update(DesignChangeModel ObjModel)
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
                        num = new DesignChangeBLL().Update(ObjModel, transaction);
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

        public int UpdateAuditMoney(int Code, string ChangeMoneyDescription, decimal TotalMoney)
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
                        DesignChangeBLL ebll = new DesignChangeBLL();
                        DesignChangeModel objModel = ebll.GetModel(Code, transaction);
                        if (!string.IsNullOrEmpty(ChangeMoneyDescription))
                        {
                            objModel.ChangeMoney = ChangeMoneyDescription;
                            objModel.TotalMoney = TotalMoney;
                        }
                        num = ebll.Update(objModel, transaction);
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

        public int YFNoPass(int Code, string number)
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
                        DesignChangeBLL ebll = new DesignChangeBLL();
                        DesignChangeModel objModel = ebll.GetModel(Code, transaction);
                        objModel.State = "3";
                        objModel.ChangeMoney = "";
                        objModel.TotalMoney = 0M;
                        objModel.Number = number;
                        num = ebll.Update(objModel, transaction);
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

        public int YFPassAudit(int Code, string number)
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
                        DesignChangeBLL ebll = new DesignChangeBLL();
                        DesignChangeModel objModel = ebll.GetModel(Code, transaction);
                        objModel.State = "2";
                        objModel.Number = number;
                        num = ebll.Update(objModel, transaction);
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

