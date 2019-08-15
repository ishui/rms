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

public partial class RmsOA_RS_ScoreForManagerEdit : PageBase
{
    DateTime dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        dt = RmsOA.BFL.RS_ScoreExtend.CheckMonth;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
    }

    protected void GetYear()
    {
        Response.Write(dt.Year);
    }

    protected void GetMonth()
    {
        Response.Write(dt.Month);
    }
}
