namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class V_MaterialInventoryDAL
    {
        private SqlDataProcess _DataProcess;

        public V_MaterialInventoryDAL(SqlConnection Connection)
        {
            try
            {
                this._DataProcess = new SqlDataProcess(Connection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public V_MaterialInventoryDAL(SqlTransaction Transaction)
        {
            try
            {
                this._DataProcess = new SqlDataProcess(Transaction);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private List<V_MaterialInventoryModel> _Select(V_MaterialInventoryQueryModel qmObj)
        {
            List<V_MaterialInventoryModel> list2;
            try
            {
                List<V_MaterialInventoryModel> list = new List<V_MaterialInventoryModel>();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from V_MaterialInventory ");
                builder.Append(qmObj.QueryConditionStr);
                if ((qmObj.SortColumns != null) && (qmObj.SortColumns.Length != 0))
                {
                    builder.Append(" ORDER BY " + qmObj.SortColumns);
                }
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.SqlParameters = qmObj.Parameters;
                SqlDataReader sqlDataReader = null;
                int num = 0;
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        if ((num >= qmObj.StartRecord) && ((list.Count < qmObj.MaxRecords) || (qmObj.MaxRecords == -1)))
                        {
                            V_MaterialInventoryModel model = new V_MaterialInventoryModel();
                            this.Initialize(sqlDataReader, model);
                            list.Add(model);
                        }
                        num++;
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }
                }
                list2 = list;
            }
            catch (Exception exception2)
            {
                throw exception2;
            }
            return list2;
        }

        private void Initialize(SqlDataReader reader, V_MaterialInventoryModel obj)
        {
            try
            {
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode")) != DBNull.Value)
                {
                    obj.ProjectCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectName")) != DBNull.Value)
                {
                    obj.ProjectName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectName"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode")) != DBNull.Value)
                {
                    obj.MaterialCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InQty")) != DBNull.Value)
                {
                    obj.InQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InMoney")) != DBNull.Value)
                {
                    obj.InMoney = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InMoney"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty")) != DBNull.Value)
                {
                    obj.OutQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InvQty")) != DBNull.Value)
                {
                    obj.InvQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InvQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialName")) != DBNull.Value)
                {
                    obj.MaterialName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialName"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Spec")) != DBNull.Value)
                {
                    obj.Spec = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Spec"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Unit")) != DBNull.Value)
                {
                    obj.Unit = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Unit"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "StandardPrice")) != DBNull.Value)
                {
                    obj.StandardPrice = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "StandardPrice"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode")) != DBNull.Value)
                {
                    obj.GroupCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupName")) != DBNull.Value)
                {
                    obj.GroupName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupName"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupFullID")) != DBNull.Value)
                {
                    obj.GroupFullID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupFullID"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupSortID")) != DBNull.Value)
                {
                    obj.GroupSortID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupSortID"));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<V_MaterialInventoryModel> Select()
        {
            List<V_MaterialInventoryModel> list;
            try
            {
                V_MaterialInventoryQueryModel qmObj = new V_MaterialInventoryQueryModel();
                list = this._Select(qmObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryModel> Select(V_MaterialInventoryQueryModel qmObj)
        {
            List<V_MaterialInventoryModel> list;
            try
            {
                list = this._Select(qmObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }
    }
}

