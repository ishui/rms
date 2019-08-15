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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PayoutInfo 的摘要说明。
	/// </summary>
	public partial class PayoutInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefViewVoucher;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddDtl;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddFromCost;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPayout;
		protected System.Web.UI.WebControls.Label lblSubjectCode;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
                
				string payoutCode = Request.QueryString["PayoutCode"] + "" ;
				ArrayList ar = user.GetResourceRight(payoutCode,"Payout");
				if ( ! ar.Contains("060201"))
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}

				IniPage();
				LoadData();

				if ( !ar.Contains("060203"))
					this.btnModify.Visible = false;

				if ( !ar.Contains("060206"))
					this.btnModifyEx.Visible = false;

				if ( !ar.Contains("060204"))
					this.btnDelete.Visible = false;

				if ( !ar.Contains("060205"))
					this.btnOldCheck.Visible = false;

                if ( !ar.Contains("060207"))
                    this.btnCheck.Visible = false;
                if ( !ar.Contains("060230"))
                    this.btnCheckDelete.Visible = false;


				this.btnBuildVoucher.Visible = user.HasRight("060302");
				this.btnSelectVoucher.Visible = user.HasRight("060302");

                if (BLL.WorkFlowRule.GetCaseCountByProcedureNameAndApplicationCode("付款审核", payoutCode) > 0)
                {
                    this.btnCheck.Visible = false;
                }

			}
		}

		private void IniPage()
		{
			try
			{
				this.txtPayoutCode.Value = Request.QueryString["PayoutCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

                string ud_sAuditingName = "付款审核";//System.Configuration.ConfigurationManager.AppSettings["PayoutAuditingName"].ToString();
                ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByName(ud_sAuditingName);


			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}


		private void LoadData()
		{
			string PayoutCode = this.txtPayoutCode.Value;
			string projectCode = "";

			try
			{
				if ( PayoutCode != "")
				{


					EntityData entity = DAL.EntityDAO.PaymentDAO.GetV_PayoutByCode(PayoutCode);
					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;
						projectCode = entity.GetString("ProjectCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtSubjectSetCode.Value = entity.GetString("SubjectSetCode");
						

						this.lblInputPersonName.Text = entity.GetString("InputPersonName");
						this.lblGroupCodeName.Text = BLL.SystemGroupRule.GetSystemGroupFullName(entity.GetString("GroupCode"));
						//						this.lblInputPersonName.Text = BLL.SystemRule.GetUserName( entity.GetString("InputPerson"));
						this.lblPayoutDate.Text = entity.GetDateTimeOnlyDate("PayoutDate");
//						this.lblMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["Money"]), "元");
                        this.ucExchangeRate.Cash = entity.GetDecimal("Cash");
                        this.ucExchangeRate.MoneyType = entity.GetString("MoneyType");
                        this.ucExchangeRate.ExchangeRate = entity.GetDecimal("ExchangeRate");
                        this.ucExchangeRate.Mode = FormViewMode.ReadOnly;
                        this.ucExchangeRate.DataBind();


						this.lblPayoutID.Text = entity.GetString("PayoutID");
						this.txtStatus.Value = entity.GetInt("status").ToString();
						this.lblStatusName.Text = entity.GetString("StatusName");

						this.lblVoucherID.InnerHtml = BLL.PaymentRule.GetVoucherName(entity.GetString("VoucherCode"));
						this.lblVoucherID.Attributes["val"] = entity.GetString("VoucherCode");

						//						this.txtMoney.Text = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));
						this.lblSupplyName.Text = entity.GetString("SupplyName");
						this.lblPayer.Text = entity.GetString("Payer");

						this.lblPaymentType.Text = entity.GetString("PaymentType");
						this.lblBillNo.Text = entity.GetString("BillNo");
						this.lblInvoNo.Text = entity.GetString("InvoNo");
						this.lblReceiptCount.Text = entity.GetInt("ReceiptCount").ToString();

						this.lblSubjectName.Text = BLL.SubjectRule.GetSubjectFullName(entity.GetString("SubjectCode"), this.txtSubjectSetCode.Value);
                        this.lblSubjectName.Attributes["title"] = BLL.SubjectRule.GetSubjectSetName(entity.GetString("SubjectSetCode"));

						this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPersonName.Text = entity.GetString("CheckPersonName");
						//						this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName( entity.GetString("CheckPerson"));
						this.lblCheckOpinion.Text = entity.GetString("CheckOpinion").Replace("\n", "<br>");

						//						this.lblStatusName.Text = BLL.PaymentRule.GetPayoutStatusName(entity.GetInt("status"));

						if ( entity.GetInt("IsApportioned") == 1 )
							this.lblApportion.Text = "已分摊";
						DataTable tbDtl = BLL.PaymentRule.GeneratePayoutItemTable(PayoutCode);
						BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tbDtl, this.txtSubjectSetCode.Value);
						BindDataGrid(tbDtl);

						switch (this.txtStatus.Value)
						{
							case "1"://已审
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";
                                this.btnOldCheck.Style["display"] = "none";
                                this.btnCheckDelete.Style["display"] = "";
								this.btnModifyEx.Style["display"] = "";

								if (this.lblVoucherID.InnerText == "") 
								{
									this.btnBuildVoucher.Style["display"] = "";
									this.btnSelectVoucher.Style["display"] = "";
								}

								break;
                            case "2"://审核中
                                this.btnModify.Style["display"] = "none";
                                this.btnDelete.Style["display"] = "none";
                                this.btnCheck.Style["display"] = "none";
                                this.btnOldCheck.Style["display"] = "none";
                                this.btnCheckDelete.Style["display"] = "none";

                                break;

							default:
								break;
						}


					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "付款单不存在"));
						return;
					}
					entity.Dispose();

                    DataTable dtApportion = BLL.CostRule.ApportionOnePayout(projectCode, PayoutCode, BLL.CostRule.GetApportionAreaField(this.txtProjectCode.Value));
                    BindApportion(dtApportion);
					dtApportion.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无付款单号"));
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
		/// 显示付款单明细
		/// </summary>
		private void BindDataGrid(DataTable tb) 
		{
			try 
			{
                string[] arrField = { "ItemCash", "TotalPayoutCash", "RemainItemCash","PayoutCash","PayoutMoney" };
                decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
                this.gvPayoutItem.Columns[3].FooterText = arrSum[0].ToString("N");
                this.gvPayoutItem.Columns[4].FooterText = arrSum[1].ToString("N");
                this.gvPayoutItem.Columns[5].FooterText = arrSum[2].ToString("N");
                this.gvPayoutItem.Columns[6].FooterText = arrSum[3].ToString("N");
                this.gvPayoutItem.Columns[8].FooterText = arrSum[4].ToString("N");

                this.gvPayoutItem.DataSource = tb;
                this.gvPayoutItem.DataBind();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示付款明细出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示分摊明细
		/// </summary>
		private void BindApportion(DataTable tb) 
		{
			try 
			{
				string[] arrField = {"ApportionMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
				this.dgGridApportion.Columns[3].FooterText = arrSum[0].ToString("N");

				this.dgGridApportion.DataSource = tb;
				this.dgGridApportion.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示分摊明细出错：" + ex.Message));
			}
		}

		private void GoBack() 
		{
			string url = "";
			if (this.txtFromUrl.Value == "")
			{
				url = "PayoutList.aspx?ProjectCode=" + this.txtProjectCode.Value;
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
				BLL.PaymentRule.DeletePayout(this.txtPayoutCode.Value);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除付款单出错：" + ex.Message));
				return;
			}

			GoBack();
		}

        protected void btnCheckDelete_ServerClick(object sender, EventArgs e)
        {
            try
            {

				QueryAgent qa = new Rms.ORMap.QueryAgent();
                DataSet ds = qa.ExecSqlForDataSet(string.Format("select distinct i.PaymentCode from PayoutItem a, PaymentItem i where a.PayoutCode = '{0}' and a.PaymentItemCode = i.PaymentItemCode", this.txtPayoutCode.Value));
				DataTable tb = ds.Tables[0];
                qa.Dispose();

                BLL.PaymentRule.DeletePayout(this.txtPayoutCode.Value);

                //付款单审核后删除后改变请款单状态
                BLL.PaymentRule.UpdatePaymentStatusByPayout(tb, base.user.UserCode);

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "删除审核后付款单出错：" + ex.Message));
                return;
            }

            GoBack();
        }

        protected void gvPayoutItem_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    //DataRowView ud_drvItem = (DataRowView)e.Row.DataItem;

                    //Label ud_lblPayoutCashMoney = (Label)e.Row.FindControl("lblPayoutCashMoney");

                    //string ud_sPaymentMoneyType = ud_drvItem["PaymentMoneyType"] == DBNull.Value ? string.Empty : ud_drvItem["PaymentMoneyType"].ToString();
                    //string ud_sPayoutMoneyType = ud_drvItem["PayoutMoneyType"] == DBNull.Value ? ud_sPaymentMoneyType : ud_drvItem["PayoutMoneyType"].ToString();

                    break;
            }
        }

	}
}
