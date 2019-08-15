namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingDtl
    {
        private string _BiddingCode = null;
        private string _BiddingDtlCode = null;
        private string _CostBudgetSetCode = null;
        private string _CostCode = null;
        private StandardEntityDAO _dao;
        private string _flag = null;
        private string _Money = null;
        private string _OtherMoney = null;
        private string _OtherMoneyRate = null;
        private string _OtherMoneyType = null;
        private string _PBSCode = null;
        private string _PBSType = null;
        private string _remark = null;
        private string _State = null;
        private string _Title = null;
        private string _Type = null;
        private string _Unit = null;

        private void _GetBiddingDtlByCode()
        {
            EntityData biddingDtlByCode = this.GetBiddingDtlByCode(this._BiddingDtlCode);
            this._BiddingDtlCode = biddingDtlByCode.GetString("BiddingDtlCode");
            this._Title = biddingDtlByCode.GetString("Title");
            this._Type = biddingDtlByCode.GetString("Type");
            this._Unit = biddingDtlByCode.GetString("Unit");
            this._Money = biddingDtlByCode.GetDecimal("Money").ToString();
            this._remark = biddingDtlByCode.GetString("remark");
            this._BiddingCode = biddingDtlByCode.GetString("BiddingCode");
            this._State = biddingDtlByCode.GetString("State");
            this._flag = biddingDtlByCode.GetString("flag");
            this._CostCode = biddingDtlByCode.GetString("CostCode");
            this._CostBudgetSetCode = biddingDtlByCode.GetString("CostBudgetSetCode");
            this._PBSCode = biddingDtlByCode.GetString("PBSCode");
            this._PBSType = biddingDtlByCode.GetString("PBSType");
            this._OtherMoney = biddingDtlByCode.GetDecimal("OtherMoney").ToString();
            this._OtherMoneyType = biddingDtlByCode.GetString("OtherMoneyType");
            this._OtherMoneyRate = biddingDtlByCode.GetString("OtherMoneyRate");
            biddingDtlByCode.Dispose();
        }

        private EntityData _GetBiddingDtls()
        {
            EntityData entitydata = new EntityData("BiddingDtl");
            BiddingDtlStrategyBuilder builder = new BiddingDtlStrategyBuilder();
            if (this._BiddingDtlCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.BiddingDtlCode, this._BiddingDtlCode));
            }
            if (this._Title != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.Title, this._Title));
            }
            if (this._Type != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.Type, this._Type));
            }
            if (this._Unit != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.Unit, this._Unit));
            }
            if (this._Money != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.Money, this._Money));
            }
            if (this._remark != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.remark, this._remark));
            }
            if (this._BiddingCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.BiddingCode, this._BiddingCode));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.State, this._State));
            }
            if (this._flag != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.flag, this._flag));
            }
            if (this._CostCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.CostCode, this._CostCode));
            }
            if (this._CostBudgetSetCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.CostBudgetSetCode, this._CostBudgetSetCode));
            }
            if (this._PBSCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.PBSCode, this._PBSCode));
            }
            if (this._PBSType != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.PBSType, this._PBSType));
            }
            if (this._OtherMoney != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.OtherMoney, this._OtherMoney));
            }
            if (this._OtherMoneyType != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.OtherMoneyType, this._OtherMoneyType));
            }
            if (this._OtherMoneyRate != null)
            {
                builder.AddStrategy(new Strategy(BiddingDtlStrategyName.OtherMoneyRate, this._OtherMoneyRate));
            }
            string sqlString = builder.BuildMainQueryString() + " order by BiddingDtlCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "BiddingDtl";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public void BiddingDtlAdd()
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingDtl(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingDtl";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingDtlDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBiddingDtl(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingDtl";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void BiddingDtlSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingDtl(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingDtl";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingDtlUpdate()
        {
            if (this._BiddingDtlCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingDtl(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingDtl";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData biddingDtlByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._BiddingDtlCode == "")
            {
                flag = true;
                biddingDtlByCode = this.GetBiddingDtlByCode("");
                this._BiddingDtlCode = SystemManageDAO.GetNewSysCode("BiddingDtl");
                newRecord = biddingDtlByCode.GetNewRecord();
            }
            else
            {
                biddingDtlByCode = this.GetBiddingDtlByCode(this._BiddingDtlCode);
                if (biddingDtlByCode.HasRecord())
                {
                    newRecord = biddingDtlByCode.CurrentRow;
                }
                else
                {
                    flag = true;
                    newRecord = biddingDtlByCode.GetNewRecord();
                }
            }
            if (this._BiddingDtlCode != null)
            {
                newRecord["BiddingDtlCode"] = this._BiddingDtlCode;
            }
            if (this._Title != null)
            {
                newRecord["Title"] = this._Title;
            }
            if (this._Type != null)
            {
                newRecord["Type"] = this._Type;
            }
            if (this._Unit != null)
            {
                newRecord["Unit"] = this._Unit;
            }
            if (this._Money != null)
            {
                newRecord["Money"] = this._Money;
            }
            if (this._remark != null)
            {
                newRecord["remark"] = this._remark;
            }
            if (this._BiddingCode != null)
            {
                newRecord["BiddingCode"] = this._BiddingCode;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (this._flag != null)
            {
                newRecord["flag"] = this._flag;
            }
            if (this._CostCode != null)
            {
                newRecord["CostCode"] = this._CostCode;
            }
            if (this._CostBudgetSetCode != null)
            {
                newRecord["CostBudgetSetCode"] = this._CostBudgetSetCode;
            }
            if (this._PBSCode != null)
            {
                newRecord["PBSCode"] = this._PBSCode;
            }
            if (this._PBSType != null)
            {
                newRecord["PBSType"] = this._PBSType;
            }
            if (this._OtherMoney != null)
            {
                newRecord["OtherMoney"] = this._OtherMoney;
            }
            if (this._OtherMoneyType != null)
            {
                newRecord["OtherMoneyType"] = this._OtherMoneyType;
            }
            if (this._OtherMoneyRate != null)
            {
                newRecord["OtherMoneyRate"] = this._OtherMoneyRate;
            }
            if (flag)
            {
                biddingDtlByCode.AddNewRecord(newRecord);
            }
            return biddingDtlByCode;
        }

        private void DeleteBiddingDtl(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingDtl";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllBiddingDtl()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingDtl";
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

        private EntityData GetBiddingDtlByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingDtl";
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

        public EntityData GetBiddingDtlEntity()
        {
            return this._GetBiddingDtls();
        }

        public static string GetBiddingDtlNameByCode(string code)
        {
            string text2;
            try
            {
                EntityData data;
                string text = "";
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                if (data.HasRecord())
                {
                    text = data.CurrentRow["Title"].ToString();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public DataTable GetBiddingDtls()
        {
            return this._GetBiddingDtls().CurrentTable;
        }

        private void InsertBiddingDtl(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingDtl";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllBiddingDtl(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
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
                    this.dao.EntityName = "BiddingDtl";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public void SubmitDtlEntity(EntityData eitity)
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingDtl(eitity);
            }
            else
            {
                this.dao.EntityName = "BiddingDtl";
                this.dao.SubmitEntity(eitity);
            }
        }

        private void UpdateBiddingDtl(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtl"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingDtl";
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
                if ((this._BiddingCode == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._BiddingCode;
            }
            set
            {
                this._BiddingCode = value;
            }
        }

        public string BiddingDtlCode
        {
            get
            {
                return this._BiddingDtlCode;
            }
            set
            {
                this._BiddingDtlCode = value;
            }
        }

        public string CostBudgetSetCode
        {
            get
            {
                if ((this._CostBudgetSetCode == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._CostBudgetSetCode;
            }
            set
            {
                this._CostBudgetSetCode = value;
            }
        }

        public string CostCode
        {
            get
            {
                if ((this._CostCode == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
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

        public string flag
        {
            get
            {
                if ((this._flag == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._flag;
            }
            set
            {
                this._flag = value;
            }
        }

        public string Money
        {
            get
            {
                if ((this._Money == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._Money;
            }
            set
            {
                this._Money = value;
            }
        }

        public string OtherMoney
        {
            get
            {
                if ((this._OtherMoney == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._OtherMoney;
            }
            set
            {
                this._OtherMoney = value;
            }
        }

        public string OtherMoneyRate
        {
            get
            {
                if ((this._OtherMoneyRate == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._OtherMoneyRate;
            }
            set
            {
                this._OtherMoneyRate = value;
            }
        }

        public string OtherMoneyType
        {
            get
            {
                if ((this._OtherMoneyType == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._OtherMoneyType;
            }
            set
            {
                this._OtherMoneyType = value;
            }
        }

        public string PBSCode
        {
            get
            {
                if ((this._PBSCode == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._PBSCode;
            }
            set
            {
                this._PBSCode = value;
            }
        }

        public string PBSType
        {
            get
            {
                if ((this._PBSType == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._PBSType;
            }
            set
            {
                this._PBSType = value;
            }
        }

        public string remark
        {
            get
            {
                if ((this._remark == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string Title
        {
            get
            {
                if ((this._Title == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        public string Type
        {
            get
            {
                if ((this._Type == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
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
                if ((this._Unit == null) && (this._BiddingDtlCode != null))
                {
                    this._GetBiddingDtlByCode();
                }
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }
    }
}

