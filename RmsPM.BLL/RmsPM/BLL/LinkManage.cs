namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class LinkManage
    {
        private string _CreateDate = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _LinkManageCode = null;
        private string _Linkname = null;
        private string _LinkUrl = null;
        private string _State = null;

        private void _GetLinkManageByCode()
        {
            try
            {
                EntityData linkManageByCode = GetLinkManageByCode(this._LinkManageCode);
                this._LinkManageCode = linkManageByCode.GetString("LinkManageCode");
                this._Linkname = linkManageByCode.GetString("Linkname");
                this._LinkUrl = linkManageByCode.GetString("LinkUrl");
                this._CreateDate = linkManageByCode.GetString("CreateDate");
                this._State = linkManageByCode.GetString("state");
                this._Flag = linkManageByCode.GetString("flag");
                linkManageByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetLinkManages()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("LinkManage");
                LinkManageStrategyBuilder builder = new LinkManageStrategyBuilder();
                if (this._LinkManageCode != null)
                {
                    builder.AddStrategy(new Strategy(LinkManageStrategyName.LinkManageCode, this._LinkManageCode));
                }
                if (this._Linkname != null)
                {
                    builder.AddStrategy(new Strategy(LinkManageStrategyName.Linkname, this._Linkname));
                }
                if (this._LinkUrl != null)
                {
                    builder.AddStrategy(new Strategy(LinkManageStrategyName.LinkUrl, this._LinkUrl));
                }
                if (this._CreateDate != null)
                {
                    builder.AddStrategy(new Strategy(LinkManageStrategyName.CreateDate, this._CreateDate));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(LinkManageStrategyName.state, this._State));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(LinkManageStrategyName.flag, this._Flag));
                }
                string sqlString = builder.BuildMainQueryString() + " order by LinkManageCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("LinkManage"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "LinkManage";
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

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData linkManageByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._LinkManageCode == "")
                {
                    flag = true;
                    linkManageByCode = GetLinkManageByCode("");
                    this._LinkManageCode = SystemManageDAO.GetNewSysCode("LinkManage");
                    newRecord = linkManageByCode.GetNewRecord();
                }
                else
                {
                    linkManageByCode = GetLinkManageByCode(this._LinkManageCode);
                    if (linkManageByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = linkManageByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = linkManageByCode.CurrentRow;
                    }
                }
                if (this._LinkManageCode != null)
                {
                    newRecord["LinkManageCode"] = this._LinkManageCode;
                }
                if (this._Linkname != null)
                {
                    newRecord["Linkname"] = this._Linkname;
                }
                if (this._LinkUrl != null)
                {
                    newRecord["LinkUrl"] = this._LinkUrl;
                }
                if (this._CreateDate != null)
                {
                    newRecord["CreateDate"] = this._CreateDate;
                }
                if (this._State != null)
                {
                    newRecord["state"] = this._State;
                }
                if (this._Flag != null)
                {
                    newRecord["flag"] = this._Flag;
                }
                if (flag)
                {
                    linkManageByCode.AddNewRecord(newRecord);
                }
                data2 = linkManageByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteLinkManage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("LinkManage"))
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

        public static EntityData GetAllLinkManage()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("LinkManage"))
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

        public static EntityData GetLinkManageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("LinkManage"))
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

        public static EntityData GetLinkManageByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetLinkManages()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetLinkManages().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertLinkManage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("LinkManage"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void LinkManageAdd()
        {
            if (this._dao == null)
            {
                SubmitAllLinkManage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "LinkManage";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void LinkManageDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteLinkManage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "LinkManage";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void LinkManageUpdate()
        {
            if (this._LinkManageCode != null)
            {
                if (this._dao == null)
                {
                    SubmitAllLinkManage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "LinkManage";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public static void SubmitAllLinkManage(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("LinkManage"))
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

        public static void UpdateLinkManage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("LinkManage"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string CreateDate
        {
            get
            {
                if ((this._CreateDate == null) && (this._LinkManageCode != null))
                {
                    this._GetLinkManageByCode();
                }
                return this._CreateDate;
            }
            set
            {
                if (this._CreateDate != value)
                {
                    this._CreateDate = value;
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

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._LinkManageCode != null))
                {
                    this._GetLinkManageByCode();
                }
                return this._Flag;
            }
            set
            {
                if (this._Flag != value)
                {
                    this._Flag = value;
                }
            }
        }

        public string LinkManageCode
        {
            get
            {
                return this._LinkManageCode;
            }
            set
            {
                if (this._LinkManageCode != value)
                {
                    this._LinkManageCode = value;
                }
            }
        }

        public string Linkname
        {
            get
            {
                if ((this._Linkname == null) && (this._LinkManageCode != null))
                {
                    this._GetLinkManageByCode();
                }
                return this._Linkname;
            }
            set
            {
                if (this._Linkname != value)
                {
                    this._Linkname = value;
                }
            }
        }

        public string LinkUrl
        {
            get
            {
                if ((this._LinkUrl == null) && (this._LinkManageCode != null))
                {
                    this._GetLinkManageByCode();
                }
                return this._LinkUrl;
            }
            set
            {
                if (this._LinkUrl != value)
                {
                    this._LinkUrl = value;
                }
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._LinkManageCode != null))
                {
                    this._GetLinkManageByCode();
                }
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

