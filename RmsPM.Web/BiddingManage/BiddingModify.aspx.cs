using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using System.Configuration;

namespace RmsPM.Web.BiddingManage
{
    /// <summary>
    /// BiddingModify ��ժҪ˵����
    /// </summary>
    public partial class BiddingModify : PageBase
    {

        private string ApplicationCode
        {
            get
            {
                if (this.ViewState["ApplicationCode"] != null)
                    return this.ViewState["ApplicationCode"].ToString();
                return "";
            }
            set
            {
                this.ViewState["ApplicationCode"] = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string BiddingDtlAddMoneyUrl
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByCode(BLL.WorkFlowRule.GetProcedureCodeByName("��������", this.ProjectCode));

            }
        }



        /// <summary>
        /// �б��������
        /// </summary>
        public string BiddingDiscussAuditingUrl
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByCode(BLL.WorkFlowRule.GetProcedureCodeByName("�б��������", this.ProjectCode));

            }
        }

        /// <summary>
        /// ��굥λ����
        /// </summary>
        public string DiscussAuditingUrl
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByCode(BLL.WorkFlowRule.GetProcedureCodeByName("��굥λ����", this.ProjectCode));

            }
        }


        /// <summary>
        /// Ͷ�굥λ����ҳ��
        /// </summary>
        public string PrejudicationAuditingUrl
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByCode(BLL.WorkFlowRule.GetProcedureCodeByName("Ͷ�굥λ����", this.ProjectCode));

            }
        }
        /// <summary>
        /// �б굥λ����ҳ��
        /// </summary>
        public string BiddingAuditingUrl
        {
            get
            {

                return BLL.WorkFlowRule.GetProcedureURLByCode(BLL.WorkFlowRule.GetProcedureCodeByName("�б굥λ����", this.ProjectCode));
            }
        }
        /// <summary>
        /// �б�֪ͨ������ҳ��
        /// </summary>
        public string BiddingMessageManageUrl
        {
            get
            {
                return BLL.WorkFlowRule.GetProcedureURLByCode(BLL.WorkFlowRule.GetProcedureCodeByName("�б�֪ͨ������", this.ProjectCode));

            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string BiddingEmitManageUrl
        {
            get
            {
                string company = this.up_sPMName.ToLower();
                switch (company)
                {

                    //case "gaokepm":
                    //    return "../WorkFlowPage/Gk_BiddingEmitManage.aspx";
                    //    break;
                    default:
                        return "BiddingEmitManage.aspx";
                        break;

                }
            }
        }


        /// ****************************************************************************
        /// <summary>
        /// ҳ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();

            }
            this.BiddingSupplierList1.BiddingCode = this.ApplicationCode;
            this.btnSave.Attributes["OnClick"] = "javascript:if(BiddingCheckSubmit()) ";
            this.btnDel.Attributes["OnClick"] = "javascript:if(confirm('ȷʵҪɾ����ǰ�б�ƻ���')) ";
        }
        /// ****************************************************************************
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// ****************************************************************************
        private void InitPage()
        {
            if (Request["ApplicationCode"] != null)
                this.ApplicationCode = Request["ApplicationCode"] + "";
            else if (Request["BiddingCode"] != null)
                this.ApplicationCode = Request["BiddingCode"] + "";
            if (Request["ProjectCode"] != null)
                this.ProjectCode = Request["ProjectCode"].ToString();


            //  //--//����Ҫ��
            string company = this.up_sPMName.ToLower();
            switch (company)
            {
                case "disaipm":
                    this.workflowmsg.Visible = false;
                    this.WorkFlowList1.Visible = false;
                    this.WorkFlowDiv.Visible = false;
                    break;
                case "tangchenpm":

                    this.btnNewSupply.Value = "�б�ִ��";
                    break;


                case "gaokepm":


                    this.Bt_ReturnOfPrice.Style["display"] = "none";

                    break;

                //this.PrejudicationListdiv.Visible = true;
                //this.ReturnListdiv.Visible = true;
                //this.BiddingPrejudicationList1.BiddingCode = this.ApplicationCode;
                //this.BiddingPrejudicationList1.DataBound();

            }
            //
            //�޸�


            if (this.user.HasRight("2106"))
            {
                BiddingDtlModify1.PriceState = WorkFlowControl.ModuleState.Operable;
                Bidding1.PriceState = WorkFlowControl.ModuleState.Operable;
            }
            else if (this.user.HasRight("2107"))//�鿴
            {
                BiddingDtlModify1.PriceState = WorkFlowControl.ModuleState.Eyeable;
                Bidding1.PriceState = WorkFlowControl.ModuleState.Eyeable;
            }


            if (!this.user.HasRight("210904"))//�б��ļ�����
            {
                this.Bidding1.BiddingFileState = ModuleState.Unbeknown;
            }
            else
            {
                this.Bidding1.BiddingFileState = ModuleState.Operable;
            }



            Bidding1.ApplicationCode = this.ApplicationCode;
            Bidding1.ProjectCode = this.ProjectCode;
            Bidding1.UserCode = this.user.UserCode;
            BiddingDtlModify1.ApplicationCode = Bidding1.ApplicationCode;
            BiddingDtlModify1.ProjectCode = this.ProjectCode;

            BLL.BiddingEmit cBiddingEmit = new BLL.BiddingEmit();
            BLL.BiddingManage bm = new BLL.BiddingManage();
            bm.BiddingCode = this.ApplicationCode;

            if (this.ApplicationCode == "")
            {

                Bidding1.State = WorkFlowControl.ModuleState.Operable;
                Bidding1.InitControl();

                this.BiddingConditionFileInfo1.State = ModuleState.Unbeknown;

                BiddingDtlModify1.State = WorkFlowControl.ModuleState.Operable;
                BiddingDtlModify1.InitControl();

                this.btnSave.Visible = true;
                this.btnAddDtl.Visible = true;
                this.btnModify.Visible = false;
                this.btnDel.Visible = false;
                this.btnNewSupply.Visible = false;
                this.btnEmit.Visible = false;
                this.btnReturn.Visible = false;
                this.btnAuditing.Visible = false;
                this.btnMessage.Visible = false;
                this.btnContract.Visible = false;
                this.Bt_LowOfPrice.Visible = false;
                this.Bt_ReturnOfPrice.Visible = false;

                //this.btnAddMoney.Visible = false;
                this.btnCheckBiddingDiscuss.Visible = false;
                this.btnOldCheckBiddingDiscuss.Visible = false;
                this.btnCheckDiscuss.Visible = false;

                //��������ѡ�
                this.BiddingCondition.Style["display"] = "none";
                this.BiddingContidionFile.Style["display"] = "none";
                this.trPrejudication.Style["display"] = "none";
                this.Tr1.Style["display"] = "none";
                this.trZBGC.Style["display"] = "none";
                this.BiddingProcessDiv.Style["display"] = "none";
                this.trHBGC.Style["display"] = "none";
                this.BiddingEmitDiv.Style["display"] = "none";
                this.tdBiddingMessage.Style["display"] = "none";
                this.BiddingMessageDiv.Style["display"] = "none";
                this.workflowmsg.Style["display"] = "none";
                this.WorkFlowDiv.Style["display"] = "none";
            }
            else
            {
                this.BiddingConditionFileInfo1.State = ModuleState.Operable;
                this.btnModify.Visible = true;
                this.btnSave.Visible = false;
                this.btnAddDtl.Visible = false;
                this.btnDel.Visible = false;
                Bidding1.State = WorkFlowControl.ModuleState.Eyeable;
                Bidding1.InitControl();
                BiddingDtlModify1.State = WorkFlowControl.ModuleState.Eyeable;
                BiddingDtlModify1.InitControl();
                if (Bidding1.BiddingState == "0")//ֻ���б�ƻ���Ϣ
                {
                    this.btnNewSupply.Visible = true;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnMessage.Visible = false;
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    this.btnDel.Visible = true;
                    //this.btnAddMoney.Visible = false;
                    //LB_BiddingState.Text="";
                }
                if (Bidding1.BiddingState == "1")//��Ԥ��Ͷ�굥λ
                {
                    this.btnNewSupply.Visible = true;
                    this.btnEmit.Visible = true;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnMessage.Visible = false;
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    //this.btnAddMoney.Visible = false;
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                    this.btnCheckDiscuss.Visible = false;
                }
                if (Bidding1.BiddingState == "2")//�Ѿ�����
                {
                    this.btnNewSupply.Visible = true;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = GetShowInfoForbtnReturn();
                    this.btnAuditing.Visible = false;
                    this.btnMessage.Visible = false;
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                    // this.btnAddMoney.Visible = false;
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                    this.btnCheckDiscuss.Visible = false;
                }
                if (Bidding1.BiddingState == "3")//�Ѿ��ر�
                {
                    this.btnNewSupply.Visible = true;
                    this.btnEmit.Visible = true;
                    this.btnReturn.Visible = GetShowInfoForbtnReturn();
                    this.btnAuditing.Visible = true;
                    this.btnMessage.Visible = false;
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                    this.btnCheckDiscuss.Visible = false;
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                }
                if (Bidding1.BiddingState == "5")//�Ѿ���ʼ����,����ѹ��״̬
                {
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnMessage.Visible = false;
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = true;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                    this.btnCheckDiscuss.Visible = false;
                    //this.btnAddMoney.Visible = false;   
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                }
                if (Bidding1.BiddingState == "6")//�Ѿ���ʼ����,����ѹ��״̬
                {
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnMessage.Visible = false;
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = true;
                    this.Bt_ReturnOfPrice.Visible = true;
                    IsShow_Control_BiddingEmitHistory(true);
                    this.btnCheckDiscuss.Visible = false;
                    //this.btnAddMoney.Visible = false;   
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                }
                if (Bidding1.BiddingState == "4")//�������
                {
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnModify.Visible = false;
                    this.btnMessage.Visible = true;
                    //this.btnMessage.Visible=user.HasOperationRight("2106");
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                    if (ConfigurationSettings.AppSettings["IsBiddingMessage"] == "0")
                    {
                        btnMessage.Visible = false;
                        this.btnContract.Visible = true;

                    }
                    this.btnCheckDiscuss.Visible = false;
                    //this.btnAddMoney.Visible = false;   
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                }
                if (Bidding1.BiddingState == "41")//�б�֪ͨ��������
                {
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnModify.Visible = false;
                    //this.btnAddMoney.Visible = false;   
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                    this.btnCheckDiscuss.Visible = false;

                    BLL.Bidding bd = new BLL.Bidding();
                    bd.BiddingCode = Bidding1.ApplicationCode;
                    if (bd.GetBiddingReturnNoMessage().Rows.Count > 0)
                        this.btnMessage.Visible = true;
                    else
                        this.btnMessage.Visible = false;

                    BLL.BiddingMessage bms = new BLL.BiddingMessage();
                    bms.BiddingCode = Bidding1.ApplicationCode;
                    DataTable dtbms = bms.GetBiddingMessages();
                    bool ContractCreateFlag = false;
                    foreach (DataRow dr in dtbms.Select())
                    {
                        if (BLL.ContractRule.GetContractCountByContractDefaultValueCode(dr["BiddingMessageCode"].ToString()) == 0)
                            ContractCreateFlag = true;
                    }

                    this.btnContract.Visible = ContractCreateFlag;

                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                }
                if (Bidding1.BiddingState == "42")//�б�֪ͨ��������
                {
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnModify.Visible = false;
                    //this.btnAddMoney.Visible = false;   
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                    this.btnCheckDiscuss.Visible = false;

                    BLL.Bidding bd = new BLL.Bidding();
                    bd.BiddingCode = Bidding1.ApplicationCode;
                    if (bd.GetBiddingReturnNoMessage().Rows.Count > 0)
                        this.btnMessage.Visible = true;
                    else
                        this.btnMessage.Visible = false;

                    BLL.BiddingMessage bms = new BLL.BiddingMessage();
                    bms.BiddingCode = Bidding1.ApplicationCode;
                    DataTable dtbms = bms.GetBiddingMessages();
                    bool ContractCreateFlag = false;
                    foreach (DataRow dr in dtbms.Select())
                    {
                        if (BLL.ContractRule.GetContractCountByContractDefaultValueCode(dr["BiddingMessageCode"].ToString()) == 0)
                            ContractCreateFlag = true;
                    }


                    this.btnContract.Visible = ContractCreateFlag;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                }
                if (Bidding1.BiddingState == "43")//�������
                {
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnModify.Visible = false;
                    this.btnMessage.Visible = false;
                    this.btnContract.Visible = false;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                    //this.btnAddMoney.Visible = false;   
                    this.btnCheckDiscuss.Visible = false;
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;


                }

                if (Bidding1.BiddingState == "7")//�б�ִ��
                {
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnModify.Visible = false;

                    //this.btnAddMoney.Visible = false;
                    this.btnCheckDiscuss.Visible = false;
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;

                    this.btnMessage.Visible = false;

                    BLL.BiddingMessage bms = new BLL.BiddingMessage();
                    bms.BiddingCode = Bidding1.ApplicationCode;
                    bms.State = "0";
                    DataTable dtbms = bms.GetBiddingMessages();
                    bool ContractCreateFlag = false;
                    foreach (DataRow dr in dtbms.Select())
                    {
                        if (BLL.ContractRule.GetContractCountByContractDefaultValueCode(dr["BiddingMessageCode"].ToString()) == 0)
                            ContractCreateFlag = true;
                    }

                    RmsPM.BLL.BiddingPrejudication cbiddingPrejudicationtemp = new RmsPM.BLL.BiddingPrejudication();
                    cbiddingPrejudicationtemp.BiddingPrejudicationCode = cbiddingPrejudicationtemp.GetPrimaryKeyByBiddingCode(this.ApplicationCode);
                    if (cbiddingPrejudicationtemp.State != "0")
                        this.btnBiddingStart.Visible = true;
                    this.btnContract.Visible = ContractCreateFlag;
                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;
                    IsShow_Control_BiddingEmitHistory(true);
                }

                RmsPM.BLL.Bidding cbidding = new RmsPM.BLL.Bidding();
                cbidding.BiddingCode = this.ApplicationCode;
                //���
                if (cbidding.BiddingType == "0" && cbidding.Status == "0")
                {
                    this.btnSave.Visible = false;
                    this.btnAddDtl.Visible = false;
                    this.btnModify.Visible = false;
                    this.btnDel.Visible = false;
                    this.btnNewSupply.Visible = false;
                    this.btnEmit.Visible = false;
                    this.btnReturn.Visible = false;
                    this.btnAuditing.Visible = false;
                    this.btnMessage.Visible = false;

                    this.Bt_LowOfPrice.Visible = false;
                    this.Bt_ReturnOfPrice.Visible = false;

                    //this.btnAddMoney.Visible = false;
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                }
                else if (cbidding.BiddingType == "1" && cbidding.Status == "0")//�б�
                {
                    this.btnCheckBiddingDiscuss.Visible = false;
                    this.btnOldCheckBiddingDiscuss.Visible = false;
                    this.btnCheckDiscuss.Visible = false;
                }
                else
                {
                    this.btnCheckDiscuss.Visible = false;
                    //����Ƿ�����б��������
                    RmsPM.BLL.BiddingAuditing cbiddingAuditing = new RmsPM.BLL.BiddingAuditing();
                    cbiddingAuditing.BiddingCode = this.ApplicationCode;
                    DataTable dtBiddingAuditing = cbiddingAuditing.GetBiddingAuditings();
                    if (dtBiddingAuditing != null)
                    {
                        if (dtBiddingAuditing.Rows.Count != 0)
                        {
                            this.btnCheckBiddingDiscuss.Visible = false;
                        }
                    }

                }


            }

            RmsPM.BLL.BiddingPrejudication cbiddingPrejudication = new RmsPM.BLL.BiddingPrejudication();
            //�������
            this.btnCheckDiscuss.Attributes["OnClick"] = "javascript:OpenDiscuss('" + this.ApplicationCode + "','" + cbiddingPrejudication.GetPrimaryKeyByBiddingCode(this.ApplicationCode) + "','" + this.ProjectCode + "');return false;";
            //�б��������
            this.btnCheckBiddingDiscuss.Attributes["OnClick"] = "javascript:OpenBiddingDiscuss('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";
            //�б���굥�����
            this.btnOldCheckBiddingDiscuss.Attributes["OnClick"] = "javascript:OpenBiddingType('" + this.ApplicationCode + "');return false;";
            ////��������
            //this.btnAddMoney.Attributes["OnClick"] = "javascript:OpenBiddingDtlAddMoney('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";
            this.btnNewSupply.Attributes["OnClick"] = "javascript:OpenNewSupply('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";

            this.btnBiddingStart.Attributes["OnClick"] = "javascript:OpenBiddingStart('" + cbiddingPrejudication.GetPrimaryKeyByBiddingCode(this.ApplicationCode) + "','" + this.ProjectCode + "');return false;";
            this.btnEmit.Attributes["OnClick"] = "javascript:OpenNewEmit('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";
            this.Bt_LowOfPrice.Attributes["OnClick"] = "javascript:OpenLowOfPrice('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";
            this.Bt_ReturnOfPrice.Attributes["OnClick"] = "javascript:ReturnLowOfPrice('" + bm.GetLastBiddingEmitCode() + "" + "','" + this.ProjectCode + "');return false;";
            this.btnReturn.Attributes["OnClick"] = "javascript:BiddingEmitListReturnModify('" + bm.GetLastBiddingEmitCode() + "" + "','" + this.ProjectCode + "');return false;";
            this.btnAuditing.Attributes["OnClick"] = "javascript:OpenAuditing('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";
            this.btnMessage.Attributes["OnClick"] = "javascript:BiddingMessage('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";
            this.btnContract.Attributes["OnClick"] = "javascript:BiddingContract('" + this.ApplicationCode + "','" + this.ProjectCode + "');return false;";

            BiddingProcess1.InitControl(Bidding1.ApplicationCode);

            if (!this.user.HasRight("210802"))//�б��ļ�����
            {
                this.BiddingCondition.Visible = false;
                this.BiddingContidionFile.Visible = false;
                this.BiddingDtl.Attributes["onclick"] = "EventClickTab(0);";
                this.trPrejudication.Attributes["onclick"] = "EventClickTab(1);";
                this.trZBGC.Attributes["onclick"] = "EventClickTab(2);";

                this.trHBGC.Attributes["onclick"] = "EventClickTab(3);";
                this.tdBiddingMessage.Attributes["onclick"] = "EventClickTab(4);";
                this.workflowmsg.Attributes["onclick"] = "EventClickTab(5);";

                this.BiddingConditionFileInfo1.Visible = false;
            }
            else
            {


                this.BiddingConditionFileInfo1.BiddingCode = Bidding1.ApplicationCode;
                this.BiddingConditionFileInfo1.ProjectCode = this.ProjectCode;
                this.BiddingConditionFileInfo1.InitControl();
            }

            if (company == "tangchenpm")//�б��ļ�����
            {
                //ȡ���б꼼������ѡ�
                this.BiddingCondition.Visible = false;
                this.BiddingContidionFile.Visible = false;
                //ȡ��Ͷ��Ԥ��λѡ�
                this.trPrejudication.Visible = false;
                this.Tr1.Visible = false;
                //�б����
                this.trZBGC.Visible = false;
                this.BiddingProcessDiv.Visible = false;
                //�б�֪ͨ��
                this.tdBiddingMessage.Visible = false;
                this.BiddingMessageList1.Visible = false;

                //�ر����
                this.trHBGC.Visible = false;
                this.BiddingEmitDiv.Visible = false;

                //this.BiddingCondition.Visible = false;
                //this.BiddingContidionFile.Visible = false;
                this.BiddingDtl.Attributes["onclick"] = "EventClickTab(0);";
                //this.trPrejudication.Attributes["onclick"] = "EventClickTab(1);";
                //this.trZBGC.Attributes["onclick"] = "EventClickTab(2);";

                //this.trHBGC.Attributes["onclick"] = "EventClickTab(3);";
                this.workflowmsg.Attributes["onclick"] = "EventClickTab(1);";

                //this.BiddingConditionFileInfo1.Visible = false;
            }


            //this.BiddingPrejudicationSupplierList1.BiddingCode = Bidding1.ApplicationCode;
            //this.BiddingPrejudicationSupplierList1.InitControl();

            if (!this.user.HasRight("2110"))
            {
                this.Bt_LowOfPrice.Visible = false;
                this.Bt_ReturnOfPrice.Visible = false;
            }

            if (!this.user.HasRight("211301"))
            {
                this.btnOldCheckBiddingDiscuss.Visible = false;
            }
            if (!this.user.HasRight("211302"))
            {
                this.btnCheckBiddingDiscuss.Visible = false;
            }
            if (!this.user.HasRight("211303"))
            {
                this.btnCheckDiscuss.Visible = false;
            }


            this.WorkFlowList1.ProcedureNameAndApplicationCodeList = GetWorkFlowListString();
            this.WorkFlowList1.DataBound();

            //�Ƿ����Ԥ��λ
            if (this.WorkFlowList1.WorkFlowCount != 0)
            {
                this.btnDel.Visible = false;
            }

            //����б�֪ͨ���б�
            this.LoadBiddingMessageList(this.ApplicationCode);

        }

        private bool GetShowInfoForbtnReturn()
        {
            //������Ͷ����б�ƻ�����������ֹ��ر갴ť
            RmsPM.BLL.BiddingManage cBllBiddingManage = new RmsPM.BLL.BiddingManage();
            cBllBiddingManage.BiddingCode = this.ApplicationCode;
            RmsPM.BLL.BiddingEmit cBiddingEmitR = new RmsPM.BLL.BiddingEmit();
            StandardEntityDAO dao = new StandardEntityDAO("BiddingEmit");
            EntityData entity = cBiddingEmitR.GetBiddingEmitByCode(dao, cBllBiddingManage.GetLastBiddingEmitCode());
            if (entity.HasRecord())
            {
                if (entity.GetInt("IsWSZTB") == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private string GetWorkFlowListString()
        {
            string ListString = "''";
            BLL.BiddingAuditing cbiddingAuditing = new RmsPM.BLL.BiddingAuditing();
            cbiddingAuditing.BiddingCode = this.ApplicationCode;
            DataTable dtba = cbiddingAuditing.GetBiddingAuditings();
            for (int i = 0; i < dtba.Rows.Count; i++)
            {
                ListString += ",'�б��������" + dtba.Rows[i]["BiddingAuditingCode"].ToString() + "'";
            }


            BLL.BiddingFile bf = new RmsPM.BLL.BiddingFile();
            bf.BiddingCode = this.ApplicationCode;
            DataTable dtbf = bf.GetBiddingFiles();
            for (int i = 0; i < dtbf.Rows.Count; i++)
            {
                ListString += ",'�б��ļ�����" + dtbf.Rows[i]["BiddingFileCode"].ToString() + "'";
            }


            BLL.BiddingConditionFile bcf = new RmsPM.BLL.BiddingConditionFile();
            bcf.BiddingCode = this.ApplicationCode;
            DataTable dtbcf = bcf.GetBiddings();
            for (int i = 0; i < dtbcf.Rows.Count; i++)
            {
                ListString += ",'�б꼼����������" + dtbcf.Rows[i]["BiddingConditionFileCode"].ToString() + "'";
            }

            BLL.BiddingPrejudication bp = new BLL.BiddingPrejudication();
            bp.BiddingCode = this.ApplicationCode;
            DataTable dtp = bp.GetBiddingPrejudications();
            for (int i = 0; i < dtp.Rows.Count; i++)
            {
                ListString += ",'Ͷ�굥λ����" + dtp.Rows[i]["BiddingPrejudicationCode"].ToString() + "'";
            }


            BLL.BiddingEmit be = new BLL.BiddingEmit();
            be.BiddingCode = this.ApplicationCode;
            DataTable tb = be.GetBiddingEmits();
            for (int z = 0; z < tb.Rows.Count; z++)
            {
                ListString += ",'�б굥λ����" + tb.Rows[z]["BiddingEmitCode"].ToString() + "'";
            }

            BLL.BiddingMessage bm = new BLL.BiddingMessage();
            bm.BiddingCode = this.ApplicationCode;
            DataTable dtm = bm.GetBiddingMessages();
            for (int i = 0; i < dtm.Rows.Count; i++)
            {
                ListString += ",'�б�֪ͨ������" + dtm.Rows[i]["BiddingMessageCode"].ToString() + "'";
            }

            return ListString;
        }
        /// <summary>
        /// ��ʾ��Ͷ����ʷ��ϸ
        /// </summary>
        /// <param name="bl"></param>
        private void IsShow_Control_BiddingEmitHistory(bool bl)
        {
            if (!bl)
                return;
            Control_BiddingEmitHistory1.Visible = true;
            Control_BiddingEmitHistory1.BiddingCode = ApplicationCode;
            Control_BiddingEmitHistory1.LoadData();
        }
        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        /// ****************************************************************************
        /// <summary>
        /// ���水ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                if (Bidding1.SystemGroup.Trim() == "")
                {
                    this.RegisterStartupScript("", "<script>alert('�������Ϊ�գ�');</script>");
                    return;
                }
                if (!BiddingDtlModify1.CheckItemValue)
                {
                    this.RegisterStartupScript("", "<script>alert('�ֱ�����������������д��');</script>");
                    return;
                }
                if (BiddingDtlModify1.BiddingDtlCount == 0)
                {
                    this.RegisterStartupScript("", "<script>alert('��Ͷ��ƻ���������ֱ�Σ�');</script>");
                    return;
                }
                Bidding1.SubmitData();
                BiddingDtlModify1.ApplicationCode = Bidding1.ApplicationCode;

                BiddingDtlModify1.SubmitData();

                if (Request["FunctionName"] != null)
                {
                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write("window.opener." + Request["FunctionName"].ToString() + "();");
                    Response.Write(Rms.Web.JavaScript.WinClose(false));
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }
                else
                {
                    if (Request["ApplicationCode"] + "" != "")
                    {
                        RmsPM.BLL.BiddingLog cbiddingLog = new RmsPM.BLL.BiddingLog();
                        cbiddingLog.BiddingLogCode = "";
                        cbiddingLog.BiddingCode = Request["ApplicationCode"] + "";
                        cbiddingLog.Type = "��Ͷ��Ԥ������";
                        if (this.Bidding1.TeamMoney != null && this.Bidding1.TeamMoney != "")
                            cbiddingLog.FormerMoney = System.Convert.ToDecimal(this.Bidding1.TeamMoney);
                        if (this.BiddingDtlModify1.TeamMoney != null && this.BiddingDtlModify1.TeamMoney != "")
                            cbiddingLog.TeamMoney = System.Convert.ToDecimal(this.BiddingDtlModify1.TeamMoney);
                        cbiddingLog.UserCode = this.user.UserCode;
                        cbiddingLog.UpdateTime = System.DateTime.Now.ToString();
                        cbiddingLog.State = "1";
                        cbiddingLog.BiddingLogAdd();


                        this.InitPage();
                        this.ApplicationCode = Bidding1.ApplicationCode;
                        Bidding1.InitControl();


                        Response.Write(Rms.Web.JavaScript.ScriptStart);
                        Response.Write("window.opener.location.reload();");
                        Response.Write(Rms.Web.JavaScript.ScriptEnd);
                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.ScriptStart);
                        Response.Write("window.opener.location.reload();");
                        Response.Write(Rms.Web.JavaScript.WinClose(false));
                        Response.Write(Rms.Web.JavaScript.ScriptEnd);
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.ToString()));
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// ɾ����ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnDel_ServerClick(object sender, System.EventArgs e)
        {

            try
            {
                //if (this.ApplicationCode != "")
                //{

                //}
                //if (this.BiddingDtlModify1.ApplicationCode != "")
                //    this.BiddingDtlModify1.Delete();
                if (this.WorkFlowList1.WorkFlowCount == 0)
                {
                    RmsPM.BLL.BiddingPrejudication cbiddingPrejudication = new RmsPM.BLL.BiddingPrejudication();
                    cbiddingPrejudication.BiddingCode = this.Bidding1.ApplicationCode;
                    EntityData entityPrejudication = cbiddingPrejudication._GetBiddingPrejudications();
                    entityPrejudication.DeleteAllTableRow("BiddingPrejudication");
                    using (StandardEntityDAO dao = new StandardEntityDAO("BiddingPrejudication"))
                    {
                        dao.SubmitEntity(entityPrejudication);
                    }
                    if (this.BiddingDtlModify1.ApplicationCode != "")
                        this.BiddingDtlModify1.Delete();
                    Bidding1.Delete();
                }




                if (Request["FunctionName"] != null)
                {
                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write("window.opener." + Request["FunctionName"].ToString() + "();");
                    Response.Write(Rms.Web.JavaScript.WinClose(false));
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write("window.opener.location.reload();");
                    Response.Write(Rms.Web.JavaScript.WinClose(false));
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.ToString()));
            }

        }

        /// ****************************************************************************
        /// <summary>
        /// �޸İ�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnModify_ServerClick(object sender, System.EventArgs e)
        {
            Bidding1.State = WorkFlowControl.ModuleState.Operable;
            Bidding1.InitControl();
            BiddingDtlModify1.State = WorkFlowControl.ModuleState.Operable;
            BiddingDtlModify1.InitControl();
            BiddingProcess1.InitControl(Bidding1.ApplicationCode);
            this.btnSave.Visible = true;
            this.btnAddDtl.Visible = true;
            this.btnModify.Visible = false;
            this.btnNewSupply.Visible = false;
            this.btnEmit.Visible = false;
            this.btnReturn.Visible = false;
            this.btnAuditing.Visible = false;
            this.btnDel.Visible = false;
            this.btnMessage.Visible = false;
            this.btnContract.Visible = false;
            if (Bidding1.BiddingState == "0" && Bidding1.PrejudicationsCount == 0)
            {
                this.btnDel.Visible = true;
            }
        }
        protected void btnAddDtl_ServerClick(object sender, EventArgs e)
        {
            BiddingDtlModify1.AddNewRows();
        }


        public void LoadBiddingMessageList(string BiddingCode)
        {
            this.BiddingMessageList1.BiddingCode = BiddingCode;
            this.BiddingMessageList1.UsebtnAdd = this.user.HasRight("210501");
            this.BiddingMessageList1.UsebtnRemove = this.user.HasRight("210503");
            this.BiddingMessageList1.UsebtnApprove = this.user.HasRight("210504");
            this.BiddingMessageList1.UsebtnCancelApprove = this.user.HasRight("210505");
            this.BiddingMessageList1.InitControl();
        }
    }
}
