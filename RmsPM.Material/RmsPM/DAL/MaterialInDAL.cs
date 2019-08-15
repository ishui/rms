namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class MaterialInDAL
    {
        private SqlDataProcess _DataProcess;

        public MaterialInDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public MaterialInDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private MaterialInModel _DataBind(int Code)
        {
            MaterialInModel model = new MaterialInModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from V_MaterialIn as MaterialIn ");
            builder.Append(" where MaterialInCode=@MaterialInCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialInCode", SqlDbType.Int, 4, Code);
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
            builder.Append("delete from MaterialIn ");
            builder.Append(" where MaterialInCode=@MaterialInCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialInCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(MaterialInModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into MaterialIn(");
            builder.Append("MaterialInCode, MaterialInID,ProjectCode,GroupCode,InDate,InPerson,Status,InputPerson,InputDate,CheckPerson,CheckDate,ContractCode,Remark)");
            builder.Append(" values (");
            builder.Append("@MaterialInCode, @MaterialInID,@ProjectCode,@GroupCode,@InDate,@InPerson,@Status,@InputPerson,@InputDate,@CheckPerson,@CheckDate,@ContractCode,@Remark) ");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialInCode", SqlDbType.Int, 4, mObj.MaterialInCode);
            this._DataProcess.ProcessParametersAdd("@MaterialInID", SqlDbType.VarChar, 50, mObj.MaterialInID);
            this._DataProcess.ProcessParametersAdd("@ProjectCode", SqlDbType.VarChar, 50, (mObj.ProjectCode == null) ? "" : mObj.ProjectCode);
            this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.VarChar, 50, mObj.GroupCode);
            this._DataProcess.ProcessParametersAdd("@InDate", SqlDbType.DateTime, 8, mObj.InDate);
            this._DataProcess.ProcessParametersAdd("@InPerson", SqlDbType.VarChar, 50, mObj.InPerson);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.Int, 4, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@InputPerson", SqlDbType.VarChar, 50, mObj.InputPerson);
            this._DataProcess.ProcessParametersAdd("@InputDate", SqlDbType.DateTime, 8, mObj.InputDate);
            this._DataProcess.ProcessParametersAdd("@CheckPerson", SqlDbType.VarChar, 50, mObj.CheckPerson);
            this._DataProcess.ProcessParametersAdd("@CheckDate", SqlDbType.DateTime, 8, mObj.CheckDate);
            this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, mObj.ContractCode);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
            this._DataProcess.RunSql();
            return mObj.MaterialInCode;
        }

        private List<MaterialInModel> _Select(MaterialInQueryModel qmObj)
        {
            List<MaterialInModel> list = new List<MaterialInModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from V_MaterialIn as MaterialIn ");
            builder.Append(qmObj.QueryConditionStr);
            if ((qmObj.SortColumns == null) || (qmObj.SortColumns.Length == 0))
            {
                builder.Append(" ORDER BY MaterialInCode desc");
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
                        MaterialInModel model = new MaterialInModel();
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

        private int _Update(MaterialInModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update MaterialIn set ");
            if (mObj.MaterialInID != null)
            {
                builder.Append("MaterialInID=@MaterialInID,");
                this._DataProcess.ProcessParametersAdd("@MaterialInID", SqlDbType.VarChar, 50, mObj.MaterialInID);
            }
            if (mObj.ProjectCode != null)
            {
                builder.Append("ProjectCode=@ProjectCode,");
                this._DataProcess.ProcessParametersAdd("@ProjectCode", SqlDbType.VarChar, 50, mObj.ProjectCode);
            }
            if (mObj.GroupCode != null)
            {
                builder.Append("GroupCode=@GroupCode,");
                this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.VarChar, 50, mObj.GroupCode);
            }
            if (mObj.InDate != DateTime.MinValue)
            {
                builder.Append("InDate=@InDate,");
                this._DataProcess.ProcessParametersAdd("@InDate", SqlDbType.DateTime, 8, mObj.InDate);
            }
            if (mObj.InPerson != null)
            {
                builder.Append("InPerson=@InPerson,");
                this._DataProcess.ProcessParametersAdd("@InPerson", SqlDbType.VarChar, 50, mObj.InPerson);
            }
            if (mObj.Status != 0)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.Int, 4, mObj.Status);
            }
            if (mObj.InputPerson != null)
            {
                builder.Append("InputPerson=@InputPerson,");
                this._DataProcess.ProcessParametersAdd("@InputPerson", SqlDbType.VarChar, 50, mObj.InputPerson);
            }
            if (mObj.InputDate != DateTime.MinValue)
            {
                builder.Append("InputDate=@InputDate,");
                this._DataProcess.ProcessParametersAdd("@InputDate", SqlDbType.DateTime, 8, mObj.InputDate);
            }
            if (mObj.CheckPerson != null)
            {
                builder.Append("CheckPerson=@CheckPerson,");
                this._DataProcess.ProcessParametersAdd("@CheckPerson", SqlDbType.VarChar, 50, mObj.CheckPerson);
            }
            if (mObj.CheckDate != DateTime.MinValue)
            {
                builder.Append("CheckDate=@CheckDate,");
                this._DataProcess.ProcessParametersAdd("@CheckDate", SqlDbType.DateTime, 8, mObj.CheckDate);
            }
            if (mObj.ContractCode != null)
            {
                builder.Append("ContractCode=@ContractCode,");
                this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, mObj.ContractCode);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where MaterialInCode=@MaterialInCode");
            this._DataProcess.ProcessParametersAdd("@MaterialInCode", SqlDbType.Int, 4, mObj.MaterialInCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(MaterialInModel mObj)
        {
            return this._Delete(mObj.MaterialInCode);
        }

        public MaterialInModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, MaterialInModel obj)
        {
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInCode")) != DBNull.Value)
            {
                obj.MaterialInCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInCode"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInID")) != DBNull.Value)
            {
                obj.MaterialInID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialInID"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode")) != DBNull.Value)
            {
                obj.ProjectCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode")) != DBNull.Value)
            {
                obj.GroupCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InDate")) != DBNull.Value)
            {
                obj.InDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InDate"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InPerson")) != DBNull.Value)
            {
                obj.InPerson = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InPerson"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Status")) != DBNull.Value)
            {
                obj.Status = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Status"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputPerson")) != DBNull.Value)
            {
                obj.InputPerson = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputPerson"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputDate")) != DBNull.Value)
            {
                obj.InputDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputDate"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "CheckPerson")) != DBNull.Value)
            {
                obj.CheckPerson = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "CheckPerson"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "CheckDate")) != DBNull.Value)
            {
                obj.CheckDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "CheckDate"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractCode")) != DBNull.Value)
            {
                obj.ContractCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractCode"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Remark")) != DBNull.Value)
            {
                obj.Remark = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Remark"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractID")) != DBNull.Value)
            {
                obj.ContractID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractID"));
            }
            if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractName")) != DBNull.Value)
            {
                obj.ContractName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ContractName"));
            }
        }

        public int Insert(MaterialInModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<MaterialInModel> Select()
        {
            MaterialInQueryModel qmObj = new MaterialInQueryModel();
            return this._Select(qmObj);
        }

        public List<MaterialInModel> Select(MaterialInQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(MaterialInModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

