namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class MaterialOutDAL
    {
        private SqlDataProcess _DataProcess;

        public MaterialOutDAL(SqlConnection Connection)
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

        public MaterialOutDAL(SqlTransaction Transaction)
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

        private MaterialOutModel _DataBind(int Code)
        {
            MaterialOutModel model2;
            try
            {
                MaterialOutModel model = new MaterialOutModel();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from V_MaterialOut as MaterialOut ");
                builder.Append(" where MaterialOutCode=@MaterialOutCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@MaterialOutCode", SqlDbType.Int, 4, Code);
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
                builder.Append("delete from MaterialOut ");
                builder.Append(" where MaterialOutCode=@MaterialOutCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@MaterialOutCode", SqlDbType.Int, 4, Code);
                num = this._DataProcess.RunSql();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        private int _Insert(MaterialOutModel mObj)
        {
            int materialOutCode;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into MaterialOut(");
                builder.Append("MaterialOutCode,MaterialOutID,ProjectCode,GroupCode,OutDate,OutPerson,Status,InputPerson,InputDate,CheckPerson,CheckDate,ContractCode,Remark)");
                builder.Append(" values (");
                builder.Append("@MaterialOutCode,@MaterialOutID,@ProjectCode,@GroupCode,@OutDate,@OutPerson,@Status,@InputPerson,@InputDate,@CheckPerson,@CheckDate,@ContractCode,@Remark) ");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@MaterialOutCode", SqlDbType.Int, 4, mObj.MaterialOutCode);
                this._DataProcess.ProcessParametersAdd("@MaterialOutID", SqlDbType.VarChar, 50, mObj.MaterialOutID);
                this._DataProcess.ProcessParametersAdd("@ProjectCode", SqlDbType.VarChar, 50, mObj.ProjectCode);
                this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.VarChar, 50, mObj.GroupCode);
                this._DataProcess.ProcessParametersAdd("@OutDate", SqlDbType.DateTime, 8, mObj.OutDate);
                this._DataProcess.ProcessParametersAdd("@OutPerson", SqlDbType.VarChar, 50, mObj.OutPerson);
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.Int, 4, mObj.Status);
                this._DataProcess.ProcessParametersAdd("@InputPerson", SqlDbType.VarChar, 50, mObj.InputPerson);
                this._DataProcess.ProcessParametersAdd("@InputDate", SqlDbType.DateTime, 8, mObj.InputDate);
                this._DataProcess.ProcessParametersAdd("@CheckPerson", SqlDbType.VarChar, 50, mObj.CheckPerson);
                this._DataProcess.ProcessParametersAdd("@CheckDate", SqlDbType.DateTime, 8, mObj.CheckDate);
                this._DataProcess.ProcessParametersAdd("@ContractCode", SqlDbType.VarChar, 50, mObj.ContractCode);
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
                this._DataProcess.RunSql();
                materialOutCode = mObj.MaterialOutCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return materialOutCode;
        }

        private List<MaterialOutModel> _Select(MaterialOutQueryModel qmObj)
        {
            List<MaterialOutModel> list2;
            try
            {
                List<MaterialOutModel> list = new List<MaterialOutModel>();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from V_MaterialOut as MaterialOut ");
                builder.Append(qmObj.QueryConditionStr);
                if ((qmObj.SortColumns == null) || (qmObj.SortColumns.Length == 0))
                {
                    builder.Append(" ORDER BY MaterialOutCode desc");
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
                            MaterialOutModel model = new MaterialOutModel();
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

        private int _Update(MaterialOutModel mObj)
        {
            int num;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update MaterialOut set ");
                if (mObj.MaterialOutID != null)
                {
                    builder.Append("MaterialOutID=@MaterialOutID,");
                    this._DataProcess.ProcessParametersAdd("@MaterialOutID", SqlDbType.VarChar, 50, mObj.MaterialOutID);
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
                if (mObj.OutDate != DateTime.MinValue)
                {
                    builder.Append("OutDate=@OutDate,");
                    this._DataProcess.ProcessParametersAdd("@OutDate", SqlDbType.DateTime, 8, mObj.OutDate);
                }
                if (mObj.OutPerson != null)
                {
                    builder.Append("OutPerson=@OutPerson,");
                    this._DataProcess.ProcessParametersAdd("@OutPerson", SqlDbType.VarChar, 50, mObj.OutPerson);
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
                builder.Append(" where MaterialOutCode=@MaterialOutCode");
                this._DataProcess.ProcessParametersAdd("@MaterialOutCode", SqlDbType.Int, 4, mObj.MaterialOutCode);
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

        public int Delete(MaterialOutModel mObj)
        {
            int num;
            try
            {
                num = this._Delete(mObj.MaterialOutCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public MaterialOutModel GetModel(int Code)
        {
            MaterialOutModel model;
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

        private void Initialize(SqlDataReader reader, MaterialOutModel obj)
        {
            try
            {
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutCode")) != DBNull.Value)
                {
                    obj.MaterialOutCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutID")) != DBNull.Value)
                {
                    obj.MaterialOutID = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "MaterialOutID"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode")) != DBNull.Value)
                {
                    obj.ProjectCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode")) != DBNull.Value)
                {
                    obj.GroupCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutDate")) != DBNull.Value)
                {
                    obj.OutDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutDate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutPerson")) != DBNull.Value)
                {
                    obj.OutPerson = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "OutPerson"));
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
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int Insert(MaterialOutModel mObj)
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

        public List<MaterialOutModel> Select()
        {
            List<MaterialOutModel> list;
            try
            {
                MaterialOutQueryModel qmObj = new MaterialOutQueryModel();
                list = this._Select(qmObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<MaterialOutModel> Select(MaterialOutQueryModel qmObj)
        {
            List<MaterialOutModel> list;
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

        public int Update(MaterialOutModel mObj)
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

