namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class SupplierLinkman
    {
        private string _AreaName = null;
        private string _ContractPerson = null;
        private StandardEntityDAO _dao;
        private string _EMail = null;
        private string _Fax = null;
        private string _Flag = null;
        private string _Mobile = null;
        private string _OfficePhone = null;
        private string _PostCode = null;
        private string _ProjectName = null;
        private string _Remark = null;
        private string _State = null;
        private string _SupplierCode = null;
        private string _SupplierLinkmanCode = null;

        private void _GetSupplierLinkmanByCode()
        {
            try
            {
                EntityData supplierLinkmanByCode = GetSupplierLinkmanByCode(this._SupplierLinkmanCode);
                this._SupplierLinkmanCode = supplierLinkmanByCode.GetString("SupplierLinkmanCode");
                this._ContractPerson = supplierLinkmanByCode.GetString("ContractPerson");
                this._SupplierCode = supplierLinkmanByCode.GetString("SupplierCode");
                this._OfficePhone = supplierLinkmanByCode.GetString("OfficePhone");
                this._PostCode = supplierLinkmanByCode.GetString("PostCode");
                this._Mobile = supplierLinkmanByCode.GetString("Mobile");
                this._Fax = supplierLinkmanByCode.GetString("Fax");
                this._EMail = supplierLinkmanByCode.GetString("EMail");
                this._AreaName = supplierLinkmanByCode.GetString("AreaName");
                this._ProjectName = supplierLinkmanByCode.GetString("ProjectName");
                this._Remark = supplierLinkmanByCode.GetString("Remark");
                this._State = supplierLinkmanByCode.GetString("state");
                this._Flag = supplierLinkmanByCode.GetString("flag");
                supplierLinkmanByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetSupplierLinkmans()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SupplierLinkman");
                SupplierLinkmanStrategyBuilder builder = new SupplierLinkmanStrategyBuilder();
                if (this._SupplierLinkmanCode != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.SupplierLinkmanCode, this._SupplierLinkmanCode));
                }
                if (this._ContractPerson != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.ContractPerson, this._ContractPerson));
                }
                if (this._SupplierCode != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.SupplierCode, this._SupplierCode));
                }
                if (this._OfficePhone != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.OfficePhone, this._OfficePhone));
                }
                if (this._PostCode != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.PostCode, this._PostCode));
                }
                if (this._Mobile != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.Mobile, this._Mobile));
                }
                if (this._Fax != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.Fax, this._Fax));
                }
                if (this._EMail != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.EMail, this._EMail));
                }
                if (this._AreaName != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.AreaName, this._AreaName));
                }
                if (this._ProjectName != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.ProjectName, this._ProjectName));
                }
                if (this._State != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.State, this._State));
                }
                if (this._Flag != null)
                {
                    builder.AddStrategy(new Strategy(SupplierLinkmanStrategyName.Flag, this._Flag));
                }
                string sqlString = builder.BuildMainQueryString() + " order by SupplierLinkmanCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierLinkman"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "SupplierLinkman";
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
                EntityData supplierLinkmanByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._SupplierLinkmanCode == "")
                {
                    flag = true;
                    supplierLinkmanByCode = GetSupplierLinkmanByCode("");
                    this._SupplierLinkmanCode = SystemManageDAO.GetNewSysCode("SupplierLinkman");
                    newRecord = supplierLinkmanByCode.GetNewRecord();
                }
                else
                {
                    supplierLinkmanByCode = GetSupplierLinkmanByCode(this._SupplierLinkmanCode);
                    if (supplierLinkmanByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = supplierLinkmanByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = supplierLinkmanByCode.CurrentRow;
                    }
                }
                if (this._SupplierLinkmanCode != null)
                {
                    newRecord["SupplierLinkmanCode"] = this._SupplierLinkmanCode;
                }
                if (this._ContractPerson != null)
                {
                    newRecord["ContractPerson"] = this._ContractPerson;
                }
                if (this._SupplierCode != null)
                {
                    newRecord["SupplierCode"] = this._SupplierCode;
                }
                if (this._OfficePhone != null)
                {
                    newRecord["OfficePhone"] = this._OfficePhone;
                }
                if (this._PostCode != null)
                {
                    newRecord["PostCode"] = this._PostCode;
                }
                if (this._Mobile != null)
                {
                    newRecord["Mobile"] = this._Mobile;
                }
                if (this._Fax != null)
                {
                    newRecord["Fax"] = this._Fax;
                }
                if (this._EMail != null)
                {
                    newRecord["EMail"] = this._EMail;
                }
                if (this._AreaName != null)
                {
                    newRecord["AreaName"] = this._AreaName;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
                }
                if (this._ProjectName != null)
                {
                    newRecord["ProjectName"] = this._ProjectName;
                }
                if (this._State != null)
                {
                    newRecord["state"] = this._State;
                }
                if (this._Flag != null)
                {
                    newRecord["flag"] = this._Flag;
                }
                if (flag)
                {
                    supplierLinkmanByCode.AddNewRecord(newRecord);
                }
                data2 = supplierLinkmanByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteSupplierLinkman(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierLinkman"))
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

        public static EntityData GetAllSupplierLinkman()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierLinkman"))
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

        public static EntityData GetSupplierLinkmanByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierLinkman"))
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

        public static EntityData GetSupplierLinkmanByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetSupplierLinkmans()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetSupplierLinkmans().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertSupplierLinkman(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierLinkman"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllSupplierLinkman(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierLinkman"))
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

        public void SupplierLinkmanAdd()
        {
            if (this._dao == null)
            {
                SubmitAllSupplierLinkman(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "SupplierLinkman";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void SupplierLinkmanDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteSupplierLinkman(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "SupplierLinkman";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SupplierLinkmanUpdate()
        {
            if (this._SupplierLinkmanCode != null)
            {
                if (this._dao == null)
                {
                    SubmitAllSupplierLinkman(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "SupplierLinkman";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public static void UpdateSupplierLinkman(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierLinkman"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string AreaName
        {
            get
            {
                if ((this._AreaName == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._AreaName;
            }
            set
            {
                if (this._AreaName != value)
                {
                    this._AreaName = value;
                }
            }
        }

        public string ContractPerson
        {
            get
            {
                if ((this._ContractPerson == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._ContractPerson;
            }
            set
            {
                if (this._ContractPerson != value)
                {
                    this._ContractPerson = value;
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

        public string EMail
        {
            get
            {
                if ((this._EMail == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._EMail;
            }
            set
            {
                if (this._EMail != value)
                {
                    this._EMail = value;
                }
            }
        }

        public string Fax
        {
            get
            {
                if ((this._Fax == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._Fax;
            }
            set
            {
                if (this._Fax != value)
                {
                    this._Fax = value;
                }
            }
        }

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
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

        public string Mobile
        {
            get
            {
                if ((this._Mobile == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._Mobile;
            }
            set
            {
                if (this._Mobile != value)
                {
                    this._Mobile = value;
                }
            }
        }

        public string OfficePhone
        {
            get
            {
                if ((this._OfficePhone == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._OfficePhone;
            }
            set
            {
                if (this._OfficePhone != value)
                {
                    this._OfficePhone = value;
                }
            }
        }

        public string PostCode
        {
            get
            {
                if ((this._PostCode == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._PostCode;
            }
            set
            {
                if (this._PostCode != value)
                {
                    this._PostCode = value;
                }
            }
        }

        public string ProjectName
        {
            get
            {
                if ((this._ProjectName == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._ProjectName;
            }
            set
            {
                if (this._ProjectName != value)
                {
                    this._ProjectName = value;
                }
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._Remark;
            }
            set
            {
                if (this._Remark != value)
                {
                    this._Remark = value;
                }
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._State;
            }
            set
            {
                if (this._State != value)
                {
                    this._State = value;
                }
            }
        }

        public string SupplierCode
        {
            get
            {
                if ((this._SupplierCode == null) && (this._SupplierLinkmanCode != null))
                {
                    this._GetSupplierLinkmanByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                if (this._SupplierCode != value)
                {
                    this._SupplierCode = value;
                }
            }
        }

        public string SupplierLinkmanCode
        {
            get
            {
                return this._SupplierLinkmanCode;
            }
            set
            {
                if (this._SupplierLinkmanCode != value)
                {
                    this._SupplierLinkmanCode = value;
                }
            }
        }
    }
}

