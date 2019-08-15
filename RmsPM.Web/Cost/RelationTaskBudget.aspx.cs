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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// RelationTaskBudget 的摘要说明。
	/// </summary>
	public partial class RelationTaskBudget : PageBase
	{
	
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
			string costCode = Request["CostCode"]+"";
			this.lblCostName.Text = BLL.CBSRule.GetCostName(costCode);
		}

		private void LoadData()
		{
			string costCode = Request["CostCode"]+"";
			try
			{

				TaskBudgetStrategyBuilder sb = new TaskBudgetStrategyBuilder();
				ArrayList ar = new ArrayList();
				ar.Add(costCode);
				ar.Add("0");
				sb.AddStrategy( new Strategy( TaskBudgetStrategyName.CostCodeEx,ar ));
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "TaskBudget", sb.BuildMainQueryString());
				qa.Dispose();
				this.dgTaskBudget.DataSource = entity.CurrentTable;
				this.dgTaskBudget.DataBind();
				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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
