using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RmsDM.BFL;
using Rms.ORMap;
using RmsPM.Web;
using RmsDM.MODEL;
using RmsPM.DAL.EntityDAO;
/// <summary>
/// 文档业务操作控件
/// <remarks>
/// 
/// </remarks>
/// </summary>
public partial class WorkFlowOperation_DocumentFile : WorkFlowOperationBase
{
    /// <summary>
    /// 文件模板Code
    /// </summary>
    public string FileTemplateCode
    {
        get { return ViewState["FileTemplateCode"].ToString(); }
        set { ViewState["FileTemplateCode"] = value; }
    }

    /// <summary>
    /// 文档外观层    /// </summary>
    private DocumentFileBFL DFBFL = new DocumentFileBFL();   
    /// <summary>
    /// 文件模板外观层    /// </summary>
    private FileTemplateBFL FTBFL = new FileTemplateBFL();
    /// <summary>
    /// 文件版本外观层    /// </summary>
    private FileTemplateVersionBFL FTVBFL = new FileTemplateVersionBFL();
    /// <summary>
    /// 文件版本查询Model
    /// </summary>
    private FileTemplateVersionQueryModel FTVQM = new FileTemplateVersionQueryModel();   
    /// <summary>
    /// 文档目录外观层    /// </summary>
    private DocumentDirectoryBFL DDBFL = new DocumentDirectoryBFL();    
    private RmsPM.Web.UserControls.AttachMentAdd ucadd;
    public bool isNew = false;
    protected void Page_Load(object sender, EventArgs e)
    {        
       
    }
    public override void LoadData()
    {
        try
        {
            if (this.ApplicationCode != "")
            {
                this.OperationCode = this.ApplicationCode;
            }
            else if (this.OperationCode != "")
            {
                this.ApplicationCode = this.OperationCode;
            }
            else
            {
                isNew = true;
            }

            //业务呈现
            if (!isNew)
            {
                if (this.State == RmsPM.Web.WorkFlowControl.ModuleState.Operable)
                {
                    this.DocumentFileFormView.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                    this.DocumentFileFormView.ChangeMode(FormViewMode.ReadOnly);

                   
                }
                this.DocumentFileObjectDataSource.SelectParameters.Clear();
                this.DocumentFileObjectDataSource.SelectParameters.Add("Code", Request.QueryString["ApplicationCode"]);
                //RmsPM.Web.UserControls.AttachMentList Attachmentlist1 = (RmsPM.Web.UserControls.AttachMentList)this.DocumentFileFormView.Row.FindControl("Attachmentlist1");
                //Attachmentlist1.MasterCode = this.ApplicationCode;

            }
            else
            {
                this.DocumentFileFormView.ChangeMode(FormViewMode.Insert);
            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "读取业务数据出错：" + ex.Message));
        }
    }
    /// <summary>
    /// 实现业务数据保存
    /// </summary>
    /// <returns></returns>
    public override string SubmitData()
    {

        if (this.DocumentFileFormView.CurrentMode == FormViewMode.Insert)
        {
            Web.SelectBox_SelectSessionUserUnit Unitddl = (Web.SelectBox_SelectSessionUserUnit)this.DocumentFileFormView.Row.FindControl("SelectSessionUserUnit");
            this.UnitCode = Unitddl.SelectedValue;
            this.DocumentFileFormView.InsertItem(true);

            TextBox txtSubject = (TextBox)this.DocumentFileFormView.Row.FindControl("SubjectTextBox");
            this.ApplicationTitle = txtSubject.Text;

        }
        else
        {
            if (this.DocumentFileFormView.CurrentMode == FormViewMode.Edit)
            {
                HiddenField UnitHF = (HiddenField)this.DocumentFileFormView.Row.FindControl("ApplyDepartmentCodeHiddenField");
                this.UnitCode = UnitHF.Value;
                this.DocumentFileFormView.UpdateItem(false);

                TextBox txtSubject = (TextBox)this.DocumentFileFormView.Row.FindControl("SubjectTextBox");
                this.ApplicationTitle = txtSubject.Text;

            }
        }
        return "";
    }
    /// <summary>
    /// 改变业务数据状态（流程中）
    /// </summary>
    /// <param name="dao"></param>
    /// <returns></returns>
    public override string ChangeStatusWhenSend(StandardEntityDAO dao)
    {
        try
        {
            base.ChangeStatusWhenSend(dao);
            string ErrMsg = "";
            if (!isNew)
            {
                //lm.ModifyAlreadyAuditing(int.Parse(this.OperationCode));
                DFBFL.AlreadyAuditing(int.Parse(this.OperationCode));
            }
            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "改变业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }
    /// <summary>
    ///  改变业务数据状态（退回）
    /// </summary>
    /// <returns></returns>
    public override string RestoreStatus()
    {
        try
        {
            base.RestoreStatus();
            string ErrMsg = "";
            //if (this.OperationCode != "new")
            //{
            //    DFBFL.ModifyNotAuditing(int.Parse(this.OperationCode));
            //}
            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "恢复业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }
    /// <summary>
    /// 业务审核
    /// </summary>
    public override bool Audit(string pm_sOpinionConfirm)
    {
        base.Audit(pm_sOpinionConfirm);

        string ErrMsg = "";

        if (pm_sOpinionConfirm != "")
        {

            switch (pm_sOpinionConfirm)
            {
                case "Approve"://批准                  
                    DFBFL.AuditingAgree(int.Parse(this.OperationCode));
                    //lm.ModifyPassAuditing(int.Parse(this.OperationCode));
                    break;
                case "Reject"://否决     
                    DFBFL.AuditingNoAgree(int.Parse(this.OperationCode));
                    //lm.ModifyNotPassAuditing(int.Parse(this.OperationCode));
                    break;
                case "Unknow":
                    ErrMsg = "请选择评审结果！";
                    break;
                default:
                    ErrMsg = "请选择评审结果！";
                    break;
            }

        }
        return true;
    }
    protected void DocumentFileObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.ucadd = (RmsPM.Web.UserControls.AttachMentAdd)this.DocumentFileFormView.Row.FindControl("Attachmentadd1");
        this.OperationCode = e.ReturnValue.ToString();
        this.ApplicationCode = e.ReturnValue.ToString();
        ucadd.SaveAttachMent(e.ReturnValue.ToString());
        
        this.DocumentFileObjectDataSource.SelectParameters.Clear();
        this.DocumentFileObjectDataSource.SelectParameters.Add("Code", e.ReturnValue.ToString());

        
    }
    protected void DocumentFileFormView_DataBound(object sender, EventArgs e)
    {

        HiddenField UserCodeHiddenField = (HiddenField)this.DocumentFileFormView.Row.FindControl("ApplyUserCodeHiddenField");
        Label UserCodeLabel = (Label)this.DocumentFileFormView.Row.FindControl("ApplyUserCodeLabel");
        switch (this.DocumentFileFormView.CurrentMode) 
        {
            case FormViewMode.Edit:
                UserCodeLabel.Text = WebFunctionRule.GetUserNameByCode(UserCodeHiddenField.Value);
                Label AppDepLabel = (Label)this.DocumentFileFormView.Row.FindControl("ApplyDepartmentCodeLabel");
                HiddenField AppDepHiddenField = (HiddenField)this.DocumentFileFormView.Row.FindControl("ApplyDepartmentCodeHiddenField");
                AppDepLabel.Text = RmsPM.BLL.SystemRule.GetUnitName(AppDepHiddenField.Value);
                break;
            case FormViewMode.Insert:
                User u = (User)Session["User"];
                UserCodeHiddenField.Value = u.UserCode;
                UserCodeLabel.Text = WebFunctionRule.GetUserNameByCode(u.UserCode);     
                string SortCode="";
                string MarkingSNCode="";
                if (!string.IsNullOrEmpty(FileTemplateCode)){
                    FileTemplateModel template = FTBFL.GetFileTemplate(int.Parse(FileTemplateCode));
                    if(template!=null)
                    {
                        SortCode=template.SortCode;
                        Web.SelectBox_SelectSessionUserUnit Unitddl = (Web.SelectBox_SelectSessionUserUnit)this.DocumentFileFormView.Row.FindControl("SelectSessionUserUnit");
                        MarkingSNCode =GetTempMarkingSN(Unitddl.SelectedValue,this.FileTemplateCode);
                    }
                }
                Label SCLabel = (Label)this.DocumentFileFormView.Row.FindControl("SortCodeLabel");//质量分类号

                SCLabel.Text =SortCode;
                TextBox DMSN = (TextBox)this.DocumentFileFormView.Row.FindControl("DoucmentMarkingSNLabel");//标识序列号                
                DMSN.Text = MarkingSNCode;
                break;
           
        }
    }
    protected void DocumentFileFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        //this.FileTemplateCode = Request["FileTemplateCode"];
        Web.SelectBox_SelectSessionUserUnit Unitddl = (Web.SelectBox_SelectSessionUserUnit)this.DocumentFileFormView.Row.FindControl("SelectSessionUserUnit");
        e.Values["ApplyDepartmentCode"] = Unitddl.SelectedValue;
        e.Values["OperationType"] = FTBFL.GetFileTemplate(int.Parse(this.FileTemplateCode)).FileTemplateName;
        e.Values["FileTemplateCode"]=int.Parse(this.FileTemplateCode);
        FTVQM.FileTemplateCodeEqual = int.Parse(this.FileTemplateCode);
        FTVQM.IsAvailabilityEqual = "有效";
        e.Values["VersionNumber"] = FTVBFL.GetFileTemplateVersionList(FTVQM)[0].VersionNumber;       
        e.Values["ArchiveState"] = "未归档";
        e.Values["CreateDate"] = DateTime.Now;
        e.Values["CreateUserCode"] = ((User)Session["User"]).UserCode;
        this.ApplicationTitle = e.Values["Subject"].ToString();
    }
    protected void DocumentFileFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        e.NewValues["LastModifyDatetime"] = DateTime.Now;
        e.NewValues["LastModifyByUserCode"] = ((User)Session["User"]).UserCode;
        if(e.NewValues["Subject"]!=null)
            this.ApplicationTitle = e.NewValues["Subject"].ToString();
    }
    /// <summary>
    /// 取得标识序列号

    /// </summary>
    /// <param name="UnitCode"></param>
    /// <param name="TemplateCode"></param>
    /// <returns></returns>
    private string GetTempMarkingSN(string UnitCode,string TemplateCode)
    {
        string MarkingSNCode = "";
        FTVQM.FileTemplateCodeEqual = int.Parse(TemplateCode);     
        FTVQM.IsAvailabilityEqual = "有效";
        IList<FileTemplateVersionModel> VersionList=FTVBFL.GetFileTemplateVersionList(FTVQM);
        if (VersionList.Count > 0)
        {
            string tmpNumber = VersionList[0].MarkingSNRule;
            string XX="";
            string YYMM = (DateTime.Today.Year).ToString().Substring(2).ToString() + DateTime.Today.Month.ToString();
            DocumentDirectoryQueryModel DDQ = new DocumentDirectoryQueryModel();
            DDQ.DepartmentCodeEqual= UnitCode;
            DDQ.FileTemplateCodeEqual = int.Parse(TemplateCode);
            IList<DocumentDirectoryModel> DirectoryList=DDBFL.GetDocumentDirectoryList(DDQ);
            if (DirectoryList.Count > 0)
            {
                XX = DirectoryList[0].DirectoryNodeCode;
            }
            //MarkingSNCode = tmpNumber.ToUpper().Replace("XX", XX).Replace("YYMM", YYMM);
            MarkingSNCode = tmpNumber.ToUpper().Replace("YYMM", YYMM).Substring(0,8);
        }
        return MarkingSNCode;
    }

    protected void SelectSessionUserUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string MarkingSNCode = GetTempMarkingSN(((Web.SelectBox_SelectSessionUserUnit)sender).SelectedValue, this.FileTemplateCode);
        TextBox DMSN = (TextBox)this.DocumentFileFormView.Row.FindControl("DoucmentMarkingSNLabel");//标识序列号                
        DMSN.Text = MarkingSNCode;
    }
}
