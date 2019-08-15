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
using System.Collections.Generic;
using System.Text;

using RmsOA.MODEL;
using RmsOA.BFL;
using RmsPM.Web;
using Rms.ORMap;

public partial class RmsOA_GR_WorkLog : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!user.HasRight("350201"))
        {
            this.NewButton.Visible = false;
        }
        GK_OA_WorkLogQueryModel wlQueryModel = new GK_OA_WorkLogQueryModel();
        GK_OA_WorkLogBFL wlBFL = new GK_OA_WorkLogBFL();
        wlQueryModel.UserIdEqual = user.UserID;
        this.GridView1.DataSource = wlBFL.ChangeWorkLogModel(wlQueryModel);
        this.GridView1.DataBind();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        GK_OA_WorkLogQueryModel wlQueryModel = new GK_OA_WorkLogQueryModel();
        GK_OA_WorkLogBFL wlBFL = new GK_OA_WorkLogBFL();
        wlQueryModel.QueryConditionStr = this._ConstructQueryString();
        this.GridView1.DataSource = wlBFL.ChangeWorkLogModel(wlQueryModel);
        this.GridView1.DataBind();
    }

    private string _ConstructQueryString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("WHERE UserId = '" + user.UserID + "' AND ");
        if (!this.TextBoxContext.Text.Trim().Equals(String.Empty))
        {
            sb.Append("Context LIKE '%" + this.TextBoxContext.Text + "%' AND ");
        }
        if(!this.dateBegin.Value.Equals(String.Empty))
        {
            sb.Append("DayWrited >= '"+ this.dateBegin.Value +"' AND ");
        }
        if (!this.dateEnd.Value.Equals(String.Empty))
        {
            sb.Append("DayWrited <= '" + this.dateEnd.Value + "' AND ");
        }
        sb.Append(" 1=1 ");
        return sb.ToString();
    }
}
