namespace Codefresh.PhotoBrowserLibrary.DataAccessLayer
{
    using Codefresh.PhotoBrowserLibrary;
    using System;
    using System.Data;
    using System.Data.OleDb;

    internal abstract class DBBase
    {
        protected IDbConnection conn;

        public DBBase(IDbConnection conn)
        {
            this.conn = conn;
        }

        protected IDbDataParameter CreateDateParam(string name, DateTime val)
        {
            if (val == DateTime.MinValue)
            {
                return this.CreateParam(name, DbType.Date, DBNull.Value);
            }
            return this.CreateParam(name, DbType.Date, val);
        }

        protected IDbDataParameter CreateDateTimeParam(string name, DateTime val)
        {
            if (val == DateTime.MinValue)
            {
                return this.CreateParam(name, DbType.DateTime, DBNull.Value);
            }
            return this.CreateParam(name, DbType.DateTime, val);
        }

        protected IDbDataParameter CreateIntParam(string name, int val)
        {
            return this.CreateParam(name, DbType.Int32, val);
        }

        protected IDbDataParameter CreateLongParam(string name, long val)
        {
            return this.CreateParam(name, DbType.Int64, val);
        }

        public IDbDataParameter CreateParam(string name, DbType type, object obj)
        {
            OleDbParameter parameter = new OleDbParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = type;
            parameter.Value = obj;
            parameter.ParameterName = name;
            return parameter;
        }

        protected IDbDataParameter CreateStringParam(string name, string val)
        {
            return this.CreateParam(name, DbType.String, val);
        }

        internal abstract void Delete(PhotoObjectBase obj);
        protected IDbCommand GetCommand()
        {
            IDbCommand command = this.conn.CreateCommand();
            command.CommandType = CommandType.Text;
            return command;
        }

        protected int GetIdentityValue(PhotoObjectBase obj)
        {
            IDbCommand command = this.GetCommand();
            command.CommandText = "SELECT @@IDENTITY";
            int id = (int) command.ExecuteScalar();
            obj.SetId(id);
            return id;
        }
    }
}

