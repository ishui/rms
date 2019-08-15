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


public partial class WorkFlowOperation_GK_LocaleViseAuditing : WorkFlowOperationBase
{
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
            GridView1.DataBind();
            //业务流程属性保存



            //SaveOperationProperty("合同金额", ud_deMoney.ToString());

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

    public override string ChangeStatusWhenSend(StandardEntityDAO dao)
    {
        try
        {
            base.ChangeStatusWhenSend(dao);
            RmsPM.BFL.LocaleViseBFL ViseBFL = new RmsPM.BFL.LocaleViseBFL();
            ViseBFL.StartAudit(int.Parse(this.OperationCode));
            return "";
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "改变业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }
    public override string RestoreStatus()
    {
        try
        {
            base.RestoreStatus();
            RmsPM.BFL.LocaleViseBFL ViseBFL = new RmsPM.BFL.LocaleViseBFL();
            ViseBFL.ReturnWait(int.Parse(this.OperationCode));
            return "";
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

        try
        {

            string ErrMsg = "";

            if (pm_sOpinionConfirm != "")
            {
                RmsPM.BFL.LocaleViseBFL ViseBFL = new RmsPM.BFL.LocaleViseBFL();
                switch (pm_sOpinionConfirm)
                {
                    case "Approve":
                        ViseBFL.PassAudit(int.Parse(this.OperationCode), ViseBFL.GetLocalViseCosts(int.Parse(this.OperationCode)));
                        ViseBFL.UpdateComeToMoney(int.Parse(this.OperationCode), RmsPM.BFL.LocaleViseBFL.GetViseSumMoney(int.Parse(this.OperationCode)));
                        break;
                    case "Reject":
                        ViseBFL.StartAudit(int.Parse(this.OperationCode));
                        break;
                    case "Unknow":
                        ErrMsg = "请选择评审结果！";
                        break;
                    default:
                        ErrMsg = "请选择评审结果！";
                        break;
                }

                if (ErrMsg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                    return false;
                }

            }

            return true;

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "业务审核出错：" + ex.Message));
            throw ex;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        TiannuoPM.MODEL.LocaleViseModel ViseModel = ((List<TiannuoPM.MODEL.LocaleViseModel>)e.ReturnValue)[0];
        this.ApplicationTitle = ViseModel.ViseName + "评审";
        this.ProjectCode = ViseModel.ViseProject;
        this.ApplicationType = ViseModel.ViseType;
        this.UnitCode = ViseModel.ViseUnit;

        SaveOperationProperty("签证金额", RmsPM.BFL.LocaleViseBFL.GetViseSumMoney(ViseModel.ViseCode).ToString());
    }
    /// <summary>
    /// 作废
    /// </summary>
    public void BlankOut()
    {
        try
        {
            RmsPM.BFL.LocaleViseBFL ViseBFL = new RmsPM.BFL.LocaleViseBFL();
            ViseBFL.NoPassAudit(int.Parse(this.OperationCode));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }
}
