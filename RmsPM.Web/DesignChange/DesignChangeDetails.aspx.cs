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
using RmsPM.Web.UserControls;
using System.Collections.Generic;

public partial class DesignChange_DesignChangeDetails : RmsPM.Web.PageBase
{
    protected string inoutType = "";
    protected string projectCode = "";
    protected void Page_Load(object sender, EventArgs e)
	{
        projectCode = Request["ProjectCode"] + "";
        if (Request["Type"] + "" == "1")
        {
            inoutType = "内部";
            this.WorkFlowList1.ProcedureNameAndApplicationCodeList = "'设计变更[内部]" + Request["DesignChangeCode"] + "'";

        }
        else
        {
            inoutType = "外部";
            this.WorkFlowList1.ProcedureNameAndApplicationCodeList = "'设计变更" + Request["DesignChangeCode"] + "'";
        }
		if (!IsPostBack)
		{

            switch (this.up_sPMNameLower)
            {
                case "yefengpm":
                    if (this.FormView1.CurrentMode != FormViewMode.ReadOnly)
                        ((RequiredFieldValidator)this.FormView1.Row.FindControl("RequiredFieldValidator1")).Enabled = true;
                    break;
                default:
                    break;
            }

			if (!user.HasRight("2401"))
			{
				Response.Redirect("../RejectAccess.aspx");
				Response.End();
			}
			if (Request["DesignChangeCode"] + "" != "")
				this.ViewState["DesignChangeCode"] = Request["DesignChangeCode"] + "";
            if (this.ViewState["DesignChangeCode"] == null)
            {
                FormView1.ChangeMode(FormViewMode.Insert);
                ((InputUser)this.FormView1.Row.FindControl("txtPerson")).Value = this.user.UserCode;
                if (!string.IsNullOrEmpty(user.BuildStationCodes()))
                {
                    string[] station = user.BuildStationCodes().Split(new char[] { ',' });
                    ((InputUnit)this.FormView1.Row.FindControl("txtUnit")).Value = RmsPM.BLL.SystemRule.GetUnitByStationCode(station[0]);
                }
            }
           
                this.WorkFlowList1.DataBound();
			//if (Request["Type"].ToString() == "1")
			//{
			//    //((HtmlTableRow)FormView1.Row.FindControl("internalTr1")).Visible = false;
			//    //((HtmlTableRow)FormView1.Row.FindControl("internalTr2")).Visible = false;
            //}

            
		}
        

	}
    
	protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
	{
		Response.Write("<script>window.opener.location.reload();window.close();</script>");
		Response.End();
	}
	protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
	{
		Response.Write("<script>window.opener.location.reload();window.close();</script>");
	}
	protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
	{
		e.Cancel = this.InvalidInput();
		e.Values["ProjectName"] = projectCode ;
		e.Values["Type"] = Request["Type"]+"";
		e.Values["State"] = "0";
		e.Values["Flag"] = "0";
        if (this.up_sPMNameLower == "yefengpm")
        {
            if (e.Values["ReferCode"].ToString() == "" || e.Values["ReferCode"] == null)
                e.Values["ReferCode"] = -1;
            string viseid = e.Values["Number"].ToString();
            e.Values["Number"] = getcode(viseid);
        }
        else
        {
            e.Values["ReferCode"] = -1;
        }
	}
    private string getcode(string viseid)
    {
            string[] viseids = viseid.Split('-');        
            if (viseids.Length == 5 && inoutType!="内部")
            {
                string viseidnum = RmsPM.DAL.EntityDAO.SystemManageDAO.GetFormatSysCode("YFID"+projectCode + viseids[1] + viseids[3], "{####}", 1001);
                 return viseids[0] + "-" + viseids[1] + "-" + viseids[2] + "-" + viseids[3] + "-" + viseidnum;
            }
            else
            {
                return viseid;
            }
    }
	protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
	{
        
	}
	protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
	{
		e.Cancel = this.InvalidInput();
        if (this.up_sPMNameLower == "yefengpm")
        {
            if (e.OldValues["Number"].ToString() != e.NewValues["Number"].ToString()&&inoutType!="内部")
            {
                string[] oldviseids = e.OldValues["Number"].ToString().Split('-');
                string[] newviseids = e.NewValues["Number"].ToString().Split('-');
                if (oldviseids[4] == newviseids[4])
                {
                   string viseid = e.NewValues["Number"].ToString();

                   e.NewValues["Number"] = getcode(viseid);
                }

            }
            RmsPM.BFL.DesignChangeBFL tmpbfl = new RmsPM.BFL.DesignChangeBFL();
            TiannuoPM.MODEL.DesignChangeQueryModel dcqmdl = new TiannuoPM.MODEL.DesignChangeQueryModel();
            List<TiannuoPM.MODEL.DesignChangeModel> lisdcmdl = tmpbfl.GetDesignChangeList(dcqmdl);
            int listlenth = lisdcmdl.Count;
            for (int i = 0; i < listlenth; i++)
            {
                if (lisdcmdl[i].Number.Contains(e.NewValues["Number"].ToString()))
                {
                    e.Cancel = true;
                    Response.Write("<script>alert(\" 设计变更编号重复！\");</script>");
                }
            }
        }

	}
	protected void FormView1_DataBound(object sender, EventArgs e)
	{
		if (FormView1.CurrentMode == FormViewMode.ReadOnly)
		{
			Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
			if (btnDelete != null)
				btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前签证吗？')";
			HtmlInputButton btnRequisition = ((HtmlInputButton)this.FormView1.Row.FindControl("btnRequisition"));
			if (btnRequisition != null)
				btnRequisition.Attributes["OnClick"] = (Request["Type"].ToString() == "1") ? "javascript:OpenRequisitionInternal(); return false;" : "javascript:OpenRequisition(); return false;";
			ButtonStatusCtrl();
		}

        //野风特殊需求增加“事件缘由”，可以选择关联的“新增公司内部变更预审”。

        if (this.up_sPMNameLower == "yefengpm")
        {
            if (Request["Type"].ToString() == "1")
            {
                ((System.Web.UI.HtmlControls.HtmlTableCell)(this.FormView1.Row.FindControl("CaseReason"))).Visible = false;
                ((System.Web.UI.HtmlControls.HtmlTableCell)(this.FormView1.Row.FindControl("CaseReasonTitle"))).Visible = false;
            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlTableCell)(this.FormView1.Row.FindControl("CaseReason"))).Visible = true;
                ((System.Web.UI.HtmlControls.HtmlTableCell)(this.FormView1.Row.FindControl("CaseReasonTitle"))).Visible = true;
            }
            //编号为只读

            TextBox viseid = (System.Web.UI.WebControls.TextBox)this.FormView1.Row.FindControl("txtNumber");
            if (viseid != null)
            {
               // viseid.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            ((System.Web.UI.HtmlControls.HtmlTableCell)(this.FormView1.Row.FindControl("CaseReason"))).Visible = false;
            ((System.Web.UI.HtmlControls.HtmlTableCell)(this.FormView1.Row.FindControl("CaseReasonTitle"))).Visible = false;
        
        }
            // 变更类型 2006-12-21

		try
		{
			Control ChangeType = FormView1.FindControl("ChangeType");
			if (typeof(Label) == ChangeType.GetType())
			{
				Label l = (Label)ChangeType;
				l.Text = RmsPM.BLL.ContractRule.GetContractTypeName(l.Text);
			}
			else if (ChangeType != null)
			{
				((RmsPM.Web.UserControls.InputSystemGroup)ChangeType).ClassCode = "22";
			}
		}
		catch (Exception ex)
		{
		    RmsPM.Web.LogHelper.Error("ChangeType", ex);
		}
		///////////////

	}
	protected void LinkButton1_Click(object sender, EventArgs e)
	{
		this.FormView1.DataBind();
		ButtonStatusCtrl();
	}
	protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
	{
		ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
		this.ViewState["DesignChangeCode"] = e.ReturnValue.ToString();
		((RmsPM.Web.UserControls.AttachMentAdd)FormView1.Row.FindControl("AttachMentAdd3")).SaveAttachMent(e.ReturnValue.ToString());
	}
	private void ButtonStatusCtrl()
	{
		Button btnModify = ((Button)this.FormView1.Row.FindControl("btnModify"));
		Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
		HtmlInputButton btnRequisition = ((HtmlInputButton)this.FormView1.Row.FindControl("btnRequisition"));
		HtmlInputButton btnAudit = ((HtmlInputButton)this.FormView1.Row.FindControl("btnAudit"));
		RmsPM.BFL.DesignChangeBFL dc = new RmsPM.BFL.DesignChangeBFL();
		TiannuoPM.MODEL.DesignChangeModel dcmodel = dc.GetDesignChange((int)FormView1.DataKey.Value);
        
        //ArrayList ar = user.GetResourceRight(FormView1.DataKey.Value.ToString(), "DesignChange");
		switch (dcmodel.State)
		{
			case "0":
				btnModify.Visible = true;
				btnDelete.Visible = true;
				btnAudit.Visible = true;
				btnRequisition.Visible = true;
				break;
			case "1":
				btnModify.Visible = false;
				btnDelete.Visible = false;
				btnAudit.Visible = true;
				btnRequisition.Visible = false;
				break;
			case "2":
				btnModify.Visible = false;
				btnDelete.Visible = false;
				btnAudit.Visible = false;
				btnRequisition.Visible = false;
				break;
			case "3":
				btnModify.Visible = false;
				btnDelete.Visible = false;
				btnAudit.Visible = true;
				btnRequisition.Visible = true;
				break;
		}
		if (!user.HasRight("2402"))
			btnModify.Visible = false;
		if (!user.HasRight("2404"))
			btnDelete.Visible = false;
		if (!user.HasRight("2405"))
			btnAudit.Visible = false;
        if (!user.HasRight("2406"))
            btnRequisition.Visible = false;
		if (FormView1.CurrentMode == FormViewMode.ReadOnly)
		{
			int iCont = RmsPM.BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("设计变更", Request["DesignChangeCode"] + "");
			if (iCont > 0)
			{
				btnRequisition.Visible = false;
			}
		}

	}
	/// <summary>
	/// 表单元素校验
	/// </summary>
	/// <returns>结果（false为校验通过）</returns>
	private bool InvalidInput()
	{
		bool ReturnCancel = false;
		if (((RmsPM.Web.UserControls.InputUnit)this.FormView1.Row.FindControl("txtUnit")).Value == "")
		{
			((HtmlGenericControl)this.FormView1.Row.FindControl("UnitMsgSpan")).InnerHtml = "<font color='red'>必填</font>";
			ReturnCancel = true;
		}
		else
		{
			((HtmlGenericControl)this.FormView1.Row.FindControl("UnitMsgSpan")).InnerHtml = "";
		}

		if (((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("ChangeType")).Value == "")
		{
			((HtmlGenericControl)this.FormView1.Row.FindControl("Span1")).InnerHtml = "<font color='red'>必填</font>";
			ReturnCancel = true;
		}
		else
		{
			((HtmlGenericControl)this.FormView1.Row.FindControl("Span1")).InnerHtml = "";
		}


		if (((RmsPM.Web.UserControls.InputUser)this.FormView1.Row.FindControl("txtPerson")).Value == "")
		{
			((HtmlGenericControl)this.FormView1.Row.FindControl("PersonMsgSpan")).InnerHtml = "<font color='red'>必填</font>";
			ReturnCancel = true;
		}
		else
		{
			((HtmlGenericControl)this.FormView1.Row.FindControl("PersonMsgSpan")).InnerHtml = "";
		}
		return ReturnCancel;
	}
	protected void FormView1_ItemCreated(object sender, EventArgs e)
	{
	}
    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }
    protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }
}
