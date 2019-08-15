namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingLog
    {
        private string _BiddingCode = null;
        private string _BiddingLogCode = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private decimal _FormerMoney;
        private string _State = null;
        private decimal _TeamMoney;
        private string _Type = null;
        private string _UpdateTime = null;
        private string _UserCode = null;

        private void _GetBiddingLogByCode()
        {
            try
            {
                EntityData biddingLogByCode = GetBiddingLogByCode(this._BiddingLogCode);
                this._BiddingLogCode = biddingLogByCode.GetString("BiddingLogCode");
                this._BiddingCode = biddingLogByCode.GetString("BiddingCode");
                this._Type = biddingLogByCode.GetString("Type");
                this._UserCode = biddingLogByCode.GetString("UserCode");
                this._UpdateTime = biddingLogByCode.GetDateTime("UpdateTime").ToString();
                this._FormerMoney = biddingLogByCode.GetDecimal("FormerMoney");
                this._TeamMoney = biddingLogByCode.GetDecimal("TeamMoney");
                this._State = biddingLogByCode.GetString("State");
                this._Flag = biddingLogByCode.GetString("Flag");
                biddingLogByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetBiddingLogs()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BiddingLog");
                BiddingLogStrategyBuilder builder = new BiddingLogStrategyBuilder();
                if (this._BiddingLogCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingLogStrategyName.BiddingLogCode, this._BiddingLogCode));
                }
                if (this._BiddingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingLogStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._Type != null)
                {
                    builder.AddStrategy(new Strategy(BiddingLogStrategyName.Type, this._Type));
                }
                if (this._UserCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingLogStrategyName.UserCode, this._UserCode));
                }
                if (this._UpdateTime != null)
                {
                    builder.AddStrategy(new Strategy(BiddingLogStrategyName.UpdateTime, this._UpdateTime));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(BiddingLogStrategyName.State, this._State));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(BiddingLogStrategyName.Flag, this._Flag));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingLogCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingLog"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingLog";
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

        public void BiddingLogAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingLog(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingLog";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingLogDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingLog(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingLog";
                    this.dao.DeleteEntity(this.BuildData());
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
                EntityData biddingLogByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingLogCode == "")
                {
                    flag = true;
                    biddingLogByCode = GetBiddingLogByCode("");
                    this._BiddingLogCode = SystemManageDAO.GetNewSysCode("BiddingLog");
                    newRecord = biddingLogByCode.GetNewRecord();
                }
                else
                {
                    biddingLogByCode = GetBiddingLogByCode(this._BiddingLogCode);
                    if (biddingLogByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingLogByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingLogByCode.CurrentRow;
                    }
                }
                if (this._BiddingLogCode != null)
                {
                    newRecord["BiddingLogCode"] = this._BiddingLogCode;
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
                }
                if (this._Type != null)
                {
                    newRecord["Type"] = this._Type;
                }
                if (this._UserCode != null)
                {
                    newRecord["UserCode"] = this._UserCode;
                }
                if (this._UpdateTime != null)
                {
                    newRecord["UpdateTime"] = this._UpdateTime;
                }
                bool flag2 = 1 == 0;
                newRecord["FormerMoney"] = this._FormerMoney;
                flag2 = 1 == 0;
                newRecord["TeamMoney"] = this._TeamMoney;
                if (this._State != null)
                {
                    newRecord["state"] = this._State;
                }
                if (this._Flag != null)
                {
                    newRecord["Flag"] = this._Flag;
                }
                if (flag)
                {
                    biddingLogByCode.AddNewRecord(newRecord);
                }
                data2 = biddingLogByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingLog(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingLog"))
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

        public static EntityData GetAllBiddingLog()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingLog"))
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

        public static EntityData GetBiddingLogByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingLog"))
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

        public static EntityData GetBiddingLogByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetBiddingLogs()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetBiddingLogs().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertBiddingLog(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingLog"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingLog(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingLog"))
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

        public static void UpdateBiddingLog(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingLog"))
                {
                    ydao.UpdateEntity(entity);
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
                if ((this._BiddingCode == null) && (this._BiddingLogCode != null))
                {
                    this._GetBiddingLogByCode();
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

        public string BiddingLogCode
        {
            get
            {
                return this._BiddingLogCode;
            }
            set
            {
                if (this._BiddingLogCode != value)
                {
                    this._BiddingLogCode = value;
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

        public decimal TeamMoney
        {
            get
            {
                return this._TeamMoney;
            }
            set
            {
                if (this._TeamMoney != value)
                {
                    this._TeamMoney = value;
                }
            }
        }

        public string Type
        {
            get
            {
                if ((this._Type == null) && (this._BiddingLogCode != null))
                {
                    this._GetBiddingLogByCode();
                }
                return this._Type;
            }
            set
            {
                if (this._Type != value)
                {
                    this._Type = value;
                }
            }
        }

        public string UpdateTime
        {
            get
            {
                if ((this._UpdateTime == null) && (this._BiddingLogCode != null))
                {
                    this._GetBiddingLogByCode();
                }
                return this._UpdateTime;
            }
            set
            {
                if (this._UpdateTime != value)
                {
                    this._UpdateTime = value;
                }
            }
        }

        public string UserCode
        {
            get
            {
                if ((this._UserCode == null) && (this._BiddingLogCode != null))
                {
                    this._GetBiddingLogByCode();
                }
                return this._UserCode;
            }
            set
            {
                if (this._UserCode != value)
                {
                    this._UserCode = value;
                }
            }
        }
    }
}

