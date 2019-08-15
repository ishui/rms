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
	/// ReportCostByPBS ��ժҪ˵����
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

		private void LoadData()
		{
			try
			{
				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

		/// <summary>
		/// ������ϸ
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�ɱ�������ܱ����" + ex.Message));
			}
		}

		/// <summary>
		/// �󶨶�̬������ϸ
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

				//��λ����չ��
//				ViewState["html_title1"] = report.DateTitleHtml1;
//				ViewState["html_title2"] = report.DateTitleHtml2;

				//�۵�ȫ��������
				BLL.CostBudgetPageRule.CollapseAll(tbDtl);

				BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 2);

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�󶨳ɱ�������ܱ����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʼͳ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

	}
}
