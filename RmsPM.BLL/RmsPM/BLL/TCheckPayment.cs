namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class TCheckPayment
    {
        private string _AcceptAccounts = null;
        private string _AcceptBank = null;
        private decimal _AcceptMoney = 0M;
        private string _AcceptMoneyType = null;
        private string _AcceptUnit = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _PaymentAccounts = null;
        private string _PaymentBank = null;
        private string _PaymentCodition = null;
        private decimal _PaymentMoney = 0M;
        private string _PaymentMoneyType = null;
        private string _PaymentRemark = null;
        private string _PaymentTicketDate = null;
        private string _PaymentTicketMark = null;
        private string _PaymentUnit = null;
        private string _Remark = null;
        private string _State = null;
        private string _TCheckPaymentCode = null;

        private void _GetTCheckPaymentByCode()
        {
            try
            {
                EntityData tCheckPaymentByCode = GetTCheckPaymentByCode(this._TCheckPaymentCode);
                this._TCheckPaymentCode = tCheckPaymentByCode.GetString("TCheckPaymentCode");
                this._PaymentUnit = tCheckPaymentByCode.GetString("PaymentUnit");
                this._PaymentRemark = tCheckPaymentByCode.GetString("PaymentRemark");
                this._AcceptUnit = tCheckPaymentByCode.GetString("AcceptUnit");
                this._AcceptBank = tCheckPaymentByCode.GetDateTime("AcceptBank").ToString();
                this._AcceptAccounts = tCheckPaymentByCode.GetDateTime("AcceptAccounts").ToString();
                this._PaymentBank = tCheckPaymentByCode.GetDateTime("PaymentBank").ToString();
                this._PaymentAccounts = tCheckPaymentByCode.GetDateTime("PaymentAccounts").ToString();
                this._PaymentTicketMark = tCheckPaymentByCode.GetDateTime("PaymentTicketMark").ToString();
                this._PaymentTicketDate = tCheckPaymentByCode.GetDateTime("PaymentTicketDate").ToString();
                this._PaymentMoneyType = tCheckPaymentByCode.GetDateTime("PaymentMoneyType").ToString();
                this._PaymentCodition = tCheckPaymentByCode.GetDateTime("PaymentCodition").ToString();
                this._Remark = tCheckPaymentByCode.GetDateTime("Remark").ToString();
                this._AcceptMoneyType = tCheckPaymentByCode.GetDateTime("AcceptMoneyType").ToString();
                this._AcceptMoney = tCheckPaymentByCode.GetDecimal("AcceptMoney");
                this._PaymentMoney = tCheckPaymentByCode.GetDecimal("PaymentMoney");
                this._State = tCheckPaymentByCode.GetString("State");
                this._Flag = tCheckPaymentByCode.GetString("Flag");
                tCheckPaymentByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetTCheckPayments()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TCheckPayment");
                TCheckPaymentStrategyBuilder builder = new TCheckPaymentStrategyBuilder();
                if (this._TCheckPaymentCode != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.TCheckPaymentCode, this._TCheckPaymentCode));
                }
                if (this._PaymentUnit != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.PaymentUnit, this._PaymentUnit));
                }
                if (this._PaymentRemark != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.PaymentRemark, this._PaymentRemark));
                }
                if (this._AcceptUnit != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.AcceptUnit, this._AcceptUnit));
                }
                if (this._AcceptBank != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.AcceptBank, this._AcceptBank));
                }
                if (this._AcceptAccounts != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.AcceptAccounts, this._AcceptAccounts));
                }
                if (this._PaymentBank != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.PaymentBank, this._PaymentBank));
                }
                if (this._PaymentAccounts != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.PaymentAccounts, this._PaymentAccounts));
                }
                if (this._PaymentTicketMark != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.PaymentTicketMark, this._PaymentTicketMark));
                }
                if (this._PaymentTicketDate != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.PaymentTicketDate, this._PaymentTicketDate));
                }
                if (this._PaymentMoneyType != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.PaymentMoneyType, this._PaymentMoneyType));
                }
                if (this._AcceptMoneyType != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.AcceptMoneyType, this._AcceptMoneyType));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.State, this._State));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(TCheckPaymentStrategyName.Flag, this._Flag));
                }
                string sqlString = builder.BuildMainQueryString() + " order by TCheckPaymentCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TCheckPayment"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "TCheckPayment";
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

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData tCheckPaymentByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._TCheckPaymentCode == "")
                {
                    flag = true;
                    tCheckPaymentByCode = GetTCheckPaymentByCode("");
                    this._TCheckPaymentCode = SystemManageDAO.GetNewSysCode("TCheckPayment");
                    newRecord = tCheckPaymentByCode.GetNewRecord();
                }
                else
                {
                    tCheckPaymentByCode = GetTCheckPaymentByCode(this._TCheckPaymentCode);
                    if (tCheckPaymentByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = tCheckPaymentByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = tCheckPaymentByCode.CurrentRow;
                    }
                }
                if (this._TCheckPaymentCode != null)
                {
                    newRecord["TCheckPaymentCode"] = this._TCheckPaymentCode;
                }
                if (this._PaymentUnit != null)
                {
                    newRecord["PaymentUnit"] = this._PaymentUnit;
                }
                if (this._PaymentRemark != null)
                {
                    newRecord["PaymentRemark"] = this._PaymentRemark;
                }
                if (this._AcceptUnit != null)
                {
                    newRecord["AcceptUnit"] = this._AcceptUnit;
                }
                if (this._AcceptBank != null)
                {
                    newRecord["AcceptBank"] = this._AcceptBank;
                }
                if (this._AcceptAccounts != null)
                {
                    newRecord["AcceptAccounts"] = this._AcceptAccounts;
                }
                if (this._PaymentBank != null)
                {
                    newRecord["PaymentBank"] = this._PaymentBank;
                }
                if (this._PaymentAccounts != null)
                {
                    newRecord["PaymentAccounts"] = this._PaymentAccounts;
                }
                if (this._PaymentTicketMark != null)
                {
                    newRecord["PaymentTicketMark"] = this._PaymentTicketMark;
                }
                if (this._PaymentTicketDate != null)
                {
                    newRecord["PaymentTicketDate"] = this._PaymentTicketDate;
                }
                if (this._PaymentMoneyType != null)
                {
                    newRecord["PaymentMoneyType"] = this._PaymentMoneyType;
                }
                if (this._PaymentCodition != null)
                {
                    newRecord["PaymentCodition"] = this._PaymentCodition;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
                }
                if (this._AcceptMoneyType != null)
                {
                    newRecord["AcceptMoneyType"] = this._AcceptMoneyType;
                }
                if (this._AcceptMoney != 0M)
                {
                    newRecord["AcceptMoney"] = this._AcceptMoney;
                }
                if (this._PaymentMoney != 0M)
                {
                    newRecord["PaymentMoney"] = this._PaymentMoney;
                }
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
                    tCheckPaymentByCode.AddNewRecord(newRecord);
                }
                data2 = tCheckPaymentByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteTCheckPayment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TCheckPayment"))
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

        public static EntityData GetAllTCheckPayment()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TCheckPayment"))
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

        public static EntityData GetTCheckPaymentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TCheckPayment"))
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

        public static EntityData GetTCheckPaymentByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetTCheckPayments()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetTCheckPayments().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertTCheckPayment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TCheckPayment"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllTCheckPayment(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TCheckPayment"))
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

        public void TCheckPaymentAdd()
        {
            if (this._dao == null)
            {
                SubmitAllTCheckPayment(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "TCheckPayment";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public static bool TCheckPaymenTBtatusChange(string TCheckPaymentCode, int TB_iStatus)
        {
            return TCheckPaymenTBtatusChange(TCheckPaymentCode, TB_iStatus, null, true);
        }

        public static bool TCheckPaymenTBtatusChange(string TCheckPaymentCode, int TB_iStatus, int? TB_iOriginalStatus)
        {
            return TCheckPaymenTBtatusChange(TCheckPaymentCode, TB_iStatus, TB_iOriginalStatus, true);
        }

        public static bool TCheckPaymenTBtatusChange(EntityData TB_Entity, int TB_iStatus, int? TB_iOriginalStatus)
        {
            return TCheckPaymenTBtatusChange(TB_Entity, "", TB_iStatus, TB_iOriginalStatus, false);
        }

        public static bool TCheckPaymenTBtatusChange(EntityData TB_Entity, int TB_iStatus, bool TB_bSubmitData)
        {
            return TCheckPaymenTBtatusChange(TB_Entity, "", TB_iStatus, null, TB_bSubmitData);
        }

        public static bool TCheckPaymenTBtatusChange(string TCheckPaymentCode, int TB_iStatus, int? TB_iOriginalStatus, bool TB_bSubmitData)
        {
            return TCheckPaymenTBtatusChange(GetTCheckPaymentByCode(TCheckPaymentCode), TCheckPaymentCode, TB_iStatus, TB_iOriginalStatus, TB_bSubmitData);
        }

        public static bool TCheckPaymenTBtatusChange(EntityData TB_Entity, int TB_iStatus, int? TB_iOriginalStatus, bool TB_bSubmitData)
        {
            return TCheckPaymenTBtatusChange(TB_Entity, "", TB_iStatus, TB_iOriginalStatus, TB_bSubmitData);
        }

        public static bool TCheckPaymenTBtatusChange(EntityData TB_Entity, string TCheckPaymentCode, int TB_iStatus, int? TB_iOriginalStatus, bool TB_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                TB_Entity.SetCurrentTable("TCheckPayment");
                if (TCheckPaymentCode.Trim() == "")
                {
                    if (TB_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("TCheckPaymentCode='{0}'", TCheckPaymentCode.Trim());
                    if (TB_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in TB_Entity.CurrentTable.Select(filterExpression))
                {
                    if (TB_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["State"] != DBNull.Value) && ((((int) row["State"]) == (nullable = TB_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["State"] = TB_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["State"] = TB_iStatus;
                    }
                }
                if (flag && TB_bSubmitData)
                {
                    SubmitAllTCheckPayment(TB_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public void TCheckPaymentDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteTCheckPayment(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "TCheckPayment";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool TCheckPaymentStatusChange(EntityData TB_Entity, int TB_iStatus)
        {
            return TCheckPaymenTBtatusChange(TB_Entity, "", TB_iStatus, null, false);
        }

        public static void UpdateTCheckPayment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TCheckPayment"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string AcceptAccounts
        {
            get
            {
                if ((this._AcceptAccounts == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._AcceptAccounts;
            }
            set
            {
                if (this._AcceptAccounts != value)
                {
                    this._AcceptAccounts = value;
                }
            }
        }

        public string AcceptBank
        {
            get
            {
                if ((this._AcceptBank == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._AcceptBank;
            }
            set
            {
                if (this._AcceptBank != value)
                {
                    this._AcceptBank = value;
                }
            }
        }

        public decimal AcceptMoney
        {
            get
            {
                if ((this._AcceptMoney == 0M) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._AcceptMoney;
            }
            set
            {
                if (this._AcceptMoney != value)
                {
                    this._AcceptMoney = value;
                }
            }
        }

        public string AcceptMoneyType
        {
            get
            {
                if ((this._AcceptMoneyType == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._AcceptMoneyType;
            }
            set
            {
                if (this._AcceptMoneyType != value)
                {
                    this._AcceptMoneyType = value;
                }
            }
        }

        public string AcceptUnit
        {
            get
            {
                if ((this._AcceptUnit == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._AcceptUnit;
            }
            set
            {
                if (this._AcceptUnit != value)
                {
                    this._AcceptUnit = value;
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

        public string PaymentAccounts
        {
            get
            {
                if ((this._PaymentAccounts == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentAccounts;
            }
            set
            {
                if (this._PaymentAccounts != value)
                {
                    this._PaymentAccounts = value;
                }
            }
        }

        public string PaymentBank
        {
            get
            {
                if ((this._PaymentBank == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentBank;
            }
            set
            {
                if (this._PaymentBank != value)
                {
                    this._PaymentBank = value;
                }
            }
        }

        public string PaymentCodition
        {
            get
            {
                if ((this._PaymentCodition == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentCodition;
            }
            set
            {
                if (this._PaymentCodition != value)
                {
                    this._PaymentCodition = value;
                }
            }
        }

        public decimal PaymentMoney
        {
            get
            {
                if ((this._PaymentMoney == 0M) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentMoney;
            }
            set
            {
                if (this._PaymentMoney != value)
                {
                    this._PaymentMoney = value;
                }
            }
        }

        public string PaymentMoneyType
        {
            get
            {
                if ((this._PaymentMoneyType == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentMoneyType;
            }
            set
            {
                if (this._PaymentMoneyType != value)
                {
                    this._PaymentMoneyType = value;
                }
            }
        }

        public string PaymentRemark
        {
            get
            {
                if ((this._PaymentRemark == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentRemark;
            }
            set
            {
                if (this._PaymentRemark != value)
                {
                    this._PaymentRemark = value;
                }
            }
        }

        public string PaymentTicketDate
        {
            get
            {
                if ((this._PaymentTicketDate == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentTicketDate;
            }
            set
            {
                if (this._PaymentTicketDate != value)
                {
                    this._PaymentTicketDate = value;
                }
            }
        }

        public string PaymentTicketMark
        {
            get
            {
                if ((this._PaymentTicketMark == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentTicketMark;
            }
            set
            {
                if (this._PaymentTicketMark != value)
                {
                    this._PaymentTicketMark = value;
                }
            }
        }

        public string PaymentUnit
        {
            get
            {
                if ((this._PaymentUnit == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
                }
                return this._PaymentUnit;
            }
            set
            {
                if (this._PaymentUnit != value)
                {
                    this._PaymentUnit = value;
                }
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._TCheckPaymentCode != null))
                {
                    this._GetTCheckPaymentByCode();
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

        public string TCheckPaymentCode
        {
            get
            {
                return this._TCheckPaymentCode;
            }
            set
            {
                if (this._TCheckPaymentCode != value)
                {
                    this._TCheckPaymentCode = value;
                }
            }
        }
    }
}

