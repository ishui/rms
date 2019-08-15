namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class ProjectCostDAL
    {
        private SqlDataProcess _DataProcess;

        public ProjectCostDAL(SqlConnection Connection)
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

        public ProjectCostDAL(SqlTransaction Transaction)
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

        private ProjectCostModel _DataBind(int Code)
        {
            Exception exception;
            ProjectCostModel model2;
            try
            {
                ProjectCostModel model = new ProjectCostModel();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from ProjectCost");
                builder.Append(" where ProjectCostCode=@ProjectCostCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@ProjectCostCode", SqlDbType.Int, 4, Code);
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
                builder.Append("delete from ProjectCost ");
                builder.Append(" where ProjectCostCode=@ProjectCostCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@ProjectCostCode", SqlDbType.Int, 4, Code);
                num = this._DataProcess.RunSql();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        private int _Insert(ProjectCostModel mObj)
        {
            int projectCostCode;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into ProjectCost(");
                builder.Append("ProjectCostCode,ProjectName,GroupCode,Area,Price,Money,Qty,Unit,InputPerson,InputDate,Remark)");
                builder.Append(" values (");
                builder.Append("@ProjectCostCode,@ProjectName,@GroupCode,@Area,@Price,@Money,@Qty,@Unit,@InputPerson,@InputDate,@Remark) ");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@ProjectCostCode", SqlDbType.Int, 4, mObj.ProjectCostCode);
                this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 100, mObj.ProjectName);
                this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.VarChar, 50, mObj.GroupCode);
                this._DataProcess.ProcessParametersAdd("@Area", SqlDbType.Decimal, 9, mObj.Area);
                this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
                this._DataProcess.ProcessParametersAdd("@Money", SqlDbType.Decimal, 9, mObj.Money);
                this._DataProcess.ProcessParametersAdd("@Qty", SqlDbType.Decimal, 9, mObj.Qty);
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
                this._DataProcess.ProcessParametersAdd("@InputPerson", SqlDbType.VarChar, 50, mObj.InputPerson);
                this._DataProcess.ProcessParametersAdd("@InputDate", SqlDbType.DateTime, 8, mObj.InputDate);
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
                this._DataProcess.RunSql();
                projectCostCode = mObj.ProjectCostCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return projectCostCode;
        }

        private List<ProjectCostModel> _Select(ProjectCostQueryModel qmObj)
        {
            Exception exception;
            List<ProjectCostModel> list2;
            try
            {
                List<ProjectCostModel> list = new List<ProjectCostModel>();
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from ProjectCost ");
                builder.Append(qmObj.QueryConditionStr);
                if ((qmObj.SortColumns == null) || (qmObj.SortColumns.Length == 0))
                {
                    builder.Append(" ORDER BY ProjectCostCode desc");
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
                                ProjectCostModel model = new ProjectCostModel();
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

        private int _Update(ProjectCostModel mObj)
        {
            int num;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update ProjectCost set ");
                if (mObj.ProjectName != null)
                {
                    builder.Append("ProjectName=@ProjectName,");
                    this._DataProcess.ProcessParametersAdd("@ProjectName", SqlDbType.VarChar, 100, mObj.ProjectName);
                }
                if (mObj.GroupCode != null)
                {
                    builder.Append("GroupCode=@GroupCode,");
                    this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.VarChar, 50, mObj.GroupCode);
                }
                if (mObj.Area != 0M)
                {
                    builder.Append("Area=@Area,");
                    this._DataProcess.ProcessParametersAdd("@Area", SqlDbType.Decimal, 9, mObj.Area);
                }
                if (mObj.Price != 0M)
                {
                    builder.Append("Price=@Price,");
                    this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
                }
                if (mObj.Money != 0M)
                {
                    builder.Append("Money=@Money,");
                    this._DataProcess.ProcessParametersAdd("@Money", SqlDbType.Decimal, 9, mObj.Money);
                }
                if (mObj.Qty != 0M)
                {
                    builder.Append("Qty=@Qty,");
                    this._DataProcess.ProcessParametersAdd("@Qty", SqlDbType.Decimal, 9, mObj.Qty);
                }
                if (mObj.Unit != null)
                {
                    builder.Append("Unit=@Unit,");
                    this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
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
                if (mObj.Remark != null)
                {
                    builder.Append("Remark=@Remark,");
                    this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(" where ProjectCostCode=@ProjectCostCode");
                this._DataProcess.ProcessParametersAdd("@ProjectCostCode", SqlDbType.Int, 4, mObj.ProjectCostCode);
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

        public int Delete(ProjectCostModel mObj)
        {
            int num;
            try
            {
                num = this._Delete(mObj.ProjectCostCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public ProjectCostModel GetModel(int Code)
        {
            ProjectCostModel model;
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

        private void Initialize(SqlDataReader reader, ProjectCostModel obj)
        {
            try
            {
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCostCode")) != DBNull.Value)
                {
                    obj.ProjectCostCode = reader.GetInt32(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectCostCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectName")) != DBNull.Value)
                {
                    obj.ProjectName = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "ProjectName"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode")) != DBNull.Value)
                {
                    obj.GroupCode = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "GroupCode"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Area")) != DBNull.Value)
                {
                    obj.Area = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Area"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Price")) != DBNull.Value)
                {
                    obj.Price = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Price"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Money")) != DBNull.Value)
                {
                    obj.Money = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Money"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Qty")) != DBNull.Value)
                {
                    obj.Qty = reader.GetDecimal(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Qty"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Unit")) != DBNull.Value)
                {
                    obj.Unit = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Unit"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputPerson")) != DBNull.Value)
                {
                    obj.InputPerson = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputPerson"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputDate")) != DBNull.Value)
                {
                    obj.InputDate = reader.GetDateTime(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "InputDate"));
                }
                if (reader.GetValue(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Remark")) != DBNull.Value)
                {
                    obj.Remark = reader.GetString(SqlDataProcess.GetSqlDataReaderFieldIndex(reader, "Remark"));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int Insert(ProjectCostModel mObj)
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

        public List<ProjectCostModel> Select()
        {
            List<ProjectCostModel> list;
            try
            {
                ProjectCostQueryModel qmObj = new ProjectCostQueryModel();
                list = this._Select(qmObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public List<ProjectCostModel> Select(ProjectCostQueryModel qmObj)
        {
            List<ProjectCostModel> list;
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

        public int Update(ProjectCostModel mObj)
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

