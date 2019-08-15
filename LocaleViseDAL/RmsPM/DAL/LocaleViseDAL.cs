namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class LocaleViseDAL
    {
        private SqlDataProcess _DataProcess;

        public LocaleViseDAL(SqlConnection Connection)
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

        public LocaleViseDAL(SqlTransaction Transaction)
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

        private LocaleViseModel _DataBind(int Code)
        {
            Exception exception;
            LocaleViseModel model2;
            try
            {
                LocaleViseModel model = new LocaleViseModel();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from LocaleVise");
                builder.Append(" where ViseCode=@ViseCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@ViseCode", SqlDbType.Int, 4, Code);
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
                    catch (Exception exception1)
                    {
                        exception = exception1;
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
                model2 = model;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
            return model2;
        }

        private int _Delete(int Code)
        {
            int num;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("delete from LocaleVise ");
                builder.Append(" where ViseCode=@ViseCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@ViseCode", SqlDbType.Int, 4, Code);
                num = this._DataProcess.RunSql();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        private int _Insert(LocaleViseModel mObj)
        {
            int viseCode;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into LocaleVise(");
                builder.Append("ViseId,ViseName,ViseDate,ViseType,VisePerson,ViseSupplier,ViseUnit,ViseEndDate,ViseRemark,ViseScrutiny,ViseReason,ViseStatus,ViseBalanceStatus,ViseProject,ViseContractCode,ViseReasonItem,ViseComeToMoney,ViseID2,ViseReferCode)");
                builder.Append(" values (");
                builder.Append("@ViseId,@ViseName,@ViseDate,@ViseType,@VisePerson,@ViseSupplier,@ViseUnit,@ViseEndDate,@ViseRemark,@ViseScrutiny,@ViseReason,@ViseStatus,@ViseBalanceStatus,@ViseProject,@ViseContractCode,@ViseReasonItem,@ViseComeToMoney,@ViseID2,@ViseReferCode) ");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@ViseId", SqlDbType.VarChar, 50, mObj.ViseId);
                this._DataProcess.ProcessParametersAdd("@ViseName", SqlDbType.VarChar, 50, mObj.ViseName);
                this._DataProcess.ProcessParametersAdd("@ViseDate", SqlDbType.DateTime, 8, mObj.ViseDate);
                this._DataProcess.ProcessParametersAdd("@ViseType", SqlDbType.VarChar, 50, mObj.ViseType);
                this._DataProcess.ProcessParametersAdd("@VisePerson", SqlDbType.VarChar, 50, mObj.VisePerson);
                this._DataProcess.ProcessParametersAdd("@ViseSupplier", SqlDbType.VarChar, 50, mObj.ViseSupplier);
                this._DataProcess.ProcessParametersAdd("@ViseUnit", SqlDbType.VarChar, 50, mObj.ViseUnit);
                this._DataProcess.ProcessParametersAdd("@ViseEndDate", SqlDbType.DateTime, 8, mObj.ViseEndDate);
                this._DataProcess.ProcessParametersAdd("@ViseRemark", SqlDbType.VarChar, 0x7d0, mObj.ViseRemark);
                this._DataProcess.ProcessParametersAdd("@ViseScrutiny", SqlDbType.VarChar, 0x7d0, mObj.ViseScrutiny);
                this._DataProcess.ProcessParametersAdd("@ViseReason", SqlDbType.VarChar, 0x7d0, mObj.ViseReason);
                this._DataProcess.ProcessParametersAdd("@ViseStatus", SqlDbType.Int, 4, mObj.ViseStatus);
                this._DataProcess.ProcessParametersAdd("@ViseBalanceStatus", SqlDbType.Int, 4, mObj.ViseBalanceStatus);
                this._DataProcess.ProcessParametersAdd("@ViseProject", SqlDbType.VarChar, 50, mObj.ViseProject);
                this._DataProcess.ProcessParametersAdd("@ViseContractCode", SqlDbType.VarChar, 50, mObj.ViseContractCode);
                this._DataProcess.ProcessParametersAdd("@ViseReasonItem", SqlDbType.VarChar, 0x1f40, mObj.ViseReasonItem);
                this._DataProcess.ProcessParametersAdd("@ViseComeToMoney", SqlDbType.Decimal, 9, mObj.ViseComeToMoney);
                this._DataProcess.ProcessParametersAdd("@ViseID2", SqlDbType.VarChar, 50, mObj.ViseID2);
                this._DataProcess.ProcessParametersAdd("@ViseReferCode", SqlDbType.Int, 4, (mObj.ViseReferCode == 0) ? 0 : mObj.ViseReferCode);
                this._DataProcess.RunSql();
                viseCode = mObj.ViseCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return viseCode;
        }

        private List<LocaleViseModel> _Select(LocaleViseQueryModel qmObj)
        {
            Exception exception;
            List<LocaleViseModel> list2;
            try
            {
                List<LocaleViseModel> list = new List<LocaleViseModel>();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from LocaleVise ");
                builder.Append(qmObj.QueryConditionStr);
                if ((qmObj.SortColumns == null) || (qmObj.SortColumns.Length == 0))
                {
                    builder.Append(" ORDER BY ViseCode desc");
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
                                LocaleViseModel model = new LocaleViseModel();
                                this.Initialize(sqlDataReader, model);
                                list.Add(model);
                            }
                            num++;
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
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
                list2 = list;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
            return list2;
        }

        private int _Update(LocaleViseModel mObj)
        {
            int num;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update LocaleVise set ");
                if (mObj.ViseId != null)
                {
                    builder.Append("ViseId=@ViseId,");
                    this._DataProcess.ProcessParametersAdd("@ViseId", SqlDbType.VarChar, 50, mObj.ViseId);
                }
                if (mObj.ViseName != null)
                {
                    builder.Append("ViseName=@ViseName,");
                    this._DataProcess.ProcessParametersAdd("@ViseName", SqlDbType.VarChar, 50, mObj.ViseName);
                }
                if (mObj.ViseDate != DateTime.MinValue)
                {
                    builder.Append("ViseDate=@ViseDate,");
                    this._DataProcess.ProcessParametersAdd("@ViseDate", SqlDbType.DateTime, 8, mObj.ViseDate);
                }
                if (mObj.ViseType != null)
                {
                    builder.Append("ViseType=@ViseType,");
                    this._DataProcess.ProcessParametersAdd("@ViseType", SqlDbType.VarChar, 50, mObj.ViseType);
                }
                if (mObj.VisePerson != null)
                {
                    builder.Append("VisePerson=@VisePerson,");
                    this._DataProcess.ProcessParametersAdd("@VisePerson", SqlDbType.VarChar, 50, mObj.VisePerson);
                }
                if (mObj.ViseSupplier != null)
                {
                    builder.Append("ViseSupplier=@ViseSupplier,");
                    this._DataProcess.ProcessParametersAdd("@ViseSupplier", SqlDbType.VarChar, 50, mObj.ViseSupplier);
                }
                if (mObj.ViseUnit != null)
                {
                    builder.Append("ViseUnit=@ViseUnit,");
                    this._DataProcess.ProcessParametersAdd("@ViseUnit", SqlDbType.VarChar, 50, mObj.ViseUnit);
                }
                if (mObj.ViseEndDate != DateTime.MinValue)
                {
                    builder.Append("ViseEndDate=@ViseEndDate,");
                    this._DataProcess.ProcessParametersAdd("@ViseEndDate", SqlDbType.DateTime, 8, mObj.ViseEndDate);
                }
                if (mObj.ViseRemark != null)
                {
                    builder.Append("ViseRemark=@ViseRemark,");
                    this._DataProcess.ProcessParametersAdd("@ViseRemark", SqlDbType.VarChar, 0x7d0, mObj.ViseRemark);
                }
                if (mObj.ViseScrutiny != null)
                {
                    builder.Append("ViseScrutiny=@ViseScrutiny,");
                    this._DataProcess.ProcessParametersAdd("@ViseScrutiny", SqlDbType.VarChar, 0x7d0, mObj.ViseScrutiny);
                }
                if (mObj.ViseReason != null)
                {
                    builder.Append("ViseReason=@ViseReason,");
                    this._DataProcess.ProcessParametersAdd("@ViseReason", SqlDbType.VarChar, 0x7d0, mObj.ViseReason);
                }
                if (mObj.ViseStatus != 0)
                {
                    builder.Append("ViseStatus=@ViseStatus,");
                    this._DataProcess.ProcessParametersAdd("@ViseStatus", SqlDbType.Int, 4, mObj.ViseStatus);
                }
                if (mObj.ViseBalanceStatus != 0)
                {
                    builder.Append("ViseBalanceStatus=@ViseBalanceStatus,");
                    this._DataProcess.ProcessParametersAdd("@ViseBalanceStatus", SqlDbType.Int, 4, mObj.ViseBalanceStatus);
                }
                if (mObj.ViseProject != null)
                {
                    builder.Append("ViseProject=@ViseProject,");
                    this._DataProcess.ProcessParametersAdd("@ViseProject", SqlDbType.VarChar, 50, mObj.ViseProject);
                }
                if (mObj.ViseContractCode != null)
                {
                    builder.Append("ViseContractCode=@ViseContractCode,");
                    this._DataProcess.ProcessParametersAdd("@ViseContractCode", SqlDbType.VarChar, 50, mObj.ViseContractCode);
                }
                if (mObj.ViseReasonItem != null)
                {
                    builder.Append("ViseReasonItem=@ViseReasonItem,");
                    this._DataProcess.ProcessParametersAdd("@ViseReasonItem", SqlDbType.VarChar, 0x1f40, mObj.ViseReasonItem);
                }
                if (mObj.ViseComeToMoney != 0M)
                {
                    builder.Append("ViseComeToMoney=@ViseComeToMoney,");
                    this._DataProcess.ProcessParametersAdd("@ViseComeToMoney", SqlDbType.Decimal, 9, mObj.ViseComeToMoney);
                }
                if (mObj.ViseID2 != null)
                {
                    builder.Append("ViseID2=@ViseID2,");
                    this._DataProcess.ProcessParametersAdd("@ViseID2", SqlDbType.VarChar, 50, mObj.ViseID2);
                }
                if (mObj.ViseReferCode != 0)
                {
                    builder.Append("ViseReferCode=@ViseReferCode,");
                    this._DataProcess.ProcessParametersAdd("@ViseReferCode", SqlDbType.Int, 4, mObj.ViseReferCode);
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(" where ViseCode=@ViseCode");
                this._DataProcess.ProcessParametersAdd("@ViseCode", SqlDbType.Int, 4, mObj.ViseCode);
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

        public int Delete(LocaleViseModel mObj)
        {
            int num;
            try
            {
                num = this._Delete(mObj.ViseCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public LocaleViseModel GetModel(int Code)
        {
            LocaleViseModel model;
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

        private void Initialize(SqlDataReader reader, LocaleViseModel obj)
        {
            try
            {
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseCode")) != DBNull.Value)
                {
                    obj.ViseCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseId")) != DBNull.Value)
                {
                    obj.ViseId = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseId"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseName")) != DBNull.Value)
                {
                    obj.ViseName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseName"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseDate")) != DBNull.Value)
                {
                    obj.ViseDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseDate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseType")) != DBNull.Value)
                {
                    obj.ViseType = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseType"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "VisePerson")) != DBNull.Value)
                {
                    obj.VisePerson = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "VisePerson"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseSupplier")) != DBNull.Value)
                {
                    obj.ViseSupplier = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseSupplier"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseUnit")) != DBNull.Value)
                {
                    obj.ViseUnit = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseUnit"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseEndDate")) != DBNull.Value)
                {
                    obj.ViseEndDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseEndDate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseRemark")) != DBNull.Value)
                {
                    obj.ViseRemark = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseRemark"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseScrutiny")) != DBNull.Value)
                {
                    obj.ViseScrutiny = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseScrutiny"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseReason")) != DBNull.Value)
                {
                    obj.ViseReason = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseReason"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseStatus")) != DBNull.Value)
                {
                    obj.ViseStatus = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseStatus"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseBalanceStatus")) != DBNull.Value)
                {
                    obj.ViseBalanceStatus = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseBalanceStatus"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseProject")) != DBNull.Value)
                {
                    obj.ViseProject = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseProject"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseContractCode")) != DBNull.Value)
                {
                    obj.ViseContractCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseContractCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseReasonItem")) != DBNull.Value)
                {
                    obj.ViseReasonItem = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseReasonItem"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseComeToMoney")) != DBNull.Value)
                {
                    obj.ViseComeToMoney = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseComeToMoney"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseID2")) != DBNull.Value)
                {
                    obj.ViseID2 = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseID2"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseReferCode")) != DBNull.Value)
                {
                    obj.ViseReferCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ViseReferCode"));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int Insert(LocaleViseModel mObj)
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

        public List<LocaleViseModel> Select()
        {
            List<LocaleViseModel> list;
            try
            {
                LocaleViseQueryModel qmObj = new LocaleViseQueryModel();
                list = this._Select(qmObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<LocaleViseModel> Select(LocaleViseQueryModel qmObj)
        {
            List<LocaleViseModel> list;
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

        public int Update(LocaleViseModel mObj)
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

