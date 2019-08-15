namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class DesignDocument
    {
        private string _Context = null;
        private string _CreateDate = null;
        private string _CreateUser = null;
        private StandardEntityDAO _dao;
        private string _DesignDocumentCode = null;
        private string _Flag = null;
        private string _ProjectCode = null;
        private string _State = null;
        private string _Title = null;
        private string _Type = null;
        private string _UnitCode = null;

        private void _GetDesignDocumentByCode()
        {
            EntityData designDocumentByCode = this.GetDesignDocumentByCode(this._DesignDocumentCode);
            this._DesignDocumentCode = designDocumentByCode.GetString("DesignDocumentCode");
            this._Title = designDocumentByCode.GetString("Title");
            this._ProjectCode = designDocumentByCode.GetString("ProjectCode");
            this._UnitCode = designDocumentByCode.GetString("UnitCode");
            this._Context = designDocumentByCode.GetString("Context");
            this._CreateDate = designDocumentByCode.GetDateTime("CreateDate").ToString();
            this._CreateUser = designDocumentByCode.GetString("CreateUser");
            this._State = designDocumentByCode.GetString("State");
            this._Flag = designDocumentByCode.GetString("Flag");
            if (this._State.Length > 0)
            {
                this._Type = designDocumentByCode.GetString("State").Substring(0, 1);
            }
            designDocumentByCode.Dispose();
        }

        private EntityData _GetDesignDocuments()
        {
            EntityData entitydata = new EntityData("DesignDocument");
            DesignDocumentStrategyBuilder builder = new DesignDocumentStrategyBuilder();
            if (this._DesignDocumentCode != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.DesignDocumentCode, this._DesignDocumentCode));
            }
            if (this._Title != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.Title, "%" + this._Title + "%"));
            }
            if (this._ProjectCode != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.ProjectCode, this._ProjectCode));
            }
            if (this._UnitCode != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.UnitCode, this._UnitCode));
            }
            if (this._Context != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.Context, this._Context));
            }
            if (this._CreateDate != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.CreateDate, this._CreateDate));
            }
            if (this._CreateUser != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.CreateUser, this._CreateUser));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.Flag, this._Flag));
            }
            if (this._Type != null)
            {
                builder.AddStrategy(new Strategy(DesignDocumentStrategyName.type, this._Type + "%"));
            }
            string queryString = builder.BuildMainQueryString() + " order by DesignDocumentCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("DesignDocument", queryString);
            }
            this.dao.EntityName = "DesignDocument";
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData designDocumentByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._DesignDocumentCode == "")
            {
                flag = true;
                designDocumentByCode = this.GetDesignDocumentByCode("");
                this._DesignDocumentCode = SystemManageDAO.GetNewSysCode("DesignDocument");
                newRecord = designDocumentByCode.GetNewRecord();
            }
            else
            {
                designDocumentByCode = this.GetDesignDocumentByCode(this._DesignDocumentCode);
                newRecord = designDocumentByCode.CurrentRow;
            }
            if (this._DesignDocumentCode != null)
            {
                newRecord["DesignDocumentCode"] = this._DesignDocumentCode;
            }
            if (this._Title != null)
            {
                newRecord["Title"] = this._Title;
            }
            if (this._ProjectCode != null)
            {
                newRecord["ProjectCode"] = this._ProjectCode;
            }
            if (this._UnitCode != null)
            {
                newRecord["UnitCode"] = this._UnitCode;
            }
            if (this._Context != null)
            {
                newRecord["Context"] = this._Context;
            }
            if (this._CreateDate != null)
            {
                newRecord["CreateDate"] = this._CreateDate;
            }
            if (this._CreateUser != null)
            {
                newRecord["CreateUser"] = this._CreateUser;
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
                designDocumentByCode.AddNewRecord(newRecord);
            }
            return designDocumentByCode;
        }

        private void DeleteDesignDocument(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("DesignDocument"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "DesignDocument";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DesignDocumentAdd()
        {
            if (this._DesignDocumentCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllDesignDocument(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "DesignDocument";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void DesignDocumentDelete()
        {
            if (this._dao == null)
            {
                this.DeleteDesignDocument(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "DesignDocument";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void DesignDocumentSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllDesignDocument(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "DesignDocument";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void DesignDocumentUpdate()
        {
            if (this._DesignDocumentCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllDesignDocument(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "DesignDocument";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData GetAllDesignDocument()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("DesignDocument"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "DesignDocument";
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

        private EntityData GetDesignDocumentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("DesignDocument"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "DesignDocument";
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

        public DataTable GetDesignDocuments()
        {
            return this._GetDesignDocuments().CurrentTable;
        }

        private void InsertDesignDocument(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("DesignDocument"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "DesignDocument";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllDesignDocument(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("DesignDocument"))
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
                    this.dao.EntityName = "DesignDocument";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateDesignDocument(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("DesignDocument"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "DesignDocument";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string Context
        {
            get
            {
                if ((this._Context == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._Context;
            }
            set
            {
                this._Context = value;
            }
        }

        public string CreateDate
        {
            get
            {
                if ((this._CreateDate == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._CreateDate;
            }
            set
            {
                this._CreateDate = value;
            }
        }

        public string CreateUser
        {
            get
            {
                if ((this._CreateUser == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._CreateUser;
            }
            set
            {
                this._CreateUser = value;
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

        public string DesignDocumentCode
        {
            get
            {
                return this._DesignDocumentCode;
            }
            set
            {
                this._DesignDocumentCode = value;
            }
        }

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                if ((this._ProjectCode == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string Title
        {
            get
            {
                if ((this._Title == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        public string Type
        {
            get
            {
                if ((this._Type == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        public string UnitCode
        {
            get
            {
                if ((this._UnitCode == null) && (this._DesignDocumentCode != null))
                {
                    this._GetDesignDocumentByCode();
                }
                return this._UnitCode;
            }
            set
            {
                this._UnitCode = value;
            }
        }
    }
}

