namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class SendMsg
    {
        private StandardEntityDAO _dao;
        private EntityData _entitydata = null;
        private string _Flag = null;
        private string _Msg = null;
        private string _senddel = null;
        private string _SendMsgCode = null;
        private string _Sendtime = null;
        private string _SendUsercode = null;
        private string _State = null;
        private string _todel = null;
        private string _ToUsercode = null;

        private void _GetSendMsgByCode()
        {
            EntityData sendMsgByCode = this.GetSendMsgByCode(this._SendMsgCode);
            this._SendMsgCode = sendMsgByCode.GetString("SendMsgCode");
            this._SendUsercode = sendMsgByCode.GetString("SendUsercode");
            this._ToUsercode = sendMsgByCode.GetString("ToUsercode");
            this._Msg = sendMsgByCode.GetString("Msg");
            this._Sendtime = sendMsgByCode.GetDateTimeOnlyDate("Sendtime");
            this._State = sendMsgByCode.GetString("State");
            this._senddel = sendMsgByCode.GetString("senddel");
            this._todel = sendMsgByCode.GetString("todel");
            this._Flag = sendMsgByCode.GetString("Flag");
            sendMsgByCode.Dispose();
        }

        private EntityData _GetSendMsgs()
        {
            EntityData entitydata = new EntityData("SendMsg");
            SendMsgStrategyBuilder builder = new SendMsgStrategyBuilder();
            if (this._SendMsgCode != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.SendMsgCode, this._SendMsgCode));
            }
            if (this._SendUsercode != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.SendUsercode, this._SendUsercode));
            }
            if (this._ToUsercode != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.ToUsercode, this._ToUsercode));
            }
            if (this._Msg != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.Msg, this._Msg));
            }
            if (this._Sendtime != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.Sendtime, this._Sendtime));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.State, this._State));
            }
            if (this._senddel != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.senddel, this._senddel));
            }
            if (this._todel != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.todel, this._todel));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(SendMsgStrategyName.Flag, this._Flag));
            }
            string sqlString = builder.BuildMainQueryString() + " order by SendMsgCode desc";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SendMsg"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "SendMsg";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData sendMsgByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._SendMsgCode == "")
            {
                flag = true;
                sendMsgByCode = this.GetSendMsgByCode("");
                this._SendMsgCode = SystemManageDAO.GetNewSysCode("SendMsg");
                newRecord = sendMsgByCode.GetNewRecord();
            }
            else
            {
                sendMsgByCode = this.GetSendMsgByCode(this._SendMsgCode);
                newRecord = sendMsgByCode.CurrentRow;
            }
            if (this._SendMsgCode != null)
            {
                newRecord["SendMsgCode"] = this._SendMsgCode;
            }
            if (this._SendUsercode != null)
            {
                newRecord["SendUsercode"] = this._SendUsercode;
            }
            if (this._ToUsercode != null)
            {
                newRecord["ToUsercode"] = this._ToUsercode;
            }
            if (this._Msg != null)
            {
                newRecord["Msg"] = this._Msg;
            }
            if (this._Sendtime != null)
            {
                newRecord["Sendtime"] = this._Sendtime;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (this._senddel != null)
            {
                newRecord["senddel"] = this._senddel;
            }
            if (this._todel != null)
            {
                newRecord["todel"] = this._todel;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (flag)
            {
                sendMsgByCode.AddNewRecord(newRecord);
            }
            return sendMsgByCode;
        }

        private EntityData BuildData(string UserCodes)
        {
            EntityData sendMsgByCode = this.GetSendMsgByCode("");
            foreach (string text in UserCodes.Split(new char[] { ',' }))
            {
                if (text != "")
                {
                    this._SendMsgCode = SystemManageDAO.GetNewSysCode("SendMsg");
                    DataRow newRecord = sendMsgByCode.GetNewRecord();
                    if (this._SendMsgCode != null)
                    {
                        newRecord["SendMsgCode"] = this._SendMsgCode;
                    }
                    if (this._SendUsercode != null)
                    {
                        newRecord["SendUsercode"] = this._SendUsercode;
                    }
                    newRecord["ToUsercode"] = text;
                    if (this._Msg != null)
                    {
                        newRecord["Msg"] = this._Msg;
                    }
                    if (this._Sendtime != null)
                    {
                        newRecord["Sendtime"] = this._Sendtime;
                    }
                    if (this._State != null)
                    {
                        newRecord["State"] = this._State;
                    }
                    if (this._senddel != null)
                    {
                        newRecord["senddel"] = this._senddel;
                    }
                    if (this._todel != null)
                    {
                        newRecord["todel"] = this._todel;
                    }
                    if (this._Flag != null)
                    {
                        newRecord["Flag"] = this._Flag;
                    }
                    sendMsgByCode.AddNewRecord(newRecord);
                }
            }
            return sendMsgByCode;
        }

        private void DeleteSendMsg(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SendMsg"))
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

        private EntityData GetAllSendMsg()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SendMsg"))
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

        private EntityData GetSendMsgByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SendMsg"))
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

        private EntityData GetSendMsgByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "SendMsg";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetSendMsgs()
        {
            return this._GetSendMsgs().CurrentTable;
        }

        private void InsertSendMsg(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SendMsg"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SendMsgAdd()
        {
            if (this._SendMsgCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllSendMsg(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "SendMsg";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void SendMsgDelete()
        {
            if (this._dao == null)
            {
                this.DeleteSendMsg(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "SendMsg";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void SendMsgSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllSendMsg(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "SendMsg";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void SendMsgSubmit(string UserCodes)
        {
            if (this._dao == null)
            {
                this.entitydata = this.BuildData(UserCodes);
                this.SubmitAllSendMsg(this.entitydata);
            }
            else
            {
                this.dao.EntityName = "SendMsg";
                this.entitydata = this.BuildData(UserCodes);
                this.dao.SubmitEntity(this.entitydata);
            }
        }

        public void SendMsgUpdate()
        {
            if (this._SendMsgCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllSendMsg(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "SendMsg";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void SubmitAllSendMsg(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SendMsg"))
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

        private void UpdateSendMsg(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SendMsg"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
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

        public EntityData entitydata
        {
            get
            {
                if (this._entitydata != null)
                {
                    return this._entitydata;
                }
                return null;
            }
            set
            {
                this._entitydata = value;
            }
        }

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string Msg
        {
            get
            {
                if ((this._Msg == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._Msg;
            }
            set
            {
                this._Msg = value;
            }
        }

        public string senddel
        {
            get
            {
                if ((this._senddel == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._senddel;
            }
            set
            {
                this._senddel = value;
            }
        }

        public string SendMsgCode
        {
            get
            {
                return this._SendMsgCode;
            }
            set
            {
                this._SendMsgCode = value;
            }
        }

        public string Sendtime
        {
            get
            {
                if ((this._Sendtime == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._Sendtime;
            }
            set
            {
                this._Sendtime = value;
            }
        }

        public string SendUsercode
        {
            get
            {
                if ((this._SendUsercode == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._SendUsercode;
            }
            set
            {
                this._SendUsercode = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string todel
        {
            get
            {
                if ((this._todel == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._todel;
            }
            set
            {
                this._todel = value;
            }
        }

        public string ToUsercode
        {
            get
            {
                if ((this._ToUsercode == null) && (this._SendMsgCode != null))
                {
                    this._GetSendMsgByCode();
                }
                return this._ToUsercode;
            }
            set
            {
                this._ToUsercode = value;
            }
        }
    }
}

