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
	/// RptFinIOFrame ��ժҪ˵����
	/// </summary>
	public partial class RptFinIOFrame : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tableHint;
	
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

				int type = BLL.ConvertRule.ToInt(Request.QueryString["Type"]);
				this.txtType.Value = type.ToString();

				switch (type)
				{
					case 2:
						this.lblTitle.InnerText = "�ڲ��������";
						this.sltIsSum.Style["display"] = "none";
						this.sltChartType.Style["display"] = "none";
						break;

					case 1:
						this.lblTitle.InnerText = "����ֵ����";
						this.sltDiscountRate.Style["display"] = "";
						BLL.CashFlowRule.LoadDiscountRateSelect(this.sltDiscountRate, "");
						break;

					default:
                        this.lblTitle.InnerText = "��Ŀ����>�������>��������";
						break;
				}

                /*
				//���
				int StartY = 0;
				int EndY = 0;
				BLL.CashFlowRule.GetCashFlowStartEnd(this.txtProjectCode.Value, ref StartY, ref EndY);

				this.ucCostBudgetSelectMonth.MonthStart = StartY.ToString() + "-01-01";
				this.ucCostBudgetSelectMonth.MonthEnd = EndY.ToString() + "-12-31";
                */

                this.ucCostBudgetSelectMonth.MonthStart = DateTime.Today.Year.ToString() + "-01-01";
                this.ucCostBudgetSelectMonth.MonthEnd = DateTime.Today.ToString("yyyy-MM-dd");

//				this.txtProjectName.Value = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
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
