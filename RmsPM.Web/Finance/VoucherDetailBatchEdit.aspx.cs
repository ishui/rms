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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// VoucherDetailBatchEdit 的摘要说明。
	/// </summary>
	public partial class VoucherDetailBatchEdit : PageBase
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
			this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
			BLL.PageFacade.LoadDictionarySelect(this.sltPaymentType,"付款类型","");

            this.ucInputSubjectJ.SubjectSetCode = this.txtSubjectSetCode.Value;
            this.ucInputSubjectD.SubjectSetCode = this.txtSubjectSetCode.Value;
		}

		private void LoadData()
		{

//			this.txtDetailVoucherCode.Value = Request["VoucherDetailCode"] + "" ;
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

		private bool CheckSubject(string SubjectCode) 
		{
			string hint = "";

			if (SubjectCode.Length > 0) 
			{
				hint = BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("科目编号“{0}”", SubjectCode));
				if (hint != "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,hint));
					return false;
				}
			}

			return true;
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			string SubjectCodeJ = this.ucInputSubjectJ.Value;
			string SubjectNameJ = this.ucInputSubjectJ.Text;
			string SubjectCodeD = this.ucInputSubjectD.Value;
			string SubjectNameD = this.ucInputSubjectD.Text;
			string PaymentType = this.sltPaymentType.Value.Trim();
			string summary = this.txtSummary.Value.Trim();
			string remark = this.txtRemark.Value.Trim();
			string BillNo = this.txtBillNo.Value.Trim();
			string SuplCode = this.txtSuplCode.Value.Trim();
			string SuplName = this.txtSuplName.Value.Trim();
			string CustCode = this.txtCustCode.Value.Trim();
			string CustName = this.txtCustName.Value.Trim();
			string UFUnitCode = this.txtUFUnitCode.Value.Trim();
			string UFUnitName = this.txtUFUnitName.Value.Trim();
			string UFProjectCode = this.txtUFProjectCode.Value.Trim();
			string UFProjectName = this.txtUFProjectName.Value.Trim();

			if (!CheckSubject(SubjectCodeJ))
				return;

			if (!CheckSubject(SubjectCodeD))
				return;

			string codes = this.txtDetailVoucherCode.Value.Trim();
			string[] arrcode = codes.Split(",".ToCharArray());

			DataTable dt = (DataTable)Session["VoucherDetailTable"];
			foreach(string code in arrcode) 
			{
				DataRow dr = dt.Select( String.Format( "VoucherDetailCode='{0}'", code ) )[0];

				if (this.chkSubjectCodeJ.Checked) 
				{
					if ((dr["DebitMoney"] != DBNull.Value) && (decimal.Parse(dr["DebitMoney"].ToString()) != 0)) 
					{
						dr["SubjectCode"] = SubjectCodeJ;
						dr["SubjectName"] = SubjectNameJ;
						dr["SubjectHint"] = "";
					}
				}

				if (this.chkSubjectCodeD.Checked) 
				{
					if ((dr["CrebitMoney"] != DBNull.Value) && (decimal.Parse(dr["CrebitMoney"].ToString()) != 0)) 
					{
						dr["SubjectCode"] = SubjectCodeD;
						dr["SubjectName"] = SubjectNameD;
						dr["SubjectHint"] = "";
					}
				}

				if (this.chkSummary.Checked) 
				{
					dr["Summary"] = summary;
				}

				if (this.chkRemark.Checked) 
				{
					dr["Remark"] = remark;
				}

				if (this.chkBillNo.Checked) 
				{
					dr["BillNo"] = BillNo;
				}

				if (this.chkPaymentType.Checked) 
				{
					dr["PaymentType"] = PaymentType;
				}

				if (this.chkSupl.Checked) 
				{
					dr["SupplyCode"] = SuplCode;
					dr["SupplyName"] = SuplName;
				}

				if (this.chkCust.Checked) 
				{
					dr["CustCode"] = CustCode;
					dr["CustName"] = CustName;
				}

				if (this.chkUFUnit.Checked) 
				{
					dr["UFUnitCode"] = UFUnitCode;
					dr["UFUnitName"] = UFUnitName;
				}

				if (this.chkUFProject.Checked) 
				{
					dr["UFProjectCode"] = UFProjectCode;
					dr["UFProjectName"] = UFProjectName;
				}
			}

			Session["VoucherDetailTable"] = dt;

			CloseWindow();

		}

		private void CloseWindow()
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write( "window.opener.ReloadDataGrid();" );
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
