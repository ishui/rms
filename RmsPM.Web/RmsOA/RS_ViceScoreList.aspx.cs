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
using RmsOA.BFL;
using RmsOA.MODEL;

public partial class RmsOA_RS_ViceScoreList : PageBase
{
    private RS_ScoreManageBFL smBFL = new RS_ScoreManageBFL();
    private DateTime dt;
    private  List<EmployScoreMode> lsModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lsModel = new List<EmployScoreMode>();
            lsModel = smBFL.GetScores(user.UserCode, ScoreType.Manager, RS_ScoreExtend.CheckMonth);
            this.GridView1.DataSource = lsModel;
            this.GridView1.DataBind();
            if ((lsModel != null) && (lsModel.Count > 0))
            {
                dt = DateTime.Parse(lsModel[0].MarkTime);
                ViewState["Time"] = dt.ToString();
            }
            else
            {
                dt = DateTime.Now;
                ViewState["Time"] = dt.ToString();
            }
        }
        this.InitButtons();
        this.InitNavigator();
    }

    public void InitNavigator()
    {
        if(user.HasRight(ScoreRight.ScoreForEmploy))
        {
            Master.DeptManage = true;
        }
        if (user.HasRight(ScoreRight.ScoreForManager))
        {
           Master.VicePresident = true;
        }
        //为部门打分-总经理
        if (user.HasRight(ScoreRight.ScoreForDept))
        {
            Master.President = true;
        }
        if(user.HasRight(ScoreRight.ViewStat))
        {
           Master.MonthState = true;
        }
    }

    protected void InitButtons()
    {
        bool hasAudit;
        bool hasScored;
        string FKCode;
        FKCode = smBFL.HasManagerScoredORAudit(user.UserCode,out hasScored,out hasAudit).ToString();
        {
            if (hasAudit.Equals(true))
            {
                this.AddButton.Enabled = false;
                this.ModifyButton.Enabled = false;
            }
            else
            {
                if (hasScored.Equals(true))
                {
                    this.AddButton.Enabled = false;
                    this.ModifyButton.Attributes.Add("OnClick", "JavaScript:OpenLargeWindow('RS_ScoreForManagerEdit.aspx?FKCode=" + FKCode + "','ScoreModify')");
                }
                else
                {
                    this.ModifyButton.Enabled = false;
                    this.AddButton.Attributes.Add("OnClick", "JavaScript:OpenLargeWindow('RS_ScoreForManager.aspx','Score')");
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = index.ToString();
        }
    }


    protected void SearchButton_ServerClick(object sender, EventArgs e)
    {
        lsModel = new List<EmployScoreMode>();
        if (String.IsNullOrEmpty(ScorceMonth.Value))
        {
            return;
        }
        else
        {
            dt = DateTime.Parse(ScorceMonth.Value);
            ViewState["Time"] = dt.ToString();
            lsModel = smBFL.GetScores(user.UserCode, ScoreType.Manager, dt);
            this.GridView1.DataSource = lsModel;
            this.GridView1.DataBind();
        }
    }
    public void GetYear()
    {
        Response.Write(DateTime.Parse(ViewState["Time"].ToString()).Year.ToString());
    }

    public void GetMonth()
    {
        Response.Write(DateTime.Parse(ViewState["Time"].ToString()).Month.ToString());
    }
}

