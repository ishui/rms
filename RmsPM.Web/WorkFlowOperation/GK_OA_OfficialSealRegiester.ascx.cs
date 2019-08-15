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

using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;
using RmsPM.Web;

public partial class WorkFlowOperation_GK_OA_OfficialSealRegiester : WorkFlowOperationBase
{
    private RmsOA.BFL.GK_OA_OfficialSealRegiesterBFL bfl = new RmsOA.BFL.GK_OA_OfficialSealRegiesterBFL();
    /// <summary>
    /// 装载控件数据
    /// </summary>
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
                return;
            }
            this.ObjectDataSource1.SelectParameters["Code"].DefaultValue = this.ApplicationCode;
            FormView1.DataBind();
            //业务流程属性保存


        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "读取业务数据出错：" + ex.Message));
        }
    }

    /// <summary>
    /// 保存控件数据
    /// </summary>
    public override string SubmitData()
    {
        try
        {
            string ErrMsg = "";
            if (this.UserCode == "")
            {
                ErrMsg = "操作用户为空";
                return ErrMsg;
            }
            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
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
            if (this.OperationCode != "new")
            {
                bfl.ModifyNotAuditing(int.Parse(this.OperationCode));

            }
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

            bfl.ModifyAlreadyAuditing(int.Parse(this.OperationCode));
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
    /// 业务审核(有审核权限的人)
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

                    bfl.ModifyPassAuditing(int.Parse(this.OperationCode));
                    break;
                case "Reject"://否决                   
                    bfl.ModifyNotPassAuditing(int.Parse(this.OperationCode));
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        RmsOA.MODEL.GK_OA_OfficialSealRegiesterModel manpowerneedModel = ((List<RmsOA.MODEL.GK_OA_OfficialSealRegiesterModel>)e.ReturnValue)[0];
        this.ApplicationTitle =manpowerneedModel.Detail+"-公章使用流程审批";
        this.UnitCode = manpowerneedModel.UnitCode;

    }
    /// <summary>
    /// 作废
    /// </summary>
    public void BlankOut()
    {
        try
        {
            bfl.ModifyBankOutAuditing(int.Parse(this.OperationCode));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Label tbxUnit = (Label)this.FormView1.Row.FindControl("UnitLabel");
            tbxUnit.Text = RmsPM.BLL.SystemRule.GetUnitName(tbxUnit.Text);
        }
    }
}
