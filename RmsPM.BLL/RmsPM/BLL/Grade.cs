namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class Grade
    {
        private string _ConsiderDiathesisCode = null;
        private StandardEntityDAO _dao;
        private string _DepartmentDefineCode = null;
        private string _GradeCode = null;
        private string _GradeMessageCode = null;
        private int _GradeValue;

        private void _GetGradeByCode()
        {
            EntityData gradeByCode = this.GetGradeByCode(this._GradeCode);
            this._GradeCode = gradeByCode.GetString("GradeCode");
            this._GradeMessageCode = gradeByCode.GetString("GradeMessageCode");
            this._ConsiderDiathesisCode = gradeByCode.GetString("ConsiderDiathesisCode");
            this._DepartmentDefineCode = gradeByCode.GetString("DepartmentDefineCode");
            this._GradeValue = gradeByCode.GetInt("GradeValue");
            gradeByCode.Dispose();
        }

        private EntityData _GetGrades()
        {
            EntityData entitydata = new EntityData("Grade");
            GradeStrategyBuilder builder = new GradeStrategyBuilder();
            if (this._GradeCode != null)
            {
                builder.AddStrategy(new Strategy(GradeStrategyName.GradeCode, this._GradeCode));
            }
            if (this._GradeMessageCode != null)
            {
                builder.AddStrategy(new Strategy(GradeStrategyName.GradeMessageCode, this._GradeMessageCode));
            }
            if (this._ConsiderDiathesisCode != null)
            {
                builder.AddStrategy(new Strategy(GradeStrategyName.ConsiderDiathesisCode, this._ConsiderDiathesisCode));
            }
            if (this._DepartmentDefineCode != null)
            {
                builder.AddStrategy(new Strategy(GradeStrategyName.DepartmentDefineCode, this._DepartmentDefineCode));
            }
            if (this._GradeValue != 0)
            {
                builder.AddStrategy(new Strategy(GradeStrategyName.GradeValue, this._GradeValue.ToString()));
            }
            string sqlString = builder.BuildMainQueryString() + " order by GradeCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Grade"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "Grade";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData gradeByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._GradeCode == "")
            {
                flag = true;
                gradeByCode = this.GetGradeByCode("");
                this._GradeCode = SystemManageDAO.GetNewSysCode("Grade");
                newRecord = gradeByCode.GetNewRecord();
            }
            else
            {
                gradeByCode = this.GetGradeByCode(this._GradeCode);
                newRecord = gradeByCode.CurrentRow;
            }
            if (this._GradeCode != null)
            {
                newRecord["GradeCode"] = this._GradeCode;
            }
            if (this._GradeMessageCode != null)
            {
                newRecord["GradeMessageCode"] = this._GradeMessageCode;
            }
            if (this._ConsiderDiathesisCode != null)
            {
                newRecord["ConsiderDiathesisCode"] = this._ConsiderDiathesisCode;
            }
            if (this._DepartmentDefineCode != null)
            {
                newRecord["DepartmentDefineCode"] = this._DepartmentDefineCode;
            }
            newRecord["GradeValue"] = this._GradeValue;
            if (flag)
            {
                gradeByCode.AddNewRecord(newRecord);
            }
            return gradeByCode;
        }

        private void DeleteGrade(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Grade"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Grade";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllGrade()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Grade"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "Grade";
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

        public EntityData GetGrade()
        {
            return this._GetGrades();
        }

        private EntityData GetGradeByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Grade"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "Grade";
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

        public DataTable GetGrades()
        {
            return this._GetGrades().CurrentTable;
        }

        public void GradeAdd()
        {
            if (this._GradeCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGrade(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Grade";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void GradeDelete()
        {
            if (this._dao == null)
            {
                this.DeleteGrade(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Grade";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void GradeSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllGrade(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "Grade";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeUpdate()
        {
            if (this._GradeCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGrade(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "Grade";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void InsertGrade(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Grade"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Grade";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllGrade(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Grade"))
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
                    this.dao.EntityName = "Grade";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateGrade(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("Grade"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "Grade";
                    this.dao.UpdateEntity(entity);
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
                if (this._ConsiderDiathesisCode == null)
                {
                    this._GetGradeByCode();
                }
                return this._ConsiderDiathesisCode;
            }
            set
            {
                this._ConsiderDiathesisCode = value;
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
                if (this._DepartmentDefineCode == null)
                {
                    this._GetGradeByCode();
                }
                return this._DepartmentDefineCode;
            }
            set
            {
                this._DepartmentDefineCode = value;
            }
        }

        public string GradeCode
        {
            get
            {
                return this._GradeCode;
            }
            set
            {
                this._GradeCode = value;
            }
        }

        public string GradeMessageCode
        {
            get
            {
                if (this._GradeMessageCode == null)
                {
                    this._GetGradeByCode();
                }
                return this._GradeMessageCode;
            }
            set
            {
                this._GradeMessageCode = value;
            }
        }

        public int GradeValue
        {
            get
            {
                return this._GradeValue;
            }
            set
            {
                this._GradeValue = value;
            }
        }
    }
}

