namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.BLL;
    using RmsOA.MODEL;

    public class OAPersonBFL
    {
        public int Delete(OAPersonModel ObjModel)
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
                        num = new OAPersonBLL().Delete(ObjModel.Code, transaction);
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

        public OAPersonModel GetOAPerson(int Code)
        {
            OAPersonModel model = new OAPersonModel();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    model = new OAPersonBLL().GetModel(Code, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return model;
        }

        public List<OAPersonModel> GetOAPersonList(OAPersonQueryModel QueryModel)
        {
            List<OAPersonModel> models = new List<OAPersonModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    if (QueryModel == null)
                    {
                        QueryModel = new OAPersonQueryModel();
                    }
                    models = new OAPersonBLL().GetModels(QueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<OAPersonModel> GetOAPersonList(string SortColumns, int StartRecord, int MaxRecords, string cnameEqual, string yardEqual, DateTime startBirthdayEqual, DateTime endBirthdayEqual, string addressEqual)
        {
            List<OAPersonModel> models = new List<OAPersonModel>();
            OAPersonQueryModel objQueryModel = new OAPersonQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.cnameEqual = cnameEqual;
            objQueryModel.birthdayEqualDY = startBirthdayEqual;
            objQueryModel.birthdayEqualXY = endBirthdayEqual;
            objQueryModel.yardEqual = yardEqual;
            objQueryModel.addressEqual = addressEqual;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    models = new OAPersonBLL().GetModels(objQueryModel, connection);
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return models;
        }

        public List<OAPersonModel> GetOAPersonListOne(int Code)
        {
            List<OAPersonModel> list = new List<OAPersonModel>();
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    OAPersonBLL nbll = new OAPersonBLL();
                    list.Add(nbll.GetModel(Code, connection));
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        public int Insert(OAPersonModel ObjModel)
        {
            int num = 0;
            GK_OA_ComBookModel objModel = new GK_OA_ComBookModel();
            GK_OA_ComBookBLL kbll = new GK_OA_ComBookBLL();
            objModel.Company = string.Empty;
            objModel.MSN = string.Empty;
            objModel.QQ = string.Empty;
            objModel.UnitCode = ObjModel.yard;
            objModel.UserCode = ObjModel.cname;
            objModel.Telephone = ObjModel.phone;
            objModel.HandleTelephone = ObjModel.mobile;
            objModel.Email = ObjModel.email;
            objModel.PrepField1 = ObjModel.address;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        OAPersonBLL nbll = new OAPersonBLL();
                        kbll.Insert(objModel, transaction);
                        num = nbll.Insert(ObjModel, transaction);
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

        public int Update(OAPersonModel ObjModel)
        {
            int num = 0;
            GK_OA_ComBookModel objModel = new GK_OA_ComBookModel();
            GK_OA_ComBookBLL kbll = new GK_OA_ComBookBLL();
            objModel.UnitCode = ObjModel.yard;
            objModel.UserCode = ObjModel.cname;
            objModel.Telephone = ObjModel.phone;
            objModel.HandleTelephone = ObjModel.mobile;
            objModel.Email = ObjModel.email;
            objModel.PrepField1 = ObjModel.address;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        OAPersonBLL nbll = new OAPersonBLL();
                        kbll.Update(objModel, transaction);
                        num = nbll.Update(ObjModel, transaction);
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

