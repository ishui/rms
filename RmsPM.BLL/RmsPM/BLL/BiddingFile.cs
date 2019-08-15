namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingFile
    {
        private string _BiddingCode = null;
        private string _BiddingFileCode = null;
        private string _BiddingFileNumber = null;
        private StandardEntityDAO _dao;
        private string _Remark = null;
        private string _State = null;

        private void _GetBiddingFileByCode()
        {
            try
            {
                EntityData biddingFileByCode = GetBiddingFileByCode(this._BiddingFileCode);
                this._BiddingFileCode = biddingFileByCode.GetString("BiddingFileCode");
                this._BiddingCode = biddingFileByCode.GetString("BiddingCode");
                this._State = biddingFileByCode.GetString("state");
                this._BiddingFileNumber = biddingFileByCode.GetString("BiddingFileNumber");
                this._Remark = biddingFileByCode.GetString("Remark");
                biddingFileByCode.Dispose();
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
                EntityData entitydata = new EntityData("BiddingFile");
                BiddingFileStrategyBuider buider = new BiddingFileStrategyBuider();
                if (this._BiddingFileCode != null)
                {
                    buider.AddStrategy(new Strategy(BiddingFileStrategyName.BiddingFileCode, this._BiddingFileCode));
                }
                if (this._BiddingCode != null)
                {
                    buider.AddStrategy(new Strategy(BiddingFileStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._BiddingFileNumber != null)
                {
                    buider.AddStrategy(new Strategy(BiddingFileStrategyName.BiddingFileNumber, this._BiddingFileNumber));
                }
                if (this._State != null)
                {
                    buider.AddStrategy(new Strategy(BiddingFileStrategyName.State, this._State));
                }
                if (this._Remark != null)
                {
                    buider.AddStrategy(new Strategy(BiddingFileStrategyName.Remark, "%" + this._Remark + "%"));
                }
                string sqlString = buider.BuildMainQueryString() + " order by BiddingFileCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingFile"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingFile";
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

        public void BiddingFileAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingFile(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingFile";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingFileDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingFile(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingFile";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool BiddingFileStatusChange(string BiddingFileCode, int gm_iStatus)
        {
            return BiddingFileStatusChange(BiddingFileCode, gm_iStatus, null, true);
        }

        public static bool BiddingFileStatusChange(EntityData gm_Entity, int gm_iStatus)
        {
            return BiddingFileStatusChange(gm_Entity, "", gm_iStatus, null, false);
        }

        public static bool BiddingFileStatusChange(string BiddingFileCode, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return BiddingFileStatusChange(BiddingFileCode, gm_iStatus, gm_iOriginalStatus, true);
        }

        public static bool BiddingFileStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return BiddingFileStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, false);
        }

        public static bool BiddingFileStatusChange(EntityData gm_Entity, int gm_iStatus, bool gm_bSubmitData)
        {
            return BiddingFileStatusChange(gm_Entity, "", gm_iStatus, null, gm_bSubmitData);
        }

        public static bool BiddingFileStatusChange(StandardEntityDAO tempdao, string pm_sBiddingFileCode, int pm_iStatus)
        {
            if (tempdao == null)
            {
                tempdao = new StandardEntityDAO("BiddingFile");
            }
            else
            {
                tempdao.EntityName = "BiddingFile";
            }
            EntityData biddingFileByCode = GetBiddingFileByCode(pm_sBiddingFileCode, tempdao);
            bool flag = BiddingFileStatusChange(biddingFileByCode, "", pm_iStatus, null, false);
            tempdao.SubmitEntity(biddingFileByCode);
            return flag;
        }

        public static bool BiddingFileStatusChange(string BiddingFileCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return BiddingFileStatusChange(GetBiddingFileByCode(BiddingFileCode), BiddingFileCode, gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool BiddingFileStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return BiddingFileStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool BiddingFileStatusChange(EntityData gm_Entity, string BiddingFileCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                gm_Entity.SetCurrentTable("BiddingFile");
                if (BiddingFileCode.Trim() == "")
                {
                    if (gm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("BiddingFileCode='{0}'", BiddingFileCode.Trim());
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
                    SubmitAllBiddingFile(gm_Entity);
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
                EntityData biddingFileByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingFileCode == "")
                {
                    flag = true;
                    biddingFileByCode = GetBiddingFileByCode("");
                    this._BiddingFileCode = SystemManageDAO.GetNewSysCode("BiddingFile");
                    newRecord = biddingFileByCode.GetNewRecord();
                }
                else
                {
                    biddingFileByCode = GetBiddingFileByCode(this._BiddingFileCode);
                    if (biddingFileByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingFileByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingFileByCode.CurrentRow;
                    }
                }
                if (this._BiddingFileCode != null)
                {
                    newRecord["BiddingFileCode"] = this._BiddingFileCode;
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
                }
                if (this._BiddingFileNumber != null)
                {
                    newRecord["BiddingFileNumber"] = this._BiddingFileNumber;
                }
                if (this._State != null)
                {
                    newRecord["state"] = this._State;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
                }
                if (flag)
                {
                    biddingFileByCode.AddNewRecord(newRecord);
                }
                data2 = biddingFileByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingFile(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingFile"))
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

        public static EntityData GetAllBiddingFile()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingFile"))
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

        public static EntityData GetBiddingFileByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingFile"))
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

        public static EntityData GetBiddingFileByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetBiddingFiles()
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

        public static string GetBiddingFileStatusName(string state)
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

        public static void InsertBiddingFile(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingFile"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingFile(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingFile"))
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

        public static void UpdateBiddingFile(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingFile"))
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
                if ((this._BiddingCode == null) && (this._BiddingFileCode != null))
                {
                    this._GetBiddingFileByCode();
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

        public string BiddingFileCode
        {
            get
            {
                return this._BiddingFileCode;
            }
            set
            {
                if (this._BiddingFileCode != value)
                {
                    this._BiddingFileCode = value;
                }
            }
        }

        public string BiddingFileNumber
        {
            get
            {
                if ((this._BiddingFileNumber == null) && (this._BiddingFileCode != null))
                {
                    this._GetBiddingFileByCode();
                }
                return this._BiddingFileNumber;
            }
            set
            {
                if (this._BiddingFileNumber != value)
                {
                    this._BiddingFileNumber = value;
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

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._BiddingFileCode != null))
                {
                    this._GetBiddingFileByCode();
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
                if ((this._State == null) && (this._BiddingFileCode != null))
                {
                    this._GetBiddingFileByCode();
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

