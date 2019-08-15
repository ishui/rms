namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Text.RegularExpressions;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingMessage
    {
        private string _AttachUser = null;
        private string _BiddingCode = null;
        private string _BiddingDtlCode = null;
        private string _BiddingMessageCode = null;
        private string _BiddingReturnCode = null;
        private string _ContractDate = null;
        private string _ContractName = null;
        private string _ContractNember = null;
        private string _ContractType = null;
        private string _CreateDate = null;
        private string _CreateUser = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _ProjectCode = null;
        private string _Remark = null;
        private string _State = null;
        private string _Supplier = null;

        private void _GetBiddingMessageByCode()
        {
            EntityData biddingMessageByCode = this.GetBiddingMessageByCode(this._BiddingMessageCode);
            this._BiddingMessageCode = biddingMessageByCode.GetString("BiddingMessageCode");
            this._BiddingCode = biddingMessageByCode.GetString("BiddingCode");
            this._ProjectCode = biddingMessageByCode.GetString("ProjectCode");
            this._ContractNember = biddingMessageByCode.GetString("ContractNember");
            this._ContractName = biddingMessageByCode.GetString("ContractName");
            this._ContractType = biddingMessageByCode.GetString("ContractType");
            this._Supplier = biddingMessageByCode.GetString("Supplier");
            this._ContractDate = biddingMessageByCode.GetDateTimeOnlyDate("ContractDate");
            this._Remark = biddingMessageByCode.GetString("Remark");
            this._CreateDate = biddingMessageByCode.GetDateTimeOnlyDate("CreateDate");
            this._CreateUser = biddingMessageByCode.GetString("CreateUser");
            this._State = biddingMessageByCode.GetString("State");
            this._Flag = biddingMessageByCode.GetString("Flag");
            this._BiddingReturnCode = biddingMessageByCode.GetString("BiddingReturnCode");
            this._BiddingDtlCode = biddingMessageByCode.GetString("BiddingDtlCode");
            this._AttachUser = biddingMessageByCode.GetString("AttachUser");
            biddingMessageByCode.Dispose();
        }

        private EntityData _GetBiddingMessages()
        {
            EntityData entitydata = new EntityData("BiddingMessage");
            BiddingMessageStrategyBuilder builder = new BiddingMessageStrategyBuilder();
            if (this._BiddingMessageCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.BiddingMessageCode, this._BiddingMessageCode));
            }
            if (this._BiddingCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.BiddingCode, this._BiddingCode));
            }
            if (this._ProjectCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.ProjectCode, this._ProjectCode));
            }
            if (this._ContractNember != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.ContractNember, this._ContractNember));
            }
            if (this._ContractName != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.ContractName, this._ContractName));
            }
            if (this._ContractType != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.ContractType, this._ContractType));
            }
            if (this._Supplier != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.Supplier, this._Supplier));
            }
            if (this._ContractDate != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.ContractDate, this._ContractDate));
            }
            if (this._Remark != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.Remark, this._Remark));
            }
            if (this._CreateDate != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.CreateDate, this._CreateDate));
            }
            if (this._CreateUser != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.CreateUser, this._CreateUser));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.Flag, this._Flag));
            }
            if (this._BiddingReturnCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.BiddingReturnCode, this._BiddingReturnCode));
            }
            if (this._BiddingDtlCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingMessageStrategyName.BiddingDtlCode, this._BiddingDtlCode));
            }
            string sqlString = builder.BuildMainQueryString() + " order by BiddingMessageCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingMessage"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "BiddingMessage";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public void BiddingMessageAdd()
        {
            if (this._BiddingMessageCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingMessage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingMessage";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void BiddingMessageDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBiddingMessage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingMessage";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public static bool BiddingMessageStatusChange(string BiddingMessageCode, int gm_iStatus)
        {
            return BiddingMessageStatusChange(BiddingMessageCode, gm_iStatus, null, true);
        }

        public static bool BiddingMessageStatusChange(EntityData gm_Entity, int gm_iStatus)
        {
            return BiddingMessageStatusChange(gm_Entity, "", gm_iStatus, null, false);
        }

        public static bool BiddingMessageStatusChange(string BiddingMessageCode, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return BiddingMessageStatusChange(BiddingMessageCode, gm_iStatus, gm_iOriginalStatus, true);
        }

        public static bool BiddingMessageStatusChange(EntityData gm_Entity, int gm_iStatus, bool gm_bSubmitData)
        {
            return BiddingMessageStatusChange(gm_Entity, "", gm_iStatus, null, gm_bSubmitData);
        }

        public static bool BiddingMessageStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return BiddingMessageStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, false);
        }

        public static bool BiddingMessageStatusChange(string BiddingMessageCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            BiddingMessage message = new BiddingMessage();
            return BiddingMessageStatusChange(message.GetBiddingMessageByCode(BiddingMessageCode), BiddingMessageCode, gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool BiddingMessageStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return BiddingMessageStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool BiddingMessageStatusChange(EntityData gm_Entity, string BiddingMessageCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                gm_Entity.SetCurrentTable("BiddingMessage");
                if (BiddingMessageCode.Trim() == "")
                {
                    if (gm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("BiddingMessageCode='{0}'", BiddingMessageCode.Trim());
                    if (gm_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in gm_Entity.CurrentTable.Select(filterExpression))
                {
                    if (gm_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["State"] != DBNull.Value) && ((((int) row["State"]) == (nullable = gm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["State"] = gm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["State"] = gm_iStatus;
                    }
                }
                if (flag && gm_bSubmitData)
                {
                    new BiddingMessage().SubmitAllBiddingMessage(gm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public void BiddingMessageSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingMessage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingMessage";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingMessageUpdate()
        {
            if (this._BiddingMessageCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingMessage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingMessage";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData biddingMessageByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._BiddingMessageCode == "")
            {
                flag = true;
                biddingMessageByCode = this.GetBiddingMessageByCode("");
                this._BiddingMessageCode = SystemManageDAO.GetNewSysCode("BiddingMessage");
                newRecord = biddingMessageByCode.GetNewRecord();
            }
            else
            {
                biddingMessageByCode = this.GetBiddingMessageByCode(this._BiddingMessageCode);
                newRecord = biddingMessageByCode.CurrentRow;
            }
            if (this._BiddingMessageCode != null)
            {
                newRecord["BiddingMessageCode"] = this._BiddingMessageCode;
            }
            if (this._BiddingCode != null)
            {
                newRecord["BiddingCode"] = this._BiddingCode;
            }
            if (this._ProjectCode != null)
            {
                newRecord["ProjectCode"] = this._ProjectCode;
            }
            if (this._ContractNember != null)
            {
                newRecord["ContractNember"] = this._ContractNember;
            }
            if (this._ContractName != null)
            {
                newRecord["ContractName"] = this._ContractName;
            }
            if (this._ContractType != null)
            {
                newRecord["ContractType"] = this._ContractType;
            }
            if (this._Supplier != null)
            {
                newRecord["Supplier"] = this._Supplier;
            }
            if (this._ContractDate != null)
            {
                newRecord["ContractDate"] = this._ContractDate;
            }
            if (this._Remark != null)
            {
                newRecord["Remark"] = this._Remark;
            }
            if (this._CreateDate != null)
            {
                newRecord["CreateDate"] = this._CreateDate;
            }
            if (this._CreateUser != null)
            {
                newRecord["CreateUser"] = this._CreateUser;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (this._BiddingReturnCode != null)
            {
                newRecord["BiddingReturnCode"] = this._BiddingReturnCode;
            }
            if (this._BiddingDtlCode != null)
            {
                newRecord["BiddingDtlCode"] = this._BiddingDtlCode;
            }
            if (this._AttachUser != null)
            {
                newRecord["AttachUser"] = this._AttachUser;
            }
            if (flag)
            {
                biddingMessageByCode.AddNewRecord(newRecord);
            }
            return biddingMessageByCode;
        }

        private void DeleteBiddingMessage(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingMessage"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingMessage";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllBiddingMessage()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingMessage"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingMessage";
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

        private EntityData GetBiddingMessageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingMessage"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingMessage";
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

        public DataTable GetBiddingMessages()
        {
            return this._GetBiddingMessages().CurrentTable;
        }

        public string GetContractNember(string Biddingcode, string SupplierCode)
        {
            string text = DateTime.Now.ToString();
            return ("T" + Biddingcode + SupplierCode + text);
        }

        public DataTable GetConvertString(string OperationStr)
        {
            string pattern = ";@;";
            string[] textArray = Regex.Split(OperationStr, pattern, RegexOptions.IgnoreCase);
            DataTable table = new DataTable("BiddingMessage");
            table.Columns.Add("Remark", Type.GetType("System.String"));
            foreach (string text2 in textArray)
            {
                if (text2 != "")
                {
                    DataRow row = table.NewRow();
                    row["Remark"] = text2;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        private void InsertBiddingMessage(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingMessage"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingMessage";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllBiddingMessage(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingMessage"))
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
                    this.dao.EntityName = "BiddingMessage";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateBiddingMessage(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingMessage"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingMessage";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string AttachUser
        {
            get
            {
                if ((this._AttachUser == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._AttachUser;
            }
            set
            {
                this._AttachUser = value;
            }
        }

        public string BiddingCode
        {
            get
            {
                if ((this._BiddingCode == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
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
                if ((this._BiddingDtlCode == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._BiddingDtlCode;
            }
            set
            {
                this._BiddingDtlCode = value;
            }
        }

        public string BiddingMessageCode
        {
            get
            {
                return this._BiddingMessageCode;
            }
            set
            {
                this._BiddingMessageCode = value;
            }
        }

        public string BiddingReturnCode
        {
            get
            {
                if ((this._BiddingReturnCode == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._BiddingReturnCode;
            }
            set
            {
                this._BiddingReturnCode = value;
            }
        }

        public string ContractDate
        {
            get
            {
                if ((this._ContractDate == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._ContractDate;
            }
            set
            {
                this._ContractDate = value;
            }
        }

        public string ContractName
        {
            get
            {
                if ((this._ContractName == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._ContractName;
            }
            set
            {
                this._ContractName = value;
            }
        }

        public string ContractNember
        {
            get
            {
                if ((this._ContractNember == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._ContractNember;
            }
            set
            {
                this._ContractNember = value;
            }
        }

        public string ContractType
        {
            get
            {
                if ((this._ContractType == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._ContractType;
            }
            set
            {
                this._ContractType = value;
            }
        }

        public string CreateDate
        {
            get
            {
                if ((this._CreateDate == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._CreateDate;
            }
            set
            {
                this._CreateDate = value;
            }
        }

        public string CreateUser
        {
            get
            {
                if ((this._CreateUser == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._CreateUser;
            }
            set
            {
                this._CreateUser = value;
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
                if ((this._Flag == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                if ((this._ProjectCode == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
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
                if ((this._Remark == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string Supplier
        {
            get
            {
                if ((this._Supplier == null) && (this._BiddingMessageCode != null))
                {
                    this._GetBiddingMessageByCode();
                }
                return this._Supplier;
            }
            set
            {
                this._Supplier = value;
            }
        }
    }
}

