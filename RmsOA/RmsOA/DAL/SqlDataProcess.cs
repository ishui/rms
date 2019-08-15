namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlDataProcess
    {
        private SqlCommand _Command;
        private List<SqlParameter> _SqlParameters;

        public SqlDataProcess()
        {
            this._Command = new SqlCommand();
            this._SqlParameters = new List<SqlParameter>();
        }

        public SqlDataProcess(SqlConnection Connection)
        {
            this._Command = new SqlCommand();
            this._SqlParameters = new List<SqlParameter>();
            this._Command.Connection = Connection;
        }

        public SqlDataProcess(SqlTransaction Transaction)
        {
            this._Command = new SqlCommand();
            this._SqlParameters = new List<SqlParameter>();
            this._Command.Connection = Transaction.Connection;
            this._Command.Transaction = Transaction;
        }

        private DataSet _GetDataSet()
        {
            if (this._SqlParameters != null)
            {
                foreach (SqlParameter parameter in this._SqlParameters)
                {
                    this._Command.Parameters.Add(parameter);
                }
            }
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = this._Command;
            try
            {
                if (this._Command.Connection.State == ConnectionState.Closed)
                {
                    this._Command.Connection.Open();
                }
                adapter.Fill(dataSet);
                this._Command.Parameters.Clear();
                this._SqlParameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            this._Command.Dispose();
            return dataSet;
        }

        private SqlDataReader _GetSqlDataReader()
        {
            SqlDataReader reader;
            if (this._SqlParameters != null)
            {
                foreach (SqlParameter parameter in this._SqlParameters)
                {
                    this._Command.Parameters.Add(parameter);
                }
            }
            try
            {
                if (this._Command.Connection.State == ConnectionState.Closed)
                {
                    this._Command.Connection.Open();
                }
                reader = this._Command.ExecuteReader();
                this._Command.Parameters.Clear();
                this._SqlParameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            this._Command.Dispose();
            return reader;
        }

        private int _RunSql()
        {
            int num = 0;
            if (this._SqlParameters != null)
            {
                foreach (SqlParameter parameter in this._SqlParameters)
                {
                    this._Command.Parameters.Add(parameter);
                }
            }
            try
            {
                if (this._Command.Connection.State == ConnectionState.Closed)
                {
                    this._Command.Connection.Open();
                }
                num = this._Command.ExecuteNonQuery();
                this._Command.Parameters.Clear();
                this._SqlParameters.Clear();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            this._Command.Dispose();
            return num;
        }

        public DataSet GetDataSet()
        {
            return this._GetDataSet();
        }

        public DataSet GetDataSet(string CommandText)
        {
            this._Command.CommandText = CommandText;
            return this._GetDataSet();
        }

        public SqlDataReader GetSqlDataReader()
        {
            return this._GetSqlDataReader();
        }

        public SqlDataReader GetSqlDataReader(string CommandText)
        {
            this._Command.CommandText = CommandText;
            return this._GetSqlDataReader();
        }

        public SqlParameter ProcessParametersAdd(string ParameterName, SqlDbType ParameterType, int Size, object Value)
        {
            SqlParameter item = new SqlParameter();
            item.ParameterName = ParameterName;
            item.SqlDbType = ParameterType;
            item.Size = Size;
            item.Value = Value;
            this._SqlParameters.Add(item);
            return item;
        }

        public int RunSql()
        {
            return this._RunSql();
        }

        public int RunSql(string CommandText)
        {
            this._Command.CommandText = CommandText;
            return this._RunSql();
        }

        public string CommandText
        {
            get
            {
                return this._Command.CommandText;
            }
            set
            {
                this._Command.CommandText = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return this._Command.Connection;
            }
            set
            {
                this._Command.Connection = value;
            }
        }

        public bool IsProcess
        {
            get
            {
                return (this._Command.CommandType == CommandType.StoredProcedure);
            }
            set
            {
                if (value)
                {
                    this._Command.CommandType = CommandType.StoredProcedure;
                }
            }
        }

        public List<SqlParameter> SqlParameters
        {
            get
            {
                return this._SqlParameters;
            }
            set
            {
                if (value != null)
                {
                    foreach (SqlParameter parameter in value)
                    {
                        this._SqlParameters.Add(parameter);
                    }
                }
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return this._Command.Transaction;
            }
            set
            {
                this._Command.Transaction = value;
            }
        }
    }
}

