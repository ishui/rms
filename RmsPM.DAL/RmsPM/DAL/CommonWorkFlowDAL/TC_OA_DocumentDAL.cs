namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_DocumentDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_DocumentDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_DocumentDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_DocumentModel _DataBind(int Code)
        {
            TC_OA_DocumentModel model = new TC_OA_DocumentModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_Document ");
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
            builder.Append("delete from TC_OA_Document ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_DocumentModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_Document(");
            builder.Append("UserCode,UnitCode,ApplayDate,DocumentName,DocumentCode,Auditing)");
            builder.Append(" values (");
            builder.Append("@UserCode,@UnitCode,@ApplayDate,@DocumentName,@DocumentCode,@Auditing) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@ApplayDate", SqlDbType.DateTime, 8, mObj.ApplayDate);
            this._DataProcess.ProcessParametersAdd("@DocumentName", SqlDbType.VarChar, 50, mObj.DocumentName);
            this._DataProcess.ProcessParametersAdd("@DocumentCode", SqlDbType.VarChar, 50, mObj.DocumentCode);
            this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_DocumentModel> _Select(TC_OA_DocumentQueryModel qmObj)
        {
            List<TC_OA_DocumentModel> list = new List<TC_OA_DocumentModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_Document ");
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
                            TC_OA_DocumentModel model = new TC_OA_DocumentModel();
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

        private int _Update(TC_OA_DocumentModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_Document set ");
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.ApplayDate != DateTime.MinValue)
            {
                builder.Append("ApplayDate=@ApplayDate,");
                this._DataProcess.ProcessParametersAdd("@ApplayDate", SqlDbType.DateTime, 8, mObj.ApplayDate);
            }
            if (mObj.DocumentName != null)
            {
                builder.Append("DocumentName=@DocumentName,");
                this._DataProcess.ProcessParametersAdd("@DocumentName", SqlDbType.VarChar, 50, mObj.DocumentName);
            }
            if (mObj.DocumentCode != null)
            {
                builder.Append("DocumentCode=@DocumentCode,");
                this._DataProcess.ProcessParametersAdd("@DocumentCode", SqlDbType.VarChar, 50, mObj.DocumentCode);
            }
            if (mObj.Auditing != 0)
            {
                builder.Append("Auditing=@Auditing,");
                this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
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

        public int Delete(TC_OA_DocumentModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_DocumentModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_DocumentModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.ApplayDate = reader.GetDateTime(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.DocumentName = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.DocumentCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Auditing = reader.GetInt32(6);
            }
        }

        public int Insert(TC_OA_DocumentModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_DocumentModel> Select()
        {
            TC_OA_DocumentQueryModel qmObj = new TC_OA_DocumentQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_DocumentModel> Select(TC_OA_DocumentQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_DocumentModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

