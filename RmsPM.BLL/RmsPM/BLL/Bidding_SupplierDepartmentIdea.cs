namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class Bidding_SupplierDepartmentIdea
    {
        private string _BiddingPrejudicationCode = null;
        private string _BiddingSupplierCode = null;
        private StandardEntityDAO _dao;
        private string _Depart_Agreement = null;
        private string _Depart_Build = null;
        private string _Depart_Project = null;
        private string _DepartmentIdearID = null;
        private string _DepartmentRemark = null;
        private string _DepartmentRemark1 = null;
        private string _DepartmentRemark2 = null;
        private string _DepartmentRemark3 = null;
        private string _Director_Agreement = null;
        private string _Director_Finance = null;
        private string _Director_Project = null;
        private string _Flag = null;
        private string _Md_Agreement = null;
        private string _Md_Item = null;
        private string _Md_Project = null;

        private void _GetBidding_SupplierDepartmentIdeaByCode()
        {
            EntityData data = this.GetBidding_SupplierDepartmentIdeaByCode(this._DepartmentIdearID);
            this._DepartmentIdearID = data.GetString("DepartmentIdearID");
            this._BiddingSupplierCode = data.GetString("BiddingSupplierCode");
            this._Flag = data.GetString("Flag");
            this._Depart_Build = data.GetString("Depart_Build");
            this._Depart_Project = data.GetString("Depart_Project");
            this._Depart_Agreement = data.GetString("Depart_Agreement");
            this._Md_Item = data.GetString("Md_Item");
            this._Md_Project = data.GetString("Md_Project");
            this._Md_Agreement = data.GetString("Md_Agreement");
            this._Director_Project = data.GetString("Director_Project");
            this._Director_Agreement = data.GetString("Director_Agreement");
            this._Director_Finance = data.GetString("Director_Finance");
            this._DepartmentRemark = data.GetString("DepartmentRemark");
            this._DepartmentRemark1 = data.GetString("DepartmentRemark1");
            this._DepartmentRemark2 = data.GetString("DepartmentRemark2");
            this._DepartmentRemark3 = data.GetString("DepartmentRemark3");
            this._BiddingPrejudicationCode = data.GetString("BiddingPrejudicationCode");
            data.Dispose();
        }

        private EntityData _GetBidding_SupplierDepartmentIdeas()
        {
            EntityData entitydata = new EntityData("Bidding_SupplierDepartmentIdea");
            Bidding_SupplierDepartmentIdeaStratebyBuilder builder = new Bidding_SupplierDepartmentIdeaStratebyBuilder();
            if (this._BiddingSupplierCode != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.BiddingSupplierCode, this._BiddingSupplierCode));
            }
            if (this._Depart_Agreement != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Depart_Agreement, this._Depart_Agreement));
            }
            if (this._Depart_Build != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Depart_Build, this._Depart_Build));
            }
            if (this._Depart_Project != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Depart_Project, this._Depart_Project));
            }
            if (this._DepartmentIdearID != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.DepartmentIdearID, this._DepartmentIdearID));
            }
            if (this._Director_Agreement != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Director_Agreement, this._Director_Agreement));
            }
            if (this._Director_Finance != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Director_Finance, this._Director_Finance));
            }
            if (this._Director_Project != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Director_Project, this._Director_Project));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Flag, this._Flag));
            }
            if (this._Md_Agreement != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Md_Agreement, this._Md_Agreement));
            }
            if (this._Md_Item != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Md_Item, this._Md_Item));
            }
            if (this._Md_Project != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.Md_Project, this._Md_Project));
            }
            if (this._DepartmentRemark != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark, this._DepartmentRemark));
            }
            if (this._DepartmentRemark1 != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark1, this._DepartmentRemark1));
            }
            if (this._DepartmentRemark2 != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark2, this._DepartmentRemark2));
            }
            if (this._DepartmentRemark3 != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.DepartmentRemark3, this._DepartmentRemark3));
            }
            if (this._BiddingPrejudicationCode != null)
            {
                builder.AddStrategy(new Strategy(Bidding_SupplierDepartmentIdeaStrategyName.BiddingPrejudicationCode, this._BiddingPrejudicationCode));
            }
            string sqlString = builder.BuildMainQueryString() + " order by DepartmentIdearID desc";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding_SupplierDepartmentIdea"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "Bidding_SupplierDepartmentIdea";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public void Bidding_SupplierDepartmentIdeaAdd()
        {
            if (this._DepartmentIdearID == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBidding_SupplierDepartmentIdea(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Bidding_SupplierDepartmentIdea";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void Bidding_SupplierDepartmentIdeaDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBidding_SupplierDepartmentIdea(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Bidding_SupplierDepartmentIdea";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void Bidding_SupplierDepartmentIdeaSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllBidding_SupplierDepartmentIdea(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Bidding_SupplierDepartmentIdea";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void Bidding_SupplierDepartmentIdeaUpdate()
        {
            if (this._DepartmentIdearID != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBidding_SupplierDepartmentIdea(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Bidding_SupplierDepartmentIdea";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData data;
            DataRow newRecord;
            bool flag = false;
            if (this._DepartmentIdearID == "")
            {
                flag = true;
                data = this.GetBidding_SupplierDepartmentIdeaByCode("");
                this._DepartmentIdearID = SystemManageDAO.GetNewSysCode("Bidding_SupplierDepartmentIdea");
                newRecord = data.GetNewRecord();
            }
            else
            {
                data = this.GetBidding_SupplierDepartmentIdeaByCode(this._DepartmentIdearID);
                newRecord = data.CurrentRow;
            }
            if (this._DepartmentIdearID != null)
            {
                newRecord["DepartmentIdearID"] = this._DepartmentIdearID;
            }
            if (this._BiddingSupplierCode != null)
            {
                newRecord["BiddingSupplierCode"] = this._BiddingSupplierCode;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (this._Depart_Build != null)
            {
                newRecord["Depart_Build"] = this._Depart_Build;
            }
            if (this._Depart_Project != null)
            {
                newRecord["Depart_Project"] = this._Depart_Project;
            }
            if (this._Depart_Agreement != null)
            {
                newRecord["Depart_Agreement"] = this._Depart_Agreement;
            }
            if (this._Md_Item != null)
            {
                newRecord["Md_Item"] = this._Md_Item;
            }
            if (this._Md_Project != null)
            {
                newRecord["Md_Project"] = this._Md_Project;
            }
            if (this._Md_Agreement != null)
            {
                newRecord["Md_Agreement"] = this._Md_Agreement;
            }
            if (this._Director_Project != null)
            {
                newRecord["Director_Project"] = this._Director_Project;
            }
            if (this._Director_Agreement != null)
            {
                newRecord["Director_Agreement"] = this._Director_Agreement;
            }
            if (this._Director_Finance != null)
            {
                newRecord["Director_Finance"] = this._Director_Finance;
            }
            if (this._DepartmentRemark != null)
            {
                newRecord["DepartmentRemark"] = this._DepartmentRemark;
            }
            if (this._DepartmentRemark1 != null)
            {
                newRecord["DepartmentRemark1"] = this._DepartmentRemark1;
            }
            if (this._DepartmentRemark2 != null)
            {
                newRecord["DepartmentRemark2"] = this._DepartmentRemark2;
            }
            if (this._DepartmentRemark3 != null)
            {
                newRecord["DepartmentRemark3"] = this._DepartmentRemark3;
            }
            if (this._BiddingPrejudicationCode != null)
            {
                newRecord["BiddingPrejudicationCode"] = this._BiddingPrejudicationCode;
            }
            if (flag)
            {
                data.AddNewRecord(newRecord);
            }
            return data;
        }

        private void DeleteBidding_SupplierDepartmentIdea(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding_SupplierDepartmentIdea"))
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

        private EntityData GetAllBidding_SupplierDepartmentIdea()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding_SupplierDepartmentIdea"))
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

        private EntityData GetBidding_SupplierDepartmentIdeaByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding_SupplierDepartmentIdea"))
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

        private EntityData GetBidding_SupplierDepartmentIdeaByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "Bidding_SupplierDepartmentIdea";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetBidding_SupplierDepartmentIdeas()
        {
            return this._GetBidding_SupplierDepartmentIdeas().CurrentTable;
        }

        private void InsertBidding_SupplierDepartmentIdea(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding_SupplierDepartmentIdea"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllBidding_SupplierDepartmentIdea(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding_SupplierDepartmentIdea"))
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

        private void UpdateBidding_SupplierDepartmentIdea(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Bidding_SupplierDepartmentIdea"))
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
                if ((this._BiddingPrejudicationCode == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
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
                if ((this._BiddingSupplierCode == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
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

        public string Depart_Agreement
        {
            get
            {
                if ((this._Depart_Agreement == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Depart_Agreement;
            }
            set
            {
                this._Depart_Agreement = value;
            }
        }

        public string Depart_Build
        {
            get
            {
                if ((this._Depart_Build == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Depart_Build;
            }
            set
            {
                this._Depart_Build = value;
            }
        }

        public string Depart_Project
        {
            get
            {
                if ((this._Depart_Project == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Depart_Project;
            }
            set
            {
                this._Depart_Project = value;
            }
        }

        public string DepartmentIdearID
        {
            get
            {
                return this._DepartmentIdearID;
            }
            set
            {
                this._DepartmentIdearID = value;
            }
        }

        public string DepartmentRemark
        {
            get
            {
                if ((this._DepartmentRemark == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._DepartmentRemark;
            }
            set
            {
                this._DepartmentRemark = value;
            }
        }

        public string DepartmentRemark1
        {
            get
            {
                if ((this._DepartmentRemark1 == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._DepartmentRemark1;
            }
            set
            {
                this._DepartmentRemark1 = value;
            }
        }

        public string DepartmentRemark2
        {
            get
            {
                if ((this._DepartmentRemark2 == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._DepartmentRemark2;
            }
            set
            {
                this._DepartmentRemark2 = value;
            }
        }

        public string DepartmentRemark3
        {
            get
            {
                if ((this._DepartmentRemark3 == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._DepartmentRemark3;
            }
            set
            {
                this._DepartmentRemark3 = value;
            }
        }

        public string Director_Agreement
        {
            get
            {
                if ((this._Director_Agreement == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Director_Agreement;
            }
            set
            {
                this._Director_Agreement = value;
            }
        }

        public string Director_Finance
        {
            get
            {
                if ((this._Director_Finance == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Director_Finance;
            }
            set
            {
                this._Director_Finance = value;
            }
        }

        public string Director_Project
        {
            get
            {
                if ((this._Director_Project == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Director_Project;
            }
            set
            {
                this._Director_Project = value;
            }
        }

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string Md_Agreement
        {
            get
            {
                if ((this._Md_Agreement == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Md_Agreement;
            }
            set
            {
                this._Md_Agreement = value;
            }
        }

        public string Md_Item
        {
            get
            {
                if ((this._Md_Item == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Md_Item;
            }
            set
            {
                this._Md_Item = value;
            }
        }

        public string Md_Project
        {
            get
            {
                if ((this._Md_Project == null) && (this._DepartmentIdearID != null))
                {
                    this._GetBidding_SupplierDepartmentIdeaByCode();
                }
                return this._Md_Project;
            }
            set
            {
                this._Md_Project = value;
            }
        }
    }
}

