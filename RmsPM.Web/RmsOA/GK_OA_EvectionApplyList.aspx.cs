/***************************************************************
 * @Author: Yiwl
 * @CreateDate: 2006-11-15
 ***************************************************************/
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

using RmsOA.MODEL;
using RmsOA.BFL;
using RmsPM.Web;

/// <summary>
/// ≥ˆ≤Ó…Í«Î¡–±Ì
/// </summary>
public partial class RmsOA_GK_OA_EvectionApply : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!user.HasRight("320601"))
        {
            this.NewButton.Visible = false;
        }

    }

    public void btSearch_Click(object sender, EventArgs e)
    {
        this.GridViewObjectDataSource.SelectParameters["StatusEqual"].DefaultValue = GetListGroupSelectedValues(this.cblStatus);
        this.EvectionGridView.DataBind();
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
