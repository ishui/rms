﻿namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_CapitalAssertAcountDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_CapitalAssertAcountDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_CapitalAssertAcountDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_CapitalAssertAcountModel _DataBind(int Code)
        {
            GK_OA_CapitalAssertAcountModel model = new GK_OA_CapitalAssertAcountModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_CapitalAssertAcount ");
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
            builder.Append("delete from GK_OA_CapitalAssertAcount ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_CapitalAssertAcountModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO GK_OA_CapitalAssertAcount (");
            builder2.Append("VALUES(");
            if (mObj.Name != null)
            {
                builder.Append("Name,");
                builder2.Append("@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 100, mObj.Name);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type,");
                builder2.Append("@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number,");
                builder2.Append("@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 100, mObj.Number);
            }
            if (mObj.Buydate != DateTime.MinValue)
            {
                builder.Append("Buydate,");
                builder2.Append("@Buydate,");
                this._DataProcess.ProcessParametersAdd("@Buydate", SqlDbType.DateTime, 8, mObj.Buydate);
            }
            if (mObj.BuyCount != 0)
            {
                builder.Append("BuyCount,");
                builder2.Append("@BuyCount,");
                this._DataProcess.ProcessParametersAdd("@BuyCount", SqlDbType.Int, 4, mObj.BuyCount);
            }
            if (mObj.Price != 0M)
            {
                builder.Append("Price,");
                builder2.Append("@Price,");
                this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            }
            if (mObj.Unit != null)
            {
                builder.Append("Unit,");
                builder2.Append("@Unit,");
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 10, mObj.Unit);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept,");
                builder2.Append("@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            }
            if (mObj.TransferRecord != null)
            {
                builder.Append("TransferRecord,");
                builder2.Append("@TransferRecord,");
                this._DataProcess.ProcessParametersAdd("@TransferRecord", SqlDbType.VarChar, 0x7d0, mObj.TransferRecord);
            }
            if (mObj.UserName != null)
            {
                builder.Append("UserName,");
                builder2.Append("@UserName,");
                this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            }
            if (mObj.Place != null)
            {
                builder.Append("Place,");
                builder2.Append("@Place,");
                this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 100, mObj.Place);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark,");
                builder2.Append("@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO,");
                builder2.Append("@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 100, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule,");
                builder2.Append("@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 100, mObj.SNRule);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            builder.Append(") ");
            builder2.Append(") ");
            builder.Append(builder2.ToString());
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_CapitalAssertAcountModel> _Select(GK_OA_CapitalAssertAcountQueryModel qmObj)
        {
            List<GK_OA_CapitalAssertAcountModel> list = new List<GK_OA_CapitalAssertAcountModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_CapitalAssertAcount ");
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
                            GK_OA_CapitalAssertAcountModel model = new GK_OA_CapitalAssertAcountModel();
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

        private int _Update(GK_OA_CapitalAssertAcountModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_CapitalAssertAcount set ");
            if (mObj.Name != null)
            {
                builder.Append("Name=@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 100, mObj.Name);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number=@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 100, mObj.Number);
            }
            if (mObj.Buydate != DateTime.MinValue)
            {
                builder.Append("Buydate=@Buydate,");
                this._DataProcess.ProcessParametersAdd("@Buydate", SqlDbType.DateTime, 8, mObj.Buydate);
            }
            if (mObj.BuyCount != 0)
            {
                builder.Append("BuyCount=@BuyCount,");
                this._DataProcess.ProcessParametersAdd("@BuyCount", SqlDbType.Int, 4, mObj.BuyCount);
            }
            if (mObj.Price != 0M)
            {
                builder.Append("Price=@Price,");
                this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            }
            if (mObj.Unit != null)
            {
                builder.Append("Unit=@Unit,");
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 10, mObj.Unit);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept=@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 50, mObj.Dept);
            }
            if (mObj.TransferRecord != null)
            {
                builder.Append("TransferRecord=@TransferRecord,");
                this._DataProcess.ProcessParametersAdd("@TransferRecord", SqlDbType.VarChar, 0x7d0, mObj.TransferRecord);
            }
            if (mObj.UserName != null)
            {
                builder.Append("UserName=@UserName,");
                this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            }
            if (mObj.Place != null)
            {
                builder.Append("Place=@Place,");
                this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 100, mObj.Place);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
            }
            if (mObj.QualityNO != null)
            {
                builder.Append("QualityNO=@QualityNO,");
                this._DataProcess.ProcessParametersAdd("@QualityNO", SqlDbType.VarChar, 100, mObj.QualityNO);
            }
            if (mObj.SNRule != null)
            {
                builder.Append("SNRule=@SNRule,");
                this._DataProcess.ProcessParametersAdd("@SNRule", SqlDbType.VarChar, 100, mObj.SNRule);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
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

        public int Delete(GK_OA_CapitalAssertAcountModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_CapitalAssertAcountModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_CapitalAssertAcountModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Name = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Type = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Number = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Buydate = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.BuyCount = reader.GetInt32(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Price = reader.GetDecimal(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Unit = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Dept = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.TransferRecord = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.UserName = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Place = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Remark = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.QualityNO = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.SNRule = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.Status = reader.GetString(15);
            }
        }

        public int Insert(GK_OA_CapitalAssertAcountModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_CapitalAssertAcountModel> Select()
        {
            GK_OA_CapitalAssertAcountQueryModel qmObj = new GK_OA_CapitalAssertAcountQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_CapitalAssertAcountModel> Select(GK_OA_CapitalAssertAcountQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_CapitalAssertAcountModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

