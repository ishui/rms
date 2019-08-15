namespace RmsPM.DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class HandMadeDAL
    {
        private SqlDataProcess _DataProcess;

        public HandMadeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public HandMadeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        public decimal GetSumCheckMoneyByContract(string ContractCode)
        {
            decimal @decimal = 0M;
            if (ContractCode != "")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("select sum(isnull(b.CheckMoney,0)) as sumcheckmoney from localevise a, localevisecost b where a.ViseCode=b.visecode and a.visecontractcode=@ContractCode");
                this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, ContractCode);
                this._DataProcess.CommandText = builder.ToString();
                SqlDataReader sqlDataReader = this._DataProcess.GetSqlDataReader();
                if (sqlDataReader.Read())
                {
                    @decimal = sqlDataReader.GetDecimal(0);
                }
            }
            return @decimal;
        }

        public DataSet GetVisesMoneySqlStringByCostGroup(string ViseCodes)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CostCode,CostBudgetSetCode,sum(CheckMoney) as 'Money' from localeviseCost where ViseCode in (" + ViseCodes + ") group by CostCode,CostBudgetSetCode");
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.GetDataSet();
        }
    }
}

