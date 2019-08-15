namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class OAPersonDAL
    {
        private SqlDataProcess _DataProcess;

        public OAPersonDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public OAPersonDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private OAPersonModel _DataBind(int Code)
        {
            OAPersonModel model = new OAPersonModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPerson ");
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
            builder.Append("delete from GK_OAPerson ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(OAPersonModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OAPerson(");
            builder.Append("workNo,cname,folk,IDcard,polity,rdt_date,nativeplace,homeplace,birthday,stature,avoirdupois,bloodgroup,education,degree,zc,yard,duty,address,cjgz_date,rgs_date,fkdz,phone,mobile,postcode,email,ismarry,htc_date,jobno,sex,photosize,phototype,leader,Status,HealthStatus,Specific,Seniority,WorkKills,DepartmentOpinion,UnitOpinion)");
            builder.Append(" values (");
            builder.Append("@workNo,@cname,@folk,@IDcard,@polity,@rdt_date,@nativeplace,@homeplace,@birthday,@stature,@avoirdupois,@bloodgroup,@education,@degree,@zc,@yard,@duty,@address,@cjgz_date,@rgs_date,@fkdz,@phone,@mobile,@postcode,@email,@ismarry,@htc_date,@jobno,@sex,@photosize,@phototype,@leader,@Status,@HealthStatus,@Specific,@Seniority,@WorkKills,@DepartmentOpinion,@UnitOpinion) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@workNo", SqlDbType.VarChar, 50, mObj.workNo);
            this._DataProcess.ProcessParametersAdd("@cname", SqlDbType.VarChar, 50, mObj.cname);
            this._DataProcess.ProcessParametersAdd("@folk", SqlDbType.VarChar, 50, mObj.folk);
            this._DataProcess.ProcessParametersAdd("@IDcard", SqlDbType.VarChar, 50, mObj.IDcard);
            this._DataProcess.ProcessParametersAdd("@polity", SqlDbType.VarChar, 50, mObj.polity);
            this._DataProcess.ProcessParametersAdd("@rdt_date", SqlDbType.DateTime, 8, mObj.rdt_date);
            this._DataProcess.ProcessParametersAdd("@nativeplace", SqlDbType.VarChar, 50, mObj.nativeplace);
            this._DataProcess.ProcessParametersAdd("@homeplace", SqlDbType.VarChar, 50, mObj.homeplace);
            this._DataProcess.ProcessParametersAdd("@birthday", SqlDbType.DateTime, 8, mObj.birthday);
            this._DataProcess.ProcessParametersAdd("@stature", SqlDbType.VarChar, 50, mObj.stature);
            this._DataProcess.ProcessParametersAdd("@avoirdupois", SqlDbType.VarChar, 50, "");
            this._DataProcess.ProcessParametersAdd("@bloodgroup", SqlDbType.VarChar, 50, mObj.bloodgroup);
            this._DataProcess.ProcessParametersAdd("@education", SqlDbType.VarChar, 50, mObj.education);
            this._DataProcess.ProcessParametersAdd("@degree", SqlDbType.VarChar, 50, mObj.degree);
            this._DataProcess.ProcessParametersAdd("@zc", SqlDbType.VarChar, 50, mObj.zc);
            this._DataProcess.ProcessParametersAdd("@yard", SqlDbType.VarChar, 50, mObj.yard);
            this._DataProcess.ProcessParametersAdd("@duty", SqlDbType.VarChar, 50, mObj.duty);
            this._DataProcess.ProcessParametersAdd("@address", SqlDbType.VarChar, 100, mObj.address);
            this._DataProcess.ProcessParametersAdd("@cjgz_date", SqlDbType.DateTime, 8, mObj.cjgz_date);
            this._DataProcess.ProcessParametersAdd("@rgs_date", SqlDbType.DateTime, 8, mObj.rgs_date);
            this._DataProcess.ProcessParametersAdd("@fkdz", SqlDbType.VarChar, 100, mObj.fkdz);
            this._DataProcess.ProcessParametersAdd("@phone", SqlDbType.VarChar, 50, mObj.phone);
            this._DataProcess.ProcessParametersAdd("@mobile", SqlDbType.VarChar, 50, mObj.mobile);
            this._DataProcess.ProcessParametersAdd("@postcode", SqlDbType.VarChar, 50, mObj.postcode);
            this._DataProcess.ProcessParametersAdd("@email", SqlDbType.VarChar, 50, mObj.email);
            this._DataProcess.ProcessParametersAdd("@ismarry", SqlDbType.VarChar, 50, mObj.ismarry);
            this._DataProcess.ProcessParametersAdd("@htc_date", SqlDbType.DateTime, 8, mObj.htc_date);
            this._DataProcess.ProcessParametersAdd("@jobno", SqlDbType.VarChar, 50, "");
            this._DataProcess.ProcessParametersAdd("@sex", SqlDbType.VarChar, 50, mObj.sex);
            this._DataProcess.ProcessParametersAdd("@photosize", SqlDbType.Int, 4, mObj.photosize);
            this._DataProcess.ProcessParametersAdd("@phototype", SqlDbType.VarChar, 50, mObj.phototype);
            this._DataProcess.ProcessParametersAdd("@leader", SqlDbType.VarChar, 50, mObj.Leader);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@HealthStatus", SqlDbType.VarChar, 50, mObj.HealthStatus);
            this._DataProcess.ProcessParametersAdd("@Specific", SqlDbType.VarChar, 50, mObj.Specific);
            this._DataProcess.ProcessParametersAdd("@Seniority", SqlDbType.VarChar, 50, mObj.Seniority);
            this._DataProcess.ProcessParametersAdd("@WorkKills", SqlDbType.VarChar, 0x1f40, mObj.WorkKills);
            this._DataProcess.ProcessParametersAdd("@DepartmentOpinion", SqlDbType.VarChar, 0x1f40, mObj.DepartmentOpinion);
            this._DataProcess.ProcessParametersAdd("@UnitOpinion", SqlDbType.VarChar, 0x1f40, mObj.UnitOpinion);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<OAPersonModel> _Select(OAPersonQueryModel qmObj)
        {
            List<OAPersonModel> list = new List<OAPersonModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OAPerson ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY workNo asc");
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
                            OAPersonModel model = new OAPersonModel();
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

        private int _Update(OAPersonModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OAPerson set ");
            if (mObj.workNo != null)
            {
                builder.Append("workNo=@workNo,");
                this._DataProcess.ProcessParametersAdd("@workNo", SqlDbType.VarChar, 50, mObj.workNo);
            }
            if (mObj.cname != null)
            {
                builder.Append("cname=@cname,");
                this._DataProcess.ProcessParametersAdd("@cname", SqlDbType.VarChar, 50, mObj.cname);
            }
            if (mObj.folk != null)
            {
                builder.Append("folk=@folk,");
                this._DataProcess.ProcessParametersAdd("@folk", SqlDbType.VarChar, 50, mObj.folk);
            }
            if (mObj.IDcard != null)
            {
                builder.Append("IDcard=@IDcard,");
                this._DataProcess.ProcessParametersAdd("@IDcard", SqlDbType.VarChar, 50, mObj.IDcard);
            }
            if (mObj.polity != null)
            {
                builder.Append("polity=@polity,");
                this._DataProcess.ProcessParametersAdd("@polity", SqlDbType.VarChar, 50, mObj.polity);
            }
            if (mObj.rdt_date != DateTime.MinValue)
            {
                builder.Append("rdt_date=@rdt_date,");
                this._DataProcess.ProcessParametersAdd("@rdt_date", SqlDbType.DateTime, 8, mObj.rdt_date);
            }
            if (mObj.nativeplace != null)
            {
                builder.Append("nativeplace=@nativeplace,");
                this._DataProcess.ProcessParametersAdd("@nativeplace", SqlDbType.VarChar, 50, mObj.nativeplace);
            }
            if (mObj.homeplace != null)
            {
                builder.Append("homeplace=@homeplace,");
                this._DataProcess.ProcessParametersAdd("@homeplace", SqlDbType.VarChar, 50, mObj.homeplace);
            }
            if (mObj.birthday != DateTime.MinValue)
            {
                builder.Append("birthday=@birthday,");
                this._DataProcess.ProcessParametersAdd("@birthday", SqlDbType.DateTime, 8, mObj.birthday);
            }
            if (mObj.stature != null)
            {
                builder.Append("stature=@stature,");
                this._DataProcess.ProcessParametersAdd("@stature", SqlDbType.VarChar, 50, mObj.stature);
            }
            if (mObj.avoirdupois != null)
            {
                builder.Append("avoirdupois=@avoirdupois,");
                this._DataProcess.ProcessParametersAdd("@avoirdupois", SqlDbType.VarChar, 50, mObj.avoirdupois);
            }
            if (mObj.bloodgroup != null)
            {
                builder.Append("bloodgroup=@bloodgroup,");
                this._DataProcess.ProcessParametersAdd("@bloodgroup", SqlDbType.VarChar, 50, mObj.bloodgroup);
            }
            if (mObj.education != null)
            {
                builder.Append("education=@education,");
                this._DataProcess.ProcessParametersAdd("@education", SqlDbType.VarChar, 50, mObj.education);
            }
            if (mObj.degree != null)
            {
                builder.Append("degree=@degree,");
                this._DataProcess.ProcessParametersAdd("@degree", SqlDbType.VarChar, 50, mObj.degree);
            }
            if (mObj.zc != null)
            {
                builder.Append("zc=@zc,");
                this._DataProcess.ProcessParametersAdd("@zc", SqlDbType.VarChar, 50, mObj.zc);
            }
            if (mObj.yard != null)
            {
                builder.Append("yard=@yard,");
                this._DataProcess.ProcessParametersAdd("@yard", SqlDbType.VarChar, 50, mObj.yard);
            }
            if (mObj.duty != null)
            {
                builder.Append("duty=@duty,");
                this._DataProcess.ProcessParametersAdd("@duty", SqlDbType.VarChar, 50, mObj.duty);
            }
            if (mObj.address != null)
            {
                builder.Append("address=@address,");
                this._DataProcess.ProcessParametersAdd("@address", SqlDbType.VarChar, 100, mObj.address);
            }
            if (mObj.cjgz_date != DateTime.MinValue)
            {
                builder.Append("cjgz_date=@cjgz_date,");
                this._DataProcess.ProcessParametersAdd("@cjgz_date", SqlDbType.DateTime, 8, mObj.cjgz_date);
            }
            if (mObj.rgs_date != DateTime.MinValue)
            {
                builder.Append("rgs_date=@rgs_date,");
                this._DataProcess.ProcessParametersAdd("@rgs_date", SqlDbType.DateTime, 8, mObj.rgs_date);
            }
            if (mObj.fkdz != null)
            {
                builder.Append("fkdz=@fkdz,");
                this._DataProcess.ProcessParametersAdd("@fkdz", SqlDbType.VarChar, 100, mObj.fkdz);
            }
            if (mObj.phone != null)
            {
                builder.Append("phone=@phone,");
                this._DataProcess.ProcessParametersAdd("@phone", SqlDbType.VarChar, 50, mObj.phone);
            }
            if (mObj.mobile != null)
            {
                builder.Append("mobile=@mobile,");
                this._DataProcess.ProcessParametersAdd("@mobile", SqlDbType.VarChar, 50, mObj.mobile);
            }
            if (mObj.postcode != null)
            {
                builder.Append("postcode=@postcode,");
                this._DataProcess.ProcessParametersAdd("@postcode", SqlDbType.VarChar, 50, mObj.postcode);
            }
            if (mObj.email != null)
            {
                builder.Append("email=@email,");
                this._DataProcess.ProcessParametersAdd("@email", SqlDbType.VarChar, 50, mObj.email);
            }
            if (mObj.ismarry != null)
            {
                builder.Append("ismarry=@ismarry,");
                this._DataProcess.ProcessParametersAdd("@ismarry", SqlDbType.VarChar, 50, mObj.ismarry);
            }
            if (mObj.htc_date != DateTime.MinValue)
            {
                builder.Append("htc_date=@htc_date,");
                this._DataProcess.ProcessParametersAdd("@htc_date", SqlDbType.DateTime, 8, mObj.htc_date);
            }
            if (mObj.jobno != null)
            {
                builder.Append("jobno=@jobno,");
                this._DataProcess.ProcessParametersAdd("@jobno", SqlDbType.VarChar, 50, mObj.jobno);
            }
            if (mObj.sex != null)
            {
                builder.Append("sex=@sex,");
                this._DataProcess.ProcessParametersAdd("@sex", SqlDbType.VarChar, 50, mObj.sex);
            }
            if (mObj.photoimages != 0)
            {
                builder.Append("photoimages=@photoimages,");
                this._DataProcess.ProcessParametersAdd("@photoimages", SqlDbType.Image, 0x10, mObj.photoimages);
            }
            if (mObj.photosize != 0)
            {
                builder.Append("photosize=@photosize,");
                this._DataProcess.ProcessParametersAdd("@photosize", SqlDbType.Int, 4, mObj.photosize);
            }
            if (mObj.phototype != null)
            {
                builder.Append("phototype=@phototype,");
                this._DataProcess.ProcessParametersAdd("@phototype", SqlDbType.VarChar, 50, mObj.phototype);
            }
            if (mObj.Leader != null)
            {
                builder.Append("Leader=@leader,");
                this._DataProcess.ProcessParametersAdd("@leader", SqlDbType.VarChar, 50, mObj.Leader);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.HealthStatus != null)
            {
                builder.Append("HealthStatus=@HealthStatus,");
                this._DataProcess.ProcessParametersAdd("@HealthStatus", SqlDbType.VarChar, 50, mObj.HealthStatus);
            }
            if (mObj.Specific != null)
            {
                builder.Append("Specific=@Specific,");
                this._DataProcess.ProcessParametersAdd("@Specific", SqlDbType.VarChar, 50, mObj.Specific);
            }
            if (mObj.Seniority != null)
            {
                builder.Append("Seniority=@Seniority,");
                this._DataProcess.ProcessParametersAdd("@Seniority", SqlDbType.VarChar, 50, mObj.Seniority);
            }
            if (mObj.WorkKills != null)
            {
                builder.Append("WorkKills=@WorkKills,");
                this._DataProcess.ProcessParametersAdd("@WorkKills", SqlDbType.VarChar, 0x1f40, mObj.WorkKills);
            }
            if (mObj.DepartmentOpinion != null)
            {
                builder.Append("DepartmentOpinion=@DepartmentOpinion,");
                this._DataProcess.ProcessParametersAdd("@DepartmentOpinion", SqlDbType.VarChar, 0x1f40, mObj.DepartmentOpinion);
            }
            if (mObj.UnitOpinion != null)
            {
                builder.Append("UnitOpinion=@UnitOpinion,");
                this._DataProcess.ProcessParametersAdd("@UnitOpinion", SqlDbType.VarChar, 0x1f40, mObj.UnitOpinion);
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

        public int Delete(OAPersonModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public OAPersonModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, OAPersonModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.workNo = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.cname = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.folk = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.IDcard = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.polity = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.rdt_date = reader.GetDateTime(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.nativeplace = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.homeplace = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.birthday = reader.GetDateTime(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.stature = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.avoirdupois = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.bloodgroup = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.education = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.degree = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.zc = reader.GetString(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.yard = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.duty = reader.GetString(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.address = reader.GetString(0x12);
            }
            if (reader.GetValue(0x13) != DBNull.Value)
            {
                obj.cjgz_date = reader.GetDateTime(0x13);
            }
            if (reader.GetValue(20) != DBNull.Value)
            {
                obj.rgs_date = reader.GetDateTime(20);
            }
            if (reader.GetValue(0x15) != DBNull.Value)
            {
                obj.fkdz = reader.GetString(0x15);
            }
            if (reader.GetValue(0x16) != DBNull.Value)
            {
                obj.phone = reader.GetString(0x16);
            }
            if (reader.GetValue(0x17) != DBNull.Value)
            {
                obj.mobile = reader.GetString(0x17);
            }
            if (reader.GetValue(0x18) != DBNull.Value)
            {
                obj.postcode = reader.GetString(0x18);
            }
            if (reader.GetValue(0x19) != DBNull.Value)
            {
                obj.email = reader.GetString(0x19);
            }
            if (reader.GetValue(0x1a) != DBNull.Value)
            {
                obj.ismarry = reader.GetString(0x1a);
            }
            if (reader.GetValue(0x1b) != DBNull.Value)
            {
                obj.htc_date = reader.GetDateTime(0x1b);
            }
            if (reader.GetValue(0x1c) != DBNull.Value)
            {
                obj.jobno = reader.GetString(0x1c);
            }
            if (reader.GetValue(0x1d) != DBNull.Value)
            {
                obj.sex = reader.GetString(0x1d);
            }
            if (reader.GetValue(30) != DBNull.Value)
            {
                obj.photoimages = reader.GetByte(30);
            }
            if (reader.GetValue(0x1f) != DBNull.Value)
            {
                obj.photosize = reader.GetInt32(0x1f);
            }
            if (reader.GetValue(0x20) != DBNull.Value)
            {
                obj.phototype = reader.GetString(0x20);
            }
            if (reader.GetValue(0x21) != DBNull.Value)
            {
                obj.Leader = reader.GetString(0x21);
            }
            if (reader.GetValue(0x22) != DBNull.Value)
            {
                obj.Status = reader.GetString(0x22);
            }
            if (reader.GetValue(0x23) != DBNull.Value)
            {
                obj.HealthStatus = reader.GetString(0x23);
            }
            if (reader.GetValue(0x24) != DBNull.Value)
            {
                obj.Specific = reader.GetString(0x24);
            }
            if (reader.GetValue(0x25) != DBNull.Value)
            {
                obj.Seniority = reader.GetString(0x25);
            }
            if (reader.GetValue(0x26) != DBNull.Value)
            {
                obj.WorkKills = reader.GetString(0x26);
            }
            if (reader.GetValue(0x27) != DBNull.Value)
            {
                obj.DepartmentOpinion = reader.GetString(0x27);
            }
            if (reader.GetValue(40) != DBNull.Value)
            {
                obj.UnitOpinion = reader.GetString(40);
            }
        }

        public int Insert(OAPersonModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<OAPersonModel> Select()
        {
            OAPersonQueryModel qmObj = new OAPersonQueryModel();
            return this._Select(qmObj);
        }

        public List<OAPersonModel> Select(OAPersonQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(OAPersonModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

