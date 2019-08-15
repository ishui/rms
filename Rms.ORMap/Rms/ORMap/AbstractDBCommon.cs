namespace Rms.ORMap
{
    using System;
    using System.Data;

    public abstract class AbstractDBCommon : IDBCommon
    {
        protected string m_ConnectionString = "";

        protected AbstractDBCommon()
        {
        }

        public abstract void BeginTrans();
        public abstract void Close();
        public abstract void CommitTrans();
        public abstract object ExecForDataReader(string queryString);
        public abstract DataSet ExecSPForDataSet(string queryString, string[] parameterNames, object[] values);
        public abstract void ExecSql(string sqlString);
        public abstract void ExecSql(DataRow datarow, SqlStruct sqlstruct);
        public abstract void ExecSql(string sqlString, string[] paramsList, object[] paramsValue);
        public abstract DataSet ExecSqlForDataSet(string queryString);
        public abstract DataSet ExecSqlForDataSet(string queryString, string tableName);
        public abstract DataSet ExecSqlForDataSet(string sqlString, string[] paramsList, object[] paramsValue);
        public abstract DataTable ExecSqlForDataTable(string queryString);
        public abstract DataTable ExecSqlForDataTable(string queryString, string tableName);
        public abstract object ExecuteScalar(string queryString);
        public abstract void FillEntity(string sqlString, EntityData entitydata);
        public abstract void FillEntity(string sqlString, string Param, object Value, EntityData entitydata);
        public abstract void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata);
        public abstract void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata, string tableName);
        public abstract void FillEntity(string sqlString, string Param, object Value, EntityData entitydata, string tableName);
        public abstract void Open();
        public abstract void RollbackTrans();
        public abstract void SubmitAllData(DataTable datatable, SqlStruct sqlInsertStruct, SqlStruct sqlUpdateStruct, SqlStruct sqlDeleteStruct);
        public abstract void SubmitDataTable(DataTable datatable, SqlStruct sqlStruct, SqlOperatorType sqlOperatorType);

        public abstract string State { get; }
    }
}

