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
using RmsOA.BFL;
using RmsOA.MODEL;

public partial class RmsOA_RS_ScoreForManager : PageBase
{
    private DateTime dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        dt = RS_ScoreExtend.CheckMonth;
        this.ObjectDataSource1.SelectParameters["UserID"].DefaultValue = user.UserCode;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = index.ToString();
        }
    }

    public void GetYear()
    {
        Response.Write(dt.Year);
    }

    public void GetMonth()
    {
        Response.Write(dt.Month);
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        RS_ScoreManageBFL smBFL = new RS_ScoreManageBFL();
        RS_EmployScoreBFL esBFL = new RS_EmployScoreBFL();
        RS_EmployScoreModel es = new RS_EmployScoreModel();
        RS_ScoreManageModel tempSmModel = new RS_ScoreManageModel();
        //tempSmModel.DeptCode = Request.QueryString["UnitCode"];
        tempSmModel.Marker = user.UserCode;
        //tempSmModel.MarkDate = smBFL.GetMonthFirstDate(DateTime.Now);
        tempSmModel.MarkDate = dt;
        tempSmModel.Type = Int32.Parse(ScoreType.Manager.ToString("d"));
        tempSmModel.Status = WorkFlowStatus.Apply.ToString("d").ToString();
        int tempCode = smBFL.Insert(tempSmModel);
        foreach (GridViewRow gvRow in GridView1.Rows)
        {
            int score = -1;
            es = new RS_EmployScoreModel();
            TextBox tbScore = (TextBox)(gvRow.FindControl("ScoreTextBox"));
            //HtmlInputHidden hidUserCode = (HtmlInputHidden)(gvRow.FindControl("HidUserCode"));
            //es.UserCode = hidUserCode.Value;
            es.UserCode = gvRow.Cells[1].Text;
            Int32.TryParse(tbScore.Text, out score);
            es.Score = score;
            es.ManageCode = tempCode;
            esBFL = new RS_EmployScoreBFL();
            esBFL.Insert(es);
        }
        Response.Redirect("RS_ScoreForManagerEdit.aspx?FKCode=" + tempCode.ToString() + "&UserCode="+user.UserCode+"");
    }
}
