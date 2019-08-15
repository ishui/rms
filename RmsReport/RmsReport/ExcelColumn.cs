namespace RmsReport
{
    using Excel;
    using System;
    using System.Data;

    public class ExcelColumn
    {
        public int BlankColumnCount = 2;
        private string[,] Fields;
        public int Height = 1;
        public System.Data.DataTable m_DataSource;
        private Worksheet m_Sheet;
        public int RowNum = 0;
        public int StartCol = 1;
        public int StartRow = 1;
        public int Width = 1;

        public void DataToColumnHead()
        {
            DataToColumnHead(this.m_DataSource.Select(), this.Sheet, this.StartRow, this.StartCol, this.Height, this.Width, this.Fields, ref this.RowNum);
        }

        public static void DataToColumnHead(DataRow[] drs, Worksheet sheet, int StartRow, int StartCol, int ColHeight, int ColWidth, string[,] Fields, ref int RowNum)
        {
            if ((drs.Length != 0) && (Fields != null))
            {
                int num = StartRow;
                int num2 = StartCol;
                int length = drs.Length;
                for (int i = 0; i < length; i++)
                {
                    DataRow dr = drs[i];
                    for (int j = 0; j < ColHeight; j++)
                    {
                        for (int k = 0; k < ColWidth; k++)
                        {
                            string columnName = Fields[j, k];
                            if ((columnName != null) && (columnName.Trim() != ""))
                            {
                                Excel.Range range = TExcel.GetCell(sheet, num + j, num2 + k);
                                try
                                {
                                    string str;
                                    if (((str = columnName.ToUpper()) != null) && (string.IsInterned(str) == "@@ROWNUM"))
                                    {
                                        RowNum++;
                                        range.Value = (int) RowNum;
                                    }
                                    else
                                    {
                                        range.Value = TExcel.GetItemValue(dr, columnName, false);
                                    }
                                }
                                catch
                                {
                                }
                                finally
                                {
                                    TExcel.DisposeObject(range);
                                }
                            }
                        }
                    }
                    num2 += ColWidth;
                }
            }
        }

        public void SetFields()
        {
            if (this.Sheet != null)
            {
                this.Fields = new string[this.Height, this.Width];
                for (int i = 0; i < this.Fields.Length; i++)
                {
                    for (int j = 0; j < this.Width; j++)
                    {
                        string text = TExcel.GetCellValue(this.Sheet, this.StartRow + i, this.StartCol + j);
                        if (text.StartsWith("@"))
                        {
                            if (text.StartsWith("@@"))
                            {
                                this.Fields[i, j] = text;
                            }
                            else
                            {
                                this.Fields[i, j] = text.TrimStart("@".ToCharArray());
                            }
                        }
                    }
                }
            }
        }

        public object DataSource
        {
            get
            {
                return this.m_DataSource;
            }
            set
            {
                this.m_DataSource = TExcel.TransDataSourceToTable(value);
            }
        }

        public Worksheet Sheet
        {
            get
            {
                return this.m_Sheet;
            }
            set
            {
                TExcel.DisposeObject(this.m_Sheet);
                this.m_Sheet = value;
            }
        }
    }
}

