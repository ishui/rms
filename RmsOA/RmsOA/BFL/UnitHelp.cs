namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using RmsPM.BLL;

    public sealed class UnitHelp
    {
        private SqlConnection sqlConn = new SqlConnection(FunctionRule.GetConnectionString());

        public List<string> GetListUnitCodeByName(string name)
        {
            List<string> list = new List<string>();
            string text = "SELECT UNITCODE FROM UNIT WHERE UNITNAME = @UNITNAME";
            SqlCommand command = this.sqlConn.CreateCommand();
            command.CommandText = text;
            command.Parameters.Add("@UNITNAME", SqlDbType.VarChar, 50).Value = name;
            try
            {
                try
                {
                    this.sqlConn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    using (SqlDataReader reader2 = reader)
                    {
                        while (reader.Read())
                        {
                            if (!reader.GetValue(0).Equals(DBNull.Value))
                            {
                                list.Add(reader.GetString(0));
                            }
                        }
                    }
                    return list;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                command.Dispose();
                this.sqlConn.Close();
            }
            return list;
        }

        public string GetUnitCode(string fullName, string name)
        {
            string text = string.Empty;
            List<string> listUnitCodeByName = this.GetListUnitCodeByName(name);
            foreach (string text2 in listUnitCodeByName)
            {
                if (fullName.Equals(SystemRule.GetUnitFullName(text2)))
                {
                    return text2;
                }
            }
            return text;
        }
    }
}

