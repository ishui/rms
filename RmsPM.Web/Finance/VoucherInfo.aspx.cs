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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// VoucherInfo 的摘要说明。
	/// </summary>
	public partial class VoucherInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
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
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtVoucherCode.Value = Request.QueryString["VoucherCode"];
				this.txtOpen.Value = Request.QueryString["Open"];

				if (this.txtOpen.Value != "") 
				{
					this.tdBack.Style["display"] = "none";
					this.tdClose.Style["display"] = "";
				}

				//权限
				this.btnModify.Visible = base.user.HasRight("060303");
				this.btnModifyEx.Visible = base.user.HasRight("060307");
				this.btnDelete.Visible = base.user.HasRight("060304");
				this.btnCheck.Visible = base.user.HasRight("060305");
				this.btnDownload.Visible = base.user.HasRight("060306");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

        public string SubjectSetName
        {
            get { return this.txtSubjectSetName.Value; }
        }

		private void LoadData()
		{
			string voucherCode = this.txtVoucherCode.Value;

			try
			{
				EntityData voucher = DAL.EntityDAO.PaymentDAO.GetV_VoucherByCode(voucherCode);
				if ( voucher.HasRecord())
				{
					txtProjectCode.Value = voucher.GetString("ProjectCode");
					this.lblVoucherID.Text = voucher.GetString("VoucherID");
					this.lblVoucherType.Text = BLL.VoucherRule.GetVoucherTypeName(voucher.GetString("VoucherType"));
					this.lblMakeDate.Text = voucher.GetDateTimeOnlyDate("MakeDate");
					this.lblAccountantName.Text = voucher.GetString("AccountantName");
					this.lblCheckDate.Text = voucher.GetDateTimeOnlyDate("CheckDate");
					this.lblCheckPersonName.Text = voucher.GetString("CheckPersonName");
					this.txtStatus.Value = voucher.GetInt("Status").ToString();
					this.lblStatusName.Text = voucher.GetString("StatusName");
					this.lblReceiptCount.Text = voucher.GetInt("ReceiptCount").ToString();
                    this.lblOutPutDate.Text = voucher.GetDateTime("OutPutDate");

                    this.txtSubjectSetCode.Value = voucher.GetString("SubjectSetCode");
                    this.txtSubjectSetName.Value = BLL.SubjectRule.GetSubjectSetName(this.txtSubjectSetCode.Value);

					switch (this.txtStatus.Value)
					{
						case "0":
							//待审
							this.btnDownload.Style["display"]= "none";
							break;

						case "1":
							//已审
							this.btnModify.Style["display"] = "none";
//							this.btnDelete.Style["display"] = "none";
							this.btnCheck.Style["display"] = "none";

							this.btnModifyEx.Style["display"] = "";

							break;

						case "2":
							//已导出
							goto case "1";

						default:
							break;
					}
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "凭证不存在"));
					return;
				}

				EntityData entityDtl = DAL.EntityDAO.PaymentDAO.GetV_VoucherDetailByVoucherCode(voucherCode);
				DataTable dt = entityDtl.CurrentTable;

				//列表显示名称
//				BLL.PaymentRule.VoucherDetailAddColumnSuplName(dt, txtProjectCode.Value);
//				BLL.PaymentRule.VoucherDetailAddColumnCustName(dt);
//				BLL.PaymentRule.VoucherDetailAddColumnUFUnitName(dt);
//				BLL.PaymentRule.VoucherDetailAddColumnUFProjectName(dt);
				BLL.PaymentRule.VoucherDetailAddColumnSubjectName(dt, txtSubjectSetCode.Value);

				string[] arrField = {"DebitMoney", "CrebitMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(dt, arrField);

				this.dgList.Columns[3].FooterText = arrSum[0].ToString("N");
				this.dgList.Columns[4].FooterText = arrSum[1].ToString("N");
				this.dgList.DataSource = dt;
				this.dgList.DataBind();

				entityDtl.Dispose();
				voucher.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//借方、贷方用不同颜色区分
			if ((e.Item.ItemType == ListItemType.Item) 
				|| (e.Item.ItemType == ListItemType.AlternatingItem ))
			{
				Label lblCrebitMoney = (Label)e.Item.FindControl("lblCrebitMoney");
				decimal val = BLL.ConvertRule.ToDecimal(lblCrebitMoney.Text.Trim());
				if (val == 0)
				{
					e.Item.CssClass = "ItemGridTr1";
				}
				else 
				{
					e.Item.CssClass = "ItemGridTr2";
				}
			}
		}

		/*
		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDownload_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string SaveFileNameHttp = BLL.VoucherRule.MakeVoucherFile(this.txtVoucherCode.Value, Server);
				Response.Write(Rms.Web.JavaScript.WinOpen(true, SaveFileNameHttp,"","","","","",true,true,false,true,true,true,false,false));

			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "导出失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			LoadData();
		}
*/

		private void GoBack() 
		{
			string url = "";
			if (this.txtFromUrl.Value == "")
			{
				url = "VoucherList.aspx?ProjectCode=" + this.txtProjectCode.Value;
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
				BLL.PaymentRule.DeleteVoucher(this.txtVoucherCode.Value);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除凭证出错：" + ex.Message));
				return;
			}

			GoBack();
		}
	}
}
