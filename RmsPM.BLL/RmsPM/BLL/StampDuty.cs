namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;

    public class StampDuty
    {
        private string _Range;
        private string _Remarks;
        private int _StampDutyID;
        private string _TaxItems;
        private string _TaxPayer;
        private decimal _TaxRate;

        public StampDuty()
        {
            this._TaxItems = null;
            this._Range = null;
            this._TaxPayer = null;
            this._Remarks = null;
        }

        public StampDuty(int StampDutyID, string TaxItems, string Range, decimal TaxRate, string TaxPayer, string Remarks)
        {
            this._TaxItems = null;
            this._Range = null;
            this._TaxPayer = null;
            this._Remarks = null;
            this._StampDutyID = StampDutyID;
            TaxItems = TaxItems;
            Range = Range;
            this._TaxRate = TaxRate;
            TaxPayer = TaxPayer;
            Remarks = Remarks;
        }

        public static void Add(EntityData entity)
        {
            using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
            {
                ydao.InsertEntity(entity);
            }
        }

        public static void Add(StampDuty stampduty)
        {
            Add(stampduty.TaxItems, stampduty.Range, stampduty.TaxRate, stampduty.TaxPayer, stampduty.Remarks);
        }

        public static void Add(string TaxItems, string Range, decimal TaxRate, string TaxPayer, string Remarks)
        {
            using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
            {
                EntityData entitydata = ydao.SelectbyPrimaryKey(0);
                DataRow newRecord = entitydata.GetNewRecord();
                newRecord["StampDutyID"] = 0;
                newRecord["TaxItems"] = TaxItems;
                newRecord["Range"] = Range;
                newRecord["TaxRate"] = TaxRate;
                newRecord["TaxPayer"] = TaxPayer;
                newRecord["Remarks"] = Remarks;
                entitydata.AddNewRecord(newRecord);
                ydao.SubmitEntity(entitydata);
            }
        }

        public static void Delete(int StampDutyID)
        {
            using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
            {
                Delete(ydao.SelectbyPrimaryKey(StampDutyID));
            }
        }

        public static void Delete(EntityData entity)
        {
            using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
            {
                ydao.DeleteAllRow(entity);
                ydao.DeleteEntity(entity);
            }
        }

        public static void Delete(StampDuty _StampDuty)
        {
            Delete(_StampDuty.StampDutyID);
        }

        public static DataTable DropDownListSource()
        {
            DataTable table = new DataTable();
            table.Columns.Add("StampDuty");
            table.Columns.Add("StampDutyIDName");
            DataTable table2 = GetList().Tables[0];
            DataRow row = table.NewRow();
            row[0] = "--------请选择--------|0";
            row[1] = "0";
            table.Rows.Add(row);
            foreach (DataRow row2 in table2.Rows)
            {
                DataRow row3 = table.NewRow();
                row3["StampDutyIDName"] = row2["StampDutyID"].ToString();
                row3["StampDuty"] = row2["TaxItems"] + "|" + row2["TaxRate"];
                table.Rows.Add(row3);
            }
            return table;
        }

        public static EntityData GetList()
        {
            EntityData data;
            using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
            {
                data = ydao.SelectAll();
                ydao.Dispose();
            }
            return data;
        }

        public static StampDuty GetModel(int StampDutyID)
        {
            StampDuty duty = new StampDuty();
            using (SingleEntityDAO ydao = new SingleEntityDAO("StampDuty"))
            {
                EntityData data = ydao.SelectbyPrimaryKey(StampDutyID);
                if (data.HasRecord())
                {
                    duty._StampDutyID = data.GetInt("StampDutyID");
                    duty.TaxItems = data.GetString("TaxItems");
                    duty.Range = data.GetString("Range");
                    duty.TaxRate = data.GetDecimal("TaxRate");
                    duty.TaxPayer = data.GetString("TaxPayer");
                    duty.Remarks = data.GetString("Remarks");
                }
            }
            return duty;
        }

        public static void Update(EntityData entity)
        {
            using (StandardEntityDAO ydao = new StandardEntityDAO("StampDuty"))
            {
                ydao.UpdateEntity(entity);
            }
        }

        public static void Update(StampDuty StampDuty)
        {
            using (StandardEntityDAO ydao = new StandardEntityDAO("StampDuty"))
            {
                EntityData entitydata = ydao.SelectbyPrimaryKey(StampDuty.StampDutyID);
                DataRow currentRow = entitydata.CurrentRow;
                currentRow["TaxItems"] = StampDuty.TaxItems;
                currentRow["Range"] = StampDuty.Range;
                currentRow["TaxRate"] = StampDuty.TaxRate;
                currentRow["TaxPayer"] = StampDuty.TaxPayer;
                currentRow["Remarks"] = StampDuty.Remarks;
                ydao.SubmitEntity(entitydata);
            }
        }

        public string Range
        {
            get
            {
                return this._Range;
            }
            set
            {
                this._Range = string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
            }
        }

        public string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                this._Remarks = string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
            }
        }

        public int StampDutyID
        {
            get
            {
                return this._StampDutyID;
            }
            set
            {
                this._StampDutyID = value;
            }
        }

        public string TaxItems
        {
            get
            {
                return this._TaxItems;
            }
            set
            {
                this._TaxItems = string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
            }
        }

        public string TaxPayer
        {
            get
            {
                return this._TaxPayer;
            }
            set
            {
                this._TaxPayer = string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
            }
        }

        public decimal TaxRate
        {
            get
            {
                return this._TaxRate;
            }
            set
            {
                this._TaxRate = value;
            }
        }
    }
}

