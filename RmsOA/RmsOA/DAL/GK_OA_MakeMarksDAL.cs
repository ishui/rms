namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_MakeMarksDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_MakeMarksDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_MakeMarksDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_MakeMarksModel _DataBind(int Code)
        {
            GK_OA_MakeMarksModel model = new GK_OA_MakeMarksModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_MakeMarks ");
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
            builder.Append("delete from GK_OA_MakeMarks ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_MakeMarksModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_MakeMarks(");
            builder.Append("UnitCode,Title,MarkType,RegisterDate,RegisterPerson)");
            builder.Append(" values (");
            builder.Append("@UnitCode,@Title,@MarkType,@RegisterDate,@RegisterPerson) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@Title", SqlDbType.VarChar, 50, mObj.Title);
            this._DataProcess.ProcessParametersAdd("@MarkType", SqlDbType.VarChar, 50, mObj.MarkType);
            this._DataProcess.ProcessParametersAdd("@RegisterDate", SqlDbType.DateTime, 8, mObj.RegisterDate);
            this._DataProcess.ProcessParametersAdd("@RegisterPerson", SqlDbType.VarChar, 50, mObj.RegisterPerson);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_MakeMarksModel> _Select(GK_OA_MakeMarksQueryModel qmObj)
        {
            List<GK_OA_MakeMarksModel> list = new List<GK_OA_MakeMarksModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_MakeMarks ");
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
                            GK_OA_MakeMarksModel model = new GK_OA_MakeMarksModel();
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

        private int _Update(GK_OA_MakeMarksModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_MakeMarks set ");
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.Title != null)
            {
                builder.Append("Title=@Title,");
                this._DataProcess.ProcessParametersAdd("@Title", SqlDbType.VarChar, 50, mObj.Title);
            }
            if (mObj.MarkType != null)
            {
                builder.Append("MarkType=@MarkType,");
                this._DataProcess.ProcessParametersAdd("@MarkType", SqlDbType.VarChar, 50, mObj.MarkType);
            }
            if (mObj.RegisterDate != DateTime.MinValue)
            {
                builder.Append("RegisterDate=@RegisterDate,");
                this._DataProcess.ProcessParametersAdd("@RegisterDate", SqlDbType.DateTime, 8, mObj.RegisterDate);
            }
            if (mObj.RegisterPerson != null)
            {
                builder.Append("RegisterPerson=@RegisterPerson,");
                this._DataProcess.ProcessParametersAdd("@RegisterPerson", SqlDbType.VarChar, 50, mObj.RegisterPerson);
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

        public int Delete(GK_OA_MakeMarksModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_MakeMarksModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_MakeMarksModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Title = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.MarkType = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.RegisterDate = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.RegisterPerson = reader.GetString(5);
            }
        }

        public int Insert(GK_OA_MakeMarksModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_MakeMarksModel> Select()
        {
            GK_OA_MakeMarksQueryModel qmObj = new GK_OA_MakeMarksQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_MakeMarksModel> Select(GK_OA_MakeMarksQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_MakeMarksModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

