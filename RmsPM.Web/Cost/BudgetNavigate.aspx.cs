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
using RmsPM.BLL;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// 输入参数 IsDynamic ， 标记： 0-预算，1 动态 。 取最新一次生效的或者正在审核的版本号s
	/// </summary>
	public partial class BudgetNavigate : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			string isDynamic = Request["IsDynamic"] + "";
			string isDynamicName = ( isDynamic== "0" )? "费用预算":"动态费用";
		
			try
			{
				BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
				sb.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode ) );
				sb.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic,isDynamic ) );
				sb.AddStrategy ( new Strategy( BudgetStrategyName.Flag , "0,1"  ) );
				sb.AddOrder( "MakeDate",false );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData budget = qa.FillEntityData("Budget",sql);
				qa.Dispose();

				string budgetCode = "";
				// 如果有审核过的生效的版本，看这个版本
				// 没有就看最新的版本
				DataRow[] drs = budget.CurrentTable.Select( "Flag=0","MakeDate DESC" );
				if (drs.Length>0)
					budgetCode=(string)drs[0]["BudgetCode"];
				else
				{
					if ( budget.HasRecord())
						budgetCode = budget.GetString("BudgetCode");
				}
				budget.Dispose();

				Response.Write(Rms.Web.JavaScript.ScriptStart);
//				if ( budgetCode == "" )
//					Response.Write( Rms.Web.JavaScript.Alert(false,"还没有做过"+isDynamicName+" ！"   ));

				if ( isDynamic == "0" )
					Response.Write(Rms.Web.JavaScript.PageTo(false,"Budget.aspx?projectCode="+projectCode+"&BudgetCode=" + budgetCode));
				else
					Response.Write(Rms.Web.JavaScript.PageTo(false,"DynamicCost.aspx?projectCode="+projectCode+"&BudgetCode=" + budgetCode));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);

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
