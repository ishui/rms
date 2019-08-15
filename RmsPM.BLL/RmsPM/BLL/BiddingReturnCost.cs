namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingReturnCost
    {
        private string _BiddingReturnCode = null;
        private string _BiddingReturnCostCode = null;
        private string _BiddingReturnCostID = null;
        private string _Cash = null;
        private StandardEntityDAO _dao;
        private string _ExchangeRate = null;
        private string _MoneyType = null;
        private string _MoneyTypeID = null;

        private void _GetBiddingReturnCostByCode()
        {
            EntityData biddingReturnCostByCode = this.GetBiddingReturnCostByCode(this._BiddingReturnCostCode);
            this._BiddingReturnCode = biddingReturnCostByCode.GetString("BiddingReturnCode");
            this._BiddingReturnCostCode = biddingReturnCostByCode.GetString("BiddingReturnCostCode");
            this._BiddingReturnCostID = biddingReturnCostByCode.GetString("BiddingReturnCostID");
            this._Cash = biddingReturnCostByCode.GetString("Cash");
            this._MoneyType = biddingReturnCostByCode.GetString("MoneyType");
            this._MoneyTypeID = biddingReturnCostByCode.GetString("MoneyTypeID");
            this._ExchangeRate = biddingReturnCostByCode.GetString("ExchangeRate");
            biddingReturnCostByCode.Dispose();
        }

        private EntityData _GetBiddingReturnCosts()
        {
            EntityData entitydata = new EntityData("BiddingReturnCost");
            BiddingReturnCostStrategyBuilder builder = new BiddingReturnCostStrategyBuilder();
            if (this._BiddingReturnCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnCostStrategyName.BiddingReturnCode, this._BiddingReturnCode));
            }
            if (this._BiddingReturnCostCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnCostStrategyName.BiddingReturnCostCode, this._BiddingReturnCostCode));
            }
            if (this._BiddingReturnCostID != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnCostStrategyName.BiddingReturnCostID, this._BiddingReturnCostID));
            }
            if (this._Cash != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnCostStrategyName.Cash, this._Cash));
            }
            if (this._MoneyType != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnCostStrategyName.MoneyType, this._MoneyType));
            }
            if (this._MoneyTypeID != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnCostStrategyName.MoneyTypeID, this._MoneyTypeID));
            }
            if (this._ExchangeRate != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnCostStrategyName.ExchangeRate, this._ExchangeRate));
            }
            string sqlString = builder.BuildMainQueryString() + " order by BiddingReturnCostCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturnCost"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "BiddingReturnCost";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public void BiddingReturnCostAdd()
        {
            if (this._BiddingReturnCostCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingReturnCost(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingReturnCost";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void BiddingReturnCostDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBiddingReturnCost(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingReturnCost";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void BiddingReturnCostSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingReturnCost(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingReturnCost";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingReturnCostUpdate()
        {
            if (this._BiddingReturnCostCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingReturnCost(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingReturnCost";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData biddingReturnCostByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._BiddingReturnCostCode == "")
            {
                flag = true;
                biddingReturnCostByCode = this.GetBiddingReturnCostByCode("");
                this._BiddingReturnCostCode = SystemManageDAO.GetNewSysCode("BiddingReturnCost");
                newRecord = biddingReturnCostByCode.GetNewRecord();
            }
            else
            {
                biddingReturnCostByCode = this.GetBiddingReturnCostByCode(this._BiddingReturnCostCode);
                newRecord = biddingReturnCostByCode.CurrentRow;
            }
            if (this._BiddingReturnCode != null)
            {
                newRecord["BiddingReturnCode"] = this._BiddingReturnCode;
            }
            if (this._BiddingReturnCostCode != null)
            {
                newRecord["BiddingReturnCostCode"] = this._BiddingReturnCostCode;
            }
            if (this._BiddingReturnCostID != null)
            {
                newRecord["BiddingReturnCostID"] = this._BiddingReturnCostID;
            }
            if (this._Cash != null)
            {
                newRecord["Cash"] = this._Cash;
            }
            if (this._MoneyType != null)
            {
                newRecord["MoneyType"] = this._MoneyType;
            }
            if (this._MoneyTypeID != null)
            {
                newRecord["MoneyTypeID"] = this._MoneyTypeID;
            }
            if (this._ExchangeRate != null)
            {
                newRecord["ExchangeRate"] = this._ExchangeRate;
            }
            if (flag)
            {
                biddingReturnCostByCode.AddNewRecord(newRecord);
            }
            return biddingReturnCostByCode;
        }

        private void DeleteBiddingReturnCost(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturnCost"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturnCost";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllBiddingReturnCost()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturnCost"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturnCost";
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

        private EntityData GetBiddingReturnCostByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturnCost"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturnCost";
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

        public DataTable GetBiddingReturnCosts()
        {
            return this._GetBiddingReturnCosts().CurrentTable;
        }

        private void InsertBiddingReturnCost(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturnCost"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturnCost";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllBiddingReturnCost(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturnCost"))
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
                    this.dao.EntityName = "BiddingReturnCost";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateBiddingReturnCost(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturnCost"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturnCost";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BiddingReturnCode
        {
            get
            {
                if ((this._BiddingReturnCode == null) && (this._BiddingReturnCostCode != null))
                {
                    this._GetBiddingReturnCostByCode();
                }
                return this._BiddingReturnCode;
            }
            set
            {
                this._BiddingReturnCode = value;
            }
        }

        public string BiddingReturnCostCode
        {
            get
            {
                return this._BiddingReturnCostCode;
            }
            set
            {
                this._BiddingReturnCostCode = value;
            }
        }

        public string BiddingReturnCostID
        {
            get
            {
                if ((this._BiddingReturnCostID == null) && (this._BiddingReturnCostCode != null))
                {
                    this._GetBiddingReturnCostByCode();
                }
                return this._BiddingReturnCostID;
            }
            set
            {
                this._BiddingReturnCostID = value;
            }
        }

        public string Cash
        {
            get
            {
                if ((this._Cash == null) && (this._BiddingReturnCostCode != null))
                {
                    this._GetBiddingReturnCostByCode();
                }
                return this._Cash;
            }
            set
            {
                this._Cash = value;
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

        public string ExchangeRate
        {
            get
            {
                if ((this._ExchangeRate == null) && (this._BiddingReturnCostCode != null))
                {
                    this._GetBiddingReturnCostByCode();
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
                if ((this._MoneyType == null) && (this._BiddingReturnCostCode != null))
                {
                    this._GetBiddingReturnCostByCode();
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
                if ((this._MoneyTypeID == null) && (this._BiddingReturnCostCode != null))
                {
                    this._GetBiddingReturnCostByCode();
                }
                return this._MoneyTypeID;
            }
            set
            {
                this._MoneyTypeID = value;
            }
        }
    }
}

