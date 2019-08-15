namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class GradeConsiderDiathesis
    {
        private string _ConsiderDiathesis = null;
        private string _ConsiderDiathesisCode = null;
        private StandardEntityDAO _dao;
        private string _GradeGuideline = null;
        private string _MainDefineCode = null;
        private string _ParentCode = null;
        private string _Percentage = null;
        private string _state = null;

        private void _GetGradeConsiderDiathesisByCode()
        {
            EntityData gradeConsiderDiathesisByCode = this.GetGradeConsiderDiathesisByCode(this._ConsiderDiathesisCode);
            this._ConsiderDiathesisCode = gradeConsiderDiathesisByCode.GetString("ConsiderDiathesisCode");
            this._MainDefineCode = gradeConsiderDiathesisByCode.GetString("MainDefineCode");
            this._ParentCode = gradeConsiderDiathesisByCode.GetString("ParentCode");
            this._ConsiderDiathesis = gradeConsiderDiathesisByCode.GetString("ConsiderDiathesis");
            this._GradeGuideline = gradeConsiderDiathesisByCode.GetString("GradeGuideline");
            this._Percentage = gradeConsiderDiathesisByCode.GetString("Percentage");
            this._state = gradeConsiderDiathesisByCode.GetString("state");
            gradeConsiderDiathesisByCode.Dispose();
        }

        private EntityData _GetGradeConsiderDiathesiss()
        {
            EntityData entitydata = new EntityData("GradeConsiderDiathesis");
            GradeConsiderDiathesisStrategyBuilder builder = new GradeConsiderDiathesisStrategyBuilder();
            if (this._ConsiderDiathesisCode != null)
            {
                builder.AddStrategy(new Strategy(GradeConsiderDiathesisStrategyName.ConsiderDiathesisCode, this._ConsiderDiathesisCode));
            }
            if (this._MainDefineCode != null)
            {
                builder.AddStrategy(new Strategy(GradeConsiderDiathesisStrategyName.MainDefineCode, this._MainDefineCode));
            }
            if (this._ParentCode != null)
            {
                builder.AddStrategy(new Strategy(GradeConsiderDiathesisStrategyName.ParentCode, this._ParentCode));
            }
            if (this._ConsiderDiathesis != null)
            {
                builder.AddStrategy(new Strategy(GradeConsiderDiathesisStrategyName.ConsiderDiathesis, this._ConsiderDiathesis));
            }
            if (this._GradeGuideline != null)
            {
                builder.AddStrategy(new Strategy(GradeConsiderDiathesisStrategyName.GradeGuideline, this._GradeGuideline));
            }
            if (this._Percentage != null)
            {
                builder.AddStrategy(new Strategy(GradeConsiderDiathesisStrategyName.Percentage, this._Percentage));
            }
            if (this._state != null)
            {
                builder.AddStrategy(new Strategy(GradeConsiderDiathesisStrategyName.state, this._state));
            }
            string sqlString = builder.BuildMainQueryString() + " order by ConsiderDiathesisCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDiathesis"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "GradeConsiderDiathesis";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData gradeConsiderDiathesisByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._ConsiderDiathesisCode == "")
            {
                flag = true;
                gradeConsiderDiathesisByCode = this.GetGradeConsiderDiathesisByCode("");
                this._ConsiderDiathesisCode = SystemManageDAO.GetNewSysCode("GradeConsiderDiathesis");
                newRecord = gradeConsiderDiathesisByCode.GetNewRecord();
            }
            else
            {
                gradeConsiderDiathesisByCode = this.GetGradeConsiderDiathesisByCode(this._ConsiderDiathesisCode);
                newRecord = gradeConsiderDiathesisByCode.CurrentRow;
            }
            if (this._ConsiderDiathesisCode != null)
            {
                newRecord["ConsiderDiathesisCode"] = this._ConsiderDiathesisCode;
            }
            if (this._MainDefineCode != null)
            {
                newRecord["MainDefineCode"] = this._MainDefineCode;
            }
            if (this._ParentCode != null)
            {
                newRecord["ParentCode"] = this._ParentCode;
            }
            if (this._ConsiderDiathesis != null)
            {
                newRecord["ConsiderDiathesis"] = this._ConsiderDiathesis;
            }
            if (this._GradeGuideline != null)
            {
                newRecord["GradeGuideline"] = this._GradeGuideline;
            }
            if (this._Percentage != null)
            {
                newRecord["Percentage"] = this._Percentage;
            }
            if (this._state != null)
            {
                newRecord["state"] = this._state;
            }
            if (flag)
            {
                gradeConsiderDiathesisByCode.AddNewRecord(newRecord);
            }
            return gradeConsiderDiathesisByCode;
        }

        private void DeleteGradeConsiderDiathesis(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDiathesis"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDiathesis";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllGradeConsiderDiathesis()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDiathesis"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDiathesis";
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

        private EntityData GetGradeConsiderDiathesisByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDiathesis"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDiathesis";
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

        public DataTable GetGradeConsiderDiathesiss()
        {
            return this._GetGradeConsiderDiathesiss().CurrentTable;
        }

        public void GradeConsiderDiathesisAdd()
        {
            if (this._ConsiderDiathesisCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeConsiderDiathesis(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDiathesis";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void GradeConsiderDiathesisDelete()
        {
            if (this._dao == null)
            {
                this.DeleteGradeConsiderDiathesis(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeConsiderDiathesis";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void GradeConsiderDiathesisSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllGradeConsiderDiathesis(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeConsiderDiathesis";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeConsiderDiathesisUpdate()
        {
            if (this._ConsiderDiathesisCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeConsiderDiathesis(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDiathesis";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void InsertGradeConsiderDiathesis(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDiathesis"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDiathesis";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllGradeConsiderDiathesis(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDiathesis"))
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
                    this.dao.EntityName = "GradeConsiderDiathesis";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateGradeConsiderDiathesis(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderDiathesis"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderDiathesis";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string ConsiderDiathesis
        {
            get
            {
                if ((this._ConsiderDiathesis == null) && (this._ConsiderDiathesisCode != null))
                {
                    this._GetGradeConsiderDiathesisByCode();
                }
                return this._ConsiderDiathesis;
            }
            set
            {
                this._ConsiderDiathesis = value;
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

        public string GradeGuideline
        {
            get
            {
                if ((this._GradeGuideline == null) && (this._ConsiderDiathesisCode != null))
                {
                    this._GetGradeConsiderDiathesisByCode();
                }
                return this._GradeGuideline;
            }
            set
            {
                this._GradeGuideline = value;
            }
        }

        public string MainDefineCode
        {
            get
            {
                if ((this._MainDefineCode == null) && (this._ConsiderDiathesisCode != null))
                {
                    this._GetGradeConsiderDiathesisByCode();
                }
                return this._MainDefineCode;
            }
            set
            {
                this._MainDefineCode = value;
            }
        }

        public string ParentCode
        {
            get
            {
                if ((this._ParentCode == null) && (this._ConsiderDiathesisCode != null))
                {
                    this._GetGradeConsiderDiathesisByCode();
                }
                return this._ParentCode;
            }
            set
            {
                this._ParentCode = value;
            }
        }

        public string Percentage
        {
            get
            {
                if ((this._Percentage == null) && (this._ConsiderDiathesisCode != null))
                {
                    this._GetGradeConsiderDiathesisByCode();
                }
                return this._Percentage;
            }
            set
            {
                this._Percentage = value;
            }
        }

        public string state
        {
            get
            {
                if ((this._state == null) && (this._ConsiderDiathesisCode != null))
                {
                    this._GetGradeConsiderDiathesisByCode();
                }
                return this._state;
            }
            set
            {
                this._state = value;
            }
        }
    }
}

