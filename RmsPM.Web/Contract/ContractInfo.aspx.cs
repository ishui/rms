//
//
// ��ͬ״̬�� 0: ������ 1 ����ˣ���ǰ��ͬ�� 2 ��ͬ������ϣ����� �� 
//            3 ���벻ͨ�� �� 4 ������룻 6 ��ʷ��¼ �� 7 ��ͬ������
//
//

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
using System.Collections.Generic;


using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using System.Configuration;

namespace RmsPM.Web.Contract
{
    /// <summary>
    /// ContractInfo ��ժҪ˵����
    /// </summary>
    public partial class ContractInfo : PageBase
    {
        protected System.Web.UI.WebControls.DataGrid dgPaymentPlanList;
        protected System.Web.UI.WebControls.Label hid_Index;

        protected void Page_Load(object sender, System.EventArgs e)
        {

            // ��ó���� ӡ��˰

            if (base.up_sPMNameLower != "shidaipm")
            {
                st1.Visible = false;
                st2.Visible = false;
                lblStampDuty.Visible = false;
                lblStampDutyID.Visible = false;
            }
            if (!IsPostBack)
            {
                ViewState["ProjectCode"] = Request["ProjectCode"] + "";

                string contractCode = Request["ContractCode"] + "";
                ArrayList ar;

                if (AvailableFunction.isAvailableFunction("050152"))
                {
                    //���Ϻ�ͬ�����˵�Ȩ��
                    ar = user.GetContractResourceRight(contractCode, "Contract", (ArrayList)Application["ContractActorOperationList"]);
                }
                else
                {
                    ar = user.GetResourceRight(contractCode, "Contract");
                }

                if (!ar.Contains("050101"))
                {
                    Response.Redirect("../RejectAccess.aspx");
                    Response.End();
                }

                if (!ar.Contains("050112")) //ʵ�ʲ�ֵ�޸�
                    this.btnModifyFactValue.Visible = false;

                if (!ar.Contains("050140")) //����������ʾ
                {
                    this.divMaterial.Visible = false;
                    this.divMaterialIn.Visible = false;
                    this.divMaterialOut.Visible = false;
                }

                if (!ar.Contains("050141")) //���������޸�
                    this.btnModifyMaterial.Visible = false;

                //��ʾ����ʾ������������ť
                if (BLL.ConvertRule.ToString(System.Configuration.ConfigurationManager.AppSettings["PMName"]) != "PMDemo")
                {
                    this.btnNewProductionPaymentApply.Visible = false;
                }

                IniPage();
                LoadData();

                InitViewState();

                // Ȩ��
                if (!ar.Contains("050106")) //��ͬ����
                {
                    this.btnAccount.Visible = false;
                }

                if (!ar.Contains("050115")) //�ύ��ͬ����
                {
                    this.btnAccountManage.Visible = false;
                }

                if (!ar.Contains("050107")) //��ͬ���
                {
                    this.btnNewPaymentApply.Visible = false;
                }

                if (!ar.Contains("050105")) //��ͬ���
                {
                    this.btnOldCheck.Visible = false;
                }

                if (!ar.Contains("050113")) //�ύ��ͬ���
                {
                    this.btnCheck.Visible = false;
                }

                if (!ar.Contains("050104")) //��ͬ���
                {
                    this.btnChange.Visible = false;
                }

                if (!ar.Contains("050103")) //��ͬ�޸�
                {
                    this.btnModify.Visible = false;
                }

                if (!ar.Contains("050108")) //��ͬɾ��
                {
                    this.btnDelete.Visible = false;
                }

                if (!ar.Contains("050111")) //�޸ĸ���ƻ�
                {
                    this.ucContractCostPlanView.ModifyPaymentPlan = false;
                }

                if (!ar.Contains("050116")) //��˺��޸�
                {
                    this.btnAuditingModify.Visible = false;
                }

                if (!ar.Contains("050117")) //��ͬ��˴�ӡ
                {
                    this.btnPrint.Visible = false;
                }

                if (!ar.Contains("050118")) //��ͬ������˴�ӡ
                {
                    this.btnAccontPrint.Visible = false;
                }

                if (!ar.Contains("050119")) //��ͬ����޸�
                {
                    this.btnContractID.Visible = false;
                }

                if ((!ar.Contains("050120")) && (!ar.Contains("050121"))) //Լ����ֵ��ʾ��ʵ�ʲ�ֵ��ʾ
                {
                    this.divContractProduction.Visible = false;
                }

                if (!ar.Contains("050152")) //�޸ĺ�ͬ������
                {
                    this.ucInputAccessRange.Readonly = true;
                }

                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ���", contractCode) > 0)
                {
                    this.btnCheck.Visible = false;
                }

                if (BLL.WorkFlowRule.GetCaseCountByProcedureNameAndApplicationCode("��ͬ�������", contractCode) > 0)
                {
                    this.btnAccountManage.Visible = false;
                }

            }
        }


        private void InitViewState()
        {
            string ud_sProjectCode = Request["ProjectCode"] + "";
            string ud_sContractCode = Request["ContractCode"] + "";

            //��������url
            ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByName("��ͬ���");

            ViewState["_AccountAuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByName("��ͬ�������");


            //�������ӡURL
            string ud_sPMName = System.Configuration.ConfigurationManager.AppSettings["PMName"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString();

            string ud_sCaseCode = BLL.WorkFlowRule.GetCaseCodeByProcedureNameAndApplicationCode("��ͬ���", ud_sContractCode);
            string ud_sPrintURL = "";
            string ud_sAccountPrintURL = "";

            switch (ud_sPMName.ToLower())
            {
                case "xinchangningpm":
                    ud_sPrintURL = "ContractAuditingPrint.aspx?ProjectCode=" + ud_sProjectCode + "&ContractCode=" + ud_sContractCode;
                    ud_sAccountPrintURL = "ContractAccountPrint.aspx?ProjectCode=" + ud_sProjectCode + "&ContractCode=" + ud_sContractCode;
                    break;
                case "tianyangoa":
                    ud_sPrintURL = "../WorkFlowPrint/TY_ContractAuditing.aspx?frameType=List&ApplicationCode=" + ud_sContractCode + "&CaseCode=" + ud_sCaseCode;
                    break;
                //case "shidaipm":
                //    ud_sPrintURL = "../WorkFlowPrint/SD_ContractAccountAuditing.aspx?ProjectCode=" + ud_sProjectCode + "&ContractCode=" + ud_sContractCode;
                //    break;
                default:
                    break;
            }

            ViewState["_PrintURL"] = ud_sPrintURL;
            ViewState["_AccountPrintURL"] = ud_sAccountPrintURL;

        }

        private void IniPage()
        {
            try
            {
                string contractCode = Request["ContractCode"] + "";
                this.myAttachMentList.AttachMentType = "ContractAttachMent";
                this.myAttachMentList.MasterCode = contractCode;

                this.trPerformingCircs.Visible = false;
                this.trAdIssueDate.Visible = false;
                this.btnPaymentList.Visible = false;

                switch (this.up_sPMNameLower)
                {
                    case "yefengpm":
                        this.lblBaohanTitle.Text = "��Լ��֤��";
                        this.divAdjustMoney.Visible = false;
                        this.lblAdjustMoney.Visible = false;
                        break;

                    case "nonggongshangpm":
                        this.trPerformingCircs.Visible = true;
                        break;

                    case "yuhongpm":
                        this.divTaskList.Visible = false;
                        this.divWorkFlowList.Visible = false;
                        break;

                    case "shimaopm":
                        this.divContractBill.Visible = false;
                        break;

                    case "gaokepm":
                        this.trAdIssueDate.Visible = true;
                        break;

                    case "tangchenpm":
//                        this.btnPaymentList.Visible = true;
                        break;

                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ��ʧ�ܡ�");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
            }
        }

        private void LoadData()
        {
            string contractCode = Request["ContractCode"] + "";
            string projectCode = Request["ProjectCode"] + "";
            bool contractInWorkFlow = false;
            // ��ǰ�汾�ĺ�ͬ��
            //			string curContractCode = BLL.ContractRule.GetCurrentContractVersionCode(contractCode);
            string curContractCode = contractCode;
            this.ViewState.Add("CurrentContractCode", curContractCode);
            string biddingCode = "";

            try
            {
                //������ xyq 2007.2.28
                if (AvailableFunction.isAvailableFunction("050152"))
                {
                    this.trAccessRange.Visible = true;
                    this.ucInputAccessRange.RelationCode = contractCode;
                }

                EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
                decimal totalMoney = decimal.Zero;
                if (entity.HasRecord())
                {
                    ViewState["ProjectCode"] = entity.GetString("ProjectCode");

                    this.LabelContractID.Text = entity.GetString("ContractID");
                    this.LabelType.Text = BLL.ContractRule.GetContractTypeName(entity.GetString("Type"));
                    this.LabelRemark.Text = entity.GetString("Remark").Replace("\n","</br>");

                    //ӡ��˰//
                    try
                    {
                        string t = entity.GetInt("StampDutyID").ToString();

                        if (Rms.Check.StringCheck.IsNumber(t))
                        {
                            StampDuty sd = RmsPM.BLL.StampDuty.GetModel(Convert.ToInt32(t));
                            if (sd != null) { lblStampDutyID.Text = sd.TaxItems; }
                        }

                        lblStampDuty.Text = entity.GetDecimalString("StampDuty");
                    }
                    catch { }
                    ///////////////////////
                    this.LabelContractName.Text = entity.GetString("ContractName");
                    this.lblCheckOpinion.Text = entity.GetString("CheckOpinion");

                    RmsPM.BLL.Bidding cbidding = new Bidding();
                    string LinkUrl = "<a href='#' onclick=\"OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + entity.GetString("BiddingCode") + "&State=edit');return false;\">" + cbidding.GetBiddingName(entity.GetString("BiddingCode")) + "</a>";
                    this.tdBidding.InnerHtml = LinkUrl;

                    this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(projectCode);

                    this.lblWorkTime.Text = entity.GetString("WorkTime");

                    this.LabelContractDate.Text = entity.GetDateTimeOnlyDate("ContractDate");
                    this.LabelContractPerson.Text = BLL.SystemRule.GetUserName(entity.GetString("ContractPerson"));

                    this.hrefSupplier.InnerText = BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));
                    this.txtSupplierCode.Value = entity.GetString("SupplierCode");
                    this.hrefSupplier2.InnerText = BLL.ProjectRule.GetSupplierName(entity.GetString("Supplier2Code"));
                    this.txtSupplier2Code.Value = entity.GetString("Supplier2Code");

                    this.lblPayMode.Text = entity.GetString("PayMode").Replace("\n", "<br>");
                    this.lblQualityRequire.Text = entity.GetString("QualityRequire").Replace("\n", "<br>");

                    this.lblPerformingCircs.Text = entity.GetString("PerformingCircs");

                    biddingCode = entity.GetString("BiddingCode");

                    totalMoney = entity.GetDecimal("TotalMoney");

                    decimal ForeignTotalMoney, ForeignTotalChangeMoney, ForeignOriginalMoney, budgetMoney, adjustMoney;

                    //�������ռ���ʾԭ�ҽ�� xyq 2007.1.25
                    ForeignTotalMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"], "Cash");
                    this.lblTotalMoney.Text = BLL.StringRule.BuildShowNumberString(ForeignTotalMoney);

                    //ʵ�ʽ��ȡ������ϸ�е�ԭ���ܶ� xyq 2007.1.25
                    ForeignOriginalMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"], "OriginalCash");

                    //�ۼƱ��ȡ�����ϸ�е�ԭ�ҽ�� xyq 2007.1.25
//                    ForeignTotalChangeMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCostChange"], "ChangeCash");

                    budgetMoney = entity.GetDecimal("BudgetMoney");
                    adjustMoney = entity.GetDecimal("AdjustMoney");

                    ForeignTotalChangeMoney = ForeignTotalMoney - ForeignOriginalMoney;

                    lblOriginalMoney.Text = ForeignOriginalMoney.ToString("N");
                    lblTotalChangeMoney.Text = ForeignTotalChangeMoney.ToString("N");
                    lblBudgetMoney.Text = budgetMoney.ToString("N");
                    lblAdjustMoney.Text = adjustMoney.ToString("N");

                    lblCheckPerson.Text = BLL.SystemRule.GetUserName(entity.GetString("CheckPerson"));
                    lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");

                    lblBaoHan.Text = entity.GetDecimal("BaoHan").ToString("N");
                    this.lblDevelopUnit.Text = entity.GetString("DevelopUnit");

                    ViewState["_TotalMoney"] = totalMoney;

                    if (entity.GetInt("Mostly") == 1)
                    {
                        lblMostly.Text = "��Ҫ���";
                    }
                    else
                    {
                        lblMostly.Text = "����Ҫ���";
                    }

                    this.lblThirdParty.Text = entity.GetString("ThirdParty");
                    this.lblUnitName.Text = BLL.SystemRule.GetUnitName(entity.GetString("UnitCode"));
                    this.lblContractObject.Text = entity.GetString("ContractObject").Replace("\n", "<br>");
                    this.lblContractArea.Text = entity.GetString("ContractArea").Replace("\n", "<br>");

                    this.lblAdIssueDate.Text = entity.GetDateTimeOnlyDate("AdIssueDate");

                    // 2007.1.25 ��ͷ����ʾ��ұ���
                    string ForeignMoneyType = BLL.ContractRule.GetForeignMoneyType(entity);
                    if (ForeignMoneyType != "")
                    {
                        ForeignMoneyType += "&nbsp;&nbsp;";
                        this.lblForeignMoneyType.Text = ForeignMoneyType;
                        this.lblForeignMoneyType2.Text = ForeignMoneyType;
                        this.lblForeignMoneyType3.Text = ForeignMoneyType;
                        this.lblForeignMoneyType4.Text = ForeignMoneyType;
                        this.lblForeignMoneyType5.Text = ForeignMoneyType;
                        this.lblForeignMoneyType6.Text = ForeignMoneyType;
                    }

                    int status = entity.GetInt("Status");
                    int ud_iChangeCount = entity.GetInt("ChangeCount");
                    // �趨��ͬ������
                    if (status == 4)
                    {
                        // Ѱ���ϴΰ汾
                        //						string ContractLabel="";
                        //						EntityData entityhistory=DAL.EntityDAO.ContractDAO.GetContractByCode(contractCode);
                        //						if(entityhistory.HasRecord())
                        //						{
                        //							ContractLabel=entityhistory.GetString("ContractLabel");
                        //						}

                        RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB = new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
                        //						CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Status,"4,6"));
                        //						CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractLabel,ContractLabel));
                        //						CSB.AddOrder("ChangeDate",false);
                        CSB.AddStrategy(new Strategy(DAL.QueryStrategy.ContractStrategyName.ContractCode, curContractCode));
                        string sql1 = CSB.BuildMainQueryString();
                        QueryAgent qa1 = new QueryAgent();
                        qa1.SetTopNumber(1);
                        EntityData entityTemp = qa1.FillEntityData("Contract", sql1);
                        qa1.Dispose();

                        // �Ա�
                        if (entityTemp.HasRecord())
                        {
                            if (entity.GetString("Type") != entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
                            if (entity.GetString("Remark") != entityTemp.GetString("Remark")) this.LabelRemark.BackColor = Color.Yellow;
                            if (entity.GetString("ContractName") != entityTemp.GetString("ContractName")) this.LabelContractName.BackColor = Color.Yellow;
                            if (entity.GetDateTimeOnlyDate("ContractDate") != entityTemp.GetDateTimeOnlyDate("ContractDate")) this.LabelContractDate.BackColor = Color.Yellow;
                            if (entity.GetString("ContractPerson") != entityTemp.GetString("ContractPerson")) this.LabelContractPerson.BackColor = Color.Yellow;
                            if (entity.GetString("SupplierCode") != entityTemp.GetString("SupplierCode")) this.hrefSupplier.Style["background-color"] = "Yellow";
                            if (entity.GetDecimal("TotalMoney") != entityTemp.GetDecimal("TotalMoney")) this.lblTotalMoney.BackColor = Color.Yellow;
                            if (entity.GetString("ThirdParty") != entityTemp.GetString("ThirdParty")) this.lblThirdParty.BackColor = Color.Yellow;
                            if (entity.GetString("UnitCode") != entityTemp.GetString("UnitCode")) this.lblUnitName.BackColor = Color.Yellow;
                            if (entity.GetString("ContractObject") != entityTemp.GetString("ContractObject")) this.lblContractObject.BackColor = Color.Yellow;
                            //							if(entity.GetString("Type")==entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
                            //							if(entity.GetString("Type")==entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
                            //							if(entity.GetString("Type")==entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
                            //							if(entity.GetString("Type")==entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
                            //							if(entity.GetString("Type")==entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
                            //							if(entity.GetString("Type")==entityTemp.GetString("Type")) this.LabelType.BackColor = Color.Yellow;
                        }
                        entityTemp.Dispose();
                    }

                    string alloType = entity.GetString("AlloType");

                    //
                    //					// ��ͬ�Ѹ���δ����
                    //					decimal ahMoney = BLL.CBSRule.GetAHMoney("","","",contractCode,"1");
                    //					this.lblAPMoney.Text = BLL.StringRule.BuildShowNumberString( ahMoney);
                    //					this.lblUPMoney.Text = BLL.StringRule.BuildShowNumberString(totalMoney-ahMoney);

                    // ��ͬ״̬�� 0: ������ 1 ����ˣ���ǰ��ͬ�� 2 ��ͬ������ϣ����� �� 
                    //            3 ���벻ͨ�� �� 4 ������룻5 ������벻ͨ�������ϣ� 6 ��ʷ��¼ �� 7 ��ͬ������
                    //			  8 Ԥ��;9 Ԥ����
                    this.btnChange.Visible = false;
                    this.btnDelete.Visible = false;
                    this.btnCheck.Visible = false;
                    this.btnOldCheck.Visible = false;
                    this.btnModify.Visible = false;
                    this.btnAccount.Visible = false;
                    this.btnAccountManage.Visible = false;
                    this.btnNewPaymentApply.Visible = false;
                    this.btnDocument.Visible = false;
                    this.dgDocumentList.Columns[4].Visible = false;
                    this.btnHistory.Visible = false;
                    this.btnAuditingModify.Visible = false;

                    switch (this.up_sPMNameLower)
                    {
                        case "xinchangningpm":
                            this.btnPrint.Visible = true;
                            this.btnAccontPrint.Visible = true;
                            break;
                        default:
                            this.btnPrint.Visible = false;
                            this.btnAccontPrint.Visible = false;
                            break;
                    }

                    this.ucContractCostPlanView.ModifyPaymentPlan = false;


                    this.lblStatus.Text = BLL.ContractRule.GetContractStatusName(status.ToString());

                    // ���ԭ��ͬ״̬Ϊ������� Ȼ���ǵ�ǰ��ͬ
                    //					string tmp = BLL.ContractRule.GetContractVersionCode(contractCode,"4");
                    bool flag = false;

                    int rowcount=entity.Tables["contractchange"].Rows.Count ;
                    string changetype=string.Empty;
                    if(rowcount >0){
                        changetype=entity.Tables["contractchange"].Rows[rowcount-1]["ChangeType"].ToString();
                    }

                    switch (entity.GetInt("ChangeStatus"))
                    {
                        case 1://����
                        case 3://�����
                            if (changetype == "����")
                            {
                            this.lblStatus.Text += "  ��������";
                            }
                            else{
                            this.lblStatus.Text += "  �������";
                            }
                            this.btnModify.Visible = false;
                            btnAccount.Visible = false;
                            btnAccountManage.Visible = false;
                            flag = true;
                            break;
                        case 2://����
                            this.lblStatus.Text += "  �ѱ��";
                            break;
                        case 0://�ޱ��
                            break;
                    }
                    //ȡ���ڰ��������������
                    int oldtableindex = entity.CurrentTableIndex;
                    entity.SetCurrentTable("ContractAccount");

                    int AccountManagerProcNum = 0;
                    if (entity.CurrentTable.Rows.Count > 0)
                    {
                        AccountManagerProcNum = WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ�������", entity.GetString("ContractAccountCode"));
                    }
                    entity.SetCurrentTable(oldtableindex);

                    if (status == 0 || status == 4 || status == 6)
                    {
                        //						this.tdBefore.InnerText = "ԭ��ͬ���(���)��";

                        //						//ȡԭʼ��ͬ�ܼ۲��� xyq 2005.8.11
                        //						decimal oldMoney = entity.GetDecimal("OldSumMoney");
                        //						decimal balance = entity.GetDecimal("TotalMoney") - oldMoney;
                        //						this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(oldMoney) + "(" + BLL.StringRule.BuildShowNumberString(balance) + ")";

                        /*
                        // ȡ�õ�һ�ʺ�ͬ�ܽ��
                        RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB1=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
                        CSB1.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Status,"0,4,6"));
                        CSB1.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractLabel,entity.GetString("ContractLabel")));
                        CSB1.AddOrder("ChangeDate",true);
                        string sql2 = CSB1.BuildMainQueryString();
                        QueryAgent qa2 = new QueryAgent();
                        qa2.SetTopNumber(1);
                        EntityData entityfirst = qa2.FillEntityData("Contract",sql2);
                        qa2.Dispose();

                        decimal dtmp = 0.0m;
                        if(entityfirst.HasRecord())
                        {
                            dtmp = entity.GetDecimal("TotalMoney") - entityfirst.GetDecimal("TotalMoney");
                            this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(entityfirst.GetDecimal("TotalMoney"))+"("+BLL.StringRule.BuildShowNumberString(dtmp)+")";
                        }
                        */
                    }

                    string ud_sAuditingNameV2;


                    //
                    // ��ͬ״̬�� 0: ������ 1 ����ˣ���ǰ��ͬ�� 2 ��ͬ������ϣ����� �� 
                    //            3 ���벻ͨ�� �� 4 ������룻 6 ��ʷ��¼ �� 7 ��ͬ������


                    switch (status)
                    {
                        case 0:
                            this.btnChange.Visible = true;
                            this.ucContractCostPlanView.ModifyPaymentPlan = true;
                            //							this.btnAccount.Visible=true;
                            if (AccountManagerProcNum <= 0)
                            {
                                this.btnAccountManage.Visible = true;
                            }
                            this.btnAuditingModify.Visible = true;

                            this.btnPrint.Visible = true;
                            this.btnAccontPrint.Visible = true;

                            this.btnNewPaymentApply.Visible = true;
                            this.btnDocument.Visible = true;


                            string changingCode = BLL.ContractRule.GetChangingContractVersionCode(contractCode);
                            this.txtChangeContractCode.Value = changingCode;
                            this.dgDocumentList.Columns[4].Visible = true;

                            if (ud_iChangeCount > 0)
                            {
                                this.btnChange.Visible = true;
                                this.txtChangeContractCode.Value = contractCode;
                                this.btnNewPaymentApply.Visible = true;
                                this.ucContractCostPlanView.ModifyPaymentPlan = true;
                                this.btnPrint.Visible = true;
                                this.btnAccontPrint.Visible = true;
                            }


                            break;

                        case 1:
                            this.btnModify.Visible = true;
                            this.btnDelete.Visible = true;
                            this.btnCheck.Visible = true;
                            this.btnOldCheck.Visible = true;
                            this.btnNewProductionPaymentApply.Visible = false;
                            break;

                        case 2:
                            this.btnChange.Visible = false;
                            //							this.tdBefore.InnerText = "ԭ��ͬ��";
                            //							this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(entity.GetDecimal("BeforeAccountTotalMoney"));
                            this.btnNewProductionPaymentApply.Visible = false;
                            btnNewPaymentApply.Visible = true; //��ͬ������Ϻ���Լ������
                            break;

                        case 3:
                            this.btnNewProductionPaymentApply.Visible = false;
                            break;

                        case 4:
                            //this.btnModify.Visible = true;
                            this.btnChange.Visible = true;
                            this.txtChangeContractCode.Value = contractCode;
                            this.btnNewPaymentApply.Visible = true;
                            this.ucContractCostPlanView.ModifyPaymentPlan = true;
                            this.btnPrint.Visible = true;
                            this.btnAccontPrint.Visible = true;

                            //							this.tdBefore.InnerText = "ԭ��ͬ���(���)��";
                            //							//this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(entity.GetDecimal("BeforeAccountTotalMoney"));
                            //							// ȡ�õ�һ�ʺ�ͬ�ܽ��
                            //							EntityData entityfirst=DAL.EntityDAO.ContractDAO.GetContractByCode(entity.GetString("ContractLabel"));
                            //							decimal dtmp = 0.0m;
                            //							if(entityfirst.HasRecord())
                            //								dtmp = entity.GetDecimal("TotalMoney")-entityfirst.GetDecimal("TotalMoney");
                            //							this.lblBeforeAccountTotalMoney.Text = BLL.StringRule.BuildShowNumberString(entityfirst.GetDecimal("TotalMoney"))+"("+BLL.StringRule.BuildShowNumberString(dtmp)+")";

                            break;

                        case 5:
                            this.btnNewProductionPaymentApply.Visible = false;
                            break;

                        case 6:
                            this.btnNewProductionPaymentApply.Visible = false;
                            break;
                        case 7:
                            this.btnOldCheck.Visible = true;
                            this.btnNewProductionPaymentApply.Visible = false;
                            this.lblStatus.Text = "����������";
                            break;
                        case 8:
                            ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByName("��ͬ�������");
                            this.btnCheck.Visible = true;
                            this.btnOldCheck.Visible = true;
                            this.btnNewProductionPaymentApply.Visible = false;
                            this.lblStatus.Text = "������";
                            break;
                        case 9:
                            ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByName("��ͬ�������");
                            this.btnOldCheck.Visible = true;
                            this.btnNewProductionPaymentApply.Visible = false;
                            this.lblStatus.Text = "����������";
                            break;

                    }

                    if (flag) btnChange.Visible = false; //�������ʱ�������ύ�±�� 2018-12-13
                    if (flag) btnAccountManage.Visible = false; //�������ʱ�������ύ�±�� 2018-12-13

                    if (base.user.HasRight("050130"))
                    {
                        btnDelete.Visible = true;
                    }

                    //					// �鿴��ͬ��˵���û��,�о���ʾ�鿴��˵�,û�о�û��
                    //					EntityData contractAuditing = DAL.EntityDAO.ContractDAO.GetContractAuditingByContractCode(contractCode);
                    //					if ( contractAuditing.HasRecord())
                    //					{
                    //						this.btnCheckInfo.Visible = true;
                    //					}
                    //					contractAuditing.Dispose();

                }

                entity.SetCurrentTable("ContractExecutePlan");
                this.dgExecuteList.DataSource = new DataView(entity.CurrentTable, "", "ExecuteDate", DataViewRowState.CurrentRows);
                this.dgExecuteList.DataBind();

                // ��ͬ������ϸ
                BindCostDataGrid(entity);

                // ����ϸ, ͳ�Ʒ��÷ֽ��и�������ĸ������
                PaymentItemStrategyBuilder sbPaymentItem = new PaymentItemStrategyBuilder();
                sbPaymentItem.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, contractCode));
                sbPaymentItem.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                //				sbPaymentItem.AddOrder("PayDate", true);  //���븶�������� xyq 2005.8.9
                //				sbPaymentItem.AddOrder("PaymentItemCode", true);  //���븶�������� xyq 2005.8.9
                string sql = sbPaymentItem.BuildQueryViewString();
                QueryAgent qa = new QueryAgent();
                EntityData paymentItem = qa.FillEntityData("V_PaymentItem", sql);

                //�����¼
                //				string contractLabel = entity.Tables["Contract"].Rows[0]["ContractLabel"].ToString();
                //
                //				RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB1=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
                //
                //				// �ж��Ƿ�Ϊԭʼ��ͬ
                //
                //				if ( contractLabel == contractCode )
                //				{
                //					CSB1.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.NotOriginalContract));
                //				}
                //
                //				CSB1.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractLabel,contractLabel));
                //				sql = CSB1.BuildMainQueryString();
                //				QueryAgent qaTemp = new QueryAgent();
                //				EntityData entityChangeTemp = qaTemp.FillEntityData("Contract",sql);
                //				qaTemp.Dispose();


                //��ع���
                entity.SetCurrentTable("TaskContract");
                entity.CurrentTable.Columns.Add("TaskName");
                entity.CurrentTable.Columns.Add("UserNames");
                entity.CurrentTable.Columns.Add("CompletePercent");
                int iCount = entity.CurrentTable.Rows.Count;
                for (int i = 0; i < iCount; i++)
                {
                    entity.SetCurrentRow(i);
                    string WBSCode = entity.GetString("WBSCode");

                    EntityData entityTask = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
                    if (entityTask.HasRecord())
                    {
                        entity.CurrentRow["TaskName"] = entityTask.CurrentRow["TaskName"];
                        entity.CurrentRow["CompletePercent"] = entityTask.CurrentRow["CompletePercent"];
                    }
                    entityTask.Dispose();

                    DataTable tbGroup = BLL.WBSRule.GetTaskPersonNameGroupByType(WBSCode);
                    entity.CurrentRow["UserNames"] = BLL.WBSRule.GetTaskPersonNameMaster(tbGroup);
                }
                this.dgTaskList.DataSource = new DataView(entity.CurrentTable, "", "", DataViewRowState.CurrentRows);
                this.dgTaskList.DataBind();

                //��ʾ��ͬ��ֵ
                if (this.divContractProduction.Visible)
                {
                    entity.SetCurrentTable("ContractProduction");

                    //���ۼ�Լ����ֵ
                    SetContractProductionTotalValue(entity.CurrentTable, 0);

                    //���ۼ�ʵ�ʲ�ֵ
                    SetContractProductionTotalValue(entity.CurrentTable, 1);

                    //Լ��
                    BindValueList(entity.CurrentTable, 0);

                    //ʵ��
                    BindValueList(entity.CurrentTable, 1);
                }

                //��ʾ��ͬ��������
                if (this.divMaterial.Visible)
                {
                    entity.SetCurrentTable("ContractMaterial");
                    BindMaterialList(entity.CurrentTable);
                    BindMaterialInList();
                    BindMaterialOutList();
                }

                //�������
                DataTable ud_dtWorkFlowFilter = new DataTable();
                ud_dtWorkFlowFilter.Columns.Add("ProcedureName");
                ud_dtWorkFlowFilter.Columns.Add("ApplicationCode");
                DataRow drWFFAdd = null;

                if (biddingCode != "")
                {
                    DataSet ud_BiddingForm = BLL.Bidding.GetBiddingForm(biddingCode);

                    foreach (DataRow dr in ud_BiddingForm.Tables[0].Rows)
                    {
                        drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                        drWFFAdd["ProcedureName"] = "Ͷ�굥λ����";
                        drWFFAdd["ApplicationCode"] = dr["BiddingPrejudicationCode"].ToString();
                        ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                    }

                    foreach (DataRow dr in ud_BiddingForm.Tables[1].Rows)
                    {
                        drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                        drWFFAdd["ProcedureName"] = "�б굥λ����";
                        drWFFAdd["ApplicationCode"] = dr["BiddingEmitCode"].ToString();
                        ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);


                    }

                    foreach (DataRow dr in ud_BiddingForm.Tables[2].Rows)
                    {
                        drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                        drWFFAdd["ProcedureName"] = "�б�֪ͨ������";
                        drWFFAdd["ApplicationCode"] = dr["BiddingMessageCode"].ToString();
                        ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                    }




                }


                if (contractCode != "")
                {
                    RmsPM.BFL.LocaleViseBFL cLocalViseBFL = new RmsPM.BFL.LocaleViseBFL();
                    TiannuoPM.MODEL.LocaleViseQueryModel cLocalViseQueryModel = new TiannuoPM.MODEL.LocaleViseQueryModel();
                    cLocalViseQueryModel.ViseContractCode = contractCode;
                    List<TiannuoPM.MODEL.LocaleViseModel> cLocalVice = new List<TiannuoPM.MODEL.LocaleViseModel>();
                    cLocalVice = cLocalViseBFL.GetLocalVises(cLocalViseQueryModel);

                    for (int tempi = 0; tempi < cLocalVice.Count; tempi++)
                    {
                        drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                        drWFFAdd["ProcedureName"] = "ǩ֤���";
                        drWFFAdd["ApplicationCode"] = cLocalVice[tempi].ViseCode;
                        ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                    }

                    string PMName = System.Configuration.ConfigurationManager.AppSettings["PMName"];
                    if (PMName.ToLower() == "tangchenpm")
                    {
                        RmsPM.BLL.TC_OA_BiddingContract cBiddingContract = new TC_OA_BiddingContract();
                        cBiddingContract.ContractCode = contractCode;
                        DataTable dtBiddingContract = cBiddingContract.GetTC_OA_BiddingContracts();
                        if (dtBiddingContract != null)
                        {
                            foreach (DataRow drBiddingContract in dtBiddingContract.Select())
                            {
                                drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                                drWFFAdd["ProcedureName"] = "��ͬ��������";
                                drWFFAdd["ApplicationCode"] = drBiddingContract["TC_OA_BiddingContractCode"].ToString();
                                ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                            }
                        }
                    }
                }

                //��ͬ�������
                drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                drWFFAdd["ProcedureName"] = "��ͬ���";
                drWFFAdd["ApplicationCode"] = contractCode;
                ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);

                //��ͬ�����������

                EntityData entityContractAccount = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
                foreach (DataRow drContractAccount in entity.Tables["ContractAccount"].Select())
                {
                    drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                    drWFFAdd["ProcedureName"] = "��ͬ�������";
                    drWFFAdd["ApplicationCode"] = drContractAccount["ContractAccountCode"].ToString();
                    ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                }
                //��ͬ����������
                DataView dv = new DataView(entity.Tables["ContractChange"], "Status in (0,1,2)", "", System.Data.DataViewRowState.CurrentRows);

                if (dv.Count > 0)
                {
                    foreach (DataRow dr in dv.Table.Rows)
                    {
                        drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                        drWFFAdd["ProcedureName"] = "��ͬ������";
                        drWFFAdd["ApplicationCode"] = dr["ContractChangeCode"];
                        ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                    }
                    this.dgChangeList.DataSource = dv;
                    this.dgChangeList.DataBind();
                    div_ChangeList.Visible = true;
                }
                else
                {
                    div_ChangeList.Visible = false;
                }

                /*
                 * ���������ĵ����ص��ǵ�ǰ��Ч�ĺ�ͬ�汾�� ������Ϣ����ع���������ƻ����ص����Լ��ġ�
                 */
                // ��
                if (curContractCode != "")
                {
                    PaymentStrategyBuilder sb = new PaymentStrategyBuilder("V_Payment");
                    sb.AddStrategy(new Strategy(PaymentStrategyName.ContractCode, curContractCode));
                    sb.AddOrder("PayDate", true);
                    sb.AddOrder("PaymentCode", true);
                    sql = sb.BuildMainQueryString();
                    EntityData payment = qa.FillEntityData("V_Payment", sql);
                    qa.Dispose();
                    //������Ϣ�У���������ԭ�����oldmoney=isnull(oldmoney,money)��������ֶΣ��ڸ�������ܱ���ɸ�����
                    ViewState["SumPayMoney"] = BLL.MathRule.SumColumn(payment.CurrentTable, "OldMoney");
                    ViewState["SumPayOutMoney"] = BLL.MathRule.SumColumn(payment.CurrentTable, "TotalPayoutMoney");

                    int ud_iNewIssue = 0;

                    foreach (DataRow dr in payment.CurrentTable.Select("", "Issue DESC", DataViewRowState.CurrentRows))
                    {
                        ud_iNewIssue = dr["Issue"] == DBNull.Value ? 1 : (int)dr["Issue"] + 1;
                        break;
                    }

                    ViewState["NewIssue"] = ud_iNewIssue.ToString();

                    DataView ud_dvPayment = new DataView(payment.CurrentTable);

                    ud_dvPayment.Sort = "Issue";

                    //��ʼ���ڳ��ۼ��Ѹ� 
                    ViewState["LastTotalPayoutMoney"] = 0;

                    //��ʼ���ڳ��ۼ�Ӧ�� 
                    ViewState["LastTotalPaymentMoney"] = 0;

                    this.dgPaymentList.DataSource = ud_dvPayment;
                    this.dgPaymentList.DataBind();

                    foreach (DataRow dr in payment.CurrentTable.Rows)
                    {
                        drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                        drWFFAdd["ProcedureName"] = "��ͬ������";
                        drWFFAdd["ApplicationCode"] = dr["PaymentCode"];
                        ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                    }


                    switch (up_sPMName)
                    {
                        case "ShiMaoPM":
                            if (payment.Tables["V_Payment"].Select("status in (0,3)").Length > 0)
                            {
                                btnNewPaymentApply.Visible = false;
                                this.btnNewProductionPaymentApply.Visible = false;
                            }
                            break;

                    }

                    payment.Dispose();

                }

                //ǩ֤
                //Vise_ViseMessage vvm = new Vise_ViseMessage();
                //vvm.ViseType = "0";
                //vvm.ViseContractID = contractCode;
                //DataTable dtVise =  vvm.GetVise_ViseMessages();
                //foreach ( DataRow dr in dtVise.Rows)
                //{
                //    drWFFAdd = ud_dtWorkFlowFilter.NewRow();
                //    drWFFAdd["ProcedureName"] = "ǩ֤���";// System.Configuration.ConfigurationSettings.AppSettings["ViseAuditingName"].ToString();
                //    drWFFAdd["ApplicationCode"] = dr["ViseCode"];
                //    ud_dtWorkFlowFilter.Rows.Add(drWFFAdd);
                //}

                string ud_sProcedureNameAndApplicationCodeList = "''";

                foreach (DataRow dr in ud_dtWorkFlowFilter.Rows)
                {
                    ud_sProcedureNameAndApplicationCodeList += ",'";
                    ud_sProcedureNameAndApplicationCodeList += dr["ProcedureName"].ToString() + dr["ApplicationCode"].ToString();
                    ud_sProcedureNameAndApplicationCodeList += "'";
                }

                this.ucWorkFlowList.ProcedureNameAndApplicationCodeList = ud_sProcedureNameAndApplicationCodeList;
                this.ucWorkFlowList.DataBound();

                entity.Dispose();
                paymentItem.Dispose();

                //���غ�ͬ������ĵ�
                LoadDocument();

                //���غ�ͬ��Ʊ
                LoadContractBill();

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "���غ�ͬ����ʧ�ܡ�");
                Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ����ʧ�ܣ�" + ex.Message));
            }
        }

        /// <summary>
        /// ���غ�ͬ��Ʊ
        /// </summary>
        private void LoadContractBill()
        {
            try
            {
                string sql = "select * from ContractBill where projectcode='" + Request["ProjectCode"].ToString() + "' and contractcode='" + Request["ContractCode"].ToString() + "'";
                QueryAgent qa = new QueryAgent();
                DataSet ds = qa.ExecSqlForDataSet(sql);
                qa.Dispose();

                string[] arrField = { "BillMoney" };
                decimal[] arrSum = BLL.MathRule.SumColumn(ds.Tables[0], arrField);
                ViewState["SumTotalBillMoney"] = arrSum[0];

                DataView dvContractBill = new DataView(ds.Tables[0]);

                dvContractBill.Sort = "Code";

                this.dgContractBillList.DataSource = dvContractBill;
                this.dgContractBillList.DataBind();
                this.GridPagination1.RowsCount = ds.Tables[0].Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "���غ�ͬ��Ʊ����");
                Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ��Ʊ����" + ex.Message));
            }
        }

        protected void dgContractBillList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    break;
                case ListItemType.Footer:
                    ((Label)e.Item.FindControl("lblSumTotalBillMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumTotalBillMoney"]);
                    break;
                default:
                    break;
            }
        }

        protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
        {
            try
            {
                this.LoadContractBill();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
            }
        }
        //���غ�ͬ������ĵ�
        private void LoadDocument()
        {
            string Code = (string)this.ViewState["CurrentContractCode"];
            try
            {
                ArrayList ar = new ArrayList();
                ar.Add("000001");
                ar.Add(Code);

                DocumentStrategyBuilder DSB = new DocumentStrategyBuilder();
                DSB.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.Code, ar));
                string Sql = DSB.BuildMainQueryString();
                QueryAgent QA = new QueryAgent();
                EntityData entityDocument = QA.FillEntityData("Document", Sql);
                QA.Dispose();

                this.dgDocumentList.DataSource = entityDocument;
                this.dgDocumentList.DataBind();
                entityDocument.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ������ĵ�����" + ex.Message));
            }
        }


        /// <summary>
        /// ��ͬ��ֵ�ӡ��ۼƲ�ֵ����
        /// </summary>
        /// <param name="tb"></param>
        private void SetContractProductionTotalValue(DataTable tb, int IsFact)
        {
            try
            {
                if (!tb.Columns.Contains("TotalProductionValue"))
                {
                    tb.Columns.Add("TotalProductionValue", typeof(decimal));
                }

                decimal TotalValue = 0;
                DataView dv = new DataView(tb, string.Format("IsFact={0}", IsFact), "ProductionDate", DataViewRowState.CurrentRows);
                foreach (DataRowView drv in dv)
                {
                    DataRow dr = drv.Row;
                    TotalValue += BLL.ConvertRule.ToDecimal(dr["ProductionValue"]);
                    dr["TotalProductionValue"] = TotalValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("��ͬ��ֵ���ۼƲ�ֵ����" + ex.Message);
            }
        }

        /// <summary>
        /// ��ʾ��ͬ��ֵ��Լ����ʵ�ʣ�
        /// </summary>
        private void BindValueList(DataTable tb, int IsFact)
        {
            try
            {
                DataView dv = new DataView(tb, string.Format("IsFact={0}", IsFact), "ProductionDate", DataViewRowState.CurrentRows);

                if (IsFact == 0)
                {
                    this.dgValueList.DataSource = dv;
                    this.dgValueList.DataBind();
                }
                else
                {
                    this.dgFactValueList.DataSource = dv;
                    this.dgFactValueList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                if (IsFact == 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԼ����ֵ����" + ex.Message));
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾʵ�ʲ�ֵ����" + ex.Message));
                }
            }
        }

        /// <summary>
        /// ��ʾ��ͬ��������
        /// </summary>
        private void BindMaterialList(DataTable tb)
        {
            try
            {
                string contractCode = Request["ContractCode"] + "";

                if (!tb.Columns.Contains("InQty")) tb.Columns.Add("InQty", typeof(decimal));
                if (!tb.Columns.Contains("OutQty")) tb.Columns.Add("OutQty", typeof(decimal));

                foreach (DataRow dr in tb.Rows)
                {
                    dr["InQty"] = RmsPM.BFL.MaterialInBFL.GetMaterialInQtyByContract(contractCode, BLL.ConvertRule.ToInt(dr["MaterialCode"]));
                    dr["OutQty"] = RmsPM.BFL.MaterialOutBFL.GetMaterialOutQtyByContract(contractCode, BLL.ConvertRule.ToInt(dr["MaterialCode"]));
                }

                string[] arrField = { "Qty", "Money", "InQty", "OutQty" };
                decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
                ViewState["MaterialSumQty"] = arrSum[0];
                ViewState["MaterialSumMoney"] = arrSum[1];
                ViewState["MaterialSumInQty"] = arrSum[2];
                ViewState["MaterialSumOutQty"] = arrSum[3];

                DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);
                this.dgMaterialList.DataSource = dv;
                this.dgMaterialList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ͬ�����������" + ex.Message));
            }
        }

        /// <summary>
        /// ��ʾ��ͬ�������
        /// </summary>
        private void BindMaterialInList()
        {
            try
            {
                string contractCode = Request["ContractCode"] + "";

                List<TiannuoPM.MODEL.MaterialInDtlModel> lst = RmsPM.BFL.MaterialInBFL.GetMaterialInDtlListByContract(contractCode);

                decimal InQty = 0;
                decimal InMoney = 0;
                foreach (TiannuoPM.MODEL.MaterialInDtlModel model in lst)
                {
                    InQty += model.InQty;
                    InMoney += model.InMoney;
                }

                ViewState["MaterialInSumInQty"] = InQty;
                ViewState["MaterialInSumInMoney"] = InMoney;

                this.dgMaterialInList.DataSource = lst;
                this.dgMaterialInList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ͬ����������" + ex.Message));
            }
        }

        /// <summary>
        /// ��ʾ��ͬ��������
        /// </summary>
        private void BindMaterialOutList()
        {
            try
            {
                string contractCode = Request["ContractCode"] + "";

                List<TiannuoPM.MODEL.MaterialOutDtlModel> lst = RmsPM.BFL.MaterialOutBFL.GetMaterialOutDtlListByContract(contractCode);

                decimal OutQty = 0;
                decimal OutMoney = 0;
                foreach (TiannuoPM.MODEL.MaterialOutDtlModel model in lst)
                {
                    OutQty += model.OutQty;
                    OutMoney += model.OutMoney;
                }

                ViewState["MaterialOutSumOutQty"] = OutQty;
                ViewState["MaterialOutSumOutMoney"] = OutMoney;

                this.dgMaterialOutList.DataSource = lst;
                this.dgMaterialOutList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ͬ����������" + ex.Message));
            }
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

        override protected void InitEventHandler()
        {
            this.dgPaymentList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPaymentList_ItemDataBound);
            this.dgDocumentList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDocumentList_DeleteCommand);
            this.dgCostList.ItemDataBound += new DataGridItemEventHandler(this.dgCostList_ItemDataBound);
        }



        private void dgDocumentList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string code = Request["ContractCode"] + "";
            BLL.DocumentRule.Instance().DeleteDocumentConfig(e.Item.Cells[0].Text, "000001", code);

            //���غ�ͬ������ĵ�
            LoadDocument();

        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            string contractCode = Request["ContractCode"] + "";
            string projectCode = ViewState["ProjectCode"] + "";

            try
            {
                EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);

                // ��
                PaymentStrategyBuilder sb = new PaymentStrategyBuilder();
                sb.AddStrategy(new Strategy(PaymentStrategyName.ContractCode, contractCode));
                sb.AddOrder("ApplyDate", true);
                string sql = sb.BuildMainQueryString();
                QueryAgent qa = new QueryAgent();
                EntityData payment = qa.FillEntityData("V_Payment", sql);
                qa.Dispose();
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ������", contractCode) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ���ڰ����̣������������"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ���", contractCode) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ���ڰ����̣������������"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ�������", contractCode) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ���ڰ����̣������������"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ�������", contractCode) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ���ڰ����̣������������"));
                    return;
                }
                foreach (DataRow dr in payment.CurrentTable.Rows)
                {
                    string paymentCode = (string)dr["PaymentCode"];
                    if (BLL.PaymentRule.GetPayoutMoneyByPayment(paymentCode) > 0)
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "���и����¼��������������¼"));
                        return;
                    }
                    if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("�Ǻ�ͬ������", paymentCode) > 0)
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "�����ڰ����̣������������"));
                        return;
                    }
                    if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ������", paymentCode) > 0)
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "�����ڰ����̣������������"));
                        return;
                    }
                }
                foreach (DataRow dr in payment.CurrentTable.Rows)
                {
                    string paymentCode = (string)dr["PaymentCode"];
                    EntityData paymentStandardEntity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
                    DAL.EntityDAO.PaymentDAO.DeleteStandard_Payment(paymentStandardEntity);
                }
                DAL.EntityDAO.ContractDAO.DeleteStandard_Contract(entity);
                payment.Dispose();
                entity.Dispose();

                // ɾ������
                this.myAttachMentList.AttachMentType = "ContractAttachMent";
                this.myAttachMentList.DelAttachMentByMasterCode(contractCode);
                LogHelper.WriteLog("��ͬɾ��:" + contractCode + " UserCode:" + ((User)Session["User"]).UserCode + " " + Request.UserHostAddress);
                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
                //Response.End();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "ɾ����ͬ����");
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ����ͬ����" + ex.Message));
            }

        }

        protected void btnAccount_Click(object sender, System.EventArgs e)
        {
            string contractCode = Request["ContractCode"] + "";
            string projectCode = ViewState["ProjectCode"] + "";

            try
            {
                EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
                if (entity.HasRecord())
                {
                    DataRow mainTableRow = entity.CurrentRow;
                    mainTableRow["Status"] = 2;


                    // ����ϸ, ͳ�Ʒ��÷ֽ��и�������ĸ������
                    PaymentItemStrategyBuilder sbPaymentItem = new PaymentItemStrategyBuilder();
                    sbPaymentItem.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, contractCode));
                    sbPaymentItem.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                    QueryAgent qa = new QueryAgent();
                    string sql = sbPaymentItem.BuildQueryViewString();
                    EntityData paymentItem = qa.FillEntityData("V_PaymentItem", sql);
                    qa.Dispose();

                    //����ֽ�,����֮�󣬿���ֽ����ս����Ϊ�Ѹ����ͬ�ܶ�䶯�����м�¼��ͬ��ԭ���ܽ��
                    entity.SetCurrentTable("ContractAllocation");
                    foreach (DataRow dr in entity.CurrentTable.Rows)
                    {
                        string allocateCode = (string)dr["AllocateCode"];
                        decimal apMoney = BLL.MathRule.SumColumn(paymentItem.CurrentTable, "ItemMoney", String.Format("AllocateCode='{0}'", allocateCode));
                        dr["Money"] = apMoney;
                    }
                    decimal newTotalMoney = BLL.MathRule.SumColumn(entity.CurrentTable, "Money");
                    mainTableRow["BeforeAccountTotalMoney"] = mainTableRow["TotalMoney"];
                    mainTableRow["TotalMoney"] = newTotalMoney;

                    DAL.EntityDAO.ContractDAO.UpdateStandard_Contract(entity);
                    paymentItem.Dispose();
                }
                entity.Dispose();
                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write("window.location.href = '../Contract/ContractInfo.aspx?ProjectCode=" + projectCode + "&ContractCode=" + contractCode + "';");
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ͬ�������");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ͬ�������" + ex.Message));
            }

        }

        protected void btnRefreshDocument_ServerClick(object sender, System.EventArgs e)
        {
            LoadDocument();
        }

        protected void dgChangeList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;
                    
                    Label lblAuditedTotalChangeMoney = (Label)e.Item.FindControl("lblAuditedTotalChangeMoney");

                    System.Web.UI.HtmlControls.HtmlAnchor Alink = e.Item.FindControl("ALink") as System.Web.UI.HtmlControls.HtmlAnchor ;
                  
                    if(ud_drvItem["changetype"].ToString()=="����"){
                        Alink.Style.Add("color", "red");
                    }
                    if (ud_drvItem["Status"].ToString() == "0")
                    {
                        lblAuditedTotalChangeMoney.Text = (BLL.ConvertRule.ToDecimal(ud_drvItem["TotalChangeMoney"]) + BLL.ConvertRule.ToDecimal(ud_drvItem["ChangeMoney"])).ToString("N");
                    }
                    else
                    {
                        lblAuditedTotalChangeMoney.Text = "";
                    }

                    break;
            }
        }

        private void dgPaymentList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    decimal totalMoney, AHMoney, APMoney, UHMoney, ud_deTotalPayMoney, ud_dePayoutMoney;

                    totalMoney = (decimal)ViewState["_TotalMoney"];
                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                    AHMoney = ud_drvItem.Row["AHMoney"] != System.DBNull.Value ?
                        (decimal)ud_drvItem.Row["AHMoney"] : Decimal.Zero;

                    ud_dePayoutMoney = ud_drvItem.Row["PayoutMoney"] != System.DBNull.Value ?
                        (decimal)ud_drvItem.Row["PayoutMoney"] : Decimal.Zero;

                    //�ۼ�Ӧ���ĳɶ�̬���㣬���ڳ��ۼ�Ӧ����������� xyq 2018.12.25
                    decimal TotalPaymentMoney = BLL.ConvertRule.ToDecimal(ViewState["LastTotalPaymentMoney"]) + BLL.ConvertRule.ToDecimal(ud_drvItem.Row["OldMoney"]);
                    ViewState["LastTotalPaymentMoney"] = TotalPaymentMoney;
                    ((Label)e.Item.FindControl("lblTotalPaymentMoney")).Text = TotalPaymentMoney.ToString("N");

                    //�ۼ�Ӧ��% xyq 2018.12.25
                    decimal TotalPaymentMoneyPercent = (totalMoney==0)?0: (TotalPaymentMoney / totalMoney);

                    //�ۼ��Ѹ��ĳɶ�̬���㣬���ڳ��ۼ��Ѹ������ڸ��� xyq 2018.11.13
                    APMoney = BLL.ConvertRule.ToDecimal(ViewState["LastTotalPayoutMoney"]) + ud_dePayoutMoney;
                    ViewState["LastTotalPayoutMoney"] = APMoney;

                    UHMoney = totalMoney - TotalPaymentMoney;
                    decimal AHper = (totalMoney==0)?0: (AHMoney / totalMoney);
                    decimal APper = (totalMoney==0)?0: (APMoney / totalMoney);

                    ((Label)e.Item.FindControl("lblPercentAHMoney")).Text = AHper.ToString("P");
                    ((Label)e.Item.FindControl("lblUHMoney")).Text = UHMoney.ToString("N");
                    ((Label)e.Item.FindControl("lblTotalPaymentMoneyPercent")).Text = TotalPaymentMoneyPercent.ToString("P");

                    ((Label)e.Item.FindControl("lblAPMoney")).Text = APMoney.ToString("N");
                    ((Label)e.Item.FindControl("lblPercentAPMoney")).Text = APper.ToString("P");

                    if (ud_drvItem.Row["Remark"].ToString().Length > 10)
                    {
                        ((Label)e.Item.FindControl("lblRemark")).Text = "<a href=\"##\" onclick=\"JavaScript:OpenMiddleWindow('../Finance/PaymentItemDescriptionView.aspx?PaymentCode="
                            + ud_drvItem.Row["PaymentCode"].ToString() + "','�鿴��ע')\">" + ud_drvItem.Row["Remark"].ToString().Substring(0, 10) + "..." + "</a>";
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblRemark")).Text = ud_drvItem.Row["Remark"].ToString();

                    }
                    break;
                case ListItemType.Footer:
                    ((Label)e.Item.FindControl("lblSumMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumPayMoney"]);
                    ((Label)e.Item.FindControl("lblSumPayOutMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumPayOutMoney"]);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ��ȡ��ͬ������ϸ
        /// </summary>
        private void BindCostDataGrid(EntityData entity)
        {
            ViewState["_SumCostMoney"] = Decimal.Zero;
            ViewState["_SumAHMoney"] = Decimal.Zero;
            ViewState["_SumAPMoney"] = Decimal.Zero;
            ViewState["_SumUPMoney"] = Decimal.Zero;

            DataTable ud_dtCostList = entity.Tables["ContractCostCash"].Clone();

            foreach (DataColumn dc in entity.Tables["ContractCost"].Columns)
            {
                switch (dc.ColumnName)
                {
                    case "CostCode":
                    case "CostBudgetDtlCode":
                    case "CostBudgetSetCode":
                    case "PBSType":
                    case "PBSCode":
                        ud_dtCostList.Columns.Add(dc.ColumnName, dc.DataType);
                        break;
                }
            }

            foreach (DataRow drCost in entity.Tables["ContractCost"].Rows)
            {
                string ud_sCashFilter = string.Format("ContractCostCode='{0}'", drCost["ContractCostCode"].ToString());

                foreach (DataRow drCash in entity.Tables["ContractCostCash"].Select(ud_sCashFilter))
                {
                    DataRow drNew = ud_dtCostList.NewRow();

                    foreach (DataColumn dc in entity.Tables["ContractCost"].Columns)
                    {
                        switch (dc.ColumnName)
                        {
                            case "CostCode":
                            case "CostBudgetDtlCode":
                            case "CostBudgetSetCode":
                            case "PBSType":
                            case "PBSCode":
                                drNew[dc.ColumnName] = drCost[dc.ColumnName];
                                break;
                        }
                    }

                    foreach (DataColumn dc in entity.Tables["ContractCostCash"].Columns)
                    {
                        drNew[dc.ColumnName] = drCash[dc.ColumnName];
                    }

                    ud_dtCostList.Rows.Add(drNew);
                }
            }


            dgCostList.DataSource = ud_dtCostList;
            dgCostList.DataBind();
        }

        private void dgCostList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            decimal ud_deSumCostMoney = ViewState["_SumCostMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumCostMoney"];
            decimal ud_deSumAHMoney = ViewState["_SumAHMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumAHMoney"];
            decimal ud_deSumAPMoney = ViewState["_SumAPMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumAPMoney"];
            decimal ud_deSumUPMoney = ViewState["_SumUPMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumUPMoney"];
            decimal ud_deTotalMoney = BLL.ConvertRule.ToDecimal(ViewState["_TotalMoney"].ToString());

            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                    UserControls.InputCostBudgetDtl ud_ucCostBudgetDtl = (UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");
                    UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");
                    Label ud_lblPBSName = (Label)e.Item.FindControl("lblPBSName");
                    Label ud_lblCostName = (Label)e.Item.FindControl("lblCostName");
                    Label ud_lblAHMoney = (Label)e.Item.FindControl("lblAHMoney");
                    Label ud_lblAHMoneyPer = (Label)e.Item.FindControl("lblAHMoneyPer");
                    Label ud_lblAPMoney = (Label)e.Item.FindControl("lblAPMoney");
                    Label ud_lblUPMoney = (Label)e.Item.FindControl("lblUPMoney");
                    Label ud_lblAHCash = (Label)e.Item.FindControl("lblAHCash");
                    Label ud_lblAHCashPer = (Label)e.Item.FindControl("lblAHCashPer");
                    Label ud_lblAPCash = (Label)e.Item.FindControl("lblAPCash");
                    Label ud_lblUPCash = (Label)e.Item.FindControl("lblUPCash");

                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                    string ud_sProjectCode = Request["ProjectCode"] + "";
                    string ud_sContractCode = Request["ContractCode"] + "";
                    string ud_sContractCostCode = ud_drvItem["ContractCostCode"].ToString();
                    string ud_sContractCostCashCode = ud_drvItem["ContractCostCashCode"].ToString();

                    ud_ucCostBudgetDtl.CostBudgetSetCode = ud_drvItem["CostBudgetSetCode"].ToString();
                    ud_ucCostBudgetDtl.CostCode = ud_drvItem["CostCode"].ToString();

                    ud_lblCostName.Text = ud_ucCostBudgetDtl.CostName;
                    ud_lblPBSName.Text = ud_ucCostBudgetDtl.PBSName;

                    // �Ѹ���δ����
                    decimal ud_deCash = BLL.ConvertRule.ToDecimal(ud_drvItem["Cash"]);
                    decimal ud_deAHCash = BLL.CBSRule.GetAHCash("", "", "", ud_sContractCode, "1", ud_sContractCostCashCode);
                    decimal ud_deExchangeRate = BLL.ConvertRule.ToDecimal(ud_drvItem["ExchangeRate"]);
                    decimal ud_deAHMoney = ud_deAHCash * ud_deExchangeRate;
                    decimal ud_deMoney = ud_deCash * ud_deExchangeRate;
                    float ud_fAHMoneyPey = (ud_deTotalMoney == Decimal.Zero) ? 0 : (float)(ud_deAHMoney / ud_deTotalMoney);
                    float ud_fAHCashPey = (ud_deCash == Decimal.Zero) ? 0 : (float)(ud_deAHCash / ud_deCash);
                    decimal ud_deAPCash = BLL.CBSRule.GetAPCash(ud_sContractCode, ud_sContractCostCashCode);
                    decimal ud_deAPMoney = ud_deAPCash * ud_deExchangeRate;
                    decimal ud_deUPMoney = ud_deMoney - ud_deAPMoney;
                    decimal ud_deUPCash = ud_deCash - ud_deAPCash;


                    ud_ucExchangeRate.Cash = ud_deCash;
                    ud_ucExchangeRate.ExchangeRate = ud_deExchangeRate;
                    ud_ucExchangeRate.MoneyType = ud_drvItem["MoneyType"].ToString();
                    ud_ucExchangeRate.EditMode = false;
                    ud_ucExchangeRate.BindControl();


                    ud_lblAHCash.Text = ud_deAHCash.ToString("N");
                    ud_lblAHCashPer.Text = ud_fAHCashPey.ToString("P");

                    ud_lblAPCash.Text = ud_deAPCash.ToString("N");
                    ud_lblUPCash.Text = ud_deUPCash.ToString("N");


                    ud_lblAHMoney.Text = ud_deAHMoney.ToString("N");
                    ud_lblAHMoneyPer.Text = ud_fAHMoneyPey.ToString("#0.00%");

                    ud_lblAPMoney.Text = ud_deAPMoney.ToString("N");
                    ud_lblUPMoney.Text = ud_deUPMoney.ToString("N");

                    ud_deSumAHMoney += ud_deAHMoney;
                    ud_deSumAPMoney += ud_deAPMoney;
                    ud_deSumCostMoney += ud_deMoney;
                    ud_deSumUPMoney += ud_deUPMoney;

                    ViewState["_SumAHMoney"] = ud_deSumAHMoney;
                    ViewState["_SumAPMoney"] = ud_deSumAPMoney;
                    ViewState["_SumCostMoney"] = ud_deSumCostMoney;
                    ViewState["_SumUPMoney"] = ud_deSumUPMoney;

                    break;
                case ListItemType.Footer:
                    Label ud_lblSumAHMoney = (Label)e.Item.FindControl("lblSumAHMoney");
                    Label ud_lblSumAHMoneyPer = (Label)e.Item.FindControl("lblSumAHMoneyPer");
                    Label ud_lblSumAPMoney = (Label)e.Item.FindControl("lblSumAPMoney");
                    Label ud_lblSumUPMoney = (Label)e.Item.FindControl("lblSumUPMoney");
                    Label ud_lblSumCostMoney = (Label)e.Item.FindControl("lblSumCostMoney");

                    float ud_fSumAHMoneyPey = (ud_deTotalMoney == Decimal.Zero) ? 0 : (float)(ud_deSumAHMoney / ud_deTotalMoney);

                    ud_lblSumAHMoney.Text = ud_deSumAHMoney.ToString("N");
                    ud_lblSumAHMoneyPer.Text = ud_fSumAHMoneyPey.ToString("#0.00%");
                    ud_lblSumAPMoney.Text = ud_deSumAPMoney.ToString("N");
                    ud_lblSumUPMoney.Text = ud_deSumUPMoney.ToString("N");
                    ud_lblSumCostMoney.Text = ud_deSumCostMoney.ToString("N");
                    break;


            }
        }

        protected void dgMaterialList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Footer:
                    ((Label)e.Item.FindControl("lblSumQty")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialSumQty"]);
                    ((Label)e.Item.FindControl("lblSumMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialSumMoney"]);
                    ((Label)e.Item.FindControl("lblSumInQty")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialSumInQty"]);
                    ((Label)e.Item.FindControl("lblSumOutQty")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialSumOutQty"]);
                    break;
            }
        }

        protected void dgMaterialInList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Footer:
                    ((Label)e.Item.FindControl("lblSumInQty")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialInSumInQty"]);
                    ((Label)e.Item.FindControl("lblSumInMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialInSumInMoney"]);
                    break;
            }
        }

        protected void dgMaterialOutList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Footer:
                    ((Label)e.Item.FindControl("lblSumOutQty")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialOutSumOutQty"]);
                    ((Label)e.Item.FindControl("lblSumOutMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["MaterialOutSumOutMoney"]);
                    break;
            }
        }
    }
}
