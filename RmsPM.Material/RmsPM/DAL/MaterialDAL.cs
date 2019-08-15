namespace RmsPM.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using TiannuoPM.MODEL;

    public class MaterialDAL
    {
        private SqlDataProcess _DataProcess;

        public MaterialDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public MaterialDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private MaterialModel _DataBind(object objCode)
        {
            MaterialModel model = new MaterialModel();
            if ((objCode != null) || (objCode.ToString() != ""))
            {
                int num = 0;
                try
                {
                    num = int.Parse(objCode.ToString());
                }
                catch
                {
                }
                StringBuilder builder = new StringBuilder();
                builder.Append("select * from Material ");
                builder.Append(" where MaterialCode=@MaterialCode");
                this._DataProcess.CommandText = builder.ToString();
                this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, num);
                SqlDataReader sqlDataReader = null;
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
                finally
                {
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }
                }
            }
            return model;
        }

        private int _Delete(int Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Material ");
            builder.Append(" where MaterialCode=@MaterialCode");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(MaterialModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Material(");
            builder.Append("MaterialCode, MaterialName,GroupCode,Spec,Unit,StandardPrice,InputPerson,InputDate,Remark)");
            builder.Append(" values (");
            builder.Append("@MaterialCode, @MaterialName,@GroupCode,@Spec,@Unit,@StandardPrice,@InputPerson,@InputDate,@Remark) ");
            builder.Append("SELECT @MaterialCode = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, mObj.MaterialCode);
            this._DataProcess.ProcessParametersAdd("@MaterialName", SqlDbType.VarChar, 100, mObj.MaterialName);
            this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.VarChar, 50, mObj.GroupCode);
            this._DataProcess.ProcessParametersAdd("@Spec", SqlDbType.VarChar, 200, mObj.Spec);
            this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            this._DataProcess.ProcessParametersAdd("@StandardPrice", SqlDbType.Decimal, 9, mObj.StandardPrice);
            this._DataProcess.ProcessParametersAdd("@InputPerson", SqlDbType.VarChar, 50, mObj.InputPerson);
            this._DataProcess.ProcessParametersAdd("@InputDate", SqlDbType.DateTime, 8, mObj.InputDate);
            this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 800, mObj.Remark);
            this._DataProcess.RunSql();
            return mObj.MaterialCode;
        }

        private List<MaterialModel> _Select(MaterialQueryModel qmObj)
        {
            List<MaterialModel> list = new List<MaterialModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Material ");
            builder.Append(qmObj.QueryConditionStr);
            if ((qmObj.SortColumns == null) || (qmObj.SortColumns.Length == 0))
            {
                builder.Append(" ORDER BY MaterialCode desc");
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
                        MaterialModel model = new MaterialModel();
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
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return list;
        }

        private int _Update(MaterialModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Material set ");
            if (mObj.MaterialName != null)
            {
                builder.Append("MaterialName=@MaterialName,");
                this._DataProcess.ProcessParametersAdd("@MaterialName", SqlDbType.VarChar, 100, mObj.MaterialName);
            }
            if (mObj.GroupCode != null)
            {
                builder.Append("GroupCode=@GroupCode,");
                this._DataProcess.ProcessParametersAdd("@GroupCode", SqlDbType.VarChar, 50, mObj.GroupCode);
            }
            if (mObj.Spec != null)
            {
                builder.Append("Spec=@Spec,");
                this._DataProcess.ProcessParametersAdd("@Spec", SqlDbType.VarChar, 200, mObj.Spec);
            }
            if (mObj.Unit != null)
            {
                builder.Append("Unit=@Unit,");
                this._DataProcess.ProcessParametersAdd("@Unit", SqlDbType.VarChar, 50, mObj.Unit);
            }
            if (mObj.StandardPrice != -79228162514264337593543950335M)
            {
                builder.Append("StandardPrice=@StandardPrice,");
                this._DataProcess.ProcessParametersAdd("@StandardPrice", SqlDbType.Decimal, 9, mObj.StandardPrice);
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
            builder.Append(" where MaterialCode=@MaterialCode");
            this._DataProcess.ProcessParametersAdd("@MaterialCode", SqlDbType.Int, 4, mObj.MaterialCode);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(MaterialModel mObj)
        {
            return this._Delete(mObj.MaterialCode);
        }

        public MaterialModel GetModel(object Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, MaterialModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.MaterialCode = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.MaterialName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.GroupCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Spec = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Unit = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.StandardPrice = reader.GetDecimal(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.InputPerson = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.InputDate = reader.GetDateTime(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Remark = reader.GetString(8);
            }
        }

        public int Insert(MaterialModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<MaterialModel> Select()
        {
            MaterialQueryModel qmObj = new MaterialQueryModel();
            return this._Select(qmObj);
        }

        public List<MaterialModel> Select(MaterialQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(MaterialModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

