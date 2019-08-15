namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class DeskTopTypeDAL
    {
        private SqlDataProcess _DataProcess;

        public DeskTopTypeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public DeskTopTypeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private DeskTopTypeModel _DataBind(int Code)
        {
            DeskTopTypeModel model = new DeskTopTypeModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DeskTopType ");
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
            builder.Append("delete from DeskTopType ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(DeskTopTypeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO DeskTopType (");
            builder2.Append("VALUES(");
            if (mObj.ControldID != 0)
            {
                builder.Append("ControldID,");
                builder2.Append("@ControldID,");
                this._DataProcess.ProcessParametersAdd("@ControldID", SqlDbType.Int, 4, mObj.ControldID);
            }
            if (mObj.DeskType != null)
            {
                builder.Append("DeskType,");
                builder2.Append("@DeskType,");
                this._DataProcess.ProcessParametersAdd("@DeskType", SqlDbType.VarChar, 50, mObj.DeskType);
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

        private List<DeskTopTypeModel> _Select(DeskTopTypeQueryModel qmObj)
        {
            List<DeskTopTypeModel> list = new List<DeskTopTypeModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DeskTopType ");
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
                            DeskTopTypeModel model = new DeskTopTypeModel();
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

        private int _Update(DeskTopTypeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DeskTopType set ");
            if (mObj.ControldID != 0)
            {
                builder.Append("ControldID=@ControldID,");
                this._DataProcess.ProcessParametersAdd("@ControldID", SqlDbType.Int, 4, mObj.ControldID);
            }
            if (mObj.DeskType != null)
            {
                builder.Append("DeskType=@DeskType,");
                this._DataProcess.ProcessParametersAdd("@DeskType", SqlDbType.VarChar, 50, mObj.DeskType);
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

        public int Delete(DeskTopTypeModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public DeskTopTypeModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, DeskTopTypeModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.ControldID = reader.GetInt32(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.DeskType = reader.GetString(2);
            }
        }

        public int Insert(DeskTopTypeModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<DeskTopTypeModel> Select()
        {
            DeskTopTypeQueryModel qmObj = new DeskTopTypeQueryModel();
            return this._Select(qmObj);
        }

        public List<DeskTopTypeModel> Select(DeskTopTypeQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(DeskTopTypeModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

