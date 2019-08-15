namespace Rms.ORMap
{
    using System;
    using System.Data;

    public abstract class AbstractEntityDAO : IEntityDAO, IDisposable
    {
        protected IDBCommon db;
        protected string m_EntityName;

        public AbstractEntityDAO(string entityName)
        {
            this.db = DBCommonBuilder.BuildDBCommon();
            this.db.Open();
            this.m_EntityName = entityName;
        }

        public AbstractEntityDAO(string entityName, IDBCommon cdb)
        {
            this.db = cdb;
            this.db.Open();
            this.m_EntityName = entityName;
        }

        public virtual void BeginTrans()
        {
            this.db.BeginTrans();
        }

        protected virtual void CheckData(EntityData entity)
        {
        }

        public virtual void CommitTrans()
        {
            this.db.CommitTrans();
        }

        public virtual void DeleteAllRow(EntityData entitydata)
        {
            try
            {
                DataTable table = entitydata.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    row.Delete();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public virtual void DeleteEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(this.m_EntityName, "Delete");
                if (sqlStruct.SqlString.Length != 0)
                {
                    this.db.SubmitDataTable(entitydata.Tables[this.m_EntityName], sqlStruct, SqlOperatorType.Delete);
                }
            }
            catch (Exception exception)
            {
                throw exception;
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

        public virtual void FillEntity(string sqlString, EntityData entitydata)
        {
            try
            {
                if (sqlString.Length != 0)
                {
                    this.db.FillEntity(sqlString, entitydata);
                }
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
        }

        public virtual void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata)
        {
            try
            {
                if (sqlString.Length != 0)
                {
                    this.db.FillEntity(sqlString, Params, Values, entitydata);
                }
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
        }

        public virtual void FillEntity(string sqlString, string Param, object Value, EntityData entitydata)
        {
            try
            {
                if (sqlString.Length != 0)
                {
                    this.db.FillEntity(sqlString, Param, Value, entitydata);
                }
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
        }

        public virtual void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata, string tableName)
        {
            try
            {
                if (sqlString.Length != 0)
                {
                    this.db.FillEntity(sqlString, Params, Values, entitydata, tableName);
                }
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
        }

        public virtual void FillEntity(string sqlString, string Param, object Value, EntityData entitydata, string tableName)
        {
            try
            {
                if (sqlString.Length != 0)
                {
                    this.db.FillEntity(sqlString, Param, Value, entitydata, tableName);
                }
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
        }

        ~AbstractEntityDAO()
        {
        }

        public virtual void InsertEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(this.m_EntityName, "Insert");
                if (sqlStruct.SqlString.Length != 0)
                {
                    this.db.SubmitDataTable(entitydata.Tables[this.m_EntityName], sqlStruct, SqlOperatorType.Insert);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public virtual void RollBackTrans()
        {
            this.db.RollbackTrans();
        }

        public virtual EntityData SelectAll()
        {
            EntityData data2;
            try
            {
                //初始化this.m_EntityName表的表结构
                EntityData entitydata = new EntityData(this.m_EntityName);
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(this.m_EntityName, "SelectAll");
                if (sqlStruct.SqlString.Length != 0)
                {
                    this.db.FillEntity(sqlStruct.GetSqlStringWithOrder(), (string[]) null, (object[]) null, entitydata, this.m_EntityName);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
            return data2;
        }

        public virtual EntityData SelectbyPrimaryKey(object[] keyvalues)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData(this.m_EntityName);
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(this.m_EntityName, "Select");
                if (sqlStruct.SqlString.Length != 0)
                {
                    this.db.FillEntity(sqlStruct.SqlString, sqlStruct.ParamsList, keyvalues, entitydata, this.m_EntityName);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
            return data2;
        }

        public virtual EntityData SelectbyPrimaryKey(object keyvalues)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData(this.m_EntityName);
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(this.m_EntityName, "Select");
                if (sqlStruct.SqlString.Length != 0)
                {
                    if (1 != sqlStruct.ParamsList.Length)
                    {
                        throw new ApplicationException("参数列表不匹配");
                    }
                    this.db.FillEntity(sqlStruct.SqlString, sqlStruct.ParamsList[0], keyvalues, entitydata, this.m_EntityName);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                this.db.Close();
                throw exception;
            }
            return data2;
        }

        public virtual void SubmitEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                this.db.SubmitAllData(entitydata.Tables[this.m_EntityName], SqlManager.GetSqlStruct(this.m_EntityName, "Insert"), SqlManager.GetSqlStruct(this.m_EntityName, "Update"), SqlManager.GetSqlStruct(this.m_EntityName, "Delete"));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public virtual void UpdateEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(this.m_EntityName, "Update");
                if (sqlStruct.SqlString.Length != 0)
                {
                    this.db.SubmitDataTable(entitydata.Tables[this.m_EntityName], sqlStruct, SqlOperatorType.Update);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string EntityName
        {
            get
            {
                return this.m_EntityName;
            }
            set
            {
                this.m_EntityName = value;
            }
        }
    }
}

