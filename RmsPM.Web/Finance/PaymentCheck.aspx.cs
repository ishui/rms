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
	/// PaymentCheck 的摘要说明。
	/// </summary>
	public partial class PaymentCheck : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try
			{
				this.txtPaymentCode.Value = Request.QueryString["PaymentCode"];
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
				if ( paymentCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无请款单号"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
				if ( entity.HasRecord())
				{
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtIsContract.Value = entity.GetInt("IsContract").ToString();
					this.txtContractCode.Value = entity.GetString("ContractCode");
					this.txtStatus.Value = entity.GetInt("Status").ToString();

                    //成本批量请款审核后，自动生成付款单 xyq 2018.8.2
                    if (entity.GetString("Payer") == "成本批量请款")
                    {
                        this.txtAutoCreatePayout.Value = "1";
                        this.trAutoCreatePayout.Visible = true;
                    }
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请款单不存在"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				ShowCheckResult(entity);

				entity.Dispose();
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
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

		private int ShowCheckResult(EntityData entity) 
		{
			int bResult = -1;

			try 
			{
				string PaymentCode = this.txtPaymentCode.Value;
				DataTable tbPayment = entity.Tables["Payment"];
				DataTable tbPaymentItem = entity.Tables["PaymentItem"];

				DataTable tbResult = BLL.PaymentRule.CreatePaymentCheckResultTable();
/* 修改合同费用检查功能，检查所有请款是否超出费用，因为代码复杂，取消ｔｂｒｅｓｕｌｔ这个结果　*/
				if (this.txtContractCode.Value != "") 
				{
                    //检查明细金额不能超出合同各费用项金额
                    string Hint = BLL.PaymentRule.CheckPaymentCostLimit("", this.txtContractCode.Value, null,ref tbResult);
                    if (Hint.Length > 0)
                    {
                        bResult = -1;
                    }
				} 
                
				if (tbResult.Rows.Count == 0) 
				{
					this.lblResultOk.Visible = true;
					this.trErr.Style["display"] = "none";
					bResult = 1;
				}
				else 
				{
					DataView dvWarn = new DataView(tbResult, "ErrLevel=0","",DataViewRowState.CurrentRows);
					DataView dvErr = new DataView(tbResult, "ErrLevel=1","",DataViewRowState.CurrentRows);

					if (dvErr.Count > 0) 
					{
						this.lblResultErr.Visible = true;
						this.divErr.Style["display"] = "block";
						this.dgList.DataSource = dvErr;
						this.dgList.DataBind();
					}

					if (dvWarn.Count > 0) 
					{
						if (dvErr.Count == 0) 
						{
							bResult = 0;
							this.lblResultWarn.Visible = true;
						}
						this.divWarn.Style["display"] = "block";
						this.dgWarn.DataSource = dvWarn;
						this.dgWarn.DataBind();
					}

				}

				if (bResult >= 0) 
				{
					this.trOpinion.Style["display"] = "";
					this.btnSave.Style["display"] = "";
				}

				// 2005.06.9 unm 洪泉需求,用户可以添加意见,并且在请款列表后显示
				this.trOpinion.Style["display"] = "";

			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
			}

			return bResult;
		}

		/// <summary>
		/// 审核
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//审核时再校验一遍
				EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(this.txtPaymentCode.Value);
				int bResult = ShowCheckResult(entity);
				entity.Dispose();

				switch (bResult)
				{
					case -1:
						return;
				}

				string PaymentCode = this.txtPaymentCode.Value;
				BLL.PaymentRule.CheckPayment(PaymentCode, this.txtCheckOpinion.Value, base.user.UserCode);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "审核失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

            //成本批量请款审核后，自动生成付款单 
            if (this.txtAutoCreatePayout.Value == "1")
            {
                try
                {
                    BLL.PaymentRule.AutoCreatePayoutFromPayment(this.txtPaymentCode.Value, user.UserCode);
                }
                catch (Exception ex)
                {
                    Response.Write(JavaScript.Alert(true, "自动生成付款单失败：" + ex.Message));
                    Response.Write(JavaScript.WinClose(true));
                    ApplicationLog.WriteLog(this.ToString(), ex, "");
                }
            }
            
            GoBack();
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
