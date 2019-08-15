namespace RmsPM.BLL
{
    using System;
    using System.Data;

    public class CostApportion
    {
        private bool m_IsCustomTotalArea = false;
        private int m_RoundDec = 2;
        private DataTable m_tbArea;
        private DataTable m_tbTotalMoney;
        private decimal m_TotalArea = 0M;

        public CostApportion()
        {
            this.CreateTable();
        }

        private void CalcTotalArea()
        {
            try
            {
                this.m_TotalArea = 0M;
                foreach (DataRow row in this.tbArea.Rows)
                {
                    this.m_TotalArea += ConvertRule.ToDecimal(row["Area"]);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void CreateTable()
        {
            try
            {
                this.m_tbArea = new DataTable();
                this.m_tbArea.Columns.Add("ID");
                this.m_tbArea.Columns.Add("Area", typeof(decimal));
                this.m_tbTotalMoney = new DataTable();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DoApportion()
        {
            try
            {
                if (this.m_tbTotalMoney.Columns.Count == 0)
                {
                    throw new Exception("未设置总金额");
                }
                DataRow row = this.m_tbTotalMoney.Rows[0];
                if (!this.m_IsCustomTotalArea)
                {
                    this.CalcTotalArea();
                }
                decimal[] numArray = new decimal[this.m_tbTotalMoney.Columns.Count];
                int count = this.tbArea.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow row2 = this.tbArea.Rows[i];
                    int index = -1;
                    foreach (DataColumn column in this.m_tbTotalMoney.Columns)
                    {
                        index++;
                        decimal num4 = 0M;
                        decimal num5 = ConvertRule.ToDecimal(row[column.ColumnName]);
                        if (!(this.m_IsCustomTotalArea || (i != (count - 1))))
                        {
                            num4 = num5 - numArray[index];
                        }
                        else
                        {
                            decimal num6 = ConvertRule.ToDecimal(row2["Area"]);
                            if ((num6 != 0M) && (this.TotalArea != 0M))
                            {
                                num4 = MathRule.Round(num5 * (num6 / this.TotalArea), this.m_RoundDec);
                            }
                        }
                        row2[column.ColumnName] = num4;
                        numArray[index] = ConvertRule.ToDecimal(numArray[index]) + num4;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetArea(string ID, decimal Area)
        {
            try
            {
                if (ID == "")
                {
                    throw new Exception("ID不能为空");
                }
                DataRow row = null;
                DataRow[] rowArray = this.tbArea.Select("ID = '" + ID + "'");
                if (rowArray.Length == 0)
                {
                    row = this.tbArea.NewRow();
                    row["ID"] = ID;
                    this.tbArea.Rows.Add(row);
                }
                else
                {
                    row = rowArray[0];
                }
                row["Area"] = Area;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetTotalMoney(string MoneyField, decimal TotalMoney)
        {
            try
            {
                DataRow row;
                if (MoneyField == "")
                {
                    throw new Exception("金额字段明不能为空");
                }
                if (!this.m_tbTotalMoney.Columns.Contains(MoneyField))
                {
                    this.m_tbTotalMoney.Columns.Add(MoneyField, typeof(decimal));
                    this.m_tbArea.Columns.Add(MoneyField, typeof(decimal));
                }
                if (this.m_tbTotalMoney.Rows.Count > 0)
                {
                    row = this.m_tbTotalMoney.Rows[0];
                }
                else
                {
                    row = this.m_tbTotalMoney.NewRow();
                    this.m_tbTotalMoney.Rows.Add(row);
                }
                row[MoneyField] = TotalMoney;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool IsCustomTotalArea
        {
            get
            {
                return this.m_IsCustomTotalArea;
            }
            set
            {
                this.m_IsCustomTotalArea = value;
            }
        }

        public int RoundDec
        {
            get
            {
                return this.m_RoundDec;
            }
            set
            {
                this.m_RoundDec = value;
            }
        }

        public DataTable tbArea
        {
            get
            {
                return this.m_tbArea;
            }
        }

        public DataTable tbTotalMoney
        {
            get
            {
                return this.m_tbTotalMoney;
            }
        }

        public decimal TotalArea
        {
            get
            {
                return this.m_TotalArea;
            }
            set
            {
                this.m_TotalArea = value;
            }
        }
    }
}

