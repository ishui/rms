namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class GradeMessage
    {
        private string _CreateDate = null;
        private StandardEntityDAO _dao;
        private string _GradeMessageCode = null;
        private string _MainDefineCode = null;
        private string _ProduceName = null;
        private string _ProjectCode = null;
        private string _ProjectManage = null;
        private string _State = null;
        private string _SupplierCode = null;

        private void _GetGradeMessageByCode()
        {
            EntityData gradeMessageByCode = this.GetGradeMessageByCode(this._GradeMessageCode);
            this._GradeMessageCode = gradeMessageByCode.GetString("GradeMessageCode");
            this._ProjectCode = gradeMessageByCode.GetString("ProjectCode");
            this._SupplierCode = gradeMessageByCode.GetString("SupplierCode");
            this._MainDefineCode = gradeMessageByCode.GetString("MainDefineCode");
            this._ProjectManage = gradeMessageByCode.GetString("ProjectManage");
            this._ProduceName = gradeMessageByCode.GetString("ProduceName");
            this._CreateDate = gradeMessageByCode.GetDateTime("CreateDate").ToString();
            this._State = gradeMessageByCode.GetInt("State").ToString();
            gradeMessageByCode.Dispose();
        }

        private EntityData _GetGradeMessages()
        {
            EntityData entitydata = new EntityData("GradeMessage");
            GradeMessageStrategyBuilder builder = new GradeMessageStrategyBuilder();
            if (this._GradeMessageCode != null)
            {
                builder.AddStrategy(new Strategy(GradeMessageStrategyName.GradeMessageCode, this._GradeMessageCode));
            }
            if (this._ProjectCode != null)
            {
                builder.AddStrategy(new Strategy(GradeMessageStrategyName.ProjectCode, this._ProjectCode));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(GradeMessageStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._MainDefineCode != null)
            {
                builder.AddStrategy(new Strategy(GradeMessageStrategyName.MainDefineCode, this._MainDefineCode));
            }
            if (this._ProjectManage != null)
            {
                builder.AddStrategy(new Strategy(GradeMessageStrategyName.ProjectManage, this._ProjectManage));
            }
            if (this._CreateDate != null)
            {
                builder.AddStrategy(new Strategy(GradeMessageStrategyName.CreateDate, this._CreateDate));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(GradeMessageStrategyName.State, this._State));
            }
            string sqlString = builder.BuildMainQueryString() + " order by GradeMessageCode Desc";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMessage"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "GradeMessage";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData gradeMessageByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._GradeMessageCode == "")
            {
                flag = true;
                gradeMessageByCode = this.GetGradeMessageByCode("");
                this._GradeMessageCode = SystemManageDAO.GetNewSysCode("GradeMessage");
                newRecord = gradeMessageByCode.GetNewRecord();
            }
            else
            {
                gradeMessageByCode = this.GetGradeMessageByCode(this._GradeMessageCode);
                newRecord = gradeMessageByCode.CurrentRow;
            }
            if (this._GradeMessageCode != null)
            {
                newRecord["GradeMessageCode"] = this._GradeMessageCode;
            }
            if (this._ProjectCode != null)
            {
                newRecord["ProjectCode"] = this._ProjectCode;
            }
            if (this._SupplierCode != null)
            {
                newRecord["SupplierCode"] = this._SupplierCode;
            }
            if (this._MainDefineCode != null)
            {
                newRecord["MainDefineCode"] = this._MainDefineCode;
            }
            if (this._ProjectManage != null)
            {
                newRecord["ProjectManage"] = this._ProjectManage;
            }
            if (this._ProduceName != null)
            {
                newRecord["ProduceName"] = this._ProduceName;
            }
            if (this._CreateDate != null)
            {
                newRecord["CreateDate"] = this._CreateDate;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (flag)
            {
                gradeMessageByCode.AddNewRecord(newRecord);
            }
            return gradeMessageByCode;
        }

        private void DeleteGradeMessage(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMessage"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMessage";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_Grade(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Grade"))
                {
                    ydao.SubmitEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_GradeMessage(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("GradeMessage"))
                {
                    ydao.SubmitEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllGradeMessage()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMessage"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMessage";
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

        public static string GetContractStatusName(string state)
        {
            switch (state)
            {
                case "0":
                    return "已审";

                case "1":
                    return "申请";

                case "2":
                    return "已结";

                case "3":
                    return "作废";

                case "4":
                    return "变更";

                case "6":
                    return "历史";

                case "7":
                    return "审核中";

                case "8":
                    return "已评审";

                case "9":
                    return "评审中";
            }
            return "";
        }

        public EntityData GetGradeMessageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMessage"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMessage";
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

        public DataTable GetGradeMessages()
        {
            return this._GetGradeMessages().CurrentTable;
        }

        public void GradeMessageAdd()
        {
            if (this._GradeMessageCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeMessage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeMessage";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void GradeMessageDelete()
        {
            if (this._dao == null)
            {
                this.DeleteGradeMessage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeMessage";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public static bool GradeMessageStatusChange(string GradeMessageCode, int gm_iStatus)
        {
            return GradeMessageStatusChange(GradeMessageCode, gm_iStatus, null, true);
        }

        public static bool GradeMessageStatusChange(EntityData gm_Entity, int gm_iStatus)
        {
            return GradeMessageStatusChange(gm_Entity, "", gm_iStatus, null, false);
        }

        public static bool GradeMessageStatusChange(string GradeMessageCode, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return GradeMessageStatusChange(GradeMessageCode, gm_iStatus, gm_iOriginalStatus, true);
        }

        public static bool GradeMessageStatusChange(EntityData gm_Entity, int gm_iStatus, bool gm_bSubmitData)
        {
            return GradeMessageStatusChange(gm_Entity, "", gm_iStatus, null, gm_bSubmitData);
        }

        public static bool GradeMessageStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return GradeMessageStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, false);
        }

        public static bool GradeMessageStatusChange(string GradeMessageCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            GradeMessage message = new GradeMessage();
            return GradeMessageStatusChange(message.GetGradeMessageByCode(GradeMessageCode), GradeMessageCode, gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool GradeMessageStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return GradeMessageStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool GradeMessageStatusChange(EntityData gm_Entity, string GradeMessageCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                gm_Entity.SetCurrentTable("GradeMessage");
                if (GradeMessageCode.Trim() == "")
                {
                    if (gm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("GradeMessageCode='{0}'", GradeMessageCode.Trim());
                    if (gm_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in gm_Entity.CurrentTable.Select(filterExpression))
                {
                    if (gm_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["State"] != DBNull.Value) && ((((int) row["State"]) == (nullable = gm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["State"] = gm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["State"] = gm_iStatus;
                    }
                }
                if (flag && gm_bSubmitData)
                {
                    new GradeMessage().SubmitAllGradeMessage(gm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public void GradeMessageSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllGradeMessage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeMessage";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeMessageUpdate()
        {
            if (this._GradeMessageCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeMessage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeMessage";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void InsertGradeMessage(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMessage"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMessage";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SubmitAllGradeMessage(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMessage"))
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
                    this.dao.EntityName = "GradeMessage";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateGradeMessage(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMessage"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMessage";
                    this.dao.UpdateEntity(entity);
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
                if (this._CreateDate == null)
                {
                    this._GetGradeMessageByCode();
                }
                return this._CreateDate;
            }
            set
            {
                this._CreateDate = value;
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

        public string GradeMessageCode
        {
            get
            {
                return this._GradeMessageCode;
            }
            set
            {
                this._GradeMessageCode = value;
            }
        }

        public string MainDefineCode
        {
            get
            {
                if (this._MainDefineCode == null)
                {
                    this._GetGradeMessageByCode();
                }
                return this._MainDefineCode;
            }
            set
            {
                this._MainDefineCode = value;
            }
        }

        public string ProduceName
        {
            get
            {
                if (this._ProduceName == null)
                {
                    this._GetGradeMessageByCode();
                }
                return this._ProduceName;
            }
            set
            {
                this._ProduceName = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                if (this._ProjectCode == null)
                {
                    this._GetGradeMessageByCode();
                }
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string ProjectManage
        {
            get
            {
                if (this._ProjectManage == null)
                {
                    this._GetGradeMessageByCode();
                }
                return this._ProjectManage;
            }
            set
            {
                this._ProjectManage = value;
            }
        }

        public string State
        {
            get
            {
                if (this._State == null)
                {
                    this._GetGradeMessageByCode();
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
                if (this._SupplierCode == null)
                {
                    this._GetGradeMessageByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }
    }
}

