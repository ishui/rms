namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class OAComputerMaintenance
    {
        private string _ApplyUser = null;
        private string _ComputerMaintenanceCode = null;
        private string _ConkOutText = null;
        private StandardEntityDAO _dao;
        private string _MaintenanceContext = null;
        private string _MaintenanceItem = null;
        private string _Unit = null;

        private void _GetOAComputerMaintenanceByCode()
        {
            EntityData oAComputerMaintenanceByCode = this.GetOAComputerMaintenanceByCode(this._ComputerMaintenanceCode);
            this._ComputerMaintenanceCode = oAComputerMaintenanceByCode.GetString("ComputerMaintenanceCode");
            this._Unit = oAComputerMaintenanceByCode.GetString("Unit");
            this._ApplyUser = oAComputerMaintenanceByCode.GetString("ApplyUser");
            this._MaintenanceItem = oAComputerMaintenanceByCode.GetString("MaintenanceItem");
            this._MaintenanceContext = oAComputerMaintenanceByCode.GetString("MaintenanceContext");
            this._ConkOutText = oAComputerMaintenanceByCode.GetString("ConkOutText");
            oAComputerMaintenanceByCode.Dispose();
        }

        private EntityData _GetOAComputerMaintenances()
        {
            EntityData entitydata = new EntityData("OAComputerMaintenance");
            OAComputerMaintenanceStrategyBuilder builder = new OAComputerMaintenanceStrategyBuilder();
            if (this._ComputerMaintenanceCode != null)
            {
                builder.AddStrategy(new Strategy(OAComputerMaintenanceStrategyName.ComputerMaintenanceCode, this._ComputerMaintenanceCode));
            }
            if (this._Unit != null)
            {
                builder.AddStrategy(new Strategy(OAComputerMaintenanceStrategyName.Unit, this._Unit));
            }
            if (this._ApplyUser != null)
            {
                builder.AddStrategy(new Strategy(OAComputerMaintenanceStrategyName.ApplyUser, this._ApplyUser));
            }
            if (this._MaintenanceItem != null)
            {
                builder.AddStrategy(new Strategy(OAComputerMaintenanceStrategyName.MaintenanceItem, this._MaintenanceItem));
            }
            if (this._MaintenanceContext != null)
            {
                builder.AddStrategy(new Strategy(OAComputerMaintenanceStrategyName.MaintenanceContext, this._MaintenanceContext));
            }
            if (this._ConkOutText != null)
            {
                builder.AddStrategy(new Strategy(OAComputerMaintenanceStrategyName.ConkOutText, this._ConkOutText));
            }
            string queryString = builder.BuildMainQueryString() + " order by ComputerMaintenanceCode";
            if (this._dao == null)
            {
                QueryAgent agent = new QueryAgent();
                return agent.FillEntityData("OAComputerMaintenance", queryString);
            }
            this.dao.FillEntity(queryString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData oAComputerMaintenanceByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._ComputerMaintenanceCode == "")
            {
                flag = true;
                oAComputerMaintenanceByCode = this.GetOAComputerMaintenanceByCode("");
                this._ComputerMaintenanceCode = SystemManageDAO.GetNewSysCode("OAComputerMaintenance");
                newRecord = oAComputerMaintenanceByCode.GetNewRecord();
            }
            else
            {
                oAComputerMaintenanceByCode = this.GetOAComputerMaintenanceByCode(this._ComputerMaintenanceCode);
                newRecord = oAComputerMaintenanceByCode.CurrentRow;
            }
            if (this._ComputerMaintenanceCode != null)
            {
                newRecord["ComputerMaintenanceCode"] = this._ComputerMaintenanceCode;
            }
            if (this._Unit != null)
            {
                newRecord["Unit"] = this._Unit;
            }
            if (this._ApplyUser != null)
            {
                newRecord["ApplyUser"] = this._ApplyUser;
            }
            if (this._MaintenanceItem != null)
            {
                newRecord["MaintenanceItem"] = this._MaintenanceItem;
            }
            if (this._MaintenanceContext != null)
            {
                newRecord["MaintenanceContext"] = this._MaintenanceContext;
            }
            if (this._ConkOutText != null)
            {
                newRecord["ConkOutText"] = this._ConkOutText;
            }
            if (flag)
            {
                oAComputerMaintenanceByCode.AddNewRecord(newRecord);
            }
            return oAComputerMaintenanceByCode;
        }

        private void DeleteOAComputerMaintenance(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAComputerMaintenance"))
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

        private EntityData GetAllOAComputerMaintenance()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAComputerMaintenance"))
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

        private EntityData GetOAComputerMaintenanceByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAComputerMaintenance"))
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

        private EntityData GetOAComputerMaintenanceByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "OAComputerMaintenance";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetOAComputerMaintenances()
        {
            return this._GetOAComputerMaintenances().CurrentTable;
        }

        private void InsertOAComputerMaintenance(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAComputerMaintenance"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void OAComputerMaintenanceAdd()
        {
            if (this._ComputerMaintenanceCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllOAComputerMaintenance(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "OAComputerMaintenance";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void OAComputerMaintenanceDelete()
        {
            if (this._dao == null)
            {
                this.DeleteOAComputerMaintenance(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "OAComputerMaintenance";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void OAComputerMaintenanceSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllOAComputerMaintenance(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "OAComputerMaintenance";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void OAComputerMaintenanceUpdate()
        {
            if (this._ComputerMaintenanceCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllOAComputerMaintenance(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "OAComputerMaintenance";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void SubmitAllOAComputerMaintenance(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAComputerMaintenance"))
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

        private void UpdateOAComputerMaintenance(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("OAComputerMaintenance"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string ApplyUser
        {
            get
            {
                if ((this._ApplyUser == null) && (this._ComputerMaintenanceCode != null))
                {
                    this._GetOAComputerMaintenanceByCode();
                }
                return this._ApplyUser;
            }
            set
            {
                this._ApplyUser = value;
            }
        }

        public string ComputerMaintenanceCode
        {
            get
            {
                return this._ComputerMaintenanceCode;
            }
            set
            {
                this._ComputerMaintenanceCode = value;
            }
        }

        public string ConkOutText
        {
            get
            {
                if ((this._ConkOutText == null) && (this._ComputerMaintenanceCode != null))
                {
                    this._GetOAComputerMaintenanceByCode();
                }
                return this._ConkOutText;
            }
            set
            {
                this._ConkOutText = value;
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

        public string MaintenanceContext
        {
            get
            {
                if ((this._MaintenanceContext == null) && (this._ComputerMaintenanceCode != null))
                {
                    this._GetOAComputerMaintenanceByCode();
                }
                return this._MaintenanceContext;
            }
            set
            {
                this._MaintenanceContext = value;
            }
        }

        public string MaintenanceItem
        {
            get
            {
                if ((this._MaintenanceItem == null) && (this._ComputerMaintenanceCode != null))
                {
                    this._GetOAComputerMaintenanceByCode();
                }
                return this._MaintenanceItem;
            }
            set
            {
                this._MaintenanceItem = value;
            }
        }

        public string Unit
        {
            get
            {
                if ((this._Unit == null) && (this._ComputerMaintenanceCode != null))
                {
                    this._GetOAComputerMaintenanceByCode();
                }
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }
    }
}

