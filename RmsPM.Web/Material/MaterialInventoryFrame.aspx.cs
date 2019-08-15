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

public partial class Material_MaterialInventoryList : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!user.HasRight("150101"))
        {
            Response.Redirect("../RejectAccess.aspx");
            Response.End();
        }
    }
}
