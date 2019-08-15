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

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.Web;
using RmsDM.MODEL;
using RmsDM.BFL;
using RmsPM.Web;

public partial class RmsDM_FileTemplateTypeModify : PageBase
{
    private string formType;
    private int parentCode;
    private string fullPath;
    private int deep;
    private static string text;
    private Label lblPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        formType = Request.QueryString["FormType"];
        if (formType != null)
        {
            if (formType.Equals("Add"))
            {
                this.FormView1.ChangeMode(FormViewMode.Insert);
            }
            parentCode = Int32.Parse(Request.QueryString["Parentcode"]);
            fullPath = Request.QueryString["FullPath"];
            deep = Int32.Parse(Request.QueryString["Deep"]);
        }
        else
        {
            FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
            FileTemplateTypeModel fttModel = new FileTemplateTypeModel();
            fttModel = fttBFL.GetFileTemplateType(Int32.Parse(Request.QueryString["code"]));
            deep = fttModel.Deep;
            fullPath = fttModel.FullID;
        }
        text = "模版类别";
        if (deep != 0 && fullPath != null)
        {
            string[] path = fullPath.Split('/');
            for (int i = 1; i < path.Length; i++)
            {
                FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
                FileTemplateTypeModel fttModel = new FileTemplateTypeModel();
                fttModel = fttBFL.GetFileTemplateType(Int32.Parse(path[i]));
                text += "-->" + fttModel.FileTemplateTypeName;
            }                
        }
        lblPath = (Label)(FormView1.Row.FindControl("lblPath"));
        lblPath.Text = text;
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        int code = Int32.Parse(Request.QueryString["code"]);
        FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
        if (fttBFL.HasChild(code))
        {
            Response.Write("<script>window.alert('该节点含有子节点不能被删除，删除子节点后再进行操作！');</script>");
            return;
        }
        FileTemplateTypeModel fttModel = new FileTemplateTypeModel();
        fttModel.Code = code;
        fttBFL.DeleteNodeAndTemplate(fttModel);
        Response.Write("<script>opener.parent.location.reload();window.close();</script>");
        Response.End();
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        TextBox tb = (TextBox)(this.FormView1.Row.FindControl("FileTemplateTypeNameTextBox"));
        if (tb.Text.Trim().Equals(String.Empty))
        {
            return;
        }
        int code = Int32.Parse(Request.QueryString["code"]);
        FileTemplateTypeModel fttModel = new FileTemplateTypeModel();
        fttModel.Code = code;
        fttModel.FileTemplateTypeName = tb.Text;
        FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
        fttBFL.Update(fttModel);
        Response.Write("<script>opener.parent.location.reload();window.close()</script>");
    }
    protected void AddButton_Click(object sender, EventArgs e)
    {
        TextBox tb = (TextBox)(this.FormView1.Row.FindControl("FileTemplateTypeNameTextBox"));
        if (tb.Text.Trim().Equals(String.Empty))
        {
            return;
        }
        parentCode = Int32.Parse(Request.QueryString["Parentcode"]);
        fullPath = Request.QueryString["FullPath"];
        deep = Int32.Parse(Request.QueryString["Deep"]);
        FileTemplateTypeModel fttModel = new FileTemplateTypeModel();
        fttModel.FullID = fullPath;
        fttModel.ParentCode = parentCode;
        fttModel.Deep = deep + 1;
        fttModel.FileTemplateTypeName = tb.Text;
        FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
        fttBFL.Insert(fttModel);
        this.FormView1.ChangeMode(FormViewMode.ReadOnly);
        Response.Write("<script>opener.parent.location.reload();window.close();</script>");
    }
    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        lblPath = (Label)(FormView1.Row.FindControl("lblPath"));
        lblPath.Text = text;
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("3702"))
            {
                Button btEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
                btEdit.Visible = false;
            }
            if (!user.HasRight("3703"))
            {
                Button btDelete = (Button)(this.FormView1.Row.FindControl("DeleteButton"));
                btDelete.Visible = false;
            }
        }
    }
}
