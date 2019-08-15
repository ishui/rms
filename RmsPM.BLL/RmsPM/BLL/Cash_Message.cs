namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class Cash_Message
    {
        private string _CashMessageCashTotal = null;
        private string _CashMessageCode = null;
        private string _CashMessageID = null;
        private string _CashMessageRemark = null;
        private string _CashMessageRemark1 = null;
        private string _CashMessageRemark2 = null;
        private string _CashMessageState = null;
        private string _CashMessageTemporaryMoney = null;
        private string _CashMessageType = null;
        private string _CashMessageTypeCode = null;
        private StandardEntityDAO _dao;

        private void _GetCash_MessageByCode()
        {
            EntityData data = this.GetCash_MessageByCode(this._CashMessageCode);
            this._CashMessageCode = data.GetString("CashMessageCode");
            this._CashMessageID = data.GetInt("CashMessageID").ToString();
            this._CashMessageType = data.GetString("CashMessageType");
            this._CashMessageTypeCode = data.GetString("CashMessageTypeCode");
            this._CashMessageState = data.GetString("CashMessageState");
            this._CashMessageRemark = data.GetString("CashMessageRemark");
            this._CashMessageCashTotal = data.GetDecimal("CashMessageCashTotal").ToString();
            this._CashMessageRemark1 = data.GetString("CashMessageRemark1");
            this._CashMessageRemark2 = data.GetString("CashMessageRemark2");
            this._CashMessageTemporaryMoney = data.GetDecimal("CashMessageTemporaryMoney").ToString();
            data.Dispose();
        }

        private EntityData _GetCash_Messages()
        {
            EntityData entitydata = new EntityData("Cash_Message");
            Cash_MessageStrategyBuilder builder = new Cash_MessageStrategyBuilder();
            if (this._CashMessageCode != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageCode, this._CashMessageCode));
            }
            if (this._CashMessageID != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageID, this._CashMessageID));
            }
            if (this._CashMessageType != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageType, this._CashMessageType));
            }
            if (this._CashMessageTypeCode != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageTypeCode, this._CashMessageTypeCode));
            }
            if (this._CashMessageState != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageState, this._CashMessageState));
            }
            if (this._CashMessageRemark != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageRemark, this._CashMessageRemark));
            }
            if (this._CashMessageCashTotal != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageCashTotal, this._CashMessageCashTotal));
            }
            if (this._CashMessageRemark1 != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageRemark1, this._CashMessageRemark1));
            }
            if (this._CashMessageRemark2 != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageRemark2, this._CashMessageRemark2));
            }
            if (this._CashMessageTemporaryMoney != null)
            {
                builder.AddStrategy(new Strategy(Cash_MessageStrategyName.CashMessageTemporaryMoney, this._CashMessageTemporaryMoney));
            }
            string queryString = builder.BuildMainQueryString() + " order by CashMessageCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("Cash_Message", queryString);
            }
            this.dao.EntityName = "Cash_Message";
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData data;
            DataRow newRecord;
            bool flag = false;
            if ((this._CashMessageCode == "") || (this._CashMessageCode == null))
            {
                flag = true;
                data = this.GetCash_MessageByCode("");
                this._CashMessageCode = SystemManageDAO.GetNewSysCode("Cash_Message");
                newRecord = data.GetNewRecord();
            }
            else
            {
                data = this.GetCash_MessageByCode(this._CashMessageCode);
                newRecord = data.CurrentRow;
            }
            if (this._CashMessageCode != null)
            {
                newRecord["CashMessageCode"] = this._CashMessageCode;
            }
            if (this._CashMessageID != null)
            {
                newRecord["CashMessageID"] = this._CashMessageID;
            }
            if (this._CashMessageType != null)
            {
                newRecord["CashMessageType"] = this._CashMessageType;
            }
            if (this._CashMessageTypeCode != null)
            {
                newRecord["CashMessageTypeCode"] = this._CashMessageTypeCode;
            }
            if (this._CashMessageState != null)
            {
                newRecord["CashMessageState"] = this._CashMessageState;
            }
            if (this._CashMessageRemark != null)
            {
                newRecord["CashMessageRemark"] = this._CashMessageRemark;
            }
            if (this._CashMessageCashTotal != null)
            {
                newRecord["CashMessageCashTotal"] = this._CashMessageCashTotal;
            }
            if (this._CashMessageRemark1 != null)
            {
                newRecord["CashMessageRemark1"] = this._CashMessageRemark1;
            }
            if (this._CashMessageRemark2 != null)
            {
                newRecord["CashMessageRemark2"] = this._CashMessageRemark2;
            }
            if ((this._CashMessageTemporaryMoney != null) & (this._CashMessageTemporaryMoney != ""))
            {
                newRecord["CashMessageTemporaryMoney"] = this._CashMessageTemporaryMoney;
            }
            if (flag)
            {
                data.AddNewRecord(newRecord);
            }
            return data;
        }

        public void Cash_MessageAdd()
        {
            if (this._CashMessageCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllCash_Message(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Cash_Message";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void Cash_MessageDelete()
        {
            if (this._dao == null)
            {
                this.DeleteCash_Message(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Cash_Message";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void Cash_MessageSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllCash_Message(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Cash_Message";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void Cash_MessageUpdate()
        {
            if (this._CashMessageCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllCash_Message(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Cash_Message";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void DeleteCash_Message(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Message"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Message";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllCash_Message()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Message"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Message";
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

        private EntityData GetCash_MessageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Message"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Message";
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

        public DataTable GetCash_Messages()
        {
            return this._GetCash_Messages().CurrentTable;
        }

        private void InsertCash_Message(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Message"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Message";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllCash_Message(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Message"))
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
                    this.dao.EntityName = "Cash_Message";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateCash_Message(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Message"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Message";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string CashMessageCashTotal
        {
            get
            {
                if ((this._CashMessageCashTotal == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageCashTotal;
            }
            set
            {
                this._CashMessageCashTotal = value;
            }
        }

        public string CashMessageCode
        {
            get
            {
                return this._CashMessageCode;
            }
            set
            {
                this._CashMessageCode = value;
            }
        }

        public string CashMessageID
        {
            get
            {
                if ((this._CashMessageID == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageID;
            }
            set
            {
                this._CashMessageID = value;
            }
        }

        public string CashMessageRemark
        {
            get
            {
                if ((this._CashMessageRemark == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageRemark;
            }
            set
            {
                this._CashMessageRemark = value;
            }
        }

        public string CashMessageRemark1
        {
            get
            {
                if ((this._CashMessageRemark1 == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageRemark1;
            }
            set
            {
                this._CashMessageRemark1 = value;
            }
        }

        public string CashMessageRemark2
        {
            get
            {
                if ((this._CashMessageRemark2 == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageRemark2;
            }
            set
            {
                this._CashMessageRemark2 = value;
            }
        }

        public string CashMessageState
        {
            get
            {
                if ((this._CashMessageState == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageState;
            }
            set
            {
                this._CashMessageState = value;
            }
        }

        public string CashMessageTemporaryMoney
        {
            get
            {
                if ((this._CashMessageTemporaryMoney == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageTemporaryMoney;
            }
            set
            {
                this._CashMessageTemporaryMoney = value;
            }
        }

        public string CashMessageType
        {
            get
            {
                if ((this._CashMessageType == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageType;
            }
            set
            {
                this._CashMessageType = value;
            }
        }

        public string CashMessageTypeCode
        {
            get
            {
                if ((this._CashMessageTypeCode == null) && (this._CashMessageCode != null))
                {
                    this._GetCash_MessageByCode();
                }
                return this._CashMessageTypeCode;
            }
            set
            {
                this._CashMessageTypeCode = value;
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
    }
}

