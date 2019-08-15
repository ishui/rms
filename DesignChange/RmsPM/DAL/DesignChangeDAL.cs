namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class DesignChangeDAL
    {
        private SqlDataProcess _DataProcess;

        public DesignChangeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public DesignChangeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private DesignChangeModel _DataBind(int Code)
        {
            DesignChangeModel model = new DesignChangeModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DesignChange ");
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
            builder.Append("delete from DesignChange ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(DesignChangeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DesignChange(");
            builder.Append("SolutionName,ProjectName,Number,Person,Unit,Supplier,Date,Type,Remark,Reason,Designer,RelationNumber,TerminatingPerson,TerminatingDate,Specialty,ChangeMoney,TotalMoney,State,Flag,Contract,ChangeType)");
            builder.Append(" values (");
            builder.Append("@SolutionName,@ProjectName,@Number,@Person,@Unit,@Supplier,@Date,@Type,@Remark,@Reason,@Designer,@RelationNumber,@TerminatingPerson,@TerminatingDate,@Specialty,@ChangeMoney,@TotalMoney,@State,@Flag,@Contract,@ChangeType) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SolutionName", SqlDbType.VarChar, 50, mObj.SolutionName);
            this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 50, mObj.ProjectName);
            this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            this._DataProcess.ProcessParametersAdd("@Person", SqlDbType.VarChar, 50, mObj.Person);
            this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            this._DataProcess.ProcessParametersAdd("@Supplier", SqlDbType.VarChar, 50, mObj.Supplier);
            this._DataProcess.ProcessParametersAdd("@Date", SqlDbType.DateTime, 8, mObj.Date);
            this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 0x7d0, mObj.Reason);
            this._DataProcess.ProcessParametersAdd("@Designer", SqlDbType.VarChar, 50, mObj.Designer);
            this._DataProcess.ProcessParametersAdd("@RelationNumber", SqlDbType.VarChar, 50, mObj.RelationNumber);
            this._DataProcess.ProcessParametersAdd("@TerminatingPerson", SqlDbType.VarChar, 50, mObj.TerminatingPerson);
            this._DataProcess.ProcessParametersAdd("@TerminatingDate", SqlDbType.DateTime, 8, mObj.TerminatingDate);
            this._DataProcess.ProcessParametersAdd("@Specialty", SqlDbType.VarChar, 50, mObj.Specialty);
            this._DataProcess.ProcessParametersAdd("@ChangeMoney", SqlDbType.VarChar, 0x7d0, mObj.ChangeMoney);
            this._DataProcess.ProcessParametersAdd("@TotalMoney", SqlDbType.Decimal, 9, mObj.TotalMoney);
            this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            this._DataProcess.ProcessParametersAdd("@Flag", SqlDbType.VarChar, 50, mObj.Flag);
            this._DataProcess.ProcessParametersAdd("@Contract", SqlDbType.VarChar, 50, mObj.Contract);
            this._DataProcess.ProcessParametersAdd("@ChangeType", SqlDbType.VarChar, 50, mObj.ChangeType);
            this._DataProcess.ProcessParametersAdd("@ReferCode", SqlDbType.Int, 4, mObj.ReferCode);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<DesignChangeModel> _Select(DesignChangeQueryModel qmObj)
        {
            List<DesignChangeModel> list = new List<DesignChangeModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from DesignChange ");
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
                            DesignChangeModel model = new DesignChangeModel();
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

        private int _Update(DesignChangeModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DesignChange set ");
            if (mObj.SolutionName != null)
            {
                builder.Append("SolutionName=@SolutionName,");
                this._DataProcess.ProcessParametersAdd("@SolutionName", SqlDbType.VarChar, 50, mObj.SolutionName);
            }
            if (mObj.ProjectName != null)
            {
                builder.Append("ProjectName=@ProjectName,");
                this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 50, mObj.ProjectName);
            }
            if (mObj.Number != null)
            {
                builder.Append("Number=@Number,");
                this._DataProcess.ProcessParametersAdd("@Number", SqlDbType.VarChar, 50, mObj.Number);
            }
            if (mObj.Person != null)
            {
                builder.Append("Person=@Person,");
                this._DataProcess.ProcessParametersAdd("@Person", SqlDbType.VarChar, 50, mObj.Person);
            }
            if (mObj.Unit != null)
            {
                builder.Append("Unit=@Unit,");
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            }
            if (mObj.Supplier != null)
            {
                builder.Append("Supplier=@Supplier,");
                this._DataProcess.ProcessParametersAdd("@Supplier", SqlDbType.VarChar, 50, mObj.Supplier);
            }
            if (mObj.Date != DateTime.MinValue)
            {
                builder.Append("Date=@Date,");
                this._DataProcess.ProcessParametersAdd("@Date", SqlDbType.DateTime, 8, mObj.Date);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            }
            if (mObj.Reason != null)
            {
                builder.Append("Reason=@Reason,");
                this._DataProcess.ProcessParametersAdd("@Reason", SqlDbType.VarChar, 0x7d0, mObj.Reason);
            }
            if (mObj.Designer != null)
            {
                builder.Append("Designer=@Designer,");
                this._DataProcess.ProcessParametersAdd("@Designer", SqlDbType.VarChar, 50, mObj.Designer);
            }
            if (mObj.RelationNumber != null)
            {
                builder.Append("RelationNumber=@RelationNumber,");
                this._DataProcess.ProcessParametersAdd("@RelationNumber", SqlDbType.VarChar, 50, mObj.RelationNumber);
            }
            if (mObj.TerminatingPerson != null)
            {
                builder.Append("TerminatingPerson=@TerminatingPerson,");
                this._DataProcess.ProcessParametersAdd("@TerminatingPerson", SqlDbType.VarChar, 50, mObj.TerminatingPerson);
            }
            if (mObj.TerminatingDate != DateTime.MinValue)
            {
                builder.Append("TerminatingDate=@TerminatingDate,");
                this._DataProcess.ProcessParametersAdd("@TerminatingDate", SqlDbType.DateTime, 8, mObj.TerminatingDate);
            }
            if (mObj.Specialty != null)
            {
                builder.Append("Specialty=@Specialty,");
                this._DataProcess.ProcessParametersAdd("@Specialty", SqlDbType.VarChar, 50, mObj.Specialty);
            }
            if (mObj.ChangeMoney != null)
            {
                builder.Append("ChangeMoney=@ChangeMoney,");
                this._DataProcess.ProcessParametersAdd("@ChangeMoney", SqlDbType.VarChar, 0x7d0, mObj.ChangeMoney);
            }
            if (mObj.TotalMoney != 0M)
            {
                builder.Append("TotalMoney=@TotalMoney,");
                this._DataProcess.ProcessParametersAdd("@TotalMoney", SqlDbType.Decimal, 9, mObj.TotalMoney);
            }
            if (mObj.State != null)
            {
                builder.Append("State=@State,");
                this._DataProcess.ProcessParametersAdd("@State", SqlDbType.VarChar, 50, mObj.State);
            }
            if (mObj.Flag != null)
            {
                builder.Append("Flag=@Flag,");
                this._DataProcess.ProcessParametersAdd("@Flag", SqlDbType.VarChar, 50, mObj.Flag);
            }
            if (mObj.Contract != null)
            {
                builder.Append("Contract=@Contract,");
                this._DataProcess.ProcessParametersAdd("@Contract", SqlDbType.VarChar, 50, mObj.Contract);
            }
            if (mObj.ChangeType != null)
            {
                builder.Append("ChangeType=@ChangeType,");
                this._DataProcess.ProcessParametersAdd("@ChangeType", SqlDbType.VarChar, 50, mObj.ChangeType);
            }
            if (mObj.ChangeType != null)
            {
                builder.Append("ReferCode=@ReferCode,");
                this._DataProcess.ProcessParametersAdd("@ReferCode", SqlDbType.Int, 4, mObj.ReferCode);
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

        public int Delete(DesignChangeModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public DesignChangeModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, DesignChangeModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.SolutionName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.ProjectName = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Number = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Person = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Unit = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.Supplier = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Date = reader.GetDateTime(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Type = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Remark = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Reason = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Designer = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.RelationNumber = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.TerminatingPerson = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.TerminatingDate = reader.GetDateTime(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.Specialty = reader.GetString(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.ChangeMoney = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.TotalMoney = reader.GetDecimal(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.State = reader.GetString(0x12);
            }
            if (reader.GetValue(0x13) != DBNull.Value)
            {
                obj.Flag = reader.GetString(0x13);
            }
            if (reader.GetValue(20) != DBNull.Value)
            {
                obj.Contract = reader.GetString(20);
            }
            if (reader.GetValue(0x15) != DBNull.Value)
            {
                obj.ChangeType = reader.GetString(0x15);
            }
            if (reader.GetValue(0x16) != DBNull.Value)
            {
                obj.ReferCode = reader.GetInt32(0x16);
            }
        }

        public int Insert(DesignChangeModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<DesignChangeModel> Select()
        {
            DesignChangeQueryModel qmObj = new DesignChangeQueryModel();
            return this._Select(qmObj);
        }

        public List<DesignChangeModel> Select(DesignChangeQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(DesignChangeModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

