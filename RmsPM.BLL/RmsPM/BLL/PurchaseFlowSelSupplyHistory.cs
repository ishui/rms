namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class PurchaseFlowSelSupplyHistory
    {
        private string _AppraiseGather = null;
        private string _Bidding = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _PaymentCondition = null;
        private string _PprovidePeriods = null;
        private string _PurchaseFlowSelSupplyCode = null;
        private string _PurchaseFlowSelSupplyHistoryCode = null;
        private string _SupplierCode = null;
        private string _TechnologyWindage = null;
        private string _VarietyProducing = null;

        private void _GetPurchaseFlowSelSupplyHistoryByCode()
        {
            EntityData purchaseFlowSelSupplyHistoryByCode = this.GetPurchaseFlowSelSupplyHistoryByCode(this._PurchaseFlowSelSupplyHistoryCode);
            this._PurchaseFlowSelSupplyHistoryCode = purchaseFlowSelSupplyHistoryByCode.GetString("PurchaseFlowSelSupplyHistoryCode");
            this._PurchaseFlowSelSupplyCode = purchaseFlowSelSupplyHistoryByCode.GetString("PurchaseFlowSelSupplyCode");
            this._SupplierCode = purchaseFlowSelSupplyHistoryByCode.GetString("SupplierCode");
            this._Bidding = purchaseFlowSelSupplyHistoryByCode.GetString("Bidding");
            this._VarietyProducing = purchaseFlowSelSupplyHistoryByCode.GetString("VarietyProducing");
            this._PprovidePeriods = purchaseFlowSelSupplyHistoryByCode.GetString("PprovidePeriods");
            this._PaymentCondition = purchaseFlowSelSupplyHistoryByCode.GetString("PaymentCondition");
            this._TechnologyWindage = purchaseFlowSelSupplyHistoryByCode.GetString("TechnologyWindage");
            this._AppraiseGather = purchaseFlowSelSupplyHistoryByCode.GetString("AppraiseGather");
            this._Flag = purchaseFlowSelSupplyHistoryByCode.GetString("Flag");
            purchaseFlowSelSupplyHistoryByCode.Dispose();
        }

        private EntityData _GetPurchaseFlowSelSupplyHistorys()
        {
            EntityData entitydata = new EntityData("PurchaseFlowSelSupplyHistory");
            PurchaseFlowSelSupplyHistoryStrategyBuilder builder = new PurchaseFlowSelSupplyHistoryStrategyBuilder();
            if (this._PurchaseFlowSelSupplyHistoryCode != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.PurchaseFlowSelSupplyHistoryCode, this._PurchaseFlowSelSupplyHistoryCode));
            }
            if (this._PurchaseFlowSelSupplyCode != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.PurchaseFlowSelSupplyCode, this._PurchaseFlowSelSupplyCode));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._Bidding != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.Bidding, this._Bidding));
            }
            if (this._VarietyProducing != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.VarietyProducing, this._VarietyProducing));
            }
            if (this._PprovidePeriods != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.PprovidePeriods, this._PprovidePeriods));
            }
            if (this._PaymentCondition != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.PaymentCondition, this._PaymentCondition));
            }
            if (this._TechnologyWindage != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.TechnologyWindage, this._TechnologyWindage));
            }
            if (this._AppraiseGather != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.AppraiseGather, this._AppraiseGather));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(PurchaseFlowSelSupplyHistoryStrategyName.Flag, this._Flag));
            }
            string queryString = builder.BuildMainQueryString() + " order by PurchaseFlowSelSupplyHistoryCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("PurchaseFlowSelSupplyHistory", queryString);
            }
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData purchaseFlowSelSupplyHistoryByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._PurchaseFlowSelSupplyHistoryCode == "")
            {
                flag = true;
                purchaseFlowSelSupplyHistoryByCode = this.GetPurchaseFlowSelSupplyHistoryByCode("");
                this._PurchaseFlowSelSupplyHistoryCode = SystemManageDAO.GetNewSysCode("PurchaseFlowSelSupplyHistory");
                newRecord = purchaseFlowSelSupplyHistoryByCode.GetNewRecord();
            }
            else
            {
                purchaseFlowSelSupplyHistoryByCode = this.GetPurchaseFlowSelSupplyHistoryByCode(this._PurchaseFlowSelSupplyHistoryCode);
                newRecord = purchaseFlowSelSupplyHistoryByCode.CurrentRow;
            }
            if (this._PurchaseFlowSelSupplyHistoryCode != null)
            {
                newRecord["PurchaseFlowSelSupplyHistoryCode"] = this._PurchaseFlowSelSupplyHistoryCode;
            }
            if (this._PurchaseFlowSelSupplyCode != null)
            {
                newRecord["PurchaseFlowSelSupplyCode"] = this._PurchaseFlowSelSupplyCode;
            }
            if (this._SupplierCode != null)
            {
                newRecord["SupplierCode"] = this._SupplierCode;
            }
            if (this._Bidding != null)
            {
                newRecord["Bidding"] = this._Bidding;
            }
            if (this._VarietyProducing != null)
            {
                newRecord["VarietyProducing"] = this._VarietyProducing;
            }
            if (this._PprovidePeriods != null)
            {
                newRecord["PprovidePeriods"] = this._PprovidePeriods;
            }
            if (this._PaymentCondition != null)
            {
                newRecord["PaymentCondition"] = this._PaymentCondition;
            }
            if (this._TechnologyWindage != null)
            {
                newRecord["TechnologyWindage"] = this._TechnologyWindage;
            }
            if (this._AppraiseGather != null)
            {
                newRecord["AppraiseGather"] = this._AppraiseGather;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (flag)
            {
                purchaseFlowSelSupplyHistoryByCode.AddNewRecord(newRecord);
            }
            return purchaseFlowSelSupplyHistoryByCode;
        }

        private void DeletePurchaseFlowSelSupplyHistory(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PurchaseFlowSelSupplyHistory"))
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

        private EntityData GetAllPurchaseFlowSelSupplyHistory()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PurchaseFlowSelSupplyHistory"))
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

        private EntityData GetPurchaseFlowSelSupplyHistoryByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PurchaseFlowSelSupplyHistory"))
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

        private EntityData GetPurchaseFlowSelSupplyHistoryByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "PurchaseFlowSelSupplyHistory";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetPurchaseFlowSelSupplyHistorys()
        {
            return this._GetPurchaseFlowSelSupplyHistorys().CurrentTable;
        }

        public void InsertPurchaseFlowSelSupplyHistory(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PurchaseFlowSelSupplyHistory"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void PurchaseFlowSelSupplyHistoryAdd()
        {
            if (this._PurchaseFlowSelSupplyHistoryCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllPurchaseFlowSelSupplyHistory(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "PurchaseFlowSelSupplyHistory";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void PurchaseFlowSelSupplyHistoryDelete()
        {
            if (this._dao == null)
            {
                this.DeletePurchaseFlowSelSupplyHistory(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "PurchaseFlowSelSupplyHistory";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void PurchaseFlowSelSupplyHistorySubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllPurchaseFlowSelSupplyHistory(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "PurchaseFlowSelSupplyHistory";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void PurchaseFlowSelSupplyHistoryUpdate()
        {
            if (this._PurchaseFlowSelSupplyHistoryCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllPurchaseFlowSelSupplyHistory(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "PurchaseFlowSelSupplyHistory";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void SubmitAllPurchaseFlowSelSupplyHistory(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PurchaseFlowSelSupplyHistory"))
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

        private void UpdatePurchaseFlowSelSupplyHistory(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PurchaseFlowSelSupplyHistory"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string AppraiseGather
        {
            get
            {
                if ((this._AppraiseGather == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._AppraiseGather;
            }
            set
            {
                this._AppraiseGather = value;
            }
        }

        public string Bidding
        {
            get
            {
                if ((this._Bidding == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._Bidding;
            }
            set
            {
                this._Bidding = value;
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
                if ((this._Flag == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string PaymentCondition
        {
            get
            {
                if ((this._PaymentCondition == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._PaymentCondition;
            }
            set
            {
                this._PaymentCondition = value;
            }
        }

        public string PprovidePeriods
        {
            get
            {
                if ((this._PprovidePeriods == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._PprovidePeriods;
            }
            set
            {
                this._PprovidePeriods = value;
            }
        }

        public string PurchaseFlowSelSupplyCode
        {
            get
            {
                if ((this._PurchaseFlowSelSupplyCode == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._PurchaseFlowSelSupplyCode;
            }
            set
            {
                this._PurchaseFlowSelSupplyCode = value;
            }
        }

        public string SupplierCode
        {
            get
            {
                if ((this._SupplierCode == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }

        public string TechnologyWindage
        {
            get
            {
                if ((this._TechnologyWindage == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._TechnologyWindage;
            }
            set
            {
                this._TechnologyWindage = value;
            }
        }

        public string VarietyProducing
        {
            get
            {
                if ((this._VarietyProducing == null) && (this._PurchaseFlowSelSupplyHistoryCode != null))
                {
                    this._GetPurchaseFlowSelSupplyHistoryByCode();
                }
                return this._VarietyProducing;
            }
            set
            {
                this._VarietyProducing = value;
            }
        }
    }
}

