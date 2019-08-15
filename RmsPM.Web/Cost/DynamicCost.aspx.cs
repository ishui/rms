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
	/// 费用预算制定好之后，审核时复制一份到动态费用，作为动态费用的当前版本；
	/// 原先基于上一个预算周期的动态费用变成历史版本。
	/// 
	/// 每次调整动态费用的当前版本，调整其中几条记录，在上一次预算的基础上制定，
	/// 
	/// 
	/// </summary>
	public partial class DynamicCost : PageBase
	{


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( IsPostBack )
				return;

			this.lblToDay.Text = DateTime.Now.ToString("yyyy-MM-dd");
			string budgetCode = Request["BudgetCode"] + "";

			if ( budgetCode == "" ) 
			{
				//this.btnDynamicApplyList.Visible = false;
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


					}

				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
				}
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
