namespace RmsPM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using RmsPM.BLL;
    using TiannuoPM.MODEL;

    public class MaterialBFL
    {
        public int Delete(MaterialModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialBLL().Delete(ObjModel, transaction);
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

        public static void DeleteAllMaterial()
        {
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();
                try
                {
                    new MaterialBLL().DeleteAll(tran);
                    tran.Commit();
                }
                catch (SqlException exception)
                {
                    tran.Rollback();
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static string GetImportMaterialFieldDesc()
        {
            MaterialImport import = new MaterialImport();
            return import.GetDefineFieldDesc();
        }

        public static MaterialModel GetMaterial(object Code)
        {
            MaterialModel model = new MaterialModel();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                model = new MaterialBLL().GetModel(Code, connection);
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

        public List<MaterialModel> GetMaterialList(MaterialQueryModel QueryModel)
        {
            List<MaterialModel> models = new List<MaterialModel>();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                if (QueryModel == null)
                {
                    QueryModel = new MaterialQueryModel();
                }
                models = new MaterialBLL().GetModels(QueryModel, connection);
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

        public List<MaterialModel> GetMaterialList(string SortColumns, int StartRecord, int MaxRecords, string AccessRange, int MaterialCodeEqual, string MaterialNameEqual, string GroupCodeEqual, string SpecEqual, string UnitEqual, string StandardPriceRange1, string StandardPriceRange2, string InputPerson, DateTime InputDateRange1, DateTime InputDateRange2, string RemarkEqual)
        {
            List<MaterialModel> models = new List<MaterialModel>();
            MaterialQueryModel objQueryModel = new MaterialQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.AccessRange = AccessRange;
            objQueryModel.MaterialCodeEqual = MaterialCodeEqual;
            objQueryModel.MaterialNameEqual = MaterialNameEqual;
            objQueryModel.GroupCodeEqual = GroupCodeEqual;
            objQueryModel.SpecEqual = SpecEqual;
            objQueryModel.UnitEqual = UnitEqual;
            objQueryModel.StandardPriceRange1 = StandardPriceRange1;
            objQueryModel.StandardPriceRange2 = StandardPriceRange2;
            objQueryModel.InputPerson = InputPerson;
            objQueryModel.InputDateRange1 = InputDateRange1;
            objQueryModel.InputDateRange2 = InputDateRange2;
            objQueryModel.RemarkEqual = RemarkEqual;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                models = new MaterialBLL().GetModels(objQueryModel, connection);
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

        public static List<MaterialModel> GetMaterialListOne(object Code)
        {
            List<MaterialModel> list = new List<MaterialModel>();
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                MaterialBLL lbll = new MaterialBLL();
                list.Add(lbll.GetModel(Code, connection));
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

        public static string GetMaterialName(object objMaterialCode)
        {
            if ((objMaterialCode == null) || (objMaterialCode.ToString() == ""))
            {
                return "";
            }
            //try
            //{
                int code = (int) objMaterialCode;
                return GetMaterial(code).MaterialName;
            //}
            //catch
            //{
            //}
        }

        public static string ImportMaterial(Stream stream, string InputPerson)
        {
            string text;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                try
                {
                    MaterialImport import = new MaterialImport();
                    import.conn = connection;
                    text = import.Import(stream, InputPerson);
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return text;
        }

        public int Insert(MaterialModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialBLL().Insert(ObjModel, transaction);
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

        public int Update(MaterialModel ObjModel)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = new MaterialBLL().Update(ObjModel, transaction);
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

