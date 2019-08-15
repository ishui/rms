namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class OACachet
    {
        private string _ApplyDate = null;
        private string _ApplyUser = null;
        private StandardEntityDAO _dao;
        private string _OACachetCode = null;
        private string _Reason = null;
        private string _Unit = null;

        private void _GetOACachetByCode()
        {
            EntityData oACachetByCode = this.GetOACachetByCode(this._OACachetCode);
            this._OACachetCode = oACachetByCode.GetString("OACachetCode");
            this._Unit = oACachetByCode.GetString("Unit");
            this._ApplyDate = oACachetByCode.GetDateTimeOnlyDate("ApplyDate");
            this._Reason = oACachetByCode.GetString("Reason");
            this._ApplyUser = oACachetByCode.GetString("ApplyUser");
            oACachetByCode.Dispose();
        }

        private EntityData _GetOACachets()
        {
            EntityData entitydata = new EntityData("OACachet");
            OACachetStrategyBuilder builder = new OACachetStrategyBuilder();
            if (this._OACachetCode != null)
            {
                builder.AddStrategy(new Strategy(OACachetStrategyName.OACachetCode, this._OACachetCode));
            }
            if (this._Unit != null)
            {
                builder.AddStrategy(new Strategy(OACachetStrategyName.Unit, this._Unit));
            }
            if (this._ApplyDate != null)
            {
                builder.AddStrategy(new Strategy(OACachetStrategyName.ApplyDate, this._ApplyDate));
            }
            if (this._Reason != null)
            {
                builder.AddStrategy(new Strategy(OACachetStrategyName.Reason, this._Reason));
            }
            if (this._ApplyUser != null)
            {
                builder.AddStrategy(new Strategy(OACachetStrategyName.ApplyUser, this._ApplyUser));
            }
            string queryString = builder.BuildMainQueryString() + " order by OACachetCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("OACachet", queryString);
            }
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData oACachetByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._OACachetCode == "")
            {
                flag = true;
                oACachetByCode = this.GetOACachetByCode("");
                this._OACachetCode = SystemManageDAO.GetNewSysCode("OACachet");
                newRecord = oACachetByCode.GetNewRecord();
            }
            else
            {
                oACachetByCode = this.GetOACachetByCode(this._OACachetCode);
                newRecord = oACachetByCode.CurrentRow;
            }
            if (this._OACachetCode != null)
            {
                newRecord["OACachetCode"] = this._OACachetCode;
            }
            if (this._Unit != null)
            {
                newRecord["Unit"] = this._Unit;
            }
            if (this._ApplyDate != null)
            {
                newRecord["ApplyDate"] = this._ApplyDate;
            }
            if (this._Reason != null)
            {
                newRecord["Reason"] = this._Reason;
            }
            if (this._ApplyUser != null)
            {
                newRecord["ApplyUser"] = this._ApplyUser;
            }
            if (flag)
            {
                oACachetByCode.AddNewRecord(newRecord);
            }
            return oACachetByCode;
        }

        private void DeleteOACachet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OACachet"))
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

        private EntityData GetAllOACachet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("OACachet"))
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

        private EntityData GetOACachetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("OACachet"))
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

        private EntityData GetOACachetByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "OACachet";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetOACachets()
        {
            return this._GetOACachets().CurrentTable;
        }

        private void InsertOACachet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OACachet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void OACachetAdd()
        {
            if (this._OACachetCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllOACachet(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "OACachet";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void OACachetDelete()
        {
            if (this._dao == null)
            {
                this.DeleteOACachet(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "OACachet";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void OACachetSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllOACachet(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "OACachet";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void OACachetUpdate()
        {
            if (this._OACachetCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllOACachet(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "OACachet";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void SubmitAllOACachet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OACachet"))
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

        private void UpdateOACachet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OACachet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string ApplyDate
        {
            get
            {
                if ((this._ApplyDate == null) && (this._OACachetCode != null))
                {
                    this._GetOACachetByCode();
                }
                return this._ApplyDate;
            }
            set
            {
                this._ApplyDate = value;
            }
        }

        public string ApplyUser
        {
            get
            {
                if ((this._ApplyUser == null) && (this._OACachetCode != null))
                {
                    this._GetOACachetByCode();
                }
                return this._ApplyUser;
            }
            set
            {
                this._ApplyUser = value;
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

        public string OACachetCode
        {
            get
            {
                return this._OACachetCode;
            }
            set
            {
                this._OACachetCode = value;
            }
        }

        public string Reason
        {
            get
            {
                if ((this._Reason == null) && (this._OACachetCode != null))
                {
                    this._GetOACachetByCode();
                }
                return this._Reason;
            }
            set
            {
                this._Reason = value;
            }
        }

        public string Unit
        {
            get
            {
                if ((this._Unit == null) && (this._OACachetCode != null))
                {
                    this._GetOACachetByCode();
                }
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }
    }
}

