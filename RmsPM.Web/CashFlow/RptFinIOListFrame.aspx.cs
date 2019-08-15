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
	/// RptFinIOListFrame ��ժҪ˵����
	/// </summary>
	public partial class RptFinIOListFrame : PageBase
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtProjectName.Value = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
//				this.divProjectName.InnerText = this.txtProjectName.Value;

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

		/// <summary>
		/// ��ͷ
		/// </summary>
		/// <param name="Sheet"></param>
		/// <param name="RowIndex"></param>
		/// <param name="GroupFieldValue"></param>
		protected void MyProcessGroupHeader(Excel.Worksheet Sheet, int RowIndex, string GroupFieldValue, DataRow drGroup)
		{
			TExcel.SetCellValue(Sheet, RowIndex, 1, BLL.ProjectRule.GetProjectName(GroupFieldValue));
		}

		/// <summary>
		/// ��β
		/// </summary>
		/// <param name="Sheet"></param>
		/// <param name="RowIndex"></param>
		/// <param name="GroupFieldValue"></param>
		protected void MyProcessGroupFooter(Excel.Worksheet Sheet, int RowIndex, string GroupFieldValue, DataRow drGroup)
		{
		}

		public void btnExcel_ServerClick(object sender, System.EventArgs e)
		{
		}

	}
}
