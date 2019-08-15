namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class MaterialOutDtlDAL
    {
        public SqlDataProcess _DataProcess;

        public MaterialOutDtlDAL(SqlConnection Connection)
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

        public MaterialOutDtlDAL(SqlTransaction Transaction)
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

        private MaterialOutDtlModel _DataBind(int Code)
        {
            MaterialOutDtlModel model2;
            try
            {
                MaterialOutDtlModel model = new MaterialOutDtlModel();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from V_MaterialOutDtl as MaterialOutDtl ");
                builder.Append(" where MaterialOutDtlCode=@MaterialOutDtlCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@MaterialOutDtlCode", SqlDbType.Int, 4, Code);
                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        this.Initialize(sqlDataReader, model);
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
                model2 = model;
            }
            catch (Exception exception2)
            {
                throw exception2;
            }
            return model2;
        }

        private int _Delete(int Code)
        {
            int num;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("delete from MaterialOutDtl ");
                builder.Append(" where MaterialOutDtlCode=@MaterialOutDtlCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@MaterialOutDtlCode", SqlDbType.Int, 4, Code);
                num = this._DataProcess.RunSql();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        private int _Insert(MaterialOutDtlModel mObj)
        {
            int materialOutDtlCode;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into MaterialOutDtl(");
                builder.Append("MaterialOutDtlCode,MaterialOutCode,MaterialCode,MaterialInDtlCode,OutQty,OutPrice,OutMoney)");
                builder.Append(" values (");
                builder.Append("@MaterialOutDtlCode,@MaterialOutCode,@MaterialCode,@MaterialInDtlCode,@OutQty,@OutPrice,@OutMoney) ");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@MaterialOutDtlCode", SqlDbType.Int, 4, mObj.MaterialOutDtlCode);
                this._DataProcess.ProcessParametersAdd("@MaterialOutCode", SqlDbType.Int, 4, mObj.MaterialOutCode);
                this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, (mObj.MaterialCode == 0) ? null : mObj.MaterialCode.ToString());
                this._DataProcess.ProcessParametersAdd("@MaterialInDtlCode", SqlDbType.Int, 4, (mObj.MaterialInDtlCode == 0) ? null : mObj.MaterialInDtlCode.ToString());
                this._DataProcess.ProcessParametersAdd("@OutQty", SqlDbType.Decimal, 9, mObj.OutQty);
                this._DataProcess.ProcessParametersAdd("@OutPrice", SqlDbType.Decimal, 9, mObj.OutPrice);
                this._DataProcess.ProcessParametersAdd("@OutMoney", SqlDbType.Decimal, 9, mObj.OutMoney);
                this._DataProcess.RunSql();
                materialOutDtlCode = mObj.MaterialOutDtlCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return materialOutDtlCode;
        }

        private List<MaterialOutDtlModel> _Select(MaterialOutDtlQueryModel qmObj)
        {
            List<MaterialOutDtlModel> list2;
            try
            {
                List<MaterialOutDtlModel> list = new List<MaterialOutDtlModel>();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from V_MaterialOutDtl as MaterialOutDtl ");
                builder.Append(qmObj.QueryConditionStr);
                if ((qmObj.SortColumns == null) || (qmObj.SortColumns.Length == 0))
                {
                    builder.Append(" ORDER BY MaterialOutDtlCode desc");
                }
                else
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
                            MaterialOutDtlModel model = new MaterialOutDtlModel();
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

        private int _Update(MaterialOutDtlModel mObj)
        {
            int num;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update MaterialOutDtl set ");
                if (mObj.MaterialOutCode != 0)
                {
                    builder.Append("MaterialOutCode=@MaterialOutCode,");
                    this._DataProcess.ProcessParametersAdd("@MaterialOutCode", SqlDbType.Int, 4, mObj.MaterialOutCode);
                }
                if (mObj.MaterialCode != 0)
                {
                    builder.Append("MaterialCode=@MaterialCode,");
                    this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, mObj.MaterialCode);
                }
                if (mObj.MaterialInDtlCode != 0)
                {
                    builder.Append("MaterialInDtlCode=@MaterialInDtlCode,");
                    this._DataProcess.ProcessParametersAdd("@MaterialInDtlCode", SqlDbType.Int, 4, mObj.MaterialInDtlCode);
                }
                if (mObj.OutQty != 0M)
                {
                    builder.Append("OutQty=@OutQty,");
                    this._DataProcess.ProcessParametersAdd("@OutQty", SqlDbType.Decimal, 9, mObj.OutQty);
                }
                if (mObj.OutPrice != 0M)
                {
                    builder.Append("OutPrice=@OutPrice,");
                    this._DataProcess.ProcessParametersAdd("@OutPrice", SqlDbType.Decimal, 9, mObj.OutPrice);
                }
                if (mObj.OutMoney != 0M)
                {
                    builder.Append("OutMoney=@OutMoney,");
                    this._DataProcess.ProcessParametersAdd("@OutMoney", SqlDbType.Decimal, 9, mObj.OutMoney);
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(" where MaterialOutDtlCode=@MaterialOutDtlCode");
                this._DataProcess.ProcessParametersAdd("@MaterialOutDtlCode", SqlDbType.Int, 4, mObj.MaterialOutDtlCode);
                this._DataProcess.CommandText = builder.ToString();
                num = this._DataProcess.RunSql();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Delete(int Code)
        {
            int num;
            try
            {
                num = this._Delete(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int Delete(MaterialOutDtlModel mObj)
        {
            int num;
            try
            {
                num = this._Delete(mObj.MaterialOutDtlCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public List<MaterialOutDtlModel> GetMaterialOutDtlListByContract(string ContractCode)
        {
            MaterialOutDtlQueryModel qmObj = new MaterialOutDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MaterialOutDtlCode";
            qmObj.ContractCodeEqual = (ContractCode == "") ? "-1" : ContractCode;
            return this.Select(qmObj);
        }

        public List<MaterialOutDtlModel> GetMaterialOutDtlListByContract(string ContractCode, int MaterialCode)
        {
            MaterialOutDtlQueryModel qmObj = new MaterialOutDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MaterialOutDtlCode";
            qmObj.ContractCodeEqual = (ContractCode == "") ? "-1" : ContractCode.ToString();
            qmObj.MaterialCodeEqual = (MaterialCode.ToString() == "0") ? "-1" : MaterialCode.ToString();
            return this._Select(qmObj);
        }

        public List<MaterialOutDtlModel> GetMaterialOutDtlListByMaterialInCode(int MaterialInCode)
        {
            MaterialOutDtlQueryModel qmObj = new MaterialOutDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MaterialOutDtlCode";
            qmObj.MaterialInCodeEqual = (MaterialInCode.ToString() == "0") ? "-1" : MaterialInCode.ToString();
            return this.Select(qmObj);
        }

        public List<MaterialOutDtlModel> GetMaterialOutDtlListByMaterialOutCode(int MaterialOutCode)
        {
            MaterialOutDtlQueryModel qmObj = new MaterialOutDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MaterialOutDtlCode";
            qmObj.MaterialOutCodeEqual = (MaterialOutCode.ToString() == "0") ? "-1" : MaterialOutCode.ToString();
            return this.Select(qmObj);
        }

        public MaterialOutDtlModel GetModel(int Code)
        {
            MaterialOutDtlModel model;
            try
            {
                model = this._DataBind(Code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return model;
        }

        private void Initialize(SqlDataReader reader, MaterialOutDtlModel obj)
        {
            try
            {
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutDtlCode")) != DBNull.Value)
                {
                    obj.MaterialOutDtlCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutDtlCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutCode")) != DBNull.Value)
                {
                    obj.MaterialOutCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode")) != DBNull.Value)
                {
                    obj.MaterialCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInDtlCode")) != DBNull.Value)
                {
                    obj.MaterialInDtlCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInDtlCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty")) != DBNull.Value)
                {
                    obj.OutQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutPrice")) != DBNull.Value)
                {
                    obj.OutPrice = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutPrice"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutMoney")) != DBNull.Value)
                {
                    obj.OutMoney = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutMoney"));
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
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutID")) != DBNull.Value)
                {
                    obj.MaterialOutID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutID"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutDate")) != DBNull.Value)
                {
                    obj.OutDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutDate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractCode")) != DBNull.Value)
                {
                    obj.ContractCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInCode")) != DBNull.Value)
                {
                    obj.MaterialInCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInID")) != DBNull.Value)
                {
                    obj.MaterialInID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInID"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InDate")) != DBNull.Value)
                {
                    obj.InDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InDate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InContractCode")) != DBNull.Value)
                {
                    obj.InContractCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InContractCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InPrice")) != DBNull.Value)
                {
                    obj.InPrice = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InPrice"));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int Insert(MaterialOutDtlModel mObj)
        {
            int num;
            try
            {
                num = this._Insert(mObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public List<MaterialOutDtlModel> Select()
        {
            List<MaterialOutDtlModel> list;
            try
            {
                MaterialOutDtlQueryModel qmObj = new MaterialOutDtlQueryModel();
                list = this._Select(qmObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutDtlModel> Select(MaterialOutDtlQueryModel qmObj)
        {
            List<MaterialOutDtlModel> list;
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

        public int Update(MaterialOutDtlModel mObj)
        {
            int num;
            try
            {
                num = this._Update(mObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }
    }
}

