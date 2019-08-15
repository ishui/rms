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

using RmsDM.BFL;
using RmsDM.MODEL;
using RmsPM.Web;

public partial class RmsDM_FileTemplateTypeList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["NodeValue"] == null)
        {
            Response.Redirect("FileTemplateTypeList.aspx?NodeValue=0&FullID=0&Deep=0");
        }
        if (!user.HasRight("3701"))
        {
            this.btnAdd.Visible = false;
        }

    }
}

