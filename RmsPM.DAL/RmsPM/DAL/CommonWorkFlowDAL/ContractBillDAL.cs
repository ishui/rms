namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ContractBillDAL
    {
        private SqlDataProcess _DataProcess;

        public ContractBillDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public ContractBillDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private ContractBillModel _DataBind(int Code)
        {
            ContractBillModel model = new ContractBillModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from ContractBill ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            SqlDataReader sqlDataReader = null;
            try
            {
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
            builder.Append("delete from ContractBill ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(ContractBillModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ContractBill(");
            builder.Append("ProjectCode,ContractCode,BillNo,BillMoney)");
            builder.Append(" values (");
            builder.Append("@ProjectCode,@ContractCode,@BillNo,@BillMoney) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ProjectCode", SqlDbType.VarChar, 50, mObj.ProjectCode);
            this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, mObj.ContractCode);
            this._DataProcess.ProcessParametersAdd("@BillNo", SqlDbType.VarChar, 50, mObj.BillNo);
            this._DataProcess.ProcessParametersAdd("@BillMoney", SqlDbType.Decimal, 9, mObj.BillMoney);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<ContractBillModel> _Select(ContractBillQueryModel qmObj)
        {
            List<ContractBillModel> list = new List<ContractBillModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from ContractBill ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY Code desc");
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
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        if ((num >= qmObj.StartRecord) && ((list.Count < qmObj.MaxRecords) || (qmObj.MaxRecords == -1)))
                        {
                            ContractBillModel model = new ContractBillModel();
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

        private int _Update(ContractBillModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ContractBill set ");
            if (mObj.ProjectCode != null)
            {
                builder.Append("ProjectCode=@ProjectCode,");
                this._DataProcess.ProcessParametersAdd("@ProjectCode", SqlDbType.VarChar, 50, mObj.ProjectCode);
            }
            if (mObj.ContractCode != null)
            {
                builder.Append("ContractCode=@ContractCode,");
                this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, mObj.ContractCode);
            }
            if (mObj.BillNo != null)
            {
                builder.Append("BillNo=@BillNo,");
                this._DataProcess.ProcessParametersAdd("@BillNo", SqlDbType.VarChar, 50, mObj.BillNo);
            }
            if (mObj.BillMoney != 0M)
            {
                builder.Append("BillMoney=@BillMoney,");
                this._DataProcess.ProcessParametersAdd("@BillMoney", SqlDbType.Decimal, 9, mObj.BillMoney);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where Code=@Code");
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, mObj.Code);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(ContractBillModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public ContractBillModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, ContractBillModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ProjectCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.ContractCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.BillNo = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.BillMoney = reader.GetDecimal(4);
            }
        }

        public int Insert(ContractBillModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<ContractBillModel> Select()
        {
            ContractBillQueryModel qmObj = new ContractBillQueryModel();
            return this._Select(qmObj);
        }

        public List<ContractBillModel> Select(ContractBillQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(ContractBillModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

