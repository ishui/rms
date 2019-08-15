namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingSupplier
    {
        private string _BiddingPrejudicationCode = null;
        private string _BiddingSupplierCode = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _NominateDate = null;
        private string _NominateUser = null;
        private string _OrderCode = null;
        private string _State = null;
        private string _SupplierCode = null;
        private string _UserCode = null;

        private void _GetBiddingSupplierByCode()
        {
            EntityData biddingSupplierByCode = this.GetBiddingSupplierByCode(this._BiddingSupplierCode);
            this._BiddingSupplierCode = biddingSupplierByCode.GetString("BiddingSupplierCode");
            this._BiddingPrejudicationCode = biddingSupplierByCode.GetString("BiddingPrejudicationCode");
            this._SupplierCode = biddingSupplierByCode.GetString("SupplierCode");
            this._NominateUser = biddingSupplierByCode.GetString("NominateUser");
            this._NominateDate = biddingSupplierByCode.GetDateTimeOnlyDate("NominateDate");
            this._UserCode = biddingSupplierByCode.GetString("UserCode");
            this._OrderCode = biddingSupplierByCode.GetIntString("OrderCode");
            this._State = biddingSupplierByCode.GetString("State");
            this._Flag = biddingSupplierByCode.GetString("Flag");
            biddingSupplierByCode.Dispose();
        }

        public EntityData _GetBiddingSuppliers()
        {
            EntityData entitydata = new EntityData("BiddingSupplier");
            BiddingSupplierStrategyBuilder builder = new BiddingSupplierStrategyBuilder();
            if (this._BiddingSupplierCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.BiddingSupplierCode, this._BiddingSupplierCode));
            }
            if (this._BiddingPrejudicationCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.BiddingPrejudicationCode, this._BiddingPrejudicationCode));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._NominateUser != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.NominateUser, this._NominateUser));
            }
            if (this._NominateDate != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.NominateDate, this._NominateDate));
            }
            if (this._UserCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.UserCode, this._UserCode));
            }
            if (this._OrderCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.OrderCode, this._OrderCode));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.Flag, this._Flag));
            }
            string sqlString = builder.BuildMainQueryString() + " order by OrderCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "BiddingSupplier";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public EntityData _GetBiddingSuppliersOrderbyPy()
        {
            EntityData entitydata = new EntityData("BiddingSupplier");
            BiddingSupplierStrategyBuilder builder = new BiddingSupplierStrategyBuilder();
            if (this._BiddingSupplierCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.BiddingSupplierCode, this._BiddingSupplierCode));
            }
            if (this._BiddingPrejudicationCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.BiddingPrejudicationCode, this._BiddingPrejudicationCode));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._NominateUser != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.NominateUser, this._NominateUser));
            }
            if (this._NominateDate != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.NominateDate, this._NominateDate));
            }
            if (this._UserCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.UserCode, this._UserCode));
            }
            if (this._OrderCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.OrderCode, this._OrderCode));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(BiddingSupplierStrategyName.Flag, this._Flag));
            }
            string sqlString = builder.BuildMainQueryString() + " order by BiddingSupplierCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "BiddingSupplier";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public void BiddingSupplierAdd()
        {
            if (this._BiddingSupplierCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingSupplier(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingSupplier";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void BiddingSupplierDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBiddingSupplier(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingSupplier";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void BiddingSupplierSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingSupplier(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingSupplier";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingSupplierUpdate()
        {
            if (this._BiddingSupplierCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingSupplier(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingSupplier";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData biddingSupplierByCode;
            DataRow newRecord;
            bool flag = false;
            if ((this._BiddingSupplierCode == "") || (this._BiddingSupplierCode == null))
            {
                flag = true;
                if (this._dao == null)
                {
                    biddingSupplierByCode = this.GetBiddingSupplierByCode("");
                }
                else
                {
                    biddingSupplierByCode = this.dao.SelectbyPrimaryKey("");
                }
                this._BiddingSupplierCode = SystemManageDAO.GetNewSysCode("BiddingSupplier");
                newRecord = biddingSupplierByCode.GetNewRecord();
            }
            else
            {
                if (this.dao == null)
                {
                    biddingSupplierByCode = this.GetBiddingSupplierByCode(this._BiddingSupplierCode);
                }
                else
                {
                    this.dao.EntityName = "BiddingSupplier";
                    biddingSupplierByCode = this.dao.SelectbyPrimaryKey(this.BiddingSupplierCode);
                }
                newRecord = biddingSupplierByCode.CurrentRow;
            }
            if (this._BiddingSupplierCode != null)
            {
                newRecord["BiddingSupplierCode"] = this._BiddingSupplierCode;
            }
            if (this._BiddingPrejudicationCode != null)
            {
                newRecord["BiddingPrejudicationCode"] = this._BiddingPrejudicationCode;
            }
            if (this._SupplierCode != null)
            {
                newRecord["SupplierCode"] = this._SupplierCode;
            }
            if (this._NominateUser != null)
            {
                newRecord["NominateUser"] = this._NominateUser;
            }
            if (this._NominateDate != null)
            {
                newRecord["NominateDate"] = this._NominateDate;
            }
            if (this._UserCode != null)
            {
                newRecord["UserCode"] = this._UserCode;
            }
            if (this._OrderCode != null)
            {
                newRecord["OrderCode"] = this._OrderCode;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (flag)
            {
                biddingSupplierByCode.AddNewRecord(newRecord);
            }
            return biddingSupplierByCode;
        }

        private void DeleteBiddingSupplier(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
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

        private EntityData GetAllBiddingSupplier()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
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

        public EntityData GetBiddingSupplierByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
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

        private EntityData GetBiddingSupplierByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "BiddingSupplier";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static DataTable GetBiddingSupplierByPrejudicationCode(string PrejudicatonCode)
        {
            BiddingSupplier supplier = new BiddingSupplier();
            supplier.BiddingPrejudicationCode = PrejudicatonCode;
            return supplier.GetBiddingSuppliersOrderBypy();
        }

        public DataTable GetBiddingSuppliers()
        {
            return this._GetBiddingSuppliers().CurrentTable;
        }

        public DataTable GetBiddingSuppliersOrderBypy()
        {
            return this._GetBiddingSuppliersOrderbyPy().CurrentTable;
        }

        private void InsertBiddingSupplier(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllBiddingSupplier(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
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

        private void UpdateBiddingSupplier(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingSupplier"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BiddingPrejudicationCode
        {
            get
            {
                if ((this._BiddingPrejudicationCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._BiddingPrejudicationCode;
            }
            set
            {
                this._BiddingPrejudicationCode = value;
            }
        }

        public string BiddingSupplierCode
        {
            get
            {
                return this._BiddingSupplierCode;
            }
            set
            {
                this._BiddingSupplierCode = value;
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
                if ((this._Flag == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string NominateDate
        {
            get
            {
                if ((this._NominateDate == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._NominateDate;
            }
            set
            {
                this._NominateDate = value;
            }
        }

        public string NominateUser
        {
            get
            {
                if ((this._NominateUser == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._NominateUser;
            }
            set
            {
                this._NominateUser = value;
            }
        }

        public string OrderCode
        {
            get
            {
                if ((this._OrderCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._OrderCode;
            }
            set
            {
                this._OrderCode = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string SupplierCode
        {
            get
            {
                if ((this._SupplierCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }

        public string UserCode
        {
            get
            {
                if ((this._UserCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetBiddingSupplierByCode();
                }
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }
    }
}

