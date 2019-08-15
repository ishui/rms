namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TC_OA_LingYongMenonyDAL
    {
        private SqlDataProcess _DataProcess;

        public TC_OA_LingYongMenonyDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public TC_OA_LingYongMenonyDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private TC_OA_LingYongMenonyModel _DataBind(int Code)
        {
            TC_OA_LingYongMenonyModel model = new TC_OA_LingYongMenonyModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_LingYongMenony ");
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
            builder.Append("delete from TC_OA_LingYongMenony ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(TC_OA_LingYongMenonyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TC_OA_LingYongMenony(");
            builder.Append("UserCode,UnitCode,ApplayDate,UseWay,Qian,Bai,Shi,Yuan,Jiao,Fen,Customer,QianShouDate,Auditing)");
            builder.Append(" values (");
            builder.Append("@UserCode,@UnitCode,@ApplayDate,@UseWay,@Qian,@Bai,@Shi,@Yuan,@Jiao,@Fen,@Customer,@QianShouDate,@Auditing) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UserCode", SqlDbType.VarChar, 50, mObj.UserCode);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@ApplayDate", SqlDbType.DateTime, 8, mObj.ApplayDate);
            this._DataProcess.ProcessParametersAdd("@UseWay", SqlDbType.VarChar, 500, mObj.UseWay);
            this._DataProcess.ProcessParametersAdd("@Qian", SqlDbType.VarChar, 50, mObj.Qian);
            this._DataProcess.ProcessParametersAdd("@Bai", SqlDbType.VarChar, 50, mObj.Bai);
            this._DataProcess.ProcessParametersAdd("@Shi", SqlDbType.VarChar, 50, mObj.Shi);
            this._DataProcess.ProcessParametersAdd("@Yuan", SqlDbType.VarChar, 50, mObj.Yuan);
            this._DataProcess.ProcessParametersAdd("@Jiao", SqlDbType.VarChar, 50, mObj.Jiao);
            this._DataProcess.ProcessParametersAdd("@Fen", SqlDbType.VarChar, 50, mObj.Fen);
            this._DataProcess.ProcessParametersAdd("@Customer", SqlDbType.VarChar, 50, mObj.Customer);
            this._DataProcess.ProcessParametersAdd("@QianShouDate", SqlDbType.DateTime, 8, mObj.QianShouDate);
            this._DataProcess.ProcessParametersAdd("@Auditing", SqlDbType.Int, 4, mObj.Auditing);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<TC_OA_LingYongMenonyModel> _Select(TC_OA_LingYongMenonyQueryModel qmObj)
        {
            List<TC_OA_LingYongMenonyModel> list = new List<TC_OA_LingYongMenonyModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from TC_OA_LingYongMenony ");
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
                            TC_OA_LingYongMenonyModel model = new TC_OA_LingYongMenonyModel();
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

        private int _Update(TC_OA_LingYongMenonyModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TC_OA_LingYongMenony set ");
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
            if (mObj.UseWay != null)
            {
                builder.Append("UseWay=@UseWay,");
                this._DataProcess.ProcessParametersAdd("@UseWay", SqlDbType.VarChar, 500, mObj.UseWay);
            }
            if (mObj.Qian != null)
            {
                builder.Append("Qian=@Qian,");
                this._DataProcess.ProcessParametersAdd("@Qian", SqlDbType.VarChar, 50, mObj.Qian);
            }
            if (mObj.Bai != null)
            {
                builder.Append("Bai=@Bai,");
                this._DataProcess.ProcessParametersAdd("@Bai", SqlDbType.VarChar, 50, mObj.Bai);
            }
            if (mObj.Shi != null)
            {
                builder.Append("Shi=@Shi,");
                this._DataProcess.ProcessParametersAdd("@Shi", SqlDbType.VarChar, 50, mObj.Shi);
            }
            if (mObj.Yuan != null)
            {
                builder.Append("Yuan=@Yuan,");
                this._DataProcess.ProcessParametersAdd("@Yuan", SqlDbType.VarChar, 50, mObj.Yuan);
            }
            if (mObj.Jiao != null)
            {
                builder.Append("Jiao=@Jiao,");
                this._DataProcess.ProcessParametersAdd("@Jiao", SqlDbType.VarChar, 50, mObj.Jiao);
            }
            if (mObj.Fen != null)
            {
                builder.Append("Fen=@Fen,");
                this._DataProcess.ProcessParametersAdd("@Fen", SqlDbType.VarChar, 50, mObj.Fen);
            }
            if (mObj.Customer != null)
            {
                builder.Append("Customer=@Customer,");
                this._DataProcess.ProcessParametersAdd("@Customer", SqlDbType.VarChar, 50, mObj.Customer);
            }
            if (mObj.QianShouDate != DateTime.MinValue)
            {
                builder.Append("QianShouDate=@QianShouDate,");
                this._DataProcess.ProcessParametersAdd("@QianShouDate", SqlDbType.DateTime, 8, mObj.QianShouDate);
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

        public int Delete(TC_OA_LingYongMenonyModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public TC_OA_LingYongMenonyModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, TC_OA_LingYongMenonyModel obj)
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
                obj.UseWay = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Qian = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Bai = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Shi = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Yuan = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Jiao = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Fen = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Customer = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.QianShouDate = reader.GetDateTime(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.Auditing = reader.GetInt32(13);
            }
        }

        public int Insert(TC_OA_LingYongMenonyModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<TC_OA_LingYongMenonyModel> Select()
        {
            TC_OA_LingYongMenonyQueryModel qmObj = new TC_OA_LingYongMenonyQueryModel();
            return this._Select(qmObj);
        }

        public List<TC_OA_LingYongMenonyModel> Select(TC_OA_LingYongMenonyQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(TC_OA_LingYongMenonyModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

