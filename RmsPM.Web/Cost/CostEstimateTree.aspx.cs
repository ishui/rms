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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// CostEstimateTree ��ժҪ˵����
	/// </summary>
	public partial class CostEstimateTree : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdFootReviseCost;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdFootDynamicCost;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			IniPage();
			SumTotalMoney();
		}

		private void IniPage()
		{
		}

		private void SumTotalMoney()
		{
//			string projectCode = Request["ProjectCode"] + "";
//			string totalEstimateCost = BLL.StringRule.BuildMoneyWanFormatString( BLL.CBSRule.SumTotalEstimateCost("",projectCode));
//			this.tdFootTotalMoney.InnerHtml = totalEstimateCost;
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
