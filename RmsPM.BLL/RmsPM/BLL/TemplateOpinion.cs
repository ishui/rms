namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class TemplateOpinion
    {
        private string _Center = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _Name = null;
        private string _Number = null;
        private string _State = null;
        private string _TemplateOpinionCode = null;
        private string _Type = null;
        private string _UserCode = null;

        private void _GetTemplateOpinionByCode()
        {
            EntityData templateOpinionByCode = this.GetTemplateOpinionByCode(this._TemplateOpinionCode);
            this._TemplateOpinionCode = templateOpinionByCode.GetString("TemplateOpinionCode");
            this._Number = templateOpinionByCode.GetString("Number");
            this._Name = templateOpinionByCode.GetString("Name");
            this._Center = templateOpinionByCode.GetString("Center");
            this._Type = templateOpinionByCode.GetString("Type");
            this._UserCode = templateOpinionByCode.GetString("UserCode");
            this._State = templateOpinionByCode.GetString("State");
            this._Flag = templateOpinionByCode.GetString("Flag");
            templateOpinionByCode.Dispose();
        }

        private EntityData _GetTemplateOpinions()
        {
            EntityData entitydata = new EntityData("TemplateOpinion");
            TemplateOpinionStrategyBuilder builder = new TemplateOpinionStrategyBuilder();
            if (this._TemplateOpinionCode != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.TemplateOpinionCode, this._TemplateOpinionCode));
            }
            if (this._Number != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.Number, this._Number));
            }
            if (this._Name != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.Name, this._Name));
            }
            if (this._Center != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.Center, this._Center));
            }
            if (this._Type != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.Type, this._Type));
            }
            if (this._UserCode != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.UserCode, this._UserCode));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(TemplateOpinionStrategyName.Flag, this._Flag));
            }
            string queryString = builder.BuildMainQueryString() + " order by TemplateOpinionCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("TemplateOpinion", queryString);
            }
            this.dao.EntityName = "TemplateOpinion";
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData templateOpinionByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._TemplateOpinionCode == "")
            {
                flag = true;
                templateOpinionByCode = this.GetTemplateOpinionByCode("");
                this._TemplateOpinionCode = SystemManageDAO.GetNewSysCode("TemplateOpinion");
                newRecord = templateOpinionByCode.GetNewRecord();
            }
            else
            {
                templateOpinionByCode = this.GetTemplateOpinionByCode(this._TemplateOpinionCode);
                newRecord = templateOpinionByCode.CurrentRow;
            }
            if (this._TemplateOpinionCode != null)
            {
                newRecord["TemplateOpinionCode"] = this._TemplateOpinionCode;
            }
            if (this._Number != null)
            {
                newRecord["Number"] = this._Number;
            }
            if (this._Name != null)
            {
                newRecord["Name"] = this._Name;
            }
            if (this._Center != null)
            {
                newRecord["Center"] = this._Center;
            }
            if (this._Type != null)
            {
                newRecord["Type"] = this._Type;
            }
            if (this._UserCode != null)
            {
                newRecord["UserCode"] = this._UserCode;
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
                templateOpinionByCode.AddNewRecord(newRecord);
            }
            return templateOpinionByCode;
        }

        private void DeleteTemplateOpinion(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TemplateOpinion"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "TemplateOpinion";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllTemplateOpinion()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TemplateOpinion"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "TemplateOpinion";
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

        private EntityData GetTemplateOpinionByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TemplateOpinion"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "TemplateOpinion";
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

        public DataTable GetTemplateOpinions()
        {
            return this._GetTemplateOpinions().CurrentTable;
        }

        private void InsertTemplateOpinion(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TemplateOpinion"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "TemplateOpinion";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllTemplateOpinion(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TemplateOpinion"))
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
                    this.dao.EntityName = "TemplateOpinion";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public void TemplateOpinionAdd()
        {
            if (this._TemplateOpinionCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllTemplateOpinion(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "TemplateOpinion";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void TemplateOpinionDelete()
        {
            if (this._dao == null)
            {
                this.DeleteTemplateOpinion(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "TemplateOpinion";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void TemplateOpinionSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllTemplateOpinion(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "TemplateOpinion";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void TemplateOpinionUpdate()
        {
            if (this._TemplateOpinionCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllTemplateOpinion(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "TemplateOpinion";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void UpdateTemplateOpinion(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TemplateOpinion"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "TemplateOpinion";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string Center
        {
            get
            {
                if ((this._Center == null) && (this._TemplateOpinionCode != null))
                {
                    this._GetTemplateOpinionByCode();
                }
                return this._Center;
            }
            set
            {
                this._Center = value;
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
                if ((this._Flag == null) && (this._TemplateOpinionCode != null))
                {
                    this._GetTemplateOpinionByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string Name
        {
            get
            {
                if ((this._Name == null) && (this._TemplateOpinionCode != null))
                {
                    this._GetTemplateOpinionByCode();
                }
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public string Number
        {
            get
            {
                if ((this._Number == null) && (this._TemplateOpinionCode != null))
                {
                    this._GetTemplateOpinionByCode();
                }
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._TemplateOpinionCode != null))
                {
                    this._GetTemplateOpinionByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string TemplateOpinionCode
        {
            get
            {
                return this._TemplateOpinionCode;
            }
            set
            {
                this._TemplateOpinionCode = value;
            }
        }

        public string Type
        {
            get
            {
                if ((this._Type == null) && (this._TemplateOpinionCode != null))
                {
                    this._GetTemplateOpinionByCode();
                }
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        public string UserCode
        {
            get
            {
                if ((this._UserCode == null) && (this._TemplateOpinionCode != null))
                {
                    this._GetTemplateOpinionByCode();
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

