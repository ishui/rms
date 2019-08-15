using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsPM.BLL;
using RmsPM.Web;
using System.IO;
using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;

public partial class Document_AttachmentConvert : PageBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["attachmentconvertType"] == "data2hd")
            {
                if (Session["attachmentconvertsize"] != null)
                {
                    convertData2HD((int)Session["attachmentconvertsize"]);
                }
            }
            else if(Session["attachmentconvertType"]=="hd2data")
            {
                if (Session["attachmentconvertsize"] != null)
                {
                    convertHD2Data((int)Session["attachmentconvertsize"]);
                }
            }
            else if (Session["attachmentconvertType"] == "hd2hd")
            {
                if (Session["attachmentconvertsize"] != null)
                {
                    convertHD2HD((int)Session["attachmentconvertsize"]);
                }
            }

            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["attachmentconvertType"] = "data2hd";
        int size = 0;
        int.TryParse(TextBox1.Text, out size);
        if (size < 1) size = 20;
        Session["attachmentconvertsize"] = size;
        convertData2HD((int)Session["attachmentconvertsize"]);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["attachmentconvertType"] = "hd2data";
        int size = 0;
        int.TryParse(TextBox1.Text, out size);
        if (size < 1) size = 20;
        Session["attachmentconvertsize"] = size;
        Session["attachmentconvertbegincode"] = 0;
        convertHD2Data((int)Session["attachmentconvertsize"]);
    }
    private void convertData2HD(int size)
    {
        try
        {
            RmsPM.BLL.DocumentRule documentRule = DocumentRule.Instance();
            LogHelper.WriteLog("TestLog", new Exception("TestLog"));
            int i = documentRule.ConvertAttachmentToFile(size);
            if (Session["attachmentconvertcount"] == null) Session["attachmentconvertcount"] = 0;
            Session["attachmentconvertcount"] = (int)Session["attachmentconvertcount"] + i;
            if (i > 0)
            {
                Session["attachmentconvertmsg"] = "ת������ȴ�����ת��" + Session["attachmentconvertcount"].ToString() + "������......";
                Label1.Text = Session["attachmentconvertmsg"].ToString();
                TextBox1.Text = Session["attachmentconvertsize"].ToString();
                Response.Write(Rms.Web.JavaScript.Reload(true));
            }
            else
            {

                Label1.Text = "ת����ɣ���ת��" + Session["attachmentconvertcount"].ToString() + "������.";
                Session["attachmentconvertsize"] = null;
                Session["attachmentconvertmsg"] = null;
                Session["attachmentconvertcount"] = null;
                Session["attachmentconvertbegincode"] = null;
                //Response.Write(Rms.Web.JavaScript.Alert(true, "ok"));
            }
        }
        catch (Exception exp)
        {
            Session["attachmentconvertsize"] = null;
            Session["attachmentconvertmsg"] = null;
            Session["attachmentconvertcount"] = null;
            Session["attachmentconvertbegincode"] = null;
            Label1.Text = Label1.Text + " ---- ת���з������󣬴�����Ϣ��<br>" + exp.ToString();
        }

        
    }
    private void convertHD2Data(int size)
    {
        try
        {
            int begincode = 0; ;
            if (Session["attachmentconvertbegincode"] != null)
            {
                begincode = int.Parse(Session["attachmentconvertbegincode"].ToString());
            }
            RmsPM.BLL.DocumentRule documentRule = DocumentRule.Instance();
            int i = documentRule.ConvertFileToAttachment(size, ref begincode);
            if (Session["attachmentconvertcount"] == null) {Session["attachmentconvertcount"] = 0;}
            Session["attachmentconvertcount"]=int.Parse(Session["attachmentconvertcount"].ToString())+i;
            Session["attachmentconvertbegincode"] = begincode;
            if (i > 0 )
            {
                Session["attachmentconvertmsg"] = "ת������ȴ�����ת��" + Session["attachmentconvertcount"].ToString() + "������......";
                Label1.Text = Session["attachmentconvertmsg"].ToString();
                TextBox1.Text = Session["attachmentconvertsize"].ToString();
                Response.Write(Rms.Web.JavaScript.Reload(true));
            }
            else
            {

                Label1.Text = "ת����ɣ���ת��" + Session["attachmentconvertcount"].ToString() + "������.";
                Session["attachmentconvertsize"] = null;
                Session["attachmentconvertmsg"] = null;
                Session["attachmentconvertcount"] = null;
                Session["attachmentconvertbegincode"] = null;
                //Response.Write(Rms.Web.JavaScript.Alert(true, "ok"));
            }
        }
        catch (Exception exp)
        {
            Session["attachmentconvertsize"] = null;
            Session["attachmentconvertmsg"] = null;
            Session["attachmentconvertcount"] = null;
            Session["attachmentconvertbegincode"] = null;
            Label1.Text = Label1.Text + " ---- ת���з������󣬴�����Ϣ��<br>" + exp.ToString();
        }


    }
    private void convertHD2HD(int size)
    {
        int begincode = 0; ;
        if (Session["attachmentconvertbegincode"] != null)
        {
            begincode = int.Parse(Session["attachmentconvertbegincode"].ToString());
        }
        RmsPM.BLL.DocumentRule documentRule = DocumentRule.Instance();
        init();
        if (_SavePathMode == AttachmentSavePathMode.ROOT)
        {
            Label1.Text = "·��ѡ�����ò���ȷ���޷�ת��";
            return;
        }
        EntityData entity = new EntityData();
        using (SingleEntityDAO dao = new SingleEntityDAO("AttachMent"))
        {
            dao.FillEntity("select top "+size.ToString()+" * from attachment where content is  null and guidname is not null and attachmentcode>"+begincode+" order by attachmentcode", "", "", entity, "AttachMent");
        }
        DataTable dt = entity.CurrentTable;
        int count = 0;
        if (dt.Rows.Count > 0)
        {
            Rms.LogHelper.LogHelper.Warn("����Ŀ¼ת����ʼ code>"+begincode.ToString());
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DirectoryInfo dir = new DirectoryInfo(GetPath(dr["createdate"].ToString()));
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    FileInfo file = new FileInfo(Path.Combine(_Path, dr["guidname"].ToString()));
                    if (file.Exists)
                    {
                        file.MoveTo(GetFileName(file.Name, dr["createdate"].ToString()));
                    }
                    else
                    {
                        Rms.LogHelper.LogHelper.Warn("�ļ�δ�ҵ�" + file.Name);
                    }
                    if (int.Parse(dr["attachmentcode"].ToString()) > begincode)
                    {
                        begincode = int.Parse(dr["attachmentcode"].ToString());
                    }

                }
                Session["attachmentconvertbegincode"] = begincode;
                Session["attachmentconvertmsg"] = "ת������ȴ�";               
                Response.Write(Rms.Web.JavaScript.Reload(true)); 
            }
            catch (Exception exp)
            {
                Rms.LogHelper.LogHelper.Warn("ת������", exp);
                Label1.Text = "ת���쳣��ֹ����鿴������־";
            }
        }
        else
        {
            Label1.Text = "ת�����;";
            Session["attachmentconvertsize"] = null;
            Session["attachmentconvertmsg"] = null;
            Session["attachmentconvertcount"] = null;
            Session["attachmentconvertbegincode"] = null;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Session["attachmentconvertType"] = "hd2hd";
        int size = 0;
        int.TryParse(TextBox1.Text, out size);
        if (size < 1) size = 20;
        Session["attachmentconvertsize"] = size;
        Session["attachmentconvertbegincode"] = 0;
        convertHD2HD((int)Session["attachmentconvertsize"]);
        

        }
        #region ���¿�����documentrule.cs �뱣��һ��
        private AttachmentSaveMode _SaveMode;
        public AttachmentSavePathMode _SavePathMode;
        private string _Path;    
    public void init()
        {
            string mode = System.Configuration.ConfigurationSettings.AppSettings["AttachMentSaveMode"];
                if (mode != null && mode.ToLower() == "file")
                {
                    _SaveMode = AttachmentSaveMode.file;                   
                        _Path = System.Configuration.ConfigurationSettings.AppSettings["AttachMentSavePath"];
                        if (_Path == null)
                        {
                            throw (new Exception("δ�����ĵ��洢Ŀ¼"));
                        }
                        try
                        {
                            if (!Directory.Exists(_Path))
                            {
                                Directory.CreateDirectory(_Path);
                            }
                        }
                        catch
                        {
                            throw (new Exception("���ܴ����ĵ��洢Ŀ¼"));
                        }
                    
                }else { _SaveMode = AttachmentSaveMode.database; }
           
                string pathmode = System.Configuration.ConfigurationSettings.AppSettings["AttachMentSavePathMode"];
                try
                {
                    _SavePathMode = (AttachmentSavePathMode)System.Enum.Parse(typeof(AttachmentSavePathMode), pathmode.ToUpper());
                }
                catch { _SavePathMode = AttachmentSavePathMode.ROOT; }
        }
        private  string GetPath(string date)
    {
        string fullname;
        switch (_SavePathMode)
        {
            case AttachmentSavePathMode.ROOT:
                fullname = _Path;
                break;
            case AttachmentSavePathMode.YYYY:
                fullname = Path.Combine(_Path, GetYearString(date));
                break;
            case AttachmentSavePathMode.YYYYMM:
                fullname = Path.Combine(_Path, GetYearMonthString(date));
                break;
            case AttachmentSavePathMode.YYYYMMDD:
                fullname = Path.Combine(_Path, GetYearMonthDateString(date));
                break;
            default:
                fullname = _Path;
                break;
        }
        return fullname;
    }
        private string GetFileName(string filename, string date)
        {
            return Path.Combine(GetPath(date), filename);
        }
        private  string GetYearString(string date)
        {
            DateTime dt = new DateTime();
            if (date == string.Empty) return string.Empty;
            if (DateTime.TryParse(date, out dt))
            {
                return dt.Year.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        private  string GetYearMonthString(string date)
        {
            DateTime dt = new DateTime();
            if (date == string.Empty) return string.Empty;
            if (DateTime.TryParse(date, out dt))
            {
                return dt.Year.ToString() + dt.Month.ToString().PadLeft(2, '0');
            }
            else
            {
                return string.Empty;
            }
        }
        private  string GetYearMonthDateString(string date)
        {
            DateTime dt = new DateTime();
            if (date == string.Empty) return string.Empty;
            if (DateTime.TryParse(date, out dt))
            {
                return dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString().PadLeft(2, '0');
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
}
