namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OAPersonPolityDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OAPersonPolityDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OAPersonPolityDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OAPersonPolityModel _DataBind(int Code)
        {
            GK_OAPersonPolityModel model = new GK_OAPersonPolityModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonPolity ");
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
            builder.Append("delete from GK_OAPersonPolity ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OAPersonPolityModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPersonPolity(");
            builder.Append("PersonID,BeginDate,EndDate,Name,Duty,Certifier)");
            builder.Append(" values (");
            builder.Append("@PersonID,@BeginDate,@EndDate,@Name,@Duty,@Certifier) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@PersonID", SqlDbType.VarChar, 50, mObj.PersonID);
            this._DataProcess.ProcessParametersAdd("@BeginDate", SqlDbType.VarChar, 50, mObj.BeginDate);
            this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.VarChar, 50, mObj.EndDate);
            this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            this._DataProcess.ProcessParametersAdd("@Duty", SqlDbType.VarChar, 50, mObj.Duty);
            this._DataProcess.ProcessParametersAdd("@Certifier", SqlDbType.VarChar, 50, mObj.Certifier);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OAPersonPolityModel> _Select(GK_OAPersonPolityQueryModel qmObj)
        {
            List<GK_OAPersonPolityModel> list = new List<GK_OAPersonPolityModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPersonPolity ");
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
                            GK_OAPersonPolityModel model = new GK_OAPersonPolityModel();
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

        private int _Update(GK_OAPersonPolityModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPersonPolity set ");
            if (mObj.PersonID != null)
            {
                builder.Append("PersonID=@PersonID,");
                this._DataProcess.ProcessParametersAdd("@PersonID", SqlDbType.VarChar, 50, mObj.PersonID);
            }
            if (mObj.BeginDate != null)
            {
                builder.Append("BeginDate=@BeginDate,");
                this._DataProcess.ProcessParametersAdd("@BeginDate", SqlDbType.VarChar, 50, mObj.BeginDate);
            }
            if (mObj.EndDate != null)
            {
                builder.Append("EndDate=@EndDate,");
                this._DataProcess.ProcessParametersAdd("@EndDate", SqlDbType.VarChar, 50, mObj.EndDate);
            }
            if (mObj.Name != null)
            {
                builder.Append("Name=@Name,");
                this._DataProcess.ProcessParametersAdd("@Name", SqlDbType.VarChar, 50, mObj.Name);
            }
            if (mObj.Duty != null)
            {
                builder.Append("Duty=@Duty,");
                this._DataProcess.ProcessParametersAdd("@Duty", SqlDbType.VarChar, 50, mObj.Duty);
            }
            if (mObj.Certifier != null)
            {
                builder.Append("Certifier=@Certifier,");
                this._DataProcess.ProcessParametersAdd("@Certifier", SqlDbType.VarChar, 50, mObj.Certifier);
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

        public int Delete(GK_OAPersonPolityModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OAPersonPolityModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OAPersonPolityModel obj)
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
                obj.BeginDate = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.EndDate = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Name = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Duty = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Certifier = reader.GetString(6);
            }
        }

        public int Insert(GK_OAPersonPolityModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OAPersonPolityModel> Select()
        {
            GK_OAPersonPolityQueryModel qmObj = new GK_OAPersonPolityQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OAPersonPolityModel> Select(GK_OAPersonPolityQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OAPersonPolityModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

