namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class Bidding
    {
        private string _Accessory = null;
        private string _ArrangedDate = null;
        public ArrayList _ArrangedDateEx = null;
        private string _BiddingAddress = null;
        private string _BiddingCode = null;
        private string _BiddingRemark1 = null;
        private string _BiddingRemark2 = null;
        private string _BiddingType = null;
        private string _ConfirmDate = null;
        public ArrayList _ConfirmDateEx = null;
        private string _Content = null;
        private string _ContentMeeting = null;
        private string _CostBudgetSetCode = null;
        private string _CostCode = null;
        private StandardEntityDAO _dao;
        private string _EmitDate = null;
        private string _Money = null;
        private string _ObligateMoney = null;
        private string _PBSCode = null;
        private string _PBSType = null;
        private string _PrejudicationDate = null;
        private string _ProjectCode = null;
        private string _Remark = null;
        private string _ReturnDate = null;
        private string _StandardDate = null;
        private string _State = null;
        private string _Status = null;
        private string _Title = null;
        private string _Type = null;

        private void _GetBiddingByCode()
        {
            try
            {
                EntityData biddingByCode = this.GetBiddingByCode(this._BiddingCode);
                this._BiddingCode = biddingByCode.GetString("BiddingCode");
                this._Type = biddingByCode.GetString("Type");
                this._Title = biddingByCode.GetString("Title");
                this._Content = biddingByCode.GetString("Content");
                this._Accessory = biddingByCode.GetString("Accessory");
                this._ArrangedDate = biddingByCode.GetDateTime("ArrangedDate", "yyyy-MM-dd HH:mm");
                this._StandardDate = biddingByCode.GetDateTime("StandardDate", "yyyy-MM-dd HH:mm");
                this._PrejudicationDate = biddingByCode.GetDateTime("PrejudicationDate", "yyyy-MM-dd HH:mm");
                this._EmitDate = biddingByCode.GetDateTime("EmitDate", "yyyy-MM-dd HH:mm");
                this._ReturnDate = biddingByCode.GetDateTime("ReturnDate", "yyyy-MM-dd HH:mm");
                this._ConfirmDate = biddingByCode.GetDateTime("ConfirmDate", "yyyy-MM-dd HH:mm");
                this._State = biddingByCode.GetString("State");
                this._Remark = biddingByCode.GetString("Remark");
                this._ProjectCode = biddingByCode.GetString("ProjectCode");
                this._CostCode = biddingByCode.GetString("CostCode");
                this._CostBudgetSetCode = biddingByCode.GetString("CostBudgetSetCode");
                this._PBSType = biddingByCode.GetString("PBSType");
                this._PBSCode = biddingByCode.GetString("PBSCode");
                this._Money = biddingByCode.GetDecimal("Money").ToString();
                this._ObligateMoney = biddingByCode.GetDecimal("ObligateMoney").ToString();
                this._BiddingRemark1 = biddingByCode.GetString("BiddingRemark1").ToString();
                this._BiddingRemark2 = biddingByCode.GetString("BiddingRemark2").ToString();
                this._ContentMeeting = biddingByCode.GetString("ContentMeeting").ToString();
                this._BiddingType = biddingByCode.GetString("BiddingType").ToString();
                this._BiddingAddress = biddingByCode.GetString("BiddingAddress").ToString();
                this._Status = biddingByCode.GetString("Status").ToString();
                biddingByCode.Dispose();
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
                EntityData entitydata = new EntityData("Bidding");
                BiddingStrategyBuilder builder = new BiddingStrategyBuilder();
                if (this._BiddingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._Type != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.Type, this._Type));
                }
                if (this._Title != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.Title, "%" + this._Title + "%"));
                }
                if (this._Content != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.Content, this._Content));
                }
                if (this._Accessory != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.Accessory, this._Accessory));
                }
                if (this._ArrangedDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.ArrangedDate, this._ArrangedDate));
                }
                if (this._PrejudicationDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.PrejudicationDate, this._PrejudicationDate));
                }
                if (this._EmitDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.EmitDate, this._EmitDate));
                }
                if (this._ReturnDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.ReturnDate, this._ReturnDate));
                }
                if (this._ConfirmDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.ConfirmDate, this._ConfirmDate));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.State, this._State));
                }
                if (this._Remark != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.Remark, this._Remark));
                }
                if (this._ProjectCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.ProjectCode, this._ProjectCode));
                }
                if (this._CostCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.CostCode, this._CostCode));
                }
                if (this._CostBudgetSetCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.CostBudgetSetCode, this._CostBudgetSetCode));
                }
                if (this._PBSType != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.PBSType, this._PBSType));
                }
                if (this._PBSCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.PBSCode, this._PBSCode));
                }
                if (this._Money != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.Money, this._Money));
                }
                if (this._ObligateMoney != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.ObligateMoney, this._ObligateMoney));
                }
                if (this._BiddingRemark1 != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.BiddingRemark1, this._BiddingRemark1));
                }
                if (this._BiddingRemark2 != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.BiddingRemark2, this._BiddingRemark2));
                }
                if (this._ArrangedDateEx != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.ArrangedDateEx, this._ArrangedDateEx));
                }
                if (this._ConfirmDateEx != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.ConfirmDateEx, this._ConfirmDateEx));
                }
                if (this._BiddingType != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.BiddingType, this._BiddingType));
                }
                if (this._BiddingAddress != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.BiddingAddress, this._BiddingAddress));
                }
                if (this._Status != null)
                {
                    builder.AddStrategy(new Strategy(BiddingStrategyName.Status, this._Status));
                }
                string sqlString = builder.BuildMainQueryString() + " order by EmitDate desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "Bidding";
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

        public void BiddingAdd()
        {
            try
            {
                if (this._BiddingCode == null)
                {
                    if (this._dao == null)
                    {
                        this.SubmitAllBidding(this.BuildData());
                    }
                    else
                    {
                        this.dao.EntityName = "Bidding";
                        this.dao.SubmitEntity(this.BuildData());
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    this.DeleteBidding(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Bidding";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable BiddingProcess(string BiddingCode)
        {
            DataTable table2;
            QueryAgent agent = new QueryAgent();
            try
            {
                string queryString = "sp_BiddingProcess";
                string[] parameterNames = new string[] { "@BiddingCode" };
                object[] values = new object[] { BiddingCode };
                DataTable table = agent.ExecSPForDataSet(queryString, parameterNames, values).Tables[0];
                table2 = table;
            }
            finally
            {
                agent.Dispose();
            }
            return table2;
        }

        public void BiddingSubmit()
        {
            try
            {
                if (this._dao == null)
                {
                    this.SubmitAllBidding(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Bidding";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingUpdate()
        {
            try
            {
                if (this._BiddingCode != null)
                {
                    if (this._dao == null)
                    {
                        this.SubmitAllBidding(this.BuildData());
                    }
                    else
                    {
                        this.dao.EntityName = "Bidding";
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
                EntityData biddingByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingCode == "")
                {
                    flag = true;
                    biddingByCode = this.GetBiddingByCode("");
                    this._BiddingCode = SystemManageDAO.GetNewSysCode("Bidding");
                    newRecord = biddingByCode.GetNewRecord();
                }
                else
                {
                    biddingByCode = this.GetBiddingByCode(this._BiddingCode);
                    if (biddingByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingByCode.CurrentRow;
                    }
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
                }
                if (this._Type != null)
                {
                    newRecord["Type"] = this._Type;
                }
                if (this._Title != null)
                {
                    newRecord["Title"] = this._Title;
                }
                if (this._Content != null)
                {
                    newRecord["Content"] = this._Content;
                }
                if (this._Accessory != null)
                {
                    newRecord["Accessory"] = this._Accessory;
                }
                if (this._ArrangedDate != null)
                {
                    newRecord["ArrangedDate"] = this._ArrangedDate;
                }
                if (this._StandardDate != null)
                {
                    newRecord["StandardDate"] = this._StandardDate;
                }
                if (this._PrejudicationDate != null)
                {
                    newRecord["PrejudicationDate"] = this._PrejudicationDate;
                }
                if (this._EmitDate != null)
                {
                    newRecord["EmitDate"] = this._EmitDate;
                }
                if (this._ReturnDate != null)
                {
                    newRecord["ReturnDate"] = this._ReturnDate;
                }
                if (this._ConfirmDate != null)
                {
                    newRecord["ConfirmDate"] = this._ConfirmDate;
                }
                if (this._State != null)
                {
                    newRecord["State"] = this._State;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
                }
                if (this._ProjectCode != null)
                {
                    newRecord["ProjectCode"] = this._ProjectCode;
                }
                if (this._CostCode != null)
                {
                    newRecord["CostCode"] = this._CostCode;
                }
                if (this._CostBudgetSetCode != null)
                {
                    newRecord["CostBudgetSetCode"] = this._CostBudgetSetCode;
                }
                if (this._PBSType != null)
                {
                    newRecord["PBSType"] = this._PBSType;
                }
                if (this._PBSCode != null)
                {
                    newRecord["PBSCode"] = this._PBSCode;
                }
                if (this._Money != null)
                {
                    newRecord["Money"] = this._Money;
                }
                if (this._ObligateMoney != null)
                {
                    newRecord["ObligateMoney"] = this._ObligateMoney;
                }
                if (this._BiddingRemark1 != null)
                {
                    newRecord["BiddingRemark1"] = this._BiddingRemark1;
                }
                if (this._BiddingRemark2 != null)
                {
                    newRecord["BiddingRemark2"] = this._BiddingRemark2;
                }
                if (this._ContentMeeting != null)
                {
                    newRecord["ContentMeeting"] = this._ContentMeeting;
                }
                if (this._BiddingType != null)
                {
                    newRecord["BiddingType"] = this._BiddingType;
                }
                if (this._BiddingAddress != null)
                {
                    newRecord["BiddingAddress"] = this._BiddingAddress;
                }
                if (this._Status != null)
                {
                    newRecord["Status"] = this._Status;
                }
                if (flag)
                {
                    biddingByCode.AddNewRecord(newRecord);
                }
                data2 = biddingByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private void DeleteBidding(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding"))
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

        private EntityData GetAllBidding()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding"))
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

        private EntityData GetBiddingByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    data = new SingleEntityDAO("Bidding").SelectbyPrimaryKey(code);
                }
                else
                {
                    this._dao.EntityName = "Bidding";
                    data = this._dao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private EntityData GetBiddingByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "Bidding";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static DataSet GetBiddingForm(string BiddingCode)
        {
            DataSet set2;
            try
            {
                DataSet set = new DataSet();
                BiddingPrejudication prejudication = new BiddingPrejudication();
                prejudication.BiddingCode = BiddingCode;
                set.Tables.Add(prejudication.GetBiddingPrejudications().Copy());
                BiddingEmit emit = new BiddingEmit();
                emit.BiddingCode = BiddingCode;
                set.Tables.Add(emit.GetBiddingEmits().Copy());
                BiddingMessage message = new BiddingMessage();
                message.BiddingCode = BiddingCode;
                set.Tables.Add(message.GetBiddingMessages().Copy());
                set2 = set;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public string GetBiddingName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData biddingByCode = this.GetBiddingByCode(code);
                if (biddingByCode.HasRecord())
                {
                    text = biddingByCode.GetString("Title");
                }
                biddingByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public DataTable GetBiddingReturn()
        {
            BiddingReturn return2 = new BiddingReturn();
            return2.BiddingEmitCode = "";
            DataTable biddingReturns = return2.GetBiddingReturns();
            BiddingEmit emit = new BiddingEmit();
            emit.BiddingCode = this.BiddingCode;
            DataTable biddingEmits = emit.GetBiddingEmits();
            for (int i = 0; i < biddingEmits.Rows.Count; i++)
            {
                BiddingReturn return3 = new BiddingReturn();
                return3.BiddingEmitCode = biddingEmits.Rows[i]["BiddingEmitCode"].ToString();
                biddingReturns.Merge(return3.GetBiddingReturns(), true);
            }
            return biddingReturns;
        }

        public DataTable GetBiddingReturnNoMessage()
        {
            int index;
            BiddingReturn return2 = new BiddingReturn();
            return2.dao = this.dao;
            return2.BiddingEmitCode = "";
            DataTable biddingReturns = return2.GetBiddingReturns();
            BiddingEmit emit = new BiddingEmit();
            emit.dao = this.dao;
            emit.BiddingCode = this.BiddingCode;
            DataTable biddingEmits = emit.GetBiddingEmits();
            for (index = 0; index < biddingEmits.Rows.Count; index++)
            {
                BiddingReturn return3 = new BiddingReturn();
                return3.dao = this.dao;
                return3.BiddingEmitCode = biddingEmits.Rows[index]["BiddingEmitCode"].ToString();
                return3.Flag = "1";
                biddingReturns.Merge(return3.GetBiddingReturns(), true);
            }
            BiddingMessage message = new BiddingMessage();
            message.dao = this.dao;
            message.BiddingCode = this.BiddingCode;
            DataTable biddingMessages = message.GetBiddingMessages();
            foreach (DataRow row in biddingMessages.Select())
            {
                DataRow[] rowArray = biddingReturns.Select("BiddingReturnCode in (" + row["BiddingReturnCode"].ToString() + "'')");
                for (index = 0; index < rowArray.Length; index++)
                {
                    biddingReturns.Rows.Remove(rowArray[index]);
                }
            }
            return biddingReturns;
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

        private void InsertBidding(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllBidding(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding"))
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

        private void UpdateBidding(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string Accessory
        {
            get
            {
                if ((this._Accessory == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._Accessory;
            }
            set
            {
                this._Accessory = value;
            }
        }

        public string ArrangedDate
        {
            get
            {
                if ((this._ArrangedDate == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._ArrangedDate;
            }
            set
            {
                this._ArrangedDate = value;
            }
        }

        public ArrayList ArrangedDateEx
        {
            set
            {
                this._ArrangedDateEx = value;
            }
        }

        public string BiddingAddress
        {
            get
            {
                if ((this._BiddingAddress == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._BiddingAddress;
            }
            set
            {
                this._BiddingAddress = value;
            }
        }

        public string BiddingCode
        {
            get
            {
                return this._BiddingCode;
            }
            set
            {
                this._BiddingCode = value;
            }
        }

        public string BiddingLastEmit
        {
            get
            {
                string text = "";
                BiddingEmit emit = new BiddingEmit();
                emit.BiddingCode = this.BiddingCode;
                emit.CreatUser = "";
                DataRow[] rowArray = emit.GetBiddingEmits().Select("", "BiddingEmitCode desc");
                if (rowArray.Length > 0)
                {
                    text = rowArray[0]["BiddingEmitCode"].ToString();
                }
                return text;
            }
        }

        public string BiddingRemark1
        {
            get
            {
                if ((this._BiddingRemark1 == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._BiddingRemark1;
            }
            set
            {
                this._BiddingRemark1 = value;
            }
        }

        public string BiddingRemark2
        {
            get
            {
                if ((this._BiddingRemark2 == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._BiddingRemark2;
            }
            set
            {
                this._BiddingRemark2 = value;
            }
        }

        public string BiddingType
        {
            get
            {
                if ((this._BiddingType == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._BiddingType;
            }
            set
            {
                this._BiddingType = value;
            }
        }

        public string ConfirmDate
        {
            get
            {
                if ((this._ConfirmDate == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._ConfirmDate;
            }
            set
            {
                this._ConfirmDate = value;
            }
        }

        public ArrayList ConfirmDateEx
        {
            set
            {
                this._ConfirmDateEx = value;
            }
        }

        public string Content
        {
            get
            {
                if ((this._Content == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        public string ContentMeeting
        {
            get
            {
                if ((this._ContentMeeting == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._ContentMeeting;
            }
            set
            {
                this._ContentMeeting = value;
            }
        }

        public string CostBudgetSetCode
        {
            get
            {
                if ((this._CostBudgetSetCode == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
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
                if ((this._CostCode == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
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

        public string EmitDate
        {
            get
            {
                if ((this._EmitDate == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._EmitDate;
            }
            set
            {
                this._EmitDate = value;
            }
        }

        public string Money
        {
            get
            {
                if ((this._Money == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._Money;
            }
            set
            {
                this._Money = value;
            }
        }

        public string ObligateMoney
        {
            get
            {
                if ((this._ObligateMoney == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._ObligateMoney;
            }
            set
            {
                this._ObligateMoney = value;
            }
        }

        public string PBSCode
        {
            get
            {
                if ((this._PBSCode == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
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
                if ((this._PBSType == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._PBSType;
            }
            set
            {
                this._PBSType = value;
            }
        }

        public string PrejudicationDate
        {
            get
            {
                if ((this._PrejudicationDate == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._PrejudicationDate;
            }
            set
            {
                this._PrejudicationDate = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                if ((this._ProjectCode == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string ReturnDate
        {
            get
            {
                if ((this._ReturnDate == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._ReturnDate;
            }
            set
            {
                this._ReturnDate = value;
            }
        }

        public string StandardDate
        {
            get
            {
                if ((this._StandardDate == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._StandardDate;
            }
            set
            {
                this._StandardDate = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string Status
        {
            get
            {
                if ((this._Status == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        public string Title
        {
            get
            {
                if ((this._Title == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
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
                if ((this._Type == null) && (this._BiddingCode != null))
                {
                    this._GetBiddingByCode();
                }
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

