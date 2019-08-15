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
using RmsOA.BFL;
using RmsOA.MODEL;
using System.Collections.Generic;
using RmsPM.Web;

public partial class RmsOA_SelectScoreDept : PageBase
{
    private RS_ScoreManageBFL smBFL = new RS_ScoreManageBFL();
    private List<UnitModel> lsUnitModel = new List<UnitModel>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["UserCode"]))
            {
                string userCode = Request.QueryString["UserCode"];
                if (!user.UserCode.Equals(userCode))
                {
                    this.ClosePage();
                }
                RS_ScoreExtend sExtend = new RS_ScoreExtend();
                lsUnitModel = sExtend.GetUnitByUserCode(user.UserCode);
                this.DeptRadioButtonList.DataSource = lsUnitModel;
                this.DeptRadioButtonList.DataTextField = "UnitName";
                this.DeptRadioButtonList.DataValueField = "UnitCode";
                this.DataBind();
                this.DeptRadioButtonList.SelectedIndex = 0;
            }
        }
    }
    public void ScoreButton_Click(object sender, EventArgs e)
    {
        bool hasAudit;
        string unitCode = this.DeptRadioButtonList.SelectedValue;
        RS_ScoreManageBFL smBFL = new RS_ScoreManageBFL();
        int manageCode = smBFL.HasScoreORAudit(unitCode, out hasAudit);
        if (manageCode == -1)
        {
            Response.Write("<script>window.open('RS_ScoreForEmploy.aspx?UnitCode=" + unitCode + "','newwindow','width=800,height=600,top=100,left=100,menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no');</script>");
            return;
        }
        if (hasAudit == false)
        {
            Response.Write("<script>window.open('RS_ScoreForEmployEdit.aspx?FKCode=" + manageCode + "&UnitCode=" + unitCode + "','newwindow','width=800,height=600,top=100,left=100,menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no');</script>");
        }
        else
        {
            Response.Write("<script>window.alert('" + this.DeptRadioButtonList.SelectedItem.Text + "的本月打分已经被审批通过不能更改或者再打分！')</script>");
        }
    }

    public void ClosePage()
    {
        string script = "<script>window.close();</script>";
        Response.Write(script);
    }
}
