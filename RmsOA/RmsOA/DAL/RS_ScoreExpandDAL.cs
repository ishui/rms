namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using RmsOA.MODEL;
    using RmsPM.BLL;
    using System.Configuration;

    internal class RS_ScoreExpandDAL
    {
        private string connStr = ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString;
        private SqlConnection sqlConn;

        public RS_ScoreExpandDAL()
        {
            this.sqlConn = new SqlConnection(this.connStr);
        }

        public List<UnitModel> GetAllUnit()
        {
            UnitModel item = null;
            List<UnitModel> list = new List<UnitModel>();
            string cmdText = "Select distinct yard from GK_OAPerson";
            SqlCommand command = new SqlCommand(cmdText, this.sqlConn);
            using (SqlConnection connection = this.sqlConn)
            {
                this.sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    item = new UnitModel();
                    item.UnitCode = reader.GetString(0);
                    list.Add(item);
                }
                reader.Close();
            }
            return list;
        }

        public string GetLeaderDept(string userCode)
        {
            string cmdText = "SELECT yard FROM GK_OAPerson WHERE cname IN(\r\nSELECT UserName from SystemUser WHERE UserCode IN(\r\nSELECT DISTINCT(leader) from GK_OAPerson WHERE cname=\r\n(SELECT UserName FROM SystemUser WHERE UserCode=@UserCode)))";
            string text2 = "";
            SqlCommand command = new SqlCommand(cmdText, this.sqlConn);
            command.Parameters.Add("@UserCode", SqlDbType.VarChar, 50).Value = userCode;
            using (SqlConnection connection = this.sqlConn)
            {
                this.sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    text2 = reader.GetString(0);
                }
                reader.Close();
            }
            return text2;
        }

        public List<UnitModel> GetUnitByUserCode(string userCode)
        {
            UnitModel item = null;
            List<UnitModel> list = new List<UnitModel>();
            string cmdText = "Select distinct yard from GK_OAPerson WHERE Leader=@UserCode";
            SqlCommand command = new SqlCommand(cmdText, this.sqlConn);
            command.Parameters.Add("@UserCode", SqlDbType.VarChar, 50).Value = userCode;
            using (SqlConnection connection = this.sqlConn)
            {
                this.sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    item = new UnitModel();
                    item.UnitCode = reader.GetString(0);
                    list.Add(item);
                }
                reader.Close();
            }
            return list;
        }

        public string GetUserNameByCode(string code)
        {
            string text = "";
            string cmdText = "SELECT cname FROM GK_OAPerson WHERE Code = @Code ";
            SqlCommand command = new SqlCommand(cmdText, this.sqlConn);
            command.Parameters.Add("@Code", SqlDbType.Int, 4).Value = int.Parse(code);
            using (SqlConnection connection = this.sqlConn)
            {
                this.sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    text = reader.GetString(0);
                }
                reader.Close();
            }
            return text;
        }

        public string GetUserNameByUserCode(string userCode)
        {
            return SystemRule.GetUserName(userCode);
        }

        public List<UserModel> GetUsersByCode(string userCode)
        {
            UserModel item = null;
            List<UserModel> list = new List<UserModel>();
            string cmdText = "SELECT Code, cname FROM GK_OAPerson WHERE Leader=@Leader";
            SqlCommand command = new SqlCommand(cmdText, this.sqlConn);
            command.Parameters.Add("@Leader", SqlDbType.VarChar, 50).Value = userCode;
            using (SqlConnection connection = this.sqlConn)
            {
                this.sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    item = new UserModel();
                    item.UserCode = reader.GetInt32(0).ToString();
                    item.UserName = reader.GetString(1);
                    list.Add(item);
                }
                reader.Close();
            }
            return list;
        }

        public List<UserModel> GetUsersByCode(string userCode, string deptCode)
        {
            UserModel item = null;
            List<UserModel> list = new List<UserModel>();
            string cmdText = "SELECT Code, cname FROM GK_OAPerson WHERE yard=@yard AND Leader=@Leader";
            SqlCommand command = new SqlCommand(cmdText, this.sqlConn);
            command.Parameters.Add("@yard", SqlDbType.VarChar, 50).Value = deptCode;
            command.Parameters.Add("@Leader", SqlDbType.VarChar, 50).Value = userCode;
            using (SqlConnection connection = this.sqlConn)
            {
                this.sqlConn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    item = new UserModel();
                    item.UserCode = reader.GetInt32(0).ToString();
                    item.UserName = reader.GetString(1);
                    list.Add(item);
                }
                reader.Close();
            }
            return list;
        }
    }
}

