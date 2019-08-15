namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_CardsFolderDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_CardsFolderDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_CardsFolderDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_CardsFolderModel _DataBind(int Code)
        {
            GK_OA_CardsFolderModel model = new GK_OA_CardsFolderModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_CardsFolder ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            SqlDataReader sqlDataReader = null;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        this.Initialize(sqlDataReader, model);
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return model;
        }

        private int _Delete(int Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from GK_OA_CardsFolder ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_CardsFolderModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO GK_OA_CardsFolder (");
            builder2.Append("VALUES(");
            if (mObj.UserId != null)
            {
                builder.Append("UserId,");
                builder2.Append("@UserId,");
                this._DataProcess.ProcessParametersAdd("@UserId", SqlDbType.VarChar, 50, mObj.UserId);
            }
            if (mObj.UserName != null)
            {
                builder.Append("UserName,");
                builder2.Append("@UserName,");
                this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            }
            if (mObj.Sex != null)
            {
                builder.Append("Sex,");
                builder2.Append("@Sex,");
                this._DataProcess.ProcessParametersAdd("@Sex", SqlDbType.VarChar, 50, mObj.Sex);
            }
            if (mObj.Age != 0)
            {
                builder.Append("Age,");
                builder2.Append("@Age,");
                this._DataProcess.ProcessParametersAdd("@Age", SqlDbType.Int, 4, mObj.Age);
            }
            if (mObj.CompanyName != null)
            {
                builder.Append("CompanyName,");
                builder2.Append("@CompanyName,");
                this._DataProcess.ProcessParametersAdd("@CompanyName", SqlDbType.VarChar, 200, mObj.CompanyName);
            }
            if (mObj.CompanyAddress != null)
            {
                builder.Append("CompanyAddress,");
                builder2.Append("@CompanyAddress,");
                this._DataProcess.ProcessParametersAdd("@CompanyAddress", SqlDbType.VarChar, 500, mObj.CompanyAddress);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept,");
                builder2.Append("@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 200, mObj.Dept);
            }
            if (mObj.Headship != null)
            {
                builder.Append("Headship,");
                builder2.Append("@Headship,");
                this._DataProcess.ProcessParametersAdd("@Headship", SqlDbType.VarChar, 200, mObj.Headship);
            }
            if (mObj.Postalcode != null)
            {
                builder.Append("Postalcode,");
                builder2.Append("@Postalcode,");
                this._DataProcess.ProcessParametersAdd("@Postalcode", SqlDbType.VarChar, 50, mObj.Postalcode);
            }
            if (mObj.Phone != null)
            {
                builder.Append("Phone,");
                builder2.Append("@Phone,");
                this._DataProcess.ProcessParametersAdd("@Phone", SqlDbType.VarChar, 50, mObj.Phone);
            }
            if (mObj.Fax != null)
            {
                builder.Append("Fax,");
                builder2.Append("@Fax,");
                this._DataProcess.ProcessParametersAdd("@Fax", SqlDbType.VarChar, 50, mObj.Fax);
            }
            if (mObj.Mobile != null)
            {
                builder.Append("Mobile,");
                builder2.Append("@Mobile,");
                this._DataProcess.ProcessParametersAdd("@Mobile", SqlDbType.VarChar, 50, mObj.Mobile);
            }
            if (mObj.Email != null)
            {
                builder.Append("Email,");
                builder2.Append("@Email,");
                this._DataProcess.ProcessParametersAdd("@Email", SqlDbType.VarChar, 200, mObj.Email);
            }
            if (mObj.NetAddress != null)
            {
                builder.Append("NetAddress,");
                builder2.Append("@NetAddress,");
                this._DataProcess.ProcessParametersAdd("@NetAddress", SqlDbType.VarChar, 200, mObj.NetAddress);
            }
            if (mObj.Hobby != null)
            {
                builder.Append("Hobby,");
                builder2.Append("@Hobby,");
                this._DataProcess.ProcessParametersAdd("@Hobby", SqlDbType.VarChar, 400, mObj.Hobby);
            }
            if (mObj.HomeAddress != null)
            {
                builder.Append("HomeAddress,");
                builder2.Append("@HomeAddress,");
                this._DataProcess.ProcessParametersAdd("@HomeAddress", SqlDbType.VarChar, 200, mObj.HomeAddress);
            }
            if (mObj.Wedlock != null)
            {
                builder.Append("Wedlock,");
                builder2.Append("@Wedlock,");
                this._DataProcess.ProcessParametersAdd("@Wedlock", SqlDbType.VarChar, 50, mObj.Wedlock);
            }
            if (mObj.CardType != null)
            {
                builder.Append("CardType,");
                builder2.Append("@CardType,");
                this._DataProcess.ProcessParametersAdd("@CardType", SqlDbType.VarChar, 50, mObj.CardType);
            }
            if (mObj.NativePlace != null)
            {
                builder.Append("NativePlace,");
                builder2.Append("@NativePlace,");
                this._DataProcess.ProcessParametersAdd("@NativePlace", SqlDbType.VarChar, 50, mObj.NativePlace);
            }
            if (mObj.Birthday != DateTime.MinValue)
            {
                builder.Append("Birthday,");
                builder2.Append("@Birthday,");
                this._DataProcess.ProcessParametersAdd("@Birthday", SqlDbType.DateTime, 8, mObj.Birthday);
            }
            if (mObj.ContactTime != DateTime.MinValue)
            {
                builder.Append("ContactTime,");
                builder2.Append("@ContactTime,");
                this._DataProcess.ProcessParametersAdd("@ContactTime", SqlDbType.DateTime, 8, mObj.ContactTime);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark,");
                builder2.Append("@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            }
            if (mObj.HomePhone != null)
            {
                builder.Append("HomePhone,");
                builder2.Append("@HomePhone,");
                this._DataProcess.ProcessParametersAdd("@HomePhone", SqlDbType.VarChar, 50, mObj.HomePhone);
            }
            if (mObj.PublicStatus != null)
            {
                builder.Append("PublicStatus,");
                builder2.Append("@PublicStatus,");
                this._DataProcess.ProcessParametersAdd("@PublicStatus", SqlDbType.VarChar, 10, mObj.PublicStatus);
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            builder.Append(") ");
            builder2.Append(") ");
            builder.Append(builder2.ToString());
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_CardsFolderModel> _Select(GK_OA_CardsFolderQueryModel qmObj)
        {
            List<GK_OA_CardsFolderModel> list = new List<GK_OA_CardsFolderModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_CardsFolder ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY Code desc");
            }
            else
            {
                builder.Append(" ORDER BY " + qmObj.SortColumns);
            }
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.SqlParameters = qmObj.Parameters;
            SqlDataReader sqlDataReader = null;
            int num = 0;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        if ((num >= qmObj.StartRecord) && ((list.Count < qmObj.MaxRecords) || (qmObj.MaxRecords == -1)))
                        {
                            GK_OA_CardsFolderModel model = new GK_OA_CardsFolderModel();
                            this.Initialize(sqlDataReader, model);
                            list.Add(model);
                        }
                        num++;
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return list;
        }

        private int _Update(GK_OA_CardsFolderModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_CardsFolder set ");
            if (mObj.UserId != null)
            {
                builder.Append("UserId=@UserId,");
                this._DataProcess.ProcessParametersAdd("@UserId", SqlDbType.VarChar, 50, mObj.UserId);
            }
            if (mObj.UserName != null)
            {
                builder.Append("UserName=@UserName,");
                this._DataProcess.ProcessParametersAdd("@UserName", SqlDbType.VarChar, 50, mObj.UserName);
            }
            if (mObj.Sex != null)
            {
                builder.Append("Sex=@Sex,");
                this._DataProcess.ProcessParametersAdd("@Sex", SqlDbType.VarChar, 50, mObj.Sex);
            }
            if (mObj.Age != 0)
            {
                builder.Append("Age=@Age,");
                this._DataProcess.ProcessParametersAdd("@Age", SqlDbType.Int, 4, mObj.Age);
            }
            if (mObj.CompanyName != null)
            {
                builder.Append("CompanyName=@CompanyName,");
                this._DataProcess.ProcessParametersAdd("@CompanyName", SqlDbType.VarChar, 200, mObj.CompanyName);
            }
            if (mObj.CompanyAddress != null)
            {
                builder.Append("CompanyAddress=@CompanyAddress,");
                this._DataProcess.ProcessParametersAdd("@CompanyAddress", SqlDbType.VarChar, 500, mObj.CompanyAddress);
            }
            if (mObj.Dept != null)
            {
                builder.Append("Dept=@Dept,");
                this._DataProcess.ProcessParametersAdd("@Dept", SqlDbType.VarChar, 200, mObj.Dept);
            }
            if (mObj.Headship != null)
            {
                builder.Append("Headship=@Headship,");
                this._DataProcess.ProcessParametersAdd("@Headship", SqlDbType.VarChar, 200, mObj.Headship);
            }
            if (mObj.Postalcode != null)
            {
                builder.Append("Postalcode=@Postalcode,");
                this._DataProcess.ProcessParametersAdd("@Postalcode", SqlDbType.VarChar, 50, mObj.Postalcode);
            }
            if (mObj.Phone != null)
            {
                builder.Append("Phone=@Phone,");
                this._DataProcess.ProcessParametersAdd("@Phone", SqlDbType.VarChar, 50, mObj.Phone);
            }
            if (mObj.Fax != null)
            {
                builder.Append("Fax=@Fax,");
                this._DataProcess.ProcessParametersAdd("@Fax", SqlDbType.VarChar, 50, mObj.Fax);
            }
            if (mObj.Mobile != null)
            {
                builder.Append("Mobile=@Mobile,");
                this._DataProcess.ProcessParametersAdd("@Mobile", SqlDbType.VarChar, 50, mObj.Mobile);
            }
            if (mObj.Email != null)
            {
                builder.Append("Email=@Email,");
                this._DataProcess.ProcessParametersAdd("@Email", SqlDbType.VarChar, 200, mObj.Email);
            }
            if (mObj.NetAddress != null)
            {
                builder.Append("NetAddress=@NetAddress,");
                this._DataProcess.ProcessParametersAdd("@NetAddress", SqlDbType.VarChar, 200, mObj.NetAddress);
            }
            if (mObj.Hobby != null)
            {
                builder.Append("Hobby=@Hobby,");
                this._DataProcess.ProcessParametersAdd("@Hobby", SqlDbType.VarChar, 400, mObj.Hobby);
            }
            if (mObj.HomeAddress != null)
            {
                builder.Append("HomeAddress=@HomeAddress,");
                this._DataProcess.ProcessParametersAdd("@HomeAddress", SqlDbType.VarChar, 200, mObj.HomeAddress);
            }
            if (mObj.Wedlock != null)
            {
                builder.Append("Wedlock=@Wedlock,");
                this._DataProcess.ProcessParametersAdd("@Wedlock", SqlDbType.VarChar, 50, mObj.Wedlock);
            }
            if (mObj.CardType != null)
            {
                builder.Append("CardType=@CardType,");
                this._DataProcess.ProcessParametersAdd("@CardType", SqlDbType.VarChar, 50, mObj.CardType);
            }
            if (mObj.NativePlace != null)
            {
                builder.Append("NativePlace=@NativePlace,");
                this._DataProcess.ProcessParametersAdd("@NativePlace", SqlDbType.VarChar, 50, mObj.NativePlace);
            }
            if (mObj.Birthday != DateTime.MinValue)
            {
                builder.Append("Birthday=@Birthday,");
                this._DataProcess.ProcessParametersAdd("@Birthday", SqlDbType.DateTime, 8, mObj.Birthday);
            }
            if (mObj.ContactTime != DateTime.MinValue)
            {
                builder.Append("ContactTime=@ContactTime,");
                this._DataProcess.ProcessParametersAdd("@ContactTime", SqlDbType.DateTime, 8, mObj.ContactTime);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            }
            if (mObj.HomePhone != null)
            {
                builder.Append("HomePhone=@HomePhone,");
                this._DataProcess.ProcessParametersAdd("@HomePhone", SqlDbType.VarChar, 50, mObj.HomePhone);
            }
            if (mObj.PublicStatus != null)
            {
                builder.Append("PublicStatus=@PublicStatus,");
                this._DataProcess.ProcessParametersAdd("@PublicStatus", SqlDbType.VarChar, 10, mObj.PublicStatus);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where Code=@Code");
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, mObj.Code);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(GK_OA_CardsFolderModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_CardsFolderModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_CardsFolderModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.UserId = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.UserName = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Sex = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.Age = reader.GetInt32(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.CompanyName = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.CompanyAddress = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Dept = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Headship = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Postalcode = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Phone = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Fax = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.Mobile = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.Email = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.NetAddress = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.Hobby = reader.GetString(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.HomeAddress = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.Wedlock = reader.GetString(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.CardType = reader.GetString(0x12);
            }
            if (reader.GetValue(0x13) != DBNull.Value)
            {
                obj.NativePlace = reader.GetString(0x13);
            }
            if (reader.GetValue(20) != DBNull.Value)
            {
                obj.Birthday = reader.GetDateTime(20);
            }
            if (reader.GetValue(0x15) != DBNull.Value)
            {
                obj.ContactTime = reader.GetDateTime(0x15);
            }
            if (reader.GetValue(0x16) != DBNull.Value)
            {
                obj.Remark = reader.GetString(0x16);
            }
            if (reader.GetValue(0x17) != DBNull.Value)
            {
                obj.HomePhone = reader.GetString(0x17);
            }
            if (reader.GetValue(0x18) != DBNull.Value)
            {
                obj.PublicStatus = reader.GetString(0x18);
            }
        }

        public int Insert(GK_OA_CardsFolderModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_CardsFolderModel> Select()
        {
            GK_OA_CardsFolderQueryModel qmObj = new GK_OA_CardsFolderQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_CardsFolderModel> Select(GK_OA_CardsFolderQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_CardsFolderModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

