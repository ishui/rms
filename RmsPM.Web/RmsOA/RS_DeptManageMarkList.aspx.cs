using System;
using System.Data;
using System.Text;
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

public partial class RmsOA_RS_DeptManageMark : PageBase
{
    private RS_ScoreManageBFL smBFL = new RS_ScoreManageBFL();
    private List<EmployScoreMode> lsModel = new List<EmployScoreMode>();
    private int count;
    private DateTime dt;
    private string deptName;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.AddButton.Attributes.Add("OnClick", "JavaScript:OpenSmallWindow('SelectScoreDept.aspx?Type=Add&UserCode="+user.UserCode+"','RecordAdd')");
            this.ModifyButton.Attributes.Add("OnClick", "JavaScript:OpenSmallWindow('SelectScoreDept.aspx?Type=Modify&UserCode=" + user.UserCode + "','RecordModify')");
            this.InitDeptDropDownList(user.UserCode);
            string deptCode = this.DeptDropDownList.SelectedValue;
            lsModel = smBFL.GetEmployScore(deptCode);
            if (lsModel != null)
            {
                count = lsModel.Count;
                this.GridView1.DataSource = lsModel;
                this.GridView1.DataBind();
                if (lsModel != null && lsModel.Count != 0)
                {
                    dt = DateTime.Parse(lsModel[0].MarkTime);
                    ViewState["Time"] = dt.ToString();
                    deptName = lsModel[0].DeptName;
                }
            }
            else
            {
                dt = DateTime.Now;
                ViewState["Time"] = dt.ToString();
            }
        }
        this.InitButtonAndOtherVisble();
    }


    public void InitButtonAndOtherVisble()
    {

        if(!user.HasRight(ScoreRight.ScoreForEmploy))
        {
            //为部门经理打分-副总经理
            if (user.HasRight(ScoreRight.ScoreForManager))
            {
               Response.Redirect("RS_ViceScoreList.aspx");
            }
            //为部门打分-总经理
            if (user.HasRight(ScoreRight.ScoreForDept))
            {
                Response.Redirect("RS_ViceScoreList.aspx");
            }
            if(user.HasRight(ScoreRight.ViewStat))
            {
                Response.Redirect("RS_AllScoreList.aspx");
            }
        }
        else
        {
            //为部门员工打分-部门经理
            this.AddButton.Enabled = true;
            this.ModifyButton.Enabled = true;
            Master.DeptManage =true;
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
    }
    /// <summary>
    /// 根据用户ID获取用户可以管理的部门
    /// </summary>
    /// <param name="userID">用户ID(编号)</param>
    protected void InitDeptDropDownList(string userID)
    {
        RS_ScoreExtend sExtend = new RS_ScoreExtend();
        this.DeptDropDownList.DataSource = sExtend.GetUnitByUserCode(userID);
        this.DeptDropDownList.DataTextField = "UnitName";
        this.DeptDropDownList.DataValueField = "UnitCode";
        this.DeptDropDownList.DataBind();
    }

    /// <summary>
    /// 搜索按钮事件响应
    /// </summary>
    public void Search_ServerClick(object sender, EventArgs e)
    {
        string deptCode = this.DeptDropDownList.SelectedValue;
        DateTime dtSearch = DateTime.Now;
        if (ScorceMonth.Value != "")
        {
            dtSearch = DateTime.Parse(ScorceMonth.Value);
        }
        lsModel = new List<EmployScoreMode>();
        lsModel = smBFL.GetEmployScore(deptCode,dtSearch,SearchType.Search);
        if (lsModel != null && lsModel.Count != 0)
        {
            count = lsModel.Count;
            dt = DateTime.Parse(lsModel[0].MarkTime);
             ViewState["Time"] = dt.ToString();
        }
        else
        {
            dt = dtSearch;
             ViewState["Time"] = dt.ToString();
        }
        this.GridView1.DataSource = lsModel;
        this.GridView1.DataBind();
        deptName = this.DeptDropDownList.SelectedItem.Text;
    }

    public string ChangeStrings(string str)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            sb.Append(str.Substring(i,1));
            sb.Append("<br/>");
        }
        return sb.ToString();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "序号";
            e.Row.Cells[1].Text = "部门";
            e.Row.Cells[2].Text = "姓名";
            e.Row.Cells[3].Text = "考核分数";
            e.Row.Cells[4].Text = "考核人";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = index.ToString();
            e.Row.Cells[2].Text = this.lsModel[e.Row.RowIndex].UserName;
            e.Row.Cells[3].Text = this.lsModel[e.Row.RowIndex].Score;
            if (this.count > 1)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[1].RowSpan = this.count;
                    e.Row.Cells[1].Text = this.ChangeStrings(this.lsModel[e.Row.RowIndex].DeptName);
                    e.Row.Cells[4].RowSpan = this.count;
                    e.Row.Cells[4].Text = this.ChangeStrings(this.lsModel[e.Row.RowIndex].Marker);
                }
                else
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
            }
        }
    }

    #region "标题显示"
    public void GetDept()
    {
        Response.Write(deptName);
    }
    public void GetYear()
    {
        Response.Write(DateTime.Parse(ViewState["Time"].ToString()).Year);
    }
    public void GetMonth()
    {
        Response.Write(DateTime.Parse(ViewState["Time"].ToString()).Month);
    }
    #endregion
}
