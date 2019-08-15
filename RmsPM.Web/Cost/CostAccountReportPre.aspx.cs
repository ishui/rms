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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// CostAccountReportPre ��ժҪ˵����
	/// </summary>
	public partial class CostAccountReportPre : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			this.dtbEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
			this.dtbReportDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
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

		protected void btnReport_ServerClick(object sender, System.EventArgs e)
		{
			string sEndDate = this.dtbEndDate.Value;
			string projectCode = Request["ProjectCode"] + "";
			Response.Write(Rms.Web.JavaScript.WinOpenMax(true,@"CostAccountReport.aspx?ProjectCode="+projectCode+"&EndDate=" + sEndDate + "&ReportDate=" + this.dtbReportDate.Value + "&UnitText=" + this.txtUnit.Value ,"�ɱ����öԱȱ�"));
			Response.End();
		}
	}
}
