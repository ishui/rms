namespace TiannuoPM.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Data;
    using System.Data.Common;

    public class TransactionManager : IDisposable
    {
        private bool _transactionOpen;
        private DbConnection connection;
        private string connectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database database;
        private bool disposed;
        private static object syncRoot = new object();
        private DbTransaction transaction;

        internal TransactionManager()
        {
            this._transactionOpen = false;
        }

        public TransactionManager(string connectionString)
        {
            this._transactionOpen = false;
            this.connectionString = connectionString;
            this.database = new SqlDatabase(connectionString);
            this.connection = this.database.CreateConnection();
        }

        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (this.IsOpen)
            {
                throw new InvalidOperationException("Transaction already open.");
            }
            try
            {
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(isolationLevel);
                this._transactionOpen = true;
            }
            catch (Exception)
            {
                if (this.connection != null)
                {
                    this.connection.Close();
                }
                if (this.transaction != null)
                {
                    this.transaction.Dispose();
                }
                this._transactionOpen = false;
                throw;
            }
        }

        public void Commit()
        {
            if (!this.IsOpen)
            {
                throw new InvalidOperationException("Transaction needs to begin first.");
            }
            try
            {
                this.transaction.Commit();
            }
            finally
            {
                this.connection.Close();
                this.transaction.Dispose();
                this._transactionOpen = false;
            }
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                lock (syncRoot)
                {
                    this.disposed = true;
                    if (this.IsOpen)
                    {
                        this.Rollback();
                    }
                }
            }
        }

        public void Rollback()
        {
            if (!this.IsOpen)
            {
                throw new InvalidOperationException("Transaction needs to begin first.");
            }
            try
            {
                this.transaction.Rollback();
            }
            finally
            {
                this.connection.Close();
                this.transaction.Dispose();
                this._transactionOpen = false;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            set
            {
                if (this.IsOpen)
                {
                    throw new InvalidOperationException("Database cannot be changed during a transaction");
                }
                this.connectionString = value;
                this.database = new SqlDatabase(this.connectionString);
                this.connection = this.database.CreateConnection();
            }
        }

        public Microsoft.Practices.EnterpriseLibrary.Data.Database Database
        {
            get
            {
                return this.database;
            }
        }

        public bool IsOpen
        {
            get
            {
                return this._transactionOpen;
            }
        }

        public DbTransaction TransactionObject
        {
            get
            {
                return this.transaction;
            }
        }
    }
}

