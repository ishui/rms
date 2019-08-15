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
	/// ReportCostByPBS 的摘要说明。
	/// </summary>
	public partial class ReportCostByPBS : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Act"];
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

		}
		#endregion

		private void LoadData()
		{
			try
			{
				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 生成明细
		/// </summary>
		private void LoadDataGrid()
		{
			try 
			{
				BLL.ReportCostByPBS report = new RmsPM.BLL.ReportCostByPBS(this.txtProjectCode.Value);

				string StartDate = "";
				string EndDate = "";

				if (this.dtDateBegin.Value != "") StartDate = DateTime.Parse(this.dtDateBegin.Value).ToString("yyyy-MM-dd");
				if (this.dtDateEnd.Value != "") EndDate = DateTime.Parse(this.dtDateEnd.Value).ToString("yyyy-MM-dd");

				report.StartDate = StartDate;
				report.EndDate = EndDate;

				report.Generate();

				BindDataGrid(report);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示成本分项汇总表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 绑定动态费用明细
		/// </summary>
		private void BindDataGrid(BLL.ReportCostByPBS report) 
		{
			try 
			{
				this.txtReportClick.Value = "1";

				ViewState["TitleHtml1"] = report.TitleHtml1;
				ViewState["TitleHtmlArea"] = report.TitleHtmlArea;
				ViewState["TitleHtmlAreaPercent"] = report.TitleHtmlAreaPercent;
				this.lblDateDesc.InnerText = BLL.StringRule.GetDateRangeDesc(report.StartDate, report.EndDate);

				ViewState["TotalPBSArea"] = BLL.StringRule.BuildShowNumberString(report.TotalPBSArea, "#,##0.##");

				DataTable tbDtl = report.tb;

				//单位工程展开
//				ViewState["html_title1"] = report.DateTitleHtml1;
//				ViewState["html_title2"] = report.DateTitleHtml2;

				//折叠全部费用项
				BLL.CostBudgetPageRule.CollapseAll(tbDtl);

				BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 2);

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "绑定成本分项汇总表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 开始统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

	}
}
