namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class Cash_Detail
    {
        private string _Cash = null;
        private string _Cash_MessageCode = null;
        private string _CashDetialCode = null;
        private string _CashDetialRemark = null;
        private string _CashDetialState = null;
        private string _CashMessageCostBudgeSetCode = null;
        private string _CashMessageCostName = null;
        private string _CashMessagePBSCode = null;
        private string _CashMessagePBSType = null;
        private string _CashMessageRemark1 = null;
        private string _CashMessageRemark2 = null;
        private string _CostCode = null;
        private StandardEntityDAO _dao;
        private string _DetailID = null;
        private string _ExchangeRate = null;
        private string _MoneyType = null;
        private string _MoneyTypeID = null;
        private string _RMB = null;
        private string _TemporaryMoney = null;

        private void _GetCash_DetailByCode()
        {
            EntityData data = this.GetCash_DetailByCode(this._CashDetialCode);
            this._DetailID = data.GetString("DetailID");
            this._CashDetialCode = data.GetString("CashDetialCode");
            this._CostCode = data.GetString("CostCode");
            this._Cash = data.GetString("Cash");
            this._MoneyType = data.GetString("MoneyType");
            this._ExchangeRate = data.GetString("ExchangeRate");
            this._MoneyTypeID = data.GetString("MoneyTypeID");
            this._CashDetialRemark = data.GetString("CashDetialRemark");
            this._CashDetialState = data.GetString("CashDetialState");
            this._Cash_MessageCode = data.GetString("Cash_MessageCode");
            this._CashMessageRemark1 = data.GetString("CashMessageRemark1");
            this._CashMessageRemark2 = data.GetString("CashMessageRemark2");
            this._TemporaryMoney = data.GetString("TemporaryMoney");
            this._CashMessageCostName = data.GetString("CashMessageCostName");
            this._CashMessageCostBudgeSetCode = data.GetString("CashMessageCostBudgeSetCode");
            this._CashMessagePBSType = data.GetString("CashMessagePBSType");
            this._CashMessagePBSCode = data.GetString("CashMessagePBSCode");
            this._RMB = data.GetString("RMB");
            data.Dispose();
        }

        private EntityData _GetCash_Details()
        {
            EntityData entitydata = new EntityData("Cash_Detail");
            Cash_DetailStrategyBuilder builder = new Cash_DetailStrategyBuilder();
            if (this._DetailID != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.DetailID, this._DetailID));
            }
            if (this._CashDetialCode != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashDetialCode, this._CashDetialCode));
            }
            if (this._CostCode != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CostCode, this._CostCode));
            }
            if (this._Cash != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.Cash, this._Cash));
            }
            if (this._MoneyType != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.MoneyType, this._MoneyType));
            }
            if (this._ExchangeRate != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.ExchangeRate, this._ExchangeRate));
            }
            if (this._MoneyTypeID != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.MoneyTypeID, this._MoneyTypeID));
            }
            if (this._CashDetialRemark != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashDetialRemark, this._CashDetialRemark));
            }
            if (this._CashDetialState != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashDetialState, this._CashDetialState));
            }
            if (this._Cash_MessageCode != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.Cash_MessageCode, this._Cash_MessageCode));
            }
            if (this._CashMessageRemark1 != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashMessageRemark1, this._CashMessageRemark1));
            }
            if (this._CashMessageRemark2 != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashMessageRemark2, this._CashMessageRemark2));
            }
            if (this._TemporaryMoney != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.TemporaryMoney, this._TemporaryMoney));
            }
            if (this._CashMessageCostName != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashMessageCostName, this._CashMessageCostName));
            }
            if (this._CashMessageCostBudgeSetCode != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashMessageCostBudgeSetCode, this._CashMessageCostBudgeSetCode));
            }
            if (this._CashMessagePBSType != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashMessagePBSType, this._CashMessagePBSType));
            }
            if (this._CashMessagePBSCode != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.CashMessagePBSCode, this._CashMessagePBSCode));
            }
            if (this._RMB != null)
            {
                builder.AddStrategy(new Strategy(Cash_DetailStrategyName.RMB, this._RMB));
            }
            string queryString = builder.BuildMainQueryString() + " order by CashDetialCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("Cash_Detail", queryString);
            }
            this.dao.EntityName = "Cash_Detail";
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData data;
            DataRow newRecord;
            bool flag = false;
            if (this._CashDetialCode == "")
            {
                flag = true;
                data = this.GetCash_DetailByCode("");
                this._CashDetialCode = SystemManageDAO.GetNewSysCode("Cash_Detail");
                newRecord = data.GetNewRecord();
            }
            else
            {
                data = this.GetCash_DetailByCode(this._CashDetialCode);
                newRecord = data.CurrentRow;
            }
            if (this._DetailID != null)
            {
                newRecord["DetailID"] = this._DetailID;
            }
            if (this._CashDetialCode != null)
            {
                newRecord["CashDetialCode"] = this._CashDetialCode;
            }
            if (this._CostCode != null)
            {
                newRecord["CostCode"] = this._CostCode;
            }
            if (this._Cash != null)
            {
                newRecord["Cash"] = this._Cash;
            }
            if (this._MoneyType != null)
            {
                newRecord["MoneyType"] = this._MoneyType;
            }
            if (this._ExchangeRate != null)
            {
                newRecord["ExchangeRate"] = this._ExchangeRate;
            }
            if (this._MoneyTypeID != null)
            {
                newRecord["MoneyTypeID"] = this._MoneyTypeID;
            }
            if (this._CashDetialRemark != null)
            {
                newRecord["CashDetialRemark"] = this._CashDetialRemark;
            }
            if (this._CashDetialState != null)
            {
                newRecord["CashDetialState"] = this._CashDetialState;
            }
            if (this._Cash_MessageCode != null)
            {
                newRecord["Cash_MessageCode"] = this._Cash_MessageCode;
            }
            if (this._CashMessageRemark1 != null)
            {
                newRecord["CashMessageRemark1"] = this._CashMessageRemark1;
            }
            if (this._CashMessageRemark2 != null)
            {
                newRecord["CashMessageRemark2"] = this._CashMessageRemark2;
            }
            if (this._TemporaryMoney != null)
            {
                newRecord["TemporaryMoney"] = this._TemporaryMoney;
            }
            if (this._CashMessageCostName != null)
            {
                newRecord["CashMessageCostName"] = this._CashMessageCostName;
            }
            if (this._CashMessageCostBudgeSetCode != null)
            {
                newRecord["CashMessageCostBudgeSetCode"] = this._CashMessageCostBudgeSetCode;
            }
            if (this._CashMessagePBSType != null)
            {
                newRecord["CashMessagePBSType"] = this._CashMessagePBSType;
            }
            if (this._CashMessagePBSCode != null)
            {
                newRecord["CashMessagePBSCode"] = this._CashMessagePBSCode;
            }
            if (this._RMB != null)
            {
                newRecord["RMB"] = this._RMB;
            }
            if (flag)
            {
                data.AddNewRecord(newRecord);
            }
            return data;
        }

        public void Cash_DetailAdd()
        {
            if (this._CashDetialCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllCash_Detail(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Cash_Detail";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void Cash_DetailDelete()
        {
            if (this._dao == null)
            {
                this.DeleteCash_Detail(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Cash_Detail";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void Cash_DetailSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllCash_Detail(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Cash_Detail";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void Cash_DetailUpdate()
        {
            if (this._CashDetialCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllCash_Detail(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Cash_Detail";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void DeleteCash_Detail(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Detail"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Detail";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllCash_Detail()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Detail"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Detail";
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

        private EntityData GetCash_DetailByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Detail"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Detail";
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

        public DataTable GetCash_Details()
        {
            return this._GetCash_Details().CurrentTable;
        }

        private void InsertCash_Detail(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Detail"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Detail";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllCash_Detail(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Detail"))
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
                    this.dao.EntityName = "Cash_Detail";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateCash_Detail(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Cash_Detail"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Cash_Detail";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string Cash
        {
            get
            {
                if ((this._Cash == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._Cash;
            }
            set
            {
                this._Cash = value;
            }
        }

        public string Cash_MessageCode
        {
            get
            {
                if ((this._Cash_MessageCode == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._Cash_MessageCode;
            }
            set
            {
                this._Cash_MessageCode = value;
            }
        }

        public string CashDetialCode
        {
            get
            {
                return this._CashDetialCode;
            }
            set
            {
                this._CashDetialCode = value;
            }
        }

        public string CashDetialRemark
        {
            get
            {
                if ((this._CashDetialRemark == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CashDetialRemark;
            }
            set
            {
                this._CashDetialRemark = value;
            }
        }

        public string CashDetialState
        {
            get
            {
                if ((this._CashDetialState == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CashDetialState;
            }
            set
            {
                this._CashDetialState = value;
            }
        }

        public string CashMessageCostBudgeSetCode
        {
            get
            {
                if ((this._CashMessageCostBudgeSetCode == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CashMessageCostBudgeSetCode;
            }
            set
            {
                this._CashMessageCostBudgeSetCode = value;
            }
        }

        public string CashMessageCostName
        {
            get
            {
                if ((this._CashMessageCostName == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CashMessageCostName;
            }
            set
            {
                this._CashMessageCostName = value;
            }
        }

        public string CashMessagePBSCode
        {
            get
            {
                if ((this._CashMessagePBSCode == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CashMessagePBSCode;
            }
            set
            {
                this._CashMessagePBSCode = value;
            }
        }

        public string CashMessagePBSType
        {
            get
            {
                if ((this._CashMessagePBSType == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CashMessagePBSType;
            }
            set
            {
                this._CashMessagePBSType = value;
            }
        }

        public string CashMessageRemark1
        {
            get
            {
                if ((this._CashMessageRemark1 == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
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
                if ((this._CashMessageRemark2 == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CashMessageRemark2;
            }
            set
            {
                this._CashMessageRemark2 = value;
            }
        }

        public string CostCode
        {
            get
            {
                if ((this._CostCode == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._CostCode;
            }
            set
            {
                this._CostCode = value;
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

        public string DetailID
        {
            get
            {
                if ((this._DetailID == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._DetailID;
            }
            set
            {
                this._DetailID = value;
            }
        }

        public string ExchangeRate
        {
            get
            {
                if ((this._ExchangeRate == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._ExchangeRate;
            }
            set
            {
                this._ExchangeRate = value;
            }
        }

        public string MoneyType
        {
            get
            {
                if ((this._MoneyType == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._MoneyType;
            }
            set
            {
                this._MoneyType = value;
            }
        }

        public string MoneyTypeID
        {
            get
            {
                if ((this._MoneyTypeID == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._MoneyTypeID;
            }
            set
            {
                this._MoneyTypeID = value;
            }
        }

        public string RMB
        {
            get
            {
                if ((this._RMB == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._RMB;
            }
            set
            {
                this._RMB = value;
            }
        }

        public string TemporaryMoney
        {
            get
            {
                if ((this._TemporaryMoney == null) && (this._CashDetialCode != null))
                {
                    this._GetCash_DetailByCode();
                }
                return this._TemporaryMoney;
            }
            set
            {
                this._TemporaryMoney = value;
            }
        }
    }
}

