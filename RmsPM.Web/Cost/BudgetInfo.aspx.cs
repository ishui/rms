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
	/// 预算信息 的摘要说明。
	/// </summary>
	public partial class BudgetInfo : PageBase
	{
		private const int IMaxPeriod = 10;
		private const int IMaxMonth = 12;


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadData();
				ContralRight();
			}
		}

		private void ContralRight()
		{

			string costCode = Request["CostCode"] + "";
			ArrayList ar = user.GetCBSResourceRight(costCode);
			if ( ! ar.Contains("040301"))
			{
				Response.Redirect( "../RejectAccess.aspx" );
				Response.End();
			}
			if ( ! ar.Contains("040302"))
			{
				this.btnModify.Visible=false;
				this.btnModifyDetail.Visible = false;
			}
		}

		private void LoadData()
		{

			string budgetCode =Request["BudgetCode"] +  "";
			string costCode = Request["CostCode"] + "";
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				// 取费用分解结构和估算费用
				V_CBSCostStrategyBuilder sb = new V_CBSCostStrategyBuilder();
				sb.AddStrategy( new Strategy( V_CBSCostStrategyName.ProjectCode,projectCode));
				sb.AddStrategy( new Strategy( V_CBSCostStrategyName.Flag,"-1" ));
				sb.AddOrder( "SortID",true);
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData allCost = qa.FillEntityData("V_CBSCost",sql);
				qa.Dispose();


				DataTable tb=allCost.CurrentTable;
				tb.Columns.Add("BudgetCost");					// 预算费用
				tb.Columns.Add("BeforeHappenCost");			// 年前发生
				tb.Columns.Add("CurrentPlanCost");				// 当年计划
				tb.Columns.Add("AfterPlanCost");				// 剩余预算


				for ( int i=1;i<=IMaxMonth;i++)
					tb.Columns.Add("CurrentPlanCost" + i.ToString() );

				for ( int i=1;i<=IMaxPeriod;i++)
					tb.Columns.Add("AfterPlanCost" + i.ToString() );

				// 取相关的成本项
				DataRow[] drs0 = tb.Select("CostCode='" +  costCode + "'" );
				string fullCode = (string) drs0[0]["FullCode"];
				string costName = (string) drs0[0]["CostName"];
				int childCount = (int)drs0[0]["ChildCount"];
				int deep = (int)drs0[0]["Deep"];


				DataRow[] drs = allCost.CurrentTable.Select( String.Format( "FullCode like '{0}%' " , fullCode),"FullCode" );
				string codes = "";
				foreach ( DataRow dr in drs )
				{
					codes += (string)dr["CostCode"] + "," ;
				}
				this.txtAllCode.Value = codes;

				if ( budgetCode != "" )
				{
					EntityData budget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
					int year = budget.GetInt("IYear");
					this.txtYear.Value = year.ToString();
					this.txtMonth.Value =  budget.GetInt("IMonth").ToString();
					this.txtPeriodMonth.Value = budget.GetInt("PeriodMonth").ToString();
					int afterPeriod  = budget.GetInt("AfterPeriod");
					this.txtAfterPeriod.Value = afterPeriod.ToString();

					int flag = budget.GetInt("Flag");
					int periodIndex = budget.GetInt( "PeriodIndex");
//					if ( flag != 1 )
//						this.btnModify.Visible = false;

					int isDynamic = budget.GetInt("IsDynamic");
					int iLength = drs.Length;
					this.tdTitle.InnerHtml = budget.GetString("budgetName") + "  " + costName ;

					// 处理每期预算的名称
					EntityData planName = DAL.EntityDAO.SystemManageDAO.GetPeriodDefineByProjectCode(projectCode);
					for ( int i=1;i<=afterPeriod;i++)
					{
						HtmlTableCell cell = (HtmlTableCell)this.FindControl( "tdYearTitle" + i.ToString() );
						if ( cell != null )
						{
							int index = periodIndex + i;
							DataRow[] drYearNames = planName.CurrentTable.Select(  String.Format( "PeriodIndex={0}" ,index ) );
							if ( drYearNames.Length>0)
							{
								if ( ! drYearNames[0].IsNull("PeriodName"))
								{
									string yearName = (string) drYearNames[0]["PeriodName"];
									if ( yearName != "" )
										cell.InnerHtml = yearName;
								}
							}
						}
					}
					planName.Dispose();

					// 预算的控制点
					// 规则 新预算时，从第一级开始制定，层层制定。
					DataRow[] drsBudget = budget.Tables["BudgetCost"].Select( String.Format( "CostCode='{0}'" ,costCode ) );
					this.btnModify.Visible = false;
					this.btnModifyDetail.Visible = false;
					int accountPoint = 2;
					if ( drsBudget.Length == 0 )
					{
						this.btnModify.Visible = true;
						if ( childCount > 0 )
							this.btnModifyDetail.Visible = true;
					}
					else
					{
						accountPoint = (int) drsBudget[0]["AccountPoint"];
						if ( accountPoint == 1 )
						{
							this.btnModify.Visible = true;
							if ( childCount > 0 )
								this.btnModifyDetail.Visible = true;
						}
					}

					for ( int i =0;i<iLength;i++)
					{
						string tempCode = (string) drs[i]["CostCode"];
						budget.SetCurrentTable("BudgetCost");
						DataRow[] drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' " , tempCode) );
						if ( drSelect.Length > 0 )
						{
							drs[i]["BudgetCost"]=drSelect[0]["BudgetCost"];
							drs[i]["BeforeHappenCost"]=drSelect[0]["BeforeHappenCost"];
							drs[i]["CurrentPlanCost"]=drSelect[0]["CurrentPlanCost"];
							drs[i]["AfterPlanCost"]=drSelect[0]["AfterPlanCost"];
						}

						// 取月份
						budget.SetCurrentTable("BudgetMonth");
						drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' " , tempCode) );
						foreach ( DataRow drMonth in drSelect )
						{
							int month = (int)drMonth["IMonth"];
							drs[i]["CurrentPlanCost" + month.ToString() ] = drMonth["Money"];
						}


						// 取年份
						budget.SetCurrentTable("BudgetYear");
						for ( int l=1; l<=IMaxPeriod; l++ )
						{
							int tempPeriod = l;
							DataRow[] yearSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' and IYear={1}  " , tempCode ,tempPeriod.ToString() ) );
							if ( yearSelect.Length>0)
							{
								drs[i]["AfterPlanCost" + l.ToString() ] = yearSelect[0]["Money"];
							}
						}

					}

					budget.Dispose();
				}

				this.repeat1.DataSource = new DataView( tb, String.Format(" FullCode like '{0}%' ",fullCode),"FullCode" ,DataViewRowState.CurrentRows);
				this.repeat1.DataBind();
				allCost.Dispose();

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
	}
}
