namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingGradeConsiderDiathesis
    {
        private string _BiddingConsiderDiathesis = null;
        private string _BiddingConsiderDiathesisCode = null;
        private string _BiddingGradeTypeCode = null;
        private string _BiddingMainDefineCode = null;
        private StandardEntityDAO _dao;
        private string _GradeGuideline = null;
        private string _ParentCode = null;
        private decimal _Percentage;
        private string _State = null;

        private void _GetBiddingGradeConsiderDiathesisByCode()
        {
            try
            {
                EntityData biddingGradeConsiderDiathesisByCode = GetBiddingGradeConsiderDiathesisByCode(this._BiddingConsiderDiathesisCode);
                this._BiddingConsiderDiathesisCode = biddingGradeConsiderDiathesisByCode.GetString("BiddingConsiderDiathesisCode");
                this._BiddingMainDefineCode = biddingGradeConsiderDiathesisByCode.GetString("BiddingMainDefineCode");
                this._ParentCode = biddingGradeConsiderDiathesisByCode.GetString("ParentCode");
                this._BiddingConsiderDiathesis = biddingGradeConsiderDiathesisByCode.GetString("BiddingConsiderDiathesis");
                this._GradeGuideline = biddingGradeConsiderDiathesisByCode.GetString("GradeGuideline");
                this._Percentage = biddingGradeConsiderDiathesisByCode.GetDecimal("Percentage");
                this._State = biddingGradeConsiderDiathesisByCode.GetString("state");
                this._BiddingGradeTypeCode = biddingGradeConsiderDiathesisByCode.GetString("BiddingGradeTypeCode");
                biddingGradeConsiderDiathesisByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetBiddingGradeConsiderDiathesiss()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BiddingGradeConsiderDiathesis");
                BiddingGradeConsiderDiathesisStrategyBuilder builder = new BiddingGradeConsiderDiathesisStrategyBuilder();
                if (this._BiddingConsiderDiathesisCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.BiddingConsiderDiathesisCode, this._BiddingConsiderDiathesisCode));
                }
                if (this._BiddingMainDefineCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.BiddingMainDefineCode, this._BiddingMainDefineCode));
                }
                if (this._ParentCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.ParentCode, this._ParentCode));
                }
                if (this._BiddingConsiderDiathesis != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.BiddingConsiderDiathesis, this._BiddingConsiderDiathesis));
                }
                if (this._GradeGuideline != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.GradeGuideline, this._GradeGuideline));
                }
                builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.Percentage, this._Percentage.ToString()));
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.state, this._State));
                }
                if (this._BiddingGradeTypeCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingGradeConsiderDiathesisStrategyName.BiddingGradeTypeCode, this._BiddingGradeTypeCode));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingConsiderDiathesisCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeConsiderDiathesis"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingGradeConsiderDiathesis";
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

        public void BiddingGradeConsiderDiathesisAdd()
        {
            if (this._dao == null)
            {
                SubmitAllBiddingGradeConsiderDiathesis(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingGradeConsiderDiathesis";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingGradeConsiderDiathesisDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteBiddingGradeConsiderDiathesis(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingGradeConsiderDiathesis";
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
                EntityData biddingGradeConsiderDiathesisByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingConsiderDiathesisCode == "")
                {
                    flag = true;
                    biddingGradeConsiderDiathesisByCode = GetBiddingGradeConsiderDiathesisByCode("");
                    this._BiddingConsiderDiathesisCode = SystemManageDAO.GetNewSysCode("BiddingGradeConsiderDiathesis");
                    newRecord = biddingGradeConsiderDiathesisByCode.GetNewRecord();
                }
                else
                {
                    biddingGradeConsiderDiathesisByCode = GetBiddingGradeConsiderDiathesisByCode(this._BiddingConsiderDiathesisCode);
                    if (biddingGradeConsiderDiathesisByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = biddingGradeConsiderDiathesisByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = biddingGradeConsiderDiathesisByCode.CurrentRow;
                    }
                }
                if (this._BiddingConsiderDiathesisCode != null)
                {
                    newRecord["BiddingConsiderDiathesisCode"] = this._BiddingConsiderDiathesisCode;
                }
                if (this._BiddingMainDefineCode != null)
                {
                    newRecord["BiddingMainDefineCode "] = this._BiddingMainDefineCode;
                }
                if (this._ParentCode != null)
                {
                    newRecord["ParentCode"] = this._ParentCode;
                }
                if (this._BiddingConsiderDiathesis != null)
                {
                    newRecord["BiddingConsiderDiathesis"] = this._BiddingConsiderDiathesis;
                }
                if (this._GradeGuideline != null)
                {
                    newRecord["GradeGuideline"] = this._GradeGuideline;
                }
                newRecord["Percentage"] = this._Percentage;
                if (this._State != null)
                {
                    newRecord["state"] = this._State;
                }
                if (this._BiddingGradeTypeCode != null)
                {
                    newRecord["BiddingGradeTypeCode"] = this._BiddingGradeTypeCode;
                }
                if (flag)
                {
                    biddingGradeConsiderDiathesisByCode.AddNewRecord(newRecord);
                }
                data2 = biddingGradeConsiderDiathesisByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteBiddingGradeConsiderDiathesis(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeConsiderDiathesis"))
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

        public static EntityData GetAllBiddingGradeConsiderDiathesis()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeConsiderDiathesis"))
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

        public static EntityData GetBiddingGradeConsiderDiathesisByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeConsiderDiathesis"))
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

        public static EntityData GetBiddingGradeConsiderDiathesisByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetBiddingGradeConsiderDiathesiss()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetBiddingGradeConsiderDiathesiss().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertBiddingGradeConsiderDiathesis(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeConsiderDiathesis"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBiddingGradeConsiderDiathesis(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeConsiderDiathesis"))
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

        public static void UpdateBiddingGradeConsiderDiathesis(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingGradeConsiderDiathesis"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BiddingConsiderDiathesis
        {
            get
            {
                return this._BiddingConsiderDiathesis;
            }
            set
            {
                if (this._BiddingConsiderDiathesis != value)
                {
                    this._BiddingConsiderDiathesis = value;
                }
            }
        }

        public string BiddingConsiderDiathesisCode
        {
            get
            {
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

        public string BiddingMainDefineCode
        {
            get
            {
                return this._BiddingMainDefineCode;
            }
            set
            {
                if (this._BiddingMainDefineCode != value)
                {
                    this._BiddingMainDefineCode = value;
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

        public string GradeGuideline
        {
            get
            {
                return this._GradeGuideline;
            }
            set
            {
                if (this._GradeGuideline != value)
                {
                    this._GradeGuideline = value;
                }
            }
        }

        public string ParentCode
        {
            get
            {
                return this._ParentCode;
            }
            set
            {
                if (this._ParentCode != value)
                {
                    this._ParentCode = value;
                }
            }
        }

        public decimal Percentage
        {
            get
            {
                return this._Percentage;
            }
            set
            {
                if (this._Percentage != value)
                {
                    this._Percentage = value;
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

