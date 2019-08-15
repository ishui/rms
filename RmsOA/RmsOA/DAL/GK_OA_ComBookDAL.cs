namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_ComBookDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_ComBookDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_ComBookDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_ComBookModel _DataBind(int Code)
        {
            GK_OA_ComBookModel model = new GK_OA_ComBookModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ComBook ");
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
            builder.Append("delete from GK_OA_ComBook ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_ComBookModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO GK_OA_ComBook (");
            builder2.Append("VALUES(");
            if (mObj.Company != null)
            {
                builder.Append("Company,");
                builder2.Append("@Company,");
                this._DataProcess.ProcessParametersAdd("@Company", SqlDbType.VarChar, 50, mObj.Company);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode,");
                builder2.Append("@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode,");
                builder2.Append("@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.Telephone != null)
            {
                builder.Append("Telephone,");
                builder2.Append("@Telephone,");
                this._DataProcess.ProcessParametersAdd("@Telephone", SqlDbType.VarChar, 50, mObj.Telephone);
            }
            if (mObj.HandleTelephone != null)
            {
                builder.Append("HandleTelephone,");
                builder2.Append("@HandleTelephone,");
                this._DataProcess.ProcessParametersAdd("@HandleTelephone", SqlDbType.VarChar, 50, mObj.HandleTelephone);
            }
            if (mObj.MSN != null)
            {
                builder.Append("MSN,");
                builder2.Append("@MSN,");
                this._DataProcess.ProcessParametersAdd("@MSN", SqlDbType.VarChar, 50, mObj.MSN);
            }
            if (mObj.QQ != null)
            {
                builder.Append("QQ,");
                builder2.Append("@QQ,");
                this._DataProcess.ProcessParametersAdd("@QQ", SqlDbType.VarChar, 50, mObj.QQ);
            }
            if (mObj.Email != null)
            {
                builder.Append("Email,");
                builder2.Append("@Email,");
                this._DataProcess.ProcessParametersAdd("@Email", SqlDbType.VarChar, 50, mObj.Email);
            }
            if (mObj.UrgePhone != null)
            {
                builder.Append("UrgePhone,");
                builder2.Append("@UrgePhone,");
                this._DataProcess.ProcessParametersAdd("@UrgePhone", SqlDbType.VarChar, 100, mObj.UrgePhone);
            }
            if (mObj.PrepField1 != null)
            {
                builder.Append("PrepField1,");
                builder2.Append("@PrepField1,");
                this._DataProcess.ProcessParametersAdd("@PrepField1", SqlDbType.VarChar, 200, mObj.PrepField1);
            }
            if (mObj.PrepField2 != null)
            {
                builder.Append("PrepField2,");
                builder2.Append("@PrepField2,");
                this._DataProcess.ProcessParametersAdd("@PrepField2", SqlDbType.VarChar, 200, mObj.PrepField2);
            }
            if (mObj.PrepField3 != null)
            {
                builder.Append("PrepField3,");
                builder2.Append("@PrepField3,");
                this._DataProcess.ProcessParametersAdd("@PrepField3", SqlDbType.VarChar, 200, mObj.PrepField3);
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

        private List<GK_OA_ComBookModel> _Select(GK_OA_ComBookQueryModel qmObj)
        {
            List<GK_OA_ComBookModel> list = new List<GK_OA_ComBookModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_ComBook ");
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
                            GK_OA_ComBookModel model = new GK_OA_ComBookModel();
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

        private int _Update(GK_OA_ComBookModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_ComBook set ");
            if (mObj.Company != null)
            {
                builder.Append("Company=@Company,");
                this._DataProcess.ProcessParametersAdd("@Company", SqlDbType.VarChar, 50, mObj.Company);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.UserCode != null)
            {
                builder.Append("UserCode=@UserCode,");
                this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            }
            if (mObj.Telephone != null)
            {
                builder.Append("Telephone=@Telephone,");
                this._DataProcess.ProcessParametersAdd("@Telephone", SqlDbType.VarChar, 50, mObj.Telephone);
            }
            if (mObj.HandleTelephone != null)
            {
                builder.Append("HandleTelephone=@HandleTelephone,");
                this._DataProcess.ProcessParametersAdd("@HandleTelephone", SqlDbType.VarChar, 50, mObj.HandleTelephone);
            }
            if (mObj.MSN != null)
            {
                builder.Append("MSN=@MSN,");
                this._DataProcess.ProcessParametersAdd("@MSN", SqlDbType.VarChar, 50, mObj.MSN);
            }
            if (mObj.QQ != null)
            {
                builder.Append("QQ=@QQ,");
                this._DataProcess.ProcessParametersAdd("@QQ", SqlDbType.VarChar, 50, mObj.QQ);
            }
            if (mObj.Email != null)
            {
                builder.Append("Email=@Email,");
                this._DataProcess.ProcessParametersAdd("@Email", SqlDbType.VarChar, 50, mObj.Email);
            }
            if (mObj.UrgePhone != null)
            {
                builder.Append("UrgePhone=@UrgePhone,");
                this._DataProcess.ProcessParametersAdd("@UrgePhone", SqlDbType.VarChar, 100, mObj.UrgePhone);
            }
            if (mObj.PrepField1 != null)
            {
                builder.Append("PrepField1=@PrepField1,");
                this._DataProcess.ProcessParametersAdd("@PrepField1", SqlDbType.VarChar, 200, mObj.PrepField1);
            }
            if (mObj.PrepField2 != null)
            {
                builder.Append("PrepField2=@PrepField2,");
                this._DataProcess.ProcessParametersAdd("@PrepField2", SqlDbType.VarChar, 200, mObj.PrepField2);
            }
            if (mObj.PrepField3 != null)
            {
                builder.Append("PrepField3=@PrepField3,");
                this._DataProcess.ProcessParametersAdd("@PrepField3", SqlDbType.VarChar, 200, mObj.PrepField3);
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

        public int Delete(GK_OA_ComBookModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_ComBookModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_ComBookModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.Company = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.UserCode = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Telephone = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.HandleTelephone = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.MSN = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.QQ = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Email = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.UrgePhone = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.PrepField1 = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.PrepField2 = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.PrepField3 = reader.GetString(12);
            }
        }

        public int Insert(GK_OA_ComBookModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_ComBookModel> Select()
        {
            GK_OA_ComBookQueryModel qmObj = new GK_OA_ComBookQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_ComBookModel> Select(GK_OA_ComBookQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_ComBookModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

