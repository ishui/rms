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

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// 更多相关合同
	/// </summary>
	public partial class MoreContractList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadDataGrid();
			}
		}

		private void IniPage()
		{

		}

		private void LoadDataGrid()
		{
			
			try
			{
				string projectCode = Request["ProjectCode"]+"";
				string costCode = Request["CostCode"]+"";
				DataTable dtContract = BLL.CBSRule.GetCostRelationContract( costCode,projectCode, 5);
				this.repeatContract.DataSource = dtContract;
				this.repeatContract.DataBind();
				dtContract.Dispose();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载合同列表错误。");
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



	}
}
