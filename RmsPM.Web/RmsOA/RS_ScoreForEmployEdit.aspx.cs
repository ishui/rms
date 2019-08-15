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

using RmsPM.Web;
using RmsOA.MODEL;
using RmsOA.BFL;

public partial class RmsOA_RS_ScoreForEmployEdit : System.Web.UI.Page
{
    private DateTime dt;
    private string deptName;
    private RS_ScoreManageBFL smBFL = new RS_ScoreManageBFL();
    private List<EmployViewModel> listModel = new List<EmployViewModel>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string manageCode = Request.QueryString["FKCode"];
        string deptCode = Request.QueryString["UnitCode"];
        dt = DateTime.Now;
        if (!String.IsNullOrEmpty(deptCode))
        {
            deptName = smBFL.GetDeptNameByDeptID(deptCode);
        }

    }

    public void GetDept()
    {
        Response.Write(deptName);
    }

    public void GetYear()
    {
        Response.Write(dt.Year.ToString());
    }

    public void GetMonth()
    {
        Response.Write(dt.Month.ToString());
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
