namespace Rms.ORMap
{
    using System;
    using System.Data.SqlClient;

    public class SqlClientQueryAgent : QueryAgent
    {
        public virtual SqlDataReader ExecSqlForDataReader(string queryString)
        {
            SqlDataReader reader2;
            queryString = this.BuildTopQueryString(queryString);
            try
            {
                SqlDataReader reader = (SqlDataReader) base.db.ExecForDataReader(queryString);
                reader2 = reader;
            }
            catch (Exception exception)
            {
                base.db.Close();
                throw exception;
            }
            return reader2;
        }
    }
}

