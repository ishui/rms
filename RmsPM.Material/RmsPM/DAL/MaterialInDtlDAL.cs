namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class MaterialInDtlDAL
    {
        private SqlDataProcess _DataProcess;

        public MaterialInDtlDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public MaterialInDtlDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private MaterialInDtlModel _DataBind(int Code)
        {
            MaterialInDtlModel model = new MaterialInDtlModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from V_MaterialInDtl as MaterialInDtl ");
            builder.Append(" where MaterialInDtlCode=@MaterialInDtlCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialInDtlCode", SqlDbType.Int, 4, Code);
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlDataReader = this._DataProcess.GetSqlDataReader();
                while (sqlDataReader.Read())
                {
                    this.Initialize(sqlDataReader, model);
                }
            }
            catch (SqlException exception)
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
            return model;
        }

        private int _Delete(int Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from MaterialInDtl ");
            builder.Append(" where MaterialInDtlCode=@MaterialInDtlCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialInDtlCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(MaterialInDtlModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into MaterialInDtl(");
            builder.Append("MaterialInDtlCode, MaterialInCode,MaterialCode,Unit,InQty,InPrice,InMoney,OutQty)");
            builder.Append(" values (");
            builder.Append("@MaterialInDtlCode, @MaterialInCode,@MaterialCode,@Unit,@InQty,@InPrice,@InMoney,@OutQty) ");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialInDtlCode", SqlDbType.Int, 4, mObj.MaterialInDtlCode);
            this._DataProcess.ProcessParametersAdd("@MaterialInCode", SqlDbType.Int, 4, mObj.MaterialInCode);
            this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, (mObj.MaterialCode == 0) ? null : mObj.MaterialCode.ToString());
            this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            this._DataProcess.ProcessParametersAdd("@InQty", SqlDbType.Decimal, 9, mObj.InQty);
            this._DataProcess.ProcessParametersAdd("@InPrice", SqlDbType.Decimal, 9, mObj.InPrice);
            this._DataProcess.ProcessParametersAdd("@InMoney", SqlDbType.Decimal, 9, mObj.InMoney);
            this._DataProcess.ProcessParametersAdd("@OutQty", SqlDbType.Decimal, 9, mObj.OutQty);
            this._DataProcess.RunSql();
            return mObj.MaterialInDtlCode;
        }

        private List<MaterialInDtlModel> _Select(MaterialInDtlQueryModel qmObj)
        {
            List<MaterialInDtlModel> list = new List<MaterialInDtlModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from V_MaterialInDtl as MaterialInDtl ");
            builder.Append(qmObj.QueryConditionStr);
            if ((qmObj.SortColumns == null) || (qmObj.SortColumns.Length == 0))
            {
                builder.Append(" ORDER BY MaterialInDtlCode desc");
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
                        MaterialInDtlModel model = new MaterialInDtlModel();
                        this.Initialize(sqlDataReader, model);
                        list.Add(model);
                    }
                    num++;
                }
            }
            catch (SqlException exception)
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
            return list;
        }

        private int _Update(MaterialInDtlModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update MaterialInDtl set ");
            if (mObj.MaterialInCode != 0)
            {
                builder.Append("MaterialInCode=@MaterialInCode,");
                this._DataProcess.ProcessParametersAdd("@MaterialInCode", SqlDbType.Int, 4, mObj.MaterialInCode);
            }
            if (mObj.MaterialCode != 0)
            {
                builder.Append("MaterialCode=@MaterialCode,");
                this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, mObj.MaterialCode);
            }
            if (mObj.Unit != null)
            {
                builder.Append("Unit=@Unit,");
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            }
            if (mObj.InQty != 0M)
            {
                builder.Append("InQty=@InQty,");
                this._DataProcess.ProcessParametersAdd("@InQty", SqlDbType.Decimal, 9, mObj.InQty);
            }
            if (mObj.InPrice != 0M)
            {
                builder.Append("InPrice=@InPrice,");
                this._DataProcess.ProcessParametersAdd("@InPrice", SqlDbType.Decimal, 9, mObj.InPrice);
            }
            if (mObj.InMoney != 0M)
            {
                builder.Append("InMoney=@InMoney,");
                this._DataProcess.ProcessParametersAdd("@InMoney", SqlDbType.Decimal, 9, mObj.InMoney);
            }
            if (mObj.OutQty != 0M)
            {
                builder.Append("OutQty=@OutQty,");
                this._DataProcess.ProcessParametersAdd("@OutQty", SqlDbType.Decimal, 9, mObj.OutQty);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where MaterialInDtlCode=@MaterialInDtlCode");
            this._DataProcess.ProcessParametersAdd("@MaterialInDtlCode", SqlDbType.Int, 4, mObj.MaterialInDtlCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(MaterialInDtlModel mObj)
        {
            return this._Delete(mObj.MaterialInDtlCode);
        }

        public List<MaterialInDtlModel> GetMaterialInDtlListByContract(string ContractCode)
        {
            MaterialInDtlQueryModel qmObj = new MaterialInDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MaterialInDtlCode";
            qmObj.ContractCodeEqual = (ContractCode == "") ? "-1" : ContractCode.ToString();
            return this._Select(qmObj);
        }

        public List<MaterialInDtlModel> GetMaterialInDtlListByContract(string ContractCode, int MaterialCode)
        {
            MaterialInDtlQueryModel qmObj = new MaterialInDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MaterialInDtlCode";
            qmObj.ContractCodeEqual = (ContractCode == "") ? "-1" : ContractCode.ToString();
            qmObj.MaterialCodeEqual = (MaterialCode.ToString() == "0") ? "-1" : MaterialCode.ToString();
            return this._Select(qmObj);
        }

        public List<MaterialInDtlModel> GetMaterialInDtlListByMaterialInCode(int MaterialInCode)
        {
            MaterialInDtlQueryModel qmObj = new MaterialInDtlQueryModel();
            qmObj.StartRecord = 0;
            qmObj.MaxRecords = -1;
            qmObj.SortColumns = "MaterialInDtlCode";
            qmObj.MaterialInCodeEqual = (MaterialInCode.ToString() == "0") ? "-1" : MaterialInCode.ToString();
            return this._Select(qmObj);
        }

        public MaterialInDtlModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, MaterialInDtlModel obj)
        {
            try
            {
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInDtlCode")) != DBNull.Value)
                {
                    obj.MaterialInDtlCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInDtlCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInCode")) != DBNull.Value)
                {
                    obj.MaterialInCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode")) != DBNull.Value)
                {
                    obj.MaterialCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InQty")) != DBNull.Value)
                {
                    obj.InQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InPrice")) != DBNull.Value)
                {
                    obj.InPrice = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InPrice"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InMoney")) != DBNull.Value)
                {
                    obj.InMoney = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InMoney"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty")) != DBNull.Value)
                {
                    obj.OutQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutQty"));
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
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InvQty")) != DBNull.Value)
                {
                    obj.InvQty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InvQty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InvMoney")) != DBNull.Value)
                {
                    obj.InvMoney = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InvMoney"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInID")) != DBNull.Value)
                {
                    obj.MaterialInID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInID"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InDate")) != DBNull.Value)
                {
                    obj.InDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InDate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractCode")) != DBNull.Value)
                {
                    obj.ContractCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InGroupCode")) != DBNull.Value)
                {
                    obj.InGroupCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InGroupCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InGroupName")) != DBNull.Value)
                {
                    obj.InGroupName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InGroupName"));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int Insert(MaterialInDtlModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<MaterialInDtlModel> Select()
        {
            MaterialInDtlQueryModel qmObj = new MaterialInDtlQueryModel();
            return this._Select(qmObj);
        }

        public List<MaterialInDtlModel> Select(MaterialInDtlQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(MaterialInDtlModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

