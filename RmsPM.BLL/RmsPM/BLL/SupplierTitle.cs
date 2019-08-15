namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class SupplierTitle
    {
        private string _BankAccount = null;
        private StandardEntityDAO _dao;
        private string _Remark = null;
        private string _SupplierCode = null;
        private string _SupplierTitleCode = null;
        private string _Title = null;

        private void _GetSupplierTitleByCode()
        {
            try
            {
                EntityData supplierTitleByCode = GetSupplierTitleByCode(this._SupplierTitleCode);
                this._SupplierTitleCode = supplierTitleByCode.GetString("SupplierTitleCode");
                this._Title = supplierTitleByCode.GetString("Title");
                this._SupplierCode = supplierTitleByCode.GetString("SupplierCode");
                this._BankAccount = supplierTitleByCode.GetString("BankAccount");
                this._Remark = supplierTitleByCode.GetString("Remark");
                supplierTitleByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetSupplierTitles()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SupplierTitle");
                SupplierTitleStrategyBuilder builder = new SupplierTitleStrategyBuilder();
                if (this._SupplierTitleCode != null)
                {
                    builder.AddStrategy(new Strategy(SupplierTitleStrategyName.SupplierTitleCode, this._SupplierTitleCode));
                }
                if (this._Title != null)
                {
                    builder.AddStrategy(new Strategy(SupplierTitleStrategyName.Title, this._Title));
                }
                if (this._SupplierCode != null)
                {
                    builder.AddStrategy(new Strategy(SupplierTitleStrategyName.SupplierCode, this._SupplierCode));
                }
                if (this._BankAccount != null)
                {
                    builder.AddStrategy(new Strategy(SupplierTitleStrategyName.BankAccount, this._BankAccount));
                }
                if (this._Remark != null)
                {
                    builder.AddStrategy(new Strategy(SupplierTitleStrategyName.Remark, this._Remark));
                }
                string sqlString = builder.BuildMainQueryString() + " order by SupplierTitleCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierTitle"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "SupplierTitle";
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
                EntityData supplierTitleByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._SupplierTitleCode == "")
                {
                    flag = true;
                    supplierTitleByCode = GetSupplierTitleByCode("");
                    this._SupplierTitleCode = SystemManageDAO.GetNewSysCode("SupplierTitle");
                    newRecord = supplierTitleByCode.GetNewRecord();
                }
                else
                {
                    supplierTitleByCode = GetSupplierTitleByCode(this._SupplierTitleCode);
                    if (supplierTitleByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = supplierTitleByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = supplierTitleByCode.CurrentRow;
                    }
                }
                if (this._SupplierTitleCode != null)
                {
                    newRecord["SupplierTitleCode"] = this._SupplierTitleCode;
                }
                if (this._Title != null)
                {
                    newRecord["Title"] = this._Title;
                }
                if (this._SupplierCode != null)
                {
                    newRecord["SupplierCode"] = this._SupplierCode;
                }
                if (this._BankAccount != null)
                {
                    newRecord["BankAccount"] = this._BankAccount;
                }
                if (this._Remark != null)
                {
                    newRecord["Remark"] = this._Remark;
                }
                if (flag)
                {
                    supplierTitleByCode.AddNewRecord(newRecord);
                }
                data2 = supplierTitleByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteSupplierTitle(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierTitle"))
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

        public static EntityData GetAllSupplierTitle()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierTitle"))
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

        public static EntityData GetSupplierTitleByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierTitle"))
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

        public static EntityData GetSupplierTitleByCode(string code, StandardEntityDAO dao)
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

        public DataTable GetSupplierTitles()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetSupplierTitles().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void InsertSupplierTitle(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierTitle"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllSupplierTitle(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierTitle"))
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

        public void SupplierTitleAdd()
        {
            if (this._dao == null)
            {
                SubmitAllSupplierTitle(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "SupplierTitle";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void SupplierTitleDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteSupplierTitle(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "SupplierTitle";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SupplierTitleUpdate()
        {
            if (this._SupplierTitleCode != null)
            {
                if (this._dao == null)
                {
                    SubmitAllSupplierTitle(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "SupplierTitle";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public static void UpdateSupplierTitle(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SupplierTitle"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BankAccount
        {
            get
            {
                if ((this._BankAccount == null) && (this._SupplierTitleCode != null))
                {
                    this._GetSupplierTitleByCode();
                }
                return this._BankAccount;
            }
            set
            {
                if (this._BankAccount != value)
                {
                    this._BankAccount = value;
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

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._SupplierTitleCode != null))
                {
                    this._GetSupplierTitleByCode();
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

        public string SupplierCode
        {
            get
            {
                if ((this._SupplierCode == null) && (this._SupplierTitleCode != null))
                {
                    this._GetSupplierTitleByCode();
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

        public string SupplierTitleCode
        {
            get
            {
                return this._SupplierTitleCode;
            }
            set
            {
                if (this._SupplierTitleCode != value)
                {
                    this._SupplierTitleCode = value;
                }
            }
        }

        public string Title
        {
            get
            {
                if ((this._Title == null) && (this._SupplierTitleCode != null))
                {
                    this._GetSupplierTitleByCode();
                }
                return this._Title;
            }
            set
            {
                if (this._Title != value)
                {
                    this._Title = value;
                }
            }
        }
    }
}

