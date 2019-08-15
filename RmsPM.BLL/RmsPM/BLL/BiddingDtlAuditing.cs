namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingDtlAuditing
    {
        private string _BiddingAuditingCode = null;
        private string _BiddingCode = null;
        private string _BiddingDtlAuditingCode = null;
        private string _BiddingDtlCode = null;
        private decimal _CurrentMoney;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private decimal _FormerMoney;
        private int _GradePoint;
        private string _State = null;

        private void _GetBiddingDtlAuditingByCode()
        {
            try
            {
                EntityData biddingDtlAuditingByCode = GetBiddingDtlAuditingByCode(this._BiddingDtlAuditingCode);
                this._BiddingDtlAuditingCode = biddingDtlAuditingByCode.GetString("BiddingDtlAuditingCode");
                this._BiddingCode = biddingDtlAuditingByCode.GetString("BiddingCode");
                this._BiddingAuditingCode = biddingDtlAuditingByCode.GetString("BiddingAuditingCode");
                this._BiddingDtlCode = biddingDtlAuditingByCode.GetString("BiddingDtlCode");
                this._State = biddingDtlAuditingByCode.GetString("State");
                this._Flag = biddingDtlAuditingByCode.GetString("Flag");
                this._FormerMoney = biddingDtlAuditingByCode.GetDecimal("FormerMoney");
                this._CurrentMoney = biddingDtlAuditingByCode.GetDecimal("CurrentMoney");
                biddingDtlAuditingByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetBiddingDtlAuditings()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BiddingDtlAuditing");
                BiddingDtlAuditingStrategyBuilder builder = new BiddingDtlAuditingStrategyBuilder();
                if (this._BiddingDtlAuditingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.BiddingDtlAuditingCode, this._BiddingDtlAuditingCode));
                }
                if (this._BiddingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._BiddingAuditingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.BiddingAuditingCode, this._BiddingAuditingCode));
                }
                if (this._BiddingDtlCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.BiddingDtlCode, this._BiddingDtlCode));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.State, this._State));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.Flag, this._Flag));
                }
                if (this._FormerMoney != 0M)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.FormerMoney, this._FormerMoney.ToString()));
                }
                if (this._CurrentMoney != 0M)
                {
                    builder.AddStrategy(new Strategy(BiddingDtlAuditingStrategyName.CurrentMoney, this._CurrentMoney.ToString()));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingDtlAuditingCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtlAuditing"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingDtlAuditing";
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

        public void BiddingDtlAuditingAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingDtlAuditing(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingDtlAuditing";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingDtlAuditingDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingDtlAuditing(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingDtlAuditing";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingDtlAuditingUpdate()
        {
            if (this._BiddingDtlAuditingCode != null)
            {
                if (this._dao == null)
                {
                    SubmitAllBiddingDtlAuditing(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingDtlAuditing";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData biddingDtlAuditingByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingDtlAuditingCode == "")
                {
                    flag = true;
                    biddingDtlAuditingByCode = GetBiddingDtlAuditingByCode("");
                    this._BiddingDtlAuditingCode = SystemManageDAO.GetNewSysCode("BiddingDtlAuditing");
                    newRecord = biddingDtlAuditingByCode.GetNewRecord();
                }
                else
                {
                    biddingDtlAuditingByCode = GetBiddingDtlAuditingByCode(this._BiddingDtlAuditingCode);
                    if (biddingDtlAuditingByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingDtlAuditingByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingDtlAuditingByCode.CurrentRow;
                    }
                }
                if (this._BiddingDtlAuditingCode != null)
                {
                    newRecord["BiddingDtlAuditingCode"] = this._BiddingDtlAuditingCode;
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
                }
                if (this._BiddingAuditingCode != null)
                {
                    newRecord["BiddingAuditingCode"] = this._BiddingAuditingCode;
                }
                if (this._BiddingDtlCode != null)
                {
                    newRecord["BiddingDtlCode"] = this._BiddingDtlCode;
                }
                if (this._State != null)
                {
                    newRecord["State"] = this._State;
                }
                if (this._Flag != null)
                {
                    newRecord["Flag"] = this._Flag;
                }
                bool flag2 = 1 == 0;
                newRecord["FormerMoney"] = this._FormerMoney;
                flag2 = 1 == 0;
                newRecord["CurrentMoney"] = this._CurrentMoney;
                if (flag)
                {
                    biddingDtlAuditingByCode.AddNewRecord(newRecord);
                }
                data2 = biddingDtlAuditingByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingDtlAuditing(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtlAuditing"))
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

        public static EntityData GetAllBiddingDtlAuditing()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtlAuditing"))
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

        public static EntityData GetBiddingDtlAuditingByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtlAuditing"))
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

        public static EntityData GetBiddingDtlAuditingByCode(string code, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetBiddingDtlAuditings()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetBiddingDtlAuditings().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertBiddingDtlAuditing(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtlAuditing"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingDtlAuditing(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtlAuditing"))
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

        public static void UpdateBiddingDtlAuditing(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingDtlAuditing"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BiddingAuditingCode
        {
            get
            {
                if ((this._BiddingAuditingCode == null) && (this._BiddingDtlAuditingCode != null))
                {
                    this._GetBiddingDtlAuditingByCode();
                }
                return this._BiddingAuditingCode;
            }
            set
            {
                if (this._BiddingAuditingCode != value)
                {
                    this._BiddingAuditingCode = value;
                }
            }
        }

        public string BiddingCode
        {
            get
            {
                if ((this._BiddingCode == null) && (this._BiddingDtlAuditingCode != null))
                {
                    this._GetBiddingDtlAuditingByCode();
                }
                return this._BiddingCode;
            }
            set
            {
                if (this._BiddingCode != value)
                {
                    this._BiddingCode = value;
                }
            }
        }

        public string BiddingDtlAuditingCode
        {
            get
            {
                return this._BiddingDtlAuditingCode;
            }
            set
            {
                if (this._BiddingDtlAuditingCode != value)
                {
                    this._BiddingDtlAuditingCode = value;
                }
            }
        }

        public string BiddingDtlCode
        {
            get
            {
                if ((this._BiddingDtlCode == null) && (this._BiddingDtlAuditingCode != null))
                {
                    this._GetBiddingDtlAuditingByCode();
                }
                return this._BiddingDtlCode;
            }
            set
            {
                if (this._BiddingDtlCode != value)
                {
                    this._BiddingDtlCode = value;
                }
            }
        }

        public decimal CurrentMoney
        {
            get
            {
                return this._CurrentMoney;
            }
            set
            {
                if (this._CurrentMoney != value)
                {
                    this._CurrentMoney = value;
                }
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

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._BiddingDtlAuditingCode != null))
                {
                    this._GetBiddingDtlAuditingByCode();
                }
                return this._Flag;
            }
            set
            {
                if (this._Flag != value)
                {
                    this._Flag = value;
                }
            }
        }

        public decimal FormerMoney
        {
            get
            {
                return this._FormerMoney;
            }
            set
            {
                if (this._FormerMoney != value)
                {
                    this._FormerMoney = value;
                }
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingDtlAuditingCode != null))
                {
                    this._GetBiddingDtlAuditingByCode();
                }
                return this._State;
            }
            set
            {
                if (this._State != value)
                {
                    this._State = value;
                }
            }
        }
    }
}

