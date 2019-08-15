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
	/// PaymentInfo 的摘要说明。
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
            //审批流程url
            string ProcedureCode = "";
            if (txtIsContract.Value == "0")
            {


                ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("非合同请款审核", projectCode);

                ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);
                ud_sProcedureName = "非合同请款审核";
            }
            else
            {
                ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("合同请款审核", projectCode);
                ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);
                ud_sProcedureName = "合同请款审核";
            }
            ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("用费报销单审核", projectCode);
            ViewState["_AuditingAccountURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);


            if (BLL.WorkFlowRule.GetCaseCountByProcedureNameAndApplicationCode(ud_sProcedureName, this.txtPaymentCode.Value) > 0)
            {
                this.btnCheck.Visible = false;
                this.btnCheckPaymentCheck.Visible = false;
                this.btnSubmitAccount.Visible = false;
            }

            ProcedureCode = "";
            ProcedureCode = BLL.WorkFlowRule.GetProcedureCodeByName("合同支票请款审核", projectCode);
            ViewState["_CheckAuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByCode(ProcedureCode);
           
            if (BLL.WorkFlowRule.GetCaseCountByProcedureNameAndApplicationCode("合同支票请款审核", this.txtPaymentCode.Value) > 0)
            {
                this.btnCheck.Visible = false;
                this.btnCheckPaymentCheck.Visible = false;
                this.btnSubmitAccount.Visible = false;
            }


            //审批表打印URL
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



				//权限
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}


		private void LoadData()
		{
			string paymentCode = this.txtPaymentCode.Value;

			try
			{
//				if (( isContract == "" ) || (isContract == "0"))
//				{
//					//非合同请款
//					this.trContract.Visible = false;
//				}

				if ( paymentCode != "")
				{
					EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						//权限
						if (entity.GetString("GroupCode") != "") 
						{
							//有类别
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
							//无类别
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
							//显示合同信息
							EntityData entityCon = DAL.EntityDAO.ContractDAO.GetContractByCode(this.txtContractCode.Value);
							//							EntityData entityCon = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.txtContractCode.Value);
							if (entityCon.HasRecord()) 
							{
								this.lblContractID.Text = entityCon.GetString("ContractID");
								this.lblContractName.Text = entityCon.GetString("ContractName");
							}

							//							//合同费用明细
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
						this.lblMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(Money), "元");
						this.lblOldMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(OldMoney), "元");

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

						//是否手工结算
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

                        //成本批量请款的请款单信息上，显示预算表名称 xyq 2006.8.9
                        if (entity.GetString("Payer") == "成本批量请款")
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
							case "1"://已审
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

							case "2"://已付
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
                                this.btnCheck.Value = "工程拨款单提交";
                                break;
                                
                            default:
                                this.btnCheckPaymentCheck.Style["display"] = "none";
                                break;

                        }


						BindPayoutDataGrid();

                        string ud_sProcedureNameAndApplicationCodeList = "''";

                        ud_sProcedureNameAndApplicationCodeList += ",'" + "合同请款审核" + this.txtPaymentCode.Value + "'";
                        ud_sProcedureNameAndApplicationCodeList += ",'" + "非合同请款审核" + this.txtPaymentCode.Value + "'";
                        ud_sProcedureNameAndApplicationCodeList += ",'" + "合同支票请款审核" + this.txtPaymentCode.Value + "'";
                        this.ucWorkFlowList.ProcedureNameAndApplicationCodeList = ud_sProcedureNameAndApplicationCodeList;
                        this.ucWorkFlowList.DataBound();

					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "请款单不存在"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无请款单号"));
					return;
				}
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}
        /// <summary>
        /// 对loaddata的重构尝试 modi by simon
        /// </summary>
        private void LoadData1() 
        {
            string paymentCode = this.txtPaymentCode.Value;

            try
            {
                //				if (( isContract == "" ) || (isContract == "0"))
                //				{
                //					//非合同请款
                //					this.trContract.Visible = false;
                //				}

                if (paymentCode != "")
                {
                    EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
                    if (entity.HasRecord())
                    {
                        DataRow dr = entity.CurrentRow;

                        //权限
                        if (entity.GetString("GroupCode") != "")
                        {
                            //有类别
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
                            //无类别
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
                            //显示合同信息
                            EntityData entityCon = DAL.EntityDAO.ContractDAO.GetContractByCode(this.txtContractCode.Value);
                            //							EntityData entityCon = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.txtContractCode.Value);
                            if (entityCon.HasRecord())
                            {
                                this.lblContractID.Text = entityCon.GetString("ContractID");
                                this.lblContractName.Text = entityCon.GetString("ContractName");
                            }

                            //							//合同费用明细
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
                        this.lblMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(Money), "元");
                        this.lblOldMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(OldMoney), "元");

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

                        //是否手工结算
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

                        //成本批量请款的请款单信息上，显示预算表名称 xyq 2006.8.9
                        if (entity.GetString("Payer") == "成本批量请款")
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
                            case "1"://已审
                                this.btnModify.Style["display"] = "none";
                                this.btnDelete.Style["display"] = "none";
                                this.btnCheck.Style["display"] = "none";
                                this.btnOldCheck.Style["display"] = "none";

                                this.btnModifyEx.Style["display"] = "";
                                this.btnPayout.Style["display"] = "";
                                this.btnAccount.Style["display"] = "";
                               

                                break;

                            case "2"://已付
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

                        ud_sProcedureNameAndApplicationCodeList += ",'" + "合同请款审核" + this.txtPaymentCode.Value + "'";
                        ud_sProcedureNameAndApplicationCodeList += ",'" + "非合同请款审核" + this.txtPaymentCode.Value + "'";

                        this.ucWorkFlowList.ProcedureNameAndApplicationCodeList = ud_sProcedureNameAndApplicationCodeList;
                        this.ucWorkFlowList.DataBound();

                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "请款单不存在"));
                        return;
                    }
                    entity.Dispose();
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "无请款单号"));
                    return;
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
            }
        }

		/// <summary>
		/// 显示合同费用明细
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示合同付款计划明细
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示请款单明细
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示付款单明细
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}


		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请款单有付款记录，请先清除付款记录" ));
                    return;
                }
                if( BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("非合同请款审核", this.txtPaymentCode.Value)>0){
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请款单有在办流程，请先完成流程"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("合同请款审核", this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请款单有在办流程，请先完成流程")); 
                    return;
                }
                BLL.PaymentRule.DeletePayment(this.txtPaymentCode.Value);
                LogHelper.WriteLog("请款单删除:" +this.txtPaymentCode.Value+" UserCode"+ ((User)Session["User"]).UserCode +" "+Request.UserHostAddress );

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除请款单出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 结算
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "请款单结算出错：" + ex.Message));
			}
		}

        /// <summary>
        /// 审核后删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckDelete_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (BLL.PaymentRule.GetPayoutMoneyByPayment(this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请款单有付款记录，请先清除付款记录"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("非合同请款审核", this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请款单有在办流程，请先完成流程"));
                    return;
                }
                if (BLL.WorkFlowRule.GetBeginCaseCountByProcedureNameAndApplicationCode("合同请款审核", this.txtPaymentCode.Value) > 0)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "请款单有在办流程，请先完成流程"));
                    return;
                }
                BLL.PaymentRule.DeletePayment(this.txtPaymentCode.Value);
                LogHelper.WriteLog("请款单删除:" + this.txtPaymentCode.Value + " UserCode" + ((User)Session["User"]).UserCode + " " + Request.UserHostAddress);

                GoBack();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "删除请款单出错：" + ex.Message));
            }
        }

	}
}
