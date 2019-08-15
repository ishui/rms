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
using Rms.ORMap;

public partial class WorkFlowContral_WokFlowCommonInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            IniPage();
            LoadData();

            ArrayList ar = user.GetResourceRight(this.hidWorkFlowCommonCode.Value, "WorkFlowCommon");


            // 权限
            if (!ar.Contains("090203")) //修改
            {
                this.btnModify.Visible = false;
            }

            if (!ar.Contains("090204")) //提交申请
            {
                this.btnSubmitAudit.Visible = false;
            }

            if (!ar.Contains("090205")) //删除
            {
                this.btnDelete.Visible = false;
            }
        }



        this.myAttachMentList.AttachMentType = "WorkFlowCommon";
        this.myAttachMentList.MasterCode = this.hidWorkFlowCommonCode.Value;
    }

    private void IniPage()
    {
        try
        {
            string ud_sProjectCode = Request["ProjectCode"] + "";
            string ud_sProcedureCode = Request["ProcedureCode"] + "";
            string ud_sWorkFlowCommonCode = Request.QueryString["WorkFlowCommonCode"] + "";

            this.hidProjectCode.Value = ud_sProjectCode.Trim();
            this.hidProcedureCode.Value = ud_sProcedureCode.Trim();
            this.hidWorkFlowCommonCode.Value = ud_sWorkFlowCommonCode.Trim();



            //switch (this.up_sPMName)
            //{
            //    case "ShiMaoPM":
            //        this.inputSystemGroup.SelectAllLeaf = false;
            //        break;
            //    default:
            //        break;
            //}

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面错误。");
        }
    }

    private void LoadData()
    {
        try
        {
            string ud_sWorkFlowCommonCode = hidWorkFlowCommonCode.Value;

            ArrayList ar = user.GetResourceRight(ud_sWorkFlowCommonCode, "WorkFlowCommon");
            if (!ar.Contains("090201"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }

            EntityData entity = RmsPM.DAL.EntityDAO.WorkFlowDAO.GetWorkFlowCommonByCode(ud_sWorkFlowCommonCode);

            entity.SetCurrentTable("WorkFlowCommon");

            string ud_sProcedureCode = entity.GetString("ProcedureCode"); ;
            string ud_sProjectCode = entity.GetString("ProjectCode");

            this.hidProcedureCode.Value = ud_sProcedureCode;
            this.hidProjectCode.Value = ud_sProjectCode;

            //审批流程url
            ViewState["_AuditingURL"] = RmsPM.BLL.WorkFlowRule.GetProcedureURLByCode(ud_sProcedureCode);

            string ud_sAuditingURLAndParam = ViewState["_AuditingURL"].ToString() + "&ProjectCode=" + ud_sProjectCode;

            ud_sAuditingURLAndParam += "&WorkFlowCommonCode=" + ud_sWorkFlowCommonCode;
//            ud_sAuditingURLAndParam += "&ProcedureCode=" + ud_sProcedureCode;

            ViewState["_AuditingURLAndParam"] = ud_sAuditingURLAndParam;


            this.btnDelete.Visible = false;
            this.btnModify.Visible = false;
            this.btnSubmitAudit.Visible = false;

            switch (entity.GetInt("Status"))
            { 
                case 0: //申请
                    this.btnDelete.Visible = true;
                    this.btnModify.Visible = true;
                    this.btnSubmitAudit.Visible = true;
                    break;
                case 1: //已审
                    break;
                case 2: //审核中


                    break;
                case 3: //作废
                    break;



            }

            this.lblProcedureName.Text = RmsPM.BLL.WorkFlowRule.GetProcedureNameByCode(entity.GetString("ProcedureCode"));

            this.lblWorkFlowTitle.Text = entity.GetString("WorkFlowTitle");
            this.lblStatus.Text = RmsPM.BLL.WorkFlowRule.GetWorkFlowCommonStatusName(entity.GetInt("Status").ToString());
            this.lblUnitName.Text = RmsPM.BLL.SystemRule.GetUnitName(entity.GetString("Unit"));

            this.lblWorkFlowID.Text = entity.GetString("WorkFlowID");
            this.lblType.Text = RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName(entity.GetString("Type"));
            this.lblTransactorName.Text = RmsPM.BLL.SystemRule.GetUserName(entity.GetString("Transactor"));

            this.lblContent.Text = entity.GetString("Content").Replace("\n","<br />");

            entity.Dispose();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "加载数据错误");
            Response.Write(Rms.Web.JavaScript.Alert(true, "加载数据出错：" + ex.Message));
        }
    }

    protected void btnDelete_ServerClick(object sender, EventArgs e)
    {
        try
        {
            string ud_sWorkFlowCommonCode = this.hidWorkFlowCommonCode.Value;

            EntityData entity = RmsPM.DAL.EntityDAO.WorkFlowDAO.GetWorkFlowCommonByCode(ud_sWorkFlowCommonCode);


            foreach (DataRow dr in entity.CurrentTable.Rows)
            {
                dr["Status"] = -1;
            }
            RmsPM.DAL.EntityDAO.WorkFlowDAO.SubmitAllWorkFlowCommon(entity);
            entity.Dispose();

            // 删除附件
            this.myAttachMentList.AttachMentType = "WorkFlowCommon";
            this.myAttachMentList.DelAttachMentByMasterCode(ud_sWorkFlowCommonCode);
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "删除错误");
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除错误：" + ex.Message));
        }
    }
}
