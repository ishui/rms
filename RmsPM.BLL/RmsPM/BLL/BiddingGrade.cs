namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingGrade
    {
        private string _BiddingConsiderDiathesisCode = null;
        private string _BiddingGradeCode = null;
        private string _BiddingGradeMessageCode = null;
        private StandardEntityDAO _dao;
        private int _GradePoint;

        private void _GetBiddingGradeByCode()
        {
            try
            {
                EntityData biddingGradeByCode = GetBiddingGradeByCode(this._BiddingGradeCode);
                this._BiddingGradeCode = biddingGradeByCode.GetString("BiddingGradeCode");
                this._BiddingGradeMessageCode = biddingGradeByCode.GetString("BiddingGradeMessageCode");
                this._BiddingConsiderDiathesisCode = biddingGradeByCode.GetString("BiddingConsiderDiathesisCode");
                this._GradePoint = biddingGradeByCode.GetInt("GradePoint");
                biddingGradeByCode.Dispose();
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
                EntityData entitydata = new EntityData("BiddingGrade");
                BiddingGradeStrategyBuilder builder = new BiddingGradeStrategyBuilder();
                if (this._BiddingGradeCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeStrategyName.BiddingGradeCode, this._BiddingGradeCode));
                }
                if (this._BiddingGradeMessageCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeStrategyName.BiddingGradeMessageCode, this._BiddingGradeMessageCode));
                }
                if (this._BiddingConsiderDiathesisCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeStrategyName.BiddingConsiderDiathesisCode, this._BiddingConsiderDiathesisCode));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingGradeCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGrade"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingGrade";
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

        public void BiddingGradeAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingGrade(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingGrade";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingGradeDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingGrade(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingGrade";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingGradeUpdate()
        {
            if (this._BiddingGradeCode != null)
            {
                if (this._dao == null)
                {
                    SubmitAllBiddingGrade(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingGrade";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData biddingGradeByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingGradeCode == "")
                {
                    flag = true;
                    biddingGradeByCode = GetBiddingGradeByCode("");
                    this._BiddingGradeCode = SystemManageDAO.GetNewSysCode("BiddingGrade");
                    newRecord = biddingGradeByCode.GetNewRecord();
                }
                else
                {
                    biddingGradeByCode = GetBiddingGradeByCode(this._BiddingGradeCode);
                    if (biddingGradeByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingGradeByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingGradeByCode.CurrentRow;
                    }
                }
                if (this._BiddingGradeCode != null)
                {
                    newRecord["BiddingGradeCode"] = this._BiddingGradeCode;
                }
                if (this._BiddingGradeMessageCode != null)
                {
                    newRecord["BiddingGradeMessageCode"] = this._BiddingGradeMessageCode;
                }
                if (this._BiddingConsiderDiathesisCode != null)
                {
                    newRecord["BiddingConsiderDiathesisCode"] = this._BiddingConsiderDiathesisCode;
                }
                newRecord["GradePoint"] = this._GradePoint;
                if (flag)
                {
                    biddingGradeByCode.AddNewRecord(newRecord);
                }
                data2 = biddingGradeByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingGrade(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGrade"))
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

        public static EntityData GetAllBiddingGrade()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGrade"))
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

        public static EntityData GetBiddingGradeByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGrade"))
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

        public static EntityData GetBiddingGradeByCode(string code, StandardEntityDAO dao)
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

        public static void InsertBiddingGrade(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGrade"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingGrade(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGrade"))
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

        public static void UpdateBiddingGrade(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGrade"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BiddingConsiderDiathesisCode
        {
            get
            {
                if ((this._BiddingConsiderDiathesisCode == null) && (this._BiddingGradeCode != null))
                {
                    this._GetBiddingGradeByCode();
                }
                return this._BiddingConsiderDiathesisCode;
            }
            set
            {
                if (this._BiddingConsiderDiathesisCode != value)
                {
                    this._BiddingConsiderDiathesisCode = value;
                }
            }
        }

        public string BiddingGradeCode
        {
            get
            {
                return this._BiddingGradeCode;
            }
            set
            {
                if (this._BiddingGradeCode != value)
                {
                    this._BiddingGradeCode = value;
                }
            }
        }

        public string BiddingGradeMessageCode
        {
            get
            {
                if ((this._BiddingGradeMessageCode == null) && (this._BiddingGradeCode != null))
                {
                    this._GetBiddingGradeByCode();
                }
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

        public int GradePoint
        {
            get
            {
                int gradePoint = this.GradePoint;
                return this.GradePoint;
            }
            set
            {
                if (this._GradePoint != value)
                {
                    this._GradePoint = value;
                }
            }
        }
    }
}

