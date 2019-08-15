namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class V_MaterialInventoryIODAL
    {
        private SqlDataProcess _DataProcess;

        public V_MaterialInventoryIODAL(SqlConnection Connection)
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

        public V_MaterialInventoryIODAL(SqlTransaction Transaction)
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

        private List<V_MaterialInventoryIOModel> _Select(V_MaterialInventoryIOQueryModel qmObj)
        {
            List<V_MaterialInventoryIOModel> list2;
            try
            {
                List<V_MaterialInventoryIOModel> list = new List<V_MaterialInventoryIOModel>();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from V_MaterialInventoryIO ");
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
                            V_MaterialInventoryIOModel model = new V_MaterialInventoryIOModel();
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

        private void Initialize(SqlDataReader reader, V_MaterialInventoryIOModel obj)
        {
            try
            {
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "IOType")) != DBNull.Value)
                {
                    obj.IOType = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "IOType"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "IOTypeName")) != DBNull.Value)
                {
                    obj.IOTypeName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "IOTypeName"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode")) != DBNull.Value)
                {
                    obj.MaterialCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode"));
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
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "IODate")) != DBNull.Value)
                {
                    obj.IODate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "IODate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InQty")) != DBNull.Value)
                {
                    obj.InQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty")) != DBNull.Value)
                {
                    obj.OutQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialIODtlCode")) != DBNull.Value)
                {
                    obj.MaterialIODtlCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialIODtlCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialIOCode")) != DBNull.Value)
                {
                    obj.MaterialIOCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialIOCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialIOID")) != DBNull.Value)
                {
                    obj.MaterialIOID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialIOID"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode")) != DBNull.Value)
                {
                    obj.ProjectCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode"));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<V_MaterialInventoryIOModel> Select()
        {
            List<V_MaterialInventoryIOModel> list;
            try
            {
                V_MaterialInventoryIOQueryModel qmObj = new V_MaterialInventoryIOQueryModel();
                list = this._Select(qmObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<V_MaterialInventoryIOModel> Select(V_MaterialInventoryIOQueryModel qmObj)
        {
            List<V_MaterialInventoryIOModel> list;
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

