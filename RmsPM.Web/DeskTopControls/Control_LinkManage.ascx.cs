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


public partial class DeskTopControls_Control_LinkManage : RmsPM.Web.Components.BaseControl
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
        
        RmsPM.BLL.LinkManage clinkManage = new RmsPM.BLL.LinkManage();
        System.Data.DataTable dtlinkManage = clinkManage.GetLinkManages();
        this.rpNotice.DataSource = dtlinkManage;
        this.rpNotice.DataBind();
    }
}
