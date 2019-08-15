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
using System.Text;

using RmsPM.Web;
using RmsOA.MODEL;
using RmsOA.BFL;


public partial class RmsOA_XZ_MeetSummaryEdit : PageBase
{
    private RmsPM.Web.UserControls.InputUnit ucDept;
    private AspWebControl.Calendar dateTime;
    private DropDownList ddlType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                if (Request.QueryString["Type"].Equals("Add"))
                {
                    this.MeetFormView.ChangeMode(FormViewMode.Insert);
                }
            }

            //如果单据不是申请状态，则控制修改/删除/提交/作废等按钮
            RmsOA.BFL.GK_OA_MeetSummaryBFL bfl = new RmsOA.BFL.GK_OA_MeetSummaryBFL();
            RmsOA.MODEL.GK_OA_MeetSummaryModel model = new RmsOA.MODEL.GK_OA_MeetSummaryModel();
            model = bfl.GetGK_OA_MeetSummary(Convert.ToInt32(Request["Code"]));
            if (model.Status != "0")
            {
                if (this.MeetFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    HtmlInputButton btnRequisition = ((HtmlInputButton)this.MeetFormView.Row.FindControl("btnRequisition"));
                    btnRequisition.Visible = false;

                    this.MeetFormView.Row.FindControl("EditButton").Visible = false;
                    this.MeetFormView.Row.FindControl("DeleteButton").Visible = false;
                    this.MeetFormView.Row.FindControl("btnBankOut").Visible = false;
                }
            }
        }
    }

    protected void MeetFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["Status"] = "0";
        e.Values["Submiter"] = user.UserCode;
        e.Values["SubmitTime"] = DateTime.Now.ToString();
        e.Values["SendStatus"] = "0";
    }
    protected void MeetFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
    }
    protected void MeetFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.location=window.opener.location;window.opener.location.reload();</script>");
    }

    /// <summary>
    /// 转化FormView到编辑状态
    /// </summary>
    protected void EditButton_Click(object sender, EventArgs e)
    {
        this.MeetFormView.ChangeMode(FormViewMode.Edit);
    }

    /// <summary>
    /// 删除后，关闭当前页,刷新父窗口。
    /// </summary>
    protected void MeetFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location = window.opener.location ;window.opener.location.reload();window.close();</script>");
        Response.End();
    }

    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        this.MeetFormView.DataBind();
    }


    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_MeetSummaryBFL bfl = new RmsOA.BFL.GK_OA_MeetSummaryBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.MeetFormView.DataKey.Value.ToString()));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }
    protected void MeetFormView_DataBound(object sender, EventArgs e)
    {
        if (this.MeetFormView.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("320202"))
            {
                Button btEdit = (Button)(this.MeetFormView.Row.FindControl("EditButton"));
                btEdit.Visible = false;
            }
            if (!user.HasRight("320203"))
            {
                Button btDelete = (Button)(this.MeetFormView.Row.FindControl("DeleteButton"));
                btDelete.Visible = false;
            }
            if (!user.HasRight("320204"))
            {
                HtmlInputButton btSubmit = (HtmlInputButton)(this.MeetFormView.Row.FindControl("btnRequisition"));
                btSubmit.Visible = false;
            }
            if (!user.HasRight("320205"))
            {
                Button btBankOut = (Button)(this.MeetFormView.Row.FindControl("btnBankOut"));
                btBankOut.Visible = false;
            }
            AttendPersonLabel.Text = ChangeCodeToName(HidAttendPerson.Value);
            OtherAttendPersonLabel.Text = ChangeCodeToName(HidOtherAttend.Value);
            WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.MeetFormView.Row.FindControl("WorkFlowList1");
            work.ProcedureNameAndApplicationCodeList = "'会议纪要审批" + this.MeetFormView.DataKey.Value.ToString() + "'";
            work.DataBound();
        }
    }
    protected void MeetFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string meetType = MeetTypeLabel.Text;
        string workFlowUrl = RmsPM.BLL.WorkFlowRule.GetProcedureURLByName(string.Format("{0}会议纪要审批", meetType));
        if (string.IsNullOrEmpty(workFlowUrl))
        {
            Response.Write("<script>window.alert('未找到流程，请检测配置');</script>");
        }
        else
        {
            string format = "width=1000,height=620,fullscreen=0,left=10,top=10,menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no";
            string url = string.Format(
                "{0}?Code={1}&MeetType={2}"
                ,workFlowUrl
                , this.MeetFormView.DataKey.Value
                , meetType
                );
            Response.Write(string.Format("<script>window.open('{0}','','{1}');</script>", url,format));
        }
    }

    public Label MeetTypeLabel
    {
        get
        {
            Label lbl = (Label)(MeetFormView.Row.FindControl("TypeLabel"));
            return lbl;
        }
    }
    public Label AttendPersonLabel
    {
        get
        {
            Label lbl = (Label)(this.MeetFormView.Row.FindControl("AttendPersonsLabel"));
            return lbl;
        }
    }
    public Label OtherAttendPersonLabel
    {
        get
        {
            Label lbl = (Label)(this.MeetFormView.Row.FindControl("OtherPersonLabel"));
            return lbl;
        }
    }
    public HiddenField HidAttendPerson
    {
        get 
        {
            HiddenField hid = (HiddenField)MeetFormView.Row.FindControl("HidAttendPerson");
            return hid;
        }
    }
    public HiddenField HidOtherAttend
    {
        get
        {
            HiddenField hid = (HiddenField)MeetFormView.Row.FindControl("HidOtherAttend");
            return hid;
        }
    }

    public string ChangeCodeToName(string userCodes)
    {
        StringBuilder builder = new StringBuilder();
        string[] tempAraay = userCodes.Split(',');
        foreach (string s in tempAraay)
        {
            builder.Append(RmsPM.BLL.SystemRule.GetUserName(s));
            builder.Append(',');
        }
        return builder.ToString();
    }
}
