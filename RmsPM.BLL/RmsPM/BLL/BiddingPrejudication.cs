namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingPrejudication
    {
        private string _BiddingCode = null;
        private string _BiddingPrejudicationCode = null;
        private string _CreateDate = null;
        private StandardEntityDAO _dao;
        private string _Flag = null;
        private string _Number = null;
        private string _Remark = null;
        private string _State = null;
        private string _UserCode = null;
        private string _WorkConfine = null;

        private void _GetBiddingPrejudicationByCode()
        {
            EntityData biddingPrejudicationByCode = this.GetBiddingPrejudicationByCode(this._BiddingPrejudicationCode);
            this._BiddingPrejudicationCode = biddingPrejudicationByCode.GetString("BiddingPrejudicationCode");
            this._BiddingCode = biddingPrejudicationByCode.GetString("BiddingCode");
            this._WorkConfine = biddingPrejudicationByCode.GetString("WorkConfine");
            this._Number = biddingPrejudicationByCode.GetString("Number").ToString();
            this._Remark = biddingPrejudicationByCode.GetString("Remark");
            this._UserCode = biddingPrejudicationByCode.GetString("UserCode");
            this._CreateDate = biddingPrejudicationByCode.GetDateTimeOnlyDate("CreateDate");
            this._State = biddingPrejudicationByCode.GetString("State");
            this._Flag = biddingPrejudicationByCode.GetString("Flag");
            biddingPrejudicationByCode.Dispose();
        }

        public EntityData _GetBiddingPrejudications()
        {
            EntityData entitydata = new EntityData("BiddingPrejudication");
            BiddingPrejudicationStrategyBuilder builder = new BiddingPrejudicationStrategyBuilder();
            if (this._BiddingPrejudicationCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.BiddingPrejudicationCode, this._BiddingPrejudicationCode));
            }
            if (this._BiddingCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.BiddingCode, this._BiddingCode));
            }
            if (this._WorkConfine != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.WorkConfine, this._WorkConfine));
            }
            if (this._Number != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.Number, this._Number));
            }
            if (this._Remark != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.Remark, this._Remark));
            }
            if (this._UserCode != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.UserCode, this._UserCode));
            }
            if (this._CreateDate != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.CreateDate, this._CreateDate));
            }
            if (this._State != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.State, this._State));
            }
            if (this._Flag != null)
            {
                builder.AddStrategy(new Strategy(BiddingPrejudicationStrategyName.Flag, this._Flag));
            }
            string sqlString = builder.BuildMainQueryString() + " order by BiddingPrejudicationCode";
            if (this._dao == null)
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingPrejudication"))
                {
                    ydao.FillEntity(sqlString, entitydata);
                }
                return entitydata;
            }
            this.dao.EntityName = "BiddingPrejudication";
            this.dao.FillEntity(sqlString, entitydata);
            return entitydata;
        }

        public void BiddingPrejudicationAdd()
        {
            if (this._BiddingPrejudicationCode == null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingPrejudication(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingPrejudication";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        public void BiddingPrejudicationDelete()
        {
            if (this._dao == null)
            {
                this.DeleteBiddingPrejudication(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingPrejudication";
                this.dao.DeleteEntity(this.BuildData());
            }
        }

        public void BiddingPrejudicationSubmit()
        {
            if (this._dao == null)
            {
                this.SubmitAllBiddingPrejudication(this.BuildData());
            }
            else
            {
                this.dao.EntityName = "BiddingPrejudication";
                this.dao.SubmitEntity(this.BuildData());
            }
        }

        public void BiddingPrejudicationUpdate()
        {
            if (this._BiddingPrejudicationCode != null)
            {
                if (this._dao == null)
                {
                    this.SubmitAllBiddingPrejudication(this.BuildData());
                }
                else
                {
                    this.dao.EntityName = "BiddingPrejudication";
                    this.dao.SubmitEntity(this.BuildData());
                }
            }
        }

        private EntityData BuildData()
        {
            EntityData biddingPrejudicationByCode;
            DataRow newRecord;
            bool flag = false;
            if ((this._BiddingPrejudicationCode == "") || (this._BiddingPrejudicationCode == null))
            {
                flag = true;
                if (this._dao == null)
                {
                    biddingPrejudicationByCode = this.GetBiddingPrejudicationByCode("");
                }
                else
                {
                    this.dao.EntityName = "BiddingPrejudication";
                    biddingPrejudicationByCode = this.dao.SelectbyPrimaryKey("");
                }
                this._BiddingPrejudicationCode = SystemManageDAO.GetNewSysCode("BiddingPrejudication");
                newRecord = biddingPrejudicationByCode.GetNewRecord();
            }
            else
            {
                if (this._dao == null)
                {
                    biddingPrejudicationByCode = this.GetBiddingPrejudicationByCode(this._BiddingPrejudicationCode);
                }
                else
                {
                    this.dao.EntityName = "BiddingPrejudication";
                    biddingPrejudicationByCode = this.dao.SelectbyPrimaryKey(this._BiddingPrejudicationCode);
                }
                newRecord = biddingPrejudicationByCode.CurrentRow;
            }
            if (this._BiddingPrejudicationCode != null)
            {
                newRecord["BiddingPrejudicationCode"] = this._BiddingPrejudicationCode;
            }
            if (this._BiddingCode != null)
            {
                newRecord["BiddingCode"] = this._BiddingCode;
            }
            if (this._WorkConfine != null)
            {
                newRecord["WorkConfine"] = this._WorkConfine;
            }
            if (this._Number != null)
            {
                newRecord["Number"] = this._Number;
            }
            if (this._Remark != null)
            {
                newRecord["Remark"] = this._Remark;
            }
            if (this._UserCode != null)
            {
                newRecord["UserCode"] = this._UserCode;
            }
            if (this._CreateDate != null)
            {
                newRecord["CreateDate"] = this._CreateDate;
            }
            if (this._State != null)
            {
                newRecord["State"] = this._State;
            }
            if (this._Flag != null)
            {
                newRecord["Flag"] = this._Flag;
            }
            if (flag)
            {
                biddingPrejudicationByCode.AddNewRecord(newRecord);
            }
            return biddingPrejudicationByCode;
        }

        private void DeleteBiddingPrejudication(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingPrejudication"))
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

        private EntityData GetAllBiddingPrejudication()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingPrejudication"))
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

        private EntityData GetBiddingPrejudicationByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                if (this._dao != null)
                {
                    this._dao.EntityName = "BiddingPrejudication";
                    data = this.dao.SelectbyPrimaryKey(code);
                }
                else
                {
                    using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingPrejudication"))
                    {
                        data = ydao.SelectbyPrimaryKey(code);
                    }
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private EntityData GetBiddingPrejudicationByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                dao.EntityName = "BiddingPrejudication";
                data2 = dao.SelectbyPrimaryKey(code);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public DataTable GetBiddingPrejudications()
        {
            return this._GetBiddingPrejudications().CurrentTable;
        }

        public string GetLastPrejudicationCodeByBiddingCode(string BiddingCode)
        {
            this.BiddingCode = BiddingCode;
            DataTable biddingPrejudications = this.GetBiddingPrejudications();
            if (biddingPrejudications != null)
            {
                DataRow[] rowArray = biddingPrejudications.Select();
                if (biddingPrejudications.Rows.Count != 0)
                {
                    return rowArray[biddingPrejudications.Rows.Count - 1]["BiddingPrejudicationCode"].ToString();
                }
            }
            return "";
        }

        public string GetPrimaryKeyByBiddingCode(string BiddingCode)
        {
            this.BiddingCode = BiddingCode;
            DataTable biddingPrejudications = this.GetBiddingPrejudications();
            if (biddingPrejudications != null)
            {
                DataRow[] rowArray = biddingPrejudications.Select();
                int index = 0;
                while (index < rowArray.Length)
                {
                    DataRow row = rowArray[index];
                    return row["BiddingPrejudicationCode"].ToString();
                }
            }
            return "";
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
                    returndt.Columns["Point" + (i + 1)].Caption = dtBiddingSupplier.Rows[i]["SupplierName"].ToString();
                    dtConsiderDiathesis.Columns.Add("Point" + (i + 1), Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns.Add("Code" + (i + 1), Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns.Add("GradeMessageCode" + (i + 1), Type.GetType("System.String"));
                    dtConsiderDiathesis.Columns["Point" + (i + 1)].Caption = dtBiddingSupplier.Rows[i]["SupplierName"].ToString();
                }
            }
            DataRow[] rowArray = dtConsiderDiathesis.Select(ParentCodeName + "='" + ParentCode.ToString() + "' and BiddingGradeTypeCode='100001'");
            DataTable currentTable = BiddingGradeMessage.GetAllBiddingGradeMessage().CurrentTable;
            string text = "";
            for (int j = 0; j < dtBiddingSupplier.Rows.Count; j++)
            {
                if (j != (dtBiddingSupplier.Rows.Count - 1))
                {
                    text = string.Concat(new object[] { text, "'", dtBiddingSupplier.Rows[j]["BiddingSupplierCode"], "'," });
                }
                else
                {
                    text = string.Concat(new object[] { text, "'", dtBiddingSupplier.Rows[j]["BiddingSupplierCode"], "'" });
                }
            }
            int num3 = 0;
            string text2 = "";
            DataTable biddings = new DataTable();
            if (text != "")
            {
                foreach (DataRow row in currentTable.Select("ApplicationCode in (" + text + ") and BiddingGradeTypeCode='100001'"))
                {
                    if (num3 != (currentTable.Select("ApplicationCode in (" + text + ") and BiddingGradeTypeCode='100001'").Length - 1))
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
                    foreach (DataRow row4 in currentTable.Select("ApplicationCode in (" + text + ") and BiddingGradeTypeCode='100001'"))
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

        private void InsertBiddingPrejudication(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingPrejudication"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool IsOnlyNumber(StandardEntityDAO dao, string number)
        {
            BiddingPrejudication prejudication = new BiddingPrejudication();
            prejudication.dao = dao;
            prejudication.Number = number;
            DataTable biddingPrejudications = prejudication.GetBiddingPrejudications();
            return ((biddingPrejudications != null) && (biddingPrejudications.Rows.Count == 0));
        }

        private void SubmitAllBiddingPrejudication(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingPrejudication"))
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

        private void UpdateBiddingPrejudication(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BiddingPrejudication"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string BiddingCode
        {
            get
            {
                if ((this._BiddingCode == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._BiddingCode;
            }
            set
            {
                this._BiddingCode = value;
            }
        }

        public string BiddingPrejudicationCode
        {
            get
            {
                return this._BiddingPrejudicationCode;
            }
            set
            {
                this._BiddingPrejudicationCode = value;
            }
        }

        public string CreateDate
        {
            get
            {
                if ((this._CreateDate == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._CreateDate;
            }
            set
            {
                this._CreateDate = value;
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
                if ((this._Flag == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._Flag;
            }
            set
            {
                this._Flag = value;
            }
        }

        public string Number
        {
            get
            {
                if ((this._Number == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        public string Remark
        {
            get
            {
                if ((this._Remark == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string State
        {
            get
            {
                if ((this._State == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string UserCode
        {
            get
            {
                if ((this._UserCode == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }

        public string WorkConfine
        {
            get
            {
                if ((this._WorkConfine == null) && (this._BiddingPrejudicationCode != null))
                {
                    this._GetBiddingPrejudicationByCode();
                }
                return this._WorkConfine;
            }
            set
            {
                this._WorkConfine = value;
            }
        }
    }
}

