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

using Rms.ORMap;
using RmsPM.Web;

public partial class WorkFlowOperation_XZ_EvectionApply : WorkFlowOperationBase
{
    private RmsOA.BFL.GK_OA_EvectionApplyBFL bfl = new RmsOA.BFL.GK_OA_EvectionApplyBFL();
    /// <summary>
    /// װ�ؿؼ�����
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
            //ҵ���������Ա���


        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡҵ�����ݳ���" + ex.Message));
        }
    }

    /// <summary>
    /// ����ؼ�����
    /// </summary>
    public override string SubmitData()
    {
        try
        {
            string ErrMsg = "";
            if (this.UserCode == "")
            {
                ErrMsg = "�����û�Ϊ��";
                return ErrMsg;
            }
            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
            throw ex;
        }
    }

    /// <summary>
    ///  �ı�ҵ������״̬���˻أ�
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
            Response.Write(Rms.Web.JavaScript.Alert(true, "�ָ�ҵ������״̬����" + ex.Message));
            throw ex;
        }
    }

    /// <summary>
    /// �ı�ҵ������״̬�������У�
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
            Response.Write(Rms.Web.JavaScript.Alert(true, "�ı�ҵ������״̬����" + ex.Message));
            throw ex;
        }
    }

    /// <summary>
    /// ҵ�����(�����Ȩ�޵���)
    /// </summary>
    public override bool Audit(string pm_sOpinionConfirm)
    {
        base.Audit(pm_sOpinionConfirm);

        string ErrMsg = "";

        if (pm_sOpinionConfirm != "")
        {
            switch (pm_sOpinionConfirm)
            {
                case "Approve"://��׼                  

                    bfl.ModifyPassAuditing(int.Parse(this.OperationCode));
                    break;
                case "Reject"://���                   
                    bfl.ModifyNotPassAuditing(int.Parse(this.OperationCode));
                    break;
                case "Unknow":
                    ErrMsg = "��ѡ����������";
                    break;
                default:
                    ErrMsg = "��ѡ����������";
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
        RmsOA.MODEL.GK_OA_EvectionApplyModel manpowerneedModel = ((List<RmsOA.MODEL.GK_OA_EvectionApplyModel>)e.ReturnValue)[0];
        this.ApplicationTitle = "Ա����������";
        this.UnitCode = manpowerneedModel.Dept;

    }
    /// <summary>
    /// ����
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
            Response.Write(Rms.Web.JavaScript.Alert(true, "���ϳ���" + ex.Message));
            throw ex;
        }
    }


}
