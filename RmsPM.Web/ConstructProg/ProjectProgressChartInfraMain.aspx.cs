using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;
using Infragistics.WebUI.UltraWebChart;
using Infragistics.UltraChart.Core;
using Infragistics.UltraChart.Core.Layers;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// ProjectProgressChartInfraMain
	/// </summary>
	public partial class ProjectProgressChartInfraMain : System.Web.UI.Page
	{

//		protected decimal MonthPixel = 50;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadChart();
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void IniPage() 
		{
			try
			{
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];	
				this.txtGanttType.Value = Request.QueryString["GanttType"];	

				//��1�ν���ʱ��ս��ȱ�
				Session["dsGantt"] = null;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
			}
		}

		private void LoadChart() 
		{
			try 
			{
				/*
				string WBSCode = this.txtWBSCode.Value;
				Session["dsGantt"] = null;

				if (WBSCode != "") 
				{
					DataSet ds = BLL.ConstructProgRule.GetProjectProgressChartDataTable(WBSCode);
					Session["dsGantt"] = ds;

					int RowCount = ds.Tables[0].Rows.Count;
					int RowHeight = BLL.ConvertRule.ToInt(this.txtChartRowHeight.Value);
					int TopHeight = BLL.ConvertRule.ToInt(this.txtChartTopHeight.Value);
					int BottomHeight = BLL.ConvertRule.ToInt(this.txtChartBottomHeight.Value);

					this.txtChartHeight.Value = BLL.ConstructProgRule.GetGanttChartHeight(RowCount, RowHeight, TopHeight, BottomHeight).ToString();
					this.txtChartDataHeight.Value = BLL.ConstructProgRule.GetGanttDataHeight(RowCount, RowHeight).ToString();
				}
				*/
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾͼ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ��ʧ�ܣ�" + ex.Message));
			}
		}

	}
}
