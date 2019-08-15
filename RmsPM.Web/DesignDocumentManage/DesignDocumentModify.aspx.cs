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
using RmsPM.Web.WorkFlowControl;


public partial class DesignDocumentManage_DesignDocumentModify : System.Web.UI.Page
{
    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tdtitle.InnerHtml = Request["Title"] + "";
            this.DesignDocument1.ApplicationCode = Request["ApplicationCode"] + "";
            this.DesignDocument1.ProjectCode = Request["ProjectCode"] + "";
            this.DesignDocument1.DocumentState = Request["Type"] + "0";

            if (Request["State"] + "" == "edit")
            {
                this.EditLoad();
            }
            else
            {
                this.ViewLoad();
            }
            this.btnAuding.Attributes["OnClick"] = "javascript:OpenAuding();";
        }
    }
    /// <summary>
    /// 修改按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnModify_ServerClick(object sender, EventArgs e)
    {
        this.EditLoad();
    }
    /// <summary>
    /// 保存按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        this.DesignDocument1.SubmitData();
        this.ViewLoad();
        Response.Write(Rms.Web.JavaScript.ScriptStart);
        Response.Write("window.opener.location.reload();");
        Response.Write(Rms.Web.JavaScript.ScriptEnd);
    }
    /// <summary>
    /// 删除按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDel_ServerClick(object sender, EventArgs e)
    {
        this.DesignDocument1.Delete();
        Response.Write(Rms.Web.JavaScript.ScriptStart);
        Response.Write("window.opener.location.reload();");
        Response.Write(Rms.Web.JavaScript.WinClose(false));
        Response.Write(Rms.Web.JavaScript.ScriptEnd);
    }
    /// <summary>
    /// 修改状态加载

    /// </summary>
    private void EditLoad()
    {

        this.btnSave.Visible = true;
        this.btnDel.Visible = false;
        this.btnModify.Visible = false;
        this.btnAuding.Visible = false;
        
        this.DesignDocument1.State = ModuleState.Operable;
        this.DesignDocument1.InitControl();
     }
    /// <summary>
    /// 查看状态加载

    /// </summary>
    private void ViewLoad()
    {
        this.DesignDocument1.State = ModuleState.Eyeable;
        this.DesignDocument1.InitControl();

        if (this.DesignDocument1.DocumentState.Length > 0)
        {
            if (this.DesignDocument1.DocumentState.Substring(1, 1) != "0")
            {
                this.btnDel.Visible = false;
                this.btnModify.Visible = false;
                this.btnAuding.Visible = false;
                this.btnSave.Visible = false;
            }
            else 
            {
                this.btnSave.Visible = false;
                this.btnDel.Visible = true;
                this.btnModify.Visible = true;
                this.btnAuding.Visible = true;
            }

        }
        else
        {
            this.btnSave.Visible = false;
            this.btnDel.Visible = true;
            this.btnModify.Visible = true;
            this.btnAuding.Visible = true;
        }
    }
}
