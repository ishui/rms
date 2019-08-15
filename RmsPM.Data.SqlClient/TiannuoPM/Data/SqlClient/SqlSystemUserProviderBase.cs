namespace TiannuoPM.Data.SqlClient
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Data.Bases;
    using TiannuoPM.Entities;

    public class SqlSystemUserProviderBase : SystemUserProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlSystemUserProviderBase()
        {
        }

        public SqlSystemUserProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<SystemUser> entities)
        {
            SqlBulkCopy copy = null;
            if ((transactionManager != null) && transactionManager.IsOpen)
            {
                SqlConnection connection = transactionManager.TransactionObject.Connection as SqlConnection;
                SqlTransaction externalTransaction = transactionManager.TransactionObject as SqlTransaction;
                copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, externalTransaction);
            }
            else
            {
                copy = new SqlBulkCopy(this._connectionString, SqlBulkCopyOptions.CheckConstraints);
            }
            copy.BulkCopyTimeout = 360;
            copy.DestinationTableName = "SystemUser";
            DataTable table = new DataTable();
            table.Columns.Add("UserCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("UserID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("UserName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("OwnName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PassWord", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Sex", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Phone", typeof(string)).AllowDBNull = true;
            table.Columns.Add("MailBox", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Note", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BirthDay", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("PhoneHome", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Address", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Mobile", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Fax", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(int)).AllowDBNull = true;
            table.Columns.Add("LastProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SortID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ShortUserName", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("UserCode", "UserCode");
            copy.ColumnMappings.Add("UserID", "UserID");
            copy.ColumnMappings.Add("UserName", "UserName");
            copy.ColumnMappings.Add("OwnName", "OwnName");
            copy.ColumnMappings.Add("PassWord", "PassWord");
            copy.ColumnMappings.Add("Sex", "Sex");
            copy.ColumnMappings.Add("Phone", "Phone");
            copy.ColumnMappings.Add("MailBox", "MailBox");
            copy.ColumnMappings.Add("Note", "Note");
            copy.ColumnMappings.Add("BirthDay", "BirthDay");
            copy.ColumnMappings.Add("PhoneHome", "PhoneHome");
            copy.ColumnMappings.Add("Address", "Address");
            copy.ColumnMappings.Add("Mobile", "Mobile");
            copy.ColumnMappings.Add("Fax", "Fax");
            copy.ColumnMappings.Add("Status", "Status");
            copy.ColumnMappings.Add("LastProjectCode", "LastProjectCode");
            copy.ColumnMappings.Add("SortID", "SortID");
            copy.ColumnMappings.Add("ShortUserName", "ShortUserName");
            foreach (SystemUser user in entities)
            {
                if (user.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["UserCode"] = user.UserCode;
                    row["UserID"] = user.UserID;
                    row["UserName"] = user.UserName;
                    row["OwnName"] = user.OwnName;
                    row["PassWord"] = user.PassWord;
                    row["Sex"] = user.Sex;
                    row["Phone"] = user.Phone;
                    row["MailBox"] = user.MailBox;
                    row["Note"] = user.Note;
                    row["BirthDay"] = user.BirthDay.HasValue ? ((object) user.BirthDay) : ((object) DBNull.Value);
                    row["PhoneHome"] = user.PhoneHome;
                    row["Address"] = user.Address;
                    row["Mobile"] = user.Mobile;
                    row["Fax"] = user.Fax;
                    row["Status"] = user.Status.HasValue ? ((object) user.Status) : ((object) DBNull.Value);
                    row["LastProjectCode"] = user.LastProjectCode;
                    row["SortID"] = user.SortID;
                    row["ShortUserName"] = user.ShortUserName;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (SystemUser user in entities)
            {
                if (user.EntityState == EntityState.Added)
                {
                    user.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string userCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@UserCode", DbType.AnsiString, userCode);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            if (DataRepository.Provider.EnableEntityTracking)
            {
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(SystemUser), new object[] { userCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<SystemUser> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<SystemUser>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@UserCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@UserID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@UserName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@OwnName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PassWord", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Sex", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Phone", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@MailBox", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Note", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BirthDay", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@PhoneHome", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Address", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Mobile", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Fax", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@LastProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SortID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ShortUserName", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("usercode ") || text.Trim().StartsWith("usercode="))
                {
                    database.SetParameterValue(command, "@UserCode", text.Replace("usercode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("userid ") || text.Trim().StartsWith("userid="))
                {
                    database.SetParameterValue(command, "@UserID", text.Replace("userid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("username ") || text.Trim().StartsWith("username="))
                {
                    database.SetParameterValue(command, "@UserName", text.Replace("username", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ownname ") || text.Trim().StartsWith("ownname="))
                {
                    database.SetParameterValue(command, "@OwnName", text.Replace("ownname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("password ") || text.Trim().StartsWith("password="))
                {
                    database.SetParameterValue(command, "@PassWord", text.Replace("password", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("sex ") || text.Trim().StartsWith("sex="))
                {
                    database.SetParameterValue(command, "@Sex", text.Replace("sex", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("phone ") || text.Trim().StartsWith("phone="))
                {
                    database.SetParameterValue(command, "@Phone", text.Replace("phone", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("mailbox ") || text.Trim().StartsWith("mailbox="))
                {
                    database.SetParameterValue(command, "@MailBox", text.Replace("mailbox", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("note ") || text.Trim().StartsWith("note="))
                {
                    database.SetParameterValue(command, "@Note", text.Replace("note", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("birthday ") || text.Trim().StartsWith("birthday="))
                {
                    database.SetParameterValue(command, "@BirthDay", text.Replace("birthday", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("phonehome ") || text.Trim().StartsWith("phonehome="))
                {
                    database.SetParameterValue(command, "@PhoneHome", text.Replace("phonehome", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("address ") || text.Trim().StartsWith("address="))
                {
                    database.SetParameterValue(command, "@Address", text.Replace("address", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("mobile ") || text.Trim().StartsWith("mobile="))
                {
                    database.SetParameterValue(command, "@Mobile", text.Replace("mobile", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("fax ") || text.Trim().StartsWith("fax="))
                {
                    database.SetParameterValue(command, "@Fax", text.Replace("fax", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("status ") || text.Trim().StartsWith("status="))
                {
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("lastprojectcode ") || text.Trim().StartsWith("lastprojectcode="))
                {
                    database.SetParameterValue(command, "@LastProjectCode", text.Replace("lastprojectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("sortid ") || text.Trim().StartsWith("sortid="))
                {
                    database.SetParameterValue(command, "@SortID", text.Replace("sortid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("shortusername ") && !text.Trim().StartsWith("shortusername="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@ShortUserName", text.Replace("shortusername", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<SystemUser> rows = new TList<SystemUser>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                SystemUserProviderBaseCore.Fill(reader, rows, start, pageLength);
                if (reader.NextResult() && reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<SystemUser> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_Find_Dynamic", typeof(SystemUserColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<SystemUser> rows = new TList<SystemUser>();
            IDataReader reader = null;
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                SystemUserProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
                count = rows.Count;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<SystemUser> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<SystemUser> rows = new TList<SystemUser>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, dbCommand);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, dbCommand);
                }
                SystemUserProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override SystemUser GetByUserCode(TransactionManager transactionManager, string userCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_GetByUserCode", this._useStoredProcedure);
            database.AddInParameter(command, "@UserCode", DbType.AnsiString, userCode);
            IDataReader reader = null;
            TList<SystemUser> rows = new TList<SystemUser>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                SystemUserProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (reader.NextResult() && reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if (rows.Count == 1)
            {
                return rows[0];
            }
            if (rows.Count != 0)
            {
                throw new DataException("Cannot find the unique instance of the class.");
            }
            return null;
        }

        public override SystemUser GetByUserID(TransactionManager transactionManager, string userID, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_GetByUserID", this._useStoredProcedure);
            database.AddInParameter(command, "@UserID", DbType.AnsiString, userID);
            IDataReader reader = null;
            TList<SystemUser> rows = new TList<SystemUser>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                SystemUserProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (reader.NextResult() && reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if (rows.Count == 1)
            {
                return rows[0];
            }
            if (rows.Count != 0)
            {
                throw new DataException("Cannot find the unique instance of the class.");
            }
            return null;
        }

        public override TList<SystemUser> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<SystemUser> rows = new TList<SystemUser>();
            try
            {
                try
                {
                    if (transactionManager != null)
                    {
                        reader = Utility.ExecuteReader(transactionManager, command);
                    }
                    else
                    {
                        reader = Utility.ExecuteReader(database, command);
                    }
                    SystemUserProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
                    count = rows.Count;
                    if (reader.NextResult() && reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                    return rows;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override bool Insert(TransactionManager transactionManager, SystemUser entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@UserCode", DbType.AnsiString, entity.UserCode);
            database.AddInParameter(command, "@UserID", DbType.AnsiString, entity.UserID);
            database.AddInParameter(command, "@UserName", DbType.AnsiString, entity.UserName);
            database.AddInParameter(command, "@OwnName", DbType.AnsiString, entity.OwnName);
            database.AddInParameter(command, "@PassWord", DbType.AnsiString, entity.PassWord);
            database.AddInParameter(command, "@Sex", DbType.AnsiString, entity.Sex);
            database.AddInParameter(command, "@Phone", DbType.AnsiString, entity.Phone);
            database.AddInParameter(command, "@MailBox", DbType.AnsiString, entity.MailBox);
            database.AddInParameter(command, "@Note", DbType.AnsiString, entity.Note);
            database.AddInParameter(command, "@BirthDay", DbType.DateTime, entity.BirthDay.HasValue ? ((object) entity.BirthDay) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PhoneHome", DbType.AnsiString, entity.PhoneHome);
            database.AddInParameter(command, "@Address", DbType.AnsiString, entity.Address);
            database.AddInParameter(command, "@Mobile", DbType.AnsiString, entity.Mobile);
            database.AddInParameter(command, "@Fax", DbType.AnsiString, entity.Fax);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@LastProjectCode", DbType.AnsiString, entity.LastProjectCode);
            database.AddInParameter(command, "@SortID", DbType.AnsiString, entity.SortID);
            database.AddInParameter(command, "@ShortUserName", DbType.AnsiString, entity.ShortUserName);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalUserCode = entity.UserCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, SystemUser entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.SystemUser_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@UserCode", DbType.AnsiString, entity.UserCode);
            database.AddInParameter(command, "@OriginalUserCode", DbType.AnsiString, entity.OriginalUserCode);
            database.AddInParameter(command, "@UserID", DbType.AnsiString, entity.UserID);
            database.AddInParameter(command, "@UserName", DbType.AnsiString, entity.UserName);
            database.AddInParameter(command, "@OwnName", DbType.AnsiString, entity.OwnName);
            database.AddInParameter(command, "@PassWord", DbType.AnsiString, entity.PassWord);
            database.AddInParameter(command, "@Sex", DbType.AnsiString, entity.Sex);
            database.AddInParameter(command, "@Phone", DbType.AnsiString, entity.Phone);
            database.AddInParameter(command, "@MailBox", DbType.AnsiString, entity.MailBox);
            database.AddInParameter(command, "@Note", DbType.AnsiString, entity.Note);
            database.AddInParameter(command, "@BirthDay", DbType.DateTime, entity.BirthDay.HasValue ? ((object) entity.BirthDay) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PhoneHome", DbType.AnsiString, entity.PhoneHome);
            database.AddInParameter(command, "@Address", DbType.AnsiString, entity.Address);
            database.AddInParameter(command, "@Mobile", DbType.AnsiString, entity.Mobile);
            database.AddInParameter(command, "@Fax", DbType.AnsiString, entity.Fax);
            database.AddInParameter(command, "@Status", DbType.Int32, entity.Status.HasValue ? ((object) entity.Status) : ((object) DBNull.Value));
            database.AddInParameter(command, "@LastProjectCode", DbType.AnsiString, entity.LastProjectCode);
            database.AddInParameter(command, "@SortID", DbType.AnsiString, entity.SortID);
            database.AddInParameter(command, "@ShortUserName", DbType.AnsiString, entity.ShortUserName);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            if (DataRepository.Provider.EnableEntityTracking)
            {
                EntityManager.StopTracking(entity.EntityTrackingKey);
            }
            entity.OriginalUserCode = entity.UserCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        public string ProviderInvariantName
        {
            get
            {
                return this._providerInvariantName;
            }
            set
            {
                this._providerInvariantName = value;
            }
        }

        public bool UseStoredProcedure
        {
            get
            {
                return this._useStoredProcedure;
            }
            set
            {
                this._useStoredProcedure = value;
            }
        }
    }
}

