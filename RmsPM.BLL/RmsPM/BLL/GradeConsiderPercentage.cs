namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class GradeConsiderPercentage
    {
        private string _ConsiderDiathesisCode = null;
        private string _ConsiderPercentageCode = null;
        private StandardEntityDAO _dao;
        private string _GradeMessageCode = null;
        private string _MainDefineCode = null;
        private decimal _Percentage;

        private void _GetGradeConsiderPercentageByCode()
        {
            try
            {
                EntityData gradeConsiderPercentageByCode = GetGradeConsiderPercentageByCode(this._ConsiderPercentageCode);
                this._ConsiderPercentageCode = gradeConsiderPercentageByCode.GetString("ConsiderPercentageCode");
                this._MainDefineCode = gradeConsiderPercentageByCode.GetString("MainDefineCode");
                this._GradeMessageCode = gradeConsiderPercentageByCode.GetString("GradeMessageCode");
                this._ConsiderDiathesisCode = gradeConsiderPercentageByCode.GetString("ConsiderDiathesisCode");
                this._Percentage = gradeConsiderPercentageByCode.GetDecimal("Percentage");
                gradeConsiderPercentageByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetGradeConsiderPercentages()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("GradeConsiderPercentage");
                GradeConsiderPercentageStrategyBuilder builder = new GradeConsiderPercentageStrategyBuilder();
                if (this._ConsiderPercentageCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderPercentageStrategyName.ConsiderPercentageCode, this._ConsiderPercentageCode));
                }
                if (this._MainDefineCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderPercentageStrategyName.MainDefineCode, this._MainDefineCode));
                }
                if (this._GradeMessageCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderPercentageStrategyName.GradeMessageCode, this._GradeMessageCode));
                }
                if (this._ConsiderDiathesisCode != null)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderPercentageStrategyName.ConsiderDiathesisCode, this._ConsiderDiathesisCode));
                }
                if (this._Percentage != 0M)
                {
                    builder.AddStrategy(new Strategy(GradeConsiderPercentageStrategyName.Percentage, this._Percentage.ToString()));
                }
                string sqlString = builder.BuildMainQueryString() + " order by ConsiderPercentageCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderPercentage"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderPercentage";
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
                EntityData gradeConsiderPercentageByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._ConsiderPercentageCode == "")
                {
                    flag = true;
                    gradeConsiderPercentageByCode = GetGradeConsiderPercentageByCode("", this.dao);
                    this._ConsiderPercentageCode = SystemManageDAO.GetNewSysCode("GradeConsiderPercentage");
                    newRecord = gradeConsiderPercentageByCode.GetNewRecord();
                }
                else
                {
                    gradeConsiderPercentageByCode = GetGradeConsiderPercentageByCode(this._ConsiderPercentageCode, this.dao);
                    if (gradeConsiderPercentageByCode.Tables[0].Rows.Count <= 0)
                    {
                        newRecord = gradeConsiderPercentageByCode.GetNewRecord();
                        flag = true;
                    }
                    else
                    {
                        newRecord = gradeConsiderPercentageByCode.CurrentRow;
                    }
                }
                if (this._ConsiderPercentageCode != null)
                {
                    newRecord["ConsiderPercentageCode"] = this._ConsiderPercentageCode;
                }
                if (this._MainDefineCode != null)
                {
                    newRecord["MainDefineCode"] = this._MainDefineCode;
                }
                if (this._GradeMessageCode != null)
                {
                    newRecord["GradeMessageCode"] = this._GradeMessageCode;
                }
                if (this._ConsiderDiathesisCode != null)
                {
                    newRecord["ConsiderDiathesisCode"] = this._ConsiderDiathesisCode;
                }
                newRecord["Percentage"] = this._Percentage;
                if (flag)
                {
                    gradeConsiderPercentageByCode.AddNewRecord(newRecord);
                }
                data2 = gradeConsiderPercentageByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void DeleteGradeConsiderPercentage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderPercentage"))
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

        public static EntityData GetAllGradeConsiderPercentage()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderPercentage"))
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

        public static EntityData GetGradeConsiderPercentageByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderPercentage"))
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

        public static EntityData GetGradeConsiderPercentageByCode(string code, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                EntityData gradeConsiderPercentageByCode;
                if (dao != null)
                {
                    dao.EntityName = "GradeConsiderPercentage";
                    gradeConsiderPercentageByCode = dao.SelectbyPrimaryKey(code);
                }
                else
                {
                    gradeConsiderPercentageByCode = GetGradeConsiderPercentageByCode(code);
                }
                data2 = gradeConsiderPercentageByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetGradeConsiderPercentages()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetGradeConsiderPercentages().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public DataTable GetLastConsiderPercentage(string GradeMessageCode, string MainDefineCode)
        {
            return this.GetLastConsiderPercentage("", GradeMessageCode, MainDefineCode);
        }

        public DataTable GetLastConsiderPercentage(string supplierCode, string GradeMessageCode, string MainDefineCode)
        {
            GradeConsiderPercentage percentage;
            DataTable table = new DataTable();
            if (GradeMessageCode != "")
            {
                percentage = new GradeConsiderPercentage();
                percentage.GradeMessageCode = "'" + GradeMessageCode + "'";
                percentage.MainDefineCode = MainDefineCode;
                percentage.dao = this.dao;
                return percentage.GetGradeConsiderPercentages();
            }
            GradeMessage message = new GradeMessage();
            message.dao = this.dao;
            message.SupplierCode = supplierCode;
            message.MainDefineCode = MainDefineCode;
            DataTable gradeMessages = new DataTable();
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
            percentage = new GradeConsiderPercentage();
            percentage.GradeMessageCode = text;
            percentage.MainDefineCode = MainDefineCode;
            percentage.dao = this.dao;
            return percentage.GetGradeConsiderPercentages();
        }

        public void GradeConsiderPercentageAdd()
        {
            if (this._dao == null)
            {
                SubmitAllGradeConsiderPercentage(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "GradeConsiderPercentage";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void GradeConsiderPercentageDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    DeleteGradeConsiderPercentage(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "GradeConsiderPercentage";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertGradeConsiderPercentage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderPercentage"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllGradeConsiderPercentage(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderPercentage"))
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

        public static void UpdateGradeConsiderPercentage(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GradeConsiderPercentage"))
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

        public string ConsiderPercentageCode
        {
            get
            {
                return this._ConsiderPercentageCode;
            }
            set
            {
                if (this._ConsiderPercentageCode != value)
                {
                    this._ConsiderPercentageCode = value;
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

