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
using Codefresh.PhotoBrowserLibrary;
using System.IO;


public partial class albumEntrance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["photo_CurrentDirectory"]=null;
        Session["photo_DirectoryLookup"]=null;
        Session["photo_DirectoryBrowser"]=null;
        Session["photo_rootpath"]=null;
        Session["photo_CurrentPageNumber"]=null;
        Session["photo_CurrentPhotos"] = null;
        if (!IsPostBack)
        {
            string projcode = Context.Request.Params["ProjectCode"];
//            string photosPath = Context.Request.FilePath + Path.DirectorySeparatorChar;
            string mappedPhotosDbPath = Context.Request.MapPath("") + "\\photos";
            DirectoryBrowser dirBrowser = new DirectoryBrowser(mappedPhotosDbPath, "");
            if (projcode != null)
            {
                string rootpath = dirBrowser.GetRootPath(projcode);
                if (rootpath != null)
                {
                    EnterAlbum(rootpath);
                }
            }
        }
    }

    private void EnterAlbum(string rootpath)
    {
        Session["photo_rootpath"] = rootpath;
        Response.Redirect("album.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string projcode = Context.Request.Params["ProjectCode"];
//        string photosPath = Context.Request.FilePath + Path.DirectorySeparatorChar;
        string mappedPhotosDbPath = Context.Request.MapPath("") + "\\photos";
        DirectoryBrowser dirBrowser = new DirectoryBrowser(mappedPhotosDbPath, "");
        if (dirBrowser.RootPathExist(TextBox1.Text.Trim()))
        {
            Label1.Text = "目录名已存在，请换个名字输入";
        }
        else
        {
            if (dirBrowser.CreateRootDirectorie(projcode, TextBox1.Text.Trim()))
            {
                EnterAlbum(TextBox1.Text.Trim());
            }
            else
            {
                Label1.Text = "创建目录失败，请重试或通知管理员";
            }

        }
    }
}
