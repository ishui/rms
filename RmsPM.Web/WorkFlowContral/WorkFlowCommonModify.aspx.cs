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

public partial class WorkFlowContral_WorkFlowCommonModify : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IniPage();
            LoadData();
        }

        this.myAttachMentAdd.AttachMentType = "WorkFlowCommon";
        this.myAttachMentAdd.MasterCode = this.hidWorkFlowCommonCode.Value;  
    }

    private void IniPage()
    {
        try
        {
            string ud_sProjectCode = Request["ProjectCode"] + "";
            string ud_sProcedureCode = Request["ProcedureCode"] + "";
            string ud_sWorkFlowCommonCode = Request.QueryString["WorkFlowCommonCode"] + "";

            this.inputSystemGroup.ClassCode = "0902";
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
            string ud_sAction = Request.QueryString["Act"] + "";
            string ud_sWorkFlowCommonCode = hidWorkFlowCommonCode.Value;

            EntityData entity = null;

            switch ( ud_sAction )
            {
                case "Add":
                    if (!user.HasRight("090202"))
                    {
                        Response.Redirect("../RejectAccess.aspx");
                        Response.End();
                    }
                    entity = new EntityData("WorkFlowCommon");
                    DataRow ud_drNew = entity.GetNewRecord();

                    ud_sWorkFlowCommonCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WorkFlowCommonCode");

                    this.hidWorkFlowCommonCode.Value = ud_sWorkFlowCommonCode;

                    ud_drNew["WorkFlowCommonCode"] = ud_sWorkFlowCommonCode;
                    ud_drNew["ProcedureCode"] = this.hidProcedureCode.Value;
                    ud_drNew["ProjectCode"] = this.hidProjectCode.Value;
                    ud_drNew["Status"] = 0;
                    ud_drNew["Transactor"] = base.user.UserCode;
                    ud_drNew["Creator"] = base.user.UserCode;
                    ud_drNew["CreateDate"] = DateTime.Now;

                    entity.AddNewRecord(ud_drNew);
                    break;
                case "Edit":
                    ArrayList ar = user.GetResourceRight(ud_sWorkFlowCommonCode, "WorkFlowCommon");
                    if (!ar.Contains("090203"))
                    {
                        Response.Redirect("../RejectAccess.aspx");
                        Response.End();
                    }
                    entity = RmsPM.DAL.EntityDAO.WorkFlowDAO.GetWorkFlowCommonByCode(ud_sWorkFlowCommonCode);
                    break;
                default:
                    Response.Redirect("../RejectAccess.aspx");
                    Response.End();
                    break;

            }

            entity.SetCurrentTable("WorkFlowCommon");
            this.lblProcedureName.Text = RmsPM.BLL.WorkFlowRule.GetProcedureNameByCode(entity.GetString("ProcedureCode"));

            this.txtWorkFlowTitle.Value = entity.GetString("WorkFlowTitle");
            this.ucUnit.Value = entity.GetString("Unit");

            this.txtWorkFlowID.Value = entity.GetString("WorkFlowID");
            this.inputSystemGroup.Value = entity.GetString("Type");
            this.txtTransactor.Value = entity.GetString("Transactor");
            this.txtTransactorName.Value = RmsPM.BLL.SystemRule.GetUserName(entity.GetString("Transactor"));

            this.ftbDetail.Text = entity.GetString("Content");

            entity.Dispose();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "加载数据错误");
            Response.Write(Rms.Web.JavaScript.Alert(true, "加载数据出错：" + ex.Message));
        }
    }


    private EntityData GetEntity()
    { 
        string ud_sAction = Request.QueryString["Act"] + "";
        string ud_sWorkFlowCommonCode = hidWorkFlowCommonCode.Value;

        EntityData entity = null;

        switch (ud_sAction)
        {
            case "Add":
                entity = new EntityData("WorkFlowCommon");
                DataRow ud_drNew = entity.GetNewRecord();

                ud_drNew["WorkFlowCommonCode"] = ud_sWorkFlowCommonCode;
                ud_drNew["ProcedureCode"] = this.hidProcedureCode.Value;
                ud_drNew["ProjectCode"] = this.hidProjectCode.Value;
                ud_drNew["Status"] = 0;
                ud_drNew["Creator"] = base.user.UserCode;
                ud_drNew["CreateDate"] = DateTime.Now;

                entity.AddNewRecord(ud_drNew); 
                
                break;
            case "Edit":
                entity = RmsPM.DAL.EntityDAO.WorkFlowDAO.GetWorkFlowCommonByCode(ud_sWorkFlowCommonCode);
                break;
        }

        foreach (DataRow dr in entity.Tables["WorkFlowCommon"].Select(string.Format("WorkFlowCommonCode='{0}'", ud_sWorkFlowCommonCode)))
        { 

            dr["WorkFlowTitle"] = this.txtWorkFlowTitle.Value.Trim();
            dr["Unit"] = this.ucUnit.Value.Trim();
            dr["WorkFlowID"] = this.txtWorkFlowID.Value.Trim();
            dr["Type"] = this.inputSystemGroup.Value.Trim();
            dr["Transactor"] = this.txtTransactor.Value.Trim();
            dr["Content"] = this.ftbDetail.Text.Trim();

            if (ud_sAction == "Edit")
            {
                dr["Modifier"] = base.user.UserCode;
                dr["ModifyDate"] = DateTime.Now;
            }
        }

        return entity;
    }

    private string CheckData()
    {
        string ud_sErrMsg = "";

        if (this.txtWorkFlowTitle.Value.Trim() == "")
        {
            ud_sErrMsg += "请输入流程标题！ ";
        }

        if (this.ucUnit.Value.Trim() == "")
        {
            ud_sErrMsg += "请输入部门！ ";
        }

        if (this.inputSystemGroup.Value.Trim() == "")
        {
            ud_sErrMsg += "请输入流程类型！ ";
        }

        return ud_sErrMsg;
    }

    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        try
        {
            string ud_sAction = Request.QueryString["Act"] + "";
            string ud_sWorkFlowCommonCode = hidWorkFlowCommonCode.Value;

            string ud_sErrMsg = CheckData();

            if (ud_sErrMsg != "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ud_sErrMsg)); 
            }
            else
            {
                EntityData entity = GetEntity();
                RmsPM.DAL.EntityDAO.WorkFlowDAO.SubmitAllWorkFlowCommon(entity);
                entity.Dispose();

                GoBack();

            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "保存数据错误");
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存数据出错：" + ex.Message));
        }
    }


    /// <summary>
    /// 返回
    /// </summary>
    private void GoBack()
    {
        string ud_sProjectCode = Request["ProjectCode"] + "";
        string ud_sProcedureCode = Request["ProcedureCode"] + "";
//        string ud_sWorkFlowCommonCode = Request.QueryString["WorkFlowCommonCode"] + "";
        string ud_sWorkFlowCommonCode = hidWorkFlowCommonCode.Value;

        Response.Write(Rms.Web.JavaScript.ScriptStart);

        Response.Write("window.opener.location = window.opener.location;");
        Response.Write("window.location.href='../WorkFlowContral/WokFlowCommonInfo.aspx?WorkFlowCommonCode=" + ud_sWorkFlowCommonCode + "&ProcedureCode=" + ud_sProcedureCode +
         "&projectCode=" + ud_sProjectCode + "';");
//        Response.Write(Rms.Web.JavaScript.WinClose(false));

        Response.Write(Rms.Web.JavaScript.ScriptEnd);
    }
}
