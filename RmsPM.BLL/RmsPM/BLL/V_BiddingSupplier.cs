namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class V_BiddingSupplier
    {
        private string _Abbreviation = null;
        private string _AreaCode = null;
        private string _ArtificialPerson = null;
        private string _BiddingPrejudicationCode = null;
        private string _BiddingSupplierCode = null;
        private string _CheckOpinion = null;
        private string _ContactDate = null;
        private string _ContractPerson = null;
        private string _CreatePerson = null;
        private string _CreditLevel = null;
        private StandardEntityDAO _dao;
        private string _EarlyArrearDate = null;
        private string _EMail = null;
        private string _Flag = null;
        private string _IndustrySort = null;
        private string _IndustryType = null;
        private string _LicenseID = null;
        private string _Mobile = null;
        private string _NominateDate = null;
        private string _NominateUser = null;
        private string _OfficePhone = null;
        private string _OrderCode = null;
        private string _PostCode = null;
        private string _Product = null;
        private string _Quality = null;
        private string _RegisteredAddress = null;
        private string _RegisteredCapital = null;
        private string _SJHG = null;
        private string _State = null;
        private string _SubjectSetCode = null;
        private string _SupplierCode = null;
        private string _SupplierName = null;
        private string _SupplierTypeCode = null;
        private string _TaxID = null;
        private string _TaxNo = null;
        private string _U8Code = null;
        private string _UserCode = null;
        private string _WebAddress = null;
        private string _WorkAddress = null;
        private string _WorkTimeLimit = null;

        private void _GetV_BiddingSupplierByCode()
        {
            EntityData data = this.GetV_BiddingSupplierByCode(this._BiddingSupplierCode);
            this._BiddingSupplierCode = data.GetString("BiddingSupplierCode");
            this._BiddingPrejudicationCode = data.GetString("BiddingPrejudicationCode");
            this._SupplierCode = data.GetString("SupplierCode");
            this._NominateUser = data.GetString("NominateUser");
            this._NominateDate = data.GetString("NominateDate");
            this._UserCode = data.GetString("UserCode");
            this._OrderCode = data.GetString("OrderCode");
            this._State = data.GetString("State");
            this._Flag = data.GetString("Flag");
            this._U8Code = data.GetString("U8Code");
            this._SubjectSetCode = data.GetString("SubjectSetCode");
            this._SupplierName = data.GetString("SupplierName");
            this._Abbreviation = data.GetString("Abbreviation");
            this._Quality = data.GetString("Quality");
            this._CreditLevel = data.GetString("CreditLevel");
            this._AreaCode = data.GetString("AreaCode");
            this._ContactDate = data.GetString("ContactDate");
            this._Mobile = data.GetString("Mobile");
            this._EarlyArrearDate = data.GetString("EarlyArrearDate");
            this._RegisteredCapital = data.GetString("RegisteredCapital");
            this._Product = data.GetString("Product");
            this._CheckOpinion = data.GetString("CheckOpinion");
            this._SupplierTypeCode = data.GetString("SupplierTypeCode");
            this._ArtificialPerson = data.GetString("ArtificialPerson");
            this._ContractPerson = data.GetString("ContractPerson");
            this._OfficePhone = data.GetString("OfficePhone");
            this._RegisteredAddress = data.GetString("RegisteredAddress");
            this._IndustryType = data.GetString("IndustryType");
            this._IndustrySort = data.GetString("IndustrySort");
            this._SJHG = data.GetString("SJHG");
            this._LicenseID = data.GetString("LicenseID");
            this._TaxID = data.GetString("TaxID");
            this._TaxNo = data.GetString("TaxNo");
            this._WorkAddress = data.GetString("WorkAddress");
            this._WorkTimeLimit = data.GetString("WorkTimeLimit");
            this._PostCode = data.GetString("PostCode");
            this._EMail = data.GetString("EMail");
            this._WebAddress = data.GetString("WebAddress");
            this._CreatePerson = data.GetString("CreatePerson");
            data.Dispose();
        }

        private EntityData _GetV_BiddingSuppliers()
        {
            EntityData entitydata = new EntityData("V_BiddingSupplier");
            V_BiddingSupplierStrategyBuilder builder = new V_BiddingSupplierStrategyBuilder();
            if (this._BiddingSupplierCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.BiddingSupplierCode, this._BiddingSupplierCode));
            }
            if (this._BiddingPrejudicationCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.BiddingPrejudicationCode, this._BiddingPrejudicationCode));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._NominateUser != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.NominateUser, this._NominateUser));
            }
            if (this._NominateDate != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.NominateDate, this._NominateDate));
            }
            if (this._UserCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.UserCode, this._UserCode));
            }
            if (this._OrderCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.OrderCode, this._OrderCode));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.Flag, this._Flag));
            }
            if (this._U8Code != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.U8Code, this._U8Code));
            }
            if (this._SubjectSetCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.SubjectSetCode, this._SubjectSetCode));
            }
            if (this._SupplierName != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.SupplierName, this._SupplierName));
            }
            if (this._Abbreviation != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.Abbreviation, this._Abbreviation));
            }
            if (this._Quality != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.Quality, this._Quality));
            }
            if (this._CreditLevel != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.CreditLevel, this._CreditLevel));
            }
            if (this._AreaCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.AreaCode, this._AreaCode));
            }
            if (this._ContactDate != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.ContactDate, this._ContactDate));
            }
            if (this._Mobile != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.Mobile, this._Mobile));
            }
            if (this._EarlyArrearDate != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.EarlyArrearDate, this._EarlyArrearDate));
            }
            if (this._RegisteredCapital != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.RegisteredCapital, this._RegisteredCapital));
            }
            if (this._Product != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.Product, this._Product));
            }
            if (this._CheckOpinion != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.CheckOpinion, this._CheckOpinion));
            }
            if (this._SupplierTypeCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.SupplierTypeCode, this._SupplierTypeCode));
            }
            if (this._ArtificialPerson != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.ArtificialPerson, this._ArtificialPerson));
            }
            if (this._ContractPerson != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.ContractPerson, this._ContractPerson));
            }
            if (this._OfficePhone != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.OfficePhone, this._OfficePhone));
            }
            if (this._RegisteredAddress != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.RegisteredAddress, this._RegisteredAddress));
            }
            if (this._IndustryType != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.IndustryType, this._IndustryType));
            }
            if (this._IndustrySort != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.IndustrySort, this._IndustrySort));
            }
            if (this._SJHG != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.SJHG, this._SJHG));
            }
            if (this._LicenseID != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.LicenseID, this._LicenseID));
            }
            if (this._TaxID != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.TaxID, this._TaxID));
            }
            if (this._TaxNo != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.TaxNo, this._TaxNo));
            }
            if (this._WorkAddress != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.WorkAddress, this._WorkAddress));
            }
            if (this._WorkTimeLimit != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.WorkTimeLimit, this._WorkTimeLimit));
            }
            if (this._PostCode != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.PostCode, this._PostCode));
            }
            if (this._EMail != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.EMail, this._EMail));
            }
            if (this._WebAddress != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.WebAddress, this._WebAddress));
            }
            if (this._CreatePerson != null)
            {
                builder.AddStrategy(new Strategy(V_BiddingSupplierStrategyName.CreatePerson, this._CreatePerson));
            }
            string queryString = builder.BuildMainQueryString() + " order by OrderCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("V_BiddingSupplier", queryString);
            }
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData data;
            DataRow newRecord;
            bool flag = false;
            if (this._BiddingSupplierCode == "")
            {
                flag = true;
                data = this.GetV_BiddingSupplierByCode("");
                this._BiddingSupplierCode = SystemManageDAO.GetNewSysCode("V_BiddingSupplier");
                newRecord = data.GetNewRecord();
            }
            else
            {
                data = this.GetV_BiddingSupplierByCode(this._BiddingSupplierCode);
                newRecord = data.CurrentRow;
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
            if (this._U8Code != null)
            {
                newRecord["U8Code"] = this._U8Code;
            }
            if (this._SubjectSetCode != null)
            {
                newRecord["SubjectSetCode"] = this._SubjectSetCode;
            }
            if (this._SupplierName != null)
            {
                newRecord["SupplierName"] = this._SupplierName;
            }
            if (this._Abbreviation != null)
            {
                newRecord["Abbreviation"] = this._Abbreviation;
            }
            if (this._Quality != null)
            {
                newRecord["Quality"] = this._Quality;
            }
            if (this._CreditLevel != null)
            {
                newRecord["CreditLevel"] = this._CreditLevel;
            }
            if (this._AreaCode != null)
            {
                newRecord["AreaCode"] = this._AreaCode;
            }
            if (this._ContactDate != null)
            {
                newRecord["ContactDate"] = this._ContactDate;
            }
            if (this._Mobile != null)
            {
                newRecord["Mobile"] = this._Mobile;
            }
            if (this._EarlyArrearDate != null)
            {
                newRecord["EarlyArrearDate"] = this._EarlyArrearDate;
            }
            if (this._RegisteredCapital != null)
            {
                newRecord["RegisteredCapital"] = this._RegisteredCapital;
            }
            if (this._Product != null)
            {
                newRecord["Product"] = this._Product;
            }
            if (this._CheckOpinion != null)
            {
                newRecord["CheckOpinion"] = this._CheckOpinion;
            }
            if (this._SupplierTypeCode != null)
            {
                newRecord["SupplierTypeCode"] = this._SupplierTypeCode;
            }
            if (this._ArtificialPerson != null)
            {
                newRecord["ArtificialPerson"] = this._ArtificialPerson;
            }
            if (this._ContractPerson != null)
            {
                newRecord["ContractPerson"] = this._ContractPerson;
            }
            if (this._OfficePhone != null)
            {
                newRecord["OfficePhone"] = this._OfficePhone;
            }
            if (this._RegisteredAddress != null)
            {
                newRecord["RegisteredAddress"] = this._RegisteredAddress;
            }
            if (this._IndustryType != null)
            {
                newRecord["IndustryType"] = this._IndustryType;
            }
            if (this._IndustrySort != null)
            {
                newRecord["IndustrySort"] = this._IndustrySort;
            }
            if (this._SJHG != null)
            {
                newRecord["SJHG"] = this._SJHG;
            }
            if (this._LicenseID != null)
            {
                newRecord["LicenseID"] = this._LicenseID;
            }
            if (this._TaxID != null)
            {
                newRecord["TaxID"] = this._TaxID;
            }
            if (this._TaxNo != null)
            {
                newRecord["TaxNo"] = this._TaxNo;
            }
            if (this._WorkAddress != null)
            {
                newRecord["WorkAddress"] = this._WorkAddress;
            }
            if (this._WorkTimeLimit != null)
            {
                newRecord["WorkTimeLimit"] = this._WorkTimeLimit;
            }
            if (this._PostCode != null)
            {
                newRecord["PostCode"] = this._PostCode;
            }
            if (this._EMail != null)
            {
                newRecord["EMail"] = this._EMail;
            }
            if (this._WebAddress != null)
            {
                newRecord["WebAddress"] = this._WebAddress;
            }
            if (this._CreatePerson != null)
            {
                newRecord["CreatePerson"] = this._CreatePerson;
            }
            if (flag)
            {
                data.AddNewRecord(newRecord);
            }
            return data;
        }

        private void DeleteV_BiddingSupplier(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BiddingSupplier"))
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

        private EntityData GetAllV_BiddingSupplier()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BiddingSupplier"))
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

        private EntityData GetV_BiddingSupplierByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BiddingSupplier"))
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

        private EntityData GetV_BiddingSupplierByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "V_BiddingSupplier";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetV_BiddingSuppliers()
        {
            return this._GetV_BiddingSuppliers().CurrentTable;
        }

        private void InsertV_BiddingSupplier(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BiddingSupplier"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllV_BiddingSupplier(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BiddingSupplier"))
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

        private void UpdateV_BiddingSupplier(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BiddingSupplier"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void V_BiddingSupplierAdd()
        {
            if (this._BiddingSupplierCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllV_BiddingSupplier(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "V_BiddingSupplier";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void V_BiddingSupplierDelete()
        {
            if (this._dao == null)
            {
                this.DeleteV_BiddingSupplier(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "V_BiddingSupplier";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void V_BiddingSupplierSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllV_BiddingSupplier(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "V_BiddingSupplier";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void V_BiddingSupplierUpdate()
        {
            if (this._BiddingSupplierCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllV_BiddingSupplier(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "V_BiddingSupplier";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public string Abbreviation
        {
            get
            {
                if ((this._Abbreviation == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._Abbreviation;
            }
            set
            {
                this._Abbreviation = value;
            }
        }

        public string AreaCode
        {
            get
            {
                if ((this._AreaCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._AreaCode;
            }
            set
            {
                this._AreaCode = value;
            }
        }

        public string ArtificialPerson
        {
            get
            {
                if ((this._ArtificialPerson == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._ArtificialPerson;
            }
            set
            {
                this._ArtificialPerson = value;
            }
        }

        public string BiddingPrejudicationCode
        {
            get
            {
                if ((this._BiddingPrejudicationCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
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

        public string CheckOpinion
        {
            get
            {
                if ((this._CheckOpinion == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._CheckOpinion;
            }
            set
            {
                this._CheckOpinion = value;
            }
        }

        public string ContactDate
        {
            get
            {
                if ((this._ContactDate == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._ContactDate;
            }
            set
            {
                this._ContactDate = value;
            }
        }

        public string ContractPerson
        {
            get
            {
                if ((this._ContractPerson == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._ContractPerson;
            }
            set
            {
                this._ContractPerson = value;
            }
        }

        public string CreatePerson
        {
            get
            {
                if ((this._CreatePerson == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._CreatePerson;
            }
            set
            {
                this._CreatePerson = value;
            }
        }

        public string CreditLevel
        {
            get
            {
                if ((this._CreditLevel == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._CreditLevel;
            }
            set
            {
                this._CreditLevel = value;
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

        public string EarlyArrearDate
        {
            get
            {
                if ((this._EarlyArrearDate == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._EarlyArrearDate;
            }
            set
            {
                this._EarlyArrearDate = value;
            }
        }

        public string EMail
        {
            get
            {
                if ((this._EMail == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._EMail;
            }
            set
            {
                this._EMail = value;
            }
        }

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string IndustrySort
        {
            get
            {
                if ((this._IndustrySort == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._IndustrySort;
            }
            set
            {
                this._IndustrySort = value;
            }
        }

        public string IndustryType
        {
            get
            {
                if ((this._IndustryType == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._IndustryType;
            }
            set
            {
                this._IndustryType = value;
            }
        }

        public string LicenseID
        {
            get
            {
                if ((this._LicenseID == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._LicenseID;
            }
            set
            {
                this._LicenseID = value;
            }
        }

        public string Mobile
        {
            get
            {
                if ((this._Mobile == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._Mobile;
            }
            set
            {
                this._Mobile = value;
            }
        }

        public string NominateDate
        {
            get
            {
                if ((this._NominateDate == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
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
                    this._GetV_BiddingSupplierByCode();
                }
                return this._NominateUser;
            }
            set
            {
                this._NominateUser = value;
            }
        }

        public string OfficePhone
        {
            get
            {
                if ((this._OfficePhone == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._OfficePhone;
            }
            set
            {
                this._OfficePhone = value;
            }
        }

        public string OrderCode
        {
            get
            {
                if ((this._OrderCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._OrderCode;
            }
            set
            {
                this._OrderCode = value;
            }
        }

        public string PostCode
        {
            get
            {
                if ((this._PostCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._PostCode;
            }
            set
            {
                this._PostCode = value;
            }
        }

        public string Product
        {
            get
            {
                if ((this._Product == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._Product;
            }
            set
            {
                this._Product = value;
            }
        }

        public string Quality
        {
            get
            {
                if ((this._Quality == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._Quality;
            }
            set
            {
                this._Quality = value;
            }
        }

        public string RegisteredAddress
        {
            get
            {
                if ((this._RegisteredAddress == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._RegisteredAddress;
            }
            set
            {
                this._RegisteredAddress = value;
            }
        }

        public string RegisteredCapital
        {
            get
            {
                if ((this._RegisteredCapital == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._RegisteredCapital;
            }
            set
            {
                this._RegisteredCapital = value;
            }
        }

        public string SJHG
        {
            get
            {
                if ((this._SJHG == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._SJHG;
            }
            set
            {
                this._SJHG = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string SubjectSetCode
        {
            get
            {
                if ((this._SubjectSetCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._SubjectSetCode;
            }
            set
            {
                this._SubjectSetCode = value;
            }
        }

        public string SupplierCode
        {
            get
            {
                if ((this._SupplierCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }

        public string SupplierName
        {
            get
            {
                if ((this._SupplierName == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._SupplierName;
            }
            set
            {
                this._SupplierName = value;
            }
        }

        public string SupplierTypeCode
        {
            get
            {
                if ((this._SupplierTypeCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._SupplierTypeCode;
            }
            set
            {
                this._SupplierTypeCode = value;
            }
        }

        public string TaxID
        {
            get
            {
                if ((this._TaxID == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._TaxID;
            }
            set
            {
                this._TaxID = value;
            }
        }

        public string TaxNo
        {
            get
            {
                if ((this._TaxNo == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._TaxNo;
            }
            set
            {
                this._TaxNo = value;
            }
        }

        public string U8Code
        {
            get
            {
                if ((this._U8Code == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._U8Code;
            }
            set
            {
                this._U8Code = value;
            }
        }

        public string UserCode
        {
            get
            {
                if ((this._UserCode == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }

        public string WebAddress
        {
            get
            {
                if ((this._WebAddress == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._WebAddress;
            }
            set
            {
                this._WebAddress = value;
            }
        }

        public string WorkAddress
        {
            get
            {
                if ((this._WorkAddress == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._WorkAddress;
            }
            set
            {
                this._WorkAddress = value;
            }
        }

        public string WorkTimeLimit
        {
            get
            {
                if ((this._WorkTimeLimit == null) && (this._BiddingSupplierCode != null))
                {
                    this._GetV_BiddingSupplierByCode();
                }
                return this._WorkTimeLimit;
            }
            set
            {
                this._WorkTimeLimit = value;
            }
        }
    }
}

