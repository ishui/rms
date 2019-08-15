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
using RmsPM.Web;

public partial class RmsOA_GK_OA_OutFileRegisterList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (user.HasRight("310101"))
            {
                this.NewButton.Visible = true;
                this.NewButton.Attributes["OnClick"] = "javascript:OpenMiddleWindow('GK_OA_OutFileRegisterEdit.aspx?ActType=add','OutFileRegisterEdit');";
            }
            else
            {
                this.NewButton.Visible = false;
            }
        }
    }
    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }
}
