namespace RmsPM.BLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingEmit
    {
        private int _AllowAfterClose;
        private string _BiddingCode = null;
        private string _BiddingEmitCode = null;
        private string _BiddingEmitResult = null;
        private string _BiddingEmitUsers = null;
        private string _BiddingModel = null;
        private string _CreatDate = null;
        private string _CreatUser = null;
        private StandardEntityDAO _dao;
        private string _EmitDate = null;
        private string _EmitNumber = null;
        private string _EndDate = null;
        private int _IsWSZTB;
        private string _PrejudicationDate = null;
        private int _State;
        private string _SupplierSelectModel = null;
        private string _TotalRemark = null;
        private string _TotalRemark2 = null;
        private string _Visualize = null;

        private void _GetBiddingEmitByCode()
        {
            try
            {
                EntityData biddingEmitByCode = this.GetBiddingEmitByCode(this._BiddingEmitCode);
                this._BiddingEmitCode = biddingEmitByCode.GetString("BiddingEmitCode");
                this._BiddingCode = biddingEmitByCode.GetString("BiddingCode");
                this._EmitNumber = biddingEmitByCode.GetString("EmitNumber").ToString();
                this._EmitDate = biddingEmitByCode.GetDateTime("EmitDate", "yyyy-MM-dd HH:mm");
                this._EndDate = biddingEmitByCode.GetDateTime("EndDate", "yyyy-MM-dd HH:mm");
                this._PrejudicationDate = biddingEmitByCode.GetDateTime("PrejudicationDate", "yyyy-MM-dd HH:mm");
                this._CreatUser = biddingEmitByCode.GetString("CreatUser");
                this._CreatDate = biddingEmitByCode.GetDateTimeOnlyDate("CreatDate");
                this._TotalRemark = biddingEmitByCode.GetString("TotalRemark");
                this._TotalRemark2 = biddingEmitByCode.GetString("TotalRemark2");
                this._IsWSZTB = biddingEmitByCode.GetInt("IsWSZTB");
                this._AllowAfterClose = biddingEmitByCode.GetInt("AllowAfterClose");
                this._State = biddingEmitByCode.GetInt("State");
                this._SupplierSelectModel = biddingEmitByCode.GetString("SupplierSelectModel");
                this._Visualize = biddingEmitByCode.GetString("Visualize");
                this._BiddingModel = biddingEmitByCode.GetString("BiddingModel");
                this._BiddingEmitUsers = biddingEmitByCode.GetString("BiddingEmitUsers");
                this._BiddingEmitResult = biddingEmitByCode.GetString("BiddingEmitResult");
                biddingEmitByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData _GetBiddingEmits()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BiddingEmit");
                BiddingEmitStrategyBuilder builder = new BiddingEmitStrategyBuilder();
                if (this._BiddingEmitCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.BiddingEmitCode, this._BiddingEmitCode));
                }
                if (this._BiddingCode != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.BiddingCode, this._BiddingCode));
                }
                if (this._EmitNumber != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.EmitNumber, this._EmitNumber));
                }
                if (this._EmitDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.EmitDate, this._EmitDate));
                }
                if (this._EndDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.EndDate, this._EndDate));
                }
                if (this._PrejudicationDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.PrejudicationDate, this._PrejudicationDate));
                }
                if (this._CreatUser != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.CreatUser, this._CreatUser));
                }
                if (this._CreatDate != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.CreatDate, this._CreatDate));
                }
                if (this._TotalRemark != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.TotalRemark, this._TotalRemark));
                }
                if (this._TotalRemark != null)
                {
                    builder.AddStrategy(new Strategy(BiddingEmitStrategyName.TotalRemark2, this._TotalRemark2));
                }
                string sqlString = builder.BuildMainQueryString() + " order by BiddingEmitCode desc";
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingEmit"))
                    {
                        ydao.FillEntity(sqlString, entitydata);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingEmit";
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

        public void BiddingEmitAdd()
        {
            try
            {
                if ((this._BiddingEmitCode == null) || (this._BiddingEmitCode == ""))
                {
                    if (this._dao == null)
                    {
                        this.SubmitAllBiddingEmit(this.BuildData());
                    }
                    else
                    {
                        this.dao.EntityName = "BiddingEmit";
                        this.dao.SubmitEntity(this.BuildData());
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingEmitDelete()
        {
            try
            {
                if (this._dao == null)
                {
                    this.DeleteBiddingEmit(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingEmit";
                    this.dao.DeleteEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingEmitDeleteByCode()
        {
            try
            {
                if (this._dao == null)
                {
                    this.dao.DeleteEntity(this.GetBiddingEmitByCode(this.BiddingEmitCode));
                }
                else
                {
                    this.dao.EntityName = "BiddingEmit";
                    this.dao.DeleteEntity(this.GetBiddingEmitByCode(this.dao, this.BiddingEmitCode));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingEmitSubmit()
        {
            try
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingEmit(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingEmit";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingEmitUpdate()
        {
            try
            {
                if (this._BiddingEmitCode != null)
                {
                    if (this._dao == null)
                    {
                        this.SubmitAllBiddingEmit(this.BuildData());
                    }
                    else
                    {
                        this.dao.EntityName = "BiddingEmit";
                        this.dao.SubmitEntity(this.BuildData());
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData BuildData()
        {
            EntityData data2;
            try
            {
                EntityData biddingEmitByCode;
                DataRow newRecord;
                bool flag = false;
                if (this._BiddingEmitCode == "")
                {
                    flag = true;
                    biddingEmitByCode = this.GetBiddingEmitByCode("");
                    this._BiddingEmitCode = SystemManageDAO.GetNewSysCode("BiddingEmit");
                    newRecord = biddingEmitByCode.GetNewRecord();
                }
                else
                {
                    biddingEmitByCode = this.GetBiddingEmitByCode(this._BiddingEmitCode);
                    newRecord = biddingEmitByCode.CurrentRow;
                }
                if (this._BiddingEmitCode != null)
                {
                    newRecord["BiddingEmitCode"] = this._BiddingEmitCode;
                }
                if (this._BiddingCode != null)
                {
                    newRecord["BiddingCode"] = this._BiddingCode;
                }
                if (this._EmitNumber != null)
                {
                    newRecord["EmitNumber"] = this._EmitNumber;
                }
                if (this._EmitDate != null)
                {
                    newRecord["EmitDate"] = this._EmitDate;
                }
                if (this._EndDate != null)
                {
                    newRecord["EndDate"] = this._EndDate;
                }
                if (this._PrejudicationDate != null)
                {
                    newRecord["PrejudicationDate"] = this._PrejudicationDate;
                }
                if (this._CreatUser != null)
                {
                    newRecord["CreatUser"] = this._CreatUser;
                }
                if (this._CreatDate != null)
                {
                    newRecord["CreatDate"] = this._CreatDate;
                }
                if (this._TotalRemark != null)
                {
                    newRecord["TotalRemark"] = this._TotalRemark;
                }
                if (this._TotalRemark2 != null)
                {
                    newRecord["TotalRemark2"] = this._TotalRemark2;
                }
                newRecord["IsWSZTB"] = this._IsWSZTB;
                newRecord["AllowAfterClose"] = this._AllowAfterClose;
                newRecord["State"] = this._State;
                if (this._SupplierSelectModel != null)
                {
                    newRecord["SupplierSelectModel"] = this._SupplierSelectModel;
                }
                if (this._Visualize != null)
                {
                    newRecord["Visualize"] = this._Visualize;
                }
                if (this._BiddingModel != null)
                {
                    newRecord["BiddingModel"] = this._BiddingModel;
                }
                if (this._BiddingEmitUsers != null)
                {
                    newRecord["BiddingEmitUsers"] = this._BiddingEmitUsers;
                }
                if (this._BiddingEmitResult != null)
                {
                    newRecord["BiddingEmitResult"] = this._BiddingEmitResult;
                }
                if (flag)
                {
                    biddingEmitByCode.AddNewRecord(newRecord);
                }
                data2 = biddingEmitByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private void DeleteBiddingEmit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingEmit"))
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

        private EntityData GetAllBiddingEmit()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingEmit"))
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

        private EntityData GetBiddingEmitByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingEmit"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingEmit";
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

        public EntityData GetBiddingEmitByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "BiddingEmit";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public EntityData GetBiddingEmitByEmitToCode(StandardEntityDAO dao, string EmitToCode)
        {
            EntityData biddingEmitByCode;
            try
            {
                QueryAgent agent = new QueryAgent();
                string queryString = string.Format("select BiddingEmitCode from BiddingEmitTo where BiddingEmitToCode={0}", EmitToCode);
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                biddingEmitByCode = this.GetBiddingEmitByCode(dao, table.Rows[0]["BiddingEmitCode"].ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return biddingEmitByCode;
        }

        public EntityData GetBiddingEmitEntitys()
        {
            EntityData data;
            try
            {
                data = this._GetBiddingEmits();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data;
        }

        public DataTable GetBiddingEmits()
        {
            DataTable currentTable;
            try
            {
                currentTable = this._GetBiddingEmits().CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public string GetFactMoneyByBiddingCode(string code)
        {
            string text3;
            try
            {
                string lastTimeEmitCode = this.GetLastTimeEmitCode(code);
                string queryString = "select [Money] from BiddingReturn where Flag='1' and BiddingEmitCode='" + lastTimeEmitCode + "'";
                QueryAgent agent = new QueryAgent();
                DataRow row = agent.ExecSqlForDataSet(queryString).Tables[0].Rows[0];
                text3 = row[0].ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public string GetLastTimeEmitCode(string Code)
        {
            string text2;
            try
            {
                this._CreatUser = "1";
                string text = "";
                this._BiddingCode = Code;
                try
                {
                    DataTable biddingEmits = this.GetBiddingEmits();
                    if (biddingEmits.Rows.Count <= 0)
                    {
                        throw new Exception("");
                    }
                    DataView view = new DataView(biddingEmits);
                    view.Sort = "BiddingEmitCode desc";
                    text2 = view.Table.Rows[0]["BiddingEmitCode"].ToString();
                }
                catch
                {
                    this._CreatUser = "";
                    EntityData data = this._GetBiddingEmits();
                    data.CurrentTable.Select("", "BiddingEmitCode desc");
                    if (data.HasRecord())
                    {
                        text = data.CurrentTable.Rows[0]["BiddingEmitCode"].ToString();
                    }
                    text2 = text;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public string GetPriceAtAuditing(string biddingCode, string supplierCode, string biddingEmits, string biddingDtlCode, ref string State)
        {
            string text3;
            try
            {
                this._CreatUser = "1";
                this._BiddingCode = biddingCode;
                DataTable table = this.GetBiddingEmits();
                int count = table.Rows.Count;
                BiddingReturn return2 = new BiddingReturn();
                DataTable table2 = new DataTable();
                string text = "-1";
                if (count > 0)
                {
                    int num2 = 1;
                    DataView view = new DataView(table);
                    view.Sort = "BiddingEmitCode desc";
                    if (Convert.ToInt32(biddingEmits) > Convert.ToInt32(view.Table.Rows[0]["BiddingEmitCode"]))
                    {
                        return text;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        return2.BiddingEmitCode = view.Table.Rows[i]["BiddingEmitCode"].ToString();
                        return2.SupplierCode = supplierCode;
                        return2.BiddingDtlCode = biddingDtlCode;
                        DataTable biddingReturns = return2.GetBiddingReturns();
                        int num4 = biddingReturns.Rows.Count;
                        if (num4 >= 0)
                        {
                            if (num2 == 1)
                            {
                                table2 = biddingReturns.Clone();
                                num2 = 2;
                            }
                            for (int j = 0; j < num4; j++)
                            {
                                DataRow row = table2.NewRow();
                                row.ItemArray = biddingReturns.Rows[j].ItemArray;
                                table2.Rows.Add(row);
                            }
                        }
                    }
                    if (table2.Rows.Count > 0)
                    {
                        string text2 = table2.Rows[0]["Money"].ToString();
                        State = table2.Rows[0]["State"].ToString();
                        if (text2 != "")
                        {
                            text = text2;
                        }
                    }
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        private void InsertBiddingEmit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingEmit"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void IsLowOfPriceByBiddingCode(string BiddingReturnCode)
        {
            try
            {
                string queryString = "Update BiddingReturn set Flag='1' where BiddingReturnCode='" + BiddingReturnCode + "'";
                new QueryAgent().ExecuteSql(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SubmitAllBiddingEmit(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingEmit"))
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
                    this.dao.EntityName = "BiddingEmit";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateBiddingEmit(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingEmit"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateRemarkByBiddingEmitCode(string Remark, string code)
        {
            string connectionString = ConfigurationSettings.AppSettings["DBConnString"];
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Update BiddingEmit set TotalRemark2='" + Remark + "' where BiddingEmitCode=" + code, connection);
            try
            {
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public int AllowAfterClose
        {
            get
            {
                if (this._BiddingEmitCode != null)
                {
                    this._GetBiddingEmitByCode();
                }
                return this._AllowAfterClose;
            }
            set
            {
                this._AllowAfterClose = value;
            }
        }

        public string BiddingCode
        {
            get
            {
                if ((this._BiddingCode == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._BiddingCode;
            }
            set
            {
                this._BiddingCode = value;
            }
        }

        public string BiddingEmitCode
        {
            get
            {
                return this._BiddingEmitCode;
            }
            set
            {
                this._BiddingEmitCode = value;
            }
        }

        public string BiddingEmitResult
        {
            get
            {
                if ((this._BiddingEmitResult == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._BiddingEmitResult;
            }
            set
            {
                this._BiddingEmitResult = value;
            }
        }

        public string BiddingEmitUsers
        {
            get
            {
                if ((this._BiddingEmitUsers == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._BiddingEmitUsers;
            }
            set
            {
                this._BiddingEmitUsers = value;
            }
        }

        public string BiddingModel
        {
            get
            {
                if ((this._BiddingModel == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._BiddingModel;
            }
            set
            {
                this._BiddingModel = value;
            }
        }

        public string CreatDate
        {
            get
            {
                if ((this._CreatDate == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._CreatDate;
            }
            set
            {
                this._CreatDate = value;
            }
        }

        public string CreatUser
        {
            get
            {
                if ((this._CreatUser == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._CreatUser;
            }
            set
            {
                this._CreatUser = value;
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

        public string EmitDate
        {
            get
            {
                if ((this._EmitDate == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._EmitDate;
            }
            set
            {
                this._EmitDate = value;
            }
        }

        public string EmitNumber
        {
            get
            {
                if ((this._EmitNumber == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._EmitNumber;
            }
            set
            {
                this._EmitNumber = value;
            }
        }

        public string EndDate
        {
            get
            {
                if ((this._EndDate == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }

        public int IsWsZTB
        {
            get
            {
                if (this._BiddingEmitCode != null)
                {
                    this._GetBiddingEmitByCode();
                }
                return this._IsWSZTB;
            }
            set
            {
                this._IsWSZTB = value;
            }
        }

        public string PrejudicationDate
        {
            get
            {
                if ((this._PrejudicationDate == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._PrejudicationDate;
            }
            set
            {
                this._PrejudicationDate = value;
            }
        }

        public int State
        {
            get
            {
                if (this._BiddingEmitCode != null)
                {
                    this._GetBiddingEmitByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string SupplierSelectModel
        {
            get
            {
                if ((this._SupplierSelectModel == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._SupplierSelectModel;
            }
            set
            {
                this._SupplierSelectModel = value;
            }
        }

        public string TotalRemark
        {
            get
            {
                if ((this._TotalRemark == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._TotalRemark;
            }
            set
            {
                this._TotalRemark = value;
            }
        }

        public string TotalRemark2
        {
            get
            {
                if ((this._TotalRemark2 == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._TotalRemark2;
            }
            set
            {
                this._TotalRemark2 = value;
            }
        }

        public string Visualize
        {
            get
            {
                if ((this._Visualize == null) && (this._BiddingEmitCode != null))
                {
                    this._GetBiddingEmitByCode();
                }
                return this._Visualize;
            }
            set
            {
                this._Visualize = value;
            }
        }
    }
}

