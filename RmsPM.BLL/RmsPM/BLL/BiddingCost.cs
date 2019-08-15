namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingCost
    {
        private string _BiddingCode = null;
        private string _BiddingCostCode = null;
        private string _BiddingCostID = null;
        private string _Cash = null;
        private string _CostCode = null;
        private StandardEntityDAO _dao;
        private string _ExchangeRate = null;
        private string _MoneyType = null;
        private string _MoneyTypeID = null;
        private string _ObligateCash = null;

        private void _GetBiddingCostByCode()
        {
            try
            {
                EntityData biddingCostByCode = this.GetBiddingCostByCode(this._BiddingCostCode);
                this._BiddingCode = biddingCostByCode.GetString("BiddingCode");
                this._CostCode = biddingCostByCode.GetString("CostCode");
                this._Cash = biddingCostByCode.GetString("Cash");
                this._MoneyType = biddingCostByCode.GetString("MoneyType");
                this._ExchangeRate = biddingCostByCode.GetString("ExchangeRate");
                this._MoneyTypeID = biddingCostByCode.GetString("MoneyTypeID");
                this._BiddingCostCode = biddingCostByCode.GetString("BiddingCostCode");
                this._BiddingCostID = biddingCostByCode.GetString("BiddingCostID");
                this._ObligateCash = biddingCostByCode.GetString("ObligateCash");
                biddingCostByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetBiddingCosts()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BiddingCost");
                BiddingCostStrategyBuilder builder = new BiddingCostStrategyBuilder();
                if (this._BiddingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._CostCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.CostCode, this._CostCode));
                }
                if (this._Cash != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.Cash, this._Cash));
                }
                if (this._MoneyType != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.MoneyType, this._MoneyType));
                }
                if (this._ExchangeRate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.ExchangeRate, this._ExchangeRate));
                }
                if (this._MoneyTypeID != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.MoneyTypeID, this._MoneyTypeID));
                }
                if (this._BiddingCostCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.BiddingCostCode, this._BiddingCostCode));
                }
                if (this._BiddingCostID != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.BiddingCostID, this._BiddingCostID));
                }
                if (this._ObligateCash != null)
                {
                    builder.AddStrategy(new Strategy(BiddingCostStrategyName.ObligateCash, this._ObligateCash));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingCostCode";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingCost"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
                    this.dao.FillEntity(sqlString, entitydata);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public void BiddingCostAdd()
        {
            try
            {
                if (this._BiddingCostCode == null)
                {
                    if (this._dao == null)
                    {
                        this.SubmitAllBiddingCost(this.BuildData());
                    }
                    else
                    {
                        this.dao.EntityName = "BiddingCost";
                        this.dao.SubmitEntity(this.BuildData());
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingCostDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    this.DeleteBiddingCost(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingCostSubmit()
        {
            try
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingCost(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingCostUpdate()
        {
            try
            {
                if (this._BiddingCostCode != null)
                {
                    if (this._dao == null)
                    {
                        this.SubmitAllBiddingCost(this.BuildData());
                    }
                    else
                    {
                        this.dao.EntityName = "BiddingCost";
                        this.dao.SubmitEntity(this.BuildData());
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData biddingCostByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingCostCode == "")
                {
                    flag = true;
                    biddingCostByCode = this.GetBiddingCostByCode("");
                    this._BiddingCostCode = SystemManageDAO.GetNewSysCode("BiddingCost");
                    newRecord = biddingCostByCode.GetNewRecord();
                }
                else
                {
                    biddingCostByCode = this.GetBiddingCostByCode(this._BiddingCostCode);
                    newRecord = biddingCostByCode.CurrentRow;
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
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
                if (this._BiddingCostCode != null)
                {
                    newRecord["BiddingCostCode"] = this._BiddingCostCode;
                }
                if (this._BiddingCostID != null)
                {
                    newRecord["BiddingCostID"] = this._BiddingCostID;
                }
                if (this._ObligateCash != null)
                {
                    newRecord["ObligateCash"] = this._ObligateCash;
                }
                if (flag)
                {
                    biddingCostByCode.AddNewRecord(newRecord);
                }
                data2 = biddingCostByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private void DeleteBiddingCost(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingCost"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllBiddingCost()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingCost"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
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

        private EntityData GetBiddingCostByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingCost"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
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

        public DataTable GetBiddingCosts()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetBiddingCosts().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        private void InsertBiddingCost(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingCost"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllBiddingCost(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingCost"))
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
                    this.dao.EntityName = "BiddingCost";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateBiddingCost(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingCost"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingCost";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BiddingCode
        {
            get
            {
                if ((this._BiddingCode == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
                }
                return this._BiddingCode;
            }
            set
            {
                this._BiddingCode = value;
            }
        }

        public string BiddingCostCode
        {
            get
            {
                return this._BiddingCostCode;
            }
            set
            {
                this._BiddingCostCode = value;
            }
        }

        public string BiddingCostID
        {
            get
            {
                if ((this._BiddingCostID == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
                }
                return this._BiddingCostID;
            }
            set
            {
                this._BiddingCostID = value;
            }
        }

        public string Cash
        {
            get
            {
                if ((this._Cash == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
                }
                return this._Cash;
            }
            set
            {
                this._Cash = value;
            }
        }

        public string CostCode
        {
            get
            {
                if ((this._CostCode == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
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

        public string ExchangeRate
        {
            get
            {
                if ((this._ExchangeRate == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
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
                if ((this._MoneyType == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
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
                if ((this._MoneyTypeID == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
                }
                return this._MoneyTypeID;
            }
            set
            {
                this._MoneyTypeID = value;
            }
        }

        public string ObligateCash
        {
            get
            {
                if ((this._ObligateCash == null) && (this._BiddingCostCode != null))
                {
                    this._GetBiddingCostByCode();
                }
                return this._ObligateCash;
            }
            set
            {
                this._ObligateCash = value;
            }
        }
    }
}

