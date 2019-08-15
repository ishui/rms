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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
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

				//第1次进入时清空进度表
				Session["dsGantt"] = null;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
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
				ApplicationLog.WriteLog(this.ToString(),ex,"显示图表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表失败：" + ex.Message));
			}
		}

	}
}
