namespace RmsReport
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using Word;

    public class TWord
    {
        public DataRow DataSource;
        public Document m_Doc;
        public static object m_Opt = Type.Missing.ToString();
        private string m_RootDir;
        private string m_TemplateFileName = "";
        private string m_TemplateFileNamePhy;
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
        public string TemplatePathPhy;
        public Application word;

        public TWord(HttpResponse a_Response, HttpRequest a_Request, HttpServerUtility a_Server, HttpSessionState a_Session)
        {
            this.Response = a_Response;
            this.Request = a_Request;
            this.Server = a_Server;
            this.Session = a_Session;
            this.InitDir();
            this.word = CreateWord();
        }

        public void AddDocument()
        {
            if (this.m_TemplateFileNamePhy == "")
            {
                this.Doc = this.word.Documents.Add(ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt);
            }
            else
            {
                this.Doc = AddDocument(this.word, this.m_TemplateFileNamePhy);
            }
        }

        public static Document AddDocument(Application word, string TemplateNamePhy)
        {
            object template = TemplateNamePhy;
            return word.Documents.Add(ref template, ref m_Opt, ref m_Opt, ref m_Opt);
        }

        public void ClientOpen()
        {
            string s = "<script language='javascript'>\nvar word = new ActiveXObject('Word.Application');\nword.Visible = true;\nword.Documents.Open('" + this.SaveFileNameHttpFull + "');\n</script>\n";
            this.Response.Write(s);
        }

        public void ClientPreview()
        {
            string s = "<script language='javascript'>\nvar word = new ActiveXObject('Word.Application');\nword.Visible = true;\nword.Documents.Open('" + this.SaveFileNameHttpFull + "');\nword.ActiveDocument.PrintPreview();\n</script>\n";
            this.Response.Write(s);
        }

        public void ClientPrint()
        {
            string s = "<script language='javascript'>\nvar word = new ActiveXObject('Word.Application');\nword.Visible = true;\nword.Documents.Open('" + this.SaveFileNameHttpFull + "');\nword.ActiveDocument.PrintOut(false);\nword.Quit();\n</script>\n";
            this.Response.Write(s);
        }

        public static Application CreateWord()
        {
            Application application2;
            try
            {
                Application application = new ApplicationClass();
                application.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                application2 = application;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return application2;
        }

        public void DataToDoc()
        {
            try
            {
                if (this.Doc != null)
                {
                    DataToDoc(this.DataSource, this.Doc);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataToDoc(DataRow dr, Document Doc)
        {
            try
            {
                int count = Doc.FormFields.Count;
                for (int i = 1; i <= count; i++)
                {
                    object index = i;
                    FormField field = Doc.FormFields.Item(ref index);
                    string columnName = field.Name;
                    string text2 = "";
                    if (columnName.ToUpper().StartsWith("_SYS_"))
                    {
                        columnName = columnName.ToUpper();
                        string text3 = columnName.Substring(5, columnName.Length - 5);
                        switch (text3.ToLower())
                        {
                            case "year":
                                text2 = DateTime.Today.Year.ToString();
                                goto Label_0132;

                            case "month":
                                text2 = DateTime.Today.Month.ToString();
                                goto Label_0132;

                            case "day":
                                text2 = DateTime.Today.Day.ToString();
                                break;

                            case "today":
                                text2 = DateTime.Today.ToString("yyyy-MM-dd");
                                break;
                        }
                    }
                    else
                    {
                        text2 = GetItemValue(dr, columnName, false);
                    }
                Label_0132:
                    field.Result = text2;
                    DisposeObject(field);
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
                this.DisposeWord();
            }
            catch
            {
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

        public void DisposeWord()
        {
            try
            {
                try
                {
                    this.word.Documents.Close(ref m_Opt, ref m_Opt, ref m_Opt);
                }
                catch
                {
                }
                this.word.Quit(ref m_Opt, ref m_Opt, ref m_Opt);
                DisposeObject(this.Doc);
                DisposeObject(this.word);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static FormField GetFormField(Document Doc, string name)
        {
            FormField field3;
            try
            {
                FormField field = null;
                int count = Doc.FormFields.Count;
                for (int i = 1; i <= count; i++)
                {
                    object index = i;
                    FormField field2 = Doc.FormFields.Item(ref index);
                    if (name == field2.Name)
                    {
                        return field2;
                    }
                }
                field3 = field;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return field3;
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
                    DataTable table = dr.Table;
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
                    DataTable table = dr.Table;
                    if (((table != null) && table.Columns.Contains(columnName)) && (IsNum(table.Columns[columnName].DataType) && (double.Parse(s) == 0)))
                    {
                        s = "";
                    }
                }
            }
            return s;
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

        private static bool IsNum(Type type)
        {
            return (((type.ToString() == "System.Decimal") || (type.ToString() == "System.Double")) || (((type.ToString() == "System.Int16") || (type.ToString() == "System.Int32")) || (type.ToString() == "System.Int64")));
        }

        public void SaveDocument()
        {
            object saveFileNamePhy = this.SaveFileNamePhy;
            this.Doc.SaveAs(ref saveFileNamePhy, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt, ref m_Opt);
            this.Doc.Close(ref m_Opt, ref m_Opt, ref m_Opt);
        }

        public void SetFieldValue(string name, object val)
        {
            try
            {
                SetFormFieldValue(this.Doc, name, val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetFormFieldValue(Document Doc, string name, object val)
        {
            try
            {
                FormField formField = GetFormField(Doc, name);
                if (formField != null)
                {
                    formField.Result = val.ToString();
                }
                DisposeObject(formField);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ShowClient()
        {
            this.Response.Write("<script language='javascript'>");
            this.Response.Write("window.open('" + this.SaveFileNameHttp + "');");
            this.Response.Write("</script>");
        }

        public Document Doc
        {
            get
            {
                return this.m_Doc;
            }
            set
            {
                DisposeObject(this.m_Doc);
                this.m_Doc = value;
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
                this.SaveFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".doc";
                this.SaveFileNamePhy = this.SavePathPhy + this.SaveFileName;
                this.SaveFileNameHttp = this.SavePathHttp + HttpUtility.UrlEncode(this.SaveFileName, Encoding.Default);
                this.SaveFileNameHttpFull = this.SavePathHttpFull + HttpUtility.UrlEncode(this.SaveFileName, Encoding.Default);
            }
        }
    }
}

