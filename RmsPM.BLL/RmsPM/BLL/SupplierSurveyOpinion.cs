namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class SupplierSurveyOpinion
    {
        private string _AdviceGrade = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _Grade = null;
        private string _Remark = null;
        private string _State = null;
        private string _SupplierCode = null;
        private string _SupplierSurveyOpinionCode = null;
        private string _SurveyDate = null;
        private string _WorkName = null;
        private string _ZYName = null;

        private void _GetSupplierSurveyOpinionByCode()
        {
            EntityData supplierSurveyOpinionByCode = this.GetSupplierSurveyOpinionByCode(this._SupplierSurveyOpinionCode);
            this._SupplierSurveyOpinionCode = supplierSurveyOpinionByCode.GetString("SupplierSurveyOpinionCode");
            this._WorkName = supplierSurveyOpinionByCode.GetString("WorkName");
            this._SupplierCode = supplierSurveyOpinionByCode.GetString("SupplierCode");
            this._ZYName = supplierSurveyOpinionByCode.GetString("ZYName");
            this._SurveyDate = supplierSurveyOpinionByCode.GetDateTime("SurveyDate");
            this._Remark = supplierSurveyOpinionByCode.GetString("Remark");
            this._Grade = supplierSurveyOpinionByCode.GetString("Grade").ToString();
            this._AdviceGrade = supplierSurveyOpinionByCode.GetString("AdviceGrade").ToString();
            this._Flag = supplierSurveyOpinionByCode.GetString("Flag").ToString();
            this._State = supplierSurveyOpinionByCode.GetString("State");
            supplierSurveyOpinionByCode.Dispose();
        }

        private EntityData _GetSupplierSurveyOpinions()
        {
            EntityData entitydata = new EntityData("SupplierSurveyOpinion");
            SupplierSurveyOpinionStrategyBuilder builder = new SupplierSurveyOpinionStrategyBuilder();
            if (this._SupplierSurveyOpinionCode != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.SupplierSurveyOpinionCode, this._SupplierSurveyOpinionCode));
            }
            if (this._WorkName != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.WorkName, this._WorkName));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._ZYName != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.ZYName, this._ZYName));
            }
            if (this._SurveyDate != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.SurveyDate, this._SurveyDate));
            }
            if (this._Grade != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.Grade, this._Grade));
            }
            if (this._AdviceGrade != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.AdviceGrade, this._AdviceGrade));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.Flag, this._Flag));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(SupplierSurveyOpinionStrategyName.State, this._State));
            }
            string sqlString = builder.BuildMainQueryString() + " order by SupplierSurveyOpinionCode Desc";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierSurveyOpinion"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "SupplierSurveyOpinion";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData supplierSurveyOpinionByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._SupplierSurveyOpinionCode == "")
            {
                flag = true;
                supplierSurveyOpinionByCode = this.GetSupplierSurveyOpinionByCode("");
                this._SupplierSurveyOpinionCode = SystemManageDAO.GetNewSysCode("SupplierSurveyOpinion");
                newRecord = supplierSurveyOpinionByCode.GetNewRecord();
            }
            else
            {
                supplierSurveyOpinionByCode = this.GetSupplierSurveyOpinionByCode(this._SupplierSurveyOpinionCode);
                newRecord = supplierSurveyOpinionByCode.CurrentRow;
            }
            if (this._SupplierSurveyOpinionCode != null)
            {
                newRecord["SupplierSurveyOpinionCode"] = this._SupplierSurveyOpinionCode;
            }
            if (this._WorkName != null)
            {
                newRecord["WorkName"] = this._WorkName;
            }
            if (this._SupplierCode != null)
            {
                newRecord["SupplierCode"] = this._SupplierCode;
            }
            if (this._ZYName != null)
            {
                newRecord["ZYName"] = this._ZYName;
            }
            if (this._SurveyDate != null)
            {
                newRecord["SurveyDate"] = this._SurveyDate;
            }
            if (this._Remark != null)
            {
                newRecord["Remark"] = this._Remark;
            }
            if (this._AdviceGrade != null)
            {
                newRecord["AdviceGrade"] = this._AdviceGrade;
            }
            if (this._Grade != null)
            {
                newRecord["Grade"] = this._Grade;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (flag)
            {
                supplierSurveyOpinionByCode.AddNewRecord(newRecord);
            }
            return supplierSurveyOpinionByCode;
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

        public static void DeleteStandard_SupplierSurveyOpinion(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("SupplierSurveyOpinion"))
                {
                    ydao.SubmitEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void DeleteSupplierSurveyOpinion(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierSurveyOpinion"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "SupplierSurveyOpinion";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllSupplierSurveyOpinion()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierSurveyOpinion"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "SupplierSurveyOpinion";
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

        public static DataTable GetSupplierGradeType()
        {
            EntityData allSupplierGradeType = SupplierGradeTypeDao.GetAllSupplierGradeType();
            if (allSupplierGradeType != null)
            {
                return allSupplierGradeType.CurrentTable;
            }
            return null;
        }

        public EntityData GetSupplierSurveyOpinionByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierSurveyOpinion"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "SupplierSurveyOpinion";
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

        public DataTable GetSupplierSurveyOpinions()
        {
            return this._GetSupplierSurveyOpinions().CurrentTable;
        }

        public static string GetSupplierSurveyOpinionStatusName(string state)
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

        private void InsertSupplierSurveyOpinion(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierSurveyOpinion"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "SupplierSurveyOpinion";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SubmitAllSupplierSurveyOpinion(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierSurveyOpinion"))
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
                    this.dao.EntityName = "SupplierSurveyOpinion";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public void SupplierSurveyOpinionAdd()
        {
            if (this._dao == null)
            {
                this.SubmitAllSupplierSurveyOpinion(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "SupplierSurveyOpinion";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void SupplierSurveyOpinionDelete()
        {
            if (this._dao == null)
            {
                this.DeleteSupplierSurveyOpinion(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "SupplierSurveyOpinion";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public static bool SupplierSurveyOpinionStatusChange(string SupplierSurveyOpinionCode, int gm_iStatus)
        {
            return SupplierSurveyOpinionStatusChange(SupplierSurveyOpinionCode, gm_iStatus, null, true);
        }

        public static bool SupplierSurveyOpinionStatusChange(EntityData gm_Entity, int gm_iStatus)
        {
            return SupplierSurveyOpinionStatusChange(gm_Entity, "", gm_iStatus, null, false);
        }

        public static bool SupplierSurveyOpinionStatusChange(string SupplierSurveyOpinionCode, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return SupplierSurveyOpinionStatusChange(SupplierSurveyOpinionCode, gm_iStatus, gm_iOriginalStatus, true);
        }

        public static bool SupplierSurveyOpinionStatusChange(EntityData gm_Entity, int gm_iStatus, bool gm_bSubmitData)
        {
            return SupplierSurveyOpinionStatusChange(gm_Entity, "", gm_iStatus, null, gm_bSubmitData);
        }

        public static bool SupplierSurveyOpinionStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return SupplierSurveyOpinionStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, false);
        }

        public static bool SupplierSurveyOpinionStatusChange(string SupplierSurveyOpinionCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            SupplierSurveyOpinion opinion = new SupplierSurveyOpinion();
            return SupplierSurveyOpinionStatusChange(opinion.GetSupplierSurveyOpinionByCode(SupplierSurveyOpinionCode), SupplierSurveyOpinionCode, gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool SupplierSurveyOpinionStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return SupplierSurveyOpinionStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool SupplierSurveyOpinionStatusChange(EntityData gm_Entity, string SupplierSurveyOpinionCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                gm_Entity.SetCurrentTable("SupplierSurveyOpinion");
                if (SupplierSurveyOpinionCode.Trim() == "")
                {
                    if (gm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("SupplierSurveyOpinionCode='{0}'", SupplierSurveyOpinionCode.Trim());
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
                    new SupplierSurveyOpinion().SubmitAllSupplierSurveyOpinion(gm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public void SupplierSurveyOpinionSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllSupplierSurveyOpinion(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "SupplierSurveyOpinion";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void SupplierSurveyOpinionUpdate()
        {
            if ((this._SupplierSurveyOpinionCode != null) || (this._SupplierSurveyOpinionCode != ""))
            {
                if (this._dao == null)
                {
                    this.SubmitAllSupplierSurveyOpinion(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "SupplierSurveyOpinion";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void UpdateSupplierSurveyOpinion(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierSurveyOpinion"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "SupplierSurveyOpinion";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string AdviceGrade
        {
            get
            {
                if (this._AdviceGrade == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._AdviceGrade;
            }
            set
            {
                this._AdviceGrade = value;
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
                if (this._Flag == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string Grade
        {
            get
            {
                if (this._Grade == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._Grade;
            }
            set
            {
                this._Grade = value;
            }
        }

        public string Remark
        {
            get
            {
                if (this._Remark == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string State
        {
            get
            {
                if (this._State == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
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
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }

        public string SupplierSurveyOpinionCode
        {
            get
            {
                return this._SupplierSurveyOpinionCode;
            }
            set
            {
                this._SupplierSurveyOpinionCode = value;
            }
        }

        public string SurveyDate
        {
            get
            {
                if (this._SurveyDate == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._SurveyDate;
            }
            set
            {
                this._SurveyDate = value;
            }
        }

        public string WorkName
        {
            get
            {
                if (this._WorkName == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._WorkName;
            }
            set
            {
                this._WorkName = value;
            }
        }

        public string ZYName
        {
            get
            {
                if (this._ZYName == null)
                {
                    this._GetSupplierSurveyOpinionByCode();
                }
                return this._ZYName;
            }
            set
            {
                this._ZYName = value;
            }
        }
    }
}

