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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using System.Configuration;


namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PaymentInfo ��ժҪ˵����
	/// </summary>
	public partial class PaymentInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefViewVoucher;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddDtl;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddFromCost;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();

                InitViewState();


			}
		}

        private void InitViewState()
        { 
            string ud_sProcedureName = "";


            string projectCode = this.txtProjectCode.Value;
            //��������url
            string ProcedureCode = "";
            if (txtIsContract.Value == "0")
            {


                ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("�Ǻ�ͬ������", projectCode);

                ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);
                ud_sProcedureName = "�Ǻ�ͬ������";
            }
            else
            {
                ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("��ͬ������", projectCode);
                ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);
                ud_sProcedureName = "��ͬ������";
            }
            ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("�÷ѱ��������", projectCode);
            ViewState["_AuditingAccountURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);


            if (BLL.WorkFlowRule.GetCaseCountByProcedureNameAndApplicationCode(ud_sProcedureName, this.txtPaymentCode.Value) > 0)
            {
                this.btnCheck.Visible = false;
                this.btnCheckPaymentCheck.Visible = false;
                this.btnSubmitAccount.Visible = false;
            }

            ProcedureCode = "";
            ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("��֧ͬƱ������", projectCode);
            ViewState["_CheckAuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);
           
            if (BLL.WorkFlowRule.GetCaseCountByProcedureNameAndApplicationCode("��֧ͬƱ������", this.txtPaymentCode.Value) > 0)
            {
                this.btnCheck.Visible = false;
                this.btnCheckPaymentCheck.Visible = false;
                this.btnSubmitAccount.Visible = false;
            }


            //�������ӡURL
            string ud_sPMName = System.Configuration.ConfigurationManager.AppSettings["PMName"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString();

            string ud_sCaseCode = BLL.WorkFlowRule.GetCaseCodeByProcedureNameAndApplicationCode(ud_sProcedureName, this.txtPaymentCode.Value);
            string ud_sPrintURL = "";

            switch (ud_sPMName)
            {
                case "XinChangNingPM":
//                    ud_sPrintURL = "../Finance/PaymentCheckPrint.aspx?ProjectCode=" + this.txtPaymentCode.Value + "&PaymentCode=" + this.txtPaymentCode.Value;
                    ud_sPrintURL = "../WorkFlowPrint/XCN_PaymentAuditing.aspx?ProjectCode=" + this.txtPaymentCode.Value + "&PaymentCode=" + this.txtPaymentCode.Value;
                    break;
                case "TianYangOA":
                    if (txtIsContract.Value == "0")
                    {
                        ud_sPrintURL = "../WorkFlowPrint/TY_PaymentAuditingV2.aspx?frameType=List&ApplicationCode=" + this.txtPaymentCode.Value + "&CaseCode=" + ud_sCaseCode;
                    }
                    else 
                    {
                        ud_sPrintURL = "../WorkFlowPrint/TY_PaymentAuditing.aspx?frameType=List&ApplicationCode=" + this.txtPaymentCode.Value + "&CaseCode=" + ud_sCaseCode;
                    }
                    break;
                default:
                    break;
            }

            ViewState["_PrintURL"] = ud_sPrintURL;

        }

		private void IniPage()
		{
			try
			{
				this.txtPaymentCode.Value = Request.QueryString["PaymentCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];




                this.myAttachMentList.AttachMentType = "PaymentAttachMent";
                this.myAttachMentList.MasterCode = this.txtPaymentCode.Value;

                switch (this.up_sPMNameLower)
                {
                    case "yixingpm":
                        this.btnSubmitAccount.Visible = true;
                        break;
                    default:
                        break;
                }



				//Ȩ��
//				this.btnModify.Visible = base.user.HasRight("060103");
//				this.btnModifyEx.Visible = base.user.HasRight("060106");
//				this.btnDelete.Visible = base.user.HasRight("060104");
//				this.btnCheck.Visible = base.user.HasRight("060105");
//				this.btnAccount.Visible = base.user.HasRight("060107");
//				this.btnPayout.Visible = base.user.HasRight("060202");
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}


		private void LoadData()
		{
			string paymentCode = this.txtPaymentCode.Value;

			try
			{
//				if (( isContract == "" ) || (isContract == "0"))
//				{
//					//�Ǻ�ͬ���
//					this.trContract.Visible = false;
//				}

				if ( paymentCode != "")
				{
					EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						//Ȩ��
						if (entity.GetString("GroupCode") != "") 
						{
							//�����
							ArrayList ar = user.GetResourceRight(paymentCode,"Payment");
							if ( ! ar.Contains("060101"))
							{
								Response.Redirect( "../RejectAccess.aspx?OperationCode=060101" );
								Response.End();
							}

							this.btnModify.Visible = ar.Contains("060103");
							this.btnModifyEx.Visible = ar.Contains("060106");
                            this.btnDelete.Visible = ar.Contains("060104") ;
							this.btnCheck.Visible = ar.Contains("060108");
							this.btnOldCheck.Visible = ar.Contains("060105");
							this.btnAccount.Visible = ar.Contains("060107");
							this.btnPayout.Visible = base.user.HasRight("060202");
                            this.btnPrint.Visible = ar.Contains("060109");
						}
						else 
						{
							//�����
							this.btnModify.Visible = base.user.HasRight("060103");
							this.btnModifyEx.Visible = base.user.HasRight("060106");
							this.btnDelete.Visible = base.user.HasRight("060104");
							this.btnCheck.Visible = base.user.HasRight("060108");
							this.btnOldCheck.Visible = base.user.HasRight("060105");
							this.btnAccount.Visible = base.user.HasRight("060107");
							this.btnPayout.Visible = base.user.HasRight("060202");
                            this.btnPrint.Visible = base.user.HasRight("060109");

						}

						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(this.txtProjectCode.Value);
						this.txtIsContract.Value = entity.GetInt("IsContract").ToString();
						this.txtContractCode.Value = entity.GetString("ContractCode");

						if (this.txtContractCode.Value != "") 
						{
							//��ʾ��ͬ��Ϣ
							EntityData entityCon = DAL.EntityDAO.ContractDAO.GetContractByCode(this.txtContractCode.Value);
							//							EntityData entityCon = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.txtContractCode.Value);
							if (entityCon.HasRecord()) 
							{
								this.lblContractID.Text = entityCon.GetString("ContractID");
								this.lblContractName.Text = entityCon.GetString("ContractName");
							}

							//							//��ͬ������ϸ
							//							DataTable tbAllo = entityCon.Tables["ContractAllocation"];
							//							tbAllo.Columns.Add("TotalPaymentMoney", typeof(System.Decimal));
							//							foreach(DataRow drAllo in tbAllo.Rows) 
							//							{
							//								drAllo["TotalPaymentMoney"] = BLL.PaymentRule.GetPaymentMoneyByContractAllocate(BLL.ConvertRule.ToString(drAllo["AllocateCode"]), 0);
							//							}
							//
							//							BindContractAllocationDataGrid(tbAllo);
							//							BindContractPlanDataGrid(entityCon.Tables["ContractPaymentPlan"]);

							entityCon.Dispose();
						}

						this.lblGroupName.Text = BLL.SystemGroupRule.GetSystemGroupFullName(entity.GetString("GroupCode"));

						this.lblApplyDate.Text = entity.GetDateTimeOnlyDate("ApplyDate");
						this.lblApplyPersonName.Text = BLL.SystemRule.GetUserName( entity.GetString("ApplyPerson"));
						this.lblUnitName.Text = BLL.SystemRule.GetUnitFullName(entity.GetString("UnitCode"));
						this.lblPayDate.Text = entity.GetDateTimeOnlyDate("PayDate");

                        this.lblPaymentTitle.Text = entity.GetString("PaymentTitle");

						decimal Money = BLL.ConvertRule.ToDecimal(dr["Money"]);
						decimal OldMoney = BLL.ConvertRule.ToDecimal(dr["OldMoney"]);
						this.lblMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(Money), "Ԫ");
						this.lblOldMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(OldMoney), "Ԫ");

                        this.lblBankName.Text = entity.GetString("BankName");
                        this.lblBankAccount.Text = entity.GetString("BankAccount");

                        switch (this.up_sPMName.ToUpper())
                        { 
                            case "TANGCHENPM":
                            case "SHIMAOPM":
                                ViewState["_TypeSortID"] = BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("GroupCode")).Substring(0, 2); ;
                                break;

                            default:
                                ViewState["_TypeSortID"] = "";
                                break;

                        }

						//�Ƿ��ֹ�����
						if ((OldMoney != 0) && (OldMoney != Money)) 
						{
							this.spanOldMoney.Style["display"] = "";
							this.spanOldMoney2.Style["display"] = "";
							this.dgList.Columns[4].Visible = true;
						}

						this.lblPaymentID.Text = entity.GetString("PaymentID");
						this.txtStatus.Value = entity.GetInt("status").ToString();
						this.lblRecieptCount.Text = entity.GetInt("RecieptCount").ToString();
						//						this.txtMoney.Text = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));
						this.lblPurpose.Text = entity.GetString("Purpose");
						this.lblSupplyName.Text = entity.GetString("SupplyName");
						this.lblPayer.Text = entity.GetString("Payer");

						this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName( entity.GetString("CheckPerson"));
						this.lblRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");

						this.lblIsContractName.Text = BLL.PaymentRule.GetPaymentIsContractName(entity.GetInt("IsContract"));
						this.lblStatusName.Text = BLL.PaymentRule.GetPaymentStatusName(entity.GetInt("status"));
                        this.lblPaymentName.Text = entity.GetString("PaymentName");

						this.lblOpinion.Text = entity.GetString("CheckOpinion");
						if(this.lblOpinion.Text.Length>0)
						{
							this.tbOpiniont.Visible = true;
							this.tbOpinionv.Visible = true;
						}

                        if (this.txtIsContract.Value == "0")
                        {
                            this.trContract.Visible = false;
                            this.trContract2.Visible = false;
                            this.trContract3.Visible = false;
                            this.trContract4.Visible = false;
                            this.trContract5.Visible = false;
                        }
                        else
                        {
                            this.TrPayment.Visible = false;
                        }

                        DataTable tbDtl = BLL.PaymentRule.GeneratePaymentItemTable(entity.Tables["PaymentItem"]);
						BindDataGrid(tbDtl);

                        //�ɱ�������������Ϣ�ϣ���ʾԤ������� xyq 2006.8.9
                        if (entity.GetString("Payer") == "�ɱ��������")
                        {
                            this.trCostBatchPayment.Style["display"] = "";

                            if (tbDtl.Rows.Count > 0)
                            {
                                string CostBudgetSetCode = BLL.ConvertRule.ToString(tbDtl.Rows[0]["CostBudgetSetCode"]);
                                this.lblCostBudgetSetName.Text = BLL.CostBudgetRule.GetCostBudgetSetName(CostBudgetSetCode);
                            }
                        }

                        switch (this.txtStatus.Value)
						{
							case "1"://����
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";
								this.btnOldCheck.Style["display"] = "none";
                                this.btnCheckPaymentCheck.Style["display"] = "none";

								this.btnModifyEx.Style["display"] = "";
								this.btnPayout.Style["display"] = "";
								this.btnAccount.Style["display"] = "";
                                this.btnSubmitAccount.Style["display"] = "none";

								break;

							case "2"://�Ѹ�
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";
								this.btnOldCheck.Style["display"] = "none";
                                this.btnCheckPaymentCheck.Style["display"] = "none";
                                this.btnSubmitAccount.Style["display"] = "none";

								this.btnModifyEx.Style["display"] = "";
								this.btnPayout.Style["display"] = "none";
								this.btnAccount.Style["display"] = "none";
                                this.btnCheckDelete.Style["display"] = "none";

								break;
							case "3":
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";
								this.btnOldCheck.Style["display"] = "none";
                                this.btnCheckDelete.Style["display"] = "none";
                                this.btnCheckPaymentCheck.Style["display"] = "none";
                                this.btnSubmitAccount.Style["display"] = "none";

                                this.btnModifyEx.Style["display"] = "none";
                                this.btnPayout.Style["display"] = "none";
                                this.btnAccount.Style["display"] = "none";

								break;
							

							default:
								this.btnPayout.Style["display"] = "none";
								this.btnAccount.Style["display"] = "none";
                                this.btnPrint.Style["display"] = "none";
                                this.btnCheckDelete.Style["display"] = "none";
								break;
						}
                        if (!base.user.HasRight("060130"))
                        {
                            this.btnCheckDelete.Style["display"] = "none";
                        }
                        switch (this.up_sPMNameLower)
                        {
                            case "xinchangningpm":
                                this.btnPrint.Style["display"] = "";
                                break;
                            case "tangchenpm":
                                this.btnCheck.Value = "���̲���ύ";
                                break;
                                
                            default:
                                this.btnCheckPaymentCheck.Style["display"] = "none";
                                break;

                        }


						BindPayoutDataGrid();

                        string ud_sProcedureNameAndApplicationCodeList = "''";

                        ud_sProcedureNameAndApplicationCodeList += ",'" + "��ͬ������" + this.txtPaymentCode.Value + "'";
                        ud_sProcedureNameAndApplicationCodeList += ",'" + "�Ǻ�ͬ������" + this.txtPaymentCode.Value + "'";
                        ud_sProcedureNameAndApplicationCodeList += ",'" + "��֧ͬƱ������" + this.txtPaymentCode.Value + "'";
                        this.ucWorkFlowList.ProcedureNameAndApplicationCodeList = ud_sProcedureNameAndApplicationCodeList;
                        this.ucWorkFlowList.DataBound();

					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��������"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "������"));
					return;
				}
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}
        /// <summary>
        /// ��loaddata���ع����� modi by simon
        /// </summary>
        private void LoadData1() 
        {
            string paymentCode = this.txtPaymentCode.Value;

            try
            {
                //				if (( isContract == "" ) || (isContract == "0"))
                //				{
                //					//�Ǻ�ͬ���
                //					this.trContract.Visible = false;
                //				}

                if (paymentCode != "")
                {
                    EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
                    if (entity.HasRecord())
                    {
                        DataRow dr = entity.CurrentRow;

                        //Ȩ��
                        if (entity.GetString("GroupCode") != "")
                        {
                            //�����
                            ArrayList ar = user.GetResourceRight(paymentCode, "Payment");
                            if (!ar.Contains("060101"))
                            {
                                Response.Redirect("../RejectAccess.aspx?OperationCode=060101");
                                Response.End();
                            }

                            this.btnModify.Visible = ar.Contains("060103");
                            this.btnModifyEx.Visible = ar.Contains("060106");
                            this.btnDelete.Visible = ar.Contains("060104");
                            this.btnCheck.Visible = ar.Contains("060108");
                            this.btnOldCheck.Visible = ar.Contains("060105");
                            this.btnAccount.Visible = ar.Contains("060107");
                            this.btnPayout.Visible = base.user.HasRight("060202");
                            this.btnPrint.Visible = ar.Contains("060109");
                        }
                        else
                        {
                            //�����
                            this.btnModify.Visible = base.user.HasRight("060103");
                            this.btnModifyEx.Visible = base.user.HasRight("060106");
                            this.btnDelete.Visible = base.user.HasRight("060104");
                            this.btnCheck.Visible = base.user.HasRight("060108");
                            this.btnOldCheck.Visible = base.user.HasRight("060105");
                            this.btnAccount.Visible = base.user.HasRight("060107");
                            this.btnPayout.Visible = base.user.HasRight("060202");
                            this.btnPrint.Visible = base.user.HasRight("060109");

                        }

                        this.txtProjectCode.Value = entity.GetString("ProjectCode");
                        this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(this.txtProjectCode.Value);
                        this.txtIsContract.Value = entity.GetInt("IsContract").ToString();
                        this.txtContractCode.Value = entity.GetString("ContractCode");

                        if (this.txtContractCode.Value != "")
                        {
                            //��ʾ��ͬ��Ϣ
                            EntityData entityCon = DAL.EntityDAO.ContractDAO.GetContractByCode(this.txtContractCode.Value);
                            //							EntityData entityCon = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.txtContractCode.Value);
                            if (entityCon.HasRecord())
                            {
                                this.lblContractID.Text = entityCon.GetString("ContractID");
                                this.lblContractName.Text = entityCon.GetString("ContractName");
                            }

                            //							//��ͬ������ϸ
                            //							DataTable tbAllo = entityCon.Tables["ContractAllocation"];
                            //							tbAllo.Columns.Add("TotalPaymentMoney", typeof(System.Decimal));
                            //							foreach(DataRow drAllo in tbAllo.Rows) 
                            //							{
                            //								drAllo["TotalPaymentMoney"] = BLL.PaymentRule.GetPaymentMoneyByContractAllocate(BLL.ConvertRule.ToString(drAllo["AllocateCode"]), 0);
                            //							}
                            //
                            //							BindContractAllocationDataGrid(tbAllo);
                            //							BindContractPlanDataGrid(entityCon.Tables["ContractPaymentPlan"]);

                            entityCon.Dispose();
                        }

                        this.lblGroupName.Text = BLL.SystemGroupRule.GetSystemGroupFullName(entity.GetString("GroupCode"));

                        this.lblApplyDate.Text = entity.GetDateTimeOnlyDate("ApplyDate");
                        this.lblApplyPersonName.Text = BLL.SystemRule.GetUserName(entity.GetString("ApplyPerson"));
                        this.lblUnitName.Text = BLL.SystemRule.GetUnitFullName(entity.GetString("UnitCode"));
                        this.lblPayDate.Text = entity.GetDateTimeOnlyDate("PayDate");

                        this.lblPaymentTitle.Text = entity.GetString("PaymentTitle");

                        decimal Money = BLL.ConvertRule.ToDecimal(dr["Money"]);
                        decimal OldMoney = BLL.ConvertRule.ToDecimal(dr["OldMoney"]);
                        this.lblMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(Money), "Ԫ");
                        this.lblOldMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(OldMoney), "Ԫ");

                        this.lblBankName.Text = entity.GetString("BankName");
                        this.lblBankAccount.Text = entity.GetString("BankAccount");

                        switch (this.up_sPMName.ToUpper())
                        {
                            case "TANGCHENPM":
                            case "SHIMAOPM":
                                ViewState["_TypeSortID"] = BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("GroupCode")).Substring(0, 2); ;
                                break;

                            default:
                                ViewState["_TypeSortID"] = "";
                                break;

                        }

                        //�Ƿ��ֹ�����
                        if ((OldMoney != 0) && (OldMoney != Money))
                        {
                            this.spanOldMoney.Style["display"] = "";
                            this.spanOldMoney2.Style["display"] = "";
                            this.dgList.Columns[4].Visible = true;
                        }

                        this.lblPaymentID.Text = entity.GetString("PaymentID");
                        this.txtStatus.Value = entity.GetInt("status").ToString();
                        this.lblRecieptCount.Text = entity.GetInt("RecieptCount").ToString();
                        //						this.txtMoney.Text = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));
                        this.lblPurpose.Text = entity.GetString("Purpose");
                        this.lblSupplyName.Text = entity.GetString("SupplyName");
                        this.lblPayer.Text = entity.GetString("Payer");

                        this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
                        this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName(entity.GetString("CheckPerson"));
                        this.lblRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");

                        this.lblIsContractName.Text = BLL.PaymentRule.GetPaymentIsContractName(entity.GetInt("IsContract"));
                        this.lblStatusName.Text = BLL.PaymentRule.GetPaymentStatusName(entity.GetInt("status"));
                        this.lblPaymentName.Text = entity.GetString("PaymentName");

                        this.lblOpinion.Text = entity.GetString("CheckOpinion");
                        if (this.lblOpinion.Text.Length > 0)
                        {
                            this.tbOpiniont.Visible = true;
                            this.tbOpinionv.Visible = true;
                        }

                        if (this.txtIsContract.Value == "0")
                        {
                            this.trContract.Visible = false;
                            this.trContract2.Visible = false;
                            this.trContract3.Visible = false;
                            this.trContract4.Visible = false;
                            this.trContract5.Visible = false;
                        }
                        else
                        {
                            this.TrPayment.Visible = false;
                        }

                        DataTable tbDtl = BLL.PaymentRule.GeneratePaymentItemTable(entity.Tables["PaymentItem"]);
                        BindDataGrid(tbDtl);

                        //�ɱ�������������Ϣ�ϣ���ʾԤ������� xyq 2006.8.9
                        if (entity.GetString("Payer") == "�ɱ��������")
                        {
                            this.trCostBatchPayment.Style["display"] = "";

                            if (tbDtl.Rows.Count > 0)
                            {
                                string CostBudgetSetCode = BLL.ConvertRule.ToString(tbDtl.Rows[0]["CostBudgetSetCode"]);
                                this.lblCostBudgetSetName.Text = BLL.CostBudgetRule.GetCostBudgetSetName(CostBudgetSetCode);
                            }
                        }

                        switch (this.txtStatus.Value)
                        {
                            case "1"://����
                                this.btnModify.Style["display"] = "none";
                                this.btnDelete.Style["display"] = "none";
                                this.btnCheck.Style["display"] = "none";
                                this.btnOldCheck.Style["display"] = "none";

                                this.btnModifyEx.Style["display"] = "";
                                this.btnPayout.Style["display"] = "";
                                this.btnAccount.Style["display"] = "";
                               

                                break;

                            case "2"://�Ѹ�
                                this.btnModify.Style["display"] = "none";
                                this.btnDelete.Style["display"] = "none";
                                this.btnCheck.Style["display"] = "none";
                                this.btnOldCheck.Style["display"] = "none";

                                this.btnModifyEx.Style["display"] = "";
                                this.btnPayout.Style["display"] = "none";
                                this.btnAccount.Style["display"] = "none";
                                this.btnCheckDelete.Style["display"] = "none";

                                break;
                            case "3":
                                this.btnModify.Style["display"] = "none";
                                this.btnDelete.Style["display"] = "none";
                                this.btnCheck.Style["display"] = "none";
                                this.btnModify.Style["display"] = "none";
                                this.btnDelete.Style["display"] = "none";
                                this.btnCheck.Style["display"] = "none";
                                this.btnOldCheck.Style["display"] = "none";

                                this.btnModifyEx.Style["display"] = "";
                                this.btnPayout.Style["display"] = "";
                                this.btnAccount.Style["display"] = "";
                                this.btnCheckDelete.Style["display"] = "none";

                                break;
                                break;

                            default:
                                this.btnPayout.Style["display"] = "none";
                                this.btnAccount.Style["display"] = "none";
                                this.btnPrint.Style["display"] = "none";
                                this.btnCheckDelete.Style["display"] = "none";
                                break;
                        }
                        if (!base.user.HasRight("060130"))
                        {
                            this.btnCheckDelete.Style["display"] = "none";
                        }
                        switch (this.up_sPMNameLower)
                        {
                            case "xinchangningpm":
                                this.btnPrint.Style["display"] = "";
                                break;

                        }


                        BindPayoutDataGrid();

                        string ud_sProcedureNameAndApplicationCodeList = "''";

                        ud_sProcedureNameAndApplicationCodeList += ",'" + "��ͬ������" + this.txtPaymentCode.Value + "'";
                        ud_sProcedureNameAndApplicationCodeList += ",'" + "�Ǻ�ͬ������" + this.txtPaymentCode.Value + "'";

                        this.ucWorkFlowList.ProcedureNameAndApplicationCodeList = ud_sProcedureNameAndApplicationCodeList;
                        this.ucWorkFlowList.DataBound();

                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "��������"));
                        return;
                    }
                    entity.Dispose();
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "������"));
                    return;
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
            }
        }

		/// <summary>
		/// ��ʾ��ͬ������ϸ
		/// </summary>
		private void BindContractAllocationDataGrid(DataTable tbAllocation) 
		{
			try 
			{
				this.dgContractAllocation.Columns[3].FooterText = BLL.MathRule.SumColumn(tbAllocation, "Money").ToString("N");
				this.dgContractAllocation.Columns[4].FooterText = BLL.MathRule.SumColumn(tbAllocation, "TotalPaymentMoney").ToString("N");
				this.dgContractAllocation.DataSource = tbAllocation;
				this.dgContractAllocation.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��ͬ����ƻ���ϸ
		/// </summary>
		private void BindContractPlanDataGrid(DataTable tbPlan) 
		{
			try 
			{
				this.dgContractPaymentPlan.Columns[4].FooterText = BLL.MathRule.SumColumn(tbPlan, "Money").ToString("N");
				this.dgContractPaymentPlan.DataSource = tbPlan;
				this.dgContractPaymentPlan.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ����ϸ
		/// </summary>
		private void BindDataGrid(DataTable tb) 
		{
			try 
			{
				string[] arrField = {"ItemCash", "OldItemMoney","TotalPayoutCash", "RemainItemCash", "TotalPayoutMoney", "RemainItemMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
				this.dgList.Columns[3].FooterText = arrSum[0].ToString("N");
				this.dgList.Columns[5].FooterText = arrSum[1].ToString("N");
                this.dgList.Columns[8].FooterText = arrSum[2].ToString("N");
                this.dgList.Columns[9].FooterText = arrSum[3].ToString("N");
                this.dgList.Columns[10].FooterText = arrSum[4].ToString("N");
                this.dgList.Columns[11].FooterText = arrSum[5].ToString("N");

				this.dgList.DataSource = tb;
				this.dgList.DataBind();

                this.gvPaymentDetail.DataSource = tb;
                this.gvPaymentDetail.DataBind();

                switch (this.up_sPMNameLower)
                { 
                    case "shimaopm":
                        this.gvPaymentDetail.Visible = false;
                        break;

                    default:
                        this.gvPaymentDetail.Visible = true;
                        break;
                        
                }


			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ�����ϸ
		/// </summary>
		private void BindPayoutDataGrid() 
		{
			try 
			{
				string PaymentCode = this.txtPaymentCode.Value;

				PayoutItemStrategyBuilder sb = new PayoutItemStrategyBuilder("V_PayoutItem");
				sb.AddStrategy( new Strategy( PayoutItemStrategyName.PaymentCode,PaymentCode));
//				sb.AddStrategy( new Strategy( PayoutItemStrategyName.Status,"1"));
				sb.AddOrder( "PayoutDate" ,true);
				sb.AddOrder( "PayoutItemCode" ,true);
				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_PayoutItem",sql );
				qa.Dispose();

				DataTable tb = entity.CurrentTable;
				BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tb, this.txtSubjectSetCode.Value);

				this.dgPayoutItem.Columns[4].FooterText = BLL.MathRule.SumColumn(tb, "PayoutCash").ToString("N");
				this.dgPayoutItem.DataSource = tb;
				this.dgPayoutItem.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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
			this.dgList.ItemDataBound += new DataGridItemEventHandler(this.dgList_ItemDataBound);
            this.gvPaymentDetail.RowDataBound += new GridViewRowEventHandler(this.gvPaymentDetail_DataBound);
		}

        private void gvPaymentDetail_DataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            { 
                case DataControlRowType.DataRow:
                    UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Row.FindControl("ucExchangeRate");
                    DataRowView ud_drvItem = (DataRowView)e.Row.DataItem;

                    ud_ucExchangeRate.Cash = BLL.ConvertRule.ToDecimal(ud_drvItem["ItemCash0"]);
                    ud_ucExchangeRate.ExchangeRate = BLL.ConvertRule.ToDecimal(ud_drvItem["ExchangeRate"]);
                    ud_ucExchangeRate.MoneyType = ud_drvItem["MoneyType"].ToString();
                    ud_ucExchangeRate.IsShowTitle = false;
                    ud_ucExchangeRate.EditMode = false;
                    ud_ucExchangeRate.BindControl();
                    break;

            }

        }
		private void dgList_ItemDataBound(object sender,DataGridItemEventArgs e)
		{
			switch ( e.Item.ItemType )
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					DataRowView	ud_drvItem = (DataRowView)e.Item.DataItem;
					break;
			}

		}

		private void GoBack() 
		{
			
			string url = "";
			if (this.txtFromUrl.Value == "")
			{
				url = "PaymentList.aspx?ProjectCode=" + this.txtProjectCode.Value;
			}
			else
			{
				url = this.txtFromUrl.Value;
			}
			Response.Redirect(url);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
                if (BLL.PaymentRule.GetPayoutMoneyByPayment(this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "���и����¼��������������¼" ));
                    return;
                }
                if( BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("�Ǻ�ͬ������", this.txtPaymentCode.Value)>0){
                    Response.Write(Rms.Web.JavaScript.Alert(true, "�����ڰ����̣������������"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ������", this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "�����ڰ����̣������������")); 
                    return;
                }
                BLL.PaymentRule.DeletePayment(this.txtPaymentCode.Value);
                LogHelper.WriteLog("��ɾ��:" +this.txtPaymentCode.Value+" UserCode"+ ((User)Session["User"]).UserCode +" "+Request.UserHostAddress );

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��������" + ex.Message));
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAccount_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.PaymentRule.CheckPaymentAccount(this.txtPaymentCode.Value, base.user.UserCode);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���������" + ex.Message));
			}
		}

        /// <summary>
        /// ��˺�ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckDelete_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (BLL.PaymentRule.GetPayoutMoneyByPayment(this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "���и����¼��������������¼"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("�Ǻ�ͬ������", this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "�����ڰ����̣������������"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("��ͬ������", this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "�����ڰ����̣������������"));
                    return;
                }
                BLL.PaymentRule.DeletePayment(this.txtPaymentCode.Value);
                LogHelper.WriteLog("��ɾ��:" + this.txtPaymentCode.Value + " UserCode" + ((User)Session["User"]).UserCode + " " + Request.UserHostAddress);

                GoBack();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��������" + ex.Message));
            }
        }

	}
}
