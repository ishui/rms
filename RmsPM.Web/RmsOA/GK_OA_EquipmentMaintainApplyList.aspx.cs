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

public partial class RmsOA_GK_OA_EquipmentMaintainApplyList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!user.HasRight("330101"))
        {
            this.NewButton.Visible = false;
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        if (this.DropDownList1.SelectedIndex != 0)
        {
            this.GridViewObjectDataSource.SelectParameters["TypeEqual"].DefaultValue = this.DropDownList1.SelectedItem.Text;
        }
        this.GridViewObjectDataSource.SelectParameters["StateEqual"].DefaultValue = GetListGroupSelectedValues(this.cblStatus);
        this.EquipmentGridView.DataBind();
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
