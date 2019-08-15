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

public partial class LinkManage_LinkManageInfo : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData();
        }
    }

    
    public void LoadData()
    {
        if (!this.user.HasRight("110704") && !this.user.HasRight("110701"))
        {

            Response.Redirect("../RejectAccess.aspx");
            Response.End();

        }
        this.btnAdd.Visible = this.user.HasRight("110701");
        RmsPM.BLL.LinkManage clinkManage = new RmsPM.BLL.LinkManage();
        System.Data.DataTable dtlinkManage = clinkManage.GetLinkManages();
        this.dgList.DataSource = dtlinkManage;
        this.dgList.DataBind();
    }
}
