namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingReturn
    {
        private int _Abnegate = 0;
        private string _BiddingDtlCode = null;
        private string _BiddingEmitCode = null;
        private string _BiddingReturnCode = null;
        private string _Condition = null;
        private string _Consultant = null;
        private StandardEntityDAO _dao;
        private string _Design = null;
        private string _FailResult = null;
        private string _Flag = null;
        private string _Money = null;
        private string _OrderCode = null;
        private string _Project = null;
        private string _Remark = null;
        private string _ReturnDate = null;
        private string _score = null;
        private string _State = null;
        private string _SupplierCode = null;

        private void _GetBiddingReturnByCode()
        {
            EntityData biddingReturnByCode = this.GetBiddingReturnByCode(this._BiddingReturnCode);
            this._BiddingReturnCode = biddingReturnByCode.GetString("BiddingReturnCode");
            this._BiddingDtlCode = biddingReturnByCode.GetString("BiddingDtlCode");
            this._BiddingEmitCode = biddingReturnByCode.GetString("BiddingEmitCode");
            this._SupplierCode = biddingReturnByCode.GetString("SupplierCode");
            this._Money = biddingReturnByCode.GetDecimal("Money").ToString();
            this._Remark = biddingReturnByCode.GetString("Remark");
            this._Design = biddingReturnByCode.GetString("Design");
            this._Project = biddingReturnByCode.GetString("Project");
            this._Consultant = biddingReturnByCode.GetString("Consultant");
            this._OrderCode = biddingReturnByCode.GetInt("OrderCode").ToString();
            this._ReturnDate = biddingReturnByCode.GetDateTime("ReturnDate").ToString();
            this._State = biddingReturnByCode.GetString("State");
            this._Flag = biddingReturnByCode.GetString("Flag");
            this._Condition = biddingReturnByCode.GetString("Condition");
            this._score = biddingReturnByCode.GetString("score");
            this._FailResult = biddingReturnByCode.GetString("FailResult");
            this._Abnegate = biddingReturnByCode.GetInt("Abnegate");
            biddingReturnByCode.Dispose();
        }

        private EntityData _GetBiddingReturns()
        {
            EntityData entitydata = new EntityData("BiddingReturn");
            BiddingReturnStrategyBuilder builder = new BiddingReturnStrategyBuilder();
            if (this._BiddingReturnCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.BiddingReturnCode, this._BiddingReturnCode));
            }
            if (this._BiddingDtlCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.BiddingDtlCode, this._BiddingDtlCode));
            }
            if (this._BiddingEmitCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.BiddingEmitCode, this._BiddingEmitCode));
            }
            if (this._SupplierCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.SupplierCode, this._SupplierCode));
            }
            if (this._Money != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.Money, this._Money));
            }
            if (this._Remark != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.Remark, this._Remark));
            }
            if (this._Design != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.Design, this._Design));
            }
            if (this._Project != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.Project, this._Project));
            }
            if (this._Consultant != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.Consultant, this._Consultant));
            }
            if (this._OrderCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.OrderCode, this._OrderCode));
            }
            if (this._ReturnDate != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.ReturnDate, this._ReturnDate));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.Flag, this._Flag));
            }
            if (this._Abnegate != 0)
            {
                builder.AddStrategy(new Strategy(BiddingReturnStrategyName.Abnegate, this._Abnegate.ToString()));
            }
            string sqlString = builder.BuildMainQueryString() + " order by BiddingReturnCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturn"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "BiddingReturn";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public void BiddingEmitReturnDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBiddingReturn(this._GetBiddingReturns());
            }
            else
            {
                this.dao.EntityName = "BiddingReturn";
                this.dao.DeleteEntity(this._GetBiddingReturns());
            }
        }

        public void BiddingReturnAdd()
        {
            if ((this._BiddingReturnCode == null) || (this._BiddingReturnCode == ""))
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingReturn(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void BiddingReturnDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBiddingReturn(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingReturn";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void BiddingReturnDeleteByCode()
        {
            try
            {
                if (this._dao == null)
                {
                    this.dao.DeleteEntity(this.GetBiddingReturnByCode(this.BiddingReturnCode));
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
                    this.dao.DeleteEntity(this.GetBiddingReturnByCode(this.BiddingReturnCode));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void BiddingReturnSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingReturn(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingReturn";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingReturnUpdate()
        {
            if (this._BiddingReturnCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingReturn(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData biddingReturnByCode;
            DataRow newRecord;
            bool flag = false;
            if (this._BiddingReturnCode == "")
            {
                flag = true;
                biddingReturnByCode = this.GetBiddingReturnByCode("");
                this._BiddingReturnCode = SystemManageDAO.GetNewSysCode("BiddingReturn");
                newRecord = biddingReturnByCode.GetNewRecord();
            }
            else
            {
                biddingReturnByCode = this.GetBiddingReturnByCode(this._BiddingReturnCode);
                newRecord = biddingReturnByCode.CurrentRow;
            }
            if (this._BiddingReturnCode != null)
            {
                newRecord["BiddingReturnCode"] = this._BiddingReturnCode;
            }
            if (this._BiddingDtlCode != null)
            {
                newRecord["BiddingDtlCode"] = this._BiddingDtlCode;
            }
            if (this._BiddingEmitCode != null)
            {
                newRecord["BiddingEmitCode"] = this._BiddingEmitCode;
            }
            if (this._SupplierCode != null)
            {
                newRecord["SupplierCode"] = this._SupplierCode;
            }
            if (this._Money != null)
            {
                newRecord["Money"] = this._Money;
            }
            if (this._Remark != null)
            {
                newRecord["Remark"] = this._Remark;
            }
            if (this._Design != null)
            {
                newRecord["Design"] = this._Design;
            }
            if (this._Project != null)
            {
                newRecord["Project"] = this._Project;
            }
            if (this._Consultant != null)
            {
                newRecord["Consultant"] = this._Consultant;
            }
            if (this._OrderCode != null)
            {
                newRecord["OrderCode"] = this._OrderCode;
            }
            if (this._ReturnDate != null)
            {
                newRecord["ReturnDate"] = this._ReturnDate;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (this._Condition != null)
            {
                newRecord["Condition"] = this._Condition;
            }
            if (this._score != null)
            {
                newRecord["score"] = this._score;
            }
            if (this._FailResult != null)
            {
                newRecord["FailResult"] = this._FailResult;
            }
            newRecord["Abnegate"] = this._Abnegate;
            if (flag)
            {
                biddingReturnByCode.AddNewRecord(newRecord);
            }
            return biddingReturnByCode;
        }

        private void DeleteBiddingReturn(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturn"))
                    {
                        ydao.DeleteAllRow(entity);
                        ydao.DeleteEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
                    this.dao.DeleteAllRow(entity);
                    this.dao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private EntityData GetAllBiddingReturn()
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturn"))
                    {
                        data = ydao.SelectAll();
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
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

        private EntityData GetBiddingReturnByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturn"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
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

        public EntityData GetBiddingReturnEntitys()
        {
            EntityData data;
            try
            {
                data = this._GetBiddingReturns();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data;
        }

        public DataTable GetBiddingReturns()
        {
            return this._GetBiddingReturns().CurrentTable;
        }

        public static void GetSHTreeDataSource(DataTable dtConsiderDiathesis, DataTable dtBiddingSupplier, DataTable returndt, string GradeMessageCode, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode)
        {
            int num5;
            if (Code == "")
            {
                returndt.Columns.Add("code", Type.GetType("System.String"));
                returndt.Columns.Add("freeflag", Type.GetType("System.String"));
                returndt.Columns.Add("issubtotal", Type.GetType("System.String"));
                returndt.Columns.Add("ColumnCount", Type.GetType("System.Int32"));
                returndt.Clear();
                dtConsiderDiathesis.Columns.Add("code", Type.GetType("System.String"));
                dtConsiderDiathesis.Columns.Add("freeflag", Type.GetType("System.String"));
                dtConsiderDiathesis.Columns.Add("issubtotal", Type.GetType("System.String"));
                dtConsiderDiathesis.Columns.Add("ColumnCount", Type.GetType("System.Int32"));
                for (int i = 0; i < dtBiddingSupplier.Rows.Count; i++)
                {
                    returndt.Columns.Add("Point" + (i + 1), Type.GetType("System.String"));
                    returndt.Columns.Add("Code" + (i + 1), Type.GetType("System.String"));
                    returndt.Columns.Add("GradeMessageCode" + (i + 1), Type.GetType("System.String"));
                    returndt.Columns["Point" + (i + 1)].Caption = ProjectRule.GetSupplierName(dtBiddingSupplier.Rows[i]["SupplierCode"].ToString());
                    dtConsiderDiathesis.Columns.Add("Point" + (i + 1), Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns.Add("Code" + (i + 1), Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns.Add("GradeMessageCode" + (i + 1), Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns["Point" + (i + 1)].Caption = ProjectRule.GetSupplierName(dtBiddingSupplier.Rows[i]["SupplierCode"].ToString());
                }
            }
            DataRow[] rowArray = dtConsiderDiathesis.Select(ParentCodeName + "='" + ParentCode.ToString() + "' and BiddingGradeTypeCode='100002'");
            DataTable currentTable = BiddingGradeMessage.GetAllBiddingGradeMessage().CurrentTable;
            string text = "";
            for (int j = 0; j < dtBiddingSupplier.Rows.Count; j++)
            {
                if (j != (dtBiddingSupplier.Rows.Count - 1))
                {
                    text = string.Concat(new object[] { text, "'", dtBiddingSupplier.Rows[j]["BiddingReturnCode"], "'," });
                }
                else
                {
                    text = string.Concat(new object[] { text, "'", dtBiddingSupplier.Rows[j]["BiddingReturnCode"], "'" });
                }
            }
            int num3 = 0;
            string text2 = "";
            DataTable biddings = new DataTable();
            if (text != "")
            {
                foreach (DataRow row in currentTable.Select("ApplicationCode in (" + text + ") and BiddingGradeTypeCode='100002'"))
                {
                    if (num3 != (currentTable.Select("ApplicationCode in (" + text + ") and BiddingGradeTypeCode='100002'").Length - 1))
                    {
                        text2 = string.Concat(new object[] { text2, "'", row["BiddingGradeMessageCode"], "'," });
                    }
                    else
                    {
                        text2 = string.Concat(new object[] { text2, "'", row["BiddingGradeMessageCode"], "'" });
                    }
                    num3++;
                }
                BiddingGrade grade = new BiddingGrade();
                grade.BiddingGradeMessageCode = text2;
                biddings = grade.GetBiddings();
            }
            int num4 = 1;
            foreach (DataRow row2 in rowArray)
            {
                if (num4 == 1)
                {
                    row2["freeflag"] = "1";
                }
                else
                {
                    row2["freeflag"] = "0";
                }
                row2["ColumnCount"] = dtBiddingSupplier.Rows.Count;
                row2["code"] = Code + ((num4.ToString().Length < 2) ? ("0" + num4.ToString()) : num4.ToString());
                row2["Percentage"] = Convert.ToDecimal(row2["Percentage"]) * 100M;
                row2["issubtotal"] = "0";
                DataRow row3 = returndt.NewRow();
                row3.ItemArray = row2.ItemArray;
                returndt.Rows.Add(row3);
                for (num5 = 0; num5 < dtBiddingSupplier.Rows.Count; num5++)
                {
                    row3["Point" + (num5 + 1)] = 0;
                    row3["Code" + (num5 + 1)] = "";
                    row3["GradeMessageCode" + (num5 + 1)] = "";
                }
                int num6 = 0;
                if (text != "")
                {
                    foreach (DataRow row4 in currentTable.Select("ApplicationCode in (" + text + ") and BiddingGradeTypeCode='100002'"))
                    {
                        num6++;
                        row3["GradeMessageCode" + num6] = row4["BiddingGradeMessageCode"];
                        foreach (DataRow row5 in biddings.Select(string.Concat(new object[] { "BiddingGradeMessageCode='", row4["BiddingGradeMessageCode"], "' and BiddingConsiderDiathesisCode='", row2["BiddingConsiderDiathesisCode"], "'" })))
                        {
                            row3["Code" + num6] = row5["BiddingGradeCode"];
                            row3["Point" + num6] = row5["GradePoint"];
                        }
                    }
                }
            }
            DataRow row6 = returndt.NewRow();
            row6["BiddingConsiderDiathesisCode"] = "";
            row6["BiddingConsiderDiathesis"] = "总计";
            row6["GradeGuideline"] = "";
            row6["freeflag"] = "0";
            row6["ColumnCount"] = dtBiddingSupplier.Rows.Count;
            row6["code"] = Code + ((num4.ToString().Length < 2) ? ("0" + num4.ToString()) : num4.ToString());
            row6["Percentage"] = Convert.ToDecimal(1) * 100M;
            row6["issubtotal"] = "1";
            returndt.Rows.Add(row6);
            for (num5 = 0; num5 < dtBiddingSupplier.Rows.Count; num5++)
            {
                row6["Point" + (num5 + 1)] = 0;
                row6["Code" + (num5 + 1)] = "";
                row6["GradeMessageCode" + (num5 + 1)] = "";
            }
            for (int k = 0; k < (returndt.Rows.Count - 1); k++)
            {
                for (int m = 0; m < Convert.ToInt32(returndt.Rows[0]["ColumnCount"]); m++)
                {
                    row6["Point" + (m + 1)] = Convert.ToString((decimal) (Convert.ToDecimal(row6["Point" + (m + 1)]) + ((Convert.ToDecimal(returndt.Rows[k]["Point" + (m + 1)]) * Convert.ToDecimal(returndt.Rows[k]["Percentage"])) / 100M)));
                }
            }
        }

        private void InsertBiddingReturn(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturn"))
                    {
                        ydao.InsertEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
                    this.dao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SubmitAllBiddingReturn(EntityData entity)
        {
            Exception exception;
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturn"))
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
                    this.dao.EntityName = "BiddingReturn";
                    this.dao.SubmitEntity(entity);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        private void UpdateBiddingReturn(EntityData entity)
        {
            try
            {
                if (this._dao == null)
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingReturn"))
                    {
                        ydao.UpdateEntity(entity);
                    }
                }
                else
                {
                    this.dao.EntityName = "BiddingReturn";
                    this.dao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int Abnegate
        {
            get
            {
                this._GetBiddingReturnByCode();
                return this._Abnegate;
            }
            set
            {
                this._Abnegate = value;
            }
        }

        public string BiddingDtlCode
        {
            get
            {
                if ((this._BiddingDtlCode == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._BiddingDtlCode;
            }
            set
            {
                this._BiddingDtlCode = value;
            }
        }

        public string BiddingEmitCode
        {
            get
            {
                if ((this._BiddingEmitCode == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._BiddingEmitCode;
            }
            set
            {
                this._BiddingEmitCode = value;
            }
        }

        public string BiddingReturnCode
        {
            get
            {
                return this._BiddingReturnCode;
            }
            set
            {
                this._BiddingReturnCode = value;
            }
        }

        public string Condition
        {
            get
            {
                if ((this._Condition == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._Condition;
            }
            set
            {
                this._Condition = value;
            }
        }

        public string Consultant
        {
            get
            {
                if ((this._Consultant == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._Consultant;
            }
            set
            {
                this._Consultant = value;
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

        public string Design
        {
            get
            {
                if ((this._Design == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._Design;
            }
            set
            {
                this._Design = value;
            }
        }

        public string FailResult
        {
            get
            {
                if ((this._FailResult == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._FailResult;
            }
            set
            {
                this._FailResult = value;
            }
        }

        public string Flag
        {
            get
            {
                if ((this._Flag == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string Money
        {
            get
            {
                if ((this._Money == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._Money;
            }
            set
            {
                this._Money = value;
            }
        }

        public string OrderCode
        {
            get
            {
                if ((this._OrderCode == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._OrderCode;
            }
            set
            {
                this._OrderCode = value;
            }
        }

        public string Project
        {
            get
            {
                if ((this._Project == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._Project;
            }
            set
            {
                this._Project = value;
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string ReturnDate
        {
            get
            {
                if ((this._ReturnDate == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._ReturnDate;
            }
            set
            {
                this._ReturnDate = value;
            }
        }

        public string Score
        {
            get
            {
                if ((this._score == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._score;
            }
            set
            {
                this._score = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
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
                if ((this._SupplierCode == null) && (this._BiddingReturnCode != null))
                {
                    this._GetBiddingReturnByCode();
                }
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }
    }
}

