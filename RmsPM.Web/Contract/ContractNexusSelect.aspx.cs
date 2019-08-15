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

public partial class ContractNexusSelect : RmsPM.Web.PageBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Request["NexusCodes"].ToString() + Request["ProjectCode"].ToString() + Request["ContractCode"].ToString());
        HiddenFieldCheckBoxCodes.Value = "";
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridView2.DataBind();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            HtmlInputCheckBox cb = ((HtmlInputCheckBox)e.Row.FindControl("CheckBoxSelect"));
            HiddenFieldCheckBoxCodes.Value += cb.ClientID + ",";
            if (Request["NexusCodes"] != null)
            {
                if (Request["NexusCodes"].ToString().IndexOf(cb.Value + ",Vise") > -1)
                    cb.Checked = true;
            }
        }
    }
}
