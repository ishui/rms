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

public partial class RmsOA_RS_ScoreForEmploy : PageBase
{
    private DateTime dt;
    private string deptName;
    private RS_ScoreManageBFL smBFL = new RS_ScoreManageBFL();
    private RS_EmployScoreBFL esBFL = new RS_EmployScoreBFL();
    private int manageCode;
    private bool hasAudit;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region "原始代码"
        //if (!Page.IsPostBack)
        //{
        //    dt = DateTime.Now;
        //    if (!String.IsNullOrEmpty(Request.QueryString["UnitCode"]))
        //    {
        //        string unitCode = Request.QueryString["UnitCode"];
        //        deptName = smBFL.GetDeptNameByDeptID(unitCode);
        //        this.GridView1.DataSource = smBFL.GetUserByUnitCode(unitCode);
        //        this.GridView1.DataBind();
        //    }
        //}
        #endregion

        #region "修改代码 修改日期 2007-1-9"
        dt = RS_ScoreExtend.CheckMonth;
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["UnitCode"]))
            {
                string unitCode = Request.QueryString["UnitCode"];
                deptName = smBFL.GetDeptNameByDeptID(unitCode);
                RS_ScoreExtend sExtend = new RS_ScoreExtend();
                this.GridView1.DataSource = sExtend.GetUsersByCode(user.UserCode,unitCode);
                this.GridView1.DataBind();
            }
        }
        #endregion
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = e.Row.RowIndex + 1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = index.ToString();
        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        RS_EmployScoreModel es = new RS_EmployScoreModel();
        RS_ScoreManageModel tempSmModel = new RS_ScoreManageModel();
        tempSmModel.DeptCode = Request.QueryString["UnitCode"];
        tempSmModel.Marker = user.UserCode;
        tempSmModel.MarkDate = smBFL.GetMonthFirstDate(DateTime.Now);
        tempSmModel.Type = Int32.Parse(ScoreType.Employ.ToString("d"));
        tempSmModel.Status = WorkFlowStatus.Apply.ToString("d").ToString();
        int tempCode = smBFL.Insert(tempSmModel);
        foreach (GridViewRow gvRow in GridView1.Rows)
        {
            int score = 1;
            es = new RS_EmployScoreModel();
            TextBox tbScore = (TextBox)(gvRow.FindControl("ScoreTextBox"));
            HtmlInputHidden hidUserCode = (HtmlInputHidden)(gvRow.FindControl("HidUserCode"));
            es.UserCode = hidUserCode.Value;
            Int32.TryParse(tbScore.Text, out score);
            es.Score = score;
            es.ManageCode = tempCode;
            esBFL = new RS_EmployScoreBFL();
            esBFL.Insert(es);
        }
        Response.Redirect("RS_ScoreForEmployEdit.aspx?FKCode="+tempCode.ToString()+"&DeptCode="+Request.QueryString["UnitCode"]+"");
    }
}
