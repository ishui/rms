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
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// BudgetHistoryList 的摘要说明。
	/// </summary>
	public partial class BudgetHistoryList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( ! IsPostBack)
			{
				IniPage();
				LoadData();
			}
			
		}

		private void IniPage()
		{
			string isDynamic =Request["IsDynamic"] + "";
			if ( isDynamic == "0" )
				this.lblBudget.Text = "历次预算";
			else
				this.lblBudget.Text = "历次动态";

		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				string isDynamic = Request["IsDynamic"] + "";
				BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
				sb.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode ) );
				if ( isDynamic != "" )
					sb.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic,isDynamic ) );

				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				qa.SetTopNumber( 1 );
				EntityData budget = qa.FillEntityData( "Budget",sql );
				qa.Dispose();

				this.dgList.DataSource = budget.CurrentTable;
				this.dgList.DataBind();
				budget.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"加载列表错误");
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
