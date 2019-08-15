namespace Rms.ORMap
{
    using System;
    using System.Data;
    using System.Text.RegularExpressions;

    public class QueryAgent
    {
        protected IDBCommon db;
        private bool m_IsOutSide;
        private int m_TopNumber;

        public QueryAgent()
        {
            this.m_TopNumber = 0;
            this.m_IsOutSide = false;
            this.db = DBCommonBuilder.BuildDBCommon();
            this.db.Open();
        }

        public QueryAgent(IDBCommon outsideDB)
        {
            this.m_TopNumber = 0;
            this.m_IsOutSide = false;
            this.db = outsideDB;
            this.db.Open();
            this.m_IsOutSide = true;
        }

        protected virtual string BuildTopQueryString(string queryString)
        {
            if (this.m_TopNumber != 0)
            {
                string pattern = "([S|s])([E|e])([L|l])([E|e])([C|c])([T|t])";
                queryString = new Regex(pattern).Replace(queryString, " select  Top " + this.m_TopNumber.ToString() + " ", 1, 0);
            }
            return queryString;
        }

        private void CloseDB()
        {
            if (!this.m_IsOutSide)
            {
                this.db.Close();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Close();
            }
        }

        public virtual DataSet ExecSPForDataSet(string queryString, string[] parameterNames, object[] values)
        {
            DataSet set2;
            queryString = this.BuildTopQueryString(queryString);
            try
            {
                set2 = this.db.ExecSPForDataSet(queryString, parameterNames, values);
            }
            catch (Exception exception)
            {
                this.CloseDB();
                throw exception;
            }
            return set2;
        }

        public virtual DataSet ExecSqlForDataSet(string queryString)
        {
            DataSet set2;
            queryString = this.BuildTopQueryString(queryString);
            try
            {
                set2 = this.db.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                this.CloseDB();
                throw exception;
            }
            return set2;
        }

        public virtual DataSet ExecSqlForDataSet(string queryString, string[] parameterNames, object[] values)
        {
            DataSet set2;
            queryString = this.BuildTopQueryString(queryString);
            try
            {
                set2 = this.db.ExecSqlForDataSet(queryString, parameterNames, values);
            }
            catch (Exception exception)
            {
                this.CloseDB();
                throw exception;
            }
            return set2;
        }

        public virtual object ExecuteScalar(string queryString)
        {
            object obj3;
            try
            {
                obj3 = this.db.ExecuteScalar(queryString);
            }
            catch (Exception exception)
            {
                this.CloseDB();
                throw exception;
            }
            return obj3;
        }

        public virtual void ExecuteSql(string queryString)
        {
            try
            {
                this.db.ExecSql(queryString);
            }
            catch (Exception exception)
            {
                this.CloseDB();
                throw exception;
            }
        }

        public virtual EntityData FillEntityData(string entityName, string queryString)
        {
            EntityData data2;
            queryString = this.BuildTopQueryString(queryString);
            try
            {
                EntityData entitydata = new EntityData(entityName);
                this.db.FillEntity(queryString, entitydata);
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                this.CloseDB();
                throw exception;
            }
            return data2;
        }

        ~QueryAgent()
        {
        }

        public virtual void SetTopNumber(int topNumber)
        {
            if (topNumber < 0)
            {
                throw new ApplicationException("查询数据行数不能为负数 ！");
            }
            this.m_TopNumber = topNumber;
        }
    }
}

