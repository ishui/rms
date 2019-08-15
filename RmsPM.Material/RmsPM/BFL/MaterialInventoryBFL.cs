namespace RmsPM.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.BLL;
    using TiannuoPM.MODEL;

    public class MaterialInventoryBFL
    {
        public V_MaterialInventoryModel GetV_MaterialInventoryByMaterialProject(string MaterialCode, string ProjectCode)
        {
            V_MaterialInventoryModel model = new V_MaterialInventoryModel();
            V_MaterialInventoryQueryModel objQueryModel = new V_MaterialInventoryQueryModel();
            objQueryModel.StartRecord = 0;
            objQueryModel.MaxRecords = -1;
            objQueryModel.SortColumns = "";
            objQueryModel.MaterialCodeEqual = MaterialCode;
            objQueryModel.ProjectCodeEqual = ProjectCode;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                List<V_MaterialInventoryModel> models = new MaterialInventoryBLL().GetModels(objQueryModel, connection);
                if (models.Count > 0)
                {
                    model = models[0];
                }
            }
            catch (Exception exception)
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

        public List<V_MaterialInventoryIOModel> GetV_MaterialInventoryIOListByMaterialProject(string MaterialCode, string ProjectCode)
        {
            List<V_MaterialInventoryIOModel> models;
            V_MaterialInventoryIOQueryModel objQueryModel = new V_MaterialInventoryIOQueryModel();
            objQueryModel.StartRecord = 0;
            objQueryModel.MaxRecords = -1;
            objQueryModel.SortColumns = "IODate, MaterialIOCode";
            objQueryModel.MaterialCodeEqual = MaterialCode;
            objQueryModel.ProjectCodeEqual = ProjectCode;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                models = new MaterialInventoryIOBLL().GetModels(objQueryModel, connection);
            }
            catch (Exception exception)
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

        public List<V_MaterialInventoryModel> GetV_MaterialInventoryList(V_MaterialInventoryQueryModel QueryModel)
        {
            List<V_MaterialInventoryModel> models;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                if (QueryModel == null)
                {
                    QueryModel = new V_MaterialInventoryQueryModel();
                }
                models = new MaterialInventoryBLL().GetModels(QueryModel, connection);
            }
            catch (Exception exception)
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

        public List<V_MaterialInventoryModel> GetV_MaterialInventoryList(string SortColumns, int StartRecord, int MaxRecords, string AccessRange, string ProjectCodeEqual, string ProjectNameEqual, string MaterialCodeEqual, string InQtyRange1, string InQtyRange2, string InMoneyRange1, string InMoneyRange2, string OutQtyRange1, string OutQtyRange2, string InvQtyRange1, string InvQtyRange2, string MaterialNameEqual, string SpecEqual, string UnitEqual, string StandardPriceRange1, string StandardPriceRange2, string GroupCodeEqual, string GroupNameEqual, string GroupFullIDEqual, string GroupSortIDEqual)
        {
            List<V_MaterialInventoryModel> models;
            V_MaterialInventoryQueryModel objQueryModel = new V_MaterialInventoryQueryModel();
            objQueryModel.StartRecord = StartRecord;
            objQueryModel.MaxRecords = MaxRecords;
            objQueryModel.SortColumns = SortColumns;
            objQueryModel.AccessRange = AccessRange;
            objQueryModel.ProjectCodeEqual = ProjectCodeEqual;
            objQueryModel.ProjectNameEqual = ProjectNameEqual;
            objQueryModel.MaterialCodeEqual = MaterialCodeEqual;
            objQueryModel.InQtyRange1 = InQtyRange1;
            objQueryModel.InQtyRange2 = InQtyRange2;
            objQueryModel.InMoneyRange1 = InMoneyRange1;
            objQueryModel.InMoneyRange2 = InMoneyRange2;
            objQueryModel.OutQtyRange1 = OutQtyRange1;
            objQueryModel.OutQtyRange2 = OutQtyRange2;
            objQueryModel.InvQtyRange1 = InvQtyRange1;
            objQueryModel.InvQtyRange2 = InvQtyRange2;
            objQueryModel.MaterialNameEqual = MaterialNameEqual;
            objQueryModel.SpecEqual = SpecEqual;
            objQueryModel.UnitEqual = UnitEqual;
            objQueryModel.StandardPriceRange1 = StandardPriceRange1;
            objQueryModel.StandardPriceRange2 = StandardPriceRange2;
            objQueryModel.GroupCodeEqual = GroupCodeEqual;
            objQueryModel.GroupNameEqual = GroupNameEqual;
            objQueryModel.GroupFullIDEqual = GroupFullIDEqual;
            objQueryModel.GroupSortIDEqual = GroupSortIDEqual;
            SqlConnection connection = new SqlConnection(FunctionRule.GetConnectionString());
            try
            {
                models = new MaterialInventoryBLL().GetModels(objQueryModel, connection);
            }
            catch (Exception exception)
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
    }
}

