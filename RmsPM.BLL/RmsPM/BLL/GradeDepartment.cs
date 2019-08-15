namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class GradeDepartment
    {
        private string _AdjustCoefficient = null;
        private StandardEntityDAO _dao;
        private string _DepartmentDefineCode = null;
        private string _DepartmentName = null;
        private string _MainDefineCode = null;
        private string _Percentage = null;

        private void _GetGradeDepartmentByCode()
        {
            EntityData gradeDepartmentByCode = this.GetGradeDepartmentByCode(this._DepartmentDefineCode);
            this._DepartmentDefineCode = gradeDepartmentByCode.GetString("DepartmentDefineCode");
            this._MainDefineCode = gradeDepartmentByCode.GetString("MainDefineCode");
            this._DepartmentName = gradeDepartmentByCode.GetString("DepartmentName");
            this._AdjustCoefficient = gradeDepartmentByCode.GetString("AdjustCoefficient");
            this._Percentage = gradeDepartmentByCode.GetString("Percentage");
            gradeDepartmentByCode.Dispose();
        }

        private EntityData _GetGradeDepartments()
        {
            EntityData entitydata = new EntityData("GradeDepartment");
            GradeDepartmentStrategyBuilder builder = new GradeDepartmentStrategyBuilder();
            if (this._DepartmentDefineCode != null)
            {
                builder.AddStrategy(new Strategy(GradeDepartmentStrategyName.DepartmentDefineCode, this._DepartmentDefineCode));
            }
            if (this._MainDefineCode != null)
            {
                builder.AddStrategy(new Strategy(GradeDepartmentStrategyName.MainDefineCode, this._MainDefineCode));
            }
            if (this._DepartmentName != null)
            {
                builder.AddStrategy(new Strategy(GradeDepartmentStrategyName.DepartmentName, this._DepartmentName));
            }
            if (this._AdjustCoefficient != null)
            {
                builder.AddStrategy(new Strategy(GradeDepartmentStrategyName.AdjustCoefficient, this._AdjustCoefficient));
            }
            if (this._Percentage != null)
            {
                builder.AddStrategy(new Strategy(GradeDepartmentStrategyName.Percentage, this._Percentage));
            }
            string sqlString = builder.BuildMainQueryString() + " order by DepartmentDefineCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartment"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "GradeDepartment";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData gradeDepartmentByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._DepartmentDefineCode == "")
            {
                flag = true;
                gradeDepartmentByCode = this.GetGradeDepartmentByCode("");
                this._DepartmentDefineCode = SystemManageDAO.GetNewSysCode("GradeDepartment");
                newRecord = gradeDepartmentByCode.GetNewRecord();
            }
            else
            {
                gradeDepartmentByCode = this.GetGradeDepartmentByCode(this._DepartmentDefineCode);
                newRecord = gradeDepartmentByCode.CurrentRow;
            }
            if (this._DepartmentDefineCode != null)
            {
                newRecord["DepartmentDefineCode"] = this._DepartmentDefineCode;
            }
            if (this._MainDefineCode != null)
            {
                newRecord["MainDefineCode"] = this._MainDefineCode;
            }
            if (this._DepartmentName != null)
            {
                newRecord["DepartmentName"] = this._DepartmentName;
            }
            if (this._AdjustCoefficient != null)
            {
                newRecord["AdjustCoefficient"] = this._AdjustCoefficient;
            }
            if (this._Percentage != null)
            {
                newRecord["Percentage"] = this._Percentage;
            }
            if (flag)
            {
                gradeDepartmentByCode.AddNewRecord(newRecord);
            }
            return gradeDepartmentByCode;
        }

        private void DeleteGradeDepartment(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartment"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeDepartment";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllGradeDepartment()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartment"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeDepartment";
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

        private EntityData GetGradeDepartmentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartment"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeDepartment";
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

        public DataTable GetGradeDepartments()
        {
            return this._GetGradeDepartments().CurrentTable;
        }

        public void GradeDepartmentAdd()
        {
            if (this._DepartmentDefineCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeDepartment(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeDepartment";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void GradeDepartmentDelete()
        {
            if (this._dao == null)
            {
                this.DeleteGradeDepartment(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeDepartment";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void GradeDepartmentSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllGradeDepartment(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeDepartment";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeDepartmentUpdate()
        {
            if (this._DepartmentDefineCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeDepartment(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeDepartment";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void InsertGradeDepartment(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartment"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeDepartment";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllGradeDepartment(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartment"))
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
                    this.dao.EntityName = "GradeDepartment";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateGradeDepartment(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeDepartment"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeDepartment";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string AdjustCoefficient
        {
            get
            {
                if ((this._AdjustCoefficient == null) && (this._DepartmentDefineCode != null))
                {
                    this._GetGradeDepartmentByCode();
                }
                return this._AdjustCoefficient;
            }
            set
            {
                this._AdjustCoefficient = value;
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
                this._DepartmentDefineCode = value;
            }
        }

        public string DepartmentName
        {
            get
            {
                if ((this._DepartmentName == null) && (this._DepartmentDefineCode != null))
                {
                    this._GetGradeDepartmentByCode();
                }
                return this._DepartmentName;
            }
            set
            {
                this._DepartmentName = value;
            }
        }

        public string MainDefineCode
        {
            get
            {
                if ((this._MainDefineCode == null) && (this._DepartmentDefineCode != null))
                {
                    this._GetGradeDepartmentByCode();
                }
                return this._MainDefineCode;
            }
            set
            {
                this._MainDefineCode = value;
            }
        }

        public string Percentage
        {
            get
            {
                if ((this._Percentage == null) && (this._DepartmentDefineCode != null))
                {
                    this._GetGradeDepartmentByCode();
                }
                return this._Percentage;
            }
            set
            {
                this._Percentage = value;
            }
        }
    }
}

