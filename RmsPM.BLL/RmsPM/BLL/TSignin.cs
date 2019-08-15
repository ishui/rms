namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class TSignin
    {
        private StandardEntityDAO _dao;
        private string _Date = null;
        private string _Flag = null;
        private string _LeitMotiv = null;
        private string _Remark = null;
        private string _SigninDepartment = null;
        private string _Signinman = null;
        private string _State = null;
        private string _TSigninCode = null;

        private void _GetTSigninByCode()
        {
            try
            {
                EntityData tSigninByCode = GetTSigninByCode(this._TSigninCode);
                this._TSigninCode = tSigninByCode.GetString("TSigninCode");
                this._SigninDepartment = tSigninByCode.GetString("SigninDepartment");
                this._Signinman = tSigninByCode.GetString("Signinman");
                this._Date = tSigninByCode.GetDateTime("Date", "yyyy-MM-dd HH:mm");
                this._LeitMotiv = tSigninByCode.GetDateTime("LeitMotiv").ToString();
                this._Remark = tSigninByCode.GetDateTime("Remark").ToString();
                this._State = tSigninByCode.GetString("State");
                this._Flag = tSigninByCode.GetString("Flag");
                tSigninByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public EntityData _GetTSignins()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TSignin");
                TSigninStrategyBuilder builder = new TSigninStrategyBuilder();
                if (this._TSigninCode != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.TSigninCode, this._TSigninCode));
                }
                if (this._SigninDepartment != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.SigninDepartment, this._SigninDepartment));
                }
                if (this._Signinman != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.Signinman, this._Signinman));
                }
                if (this._Date != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.Date, this._Date));
                }
                if (this._LeitMotiv != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.LeitMotiv, this._LeitMotiv));
                }
                if (this._Remark != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.Remark, this._Remark));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.State, this._State));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(TSigninStrategyName.Flag, this._Flag));
                }
                string sqlString = builder.BuildMainQueryString() + " order by TSigninCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TSignin"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "TSignin";
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
                EntityData tSigninByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._TSigninCode == "")
                {
                    flag = true;
                    tSigninByCode = GetTSigninByCode("");
                    this._TSigninCode = SystemManageDAO.GetNewSysCode("TSignin");
                    newRecord = tSigninByCode.GetNewRecord();
                }
                else
                {
                    tSigninByCode = GetTSigninByCode(this._TSigninCode);
                    if (tSigninByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = tSigninByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = tSigninByCode.CurrentRow;
                    }
                }
                if (this._TSigninCode != null)
                {
                    newRecord["TSigninCode"] = this._TSigninCode;
                }
                if (this._SigninDepartment != null)
                {
                    newRecord["SigninDepartment"] = this._SigninDepartment;
                }
                if (this._Signinman != null)
                {
                    newRecord["Signinman"] = this._Signinman;
                }
                if (this._Date != null)
                {
                    newRecord["Date"] = this._Date;
                }
                if (this._LeitMotiv != null)
                {
                    newRecord["LeitMotiv"] = this._LeitMotiv;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
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
                    tSigninByCode.AddNewRecord(newRecord);
                }
                data2 = tSigninByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteTSignin(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TSignin"))
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

        public static EntityData GetAllTSignin()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TSignin"))
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

        public static EntityData GetTSigninByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TSignin"))
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

        public static EntityData GetTSigninByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetTSignins()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetTSignins().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertTSignin(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TSignin"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllTSignin(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TSignin"))
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

        public void TSigninAdd()
        {
            if (this._dao == null)
            {
                SubmitAllTSignin(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "TSignin";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void TSigninDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteTSignin(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "TSignin";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool TSigninStatusChange(string TSigninCode, int TS_iStatus)
        {
            return TSigninStatusChange(TSigninCode, TS_iStatus, null, true);
        }

        public static bool TSigninStatusChange(EntityData TS_Entity, int TS_iStatus)
        {
            return TSigninStatusChange(TS_Entity, "", TS_iStatus, null, false);
        }

        public static bool TSigninStatusChange(string TSigninCode, int TS_iStatus, int? TS_iOriginalStatus)
        {
            return TSigninStatusChange(TSigninCode, TS_iStatus, TS_iOriginalStatus, true);
        }

        public static bool TSigninStatusChange(EntityData TS_Entity, int TS_iStatus, bool TS_bSubmitData)
        {
            return TSigninStatusChange(TS_Entity, "", TS_iStatus, null, TS_bSubmitData);
        }

        public static bool TSigninStatusChange(EntityData TS_Entity, int TS_iStatus, int? TS_iOriginalStatus)
        {
            return TSigninStatusChange(TS_Entity, "", TS_iStatus, TS_iOriginalStatus, false);
        }

        public static bool TSigninStatusChange(string TSigninCode, int TS_iStatus, int? TS_iOriginalStatus, bool TS_bSubmitData)
        {
            return TSigninStatusChange(GetTSigninByCode(TSigninCode), TSigninCode, TS_iStatus, TS_iOriginalStatus, TS_bSubmitData);
        }

        public static bool TSigninStatusChange(EntityData TS_Entity, int TS_iStatus, int? TS_iOriginalStatus, bool TS_bSubmitData)
        {
            return TSigninStatusChange(TS_Entity, "", TS_iStatus, TS_iOriginalStatus, TS_bSubmitData);
        }

        public static bool TSigninStatusChange(EntityData TS_Entity, string TSigninCode, int TS_iStatus, int? TS_iOriginalStatus, bool TS_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                TS_Entity.SetCurrentTable("TSignin");
                if (TSigninCode.Trim() == "")
                {
                    if (TS_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("TSigninCode='{0}'", TSigninCode.Trim());
                    if (TS_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in TS_Entity.CurrentTable.Select(filterExpression))
                {
                    if (TS_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["State"] != DBNull.Value) && ((((int) row["State"]) == (nullable = TS_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["State"] = TS_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["State"] = TS_iStatus;
                    }
                }
                if (flag && TS_bSubmitData)
                {
                    SubmitAllTSignin(TS_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void UpdateTSignin(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TSignin"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
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

        public string Date
        {
            get
            {
                if ((this._Date == null) && (this._TSigninCode != null))
                {
                    this._GetTSigninByCode();
                }
                return this._Date;
            }
            set
            {
                if (this._Date != value)
                {
                    this._Date = value;
                }
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

        public string LeitMotiv
        {
            get
            {
                if ((this._LeitMotiv == null) && (this._TSigninCode != null))
                {
                    this._GetTSigninByCode();
                }
                return this._LeitMotiv;
            }
            set
            {
                if (this._LeitMotiv != value)
                {
                    this._LeitMotiv = value;
                }
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._TSigninCode != null))
                {
                    this._GetTSigninByCode();
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

        public string SigninDepartment
        {
            get
            {
                if ((this._SigninDepartment == null) && (this._TSigninCode != null))
                {
                    this._GetTSigninByCode();
                }
                return this._SigninDepartment;
            }
            set
            {
                if (this._SigninDepartment != value)
                {
                    this._SigninDepartment = value;
                }
            }
        }

        public string Signinman
        {
            get
            {
                if ((this._Signinman == null) && (this._TSigninCode != null))
                {
                    this._GetTSigninByCode();
                }
                return this._Signinman;
            }
            set
            {
                if (this._Signinman != value)
                {
                    this._Signinman = value;
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

        public string TSigninCode
        {
            get
            {
                return this._TSigninCode;
            }
            set
            {
                if (this._TSigninCode != value)
                {
                    this._TSigninCode = value;
                }
            }
        }
    }
}

