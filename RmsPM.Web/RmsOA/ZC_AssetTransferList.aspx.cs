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

public partial class RmsOA_ZC_AssetTransferlist : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!user.HasRight("330201"))
        {
            this.NewButton.Visible = false;
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (this.SortDropDownList.SelectedIndex != 0)
        {
            this.ObjectDataSource1.SelectParameters["SortEqual"].DefaultValue = this.SortDropDownList.SelectedItem.Text;
        }
        this.ObjectDataSource1.SelectParameters["StatusEqual"].DefaultValue = GetListGroupSelectedValues(this.cblStatus);
        this.GridView1.DataBind();
    }

    /// <summary>
    /// 获取选中的状态

    /// </summary>
    /// <param name="cbl"></param>
    /// <returns></returns>
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
