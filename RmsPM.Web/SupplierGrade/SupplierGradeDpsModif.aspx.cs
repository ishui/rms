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

public partial class SupplierGrade_SupplierGradeDpsModif : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadDate();
        }
    }


    public void LoadDate()
    {
        if (!this.user.HasRight("271001"))
        {
            Response.Redirect("../RejectAccess.aspx");
            Response.End();
        }

        if (!this.user.HasRight("271002"))
        {
            this.btnSave.Visible = false;
        }
        string MainDefineCode = this.ddlWorkFlowTypeView.SelectedValue;
        RmsPM.BLL.GradeDepartment cgradedepartment = new RmsPM.BLL.GradeDepartment();
        if (MainDefineCode != "")
        {
            cgradedepartment.MainDefineCode = MainDefineCode;
        }
        else
        {
            cgradedepartment.MainDefineCode = "100001";
        }
        switch(MainDefineCode)
        {
            case "100001":
                this.lblTitlename.Text = "承包商部门权重管理";
                break;
            case "100002":
                this.lblTitlename.Text = "供应商部门权重管理";
                break;
            default:
                this.lblTitlename.Text = "部门权重管理";
                break;
        }

        DataTable dtGradeDep=cgradedepartment.GetGradeDepartments();

        foreach (DataRow drGradeDep in dtGradeDep.Select())
        {
            drGradeDep["Percentage"]=System.Convert.ToDecimal(drGradeDep["Percentage"]) * 100;
        }

        this.dgList.DataSource = dtGradeDep;
        this.dgList.DataBind();

    }

    protected void btnSave_ServerClick(object sender, EventArgs e)
    {


        try
        {
            decimal tempPercentage = 0;
            foreach (DataGridItem dgItem in dgList.Items)
            {
                tempPercentage += System.Convert.ToDecimal(((TextBox)dgItem.FindControl("TxtPercentage")).Text.Trim());
            }
            if (tempPercentage != 100)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "权重总和为　" + tempPercentage.ToString() + "%  不为100%!"));
                return;
            }
            //System.Web.UI.WebControls.DataGridItem
            foreach (DataGridItem dgItem in dgList.Items)
            {
                RmsPM.BLL.GradeDepartment cgradeDepartment = new RmsPM.BLL.GradeDepartment();
                string departmentCode = ((Label)dgItem.FindControl("lblDepartmentCode")).Text.Trim();
                string percentage = System.Convert.ToString(System.Convert.ToDecimal(((TextBox)dgItem.FindControl("TxtPercentage")).Text.Trim()) / 100);

                cgradeDepartment.DepartmentDefineCode = departmentCode;
                cgradeDepartment.Percentage = percentage;
                cgradeDepartment.GradeDepartmentSubmit();


            }
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存成功"));
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "改变业务权重报错：" + ex.Message));
            throw ex;
        }
        
    }
    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        this.LoadDate();
    }
}
