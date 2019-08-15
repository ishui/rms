namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingConditionFile
    {
        private string _BiddingCode = null;
        private string _BiddingConditionFileCode = null;
        private string _BiddingConditionFileNumber = null;
        private StandardEntityDAO _dao;
        private string _Gq = null;
        private string _Jsxq = null;
        private string _Name = null;
        private string _Rctj = null;
        private string _Remark = null;
        private string _Shfw = null;
        private string _State = null;
        private string _Zbfw = null;
        private string _Zlbz = null;

        private void _BiddingConditionFileByCode()
        {
            try
            {
                EntityData biddingConditionFileByCode = GetBiddingConditionFileByCode(this._BiddingConditionFileCode);
                this._BiddingConditionFileCode = biddingConditionFileByCode.GetString("BiddingConditionFileCode");
                this._BiddingCode = biddingConditionFileByCode.GetString("BiddingCode");
                this._Name = biddingConditionFileByCode.GetString("name");
                this._State = biddingConditionFileByCode.GetString("state");
                this._BiddingConditionFileNumber = biddingConditionFileByCode.GetString("BiddingConditionFileNumber");
                this._Zbfw = biddingConditionFileByCode.GetString("ZBFW");
                this._Jsxq = biddingConditionFileByCode.GetString("JSXQ");
                this._Zlbz = biddingConditionFileByCode.GetString("ZLBZ");
                this._Gq = biddingConditionFileByCode.GetString("GQ");
                this._Rctj = biddingConditionFileByCode.GetString("RCTJ");
                this._Shfw = biddingConditionFileByCode.GetString("SHFW");
                this._Remark = biddingConditionFileByCode.GetString("Remark");
                biddingConditionFileByCode.Dispose();
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
                EntityData entitydata = new EntityData("BiddingConditionFile");
                BiddingConditionFileStrategyBuilder builder = new BiddingConditionFileStrategyBuilder();
                if (this._BiddingConditionFileCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.BiddingConditionFileCode, this._BiddingConditionFileCode));
                }
                if (this._BiddingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._Name != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.name, this._Name));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.State, this._State));
                }
                if (this._BiddingConditionFileNumber != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.BiddingConditionFileNumber, this._BiddingConditionFileNumber));
                }
                if (this._Zbfw != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.ZBFW, this._Zbfw));
                }
                if (this._Jsxq != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.JSXQ, this._Jsxq));
                }
                if (this._Zlbz != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.ZLBZ, this._Zlbz));
                }
                if (this._Gq != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.GQ, this._Gq));
                }
                if (this._Rctj != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.RCTJ, this._Rctj));
                }
                if (this._Shfw != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.SHFW, this._Shfw));
                }
                if (this._Remark != null)
                {
                    builder.AddStrategy(new Strategy(BiddingConditionFileStrategyName.Remark, "%" + this._Remark + "%"));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingConditionFileCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingConditionFile"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingConditionFile";
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

        public void BiddingConditionFileAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingConditionFile(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingConditionFile";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingConditionFileDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingConditionFile(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingConditionFile";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool BiddingConditionFileStatusChange(string BiddingConditionFileCode, int gm_iStatus)
        {
            return BiddingConditionFileStatusChange(BiddingConditionFileCode, gm_iStatus, null, true);
        }

        public static bool BiddingConditionFileStatusChange(EntityData gm_Entity, int gm_iStatus)
        {
            return BiddingConditionFileStatusChange(gm_Entity, "", gm_iStatus, null, false);
        }

        public static bool BiddingConditionFileStatusChange(string BiddingConditionFileCode, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return BiddingConditionFileStatusChange(BiddingConditionFileCode, gm_iStatus, gm_iOriginalStatus, true);
        }

        public static bool BiddingConditionFileStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return BiddingConditionFileStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, false);
        }

        public static bool BiddingConditionFileStatusChange(EntityData gm_Entity, int gm_iStatus, bool gm_bSubmitData)
        {
            return BiddingConditionFileStatusChange(gm_Entity, "", gm_iStatus, null, gm_bSubmitData);
        }

        public static bool BiddingConditionFileStatusChange(StandardEntityDAO tempdao, string pm_sBiddingConditionFileCode, int pm_iStatus)
        {
            if (tempdao == null)
            {
                tempdao = new StandardEntityDAO("BiddingConditionFile");
            }
            else
            {
                tempdao.EntityName = "BiddingConditionFile";
            }
            EntityData biddingConditionFileByCode = GetBiddingConditionFileByCode(pm_sBiddingConditionFileCode, tempdao);
            bool flag = BiddingConditionFileStatusChange(biddingConditionFileByCode, "", pm_iStatus, null, false);
            tempdao.SubmitEntity(biddingConditionFileByCode);
            return flag;
        }

        public static bool BiddingConditionFileStatusChange(string BiddingConditionFileCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return BiddingConditionFileStatusChange(GetBiddingConditionFileByCode(BiddingConditionFileCode), BiddingConditionFileCode, gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool BiddingConditionFileStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return BiddingConditionFileStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool BiddingConditionFileStatusChange(EntityData gm_Entity, string BiddingConditionFileCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                gm_Entity.SetCurrentTable("BiddingConditionFile");
                if (BiddingConditionFileCode.Trim() == "")
                {
                    if (gm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("BiddingConditionFileCode='{0}'", BiddingConditionFileCode.Trim());
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
                        if ((row["state"] != DBNull.Value) && ((((int) row["state"]) == (nullable = gm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["state"] = gm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["state"] = gm_iStatus;
                    }
                }
                if (flag && gm_bSubmitData)
                {
                    SubmitAllBiddingConditionFile(gm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData biddingConditionFileByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingConditionFileCode == "")
                {
                    flag = true;
                    biddingConditionFileByCode = GetBiddingConditionFileByCode("");
                    this._BiddingConditionFileCode = SystemManageDAO.GetNewSysCode("BiddingConditionFile");
                    newRecord = biddingConditionFileByCode.GetNewRecord();
                }
                else
                {
                    biddingConditionFileByCode = GetBiddingConditionFileByCode(this._BiddingConditionFileCode);
                    if (biddingConditionFileByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingConditionFileByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingConditionFileByCode.CurrentRow;
                    }
                }
                if (this._BiddingConditionFileCode != null)
                {
                    newRecord["BiddingConditionFileCode"] = this._BiddingConditionFileCode;
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
                }
                if (this._Name != null)
                {
                    newRecord["name"] = this._Name;
                }
                if (this._State != null)
                {
                    newRecord["state"] = this._State;
                }
                if (this._BiddingConditionFileNumber != null)
                {
                    newRecord["BiddingConditionFileNumber"] = this._BiddingConditionFileNumber;
                }
                if (this._Zbfw != null)
                {
                    newRecord["ZBFW"] = this._Zbfw;
                }
                if (this._Jsxq != null)
                {
                    newRecord["JSXQ"] = this._Jsxq;
                }
                if (this._Zlbz != null)
                {
                    newRecord["ZLBZ"] = this._Zlbz;
                }
                if (this._Gq != null)
                {
                    newRecord["GQ"] = this._Gq;
                }
                if (this._Rctj != null)
                {
                    newRecord["RCTJ"] = this._Rctj;
                }
                if (this._Shfw != null)
                {
                    newRecord["SHFW"] = this._Shfw;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
                }
                if (flag)
                {
                    biddingConditionFileByCode.AddNewRecord(newRecord);
                }
                data2 = biddingConditionFileByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingConditionFile(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingConditionFile"))
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

        public static EntityData GetAllBiddingConditionFile()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingConditionFile"))
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

        public static EntityData GetBiddingConditionFileByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingConditionFile"))
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

        public static EntityData GetBiddingConditionFileByCode(string code, StandardEntityDAO dao)
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

        public static string GetBiddingConditionFileStatusName(string state)
        {
            switch (state)
            {
                case "0":
                    return "已审";

                case "1":
                    return "申请";

                case "2":
                    return "已结";

                case "3":
                    return "作废";

                case "4":
                    return "变更";

                case "6":
                    return "历史";

                case "7":
                    return "审核中";

                case "8":
                    return "已评审";

                case "9":
                    return "评审中";
            }
            return "";
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

        public static void InsertBiddingConditionFile(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingConditionFile"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingConditionFile(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingConditionFile"))
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

        public static void UpdateBiddingConditionFile(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingConditionFile"))
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
                if ((this._BiddingCode == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
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

        public string BiddingConditionFileCode
        {
            get
            {
                return this._BiddingConditionFileCode;
            }
            set
            {
                if (this._BiddingConditionFileCode != value)
                {
                    this._BiddingConditionFileCode = value;
                }
            }
        }

        public string BiddingConditionFileNumber
        {
            get
            {
                if ((this._BiddingConditionFileNumber == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._BiddingConditionFileNumber;
            }
            set
            {
                if (this._BiddingConditionFileNumber != value)
                {
                    this._BiddingConditionFileNumber = value;
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

        public string Gq
        {
            get
            {
                if ((this._Gq == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._Gq;
            }
            set
            {
                if (this._Gq != value)
                {
                    this._Gq = value;
                }
            }
        }

        public string Jsxq
        {
            get
            {
                if ((this._Jsxq == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._Jsxq;
            }
            set
            {
                if (this._Jsxq != value)
                {
                    this._Jsxq = value;
                }
            }
        }

        public string Name
        {
            get
            {
                if ((this._Name == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._Name;
            }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                }
            }
        }

        public string Rctj
        {
            get
            {
                if ((this._Rctj == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._Rctj;
            }
            set
            {
                if (this._Rctj != value)
                {
                    this._Rctj = value;
                }
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
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

        public string Shfw
        {
            get
            {
                if ((this._Shfw == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._Shfw;
            }
            set
            {
                if (this._Shfw != value)
                {
                    this._Shfw = value;
                }
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
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

        public string Zbfw
        {
            get
            {
                if ((this._Zbfw == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._Zbfw;
            }
            set
            {
                if (this._Zbfw != value)
                {
                    this._Zbfw = value;
                }
            }
        }

        public string Zlbz
        {
            get
            {
                if ((this._Zlbz == null) && (this._BiddingConditionFileCode != null))
                {
                    this._BiddingConditionFileByCode();
                }
                return this._Zlbz;
            }
            set
            {
                if (this._Zlbz != value)
                {
                    this._Zlbz = value;
                }
            }
        }
    }
}

