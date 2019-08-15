namespace Rms.ORMap
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlClientDBCommon : AbstractDBCommon
    {
        private SqlConnection m_DBConnection = null;
        private bool m_IsInTransaction = false;
        private SqlTransaction m_Transaction;

        public SqlClientDBCommon(string connectionString)
        {
            base.m_ConnectionString = connectionString;
            this.Initialize();
        }

        public override void BeginTrans()
        {
            try
            {
                this.m_Transaction = this.m_DBConnection.BeginTransaction();
                this.m_IsInTransaction = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private SqlCommand BuildSPCommand(string sqlString, string[] parameterNames, object[] values)
        {
            SqlCommand command = new SqlCommand(sqlString, this.m_DBConnection);
            command.CommandTimeout = this.m_DBConnection.ConnectionTimeout;
            command.CommandType = CommandType.StoredProcedure;
            int length = parameterNames.Length;
            for (int i = 0; i < length; i++)
            {
                command.Parameters.Add(parameterNames[i], values[i]);
            }
            if (this.m_IsInTransaction)
            {
                command.Transaction = this.m_Transaction;
            }
            return command;
        }

        private SqlCommand BuildSqlCommand(string sqlString)
        {
            SqlCommand command = new SqlCommand(sqlString, this.m_DBConnection);
            command.CommandTimeout = this.m_DBConnection.ConnectionTimeout;
            if (this.m_IsInTransaction)
            {
                command.Transaction = this.m_Transaction;
            }
            return command;
        }

        private SqlCommand BuildSqlCommandByStructure(SqlStruct sqlStruct)
        {
            SqlCommand command2;
            if (sqlStruct.SqlString.Length == 0)
            {
                return null;
            }
            try
            {
                SqlDbType varChar = SqlDbType.VarChar;
                SqlCommand command = this.BuildSqlCommand(sqlStruct.SqlString);
                if (sqlStruct.CommandType == "Text")
                {
                    command.CommandType = CommandType.Text;
                }
                else if (sqlStruct.CommandType == "StoredProcedure")
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                else if (sqlStruct.CommandType == "TableDirect")
                {
                    command.CommandType = CommandType.TableDirect;
                }
                int length = sqlStruct.ParamsList.Length;
                for (int i = 0; i < length; i++)
                {
                    SqlParameter parameter = new SqlParameter(sqlStruct.ParamsList[i], (SqlDbType) Enum.Parse(varChar.GetType(), sqlStruct.SqlDbTypeList[i]));
                    parameter.SourceColumn = sqlStruct.ColumnsList[i];
                    command.Parameters.Add(parameter);
                }
                command2 = command;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return command2;
        }

        public override void Close()
        {
            try
            {
                if (this.m_Transaction != null)
                {
                    this.m_Transaction.Dispose();
                }
                this.m_DBConnection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override void CommitTrans()
        {
            try
            {
                this.m_Transaction.Commit();
                this.m_IsInTransaction = false;
                this.m_Transaction = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override object ExecForDataReader(string queryString)
        {
            object obj2;
            if (queryString.Length == 0)
            {
                return null;
            }
            try
            {
                this.Open();
                obj2 = this.BuildSqlCommand(queryString).ExecuteReader();
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + queryString;
                throw exception;
            }
            return obj2;
        }

        public override DataSet ExecSPForDataSet(string queryString, string[] parameterNames, object[] values)
        {
            DataSet set2;
            if (queryString.Length == 0)
            {
                return null;
            }
            try
            {
                this.Open();
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(this.BuildSPCommand(queryString, parameterNames, values));
                adapter.Fill(dataSet);
                adapter.Dispose();
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + queryString;
                throw exception;
            }
            return set2;
        }

        public override void ExecSql(string sqlString)
        {
            if (sqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand command = this.BuildSqlCommand(sqlString);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlString;
                    throw exception;
                }
            }
        }

        public override void ExecSql(DataRow datarow, SqlStruct sqlstruct)
        {
            if (sqlstruct.SqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand command = this.BuildSqlCommand(sqlstruct.SqlString);
                    if (sqlstruct.ParamsList.Length != sqlstruct.ParamsList.Length)
                    {
                        throw new ApplicationException("参数和参数值不匹配");
                    }
                    int length = sqlstruct.ParamsList.Length;
                    if (length != 0)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            try
                            {
                                command.Parameters.Add(sqlstruct.ParamsList[i], datarow[sqlstruct.ColumnsList[i]]);
                            }
                            catch
                            {
                                throw new ApplicationException("数据结构和数据行不匹配");
                            }
                        }
                    }
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlstruct.SqlString;
                    throw exception;
                }
            }
        }

        public override void ExecSql(string sqlString, string[] paramsList, object[] paramsValue)
        {
            if (sqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand command = this.BuildSqlCommand(sqlString);
                    if (paramsList.Length != paramsValue.Length)
                    {
                        throw new ApplicationException("参数和参数值不匹配");
                    }
                    int length = paramsList.Length;
                    if (length != 0)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            command.Parameters.Add(paramsList[i], paramsValue[i]);
                        }
                    }
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlString;
                    throw exception;
                }
            }
        }

        public override DataSet ExecSqlForDataSet(string queryString)
        {
            DataSet set2;
            if (queryString.Length == 0)
            {
                return null;
            }
            try
            {
                this.Open();
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, this.m_DBConnection);
                adapter.Fill(dataSet);
                adapter.Dispose();
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + queryString;
                throw exception;
            }
            return set2;
        }

        public override DataSet ExecSqlForDataSet(string queryString, string tableName)
        {
            DataSet set2;
            if (queryString.Length == 0)
            {
                return null;
            }
            try
            {
                this.Open();
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, this.m_DBConnection);
                adapter.Fill(dataSet, tableName);
                adapter.Dispose();
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + queryString;
                throw exception;
            }
            return set2;
        }

        public override DataSet ExecSqlForDataSet(string sqlString, string[] paramsList, object[] paramsValue)
        {
            DataSet set2;
            if (sqlString.Length == 0)
            {
                return null;
            }
            try
            {
                this.Open();
                DataSet dataSet = new DataSet();
                SqlCommand selectCommand = this.BuildSqlCommand(sqlString);
                if (paramsList.Length != paramsValue.Length)
                {
                    throw new ApplicationException("参数和参数值不匹配");
                }
                int length = paramsList.Length;
                if (length != 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        selectCommand.Parameters.Add(paramsList[i], paramsValue[i]);
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                adapter.Fill(dataSet);
                adapter.Dispose();
                selectCommand.Dispose();
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + sqlString;
                throw exception;
            }
            return set2;
        }

        public override DataTable ExecSqlForDataTable(string queryString)
        {
            DataTable table;
            if (queryString.Length == 0)
            {
                return null;
            }
            try
            {
                this.Open();
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, this.m_DBConnection);
                adapter.Fill(dataSet);
                adapter.Dispose();
                table = dataSet.Tables[0];
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + queryString;
                throw exception;
            }
            return table;
        }

        public override DataTable ExecSqlForDataTable(string queryString, string tableName)
        {
            DataTable table;
            if (queryString.Length == 0)
            {
                return null;
            }
            try
            {
                this.Open();
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, this.m_DBConnection);
                adapter.Fill(dataSet, tableName);
                adapter.Dispose();
                table = dataSet.Tables[0];
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + queryString;
                throw exception;
            }
            return table;
        }

        public override object ExecuteScalar(string queryString)
        {
            object obj3;
            try
            {
                this.Open();
                obj3 = this.BuildSqlCommand(queryString).ExecuteScalar();
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： " + queryString;
                throw exception;
            }
            return obj3;
        }

        public override void FillEntity(string sqlString, EntityData entitydata)
        {
            if (sqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand selectCommand = this.BuildSqlCommand(sqlString);
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    adapter.Fill(entitydata, entitydata.ClassName);
                    selectCommand.Dispose();
                    adapter.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlString;
                    throw exception;
                }
            }
        }

        public override void FillEntity(string sqlString, string Param, object Value, EntityData entitydata)
        {
            if (sqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand selectCommand = this.BuildSqlCommand(sqlString);
                    if (Param != null)
                    {
                        selectCommand.Parameters.Add(Param, Value);
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    adapter.Fill(entitydata);
                    selectCommand.Dispose();
                    adapter.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlString;
                    throw exception;
                }
            }
        }

        public override void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata)
        {
            if (sqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand selectCommand = this.BuildSqlCommand(sqlString);
                    if (Params.Length != Values.Length)
                    {
                        throw new ApplicationException("参数和参数值不匹配");
                    }
                    int length = Params.Length;
                    if (length != 0)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            if (Params[i] != null)
                            {
                                selectCommand.Parameters.Add(Params[i], Values[i]);
                            }
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    adapter.Fill(entitydata);
                    selectCommand.Dispose();
                    adapter.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlString;
                    throw exception;
                }
            }
        }

        public override void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata, string tableName)
        {
            if (sqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand selectCommand = this.BuildSqlCommand(sqlString);
                    if ((Params != null) && (Values != null))
                    {
                        if (Params.Length != Values.Length)
                        {
                            throw new ApplicationException("参数和参数值不匹配");
                        }
                        int length = Params.Length;
                        if (length != 0)
                        {
                            for (int i = 0; i < length; i++)
                            {
                                if (Params[i] != null)
                                {
                                    selectCommand.Parameters.Add(Params[i], Values[i]);
                                }
                            }
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    adapter.Fill(entitydata, tableName);
                    selectCommand.Dispose();
                    adapter.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlString;
                    throw exception;
                }
            }
        }

        public override void FillEntity(string sqlString, string Param, object Value, EntityData entitydata, string tableName)
        {
            if (sqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlCommand selectCommand = this.BuildSqlCommand(sqlString);
                    if (Param != null)
                    {
                        selectCommand.Parameters.Add(Param, Value);
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    adapter.Fill(entitydata, tableName);
                    selectCommand.Dispose();
                    adapter.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlString;
                    throw exception;
                }
            }
        }

        public SqlConnection GetSqlConnection()
        {
            return this.m_DBConnection;
        }

        private void Initialize()
        {
            try
            {
                this.m_DBConnection = new SqlConnection(base.m_ConnectionString);
                this.m_DBConnection.Open();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override void Open()
        {
            try
            {
                if (ConnectionState.Open != this.m_DBConnection.State)
                {
                    this.m_DBConnection.Open();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("打不开数据库服务器\n" + exception.Message);
            }
        }

        public override void RollbackTrans()
        {
            try
            {
                this.m_IsInTransaction = false;
                this.m_Transaction.Rollback();
                this.m_Transaction = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override void SubmitAllData(DataTable datatable, SqlStruct sqlInsertStruct, SqlStruct sqlUpdateStruct, SqlStruct sqlDeleteStruct)
        {
            try
            {
                this.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                if (sqlInsertStruct.SqlString.Length != 0)
                {
                    adapter.InsertCommand = this.BuildSqlCommandByStructure(sqlInsertStruct);
                }
                if (sqlUpdateStruct.SqlString.Length != 0)
                {
                    adapter.UpdateCommand = this.BuildSqlCommandByStructure(sqlUpdateStruct);
                }
                if (sqlDeleteStruct.SqlString.Length != 0)
                {
                    adapter.DeleteCommand = this.BuildSqlCommandByStructure(sqlDeleteStruct);
                }
                adapter.Update(datatable);
                if (adapter.InsertCommand != null)
                {
                    adapter.InsertCommand.Dispose();
                }
                if (adapter.UpdateCommand != null)
                {
                    adapter.UpdateCommand.Dispose();
                }
                if (adapter.DeleteCommand != null)
                {
                    adapter.DeleteCommand.Dispose();
                }
                adapter.Dispose();
            }
            catch (Exception exception)
            {
                exception.HelpLink = " 相关sql语句： /n插入语句： " + sqlInsertStruct.SqlString + "/n更新语句： " + sqlUpdateStruct.SqlString + "/n删除语句： " + sqlDeleteStruct.SqlString + "/n";
                throw exception;
            }
        }

        public override void SubmitDataTable(DataTable datatable, SqlStruct sqlStruct, SqlOperatorType sqlOperatorType)
        {
            if (sqlStruct.SqlString.Length != 0)
            {
                try
                {
                    this.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = this.BuildSqlCommandByStructure(sqlStruct);
                    switch (sqlOperatorType)
                    {
                        case SqlOperatorType.Insert:
                            adapter.InsertCommand = command;
                            break;

                        case SqlOperatorType.Update:
                            adapter.UpdateCommand = command;
                            break;

                        case SqlOperatorType.Delete:
                            adapter.DeleteCommand = command;
                            break;
                    }
                    adapter.Update(datatable);
                    command.Dispose();
                    adapter.Dispose();
                }
                catch (Exception exception)
                {
                    exception.HelpLink = " 相关sql语句： " + sqlStruct.SqlString;
                    throw exception;
                }
            }
        }

        public override string State
        {
            get
            {
                return this.m_DBConnection.State.ToString();
            }
        }
    }
}

