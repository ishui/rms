namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Text.RegularExpressions;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class TC_OA_BiddingContract
    {
        private string _ApplyDate = null;
        private string _CommendSupplier = null;
        private string _CompleteDate = null;
        private string _ContractCode = null;
        private string _ContractContent = null;
        private string _ContractDate = null;
        private string _ContractID = null;
        private string _ContractType = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _Opinion = null;
        private string _ProjectCode = null;
        private string _State = null;
        private string _SupplierCode = null;
        private string _TC_OA_BiddingContractCode = null;
        private string _WorkName = null;

        private void _GetTC_OA_BiddingContractByCode()
        {
            EntityData data = this.GetTC_OA_BiddingContractByCode(this._TC_OA_BiddingContractCode);
            this._TC_OA_BiddingContractCode = data.GetString("TC_OA_BiddingContractCode");
            this._ProjectCode = data.GetString("ProjectCode");
            this._WorkName = data.GetString("WorkName");
            this._ApplyDate = data.GetDateTime("ApplyDate").ToString();
            this._CompleteDate = data.GetDateTime("CompleteDate").ToString();
            this._CommendSupplier = data.GetString("CommendSupplier");
            this._Opinion = data.GetString("Opinion").ToString();
            this._ContractDate = data.GetDateTime("ContractDate").ToString().ToString();
            this._ContractType = data.GetString("ContractType").ToString();
            this._SupplierCode = data.GetString("SupplierCode").ToString();
            this._ContractID = data.GetString("ContractID").ToString();
            this._ContractContent = data.GetString("ContractContent").ToString();
            this._ContractCode = data.GetString("ContractCode").ToString();
            this._Flag = data.GetString("Flag").ToString();
            this._State = data.GetInt("State").ToString();
            data.Dispose();
        }

        private EntityData _GetTC_OA_BiddingContracts()
        {
            EntityData entitydata = new EntityData("TC_OA_BiddingContract");
            TC_OA_BiddingContractStrategyBuilder builder = new TC_OA_BiddingContractStrategyBuilder();
            if (this._TC_OA_BiddingContractCode != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.TC_OA_BiddingContractCode, this._TC_OA_BiddingContractCode));
            }
            if (this._ProjectCode != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.ProjectCode, this._ProjectCode));
            }
            if (this._WorkName != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.WorkName, this._WorkName));
            }
            if (this._ApplyDate != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.ApplyDate, this._ApplyDate));
            }
            if (this._CompleteDate != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.CompleteDate, this._CompleteDate));
            }
            if (this._Opinion != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.Opinion, this._Opinion));
            }
            if (this._ContractDate != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.ContractDate, this._ContractDate));
            }
            if (this._ContractType != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.ContractType, this._ContractType));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._ContractID != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.ContractID, this._ContractID));
            }
            if (this._ContractContent != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.ContractContent, this._ContractContent));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.Flag, this._Flag));
            }
            if (this._ContractCode != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.ContractCode, this._ContractCode));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(TC_OA_BiddingContractStrategyName.State, this._State));
            }
            string sqlString = builder.BuildMainQueryString() + " order by TC_OA_BiddingContractCode Desc";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TC_OA_BiddingContract"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "TC_OA_BiddingContract";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        private EntityData BuildData()
        {
            EntityData data;
            DataRow newRecord;
            bool flag = false;
            if (this._TC_OA_BiddingContractCode == "")
            {
                flag = true;
                data = this.GetTC_OA_BiddingContractByCode("");
                this._TC_OA_BiddingContractCode = SystemManageDAO.GetNewSysCode("TC_OA_BiddingContract");
                newRecord = data.GetNewRecord();
            }
            else
            {
                data = this.GetTC_OA_BiddingContractByCode(this._TC_OA_BiddingContractCode);
                newRecord = data.CurrentRow;
            }
            if (this._TC_OA_BiddingContractCode != null)
            {
                newRecord["TC_OA_BiddingContractCode"] = this._TC_OA_BiddingContractCode;
            }
            if (this._ProjectCode != null)
            {
                newRecord["ProjectCode"] = this._ProjectCode;
            }
            if (this._WorkName != null)
            {
                newRecord["WorkName"] = this._WorkName;
            }
            if (this._ApplyDate != null)
            {
                newRecord["ApplyDate"] = this._ApplyDate;
            }
            if (this._CompleteDate != null)
            {
                newRecord["CompleteDate"] = this._CompleteDate;
            }
            if (this._CommendSupplier != null)
            {
                newRecord["CommendSupplier"] = this._CommendSupplier;
            }
            if (this._ContractDate != null)
            {
                newRecord["ContractDate"] = this._ContractDate;
            }
            if (this._Opinion != null)
            {
                newRecord["Opinion"] = this._Opinion;
            }
            if (this._ContractType != null)
            {
                newRecord["ContractType"] = this._ContractType;
            }
            if (this._SupplierCode != null)
            {
                newRecord["SupplierCode"] = this._SupplierCode;
            }
            if (this._ContractID != null)
            {
                newRecord["ContractID"] = this._ContractID;
            }
            if (this._ContractContent != null)
            {
                newRecord["ContractContent"] = this._ContractContent;
            }
            if (this._ContractCode != null)
            {
                newRecord["ContractCode"] = this._ContractCode;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (flag)
            {
                data.AddNewRecord(newRecord);
            }
            return data;
        }

        public static void DeleteStandard_TC_OA_BiddingContract(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("TC_OA_BiddingContract"))
                {
                    ydao.SubmitEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void DeleteTC_OA_BiddingContract(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TC_OA_BiddingContract"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "TC_OA_BiddingContract";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllTC_OA_BiddingContract()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TC_OA_BiddingContract"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "TC_OA_BiddingContract";
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

        public static DataTable GetConvertString(string OperationStr)
        {
            string pattern = ";@;";
            string[] textArray = Regex.Split(OperationStr, pattern, RegexOptions.IgnoreCase);
            DataTable table = new DataTable("Suppliers");
            table.Columns.Add("SupplierName", Type.GetType("System.String"));
            foreach (string text2 in textArray)
            {
                if (text2 != "")
                {
                    DataRow row = table.NewRow();
                    row["SupplierName"] = text2;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public EntityData GetTC_OA_BiddingContractByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TC_OA_BiddingContract"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "TC_OA_BiddingContract";
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

        public DataTable GetTC_OA_BiddingContracts()
        {
            return this._GetTC_OA_BiddingContracts().CurrentTable;
        }

        public static string GetTC_OA_BiddingContractStatusName(string state)
        {
            switch (state)
            {
                case "0":
                    return "已审";

                case "1":
                    return "申请";

                case "2":
                    return "已结";

                case "3":
                    return "作废";

                case "4":
                    return "变更";

                case "6":
                    return "历史";

                case "7":
                    return "审核中";

                case "8":
                    return "已评审";

                case "9":
                    return "评审中";
            }
            return "";
        }

        private void InsertTC_OA_BiddingContract(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TC_OA_BiddingContract"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "TC_OA_BiddingContract";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SubmitAllTC_OA_BiddingContract(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TC_OA_BiddingContract"))
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
                    this.dao.EntityName = "TC_OA_BiddingContract";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public void TC_OA_BiddingContractAdd()
        {
            if (this._dao == null)
            {
                this.SubmitAllTC_OA_BiddingContract(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "TC_OA_BiddingContract";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void TC_OA_BiddingContractDelete()
        {
            if (this._dao == null)
            {
                this.DeleteTC_OA_BiddingContract(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "TC_OA_BiddingContract";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public static bool TC_OA_BiddingContractStatusChange(string TC_OA_BiddingContractCode, int gm_iStatus)
        {
            return TC_OA_BiddingContractStatusChange(TC_OA_BiddingContractCode, gm_iStatus, null, true);
        }

        public static bool TC_OA_BiddingContractStatusChange(EntityData gm_Entity, int gm_iStatus)
        {
            return TC_OA_BiddingContractStatusChange(gm_Entity, "", gm_iStatus, null, false);
        }

        public static bool TC_OA_BiddingContractStatusChange(string TC_OA_BiddingContractCode, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return TC_OA_BiddingContractStatusChange(TC_OA_BiddingContractCode, gm_iStatus, gm_iOriginalStatus, true);
        }

        public static bool TC_OA_BiddingContractStatusChange(EntityData gm_Entity, int gm_iStatus, bool gm_bSubmitData)
        {
            return TC_OA_BiddingContractStatusChange(gm_Entity, "", gm_iStatus, null, gm_bSubmitData);
        }

        public static bool TC_OA_BiddingContractStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus)
        {
            return TC_OA_BiddingContractStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, false);
        }

        public static bool TC_OA_BiddingContractStatusChange(string TC_OA_BiddingContractCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            TC_OA_BiddingContract contract = new TC_OA_BiddingContract();
            return TC_OA_BiddingContractStatusChange(contract.GetTC_OA_BiddingContractByCode(TC_OA_BiddingContractCode), TC_OA_BiddingContractCode, gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool TC_OA_BiddingContractStatusChange(EntityData gm_Entity, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            return TC_OA_BiddingContractStatusChange(gm_Entity, "", gm_iStatus, gm_iOriginalStatus, gm_bSubmitData);
        }

        public static bool TC_OA_BiddingContractStatusChange(EntityData gm_Entity, string TC_OA_BiddingContractCode, int gm_iStatus, int? gm_iOriginalStatus, bool gm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                gm_Entity.SetCurrentTable("TC_OA_BiddingContract");
                if (TC_OA_BiddingContractCode.Trim() == "")
                {
                    if (gm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("TC_OA_BiddingContractCode='{0}'", TC_OA_BiddingContractCode.Trim());
                    if (gm_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in gm_Entity.CurrentTable.Select(filterExpression))
                {
                    if (gm_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["State"] != DBNull.Value) && ((((int) row["State"]) == (nullable = gm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["State"] = gm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["State"] = gm_iStatus;
                    }
                }
                if (flag && gm_bSubmitData)
                {
                    new TC_OA_BiddingContract().SubmitAllTC_OA_BiddingContract(gm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public void TC_OA_BiddingContractSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllTC_OA_BiddingContract(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "TC_OA_BiddingContract";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void TC_OA_BiddingContractUpdate()
        {
            if ((this._TC_OA_BiddingContractCode != null) || (this._TC_OA_BiddingContractCode != ""))
            {
                if (this._dao == null)
                {
                    this.SubmitAllTC_OA_BiddingContract(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "TC_OA_BiddingContract";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private void UpdateTC_OA_BiddingContract(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("TC_OA_BiddingContract"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "TC_OA_BiddingContract";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string ApplyDate
        {
            get
            {
                if (this._ApplyDate == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._ApplyDate;
            }
            set
            {
                this._ApplyDate = value;
            }
        }

        public string CommendSupplier
        {
            get
            {
                if (this._CommendSupplier == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._CommendSupplier;
            }
            set
            {
                this._CommendSupplier = value;
            }
        }

        public string CompleteDate
        {
            get
            {
                if (this._CompleteDate == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._CompleteDate;
            }
            set
            {
                this._CompleteDate = value;
            }
        }

        public string ContractCode
        {
            get
            {
                if (this._ContractCode == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._ContractCode;
            }
            set
            {
                this._ContractCode = value;
            }
        }

        public string ContractContent
        {
            get
            {
                if (this._ContractContent == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._ContractContent;
            }
            set
            {
                this._ContractContent = value;
            }
        }

        public string ContractDate
        {
            get
            {
                if (this._ContractDate == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._ContractDate;
            }
            set
            {
                this._ContractDate = value;
            }
        }

        public string ContractID
        {
            get
            {
                if (this._ContractID == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._ContractID;
            }
            set
            {
                this._ContractID = value;
            }
        }

        public string ContractType
        {
            get
            {
                if (this._ContractType == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._ContractType;
            }
            set
            {
                this._ContractType = value;
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

        public string Flag
        {
            get
            {
                if (this._Flag == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string Opinion
        {
            get
            {
                if (this._Opinion == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._Opinion;
            }
            set
            {
                this._Opinion = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                if (this._ProjectCode == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string State
        {
            get
            {
                if (this._State == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string SupplierCode
        {
            get
            {
                if (this._SupplierCode == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }

        public string TC_OA_BiddingContractCode
        {
            get
            {
                return this._TC_OA_BiddingContractCode;
            }
            set
            {
                this._TC_OA_BiddingContractCode = value;
            }
        }

        public string WorkName
        {
            get
            {
                if (this._WorkName == null)
                {
                    this._GetTC_OA_BiddingContractByCode();
                }
                return this._WorkName;
            }
            set
            {
                this._WorkName = value;
            }
        }
    }
}

