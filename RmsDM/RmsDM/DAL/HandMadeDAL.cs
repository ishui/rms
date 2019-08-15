namespace RmsDM.DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class HandMadeDAL
    {
        private SqlDataProcess _DataProcess;
        private SqlConnection _SqlConnection;

        public HandMadeDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
            this._SqlConnection = Connection;
        }

        public HandMadeDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        public int GetFileTemplateCodeByProcCode(string ProcedureCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FileTemplateCode from filetemplateversion V join workflowProcedure P \r\n\t                            on V.WorkFlowProcedureName=P.ProcedureName\r\n\t                            where P.ProcedureCode=@ProcedureCode and V.IsAvailability='有效'\r\n                            ");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.SqlParameters.Add(new SqlParameter("@ProcedureCode", ProcedureCode));
            SqlDataReader sqlDataReader = this._DataProcess.GetSqlDataReader();
            if (sqlDataReader.Read())
            {
                if (!sqlDataReader.IsDBNull(0))
                {
                    return sqlDataReader.GetInt32(0);
                }
                return 0;
            }
            return 0;
        }

        public DataSet GetProcedureDS()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select wfp.ProcedureCode,wfp.Description,wfp.ProcedureName,wfp.Type,wfp.ApplicationPath\r\n                            from Workflowprocedure wfp\r\n                            where wfp.Activity=1 and wfp.Type=1");
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.GetDataSet();
        }

        public void UpVersionState(int tempCode)
        {
            string text = "UPDATE FileTemplateVersion SET IsAvailability='无效' WHERE FileTemplateCode = " + tempCode;
            SqlCommand command = new SqlCommand();
            command.CommandText = text;
            command.Connection = this._SqlConnection;
            try
            {
                try
                {
                    this._SqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                this._SqlConnection.Close();
                command.Dispose();
            }
        }
    }
}

