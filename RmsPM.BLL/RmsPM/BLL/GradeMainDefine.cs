namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class GradeMainDefine
    {
        private StandardEntityDAO _dao;
        private string _MainDefineCode = null;
        private string _Name = null;
        private string _state = null;

        private void _GetGradeMainDefineByCode()
        {
            EntityData gradeMainDefineByCode = this.GetGradeMainDefineByCode(this._MainDefineCode);
            this._MainDefineCode = gradeMainDefineByCode.GetString("MainDefineCode");
            this._Name = gradeMainDefineByCode.GetString("Name");
            this._state = gradeMainDefineByCode.GetString("state");
            gradeMainDefineByCode.Dispose();
        }

        private EntityData _GetGradeMainDefines()
        {
            EntityData entitydata = new EntityData("GradeMainDefine");
            GradeMainDefineStrategyBuilder builder = new GradeMainDefineStrategyBuilder();
            if (this._MainDefineCode != null)
            {
                builder.AddStrategy(new Strategy(GradeMainDefineStrategyName.MainDefineCode, this._MainDefineCode));
            }
            if (this._Name != null)
            {
                builder.AddStrategy(new Strategy(GradeMainDefineStrategyName.Name, this._Name));
            }
            if (this._state != null)
            {
                builder.AddStrategy(new Strategy(GradeMainDefineStrategyName.state, this._state));
            }
            string sqlString = builder.BuildMainQueryString() + " order by MainDefineCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMainDefine"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "GradeMainDefine";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData gradeMainDefineByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._MainDefineCode == "")
            {
                flag = true;
                gradeMainDefineByCode = this.GetGradeMainDefineByCode("");
                this._MainDefineCode = SystemManageDAO.GetNewSysCode("GradeMainDefine");
                newRecord = gradeMainDefineByCode.GetNewRecord();
            }
            else
            {
                gradeMainDefineByCode = this.GetGradeMainDefineByCode(this._MainDefineCode);
                newRecord = gradeMainDefineByCode.CurrentRow;
            }
            if (this._MainDefineCode != null)
            {
                newRecord["MainDefineCode"] = this._MainDefineCode;
            }
            if (this._Name != null)
            {
                newRecord["Name"] = this._Name;
            }
            if (this._state != null)
            {
                newRecord["state"] = this._state;
            }
            if (flag)
            {
                gradeMainDefineByCode.AddNewRecord(newRecord);
            }
            return gradeMainDefineByCode;
        }

        private void DeleteGradeMainDefine(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMainDefine"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMainDefine";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllGradeMainDefine()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMainDefine"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMainDefine";
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

        private EntityData GetGradeMainDefineByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMainDefine"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMainDefine";
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

        public DataTable GetGradeMainDefines()
        {
            return this._GetGradeMainDefines().CurrentTable;
        }

        public void GradeMainDefineAdd()
        {
            if (this._MainDefineCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeMainDefine(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeMainDefine";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void GradeMainDefineDelete()
        {
            if (this._dao == null)
            {
                this.DeleteGradeMainDefine(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeMainDefine";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void GradeMainDefineSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllGradeMainDefine(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeMainDefine";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeMainDefineUpdate()
        {
            if (this._MainDefineCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllGradeMainDefine(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeMainDefine";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void InsertGradeMainDefine(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMainDefine"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMainDefine";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SubmitAllGradeMainDefine(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMainDefine"))
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
                    this.dao.EntityName = "GradeMainDefine";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateGradeMainDefine(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeMainDefine"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeMainDefine";
                    this.dao.UpdateEntity(entity);
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

        public string MainDefineCode
        {
            get
            {
                return this._MainDefineCode;
            }
            set
            {
                this._MainDefineCode = value;
            }
        }

        public string Name
        {
            get
            {
                if ((this._Name == null) && (this._MainDefineCode != null))
                {
                    this._GetGradeMainDefineByCode();
                }
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public string state
        {
            get
            {
                if ((this._state == null) && (this._MainDefineCode != null))
                {
                    this._GetGradeMainDefineByCode();
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

