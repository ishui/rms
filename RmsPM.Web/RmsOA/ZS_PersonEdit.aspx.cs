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

public partial class PersonalManage_ZS_PersonEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            
        }
        if (Request["Inserted"] + "" == "true")
        {
            //Response.Write("<script>window.opener.location.reload();</script>");
        }
    }

    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        
        Response.Write("<script>window.opener.location.reload();</script>");
        
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }

    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
       
        Response.Redirect(string.Format("ZS_PersonEdit.aspx?Code={0}&Inserted=true", e.ReturnValue.ToString()));
       
    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Label tbxUnit = (Label)this.FormView1.Row.FindControl("yardLabel");
            tbxUnit.Text = RmsPM.BLL.SystemRule.GetUnitName(tbxUnit.Text);
        }
        if (this.FormView1.CurrentMode == FormViewMode.Edit)
        {

            HtmlInputHidden tbxUnitCode = (HtmlInputHidden)this.FormView1.Row.FindControl("txtUnit");
            HtmlInputText tbxUnitName = (HtmlInputText)this.FormView1.Row.FindControl("txtUnitName");
            tbxUnitName.Value = RmsPM.BLL.SystemRule.GetUnitName(tbxUnitCode.Value);
        }

        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {


            Button btnAddHome = (Button)this.FormView1.Row.FindControl("btnAddHome");
            btnAddHome.Attributes["OnClick"] = "javascript:AddHomeDtl();return false;";

            Button btnAddReward = (Button)this.FormView1.Row.FindControl("btnAddReward");
            btnAddReward.Attributes["OnClick"] = "javascript:AddRewardDtl();return false;";

            Button btnAddStudy = (Button)this.FormView1.Row.FindControl("btnAddStudy");
            btnAddStudy.Attributes["OnClick"] = "javascript:AddStudyDtl();return false;";

            Button btnAddTrain = (Button)this.FormView1.Row.FindControl("btnAddTrain");
            btnAddTrain.Attributes["OnClick"] = "javascript:AddTrainDtl();return false;";

            Button btnAddContract = (Button)this.FormView1.Row.FindControl("btnAddContract");
            btnAddContract.Attributes["OnClick"] = "javascript:AddContractDtl();return false;";

            Button btnAddWork = (Button)this.FormView1.Row.FindControl("btnAddWork");
            btnAddWork.Attributes["OnClick"] = "javascript:AddWorkDtl();return false;";

            Button btnAddPolity = (Button)this.FormView1.Row.FindControl("btnAddPolity");
            btnAddPolity.Attributes["OnClick"] = "javascript:AddPolityDtl();return false;";

            //奖惩记录
            if (user.HasRight("340107"))
            {
                btnAddReward.Visible = true;
            }
            else
            {
                btnAddReward.Visible = false;
            }

            //学习经历
            if (user.HasRight("340104"))
            {
                btnAddStudy.Visible = true;
            }
            else
            {
                btnAddStudy.Visible = false;
            }

            //培训记录
            if (user.HasRight("340116"))
            {
                btnAddTrain.Visible = true;
            }
            else
            {
                btnAddTrain.Visible = false;
            }

            //合同信息
            if (user.HasRight("340113"))
            {
                btnAddContract.Visible = true;
            }
            else
            {
                btnAddContract.Visible = false;
            }

            //家庭信息
            if (user.HasRight("340110"))
            {
                btnAddHome.Visible = true;
            }
            else
            {
                btnAddHome.Visible = false;
            }

            //工作经历
            if (user.HasRight("340119"))
            {
                btnAddWork.Visible = true;
            }
            else
            {
                btnAddWork.Visible = false;
            }

            //政治经历
            if (user.HasRight("340122"))
            {
                btnAddPolity.Visible = true;
            }
            else
            {
                btnAddPolity.Visible = false;
            }

            //修改员工信息
            if (user.HasRight("340102"))
            {
                this.FormView1.Row.FindControl("EditButton").Visible = true;
            }
            else
            {
                this.FormView1.Row.FindControl("EditButton").Visible = false;
            }

            //删除员工信息
            if (user.HasRight("340103"))
            {
                this.FormView1.Row.FindControl("DeleteButton").Visible = true;
            }
            else
            {
                this.FormView1.Row.FindControl("DeleteButton").Visible = false;
            }
        }
    }
    protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {

    }
}
