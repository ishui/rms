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

public partial class PersonalManage_ZS_PersonList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (user.HasRight("340101"))
            {
                this.NewButton.Visible = true;
                this.NewButton.Attributes["OnClick"] = "javascript:OpenMiddleWindow('ZS_PersonEdit.aspx?ActType=add','PersonEdit');";
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

    protected void NewButton_ServerClick(object sender, EventArgs e)
    {

    }
}
