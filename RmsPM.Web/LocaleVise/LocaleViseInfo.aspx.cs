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
using System.Collections.Generic;
using RmsPM.Web.UserControls;

public partial class LocaleVise_LocaleViseInfo : RmsPM.Web.PageBase
{
    protected string _projectCode;
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request["Projectcode"] + string.Empty != string.Empty)
        {
            _projectCode = Request["Projectcode"];
        } 
        if (!IsPostBack)
        {
            if (!user.HasRight("220101"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }

            if (Request["ViseCode"] + "" != "")
                this.ViewState["ViseCode"] = Request["ViseCode"] + "";
            if (this.ViewState["ViseCode"] == null)
                FormView1.ChangeMode(FormViewMode.Insert);

            RmsPM.BFL.LocaleViseBFL vise = new RmsPM.BFL.LocaleViseBFL();
            if (Request["Projectcode"] + string.Empty != string.Empty)
            {
                _projectCode = Request["Projectcode"];
            }else{
                int viseCode;
                if(int.TryParse(this.ViewState["ViseCode"].ToString(),out viseCode)){
                    _projectCode=vise.GetLocalVise(viseCode)[0].ViseProject;
                }else{
                    _projectCode=string.Empty;
                }
            }
            this.btnAddDtl.Attributes["OnClick"] = "javascript:AddDtl('');return false;";
            this.btnAddDtl.Visible = (FormView1.CurrentMode != FormViewMode.Insert && (vise.GetLocalViseCosts(int.Parse(Request["ViseCode"].ToString())).Count == 0));
            this.WorkFlowList1.ProcedureNameAndApplicationCodeList = "'签证审核" + Request["ViseCode"] + "'";
            this.WorkFlowList1.DataBound();



            //前一条流程未结束，不允许在提交发送


            if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                int iCont = RmsPM.BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("签证审核", Request["ViseCode"] + "");
                if (iCont > 0)
                {
                    HtmlInputButton btnRequisition = ((HtmlInputButton)this.FormView1.Row.FindControl("btnRequisition"));
                    btnRequisition.Visible = false;
                }
            }
            else
            {
                if ("yefengpm" == this.up_sPMNameLower)
                {
                    ((RequiredFieldValidator)this.FormView1.Row.FindControl("RequiredFieldValidator4")).Enabled = true;
                }

            }
       
           
        }
    }
    private string getcode(string viseid)
    {
        
        string[] viseids = viseid.Split('-');
        if (viseids.Length == 5)
        {
            if (viseids[4].IndexOf('#') >= 0)//修改时，不需要再次获取code
            {
                string viseidnum = RmsPM.DAL.EntityDAO.SystemManageDAO.GetFormatSysCode("YFQZ" + _projectCode + viseids[1] + viseids[3], "{####}", 1001);
                return viseids[0] + "-" + viseids[1] + "-" + viseids[2] + "-" + viseids[3] + "-" + viseidnum;
            }
            else { return viseid; }
        }
        else
        {
            return viseid;
        }
    }
    /// <summary>
    /// 增加前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Cancel = InvalidInput();

        e.Values["ViseProject"] = _projectCode + "";
        string TempReasonStr = "";
        foreach (ListItem Item in ((CheckBoxList)this.FormView1.FindControl("CheckBoxList1")).Items)
        {
            if (Item.Selected)
                TempReasonStr += Item.Value + ",";
        }
        if (TempReasonStr.Length > 0) TempReasonStr = TempReasonStr.Remove(TempReasonStr.Length - 1, 1);
        e.Values["ViseReasonItem"] = TempReasonStr;
        if (this.up_sPMNameLower == "yefengpm")
        {
            string viseid = e.Values["ViseId"].ToString();
            e.Values["ViseId"] = getcode(viseid);
            
        }

      
        if (RmsPM.BLL.ConvertRule.ToString(e.Values["ViseReferCode"]) == "")
        {
            e.Values["ViseReferCode"] = 0;
        }
    }
    /// <summary>
    /// 增加后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
//        RmsPM.BFL.LocaleViseBFL vise = new RmsPM.BFL.LocaleViseBFL();
        Response.Write("<script>window.opener.location.reload();</script>");
        this.btnAddDtl.Visible = true;
    }
    /// <summary>
    /// 更新前

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        e.Cancel = InvalidInput();
        string TempReasonStr = "";
        foreach (ListItem Item in ((CheckBoxList)this.FormView1.FindControl("CheckBoxList1")).Items)
        {
            if (Item.Selected)
                TempReasonStr += Item.Value + ",";
        }
        if (TempReasonStr.Length > 0) TempReasonStr = TempReasonStr.Remove(TempReasonStr.Length - 1, 1);
        e.NewValues["ViseReasonItem"] = TempReasonStr;
        if (this.up_sPMNameLower == "yefengpm")
        {
            string viseid = e.NewValues["ViseId"].ToString();
            e.NewValues["ViseId"] = getcode(viseid);           
        }

        if (RmsPM.BLL.ConvertRule.ToString(e.NewValues["ViseReferCode"]) == "")
        {
            e.NewValues["ViseReferCode"] = 0;
        }
    }
    /// <summary>
    /// 更新后

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener=null;window.close();</script>");
    }
    /// <summary>
    /// 删除后

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }

    /// <summary>
    /// 增加后

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        /* Rms.LogHelper.LogHelper.Error(ObjectDataSource1.SelectParameters["Code"].ToString());
         Rms.LogHelper.LogHelper.Error(e.ReturnValue.ToString());
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        this.ViewState["ViseCode"] = e.ReturnValue.ToString();
         ((RmsPM.Web.UserControls.AttachMentAdd)FormView1.Row.FindControl("AttachMentAdd1")).SaveAttachMent(e.ReturnValue.ToString());
         ((RmsPM.Web.UserControls.AttachMentAdd)FormView1.Row.FindControl("AttachMentAdd2")).SaveAttachMent(e.ReturnValue.ToString());
         ((RmsPM.Web.UserControls.AttachMentAdd)FormView1.Row.FindControl("AttachMentAdd3")).SaveAttachMent(e.ReturnValue.ToString());*/
    }
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Balance")
        {
            RmsPM.BFL.LocaleViseBFL ViseBFL = new RmsPM.BFL.LocaleViseBFL();
            ViseBFL.Balance((int)FormView1.DataKey.Value);
        }
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        this.FormView1.DataBind();
        this.GridView1.DataBind();
        ButtonStatusCtrl();
    }
    /// <summary>
    /// 帮定FormView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
            Button btnBalance = ((Button)this.FormView1.Row.FindControl("btnBalance"));
            if (btnDelete != null)
                btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前签证吗？')";
            if (btnBalance != null)
                btnBalance.Attributes["OnClick"] = "javascript:return confirm('确实要结算当前签证吗？')";
            ButtonStatusCtrl();
            ((CheckBoxList)this.FormView1.FindControl("CheckBoxList1")).Enabled = false;
        }
        else if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            ((RmsPM.Web.UserControls.InputUser)this.FormView1.Row.FindControl("VisePersonTextBox")).Value = user.UserCode;
            if (!string.IsNullOrEmpty(user.BuildStationCodes()))
            {
                string[] station = user.BuildStationCodes().Split(new char[] { ',' });
                ((InputUnit)this.FormView1.Row.FindControl("ViseUnitTextBox")).Value = RmsPM.BLL.SystemRule.GetUnitByStationCode(station[0]);
            }//((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.FindControl("inputSystemGroup")).OnChange += new System.EventHandler(inputSystemGroup_OnChange);
        }
        else if (FormView1.CurrentMode == FormViewMode.Edit)
        {
            //((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.FindControl("inputSystemGroup")).OnChange += new System.EventHandler(inputSystemGroup_OnChange);
        }
        if (this.up_sPMNameLower == "yefengpm")
        {
            TextBox viseid = (System.Web.UI.WebControls.TextBox)this.FormView1.Row.FindControl("ViseIdTextBox");
            if (viseid != null)
            {
                viseid.Attributes.Add("readonly", "readonly");
            }
        }
    }

    /// <summary>
    /// 表单元素校验
    /// </summary>
    /// <returns>结果（false为校验通过）</returns>
    private bool InvalidInput()
    {
        try
        {
            bool ReturnCancel = false;
            if (((RmsPM.Web.UserControls.InputUnit)this.FormView1.Row.FindControl("ViseUnitTextBox")).Value == "")
            {
                ((HtmlGenericControl)this.FormView1.Row.FindControl("UnitMsgSpan")).InnerHtml = "<font color='red'>必填</font>";
                ReturnCancel = true;
            }
            else
            {
                ((HtmlGenericControl)this.FormView1.Row.FindControl("UnitMsgSpan")).InnerHtml = "";
            }
            if (((RmsPM.Web.UserControls.InputUser)this.FormView1.Row.FindControl("VisePersonTextBox")).Value == "")
            {
                ((HtmlGenericControl)this.FormView1.Row.FindControl("PersonMsgSpan")).InnerHtml = "<font color='red'>必填</font>";
                ReturnCancel = true;
            }
            else
            {
                ((HtmlGenericControl)this.FormView1.Row.FindControl("PersonMsgSpan")).InnerHtml = "";
            }
            if (((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("InputSystemGroup")).Value == "")
            {
                ((HtmlGenericControl)this.FormView1.Row.FindControl("SystemGroupSpan")).InnerHtml = "<font color='red'>必填</font>";
                ReturnCancel = true;
            }
            else
            {
                ((HtmlGenericControl)this.FormView1.Row.FindControl("SystemGroupSpan")).InnerHtml = "";
            }

            //签证编号不能重复
            string sql = "";
            if (FormView1.CurrentMode == FormViewMode.Insert)
            {
                sql = "select visecode,viseid from localevise where viseproject='" + _projectCode + "'";
            }
            else if (FormView1.CurrentMode == FormViewMode.Edit)
            {
                sql = "select visecode,viseid from localevise where visecode!=" + Convert.ToInt32(FormView1 .DataKey.Value.ToString()+ "") + " and viseproject='" + _projectCode + "'";
            }

            Rms.ORMap.QueryAgent qa = new Rms.ORMap.QueryAgent();
            DataSet ds = qa.ExecSqlForDataSet(sql);
            qa.Dispose();
            TextBox txtViseID;
            if (FormView1.CurrentMode == FormViewMode.Insert)
            {
                txtViseID = ((TextBox)this.FormView1.Row.FindControl("ViseIdTextBox"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["viseid"].ToString().Trim() == txtViseID.Text.Trim())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "签证编号重复"));
                        ReturnCancel = true;
                        break;
                    }
                }
            }

            if (FormView1.CurrentMode == FormViewMode.Edit)
            {

                txtViseID = ((TextBox)this.FormView1.Row.FindControl("ViseIdTextBox"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["viseid"].ToString().Trim() == txtViseID.Text.Trim())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "签证编号重复"));
                        ReturnCancel = true;
                        break;

                    }
                }
            }
            return ReturnCancel;
        }
        catch (Exception ex)
        {
            RmsPM.Web.LogHelper.Error(ex.ToString());
            Response.Write(Rms.Web.JavaScript.Alert(true, "表单元素校验出错：" + ex.Message));
            return false;
        }
    }
    /// <summary>
    /// 按钮状态控制




    /// </summary>
    private void ButtonStatusCtrl()
    {
        RmsPM.BFL.LocaleViseBFL vise = new RmsPM.BFL.LocaleViseBFL();
        Button btnModify = ((Button)this.FormView1.Row.FindControl("btnModify"));
        Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
        HtmlInputButton btnRequisition = ((HtmlInputButton)this.FormView1.Row.FindControl("btnRequisition"));
        HtmlInputButton btnAudit = ((HtmlInputButton)this.FormView1.Row.FindControl("btnAudit"));
        HtmlInputButton btnPrint = ((HtmlInputButton)this.FormView1.Row.FindControl("btnPrint"));
        Button btnBalance = ((Button)this.FormView1.Row.FindControl("btnBalance"));

        //结算状态





        if (RmsPM.BFL.LocaleViseBFL.GetBalanceStatus((int)FormView1.DataKey.Value) == TiannuoPM.MODEL.ViseBalanceStatusEnum.isbalance)
        {
            btnModify.Visible = false;
            btnAddDtl.Visible = false;
            btnDelete.Visible = false;
            btnRequisition.Visible = false;
            btnAudit.Visible = false;
            btnPrint.Visible = true;
            btnBalance.Visible = false;
            GridView1.Columns[0].Visible = false;
        }
        else
        {
            //审核状态





            switch (RmsPM.BFL.LocaleViseBFL.GetStatus((int)FormView1.DataKey.Value))
            {
                case TiannuoPM.MODEL.ViseStatusEnum.wait:
                    btnModify.Visible = true;
                    if (FormView1.CurrentMode == FormViewMode.ReadOnly)
                        btnAddDtl.Visible = (vise.GetLocalViseCosts((int)FormView1.DataKey.Value).Count == 0);
                    else
                        btnAddDtl.Visible = false;
                    btnDelete.Visible = true;

                    btnRequisition.Visible = true;
                    btnAudit.Visible = true;
                    btnPrint.Visible = false;
                    btnBalance.Visible = false;
                    break;
                case TiannuoPM.MODEL.ViseStatusEnum.process:
                    btnModify.Visible = false;
                    btnAddDtl.Visible = false;
                    btnDelete.Visible = false;
                    btnRequisition.Visible = false;
                    btnAudit.Visible = true;
                    btnPrint.Visible = false;
                    btnBalance.Visible = false;
                    GridView1.Columns[0].Visible = false;
                    break;
                case TiannuoPM.MODEL.ViseStatusEnum.ispass:
                    btnModify.Visible = false;
                    btnAddDtl.Visible = false;
                    btnDelete.Visible = false;
                    btnRequisition.Visible = false;
                    btnAudit.Visible = false;
                    btnPrint.Visible = true;
                    btnBalance.Visible = true;
                    GridView1.Columns[0].Visible = false;
                    break;
                case TiannuoPM.MODEL.ViseStatusEnum.nopass:
                    btnModify.Visible = false;
                    btnAddDtl.Visible = false;
                    btnDelete.Visible = false;
                    btnRequisition.Visible = false;
                    btnAudit.Visible = false;
                    btnPrint.Visible = true;
                    btnBalance.Visible = false;
                    GridView1.Columns[0].Visible = false;
                    break;
            }
        }
        if (!user.HasRight("220103"))
        {
            btnModify.Visible = false;
            btnAddDtl.Visible = false;
            GridView1.Columns[0].Visible = false;
        }
        if (!user.HasRight("220104"))
            btnAudit.Visible = false;
        if (!user.HasRight("220105"))
            btnBalance.Visible = false;
        //if (!user.HasRight("220106"))
        if (!user.HasRight("220107"))
            btnDelete.Visible = false;
        if (!user.HasRight("220108"))
            btnRequisition.Visible = false;
        //if (!user.HasRight("220109"))
        if (!user.HasRight("220110"))
            btnPrint.Visible = false;
    }
    protected void CheckBoxList1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.DataItem != null)
        {
            string TypeName = RmsPM.BLL.SystemGroupRule.GetSystemGroupName(((TiannuoPM.MODEL.LocaleViseModel)this.FormView1.DataItem).ViseType);
            XmlDataSource1.XPath = "Reason/Type[@Name='" + TypeName + "']/Item";
            foreach (ListItem Item in ((CheckBoxList)this.FormView1.FindControl("CheckBoxList1")).Items)
            {
                string tmp = ((TiannuoPM.MODEL.LocaleViseModel)this.FormView1.DataItem).ViseReasonItem;
                if (tmp != null)
                {
                    if (tmp.IndexOf(Item.Value) != -1)
                        Item.Selected = true;
                }
            }
        }
    }
    protected void inputSystemGroup_OnChange(object sender, EventArgs e)
    {
        string TypeName = ((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.FindControl("inputSystemGroup")).Text;
        XmlDataSource1.XPath = "Reason/Type[@Name='" + TypeName + "']/Item";
        ((CheckBoxList)this.FormView1.FindControl("CheckBoxList1")).DataBind();
    }


    private string AutoRunViseID(string contractCode)
    {
        if (contractCode == "" || contractCode==null) return "";

        string strFirstTemp = "";
        string strNextTemp = "";
        int iFirstTemp = 0;
        int iNextTemp = 0;
        string contractID = "";

        RmsPM.BFL.LocaleViseBFL bfl = new RmsPM.BFL.LocaleViseBFL();
        TiannuoPM.MODEL.LocaleViseQueryModel querymodel = new TiannuoPM.MODEL.LocaleViseQueryModel();
        List<TiannuoPM.MODEL.LocaleViseModel> models = new List<TiannuoPM.MODEL.LocaleViseModel>();


        querymodel.ViseProject = _projectCode + "";
        querymodel.ViseContractCode = contractCode;
        models = bfl.GetLocalVises(querymodel);

        
        if (models.Count>0)
        {

            strFirstTemp = models[0].ViseId.ToString().Substring(models[0].ViseId.ToString().Length - 3,3); ;
            try
            {
                iFirstTemp = Convert.ToInt32(strFirstTemp);
            }
            catch
            {
                iFirstTemp = 1; //如果第一个是非数字，则设置默认值


            }

            for (int k = 1; k < models.Count; k++)
            {
                strNextTemp = models[k].ToString().Substring(models[k].ViseId.ToString().Length - 3,3); ;
                try
                {
                    iNextTemp = Convert.ToInt32(strNextTemp);
                }
                catch
                {
                    ; //如果是非数字，则滤过
                }

                if (iFirstTemp <= iNextTemp)
                {
                    iFirstTemp = iNextTemp;  
                }
            }
            strFirstTemp = Convert.ToString(iFirstTemp + 1).PadLeft(3, '0');

        }
        else
        {
            strFirstTemp = "001";
        }
        contractID = RmsPM.BLL.ContractRule.GetContractID(contractCode);  //合同编号可能是空字符串，要注意


        return contractID+strFirstTemp;
    }
}
