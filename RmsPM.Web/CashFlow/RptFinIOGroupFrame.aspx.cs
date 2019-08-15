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
using Rms.Web;
using RmsPM.DAL;
using RmsReport;

namespace RmsPM.Web.CashFlow
{
	/// <summary>
	/// RptFinIOGroupFrame ��ժҪ˵����
	/// </summary>
	public partial class RptFinIOGroupFrame : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tableHint;
		protected System.Web.UI.HtmlControls.HtmlSelect sltReportType;
		protected System.Web.UI.HtmlControls.HtmlSelect sltChartType;
		protected System.Web.UI.HtmlControls.HtmlSelect sltIsSum;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try 
			{
//				this.dtEnd.Value = DateTime.Now.ToString("yyyy-MM-dd");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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

	}
}
