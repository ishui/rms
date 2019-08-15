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

using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;


public partial class LinkManage_LinkManageModify : RmsPM.Web.PageBase
{
    private string _LinkManageCode;

    public string LinkManageCode
    {
        get
        {
            if (_LinkManageCode == null)
            {
                if (this.ViewState["_LinkManageCode"] != null)
                    return this.ViewState["_LinkManageCode"].ToString();
                return "";
            }
            return _LinkManageCode;
        }
        set
        {

            this._LinkManageCode = value;
            this.ViewState["_LinkManageCode"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData();
        }

    }

 
    private void LoadData()
    {
        LinkManageCode = Request["LinkManageCode"] + "";
        if (!this.user.HasRight("110701") && !this.user.HasRight("110702") && !this.user.HasRight("110703"))
        {
            Response.Redirect("../RejectAccess.aspx");
            Response.End();
        }

        if (!this.user.HasRight("110701") && !this.user.HasRight("110702"))
        {
            this.btnSave.Visible = false;
        }
        if (!this.user.HasRight("110703"))
        {
            this.btnDelete.Visible = false;
        }




        if (LinkManageCode != "")
        {
            RmsPM.BLL.LinkManage clinkManage = new RmsPM.BLL.LinkManage();
            clinkManage.LinkManageCode = LinkManageCode;
            System.Data.DataTable dtlinkManage = clinkManage.GetLinkManages();
            for (int i = 0; i < dtlinkManage.Rows.Count; i++)
            {
                this.txtSoftwareName.Value = dtlinkManage.Rows[i]["Linkname"].ToString();
                this.TxtLinkUrl.Value = dtlinkManage.Rows[i]["LinkUrl"].ToString();
                this.tdCreateDate.InnerHtml = System.Convert.ToDateTime(dtlinkManage.Rows[i]["CreateDate"]).ToString("yyyy-MM-dd");
            }
        }
        else
        {
            this.btnDelete.Visible = false;
        }
    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (this.txtSoftwareName.Value.Trim() == "")
            {
                Response.Write(JavaScript.Alert(true, "软件名称不允许为空"));
                return;
            }
            if (this.TxtLinkUrl.Value.Trim() == "")
            {
                Response.Write(JavaScript.Alert(true, "链接地址不允许为空"));
                return;
            }

            RmsPM.BLL.LinkManage clinkManage = new RmsPM.BLL.LinkManage();
       
            clinkManage.LinkManageCode = LinkManageCode;
           
            clinkManage.Linkname = this.txtSoftwareName.Value;
            clinkManage.LinkUrl = this.TxtLinkUrl.Value;
            clinkManage.CreateDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            clinkManage.LinkManageAdd();


            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("window.opener.location = window.opener.location;");
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            //			string FromUrl = this.txtFromUrl.Value.Trim();
            //			if (FromUrl != "") 
            //			{
            //				Response.Write(string.Format("window.location = '{0}';", FromUrl));
            //			}
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        catch (Exception ex)
        {
            Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
           
            return;
        }
    }
    protected void btnDelete_ServerClick(object sender, EventArgs e)
    {
        try
        {
            RmsPM.BLL.LinkManage clinkManage=new RmsPM.BLL.LinkManage();
            clinkManage.LinkManageCode = LinkManageCode;
            clinkManage.LinkManageDelete();


            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("window.opener.location = window.opener.location;");
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            //			string FromUrl = this.txtFromUrl.Value.Trim();
            //			if (FromUrl != "") 
            //			{
            //				Response.Write(string.Format("window.location = '{0}';", FromUrl));
            //			}
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        catch (Exception ex)
        {
            Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
          
            return;
        }
    }
}
