namespace RmsReport
{
    using Excel;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;

    public class TExcel
    {
        private ArrayList arrColumnHead = null;
        public int BlankRowCount = 2;
        public Workbook Book;
        public ExcelColumn Column;
        private string[] ColumnHead;
        public int ColumnHeadRow = 0;
        public int DetailHeight = 1;
        public Application excel;
        public ExcelGroup Group;
        public bool HideZero = true;
        public bool IsDeleteBlank = true;
        private System.Data.DataTable m_DataSource;
        public static object m_Opt = Type.Missing.ToString();
        private string m_RootDir;
        private int m_RowNum = 0;
        private int m_RowNumLocal = 0;
        private Worksheet m_Sheet;
        private string m_TemplateFileName = "";
        private string m_TemplateFileNamePhy = "";
        private HttpRequest Request;
        private HttpResponse Response;
        public string SaveFileName;
        public string SaveFileNameHttp;
        public string SaveFileNameHttpFull;
        public string SaveFileNamePhy;
        public string SavePathHttp;
        public string SavePathHttpFull;
        public string SavePathPhy;
        private HttpServerUtility Server;
        private HttpSessionState Session;
        public int StartCol = 1;
        public int StartFieldIndex = 0;
        public int StartRow = 1;
        public string TemplatePathPhy;
        public string TemplateSheetName = "Sheet1";

        public event GroupDataSourceEventHandler AfterGetGroupDataSource;

        public event GroupDataViewEventHandler OnGetGroupDataView;

        public event GroupEventHandler OnGroupFooter;

        public event GroupEventHandler OnGroupHeader;

        public TExcel(HttpResponse a_Response, HttpRequest a_Request, HttpServerUtility a_Server, HttpSessionState a_Session)
        {
            this.Response = a_Response;
            this.Request = a_Request;
            this.Server = a_Server;
            this.Session = a_Session;
            this.InitDir();
            this.excel = CreateExcel();
        }

        public void AddWorkbook()
        {
            DisposeObject(this.Book);
            if ((this.m_TemplateFileNamePhy == null) || (this.m_TemplateFileNamePhy == ""))
            {
                this.Book = this.excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            }
            else
            {
                this.Book = AddWorkbook(this.excel, this.m_TemplateFileNamePhy, this.TemplateSheetName);
            }
            this.Sheet = (Worksheet) this.Book.Worksheets[1];
        }

        public static Workbook AddWorkbook(Application excel, string TemplateNamePhy, string SheetName)
        {
            Workbook book = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            AddWorksheet(excel, TemplateNamePhy, SheetName, book);
            return book;
        }

        public int AddWorksheet()
        {
            return this.AddWorksheet(1, false);
        }

        public int AddWorksheet(int BeforeIndex, bool IsAfter)
        {
            int num = 1;
            if (this.Book == null)
            {
                this.AddWorkbook();
                return 1;
            }
            num = AddWorksheet(this.excel, this.m_TemplateFileNamePhy, this.TemplateSheetName, this.Book, BeforeIndex, IsAfter);
            this.Sheet = (Worksheet) this.Book.Worksheets[num];
            return num;
        }

        public static int AddWorksheet(Application excel, string TemplateNamePhy, string SheetName, Workbook book)
        {
            return AddWorksheet(excel, TemplateNamePhy, SheetName, book, 1, false);
        }

        public static int AddWorksheet(Application excel, string TemplateNamePhy, string SheetName, Workbook book, int BeforeIndex, bool IsAfter)
        {
            Workbook workbook = null;
            Sheets worksheets = null;
            Worksheet worksheet = null;
            workbook = excel.Workbooks.Open(TemplateNamePhy, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt);
            try
            {
                if (SheetName == "")
                {
                    worksheets = workbook.Worksheets;
                    if (IsAfter)
                    {
                        worksheets.Copy(m_Opt, book.Worksheets[BeforeIndex]);
                        return (BeforeIndex + 1);
                    }
                    worksheets.Copy(book.Worksheets[BeforeIndex], m_Opt);
                    return BeforeIndex;
                }
                try
                {
                    worksheet = (Worksheet) workbook.Worksheets.get_Item(SheetName);
                    if (IsAfter)
                    {
                        worksheet.Copy(m_Opt, book.Worksheets[BeforeIndex]);
                        return (BeforeIndex + 1);
                    }
                    worksheet.Copy(book.Worksheets[BeforeIndex], m_Opt);
                    return BeforeIndex;
                }
                finally
                {
                    DisposeObject(worksheet);
                }
            }
            finally
            {
                workbook.Close(false, m_Opt, m_Opt);
                DisposeObject(worksheet);
                DisposeObject(worksheets);
                DisposeObject(workbook);
            }
            return 1;
        }

        public static void ArrayToSheet(Worksheet sheet, object[,] objData, int StartRow, int StartCol, int EndRow, int EndCol)
        {
            Excel.Range range = GetCell(sheet, StartRow, StartCol);
            Excel.Range range2 = GetCell(sheet, EndRow, EndCol);
            Excel.Range range3 = sheet.get_Range(range, range2);
            range3.Value = objData;
            DisposeObject(range3);
            DisposeObject(range);
            DisposeObject(range2);
        }

        public void ClientOpen()
        {
            string s = "<script language='javascript'>\nvar excel = new ActiveXObject('Excel.Application');\nexcel.Visible = true;\nexcel.Workbooks.Open('" + this.SaveFileNameHttpFull + "');\n</script>\n";
            this.Response.Write(s);
        }

        public void ClientPreview()
        {
            string s = "<script language='javascript'>\nvar excel = new ActiveXObject('Excel.Application');\nexcel.Visible = true;\nvar book = excel.Workbooks.Open('" + this.SaveFileNameHttpFull + "');\nbook.PrintPreview();\n</script>\n";
            this.Response.Write(s);
        }

        public void ClientPrint()
        {
            string s = "<script language='javascript'>\nvar excel = new ActiveXObject('Excel.Application');\nexcel.Visible = true;\nvar book = excel.Workbooks.Open('" + this.SaveFileNameHttpFull + "');\nbook.PrintOut(false);\nexcel.Quit();\n</script>\n";
            this.Response.Write(s);
        }

        public static Application CreateExcel()
        {
            Application application2;
            try
            {
                Application application = new ApplicationClass();
                application.DisplayAlerts = false;
                application2 = application;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return application2;
        }

        public void DataToSheet()
        {
            try
            {
                this.m_RowNum = 0;
                this.m_RowNumLocal = 0;
                this.InitColumnHead();
                if ((this.Column != null) && (this.Column.DataSource != null))
                {
                    this.Column.Sheet = this.Sheet;
                    try
                    {
                        InsertBlankColumn(this.Sheet, this.Column.StartCol, this.Column.Width, this.Column.BlankColumnCount, this.Column.m_DataSource.Rows.Count);
                        this.Column.SetFields();
                        this.Column.DataToColumnHead();
                    }
                    finally
                    {
                        DisposeObject(this.Column.Sheet);
                    }
                }
                if (this.Group != null)
                {
                    if (this.Group.DataSource == null)
                    {
                        this.Group.DataSource = GetGroup(this.m_DataSource, this.Group.GroupFieldName);
                        if (this.AfterGetGroupDataSource != null)
                        {
                            this.AfterGetGroupDataSource(this.Group.GroupFieldName, this.Group.m_DataSource);
                        }
                    }
                    InsertBlankGroup(this.Sheet, this.StartRow, this.Group.Height, this.Group.BlankGroupCount, this.Group.m_DataSource.Rows.Count);
                    DataView groupDataView = null;
                    if (this.OnGetGroupDataView != null)
                    {
                        this.OnGetGroupDataView(this.Group.GroupFieldName, this.Group.m_DataSource, ref groupDataView);
                    }
                    if (groupDataView == null)
                    {
                        if (this.Group.AutoSort)
                        {
                            groupDataView = new DataView(this.Group.m_DataSource, "", this.Group.GroupFieldName, DataViewRowState.CurrentRows);
                        }
                        else
                        {
                            groupDataView = new DataView(this.Group.m_DataSource);
                        }
                    }
                    int rowIndex = this.StartRow;
                    foreach (DataRowView view2 in groupDataView)
                    {
                        DataRow drGroup = view2.Row;
                        this.m_RowNumLocal = 0;
                        string groupFieldValue = drGroup[this.Group.GroupFieldName].ToString();
                        if ((this.Group.HeaderHeight > 0) && (this.OnGroupHeader != null))
                        {
                            this.OnGroupHeader(this.Sheet, rowIndex, groupFieldValue, drGroup);
                        }
                        string filterExpression = this.Group.GroupFieldName + "='" + groupFieldValue + "'";
                        if (groupFieldValue == "")
                        {
                            filterExpression = filterExpression + " or " + this.Group.GroupFieldName + " is null";
                        }
                        DataRow[] drs = this.m_DataSource.Select(filterExpression);
                        InsertBlankRow(this.Sheet, rowIndex + this.Group.HeaderHeight, this.Group.DetailHeight, drs.Length, this.DetailHeight, this.IsDeleteBlank);
                        if (this.DetailHeight == 1)
                        {
                            if (this.arrColumnHead != null)
                            {
                                DataToSheetByArray(drs, this.Sheet, rowIndex + this.Group.HeaderHeight, this.StartCol, true, this.arrColumnHead, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal);
                            }
                            else if (this.ColumnHead != null)
                            {
                                DataToSheetByArray(drs, this.Sheet, rowIndex + this.Group.HeaderHeight, this.StartCol, this.ColumnHead, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal);
                            }
                            else
                            {
                                bool hasColumnHead = (this.TemplateFileName == null) || (this.TemplateFileName == "");
                                DataToSheetByArray(drs, this.Sheet, rowIndex + this.Group.HeaderHeight, this.StartCol, this.StartFieldIndex, hasColumnHead, this.m_DataSource, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal);
                            }
                        }
                        else
                        {
                            DataToSheetByCell(drs, this.Sheet, rowIndex + this.Group.HeaderHeight, this.StartCol, this.StartFieldIndex, this.ColumnHead, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal, this.DetailHeight);
                        }
                        if ((this.Group.FooterHeight > 0) && (this.OnGroupFooter != null))
                        {
                            this.OnGroupFooter(this.Sheet, (rowIndex + this.Group.HeaderHeight) + drs.Length, groupFieldValue, drGroup);
                        }
                        rowIndex = ((rowIndex + this.Group.Height) + drs.Length) - this.Group.DetailHeight;
                    }
                }
                else
                {
                    InsertBlankRow(this.Sheet, this.StartRow, this.BlankRowCount, this.m_DataSource.Rows.Count, this.DetailHeight, this.IsDeleteBlank);
                    if (this.DetailHeight == 1)
                    {
                        if (this.arrColumnHead != null)
                        {
                            DataToSheetByArray(this.m_DataSource.Select(), this.Sheet, this.StartRow, this.StartCol, true, this.arrColumnHead, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal);
                        }
                        else if (this.ColumnHead != null)
                        {
                            DataToSheetByArray(this.m_DataSource.Select(), this.Sheet, this.StartRow, this.StartCol, this.ColumnHead, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal);
                        }
                        else
                        {
                            bool flag2 = (this.TemplateFileName == null) || (this.TemplateFileName == "");
                            DataToSheetByArray(this.m_DataSource.Select(), this.Sheet, this.StartRow, this.StartCol, this.StartFieldIndex, flag2, this.m_DataSource, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal);
                        }
                    }
                    else
                    {
                        DataToSheetByCell(this.m_DataSource.Select(), this.Sheet, this.StartRow, this.StartCol, this.StartFieldIndex, this.ColumnHead, this.HideZero, ref this.m_RowNum, ref this.m_RowNumLocal, this.DetailHeight);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataToSheetByArray(DataRow[] drs, Worksheet sheet, int StartRow, int StartCol, string[] ColumnHead, bool HideZero, ref int RowNum, ref int RowNumLocal)
        {
            if (drs.Length != 0)
            {
                int num = StartRow;
                int length = drs.Length;
                if ((ColumnHead != null) && (ColumnHead.Length > 0))
                {
                    int num3 = ColumnHead.Length;
                    object[,] objData = new object[length, num3];
                    for (int i = 0; i < length; i++)
                    {
                        DataRow dr = drs[i];
                        for (int j = 0; j < num3; j++)
                        {
                            string columnName = ColumnHead[j];
                            if ((columnName != null) && (columnName.Trim() != ""))
                            {
                                try
                                {
                                    switch (columnName.ToUpper())
                                    {
                                        case "@@ROWNUM":
                                            RowNum++;
                                            objData[i, j] = (int) RowNum;
                                            goto Label_00F0;

                                        case "@@ROWNUMLOCAL":
                                            RowNumLocal++;
                                            objData[i, j] = (int) RowNumLocal;
                                            goto Label_00F0;
                                    }
                                    objData[i, j] = GetItemValue(dr, columnName, HideZero);
                                }
                                catch
                                {
                                }
                            Label_00F0:;
                            }
                        }
                    }
                    int num6 = -1;
                    int num7 = -1;
                    for (int k = 0; k <= num3; k++)
                    {
                        string text2 = "";
                        if (k < num3)
                        {
                            text2 = ColumnHead[k];
                        }
                        if ((text2 != null) && (text2.Trim() != ""))
                        {
                            if (num6 < 0)
                            {
                                num6 = k;
                                num7 = k;
                            }
                            else
                            {
                                num7 = k;
                            }
                        }
                        else if (num6 >= 0)
                        {
                            int num9 = (num7 - num6) + 1;
                            if (num9 == num3)
                            {
                                ArrayToSheet(sheet, objData, StartRow, StartCol + num6, (StartRow + length) - 1, StartCol + num7);
                            }
                            else
                            {
                                object[,] objArray2 = new object[length, num9];
                                for (int m = 0; m < length; m++)
                                {
                                    for (int n = 0; n < num9; n++)
                                    {
                                        objArray2[m, n] = objData[m, n + num6];
                                    }
                                }
                                ArrayToSheet(sheet, objArray2, StartRow, StartCol + num6, (StartRow + length) - 1, StartCol + num7);
                            }
                            num6 = -1;
                            num7 = -1;
                        }
                    }
                }
            }
        }

        public static void DataToSheetByArray(DataRow[] drs, Worksheet sheet, int StartRow, int StartCol, bool HasColumnHead, ArrayList arrColumnHead, bool HideZero, ref int RowNum, ref int RowNumLocal)
        {
            if (drs.Length != 0)
            {
                int num = StartRow;
                int length = drs.Length;
                if (arrColumnHead != null)
                {
                    int count = arrColumnHead.Count;
                    int i = StartRow;
                    if (HasColumnHead)
                    {
                        object[,] objArray = new object[1, count];
                        for (int j = 0; j < count; j++)
                        {
                            try
                            {
                                objArray[0, j] = GetColumnHeadDesc(arrColumnHead[j].ToString());
                            }
                            catch
                            {
                            }
                        }
                        Excel.Range range = GetCell(sheet, i, StartCol);
                        Excel.Range range2 = GetCell(sheet, i, (StartCol + count) - 1);
                        Excel.Range range3 = sheet.get_Range(range, range2);
                        range3.Value = objArray;
                        DisposeObject(range3);
                        DisposeObject(range);
                        DisposeObject(range2);
                        i++;
                    }
                    object[,] objData = new object[length, count];
                    for (int k = 0; k < length; k++)
                    {
                        DataRow dr = drs[k];
                        for (int m = 0; m < count; m++)
                        {
                            string columnName = GetColumnHeadName(arrColumnHead[m].ToString());
                            if ((columnName != null) && (columnName.Trim() != ""))
                            {
                                try
                                {
                                    switch (columnName.ToUpper())
                                    {
                                        case "@@ROWNUM":
                                            RowNum++;
                                            objData[k, m] = (int) RowNum;
                                            goto Label_0192;

                                        case "@@ROWNUMLOCAL":
                                            RowNumLocal++;
                                            objData[k, m] = (int) RowNumLocal;
                                            goto Label_0192;
                                    }
                                    objData[k, m] = "'" + GetItemValue(dr, columnName, HideZero);
                                }
                                catch
                                {
                                }
                            Label_0192:;
                            }
                        }
                    }
                    int num8 = -1;
                    int num9 = -1;
                    for (int n = 0; n <= count; n++)
                    {
                        string columnHeadDesc = "";
                        if (n < count)
                        {
                            columnHeadDesc = GetColumnHeadDesc(arrColumnHead[n].ToString());
                        }
                        if ((columnHeadDesc != null) && (columnHeadDesc.Trim() != ""))
                        {
                            if (num8 < 0)
                            {
                                num8 = n;
                                num9 = n;
                            }
                            else
                            {
                                num9 = n;
                            }
                        }
                        else if (num8 >= 0)
                        {
                            int num11 = (num9 - num8) + 1;
                            if (num11 == count)
                            {
                                ArrayToSheet(sheet, objData, i, StartCol + num8, (i + length) - 1, StartCol + num9);
                            }
                            else
                            {
                                object[,] objArray3 = new object[length, num11];
                                for (int num12 = 0; num12 < length; num12++)
                                {
                                    for (int num13 = 0; num13 < num11; num13++)
                                    {
                                        objArray3[num12, num13] = objData[num12, num13 + num8];
                                    }
                                }
                                ArrayToSheet(sheet, objArray3, i, StartCol + num8, (i + length) - 1, StartCol + num9);
                            }
                            num8 = -1;
                            num9 = -1;
                        }
                    }
                }
            }
        }

        public static void DataToSheetByArray(DataRow[] drs, Worksheet sheet, int StartRow, int StartCol, int StartFieldIndex, bool HasColumnHead, System.Data.DataTable tbData, bool HideZero, ref int RowNum, ref int RowNumLocal)
        {
            if (drs.Length != 0)
            {
                int num = StartRow;
                int length = drs.Length;
                int num3 = drs[0].ItemArray.Length - StartFieldIndex;
                int i = StartRow;
                if (HasColumnHead && (tbData != null))
                {
                    object[] objArray = new object[num3];
                    for (int j = 0; j < num3; j++)
                    {
                        try
                        {
                            string caption = tbData.Columns[j].Caption;
                            switch (caption)
                            {
                                case null:
                                case "":
                                    caption = tbData.Columns[j].ColumnName;
                                    break;
                            }
                            objArray[j] = caption;
                        }
                        catch
                        {
                        }
                    }
                    Excel.Range range = GetCell(sheet, i, StartCol);
                    Excel.Range range2 = GetCell(sheet, i, (StartCol + num3) - 1);
                    Excel.Range range3 = sheet.get_Range(range, range2);
                    range3.Value = objArray;
                    DisposeObject(range3);
                    DisposeObject(range);
                    DisposeObject(range2);
                    i++;
                }
                object[,] objArray2 = new object[length, num3];
                for (int k = 0; k < length; k++)
                {
                    DataRow dr = drs[k];
                    for (int m = 0; m < num3; m++)
                    {
                        try
                        {
                            objArray2[k, m] = GetItemValue(dr, (int) (m + StartFieldIndex), HideZero);
                        }
                        catch
                        {
                        }
                    }
                }
                Excel.Range range4 = GetCell(sheet, i, StartCol);
                Excel.Range range5 = GetCell(sheet, (i + length) - 1, (StartCol + num3) - 1);
                Excel.Range range6 = sheet.get_Range(range4, range5);
                range6.Value = objArray2;
                DisposeObject(range6);
                DisposeObject(range4);
                DisposeObject(range5);
            }
        }

        public static void DataToSheetByCell(DataRow[] drs, Worksheet sheet, int StartRow, int StartCol, int StartFieldIndex, string[] ColumnHead, bool HideZero, ref int RowNum, ref int RowNumLocal, int DetailHeight)
        {
            if (drs.Length != 0)
            {
                int i = StartRow;
                int length = drs.Length;
                int num4 = drs[0].ItemArray.Length;
                for (int j = 0; j < length; j++)
                {
                    DataRow dr = drs[j];
                    int num2 = StartCol;
                    if ((ColumnHead == null) || (ColumnHead.Length <= 0))
                    {
                        for (int k = StartFieldIndex; k < num4; k++)
                        {
                            try
                            {
                                Excel.Range range = GetCell(sheet, i, num2);
                                range.Value = GetItemValue(dr, k, HideZero);
                                DisposeObject(range);
                            }
                            catch
                            {
                            }
                            num2++;
                        }
                    }
                    else
                    {
                        for (int m = 0; m < ColumnHead.Length; m++)
                        {
                            string columnName = ColumnHead[m];
                            if ((columnName != null) && (columnName.Trim() != ""))
                            {
                                Excel.Range range2 = GetCell(sheet, i, num2);
                                try
                                {
                                    switch (columnName.ToUpper())
                                    {
                                        case "@@ROWNUM":
                                            RowNum++;
                                            range2.Value = (int) RowNum;
                                            goto Label_0138;

                                        case "@@ROWNUMLOCAL":
                                            RowNumLocal++;
                                            range2.Value = (int) RowNumLocal;
                                            goto Label_0138;
                                    }
                                    range2.Value = GetItemValue(dr, columnName, HideZero);
                                }
                                catch
                                {
                                }
                                finally
                                {
                                    DisposeObject(range2);
                                }
                            }
                        Label_0138:
                            num2++;
                        }
                    }
                    i += DetailHeight;
                }
            }
        }

        public void DataToSheetSingle()
        {
            try
            {
                if (this.DataSource != null)
                {
                    DataRow dr = null;
                    if (this.m_DataSource.Rows.Count > 0)
                    {
                        dr = this.m_DataSource.Rows[0];
                    }
                    DataToSheetSingle(dr, this.Sheet, this.HideZero);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DataToSheetSingle(DataRow dr)
        {
            try
            {
                DataToSheetSingle(dr, this.Sheet, this.HideZero);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataToSheetSingle(DataRow dr, Worksheet sheet, bool HideZero)
        {
            try
            {
                if (dr != null)
                {
                    int count = sheet.UsedRange.Rows.Count;
                    int num2 = sheet.UsedRange.Columns.Count;
                    for (int i = 1; i <= count; i++)
                    {
                        for (int j = 1; j <= num2; j++)
                        {
                            string text = GetCellValue(sheet, i, j);
                            string val = "";
                            if (!text.StartsWith("@@"))
                            {
                                goto Label_0135;
                            }
                            string text3 = text.TrimStart("@@".ToCharArray());
                            switch (text3.ToLower())
                            {
                                case "year":
                                    val = DateTime.Today.Year.ToString();
                                    goto Label_0129;

                                case "month":
                                    val = DateTime.Today.Month.ToString();
                                    goto Label_0129;

                                case "day":
                                    val = DateTime.Today.Day.ToString();
                                    break;

                                case "today":
                                    val = DateTime.Today.ToString("yyyy-MM-dd");
                                    break;
                            }
                        Label_0129:
                            SetCellValue(sheet, i, j, val);
                            continue;
                        Label_0135:
                            if (text.StartsWith("@"))
                            {
                                string columnName = text.TrimStart("@".ToCharArray());
                                val = GetItemValue(dr, columnName, HideZero);
                                SetCellValue(sheet, i, j, val);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Dispose()
        {
            try
            {
                this.DisposeExcel();
            }
            catch
            {
            }
        }

        public void DisposeExcel()
        {
            try
            {
                this.excel.Workbooks.Close();
                this.excel.Quit();
                DisposeObject(this.Sheet);
                DisposeObject(this.Book);
                DisposeObject(this.excel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DisposeObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
            }
            catch
            {
            }
            finally
            {
                obj = null;
            }
        }

        public static Excel.Range GetCell(Worksheet sheet, int i, int j)
        {
            Excel.Range cells = null;
            Excel.Range range3;
            try
            {
                cells = sheet.Cells;
                Excel.Range range2 = (Excel.Range) cells[i, j];
                range3 = range2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                DisposeObject(cells);
            }
            return range3;
        }

        public static string GetCellValue(Worksheet sheet, int i, int j)
        {
            Excel.Range cells = null;
            string text2;
            try
            {
                cells = sheet.Cells;
                Excel.Range range2 = (Excel.Range) cells[i, j];
                string text = range2.Text.ToString();
                DisposeObject(range2);
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                DisposeObject(cells);
            }
            return text2;
        }

        private static string GetColumnHeadDesc(string val)
        {
            if (val == "")
            {
                return "";
            }
            int index = val.IndexOf("=");
            if (index >= 0)
            {
                return val.Substring(index + 1, val.Length - (index + 1));
            }
            return val;
        }

        private static string GetColumnHeadName(string val)
        {
            if (val == "")
            {
                return "";
            }
            int length = val.IndexOf("=");
            if (length >= 0)
            {
                return val.Substring(0, length);
            }
            return val;
        }

        public static System.Data.DataTable GetGroup(System.Data.DataTable tb, string GroupField)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add(GroupField, Type.GetType("System.String"));
            foreach (DataRow row in tb.Rows)
            {
                string text = row[GroupField].ToString();
                if (table.Select(GroupField + "='" + text + "'").Length == 0)
                {
                    DataRow row2 = table.NewRow();
                    row2[GroupField] = text;
                    table.Rows.Add(row2);
                }
            }
            return table;
        }

        public static string GetItemValue(DataRow dr, int columnIndex, bool HideZero)
        {
            string s = "";
            if (dr != null)
            {
                try
                {
                    s = dr[columnIndex].ToString();
                }
                catch
                {
                }
                if (HideZero && (s != ""))
                {
                    System.Data.DataTable table = dr.Table;
                    if (((table != null) && IsNum(table.Columns[columnIndex].DataType)) && (double.Parse(s) == 0))
                    {
                        s = "";
                    }
                }
            }
            return s;
        }

        public static string GetItemValue(DataRow dr, string columnName, bool HideZero)
        {
            string s = "";
            if (dr != null)
            {
                try
                {
                    s = dr[columnName].ToString();
                }
                catch
                {
                }
                if (HideZero && (s != ""))
                {
                    System.Data.DataTable table = dr.Table;
                    if (((table != null) && table.Columns.Contains(columnName)) && (IsNum(table.Columns[columnName].DataType) && (double.Parse(s) == 0)))
                    {
                        s = "";
                    }
                }
            }
            return s;
        }

        public static void HideColumn(Worksheet sheet, int column)
        {
            try
            {
                ((Excel.Range) sheet.Cells[1, column]).EntireColumn.Hidden = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void HideRow(Worksheet sheet, int row)
        {
            try
            {
                ((Excel.Range) sheet.Cells[row, 1]).EntireRow.Hidden = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void InitColumnHead()
        {
            this.ColumnHead = null;
            if ((this.Sheet != null) && (this.ColumnHeadRow > 0))
            {
                this.ColumnHead = new string[(this.Sheet.UsedRange.Columns.Count - this.StartCol) + 1];
                for (int i = 0; i < this.ColumnHead.Length; i++)
                {
                    this.ColumnHead[i] = GetCellValue(this.Sheet, this.ColumnHeadRow, i + this.StartCol);
                }
            }
        }

        public void InitDir()
        {
            this.m_RootDir = this.Request.ServerVariables["appl_physical_path"].ToString();
            this.TemplatePathPhy = this.m_RootDir + @"template\";
            this.SavePathPhy = this.m_RootDir + @"temp\";
            this.SavePathHttp = "../temp/" + this.Session.SessionID.ToString() + "/";
            string text = ConfigurationSettings.AppSettings["VirtualDirectory"];
            text = text.Replace("\"", "");
            this.SavePathHttpFull = "http://" + this.Request.ServerVariables["http_host"].ToString() + "/" + text + "/temp/" + this.Session.SessionID.ToString() + "/";
            if (!Directory.Exists(this.SavePathPhy))
            {
                Directory.CreateDirectory(this.SavePathPhy);
            }
            this.SavePathPhy = this.SavePathPhy + this.Session.SessionID.ToString() + @"\";
            if (!Directory.Exists(this.SavePathPhy))
            {
                Directory.CreateDirectory(this.SavePathPhy);
            }
        }

        public static void InsertBlankColumn(Worksheet sheet, int ColumnStart, int ColumnWidth, int BlankColumnCount, int ColumnCount)
        {
            if ((BlankColumnCount > 0) && (BlankColumnCount != ColumnCount))
            {
                int num = (ColumnStart + ColumnWidth) - 1;
                if (BlankColumnCount > ColumnCount)
                {
                    for (int i = 0; i < (BlankColumnCount - ColumnCount); i++)
                    {
                        Excel.Range range = (Excel.Range) sheet.Columns[ColumnStart, m_Opt];
                        Excel.Range range2 = (Excel.Range) sheet.Columns[num, m_Opt];
                        Excel.Range range3 = sheet.get_Range(range, range2);
                        range3.Delete(m_Opt);
                        DisposeObject(range3);
                        DisposeObject(range);
                        DisposeObject(range2);
                    }
                }
                else if (BlankColumnCount < ColumnCount)
                {
                    int num3 = ColumnStart + ColumnWidth;
                    int num4 = (num3 + ColumnWidth) - 1;
                    for (int j = 0; j < (ColumnCount - BlankColumnCount); j++)
                    {
                        Excel.Range range4 = (Excel.Range) sheet.Columns[num3, m_Opt];
                        Excel.Range range5 = (Excel.Range) sheet.Columns[num4, m_Opt];
                        Excel.Range range6 = sheet.get_Range(range4, range5);
                        range6.Copy(m_Opt);
                        range6.Insert(XlInsertShiftDirection.xlShiftDown);
                        DisposeObject(range6);
                        DisposeObject(range4);
                        DisposeObject(range5);
                    }
                }
            }
        }

        public static void InsertBlankGroup(Worksheet sheet, int GroupStart, int GroupHeight, int BlankGroupCount, int GroupCount)
        {
            if ((BlankGroupCount > 0) && (BlankGroupCount != GroupCount))
            {
                int num = (GroupStart + GroupHeight) - 1;
                if (BlankGroupCount > GroupCount)
                {
                    for (int i = 0; i < (BlankGroupCount - GroupCount); i++)
                    {
                        Excel.Range range = (Excel.Range) sheet.Rows[GroupStart, m_Opt];
                        Excel.Range range2 = (Excel.Range) sheet.Rows[num, m_Opt];
                        Excel.Range range3 = sheet.get_Range(range, range2);
                        range3.Delete(m_Opt);
                        DisposeObject(range3);
                        DisposeObject(range);
                        DisposeObject(range2);
                    }
                }
                else if (BlankGroupCount < GroupCount)
                {
                    int num3 = GroupStart + GroupHeight;
                    int num4 = (num3 + GroupHeight) - 1;
                    for (int j = 0; j < (GroupCount - BlankGroupCount); j++)
                    {
                        Excel.Range range4 = (Excel.Range) sheet.Rows[num3, m_Opt];
                        Excel.Range range5 = (Excel.Range) sheet.Rows[num4, m_Opt];
                        Excel.Range range6 = sheet.get_Range(range4, range5);
                        range6.Copy(m_Opt);
                        range6.Insert(XlInsertShiftDirection.xlShiftDown);
                        DisposeObject(range6);
                        DisposeObject(range4);
                        DisposeObject(range5);
                    }
                }
            }
        }

        public static void InsertBlankRow(Worksheet sheet, int RowStart, int BlankRowCount, int RowCount, int DetailHeight, bool IsDeleteBlank)
        {
            if ((BlankRowCount > 0) && (BlankRowCount != RowCount))
            {
                int num = (RowStart + BlankRowCount) - 1;
                if ((BlankRowCount > RowCount) && IsDeleteBlank)
                {
                    Excel.Range range = (Excel.Range) sheet.Rows[RowStart + (RowCount * DetailHeight), m_Opt];
                    Excel.Range range2 = (Excel.Range) sheet.Rows[(RowStart + (BlankRowCount * DetailHeight)) - 1, m_Opt];
                    Excel.Range range3 = sheet.get_Range(range, range2);
                    DisposeObject(range3.Delete(m_Opt));
                    DisposeObject(range3);
                    DisposeObject(range);
                    DisposeObject(range2);
                }
                else if (BlankRowCount < RowCount)
                {
                    while (BlankRowCount < RowCount)
                    {
                        int num3;
                        int num2 = RowCount - BlankRowCount;
                        if (num2 < (BlankRowCount - 1))
                        {
                            num3 = num2;
                        }
                        else
                        {
                            num3 = BlankRowCount - 1;
                            if (num3 == 0)
                            {
                                num3 = 1;
                            }
                        }
                        int num4 = RowStart + DetailHeight;
                        int num5 = ((RowStart + DetailHeight) + (num3 * DetailHeight)) - 1;
                        Excel.Range range4 = (Excel.Range) sheet.Rows[num4, m_Opt];
                        Excel.Range range5 = (Excel.Range) sheet.Rows[num5, m_Opt];
                        Excel.Range range6 = sheet.get_Range(range4, range5);
                        range6.Copy(m_Opt);
                        DisposeObject(range6.Insert(XlInsertShiftDirection.xlShiftDown));
                        BlankRowCount += num3;
                        DisposeObject(range6);
                        DisposeObject(range4);
                        DisposeObject(range5);
                    }
                }
            }
        }

        private static bool IsNum(Type type)
        {
            return (((type.ToString() == "System.Decimal") || (type.ToString() == "System.Double")) || (((type.ToString() == "System.Int16") || (type.ToString() == "System.Int32")) || (type.ToString() == "System.Int64")));
        }

        public virtual void ProcessGroupHeader(Worksheet Sheet, int RowIndex, string GroupFieldValue)
        {
        }

        public void SaveWorkbook()
        {
            if ((this.SaveFileNamePhy == null) || (this.SaveFileNamePhy == ""))
            {
                this.SetSaveFile("");
            }
            this.Book.SaveAs(this.SaveFileNamePhy, m_Opt, m_Opt, m_Opt, m_Opt, m_Opt, XlSaveAsAccessMode.xlNoChange, m_Opt, m_Opt, m_Opt, m_Opt);
            this.Book.Close(false, m_Opt, m_Opt);
        }

        public void SetCellFormulaR1C1(string name, object val)
        {
            try
            {
                SetCellFormulaR1C1(this.Sheet, name, val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetCellFormulaR1C1(Worksheet sheet, string name, object val)
        {
            try
            {
                Excel.Range range = sheet.get_Range(name, m_Opt);
                range.FormulaR1C1 = val;
                DisposeObject(range);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetCellFormulaR1C1(int row, int col, object val)
        {
            try
            {
                SetCellFormulaR1C1(this.Sheet, row, col, val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetCellFormulaR1C1(Worksheet sheet, int row, int col, object val)
        {
            try
            {
                Excel.Range range = GetCell(sheet, row, col);
                range.FormulaR1C1 = val;
                DisposeObject(range);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetCellNumberFormatLocal(string name, object val)
        {
            try
            {
                SetCellNumberFormatLocal(this.Sheet, name, val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetCellNumberFormatLocal(Worksheet sheet, string name, object val)
        {
            try
            {
                Excel.Range range = sheet.get_Range(name, m_Opt);
                range.NumberFormatLocal = val;
                DisposeObject(range);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetCellNumberFormatLocal(int row, int col, object val)
        {
            try
            {
                SetCellNumberFormatLocal(this.Sheet, row, col, val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetCellNumberFormatLocal(Worksheet sheet, int row, int col, object val)
        {
            try
            {
                Excel.Range range = GetCell(sheet, row, col);
                range.NumberFormatLocal = val;
                DisposeObject(range);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetCellValue(string name, object val)
        {
            try
            {
                SetCellValue(this.Sheet, name, val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetCellValue(Worksheet sheet, string name, object val)
        {
            try
            {
                Excel.Range range = sheet.get_Range(name, m_Opt);
                range.Value = val;
                DisposeObject(range);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetCellValue(int row, int col, object val)
        {
            try
            {
                SetCellValue(this.Sheet, row, col, val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetCellValue(Worksheet sheet, int row, int col, object val)
        {
            try
            {
                Excel.Range range = GetCell(sheet, row, col);
                range.Value = val;
                DisposeObject(range);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetColumnHead(ArrayList arr)
        {
            try
            {
                this.arrColumnHead = arr;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SetSaveFile(string a_TemplateFileName)
        {
            this.SaveFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            this.SaveFileNamePhy = this.SavePathPhy + this.SaveFileName;
            this.SaveFileNameHttp = this.SavePathHttp + HttpUtility.UrlEncode(this.SaveFileName, Encoding.Default);
            this.SaveFileNameHttpFull = this.SavePathHttpFull + HttpUtility.UrlEncode(this.SaveFileName, Encoding.Default);
        }

        public void ShowClient()
        {
            this.Response.Write("<script language='javascript'>");
            this.Response.Write("window.open('" + this.SaveFileNameHttp + "');");
            this.Response.Write("</script>");
        }

        public static System.Data.DataTable TransDataSourceToTable(object DataSource)
        {
            System.Data.DataTable table2;
            try
            {
                System.Data.DataTable table = null;
                if (DataSource is System.Data.DataTable)
                {
                    table = (System.Data.DataTable) DataSource;
                }
                else
                {
                    if (!(DataSource is DataView))
                    {
                        throw new Exception("无法识别的数据源");
                    }
                    DataView view = (DataView) DataSource;
                    table = view.Table.Clone();
                    foreach (DataRowView view2 in view)
                    {
                        DataRow row = view2.Row;
                        DataRow row2 = table.NewRow();
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            row2[i] = row[i];
                        }
                        table.Rows.Add(row2);
                    }
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public object DataSource
        {
            get
            {
                return this.m_DataSource;
            }
            set
            {
                this.m_DataSource = TransDataSourceToTable(value);
            }
        }

        public int RowNum
        {
            get
            {
                return this.m_RowNum;
            }
            set
            {
                this.m_RowNum = value;
            }
        }

        public int RowNumLocal
        {
            get
            {
                return this.m_RowNumLocal;
            }
            set
            {
                this.m_RowNumLocal = value;
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
                DisposeObject(this.m_Sheet);
                this.m_Sheet = value;
            }
        }

        public string TemplateFileName
        {
            get
            {
                return this.m_TemplateFileName;
            }
            set
            {
                this.m_TemplateFileName = value;
                this.m_TemplateFileNamePhy = this.TemplatePathPhy + this.m_TemplateFileName;
                this.SetSaveFile(this.m_TemplateFileName);
            }
        }

        public delegate void GroupDataSourceEventHandler(string GroupFieldValue, System.Data.DataTable GroupDataSource);

        public delegate void GroupDataViewEventHandler(string GroupFieldValue, System.Data.DataTable GroupDataSource, ref DataView GroupDataView);

        public delegate void GroupEventHandler(Worksheet Sheet, int RowIndex, string GroupFieldValue, DataRow drGroup);
    }
}

