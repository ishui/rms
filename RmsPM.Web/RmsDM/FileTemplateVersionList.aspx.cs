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
using System.Data.SqlClient;
using System.Collections.Generic;

using RmsDM.BFL;
using RmsDM.MODEL;
using RmsDM.DAL;
using RmsPM.Web;

public partial class RmsDM_FileTemplateVersionList : PageBase
{
    private Label lblSortName;
    private DropDownList ddl;
    private static ArrayList arr;
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 删除，破坏四层架构需要更改。
    /// </summary>
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        int code = Convert.ToInt32(Page.Request.QueryString["code"]);
        SqlConnection sqlConn = new SqlConnection();
        sqlConn.ConnectionString = FunctionRule.GetConnectionString();
        HandMadeDAL hmd = new HandMadeDAL(sqlConn);
        hmd.UpVersionState(code);
        Response.Write("<script>window.close();</script>");
        Response.Redirect("FileTemplateVersionList.aspx?code=" + code.ToString());
    }
    /// <summary>
    /// 更新按钮
    ///Distination of Document file haxn't been Implement
    /// </summary>
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        TextBox tbx = (TextBox)(this.FormView1.Row.FindControl("FileTemplateNameTextBox"));
        TextBox tbxSortCode = (TextBox)(this.FormView1.Row.FindControl("SortCodeTextBox"));
        ddl = (DropDownList)(this.FormView1.Row.FindControl("SortDropDownList"));
        string fileTemplateName = tbx.Text;
        string sortCode = tbxSortCode.Text;
        FileTemplateBFL ftBFL = new FileTemplateBFL();
        FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
        if (fileTemplateName.Equals(String.Empty))
        {
            return;
        }
        else
        {
            FileTemplateModel ftModel = new FileTemplateModel();
            if (ddl.SelectedIndex > 0) //index<0　没有被选择或者DoroDownList没有显示．
            {
                int index = ddl.SelectedIndex;
                int code = fttBFL.GetCodeByIndex(ViewState["Code"].ToString(),index);
                ftModel.FileTemplateTypeCode = code;
            }
            ftModel.Code = Convert.ToInt32(Page.Request.QueryString["code"]);
            ftModel.FileTemplateName = fileTemplateName;
            ftModel.SortCode = sortCode;
            ftBFL.Update(ftModel); 
        }
        Response.Redirect("FileTemplateVersionList.aspx?code=" + Request.QueryString["code"]);
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        int code = Int32.Parse(Page.Request.QueryString["code"]);
        int parentCode;
        int typeCode;
        bool needEdit;
        string text;
        string[] nameAndcode;
        
        FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
        text = fttBFL.GetParentPath(code, out parentCode, out typeCode);
        nameAndcode = fttBFL.GetNodeBrother(parentCode,typeCode,out needEdit);
        ViewState["Code"] = nameAndcode[0];
        if (needEdit.Equals(false))
        {
            lblSortName = (Label)(this.FormView1.Row.FindControl("lblSortName"));
            lblSortName.Text = text;
            if (this.FormView1.CurrentMode == FormViewMode.Edit)
            {
                ddl = (DropDownList)(this.FormView1.Row.FindControl("SortDropDownList"));
                ddl.Visible = false;
            }
        }
        else
        {
            if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                lblSortName = (Label)(this.FormView1.Row.FindControl("lblSortName"));
                lblSortName.Text = text;
                if (!user.HasRight("380102"))
                {

                    Button btEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
                    btEdit.Visible = false;
                }
                if (!user.HasRight("380201"))
                {
                    Button btAdd = (Button)(this.FormView1.Row.FindControl("btnAdd"));
                    btAdd.Visible = false;
                }
            }
            else//EditMode
            {
                ddl = (DropDownList)(this.FormView1.Row.FindControl("SortDropDownList"));
                lblSortName = (Label)(this.FormView1.Row.FindControl("lblSortName"));
                if (text.Contains(">"))
                {
                    int index = text.LastIndexOf('>');
                    lblSortName.Text = text.Substring(0, index+1);
                }
                ddl.DataSource = fttBFL.SpiltStringToArray(nameAndcode[1]);
                ddl.DataBind();
            }
         }
        }

    /// <summary>
    ///变换到编辑状态 
    /// </summary>
    protected void EditButton_Click(object sender, EventArgs e)
    {
        this.FormView1.ChangeMode(FormViewMode.Edit);
    }
}
