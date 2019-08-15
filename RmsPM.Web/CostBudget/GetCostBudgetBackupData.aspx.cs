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
	/// GetCostBudgetBackupData 的摘要说明。
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
	}
}
