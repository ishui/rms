namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class GradeDepartmentPercentage
    {
        private decimal _AdjustCoefficient;
        private StandardEntityDAO _dao;
        private string _DepartmentDefineCode = null;
        private string _DepartmentPercentageCode = null;
        private string _GradeMessageCode = null;
        private string _MainDefineCode = null;
        private decimal _Percentage;

        private void _GetGradeDepartmentPercentageByCode()
        {
            try
            {
                EntityData gradeDepartmentPercentageByCode = GetGradeDepartmentPercentageByCode(this._DepartmentPercentageCode);
                this._DepartmentPercentageCode = gradeDepartmentPercentageByCode.GetString("DepartmentPercentageCode");
                this._MainDefineCode = gradeDepartmentPercentageByCode.GetString("MainDefineCode");
                this._GradeMessageCode = gradeDepartmentPercentageByCode.GetString("GradeMessageCode");
                this._DepartmentDefineCode = gradeDepartmentPercentageByCode.GetString("DepartmentDefineCode");
                this._Percentage = gradeDepartmentPercentageByCode.GetDecimal("Percentage");
                this._AdjustCoefficient = gradeDepartmentPercentageByCode.GetDecimal("AdjustCoefficient");
                gradeDepartmentPercentageByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetGradeDepartmentPercentages()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("GradeDepartmentPercentage");
                GradeDepartmentPercentageStrategyBuilder builder = new GradeDepartmentPercentageStrategyBuilder();
                if (this._DepartmentPercentageCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeDepartmentPercentageStrategyName.DepartmentPercentageCode, this._DepartmentPercentageCode));
                }
                if (this._MainDefineCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeDepartmentPercentageStrategyName.MainDefineCode, this._MainDefineCode));
                }
                if (this._GradeMessageCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeDepartmentPercentageStrategyName.GradeMessageCode, this._GradeMessageCode));
                }
                if (this._DepartmentDefineCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeDepartmentPercentageStrategyName.DepartmentDefineCode, this._DepartmentDefineCode));
                }
                if (this._Percentage != 0M)
                {
                    builder.AddStrategy(new Strategy(GradeDepartmentPercentageStrategyName.Percentage, this._Percentage.ToString()));
                }
                string sqlString = builder.BuildMainQueryString() + " order by DepartmentPercentageCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartmentPercentage"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeDepartmentPercentage";
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
                EntityData gradeDepartmentPercentageByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._DepartmentPercentageCode == "")
                {
                    flag = true;
                    gradeDepartmentPercentageByCode = GetGradeDepartmentPercentageByCode("", this.dao);
                    this._DepartmentPercentageCode = SystemManageDAO.GetNewSysCode("GradeDepartmentPercentage");
                    newRecord = gradeDepartmentPercentageByCode.GetNewRecord();
                }
                else
                {
                    gradeDepartmentPercentageByCode = GetGradeDepartmentPercentageByCode(this._DepartmentPercentageCode, this.dao);
                    if (gradeDepartmentPercentageByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = gradeDepartmentPercentageByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = gradeDepartmentPercentageByCode.CurrentRow;
                    }
                }
                if (this._DepartmentPercentageCode != null)
                {
                    newRecord["DepartmentPercentageCode"] = this._DepartmentPercentageCode;
                }
                if (this._MainDefineCode != null)
                {
                    newRecord["MainDefineCode"] = this._MainDefineCode;
                }
                if (this._GradeMessageCode != null)
                {
                    newRecord["GradeMessageCode"] = this._GradeMessageCode;
                }
                if (this._DepartmentDefineCode != null)
                {
                    newRecord["DepartmentDefineCode"] = this._DepartmentDefineCode;
                }
                bool flag2 = 1 == 0;
                newRecord["Percentage"] = this._Percentage;
                flag2 = 1 == 0;
                newRecord["AdjustCoefficient"] = this._AdjustCoefficient;
                if (flag)
                {
                    gradeDepartmentPercentageByCode.AddNewRecord(newRecord);
                }
                data2 = gradeDepartmentPercentageByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteGradeDepartmentPercentage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartmentPercentage"))
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

        public static EntityData GetAllGradeDepartmentPercentage()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartmentPercentage"))
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

        public static EntityData GetGradeDepartmentPercentageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartmentPercentage"))
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

        public static EntityData GetGradeDepartmentPercentageByCode(string code, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                EntityData gradeDepartmentPercentageByCode;
                if (dao != null)
                {
                    dao.EntityName = "GradeDepartmentPercentage";
                    gradeDepartmentPercentageByCode = dao.SelectbyPrimaryKey(code);
                }
                else
                {
                    gradeDepartmentPercentageByCode = GetGradeDepartmentPercentageByCode(code);
                }
                data2 = gradeDepartmentPercentageByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetGradeDepartmentPercentages()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetGradeDepartmentPercentages().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public DataTable GetLastDepartmentPercentage(string GradeMessageCode, string MainDefineCode)
        {
            return this.GetLastDepartmentPercentage("", GradeMessageCode, MainDefineCode);
        }

        public DataTable GetLastDepartmentPercentage(string supplierCode, string GradeMessageCode, string MainDefineCode)
        {
            GradeDepartmentPercentage percentage;
            DataTable table = new DataTable();
            if (GradeMessageCode != "")
            {
                percentage = new GradeDepartmentPercentage();
                percentage.GradeMessageCode = "'" + GradeMessageCode + "'";
                percentage.MainDefineCode = MainDefineCode;
                percentage.dao = this.dao;
                return percentage.GetGradeDepartmentPercentages();
            }
            GradeMessage message = new GradeMessage();
            message.SupplierCode = supplierCode;
            message.MainDefineCode = MainDefineCode;
            DataTable gradeMessages = new DataTable();
            message.dao = this.dao;
            gradeMessages = message.GetGradeMessages();
            string text = "";
            int num = 0;
            if (gradeMessages != null)
            {
                foreach (DataRow row in gradeMessages.Select())
                {
                    if (num != (gradeMessages.Rows.Count - 1))
                    {
                        text = text + "'" + row["GradeMessageCode"].ToString() + "',";
                    }
                    else
                    {
                        text = text + "'" + row["GradeMessageCode"].ToString() + "'";
                    }
                    num++;
                }
            }
            percentage = new GradeDepartmentPercentage();
            percentage.GradeMessageCode = text;
            percentage.MainDefineCode = MainDefineCode;
            percentage.dao = this.dao;
            return percentage.GetGradeDepartmentPercentages();
        }

        public void GradeDepartmentPercentageAdd()
        {
            if (this._dao == null)
            {
                SubmitAllGradeDepartmentPercentage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeDepartmentPercentage";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeDepartmentPercentageDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteGradeDepartmentPercentage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeDepartmentPercentage";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertGradeDepartmentPercentage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartmentPercentage"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllGradeDepartmentPercentage(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartmentPercentage"))
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

        public static void UpdateGradeDepartmentPercentage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartmentPercentage"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public decimal AdjustCoefficient
        {
            get
            {
                return this._AdjustCoefficient;
            }
            set
            {
                if (this._AdjustCoefficient != value)
                {
                    this._AdjustCoefficient = value;
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

        public string DepartmentDefineCode
        {
            get
            {
                return this._DepartmentDefineCode;
            }
            set
            {
                if (this._DepartmentDefineCode != value)
                {
                    this._DepartmentDefineCode = value;
                }
            }
        }

        public string DepartmentPercentageCode
        {
            get
            {
                return this._DepartmentPercentageCode;
            }
            set
            {
                if (this._DepartmentPercentageCode != value)
                {
                    this._DepartmentPercentageCode = value;
                }
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
                if (this._GradeMessageCode != value)
                {
                    this._GradeMessageCode = value;
                }
            }
        }

        public string MainDefineCode
        {
            get
            {
                return this._MainDefineCode;
            }
            set
            {
                if (this._MainDefineCode != value)
                {
                    this._MainDefineCode = value;
                }
            }
        }

        public decimal Percentage
        {
            get
            {
                return this._Percentage;
            }
            set
            {
                if (this._Percentage != value)
                {
                    this._Percentage = value;
                }
            }
        }
    }
}

