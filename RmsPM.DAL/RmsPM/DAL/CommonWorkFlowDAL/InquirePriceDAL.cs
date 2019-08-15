namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class InquirePriceDAL
    {
        private SqlDataProcess _DataProcess;

        public InquirePriceDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public InquirePriceDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private InquirePriceModel _DataBind(int Code)
        {
            InquirePriceModel model = new InquirePriceModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from InquirePrice ");
            builder.Append(" where InquirePriceCode=@InquirePriceCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@InquirePriceCode", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from InquirePrice ");
            builder.Append(" where InquirePriceCode=@InquirePriceCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@InquirePriceCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(InquirePriceModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into InquirePrice(");
            builder.Append("ProjectName,InquireObject,Requirement,Aduiting,Field1)");
            builder.Append(" values (");
            builder.Append("@ProjectName,@InquireObject,@Requirement,@Aduiting,@Field1) ");
            builder.Append("SELECT @InquirePriceCode = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 50, mObj.ProjectName);
            this._DataProcess.ProcessParametersAdd("@InquireObject", SqlDbType.VarChar, 50, mObj.InquireObject);
            this._DataProcess.ProcessParametersAdd("@Requirement", SqlDbType.Text, 0x7fffffff, mObj.Requirement);
            this._DataProcess.ProcessParametersAdd("@Aduiting", SqlDbType.Int, 4, mObj.Aduiting);
            this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@InquirePriceCode", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.InquirePriceCode = (int) parameter.Value;
            return mObj.InquirePriceCode;
        }

        private List<InquirePriceModel> _Select(InquirePriceQueryModel qmObj)
        {
            List<InquirePriceModel> list = new List<InquirePriceModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from InquirePrice ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY InquirePriceCode desc");
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
                            InquirePriceModel model = new InquirePriceModel();
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

        private int _Update(InquirePriceModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update InquirePrice set ");
            if (mObj.ProjectName != null)
            {
                builder.Append("ProjectName=@ProjectName,");
                this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 50, mObj.ProjectName);
            }
            if (mObj.InquireObject != null)
            {
                builder.Append("InquireObject=@InquireObject,");
                this._DataProcess.ProcessParametersAdd("@InquireObject", SqlDbType.VarChar, 50, mObj.InquireObject);
            }
            if (mObj.Requirement != null)
            {
                builder.Append("Requirement=@Requirement,");
                this._DataProcess.ProcessParametersAdd("@Requirement", SqlDbType.Text, 0x7fffffff, mObj.Requirement);
            }
            if (mObj.Aduiting != 0)
            {
                builder.Append("Aduiting=@Aduiting,");
                this._DataProcess.ProcessParametersAdd("@Aduiting", SqlDbType.Int, 4, mObj.Aduiting);
            }
            if (mObj.Field1 != null)
            {
                builder.Append("Field1=@Field1,");
                this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where InquirePriceCode=@InquirePriceCode");
            this._DataProcess.ProcessParametersAdd("@InquirePriceCode", SqlDbType.Int, 4, mObj.InquirePriceCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(InquirePriceModel mObj)
        {
            return this._Delete(mObj.InquirePriceCode);
        }

        public InquirePriceModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, InquirePriceModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.InquirePriceCode = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ProjectName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.InquireObject = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Requirement = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Aduiting = reader.GetInt32(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Field1 = reader.GetString(5);
            }
        }

        public int Insert(InquirePriceModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<InquirePriceModel> Select()
        {
            InquirePriceQueryModel qmObj = new InquirePriceQueryModel();
            return this._Select(qmObj);
        }

        public List<InquirePriceModel> Select(InquirePriceQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(InquirePriceModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

