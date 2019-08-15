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

public partial class RmsOA_GK_OA_MaterialTransferList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        if (this.DropDownList1.SelectedIndex != 0)
        {
            this.GridViewObjectDataSource.SelectParameters["TypeEqual"].DefaultValue = this.DropDownList1.SelectedItem.Text;
        }
        this.TransferGridView.DataBind();
    }
}
