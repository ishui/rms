namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingGradeMessage
    {
        private string _ApplicationCode = null;
        private string _BiddingGradeMessageCode = null;
        private string _BiddingGradeTypeCode = null;
        private StandardEntityDAO _dao;
        private string _MainDefineCode = null;
        private string _ProjectManage = null;
        private string _State = null;

        private void _GetBiddingGradeMessageByCode()
        {
            try
            {
                EntityData biddingGradeMessageByCode = GetBiddingGradeMessageByCode(this._BiddingGradeMessageCode);
                this._BiddingGradeMessageCode = biddingGradeMessageByCode.GetString("BiddingGradeMessageCode");
                this._ApplicationCode = biddingGradeMessageByCode.GetString("ApplicationCode");
                this._BiddingGradeTypeCode = biddingGradeMessageByCode.GetString("BiddingGradeTypeCode");
                this._MainDefineCode = biddingGradeMessageByCode.GetString("MainDefineCode");
                this._ProjectManage = biddingGradeMessageByCode.GetString("ProjectManage");
                this._State = biddingGradeMessageByCode.GetString("State");
                biddingGradeMessageByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetBiddings()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BiddingGradeMessage");
                BiddingGradeMessageStrategyBuilder builder = new BiddingGradeMessageStrategyBuilder();
                if (this._BiddingGradeMessageCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeMessageStrategyName.BiddingGradeMessageCode, this._BiddingGradeMessageCode));
                }
                if (this._ApplicationCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeMessageStrategyName.ApplicationCode, this._ApplicationCode));
                }
                if (this._BiddingGradeTypeCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeMessageStrategyName.BiddingGradeTypeCode, this._BiddingGradeTypeCode));
                }
                if (this._MainDefineCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeMessageStrategyName.MainDefineCode, this._MainDefineCode));
                }
                if (this._ProjectManage != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeMessageStrategyName.ProjectManage, this._ProjectManage));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeMessageStrategyName.State, this._State));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingGradeMessageCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeMessage"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingGradeMessage";
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

        public void BiddingGradeMessageAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingGradeMessage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingGradeMessage";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingGradeMessageDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingGradeMessage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingGradeMessage";
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
                EntityData biddingGradeMessageByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingGradeMessageCode == "")
                {
                    flag = true;
                    biddingGradeMessageByCode = GetBiddingGradeMessageByCode("");
                    this._BiddingGradeMessageCode = SystemManageDAO.GetNewSysCode("BiddingGradeMessage");
                    newRecord = biddingGradeMessageByCode.GetNewRecord();
                }
                else
                {
                    biddingGradeMessageByCode = GetBiddingGradeMessageByCode(this._BiddingGradeMessageCode);
                    if (biddingGradeMessageByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingGradeMessageByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingGradeMessageByCode.CurrentRow;
                    }
                }
                if (this._BiddingGradeMessageCode != null)
                {
                    newRecord["BiddingGradeMessageCode"] = this._BiddingGradeMessageCode;
                }
                if (this._ApplicationCode != null)
                {
                    newRecord["ApplicationCode"] = this._ApplicationCode;
                }
                if (this._BiddingGradeTypeCode != null)
                {
                    newRecord["BiddingGradeTypeCode"] = this._BiddingGradeTypeCode;
                }
                if (this._MainDefineCode != null)
                {
                    newRecord["MainDefineCode"] = this._MainDefineCode;
                }
                if (this._ProjectManage != null)
                {
                    newRecord["ProjectManage"] = this._ProjectManage;
                }
                if (this._State != null)
                {
                    newRecord["State"] = this._State;
                }
                if (flag)
                {
                    biddingGradeMessageByCode.AddNewRecord(newRecord);
                }
                data2 = biddingGradeMessageByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingGradeMessage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeMessage"))
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

        public static EntityData GetAllBiddingGradeMessage()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeMessage"))
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

        public static EntityData GetBiddingGradeMessageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeMessage"))
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

        public static EntityData GetBiddingGradeMessageByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetBiddings()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetBiddings().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertBiddingGradeMessage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeMessage"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingGradeMessage(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeMessage"))
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

        public static void UpdateBiddingGradeMessage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeMessage"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string ApplicationCode
        {
            get
            {
                return this._ApplicationCode;
            }
            set
            {
                if (this._ApplicationCode != value)
                {
                    this._ApplicationCode = value;
                }
            }
        }

        public string BiddingGradeMessageCode
        {
            get
            {
                return this._BiddingGradeMessageCode;
            }
            set
            {
                if (this._BiddingGradeMessageCode != value)
                {
                    this._BiddingGradeMessageCode = value;
                }
            }
        }

        public string BiddingGradeTypeCode
        {
            get
            {
                return this._BiddingGradeTypeCode;
            }
            set
            {
                if (this._BiddingGradeTypeCode != value)
                {
                    this._BiddingGradeTypeCode = value;
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

        public string MainDefineCode
        {
            get
            {
                return this._MainDefineCode;
            }
            set
            {
                if (this._MainDefineCode != value)
                {
                    this._MainDefineCode = value;
                }
            }
        }

        public string ProjectManage
        {
            get
            {
                return this._ProjectManage;
            }
            set
            {
                if (this._ProjectManage != value)
                {
                    this._ProjectManage = value;
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
    }
}

