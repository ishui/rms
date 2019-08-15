namespace Rms.ORMap
{
    using System;
    using System.Data;

    public interface IDBCommon
    {
        void BeginTrans();
        void Close();
        void CommitTrans();
        object ExecForDataReader(string queryString);
        DataSet ExecSPForDataSet(string queryString, string[] parameterNames, object[] values);
        void ExecSql(string sqlString);
        void ExecSql(DataRow datarow, SqlStruct sqlstruct);
        void ExecSql(string sqlString, string[] paramsList, object[] paramsValue);
        DataSet ExecSqlForDataSet(string queryString);
        DataSet ExecSqlForDataSet(string queryString, string tableName);
        DataSet ExecSqlForDataSet(string sqlString, string[] paramsList, object[] paramsValue);
        DataTable ExecSqlForDataTable(string queryString);
        DataTable ExecSqlForDataTable(string queryString, string tableName);
        object ExecuteScalar(string queryString);
        void FillEntity(string sqlString, EntityData entitydata);
        void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata);
        void FillEntity(string sqlString, string Param, object Value, EntityData entitydata);
        void FillEntity(string sqlString, string[] Params, object[] Values, EntityData entitydata, string tableName);
        void FillEntity(string sqlString, string Param, object Value, EntityData entitydata, string tableName);
        void Open();
        void RollbackTrans();
        void SubmitAllData(DataTable datatable, SqlStruct sqlInsertStruct, SqlStruct sqlUpdateStruct, SqlStruct sqlDeleteStruct);
        void SubmitDataTable(DataTable datatable, SqlStruct sqlStruct, SqlOperatorType sqlOperatorType);
    }
}

