using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using RmsPM.Web;

	/// <summary>
	///		GK_ContractAuditing ��ժҪ˵����
	/// </summary>
public partial class WorkFlowOperation_GK_ContractAuditing : WorkFlowOperationBase
{



    #region --- �������� ---

    /// <summary>
    /// �ؼ���ʼ��
    /// </summary>
    public override void InitControl()
    {
        try
        {
            this.inputOperSystemGroup.ClassCode = "0501";
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

            this.ContractCode = this.OperationCode;

            EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);

            entity.SetCurrentTable("Contract");

            if (!entity.HasRecord())
            {
                return;
            }

            this.ProjectCode = entity.GetString("ProjectCode");
            this.ApplicationTitle = entity.GetString("ContractName");
            this.ApplicationType = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("Type"));


            string ud_sHyperLinkFormat = "../Contract/ContractInfo.aspx?ContractCode={0}&ProjectCode={1}";

            //��ͬ������Ϣ
            lblOperProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(this.ProjectCode);
            lblEyeProjectName.Text = lblOperProjectName.Text;

            txtOperContractID.Value = entity.GetString("ContractID");
            lblEyeContractID.Text = txtOperContractID.Value;

            txtOperContractName.Value = entity.GetString("ContractName");
            lblEyeContractName.Text = ShowApplicationHyperLink(entity.GetString("ContractName"), string.Format(ud_sHyperLinkFormat, this.ContractCode, this.ProjectCode));

            inputOperSystemGroup.Value = entity.GetString("Type");
            lblEyeSystemGroupName.Text = inputOperSystemGroup.Text;

            txtOperSupplierCode.Value = entity.GetString("SupplierCode");
            txtOperSupplierName.Value = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));
            lblEyeSupplierName.Text = txtOperSupplierName.Value;

            txtOperContractObject.Value = entity.GetString("ContractObject");
            lblEyeContractObject.Text = HttpUtility.HtmlEncode(txtOperContractObject.Value).Replace("\n", "<br>");

            OperContractDate.Value = entity.GetDateTimeOnlyDate("ContractDate");
            lblEyeContractDate.Text = OperContractDate.Value;

            txtOperSupplier2Code.Value = entity.GetString("Supplier2Code");
            txtOperSupplier2Name.Value = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("Supplier2Code"));
            lblEyeSupplier2Name.Text = txtOperSupplier2Name.Value;

            txtOperBuilding.Value = entity.GetString("Building");
            lblEyeBuilding.Text = txtOperBuilding.Value;

            //��ʾ��ͬ���
            decimal TotalMoney, TotalChangeMoney, OriginalMoney, BudgetMoney, AdjustMoney;

            string contractLabel = entity.GetString("ContractLabel");

            TotalMoney = entity.GetDecimal("TotalMoney");
            OriginalMoney = entity.GetDecimal("OriginalMoney");
            BudgetMoney = entity.GetDecimal("BudgetMoney");
            AdjustMoney = entity.GetDecimal("AdjustMoney");

            TotalChangeMoney = TotalMoney - OriginalMoney;

            switch (this.MoneyState)
            {
                case ModuleState.Sightless://���ɼ���
                case ModuleState.Begin://���ɼ���
                case ModuleState.End://���ɼ���

                    txtOperOriginalMoney.Value = "***************";
                    txtOperTotalChangeMoney.Value = "***************";
                    txtOperNewTotalMoney.Value = "***************";
                    txtOperBudgetMoney.Value = "***************";
                    txtOperAdjustMoney.Value = "***************";
                    break;

                case ModuleState.Operable://�ɲ�����
                case ModuleState.Eyeable://�ɼ���

                    txtOperOriginalMoney.Value = OriginalMoney.ToString("N");
                    txtOperTotalChangeMoney.Value = TotalChangeMoney.ToString("N");
                    txtOperNewTotalMoney.Value = TotalMoney.ToString("N");
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
            txtEyeNewTotalMoney.Value = txtOperNewTotalMoney.Value;
            txtEyeBudgetMoney.Value = txtOperBudgetMoney.Value;
            txtEyeAdjustMoney.Value = txtOperAdjustMoney.Value;


            //ҵ���������Ա���
            SaveOperationProperty("��ͬ���", entity.GetDecimal("TotalMoney").ToString());
            SaveOperationProperty("��Ҫ���", entity.GetInt("Mostly").ToString());


            entity.Dispose();
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

            if (txtOperContractName.Value.Trim() == "")
            {
                ErrMsg = "����д��ͬ����";
                return ErrMsg;
            }

            if (inputOperSystemGroup.Value.Trim() == "")
            {
                ErrMsg = "����д��ͬ����";
                return ErrMsg;
            }

            if (txtOperSupplierCode.Value.Trim() == "")
            {
                ErrMsg = "����дǩԼ��λ";
                return ErrMsg;
            }

            if (OperContractDate.Value.Trim() == "")
            {
                ErrMsg = "����дԤ��ǩԼ����";
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


            //��ͬ������Ϣ����
            entity.SetCurrentTable("Contract");

            //����ҵ���ţ���ͬ���� 2007.1.16
            base.UnitCode = entity.GetString("UnitCode");

            foreach (DataRow dr in entity.CurrentTable.Select(String.Format("ContractCode='{0}'", this.ContractCode), "", DataViewRowState.CurrentRows))
            {
                dr["ContractID"] = txtOperContractID.Value;
                dr["ContractName"] = txtOperContractName.Value;
                dr["Type"] = inputOperSystemGroup.Value;

                dr["SupplierCode"] = txtOperSupplierCode.Value;
                dr["Supplier2Code"] = txtOperSupplier2Code.Value;

                dr["Building"] = txtOperBuilding.Value;

                dr["ContractObject"] = txtOperContractObject.Value;
                dr["ContractDate"] = OperContractDate.Value;

//                dr["Status"] = 7;
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

                EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.OperationCode, this.dao);

                entity.SetCurrentTable("Contract");

                if (entity.HasRecord())
                {
                    DataRow dr = entity.CurrentRow;

                    switch (pm_sOpinionConfirm)
                    {
                        case "Approve":

                            dr["Status"] = 0;
                            dr["CheckPerson"] = this.UserCode;
                            dr["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            break;
                        case "Reject":

                            dr["Status"] = 1;
                            dr["CheckPerson"] = this.UserCode;
                            dr["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");

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

                    RmsPM.DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);

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

            RmsPM.BLL.ContractRule.ContractStatusChange(dao, this.OperationCode, 7);

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

            RmsPM.BLL.ContractRule.ContractStatusChange(this.OperationCode, 1);

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
            this.myOperAttachMentAdd.AttachMentType = "ContractAttachMent";
            this.myOperAttachMentAdd.MasterCode = this.ApplicationCode;

            this.myEyeAttachMentList.AttachMentType = "ContractAttachMent";
            this.myEyeAttachMentList.MasterCode = this.ApplicationCode;
        }
    }

 }