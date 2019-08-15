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

using RmsDM.MODEL;
using RmsDM.BFL;
using RmsPM.Web;

public partial class RmsDM_FileTemplateList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["NodeValue"]))
            {
                if (Request.QueryString["NodeValue"].Equals("0"))
                {
                    this.btAdd.Visible = false;
                }
                else
                {
                    if (!user.HasRight("380201"))
                    {
                        this.btAdd.Visible = false;
                    }
                    this.btAdd.Attributes["OnClick"] = "javascript:OpenLargeWindow('FileTemplateAdd.aspx?NodeValue=" + Request.QueryString["NodeValue"] + "','FileTemplateAdd');";
                }
            }
        }
    }
}
