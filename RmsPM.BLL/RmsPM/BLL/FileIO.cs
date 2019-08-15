namespace RmsPM.BLL
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;

    public class FileIO
    {
        private string m_RootDir;
        private string m_SaveFileName = "";
        private string m_SaveFileNamePhy;
        private HttpRequest Request;
        private HttpResponse Response;
        private string SaveFileNameHttp;
        private string SaveFileNameHttpFull;
        private string SavePathHttp;
        private string SavePathHttpFull;
        private string SavePathPhy;
        private HttpServerUtility Server;
        private HttpSessionState Session;
        private string TemplatePathPhy;

        public FileIO(HttpResponse a_Response, HttpRequest a_Request, HttpServerUtility a_Server, HttpSessionState a_Session)
        {
            this.Response = a_Response;
            this.Request = a_Request;
            this.Server = a_Server;
            this.Session = a_Session;
            this.SaveFileName = "";
            this.InitDir();
        }

        public static void CreateDir(string PathPhy)
        {
            try
            {
                if (!Directory.Exists(PathPhy))
                {
                    Directory.CreateDirectory(PathPhy);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string FormatCsvCell(string val)
        {
            string text2;
            try
            {
                string text = val;
                if (val != "")
                {
                    text = "'" + text;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private void InitDir()
        {
            try
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
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SetSaveFile(string val)
        {
            if (val == "")
            {
                this.m_SaveFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            }
            else
            {
                this.m_SaveFileName = val;
            }
            this.m_SaveFileNamePhy = this.SavePathPhy + this.SaveFileName;
            this.SaveFileNameHttp = this.SavePathHttp + HttpUtility.UrlEncode(this.SaveFileName, Encoding.Default);
            this.SaveFileNameHttpFull = this.SavePathHttpFull + HttpUtility.UrlEncode(this.SaveFileName, Encoding.Default);
        }

        public void ShowClient()
        {
            this.Response.Write("<script language='javascript'>");
            this.Response.Write("window.open('" + this.SaveFileNameHttp + "');");
            this.Response.Write("</script>");
        }

        public string SaveFileName
        {
            get
            {
                return this.m_SaveFileName;
            }
            set
            {
                this.SetSaveFile(value);
            }
        }

        public string SaveFileNamePhy
        {
            get
            {
                return this.m_SaveFileNamePhy;
            }
        }
    }
}

