namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingAuditing
    {
        private string _AppentDate = null;
        private string _AppentUser = null;
        private string _BiddingAuditingCode = null;
        private string _BiddingCode = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private int _GradePoint;
        private string _Preparation1 = null;
        private string _Preparation2 = null;
        private string _Remark = null;
        private string _State = null;

        private void _GetBiddingAuditingByCode()
        {
            try
            {
                EntityData biddingAuditingByCode = GetBiddingAuditingByCode(this._BiddingAuditingCode);
                this._BiddingAuditingCode = biddingAuditingByCode.GetString("BiddingAuditingCode");
                this._BiddingCode = biddingAuditingByCode.GetString("BiddingCode");
                this._AppentDate = biddingAuditingByCode.GetDateTime("AppentDate");
                this._AppentUser = biddingAuditingByCode.GetString("AppentUser");
                this._Remark = biddingAuditingByCode.GetString("Remark");
                this._State = biddingAuditingByCode.GetString("State");
                this._Flag = biddingAuditingByCode.GetString("Flag");
                this._Preparation1 = biddingAuditingByCode.GetString("Preparation1");
                this._Preparation2 = biddingAuditingByCode.GetString("Preparation2");
                biddingAuditingByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetBiddingAuditings()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BiddingAuditing");
                BiddingAuditingStrategyBuilder builder = new BiddingAuditingStrategyBuilder();
                if (this._BiddingAuditingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingAuditingStrategyName.BiddingAuditingCode, this._BiddingAuditingCode));
                }
                if (this._BiddingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingAuditingStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._AppentDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingAuditingStrategyName.AppentDate, this._AppentDate));
                }
                if (this._AppentUser != null)
                {
                    builder.AddStrategy(new Strategy(BiddingAuditingStrategyName.AppentUser, this._AppentUser));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(BiddingAuditingStrategyName.State, this._State));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(BiddingAuditingStrategyName.Flag, this._Flag));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingAuditingCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingAuditing"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingAuditing";
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

        public void BiddingAuditingAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingAuditing(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingAuditing";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingAuditingDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingAuditing(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingAuditing";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingAuditingUpdate()
        {
            if (this._BiddingAuditingCode != null)
            {
                if (this._dao == null)
                {
                    SubmitAllBiddingAuditing(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingAuditing";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData biddingAuditingByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingAuditingCode == "")
                {
                    flag = true;
                    biddingAuditingByCode = GetBiddingAuditingByCode("");
                    this._BiddingAuditingCode = SystemManageDAO.GetNewSysCode("BiddingAuditing");
                    newRecord = biddingAuditingByCode.GetNewRecord();
                }
                else
                {
                    biddingAuditingByCode = GetBiddingAuditingByCode(this._BiddingAuditingCode);
                    if (biddingAuditingByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingAuditingByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingAuditingByCode.CurrentRow;
                    }
                }
                if (this._BiddingAuditingCode != null)
                {
                    newRecord["BiddingAuditingCode"] = this._BiddingAuditingCode;
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
                }
                if (this._AppentDate != null)
                {
                    newRecord["AppentDate"] = this._AppentDate;
                }
                if (this._AppentUser != null)
                {
                    newRecord["AppentUser"] = this._AppentUser;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
                }
                if (this._State != null)
                {
                    newRecord["State"] = this._State;
                }
                if (this._Flag != null)
                {
                    newRecord["Flag"] = this._Flag;
                }
                if (this._Preparation1 != null)
                {
                    newRecord["Preparation1"] = this._Preparation1;
                }
                if (this._Preparation2 != null)
                {
                    newRecord["Preparation2"] = this._Preparation2;
                }
                if (flag)
                {
                    biddingAuditingByCode.AddNewRecord(newRecord);
                }
                data2 = biddingAuditingByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingAuditing(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingAuditing"))
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

        public static EntityData GetAllBiddingAuditing()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingAuditing"))
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

        public static EntityData GetBiddingAuditingByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingAuditing"))
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

        public static EntityData GetBiddingAuditingByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetBiddingAuditings()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetBiddingAuditings().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertBiddingAuditing(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingAuditing"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingAuditing(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingAuditing"))
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

        public static void UpdateBiddingAuditing(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingAuditing"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string AppentDate
        {
            get
            {
                if ((this._AppentDate == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
                }
                return this._AppentDate;
            }
            set
            {
                if (this._AppentDate != value)
                {
                    this._AppentDate = value;
                }
            }
        }

        public string AppentUser
        {
            get
            {
                if ((this._AppentUser == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
                }
                return this._AppentUser;
            }
            set
            {
                if (this._AppentUser != value)
                {
                    this._AppentUser = value;
                }
            }
        }

        public string BiddingAuditingCode
        {
            get
            {
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
                if ((this._BiddingCode == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
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
                if ((this._Flag == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
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

        public string Preparation1
        {
            get
            {
                if ((this._Preparation1 == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
                }
                return this._Preparation1;
            }
            set
            {
                if (this._Preparation1 != value)
                {
                    this._Preparation1 = value;
                }
            }
        }

        public string Preparation2
        {
            get
            {
                if ((this._Preparation2 == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
                }
                return this._Preparation2;
            }
            set
            {
                if (this._Preparation2 != value)
                {
                    this._Preparation2 = value;
                }
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
                }
                return this._Remark;
            }
            set
            {
                if (this._Remark != value)
                {
                    this._Remark = value;
                }
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingAuditingCode != null))
                {
                    this._GetBiddingAuditingByCode();
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

