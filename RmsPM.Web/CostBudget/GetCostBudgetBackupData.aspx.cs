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

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// GetCostBudgetBackupData ��ժҪ˵����
	/// </summary>
	public partial class GetCostBudgetBackupData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				string ProjectCode = Request.QueryString["ProjectCode"] + "";
				string SelectCostBudgetBackupCode = Request.QueryString["SelectCostBudgetBackupCode"] + "";

				DataTable tb = BLL.CostBudgetRule.GetCostBudgetBackupSelectTable(ProjectCode, SelectCostBudgetBackupCode);

				Response.Write(BLL.XmlTree.GetDataToXmlString(tb));
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			Response.End();
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
