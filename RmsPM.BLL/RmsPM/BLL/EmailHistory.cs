namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class EmailHistory
    {
        private StandardEntityDAO _dao;
        private string _EmailContent = null;
        private string _EmailHistoryCode = null;
        private string _EmailTitle = null;
        private string _EmailType = null;
        private string _MasterCode = null;
        private string _Receiver = null;
        private string _SendDate = null;
        private string _Sender = null;

        private void _GetEmailHistoryByCode()
        {
            EntityData emailHistoryByCode = this.GetEmailHistoryByCode(this._EmailHistoryCode);
            this._EmailHistoryCode = emailHistoryByCode.GetString("EmailHistoryCode");
            this._EmailType = emailHistoryByCode.GetString("EmailType");
            this._MasterCode = emailHistoryByCode.GetString("MasterCode");
            this._EmailTitle = emailHistoryByCode.GetString("EmailTitle");
            this._Receiver = emailHistoryByCode.GetString("Receiver");
            this._Sender = emailHistoryByCode.GetString("Sender");
            this._EmailContent = emailHistoryByCode.GetString("EmailContent");
            this._SendDate = emailHistoryByCode.GetDateTime("SendDate");
            emailHistoryByCode.Dispose();
        }

        private EntityData _GetEmailHistorys()
        {
            EntityData entitydata = new EntityData("EmailHistory");
            EmailHistoryStrategyBuilder builder = new EmailHistoryStrategyBuilder();
            if (this._EmailHistoryCode != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.EmailHistoryCode, this._EmailHistoryCode));
            }
            if (this._EmailType != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.EmailType, this._EmailType));
            }
            if (this._MasterCode != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.MasterCode, this._MasterCode));
            }
            if (this._EmailTitle != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.EmailTitle, this._EmailTitle));
            }
            if (this._Receiver != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.Receiver, this._Receiver));
            }
            if (this._Sender != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.Sender, this._Sender));
            }
            if (this._EmailContent != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.EmailContent, this._EmailContent));
            }
            if (this._SendDate != null)
            {
                builder.AddStrategy(new Strategy(EmailHistoryStrategyName.SendDate, this._SendDate));
            }
            string sqlString = builder.BuildMainQueryString() + " order by EmailHistoryCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("EmailHistory"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "EmailHistory";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData emailHistoryByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._EmailHistoryCode == "")
            {
                flag = true;
                emailHistoryByCode = this.GetEmailHistoryByCode("");
                this._EmailHistoryCode = SystemManageDAO.GetNewSysCode("EmailHistory");
                newRecord = emailHistoryByCode.GetNewRecord();
            }
            else
            {
                emailHistoryByCode = this.GetEmailHistoryByCode(this._EmailHistoryCode);
                newRecord = emailHistoryByCode.CurrentRow;
            }
            if (this._EmailHistoryCode != null)
            {
                newRecord["EmailHistoryCode"] = this._EmailHistoryCode;
            }
            if (this._EmailType != null)
            {
                newRecord["EmailType"] = this._EmailType;
            }
            if (this._MasterCode != null)
            {
                newRecord["MasterCode"] = this._MasterCode;
            }
            if (this._EmailTitle != null)
            {
                newRecord["EmailTitle"] = this._EmailTitle;
            }
            if (this._Receiver != null)
            {
                newRecord["Receiver"] = this._Receiver;
            }
            if (this._Sender != null)
            {
                newRecord["Sender"] = this._Sender;
            }
            if (this._EmailContent != null)
            {
                newRecord["EmailContent"] = this._EmailContent;
            }
            if (this._SendDate != null)
            {
                newRecord["SendDate"] = this._SendDate;
            }
            if (flag)
            {
                emailHistoryByCode.AddNewRecord(newRecord);
            }
            return emailHistoryByCode;
        }

        private void DeleteEmailHistory(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("EmailHistory"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void EmailHistoryAdd()
        {
            if ((this._EmailHistoryCode == null) || (this._EmailHistoryCode == ""))
            {
                if (this._dao == null)
                {
                    this.SubmitAllEmailHistory(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void EmailHistoryDelete()
        {
            if (this._dao == null)
            {
                this.DeleteEmailHistory(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "EmailHistory";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void EmailHistoryDeleteByCode()
        {
            try
            {
                if (this._dao == null)
                {
                    this.dao.DeleteEntity(this.GetEmailHistoryByCode(this.EmailHistoryCode));
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    this.dao.DeleteEntity(this.GetEmailHistoryByCode(this.EmailHistoryCode));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void EmailHistorySubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllEmailHistory(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "EmailHistory";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void EmailHistoryUpdate()
        {
            if (this._EmailHistoryCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllEmailHistory(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData GetAllEmailHistory()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("EmailHistory"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    data = this.dao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private EntityData GetEmailHistoryByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("EmailHistory"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    data = this.dao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetEmailHistorys()
        {
            return this._GetEmailHistorys().CurrentTable;
        }

        private void InsertEmailHistory(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("EmailHistory"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllEmailHistory(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("EmailHistory"))
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
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateEmailHistory(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("EmailHistory"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "EmailHistory";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
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

        public string EmailContent
        {
            get
            {
                if ((this._EmailContent == null) && (this._EmailHistoryCode != null))
                {
                    this._GetEmailHistoryByCode();
                }
                return this._EmailContent;
            }
            set
            {
                this._EmailContent = value;
            }
        }

        public string EmailHistoryCode
        {
            get
            {
                return this._EmailHistoryCode;
            }
            set
            {
                this._EmailHistoryCode = value;
            }
        }

        public string EmailTitle
        {
            get
            {
                if ((this._EmailTitle == null) && (this._EmailHistoryCode != null))
                {
                    this._GetEmailHistoryByCode();
                }
                return this._EmailTitle;
            }
            set
            {
                this._EmailTitle = value;
            }
        }

        public string EmailType
        {
            get
            {
                if ((this._EmailType == null) && (this._EmailHistoryCode != null))
                {
                    this._GetEmailHistoryByCode();
                }
                return this._EmailType;
            }
            set
            {
                this._EmailType = value;
            }
        }

        public string MasterCode
        {
            get
            {
                if ((this._MasterCode == null) && (this._EmailHistoryCode != null))
                {
                    this._GetEmailHistoryByCode();
                }
                return this._MasterCode;
            }
            set
            {
                this._MasterCode = value;
            }
        }

        public string Receiver
        {
            get
            {
                if ((this._Receiver == null) && (this._EmailHistoryCode != null))
                {
                    this._GetEmailHistoryByCode();
                }
                return this._Receiver;
            }
            set
            {
                this._Receiver = value;
            }
        }

        public string SendDate
        {
            get
            {
                if ((this._SendDate == null) && (this._EmailHistoryCode != null))
                {
                    this._GetEmailHistoryByCode();
                }
                return this._SendDate;
            }
            set
            {
                this._SendDate = value;
            }
        }

        public string Sender
        {
            get
            {
                if ((this._Sender == null) && (this._EmailHistoryCode != null))
                {
                    this._GetEmailHistoryByCode();
                }
                return this._Sender;
            }
            set
            {
                this._Sender = value;
            }
        }
    }
}

