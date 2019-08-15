namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class Design_Message
    {
        private string _ContractCode = null;
        private string _ContractID = null;
        private string _ContractName = null;
        private StandardEntityDAO _dao;
        private string _DesignBeingTime = null;
        private string _DesignCode = null;
        private string _DesignDepartMentID = null;
        private string _DesignFlag = null;
        private string _DesignID = null;
        private string _DesignJionTime = null;
        private string _DesignLastTime = null;
        private string _DesignName = null;
        private string _DesignPerson = null;
        private string _DesignReason = null;
        private string _DesignRemark = null;
        private string _DesignState = null;
        private string _DesignSupplier = null;
        private string _ProjectCode = null;
        private string _ProjectName = null;

        private void _GetDesign_MessageByCode()
        {
            EntityData data = this.GetDesign_MessageByCode(this._DesignCode);
            this._DesignCode = data.GetString("DesignCode");
            this._DesignName = data.GetString("DesignName");
            this._DesignID = data.GetString("DesignID");
            this._ProjectName = data.GetString("ProjectName");
            this._ContractName = data.GetString("ContractName");
            this._ContractCode = data.GetString("ContractCode");
            this._ContractID = data.GetString("ContractID");
            this._DesignReason = data.GetString("DesignReason");
            this._DesignSupplier = data.GetString("DesignSupplier");
            this._DesignPerson = data.GetString("DesignPerson");
            this._DesignLastTime = data.GetDateTimeOnlyDate("DesignLastTime").ToString();
            this._DesignBeingTime = data.GetDateTimeOnlyDate("DesignBeingTime").ToString();
            this._DesignJionTime = data.GetDateTimeOnlyDate("DesignJionTime").ToString();
            this._DesignFlag = data.GetString("DesignFlag");
            this._DesignState = data.GetString("DesignState");
            this._DesignDepartMentID = data.GetString("DesignDepartMentID");
            this._DesignRemark = data.GetString("DesignRemark");
            this._ProjectCode = data.GetString("ProjectCode");
            data.Dispose();
        }

        private EntityData _GetDesign_Messages()
        {
            EntityData entitydata = new EntityData("Design_Message");
            Design_MessageStrategyBuilder builder = new Design_MessageStrategyBuilder();
            if (this._DesignCode != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignCode, this._DesignCode));
            }
            if (this._DesignName != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignName, this._DesignName));
            }
            if (this._DesignID != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignID, this._DesignID));
            }
            if (this._ProjectName != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.ProjectName, this._ProjectName));
            }
            if (this._ContractName != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.ContractName, this._ContractName));
            }
            if (this._ContractCode != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.ContractCode, this._ContractCode));
            }
            if (this._ContractID != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.ContractID, this._ContractID));
            }
            if (this._DesignReason != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignReason, this._DesignReason));
            }
            if (this._DesignSupplier != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignSupplier, this._DesignSupplier));
            }
            if (this._DesignPerson != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignPerson, this._DesignPerson));
            }
            if (this._DesignLastTime != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignLastTime, this._DesignLastTime));
            }
            if (this._DesignBeingTime != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignBeingTime, this._DesignBeingTime));
            }
            if (this._DesignJionTime != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignJionTime, this._DesignJionTime));
            }
            if (this._DesignFlag != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignFlag, this._DesignFlag));
            }
            if (this._DesignState != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignState, this._DesignState));
            }
            if (this._DesignDepartMentID != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignDepartMentID, this._DesignDepartMentID));
            }
            if (this._DesignRemark != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.DesignRemark, this._DesignRemark));
            }
            if (this._ProjectCode != null)
            {
                builder.AddStrategy(new Strategy(Design_MessageStrategyName.ProjectCode, this._ProjectCode));
            }
            string queryString = builder.BuildMainQueryString() + " order by DesignCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("Design_Message", queryString);
            }
            this.dao.EntityName = "Design_Message";
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData data;
            DataRow newRecord;
            bool flag = false;
            if (this._DesignCode == "")
            {
                flag = true;
                data = this.GetDesign_MessageByCode("");
                this._DesignCode = SystemManageDAO.GetNewSysCode("Design_Message");
                newRecord = data.GetNewRecord();
            }
            else
            {
                data = this.GetDesign_MessageByCode(this._DesignCode);
                if (data.Tables[0].Rows.Count > 0)
                {
                    newRecord = data.CurrentRow;
                }
                else
                {
                    newRecord = data.GetNewRecord();
                    flag = true;
                }
            }
            if (this._DesignCode != null)
            {
                newRecord["DesignCode"] = this._DesignCode;
            }
            if (this._DesignName != null)
            {
                newRecord["DesignName"] = this._DesignName;
            }
            if (this._DesignID != null)
            {
                newRecord["DesignID"] = this._DesignID;
            }
            if (this._ProjectName != null)
            {
                newRecord["ProjectName"] = this._ProjectName;
            }
            if (this._ContractName != null)
            {
                newRecord["ContractName"] = this._ContractName;
            }
            if (this._ContractCode != null)
            {
                newRecord["ContractCode"] = this._ContractCode;
            }
            if (this._ContractID != null)
            {
                newRecord["ContractID"] = this._ContractID;
            }
            if (this._DesignReason != null)
            {
                newRecord["DesignReason"] = this._DesignReason;
            }
            if (this._DesignSupplier != null)
            {
                newRecord["DesignSupplier"] = this._DesignSupplier;
            }
            if (this._DesignPerson != null)
            {
                newRecord["DesignPerson"] = this._DesignPerson;
            }
            if (this._DesignLastTime != null)
            {
                newRecord["DesignLastTime"] = this._DesignLastTime;
            }
            if (this._DesignBeingTime != null)
            {
                newRecord["DesignBeingTime"] = this._DesignBeingTime;
            }
            if (this._DesignJionTime != null)
            {
                newRecord["DesignJionTime"] = this._DesignJionTime;
            }
            if (this._DesignFlag != null)
            {
                newRecord["DesignFlag"] = this._DesignFlag;
            }
            if (this._DesignState != null)
            {
                newRecord["DesignState"] = this._DesignState;
            }
            if (this._DesignDepartMentID != null)
            {
                newRecord["DesignDepartMentID"] = this._DesignDepartMentID;
            }
            if (this._DesignRemark != null)
            {
                newRecord["DesignRemark"] = this._DesignRemark;
            }
            if (this._ProjectCode != null)
            {
                newRecord["ProjectCode"] = this._ProjectCode;
            }
            if (flag)
            {
                data.AddNewRecord(newRecord);
            }
            return data;
        }

        private void DeleteDesign_Message(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Design_Message"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Design_Message";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Design_MessageAdd()
        {
            if (this._DesignCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllDesign_Message(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Design_Message";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void Design_MessageDelete()
        {
            if (this._dao == null)
            {
                this.DeleteDesign_Message(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Design_Message";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void Design_MessageSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllDesign_Message(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Design_Message";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void Design_MessageUpdate()
        {
            if (this._DesignCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllDesign_Message(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Design_Message";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData GetAllDesign_Message()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Design_Message"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "Design_Message";
                    data = this.dao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private EntityData GetDesign_MessageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Design_Message"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "Design_Message";
                    data = this.dao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetDesign_Messages()
        {
            return this._GetDesign_Messages().CurrentTable;
        }

        private void InsertDesign_Message(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Design_Message"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Design_Message";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllDesign_Message(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Design_Message"))
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
                else
                {
                    this.dao.EntityName = "Design_Message";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateDesign_Message(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Design_Message"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Design_Message";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string ContractCode
        {
            get
            {
                if ((this._ContractCode == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._ContractCode;
            }
            set
            {
                this._ContractCode = value;
            }
        }

        public string ContractID
        {
            get
            {
                if ((this._ContractID == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._ContractID;
            }
            set
            {
                this._ContractID = value;
            }
        }

        public string ContractName
        {
            get
            {
                if ((this._ContractName == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._ContractName;
            }
            set
            {
                this._ContractName = value;
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

        public string DesignBeingTime
        {
            get
            {
                if ((this._DesignBeingTime == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignBeingTime;
            }
            set
            {
                this._DesignBeingTime = value;
            }
        }

        public string DesignCode
        {
            get
            {
                return this._DesignCode;
            }
            set
            {
                this._DesignCode = value;
            }
        }

        public string DesignDepartMentID
        {
            get
            {
                if ((this._DesignDepartMentID == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignDepartMentID;
            }
            set
            {
                this._DesignDepartMentID = value;
            }
        }

        public string DesignFlag
        {
            get
            {
                if ((this._DesignFlag == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignFlag;
            }
            set
            {
                this._DesignFlag = value;
            }
        }

        public string DesignID
        {
            get
            {
                if ((this._DesignID == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignID;
            }
            set
            {
                this._DesignID = value;
            }
        }

        public string DesignJionTime
        {
            get
            {
                if ((this._DesignJionTime == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignJionTime;
            }
            set
            {
                this._DesignJionTime = value;
            }
        }

        public string DesignLastTime
        {
            get
            {
                if ((this._DesignLastTime == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignLastTime;
            }
            set
            {
                this._DesignLastTime = value;
            }
        }

        public string DesignName
        {
            get
            {
                if ((this._DesignName == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignName;
            }
            set
            {
                this._DesignName = value;
            }
        }

        public string DesignPerson
        {
            get
            {
                if ((this._DesignPerson == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignPerson;
            }
            set
            {
                this._DesignPerson = value;
            }
        }

        public string DesignReason
        {
            get
            {
                if ((this._DesignReason == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignReason;
            }
            set
            {
                this._DesignReason = value;
            }
        }

        public string DesignRemark
        {
            get
            {
                if ((this._DesignRemark == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignRemark;
            }
            set
            {
                this._DesignRemark = value;
            }
        }

        public string DesignState
        {
            get
            {
                if ((this._DesignState == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignState;
            }
            set
            {
                this._DesignState = value;
            }
        }

        public string DesignSupplier
        {
            get
            {
                if ((this._DesignSupplier == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._DesignSupplier;
            }
            set
            {
                this._DesignSupplier = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                if ((this._ProjectCode == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string ProjectName
        {
            get
            {
                if ((this._ProjectName == null) && (this._DesignCode != null))
                {
                    this._GetDesign_MessageByCode();
                }
                return this._ProjectName;
            }
            set
            {
                this._ProjectName = value;
            }
        }
    }
}

