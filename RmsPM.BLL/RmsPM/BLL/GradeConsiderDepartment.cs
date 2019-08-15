namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class GradeConsiderDepartment
    {
        private string _ConsiderDiathesisCode = null;
        private StandardEntityDAO _dao;
        private string _DepartmentDefineCode = null;
        private string _Flag = null;
        private string _GradeConsiderDepartmentCode = null;

        private void _GetGradeConsiderDepartmentByCode()
        {
            try
            {
                EntityData gradeConsiderDepartmentByCode = GetGradeConsiderDepartmentByCode(this._GradeConsiderDepartmentCode);
                this._GradeConsiderDepartmentCode = gradeConsiderDepartmentByCode.GetString("GradeConsiderDepartmentCode");
                this._DepartmentDefineCode = gradeConsiderDepartmentByCode.GetString("DepartmentDefineCode");
                this._ConsiderDiathesisCode = gradeConsiderDepartmentByCode.GetString("ConsiderDiathesisCode");
                this._Flag = gradeConsiderDepartmentByCode.GetString("Flag");
                gradeConsiderDepartmentByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetGradeConsiderDepartments()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("GradeConsiderDepartment");
                GradeConsiderDepartmentStrategyBuilder builder = new GradeConsiderDepartmentStrategyBuilder();
                if (this._GradeConsiderDepartmentCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderDepartmentStrategyName.GradeConsiderDepartmentCode, this._GradeConsiderDepartmentCode));
                }
                if (this._DepartmentDefineCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderDepartmentStrategyName.DepartmentDefineCode, this._DepartmentDefineCode));
                }
                if (this._ConsiderDiathesisCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderDepartmentStrategyName.ConsiderDiathesisCode, this._ConsiderDiathesisCode));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderDepartmentStrategyName.Flag, this._Flag));
                }
                string sqlString = builder.BuildMainQueryString() + " order by GradeConsiderDepartmentCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDepartment";
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

        private EntityData _GetLocalConsiderDepartments()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("GradeConsiderDepartment");
                string sqlString = "select * from GradeConsiderDepartment";
                sqlString = sqlString + " where GradeConsiderDepartment.ConsiderDiathesisCode in (select GradeConsiderDiathesis.ConsiderDiathesisCode from GradeConsiderDiathesis where ParentCode='') " + " order by GradeConsiderDepartmentCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDepartment";
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
                EntityData gradeConsiderDepartmentByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._GradeConsiderDepartmentCode == "")
                {
                    flag = true;
                    gradeConsiderDepartmentByCode = GetGradeConsiderDepartmentByCode("");
                    this._GradeConsiderDepartmentCode = SystemManageDAO.GetNewSysCode("GradeConsiderDepartment");
                    newRecord = gradeConsiderDepartmentByCode.GetNewRecord();
                }
                else
                {
                    gradeConsiderDepartmentByCode = GetGradeConsiderDepartmentByCode(this._GradeConsiderDepartmentCode);
                    if (gradeConsiderDepartmentByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = gradeConsiderDepartmentByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = gradeConsiderDepartmentByCode.CurrentRow;
                    }
                }
                if (this._GradeConsiderDepartmentCode != null)
                {
                    newRecord["GradeConsiderDepartmentCode"] = this._GradeConsiderDepartmentCode;
                }
                if (this._DepartmentDefineCode != null)
                {
                    newRecord["DepartmentDefineCode "] = this._DepartmentDefineCode;
                }
                if (this._ConsiderDiathesisCode != null)
                {
                    newRecord["ConsiderDiathesisCode"] = this._ConsiderDiathesisCode;
                }
                if (this._Flag != null)
                {
                    newRecord["Flag"] = this._Flag;
                }
                if (flag)
                {
                    gradeConsiderDepartmentByCode.AddNewRecord(newRecord);
                }
                data2 = gradeConsiderDepartmentByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteGradeConsiderDepartment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
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

        public string GetAdjustCoefficient(string DepartmentCode, string GradeMessageCode)
        {
            return this.GetAdjustCoefficient(DepartmentCode, GradeMessageCode, "100001");
        }

        public string GetAdjustCoefficient(string DepartmentCode, string GradeMessageCode, string MainDefineCode)
        {
            DataTable localConsiderDepartments = this.GetLocalConsiderDepartments();
            decimal num = 0M;
            GradeConsiderPercentage percentage = new GradeConsiderPercentage();
            percentage.dao = this.dao;
            DataTable lastConsiderPercentage = percentage.GetLastConsiderPercentage(GradeMessageCode, MainDefineCode);
            foreach (DataRow row in localConsiderDepartments.Select("DepartmentDefineCode='" + DepartmentCode + "' and flag='0'"))
            {
                if (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"] + "'").Length != 0)
                {
                    num += Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"] + "'")[0]["Percentage"]);
                }
            }
            decimal num2 = 0M;
            if (Convert.ToDecimal((decimal) (1M - num)) != 0M)
            {
                num2 = 1M / (1M - num);
            }
            return num2.ToString();
        }

        public static EntityData GetAllGradeConsiderDepartment()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
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

        public static EntityData GetGradeConsiderDepartmentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
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

        public static EntityData GetGradeConsiderDepartmentByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetGradeConsiderDepartments()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetGradeConsiderDepartments().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public DataTable GetLocalConsiderDepartments()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetLocalConsiderDepartments().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public void GradeConsiderDepartmentAdd()
        {
            if (this._dao == null)
            {
                SubmitAllGradeConsiderDepartment(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeConsiderDepartment";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeConsiderDepartmentDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteGradeConsiderDepartment(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDepartment";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertGradeConsiderDepartment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllGradeConsiderDepartment(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
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

        public static void UpdateGradeConsiderDepartment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDepartment"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string ConsiderDiathesisCode
        {
            get
            {
                return this._ConsiderDiathesisCode;
            }
            set
            {
                if (this._ConsiderDiathesisCode != value)
                {
                    this._ConsiderDiathesisCode = value;
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

        public string Flag
        {
            get
            {
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

        public string GradeConsiderDepartmentCode
        {
            get
            {
                return this._GradeConsiderDepartmentCode;
            }
            set
            {
                if (this._GradeConsiderDepartmentCode != value)
                {
                    this._GradeConsiderDepartmentCode = value;
                }
            }
        }
    }
}

