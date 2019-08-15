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

public partial class RmsOA_GK_OA_CapitalAssertAcountList : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!user.HasRight("330301"))
        {
            this.NewButton.Visible = false;
        }
    }
    public void btSearch_Click(object sender, EventArgs e)
    {
        //When Index=0 Text=|--«Î—°‘Ò--|
        if (!this.TypeDropDownList.SelectedIndex.Equals(0))
        {
            this.GridViewObjectDataSource.SelectParameters["Type"].DefaultValue = this.TypeDropDownList.SelectedItem.Text;
        }
        this.GridViewObjectDataSource.SelectParameters["StatusEqual"].DefaultValue = GetListGroupSelectedValues(this.cblStatus);
        this.AccountGridView.DataBind();
    }

    public static string GetListGroupSelectedValues(CheckBoxList cbl)
    {
        string re = "";
        foreach (ListItem li in cbl.Items)
        {
            if (li.Selected)
            {
                if (re != "")
                {
                    re += ",";
                }

                re += li.Value;

            }
        }
        return re;
    }
}

