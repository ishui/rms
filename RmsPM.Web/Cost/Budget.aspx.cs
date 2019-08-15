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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// Budget 的摘要说明。
	/// </summary>
	public partial class Budget : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( IsPostBack )
				return;

			string budgetCode = Request["BudgetCode"] + "";
			string projectCode = Request["ProjectCode"] + "";

			if ( budgetCode == "" ) 
			{
//				this.lblYear.Text = " ( 没有制定预算 ！) ";
				this.btnBudgetCheck.Visible = false;
				this.btnNewestBudget.Visible = false;
				return;
			}
			else
			{
				try
				{
					EntityData budget = DAL.EntityDAO.CBSDAO.GetBudgetByCode(budgetCode);
					if ( budget.HasRecord())
					{
						int iYear = budget.GetInt("IYear");
						int iMonth =  budget.GetInt("IMonth");
						int afterPeriod =  budget.GetInt("AfterPeriod");
//						this.lblYear.Text = iYear.ToString();
//						this.lblMonth.Text = iMonth.ToString();

						int flag = budget.GetInt("Flag");
						string flagName = "";
						if ( flag == 0 )
							flagName = "（当前预算，生效中）";
						else if ( flag == 1)
							flagName = "（制定中，未审核）";
						else if ( flag == 2 )
							flagName = "（历年预算）";

						this.lblBudgetName.Text = budget.GetString("BudgetName");
						this.lblRemark.Text = budget.GetString("Remark");
						this.lblStatus.Text	= flagName ;
						this.lblMakePersonName.Text = BLL.SystemRule.GetUserName( budget.GetString("MakePerson"));

						this.lblCheckDate.Text = budget.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName( budget.GetString("CheckPerson"));

						this.lblAfterPeriod.Text = afterPeriod.ToString();
						
						int pMonth = budget.GetInt("PeriodMonth");


						int endMonth = iMonth + pMonth - 1;
						int endYear = iYear;
						if ( endMonth > 12 )
						{
							endYear = endYear + 1;
							endMonth = endMonth - 12;
						}

						int startAfterPeriod = iYear+1;
						int endAfterPerod = iYear + afterPeriod;
						int endPeriod = iYear + afterPeriod;

						this.lblPeriodMonth.Text = iYear.ToString() + "年" + iMonth.ToString() + "月 到 "  + endYear.ToString() + "年" + endMonth.ToString() + "月" ;
						this.lblAfterPeriod.Text = startAfterPeriod.ToString() + "年 到 " + endPeriod.ToString() + "年";


						if ( flag == 1 )
						{
							this.btnNewestBudget.Visible = false;
							this.btnNewBudget.Visible = false;
						}
						else
						{
							this.btnBudgetCheck.Visible = false;

							// 检查是否有正在做的预算
							BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
							sb.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode ) );
							sb.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic,"0" ) );
							sb.AddStrategy ( new Strategy( BudgetStrategyName.Flag , "1"  ) );
							string sql = sb.BuildMainQueryString();
							QueryAgent qa = new QueryAgent();
							EntityData budgets = qa.FillEntityData("Budget",sql);
							qa.Dispose();
							if ( budgets.HasRecord() )
							{
								this.btnNewBudget.Visible = false;
								string budgetNewestCode = budgets.GetString("BudgetCode");
								this.btnNewestBudget.Attributes.Add("onclick",@"gotoBudget('" + budgetNewestCode + @"'); return false;");
							}
							else
								this.btnNewestBudget.Visible = false;
							budgets.Dispose();

						}
					}

				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
				}
			}

			if ( !user.HasRight("040302"))
				this.btnNewBudget.Visible = false;

			if ( !user.HasRight("040303"))
				this.btnBudgetCheck.Visible = false;


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
