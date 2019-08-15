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

public partial class RmsOA_GR_WorkLogModify : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
        {
            if (Request.QueryString["Type"].Equals("Add"))
            {
                this.FormView1.ChangeMode(FormViewMode.Insert);
            }
        }

    }
    /// <summary>
    /// 更改FormView到编辑状态
    /// </summary>
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        this.FormView1.ChangeMode(FormViewMode.Edit);
    }
    /// <summary>
    /// 删除该篇工作日志
    /// </summary>
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Request.QueryString["Code"]))
        {
            return;
        }
        else
        {
            int code = Int32.Parse(Request.QueryString["Code"]);
            GK_OA_WorkLogBFL wlBFL = new GK_OA_WorkLogBFL();
            GK_OA_WorkLogModel wlModel = new GK_OA_WorkLogModel();
            wlModel.Code = code;
            wlBFL.Delete(wlModel);
            this._CloseWindowAndRefeshParent();
        }
        
    }

    private GK_OA_WorkLogModel _GetModelValue()
    {
        GK_OA_WorkLogModel wlModel = new GK_OA_WorkLogModel();
        FreeTextBoxControls.FreeTextBox tbxContext
            = (FreeTextBoxControls.FreeTextBox)(this.FormView1.Row.FindControl("ContextTextBox"));
        AspWebControl.Calendar dayWrited
            = (AspWebControl.Calendar)(this.FormView1.Row.FindControl("DayWrited"));
        DropDownList ddlWeather = (DropDownList)(this.FormView1.Row.FindControl("WeatherDropDownList"));
        DropDownList ddlMood = (DropDownList)(this.FormView1.Row.FindControl("MoodDropDownList"));
        wlModel.UserId = user.UserID;
        DateTime time;
        if (DateTime.TryParse(dayWrited.Value, out time))
        {
            if (time.Year < 1800)
            {
                wlModel.DayWrited = DateTime.Now;
            }
            else
            {
                wlModel.DayWrited = time;
            }
        }
        else
        {
            wlModel.DayWrited = DateTime.Now;
        }
        if (!ddlMood.SelectedIndex.Equals(0))
        {
            wlModel.Mood = ddlMood.SelectedItem.Text.Trim();
        }
        else
        {
            wlModel.Mood = "";
        }
        if (!ddlWeather.SelectedIndex.Equals(0))
        {
            wlModel.Weather = ddlWeather.SelectedItem.Text.Trim();
        }
        else
        {
            wlModel.Weather = "";
        }
        wlModel.Context = tbxContext.Text.Trim();
        return wlModel;
    }
    private void _CloseWindowAndRefeshParent()
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        int code;
        GK_OA_WorkLogModel wlModel = this._GetModelValue();
        GK_OA_WorkLogBFL wlBFL = new GK_OA_WorkLogBFL();
        code = wlBFL.Insert(wlModel);
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        //Response.Redirect("GR_WorkLogModify.aspx?Type=Read&Code=" + code.ToString());
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int code = Int32.Parse(Request.QueryString["Code"]);
        GK_OA_WorkLogModel wlModel = this._GetModelValue();
        wlModel.Code = code;
        GK_OA_WorkLogBFL wlBFL = new GK_OA_WorkLogBFL();
        wlBFL.Update(wlModel);
        Response.Write("<script>window.opener.location.reload();</script>");
        this.FormView1.ChangeMode(FormViewMode.ReadOnly);
        //Response.Redirect("GR_WorkLogModify.aspx?Type=Read&Code=" + Request.QueryString["Code"]);
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("350203"))
            {
                Button btnDelete = (Button)(this.FormView1.Row.FindControl("ButtonDelete"));
                btnDelete.Visible = false;
            }
            if (!user.HasRight("350202"))
            {
                Button btnEdit = (Button)(this.FormView1.Row.FindControl("ButtonEdit"));
                btnEdit.Visible = false;
            }
        }
    }
}
