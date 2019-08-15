namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class OAContact
    {
        private string _Author = null;
        private string _CDate = null;
        private string _content = null;
        private StandardEntityDAO _dao;
        private string _OAContactCode = null;
        private string _phone = null;
        private string _SubmitReport = null;
        private string _takPerson = null;
        private string _takTime = null;
        private string _title = null;
        private string _Type = null;
        private string _Unit = null;
        private string _UserSend = null;

        private void _GetOAContactByCode()
        {
            EntityData oAContactByCode = this.GetOAContactByCode(this._OAContactCode);
            this._OAContactCode = oAContactByCode.GetString("OAContactCode");
            this._title = oAContactByCode.GetString("title");
            this._phone = oAContactByCode.GetString("phone");
            this._Unit = oAContactByCode.GetString("Unit");
            this._CDate = oAContactByCode.GetDateTimeOnlyDate("CDate");
            this._UserSend = oAContactByCode.GetString("UserSend");
            this._SubmitReport = oAContactByCode.GetString("SubmitReport");
            this._takPerson = oAContactByCode.GetString("takPerson");
            this._content = oAContactByCode.GetString("content");
            this._takTime = oAContactByCode.GetDateTimeOnlyDate("takTime");
            this._Author = oAContactByCode.GetString("Author");
            this._Type = oAContactByCode.GetString("Type");
            oAContactByCode.Dispose();
        }

        private EntityData _GetOAContacts()
        {
            EntityData entitydata = new EntityData("OAContact");
            OAContactStrategyBuilder builder = new OAContactStrategyBuilder();
            if (this._OAContactCode != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.OAContactCode, this._OAContactCode));
            }
            if (this._title != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.title, this._title));
            }
            if (this._phone != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.phone, this._phone));
            }
            if (this._Unit != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.Unit, this._Unit));
            }
            if (this._CDate != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.CDate, this._CDate));
            }
            if (this._UserSend != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.UserSend, this._UserSend));
            }
            if (this._SubmitReport != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.SubmitReport, this._SubmitReport));
            }
            if (this._takPerson != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.takPerson, this._takPerson));
            }
            if (this._content != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.content, this._content));
            }
            if (this._takTime != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.takTime, this._takTime));
            }
            if (this._Author != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.Author, this._Author));
            }
            if (this._Type != null)
            {
                builder.AddStrategy(new Strategy(OAContactStrategyName.Type, this._Type));
            }
            string queryString = builder.BuildMainQueryString() + " order by OAContactCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("OAContact", queryString);
            }
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData oAContactByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._OAContactCode == "")
            {
                flag = true;
                oAContactByCode = this.GetOAContactByCode("");
                this._OAContactCode = SystemManageDAO.GetNewSysCode("OAContact");
                newRecord = oAContactByCode.GetNewRecord();
            }
            else
            {
                oAContactByCode = this.GetOAContactByCode(this._OAContactCode);
                newRecord = oAContactByCode.CurrentRow;
            }
            if (this._OAContactCode != null)
            {
                newRecord["OAContactCode"] = this._OAContactCode;
            }
            if (this._title != null)
            {
                newRecord["title"] = this._title;
            }
            if (this._phone != null)
            {
                newRecord["phone"] = this._phone;
            }
            if (this._Unit != null)
            {
                newRecord["Unit"] = this._Unit;
            }
            if (this._CDate != null)
            {
                newRecord["CDate"] = this._CDate;
            }
            if (this._UserSend != null)
            {
                newRecord["UserSend"] = this._UserSend;
            }
            if (this._SubmitReport != null)
            {
                newRecord["SubmitReport"] = this._SubmitReport;
            }
            if (this._takPerson != null)
            {
                newRecord["takPerson"] = this._takPerson;
            }
            if (this._content != null)
            {
                newRecord["content"] = this._content;
            }
            if (this._takTime != null)
            {
                newRecord["takTime"] = this._takTime;
            }
            if (this._Author != null)
            {
                newRecord["Author"] = this._Author;
            }
            if (this._Type != null)
            {
                newRecord["Type"] = this._Type;
            }
            if (flag)
            {
                oAContactByCode.AddNewRecord(newRecord);
            }
            return oAContactByCode;
        }

        private void DeleteOAContact(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAContact"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllOAContact()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAContact"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private EntityData GetOAContactByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAContact"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private EntityData GetOAContactByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "OAContact";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetOAContacts()
        {
            return this._GetOAContacts().CurrentTable;
        }

        private void InsertOAContact(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAContact"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void OAContactAdd()
        {
            if (this._OAContactCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllOAContact(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "OAContact";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void OAContactDelete()
        {
            if (this._dao == null)
            {
                this.DeleteOAContact(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "OAContact";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void OAContactSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllOAContact(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "OAContact";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void OAContactUpdate()
        {
            if (this._OAContactCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllOAContact(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "OAContact";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void SubmitAllOAContact(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAContact"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateOAContact(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAContact"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string Author
        {
            get
            {
                if ((this._Author == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._Author;
            }
            set
            {
                this._Author = value;
            }
        }

        public string CDate
        {
            get
            {
                if ((this._CDate == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._CDate;
            }
            set
            {
                this._CDate = value;
            }
        }

        public string content
        {
            get
            {
                if ((this._content == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public StandardEntityDAO dao
        {
            get
            {
                return this._dao;
            }
            set
            {
                this._dao = value;
            }
        }

        public string OAContactCode
        {
            get
            {
                return this._OAContactCode;
            }
            set
            {
                this._OAContactCode = value;
            }
        }

        public string phone
        {
            get
            {
                if ((this._phone == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._phone;
            }
            set
            {
                this._phone = value;
            }
        }

        public string SubmitReport
        {
            get
            {
                if ((this._SubmitReport == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._SubmitReport;
            }
            set
            {
                this._SubmitReport = value;
            }
        }

        public string takPerson
        {
            get
            {
                if ((this._takPerson == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._takPerson;
            }
            set
            {
                this._takPerson = value;
            }
        }

        public string takTime
        {
            get
            {
                if ((this._takTime == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._takTime;
            }
            set
            {
                this._takTime = value;
            }
        }

        public string title
        {
            get
            {
                if ((this._title == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public string Type
        {
            get
            {
                if ((this._Type == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        public string Unit
        {
            get
            {
                if ((this._Unit == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }

        public string UserSend
        {
            get
            {
                if ((this._UserSend == null) && (this._OAContactCode != null))
                {
                    this._GetOAContactByCode();
                }
                return this._UserSend;
            }
            set
            {
                this._UserSend = value;
            }
        }
    }
}

