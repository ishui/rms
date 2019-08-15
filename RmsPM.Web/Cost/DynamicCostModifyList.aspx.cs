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
	/// DynamicCostModifyList 的摘要说明。
	/// </summary>
	public partial class DynamicCostModifyList : PageBase
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

			string projectCode = Request["ProjectCode"] + "";
			string inputCostCode = Request["CostCode"] + "";
			try
			{
				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				foreach ( DataRow dr in cbs.CurrentTable.Select( "ChildCount=0","SortID"  ))
				{
					string costCode = (string)dr["CostCode"];
					string costName = (string)dr["CostName"];
					this.sltCost.Items.Add( new ListItem( costName,costCode));
				}

				cbs.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			this.dtCheckDate0.Value = "";
			this.dtCheckDate1.Value = "";
			this.dtMakeDate0.Value = "";
			this.dtMakeDate1.Value = "";

			if ( inputCostCode != "" )
			{
				this.sltCost.Value = inputCostCode;
				this.sltFlag.Value = "";
			}

		}




		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				V_BudgetCostStrategyBuilder sb = new V_BudgetCostStrategyBuilder();
				sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.ProjectCode,projectCode) );
				sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.IsDynamic,"2") );					//动态申请
				if ( this.sltFlag.Value != "" )
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.Flag,this.sltFlag.Value) );		//申请审核状态

				ArrayList ar = new ArrayList();
				if ( this.dtMakeDate0.Value != "" || this.dtMakeDate1.Value !="" )
				{
					ar.Add(this.dtMakeDate0.Value);
					ar.Add(this.dtMakeDate1.Value);
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.MakeDate,ar) );
				}

				ArrayList ar1 = new ArrayList();
				if ( this.dtCheckDate0.Value !="" || this.dtCheckDate1.Value != "" )
				{
					ar1.Add(this.dtCheckDate0.Value);
					ar1.Add(this.dtCheckDate1.Value);
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.CheckDate,ar1) );
				}

				if ( this.sltCost.Value !="")
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.CostCode,this.sltCost.Value) );

				sb.AddOrder("MakeDate",false);
				
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData budgets =qa.FillEntityData( "V_BudgetCost",sql);
				qa.Dispose();

				budgets.CurrentTable.Columns.Add("FlagName");
				foreach ( DataRow dr in budgets.CurrentTable.Rows)
				{
					int flag = (int)dr["Flag"];
					switch (flag)
					{
						case 0:		//没有这个状态
							dr["FlagName"] = "生效";
							break;
						case 1:
							dr["FlagName"] = "未审核";
							break;
						case 2:
							dr["FlagName"] = "审核通过";
							break;
						case 3:
							dr["FlagName"] = "作废";
							break;
					}
				}

				this.dgList.DataSource = budgets.CurrentTable;
				this.dgList.DataBind();
				budgets.Dispose();

			}
			catch ( Exception ex)
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

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}


	}
}
