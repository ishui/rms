namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class OAPersonContractDAL
    {
        private SqlDataProcess _DataProcess;

        public OAPersonContractDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public OAPersonContractDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private OAPersonContractModel _DataBind(int Code)
        {
            OAPersonContractModel model = new OAPersonContractModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonContract ");
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
            builder.Append("delete from GK_OAPersonContract ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(OAPersonContractModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPersonContract(");
            builder.Append("PersonID,ContractCode,RegisterDate,StartDate,EndDate,StationCode,Remark)");
            builder.Append(" values (");
            builder.Append("@PersonID,@ContractCode,@RegisterDate,@StartDate,@EndDate,@StationCode,@Remark) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@PersonID", SqlDbType.VarChar, 50, mObj.PersonID);
            this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, mObj.ContractCode);
            this._DataProcess.ProcessParametersAdd("@RegisterDate", SqlDbType.VarChar, 50, mObj.RegisterDate);
            this._DataProcess.ProcessParametersAdd("@StartDate", SqlDbType.VarChar, 50, mObj.StartDate);
            this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.VarChar, 50, mObj.EndDate);
            this._DataProcess.ProcessParametersAdd("@StationCode", SqlDbType.VarChar, 50, mObj.StationCode);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 50, mObj.Remark);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<OAPersonContractModel> _Select(OAPersonContractQueryModel qmObj)
        {
            List<OAPersonContractModel> list = new List<OAPersonContractModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonContract ");
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
                            OAPersonContractModel model = new OAPersonContractModel();
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

        private int _Update(OAPersonContractModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPersonContract set ");
            if (mObj.PersonID != null)
            {
                builder.Append("PersonID=@PersonID,");
                this._DataProcess.ProcessParametersAdd("@PersonID", SqlDbType.VarChar, 50, mObj.PersonID);
            }
            if (mObj.ContractCode != null)
            {
                builder.Append("ContractCode=@ContractCode,");
                this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, mObj.ContractCode);
            }
            if (mObj.RegisterDate != null)
            {
                builder.Append("RegisterDate=@RegisterDate,");
                this._DataProcess.ProcessParametersAdd("@RegisterDate", SqlDbType.VarChar, 50, mObj.RegisterDate);
            }
            if (mObj.StartDate != null)
            {
                builder.Append("StartDate=@StartDate,");
                this._DataProcess.ProcessParametersAdd("@StartDate", SqlDbType.VarChar, 50, mObj.StartDate);
            }
            if (mObj.EndDate != null)
            {
                builder.Append("EndDate=@EndDate,");
                this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.VarChar, 50, mObj.EndDate);
            }
            if (mObj.StationCode != null)
            {
                builder.Append("StationCode=@StationCode,");
                this._DataProcess.ProcessParametersAdd("@StationCode", SqlDbType.VarChar, 50, mObj.StationCode);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 50, mObj.Remark);
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

        public int Delete(OAPersonContractModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public OAPersonContractModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, OAPersonContractModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.PersonID = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.ContractCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.RegisterDate = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.StartDate = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.EndDate = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.StationCode = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Remark = reader.GetString(7);
            }
        }

        public int Insert(OAPersonContractModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<OAPersonContractModel> Select()
        {
            OAPersonContractQueryModel qmObj = new OAPersonContractQueryModel();
            return this._Select(qmObj);
        }

        public List<OAPersonContractModel> Select(OAPersonContractQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(OAPersonContractModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

