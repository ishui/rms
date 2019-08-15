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
using RmsOA.MODEL;
using RmsOA.BFL;

public partial class RmsOA_YF_AssetManageList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SetButtonDisplay();
    }

    public void SetButtonDisplay()
    {
        if(!user.HasRight(YF_AssetRight.Add))
        {
            this.NewButton.Visible = false;
        }
    }

    public void SearchButton_Click(object sender,EventArgs e)
    {
        if(this.SortDropDownList.SelectedIndex != 0)
        {
            this.ObjectDataSource1.SelectParameters["SortTypeEqual"].DefaultValue = this.SortDropDownList.SelectedItem.Text;
            this.GridView1.DataBind();
        }
    }
}
