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

using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;

public partial class TemplateOpinionManage_TemplateOpinionModify : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["TemplateOpinionCode"] + "" != "")
        {
            this.btnModify.Visible = true;
            this.btnDel.Visible = true;
            this.btnSave.Visible = false;
            this.OperableDiv.Visible = false;
            this.EyeableDiv.Visible = true;
            LoadData(false);
        }
        else
        {
            this.btnModify.Visible = false;
            this.btnDel.Visible = false;
            this.btnSave.Visible = true;
            this.OperableDiv.Visible = true;
            this.EyeableDiv.Visible = false;

        }
    }
    /// ****************************************************************************
    /// <summary>
    /// 数据加载
    /// </summary>
    /// ****************************************************************************
    private void LoadData(bool Flag)
    {
        if (Request["TemplateOpinionCode"]+"" != "")
        {
            TemplateOpinion cTemplateOpinion = new TemplateOpinion();
            cTemplateOpinion.TemplateOpinionCode = Request["TemplateOpinionCode"] + "";

            if (Flag)
            {
                this.txtName.Value = cTemplateOpinion.Name;
                this.txtCenter.Value = cTemplateOpinion.Center;
            }
            else
            {
                this.tdName.InnerHtml = cTemplateOpinion.Name;
                this.tdCenter.InnerHtml = cTemplateOpinion.Center.Replace("\n", "<br>"); ;
            }
        }
        else
        {
            if (Flag)
            {

            }
        }
    }
    /// ****************************************************************************
    /// <summary>
    /// 提交数据
    /// </summary>
    /// ****************************************************************************
    public string SubmitData()
    {
        string ErrMsg = "";
        if (this.txtName.Value == "")
        {
            ErrMsg = "标题不能为空";
            Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
            return ErrMsg;
        }
        TemplateOpinion cTemplateOpinion = new TemplateOpinion();
        cTemplateOpinion.TemplateOpinionCode = Request["TemplateOpinionCode"] + "";
        cTemplateOpinion.Name = this.txtName.Value;
        cTemplateOpinion.Center = this.txtCenter.Value;
        cTemplateOpinion.UserCode = user.UserCode;
        cTemplateOpinion.TemplateOpinionSubmit();
        return "";
    }
    /// ****************************************************************************
    /// <summary>
    /// 删除数据
    /// </summary>
    /// ****************************************************************************
    public void Delete()
    {
        TemplateOpinion cTemplateOpinion = new TemplateOpinion();
        cTemplateOpinion.TemplateOpinionCode = Request["TemplateOpinionCode"] + "";
        cTemplateOpinion.TemplateOpinionDelete();
    }


    protected void btnModify_ServerClick(object sender, EventArgs e)
    {
        this.btnModify.Visible = false;
        this.btnDel.Visible = false;
        this.btnSave.Visible = true;
        this.OperableDiv.Visible = true;
        this.EyeableDiv.Visible = false;
        LoadData(true);
    }
    protected void btnDel_ServerClick(object sender, EventArgs e)
    {
        Delete();
        Response.Write(Rms.Web.JavaScript.ScriptStart);
        Response.Write("window.opener.location.reload();");
        Response.Write(Rms.Web.JavaScript.WinClose(false));
        Response.Write(Rms.Web.JavaScript.ScriptEnd);
    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        string Errmsg = SubmitData();
        if (Errmsg != "")
        {
            return;
        }
        Response.Write(Rms.Web.JavaScript.ScriptStart);
        Response.Write("window.opener.location.reload();");
        Response.Write(Rms.Web.JavaScript.WinClose(false));
        Response.Write(Rms.Web.JavaScript.ScriptEnd);

    }
}
