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
using RmsDM.BFL;
using RmsDM.MODEL;

public partial class SelectBox_selectDocumentTemplateList : System.Web.UI.Page
{
    private string ParentCode
    {
        set
        {
            this.ViewState["ParentCode"] = value;
        }
        get
        {
            if (this.ViewState["ParentCode"] == null)
                return "";
                return ViewState["ParentCode"].ToString();
           
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //返回函数名

            string ReturnFunc = Request.QueryString["ReturnFunction"] + "";
            if (ReturnFunc == "") 
            {
                ReturnFunc = "ReturnTemplate";
            }
            ViewState["ReturnFunc"] = ReturnFunc;

            string AddSignleType = Request["AddSignleType"];
            if (AddSignleType != "" && AddSignleType != null)
            {
                this.GridView1.Columns[3].Visible = true;
                this.SelectButton.Visible = true;
                this.CloseButton.Visible = true;
                this.GridView1.AllowPaging = false;
            }
            else
            {
                this.CloseButton.Visible = false;
                this.SelectButton.Visible = false;
            }

            this.ParentCode = Request["ParentCode"];

        }
    }

    protected void SelectButton_Click(object sender, EventArgs e)
    {
        if (this.ParentCode != "" && this.ParentCode != null)
        {
            DocumentDirectoryBFL objBFL = new DocumentDirectoryBFL();
            DocumentDirectoryModel ObjModel = new DocumentDirectoryModel();

            string DepartmentCode = objBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).DepartmentCode;
            string DirectoryNodeCode = objBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).DirectoryNodeCode;

            string FullID = objBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).FullID;
            int Deep = objBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).Deep;

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                 CheckBox chk = (CheckBox)gvr.Cells[3].Controls[1];
                 if (chk.Checked)
                 {
                     Label labFileTemplateName = (Label)gvr.Cells[1].Controls[1];
                     ObjModel.DirectoryName = labFileTemplateName.Text;

                     ObjModel.ParentCode = int.Parse(ParentCode);
                     ObjModel.DepartmentCode = DepartmentCode;
                     ObjModel.DirectoryNodeCode = DirectoryNodeCode;
                     ObjModel.CreateDate = System.DateTime.Now;
                     ObjModel.Deep = Deep + 1;

                     Label labCode = (Label)gvr.Cells[0].Controls[1];
                     ObjModel.FileTemplateCode = int.Parse(labCode.Text);

                     ObjModel.FullID = FullID + "/" + ParentCode;
                     objBFL.Insert(ObjModel);
                 }
            }

            Response.Write("<script>window.parent.opener.location.reload();</script>");
        }
    }
}
