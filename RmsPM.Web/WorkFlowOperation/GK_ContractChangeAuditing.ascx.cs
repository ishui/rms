
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using RmsPM.Web.WorkFlowControl;
	using Rms.ORMap;
    using RmsPM.Web;

	/// <summary>
	///		GK_ContractChangeAuditing ��ժҪ˵����
	/// </summary>
public partial class WorkFlowOperation_GK_ContractChangeAuditing : WorkFlowOperationBase
{

 

    #region --- �������� ---

    /// <summary>
    /// �ؼ���ʼ��
    /// </summary>
    public override void InitControl()
    {
        try
        {
            base.InitControl();

            switch (this.AttachmentState)
            {
                case ModuleState.Sightless://���ɼ���
                    trOperAttachment.Visible = false;
                    trEyeAttachment.Visible = false;
                    break;

                case ModuleState.Begin://�ɼ���
                case ModuleState.End://�ɼ���
                case ModuleState.Operable://�ɼ���
                case ModuleState.Eyeable://�ɼ���
                    trOperAttachment.Visible = true;
                    trEyeAttachment.Visible = true;
                    break;

                default:
                    trOperAttachment.Visible = false;
                    trEyeAttachment.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }


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

            this.ContractCode = RmsPM.BLL.ContractRule.GetContractCodeByChangeCode(this.OperationCode);

            EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);

            entity.SetCurrentTable("Contract");

            if (!entity.HasRecord())
            {
                return;
            }

            this.ProjectCode = entity.GetString("ProjectCode");
            this.ApplicationType = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("Type"));
            this.ApplicationTitle = entity.GetString("ContractName");

            string ud_sHyperLinkFormat = "../Contract/ContractInfo.aspx?ContractCode={0}&ProjectCode={1}";

            decimal ud_deMoney, ud_deTotalChangeMoney, ud_deChangeMoney;

            ud_deMoney = decimal.Zero;
            ud_deTotalChangeMoney = decimal.Zero;
            ud_deChangeMoney = decimal.Zero;


            //��ͬ������Ϣ
            lblProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(this.ProjectCode);
            lblContractID.Text = entity.GetString("ContractID");

            lblOperContractName.Text = ShowApplicationHyperLink(entity.GetString("ContractName"), string.Format(ud_sHyperLinkFormat, this.ContractCode, this.ProjectCode)); 
            lblEyeContractName.Text = lblOperContractName.Text;

            lblOperSupplierName.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));
            lblEyeSupplierName.Text = lblOperSupplierName.Text;

            lblOperSupplier2Name.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("Supplier2Code"));
            lblEyeSupplier2Name.Text = lblOperSupplier2Name.Text;

            lblOperBuilding.Text = entity.GetString("Building");
            lblEyeBuilding.Text = lblOperBuilding.Text;

            //��ʾ��ͬ���
            decimal TotalMoney, OriginalMoney, NewTotalMoney, BudgetMoney, AdjustMoney;

            TotalMoney = entity.GetDecimal("TotalMoney");
            OriginalMoney = entity.GetDecimal("OriginalMoney");
            BudgetMoney = entity.GetDecimal("BudgetMoney");
            AdjustMoney = entity.GetDecimal("AdjustMoney");

            NewTotalMoney = TotalMoney;

            ud_deMoney = TotalMoney;

            foreach (DataRow dr in entity.Tables["ContractChange"].Select(string.Format("ContractChangeCode={0}", this.ApplicationCode), "", System.Data.DataViewRowState.CurrentRows))
            {
                NewTotalMoney = (decimal)dr["NewMoney"];

                ud_deTotalChangeMoney = (decimal)dr["TotalChangeMoney"];
                ud_deChangeMoney = (decimal)dr["ChangeMoney"];
            }


             

            switch (this.MoneyState)
            {
                case ModuleState.Sightless://���ɼ���
                case ModuleState.Begin://���ɼ���
                case ModuleState.End://���ɼ���

                    txtOperOriginalMoney.Value = "***************";
                    txtOperTotalChangeMoney.Value = "***************";
                    txtOperChangeMoney.Value = "***************";
                    txtOperNewTotalMoney.Value = "***************";
                    txtOperBudgetMoney.Value = "***************";
                    txtOperAdjustMoney.Value = "***************";
                    break;

                case ModuleState.Operable://�ɲ�����
                case ModuleState.Eyeable://�ɼ���

                    txtOperOriginalMoney.Value = OriginalMoney.ToString("N");
                    txtOperTotalChangeMoney.Value = ud_deTotalChangeMoney.ToString("N");
                    txtOperChangeMoney.Value = ud_deChangeMoney.ToString("N");
                    txtOperNewTotalMoney.Value = NewTotalMoney.ToString("N");
                    txtOperBudgetMoney.Value = BudgetMoney.ToString("N");
                    txtOperAdjustMoney.Value = AdjustMoney.ToString("N");
                    break;

                default:
                    tabOperMoney.Visible = false;
                    tabEyeMoney.Visible = false;
                    break;
            }



            txtEyeOriginalMoney.Value = txtOperOriginalMoney.Value;
            txtEyeTotalChangeMoney.Value = txtOperTotalChangeMoney.Value;
            txtEyeChangeMoney.Value = txtOperChangeMoney.Value;
            txtEyeNewTotalMoney.Value = txtOperNewTotalMoney.Value;
            txtEyeBudgetMoney.Value = txtOperBudgetMoney.Value;
            txtEyeAdjustMoney.Value = txtOperAdjustMoney.Value;


            //��ͬ�����Ϣ
            entity.SetCurrentTable("ContractChange");
            foreach (DataRow dr in entity.CurrentTable.Select(String.Format("ContractChangeCode='{0}'", this.ApplicationCode)))
            {
                txtOperVoucher.Value = dr["Voucher"].ToString();
                lblEyeVoucher.Text = txtOperVoucher.Value;

                txtOperChangeId.Value = dr["ContractChangeId"].ToString();
                lblEyeChangeId.Text = txtOperChangeId.Value;

                txtOperChangeReason.Value = dr["ChangeReason"].ToString();
                lblEyeChangeReason.Text = HttpUtility.HtmlEncode(txtOperChangeReason.Value).Replace("\n", "<br>");

                txtOperSupplierChangeMoney.Value = dr["SupplierChangeMoney"].ToString();
                lblEyeSupplierChangeMoney.Text = txtOperSupplierChangeMoney.Text;

                txtOperConsultantAuditMoney.Value = dr["ConsultantAuditMoney"].ToString();
                lblEyeConsultantAuditMoney.Text = txtOperConsultantAuditMoney.Text;

                txtOperProjectAuditMoney.Value = dr["ProjectAuditMoney"].ToString();
                lblEyeProjectAuditMoney.Text = txtOperProjectAuditMoney.Text;
            }

            entity.Dispose();

            //ҵ���������Ա���
            SaveOperationProperty("��ͬ���", ud_deMoney.ToString());
            SaveOperationProperty("��һ���", ud_deChangeMoney.ToString());
            SaveOperationProperty("�ۼƱ��", ud_deTotalChangeMoney.ToString()); 

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
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

            EntityData entity = new EntityData("Standard_Contract");

            if (UserCode == "")
            {
                ErrMsg = "�����û�Ϊ��";
                return ErrMsg;
            }

            if (dao == null)
            {
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);
            }
            else
            {
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode, dao);
            }


            //��ͬ���������Ϣ
            entity.SetCurrentTable("ContractChange");

            foreach (DataRow dr in entity.Tables["ContractChange"].Select(String.Format("ContractChangeCode='{0}'", this.ApplicationCode), "", DataViewRowState.CurrentRows))
            {
                dr["Voucher"] = txtOperVoucher.Value;
                dr["ContractChangeId"] = txtOperChangeId.Value;
                dr["ChangeReason"] = txtOperChangeReason.Value;

                dr["SupplierChangeMoney"] = txtOperSupplierChangeMoney.ValueDecimal;
                dr["ConsultantAuditMoney"] = txtOperConsultantAuditMoney.ValueDecimal;
                dr["ProjectAuditMoney"] = txtOperProjectAuditMoney.ValueDecimal;

                // ��¼����ˡ����ʱ��
                dr["ChangePerson"] = this.UserCode;
                dr["ChangeDate"] = DateTime.Now.ToString("yyyy-MM-dd");

//                dr["Status"] = 2;
            }

            dao.SubmitEntity(entity);
            entity.Dispose();

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
    /// ҵ�����
    /// </summary>
    public override bool Audit(string pm_sOpinionConfirm)
    {
        base.Audit(pm_sOpinionConfirm);

        try
        {

            string ErrMsg = "";

            if (pm_sOpinionConfirm != "")
            {

                EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode, this.dao);

                entity.SetCurrentTable("Contract");

                if (entity.HasRecord())
                {

                    switch (pm_sOpinionConfirm)
                    {
                        case "Approve":
                            RmsPM.BLL.ContractRule.ContractChangeAuditing(entity, this.OperationCode);
                            break;
                        case "Reject":
                            foreach (DataRow drCostChange in entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}'", this.OperationCode), "", DataViewRowState.CurrentRows))
                            {
                                drCostChange["Status"] = 1;
                            }

                            foreach (DataRow drChange in entity.Tables["ContractChange"].Select(String.Format("ContractChangeCode='{0}'", this.OperationCode), "", DataViewRowState.CurrentRows))
                            {
                                drChange["Status"] = 1;

                                foreach (DataRow dr in entity.Tables["Contract"].Select(String.Format("ContractCode='{0}'", this.ContractCode), "", DataViewRowState.CurrentRows))
                                {
                                    dr["ChangeStatus"] = 1;
                                }
                            }
                            break;
                        case "Unknow":
                            ErrMsg = "��ѡ����������";
                            break;
                        default:
                            ErrMsg = "��ѡ����������";
                            break;
                    }


                    if (ErrMsg != "")
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                        return false;
                    }

                    RmsPM.DAL.EntityDAO.ContractDAO.UpdateStandard_Contract(entity);

                }

                entity.Dispose();
            }

            return true;

        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "ҵ����˳���" + ex.Message));
            throw ex;
        }
    }

    public override string ChangeStatusWhenSend(StandardEntityDAO dao)
    {
        try
        {

            base.ChangeStatusWhenSend(dao);

            string ErrMsg = "";

            RmsPM.BLL.ContractRule.ContractChangeStatusChange(dao, this.OperationCode, 2);

            return ErrMsg;
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "�ı�ҵ������״̬����" + ex.Message));
            throw ex;
        }

    }

    public override string RestoreStatus()
    {
        try
        {

            base.RestoreStatus();

            string ErrMsg = "";

            RmsPM.BLL.ContractRule.ContractChangeStatusChange(dao, this.OperationCode, 1);

            return ErrMsg;
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "�ָ�ҵ������״̬����" + ex.Message));
            throw ex;
        }
    }

    #endregion --- �������� ---

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // �ڴ˴������û������Գ�ʼ��ҳ��
        if (this.ApplicationCode != "")
        {
            this.myOperAttachMentAdd.AttachMentType = "ContractChangeAttachMent";
            this.myOperAttachMentAdd.MasterCode = this.ApplicationCode;

            this.myEyeAttachMentList.AttachMentType = "ContractChangeAttachMent";
            this.myEyeAttachMentList.MasterCode = this.ApplicationCode;
        }
    }

  
}