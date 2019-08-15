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
	/// DynamicCostInfo 的摘要说明。
	/// </summary>
	public partial class DynamicCostInfo : PageBase
	{

		private const int IMaxPeriod = 10;
		private const int IMaxMonth = 12;
		private const int IWan = 10000;



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
			if ( ! ar.Contains("040401"))
			{
				Response.Redirect( "../RejectAccess.aspx" );
				Response.End();
			}

			if ( ! user.HasRight("040402"))
			{
				this.btnAdjust.Visible = false;
				this.btnModifyDetail.Visible = false;
			}
		}

		private void LoadData()
		{

			try
			{
				string costCode = Request["CostCode"] + "";
				string projectCode = Request["ProjectCode"] + "";
				string budgetCode = "" ;

				this.btnAdjust.Visible = false;
				this.btnModifyDetail.Visible = false;
 
				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);

				// 做一个费用项标题
				#region
				string tdText = "";
				string tempCostCode = costCode;
				while ( tempCostCode != "" )
				{
					DataRow[] drsTemp = cbs.CurrentTable.Select ( String.Format( "CostCode='{0}'",tempCostCode) );
					if ( drsTemp.Length > 0 )
					{
						string costName = (string)drsTemp[0]["CostName"];
						if ( tdText != "" )
							tdText = "--" + tdText;
						tdText = @"<a href=## onclick=""doViewDynamicCost('"+tempCostCode+@"')"">"+costName+"</a>" + tdText;

						string parentCode = (string)drsTemp[0]["ParentCode"];
						tempCostCode = parentCode;
					}
					else
						tempCostCode = "";
				}
				
				this.tdCostName.InnerHtml = tdText;
				#endregion

				cbs.Dispose();

				// 取费用分解结构和估算费用
				#region
				int deep = 1;
				EntityData allCost = BLL.CBSRule.GetCostEstimate(projectCode);
				bool isChild = ( allCost.CurrentTable.Select( String.Format( " ParentCode='{0}' " ,costCode ) ).Length  == 0 );
				DataRow[] drCosts = allCost.CurrentTable.Select( String.Format( " CostCode='{0}' ",costCode ) );
				if ( drCosts.Length>0)
				{
					// 估算
					if ( !drCosts[0].IsNull("TotalMoney"))
						this.lblCostEstimate.Text = BLL.StringRule.BuildMoneyWanFormatString((decimal)drCosts[0]["TotalMoney"]);
					deep = (int)drCosts[0]["Deep"];
				}
				#endregion
				
				EntityData budget = BLL.CBSRule.GetCurrentDynamicEntity( projectCode,ref budgetCode );
				if ( budgetCode != "" )
				{
					this.txtBudgetCode.Value = budgetCode;

					// 预算
					string refBudgetCode = budget.GetString("ReferBudgetCode");
					EntityData refBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode( refBudgetCode);

					// 取参数， 提取出基本信息
					#region
					int iYear = budget.GetInt("IYear");
					int iMonth =  budget.GetInt("IMonth");
					int afterPeriod =  budget.GetInt("AfterPeriod");
					int periodIndex = budget.GetInt( "PeriodIndex");
					int periodMonth = budget.GetInt("PeriodMonth");
					int flag = budget.GetInt("Flag");
					DateTime periodEndDate = DateTime.Parse(budget.GetDateTime("EndDate"));
					DateTime periodStartDate = DateTime.Parse(budget.GetDateTime("StartDate"));

					int iNowMonth = (DateTime.Now.Year - iYear) * 12 + (DateTime.Now.Month - iMonth) + 1;
					((HtmlTableCell)this.FindControl("tdMonthTitle"+iNowMonth.ToString())).Attributes.Add("class","tdAlert");

					this.txtYear.Value = iYear.ToString();
					this.txtMonth.Value =  budget.GetInt("IMonth").ToString();
					this.txtPeriodMonth.Value = budget.GetInt("PeriodMonth").ToString();
					this.txtAfterPeriod.Value = afterPeriod.ToString();

					DataRow[] drsBudget = budget.Tables["BudgetCost"].Select( String.Format( "CostCode='{0}'" ,costCode ) );
					if ( drsBudget.Length>0)
					{
						
						// 查看是否 预算控制节点， 可以在这里细化预算
						#region
						
						int accountPoint = 0;
						if ( !drsBudget[0].IsNull("AccountPoint"))
							accountPoint = (int)drsBudget[0]["AccountPoint"];

						if ( accountPoint == 1 )
						{
							this.btnAdjust.Visible = true;
							if ( ! isChild )
								this.btnModifyDetail.Visible = true;
						}

						#endregion


						// 已发生费用(合同加非合同)
						decimal apMoney = BLL.CBSRule.GetAHMoney(costCode,"",DateTime.Now.ToString("yyyy-MM-dd"));

						// 动态费用
						decimal dynamicCost = decimal.Zero;
						if (! drsBudget[0].IsNull("BudgetCost"))
						{
							dynamicCost = (decimal)drsBudget[0]["BudgetCost"];
						}
						// 待发生费用
						decimal dpMoney = dynamicCost - apMoney;

						// 动态费用
						this.lblDynamciCost.Text = BLL.StringRule.BuildMoneyWanFormatString(dynamicCost);
						// 已发生发生
						this.lblAPMoney.Text = BLL.StringRule.BuildMoneyWanFormatString(apMoney);
						// 待发生费用
						this.lblDPMoney.Text = BLL.StringRule.BuildMoneyWanFormatString(dpMoney);

						// 当前节余款 = 赢余 ＋ （ 本期累计到上个月末为止预算－本期累计到上个月末为止实际发生数） 
						// 保存在数据库里面的赢余
						decimal surplusCost = decimal.Zero;
						if ( ! drsBudget[0].IsNull("SurplusCost"))
							surplusCost = (decimal)drsBudget[0]["SurplusCost"];

						// 第一期就不用计算上个月的了
						if ( iNowMonth != 1 )
						{
							//上一个月的月末
							string preMonthEndDate = DateTime.Parse( DateTime.Now.ToString("yyyy-MM-1")).AddDays(-1).ToString("yyyy-MM-dd");
							surplusCost += BLL.MathRule.SumColumn( budget.Tables["BudgetMonth"],"Money",String.Format( "CostCode='{0}' and IMonth<{1} " ,costCode,iNowMonth ) )  -  BLL.CBSRule.GetAHMoney(costCode,periodStartDate.ToString("yyyy-MM-dd"),preMonthEndDate);
						}

						this.lblSurplusCost.Text = BLL.StringRule.BuildMoneyWanFormatString(surplusCost);

						// 后续总预算
						this.lblAfterPlanCost.Text = BLL.StringRule.BuildMoneyWanFormatString(drsBudget[0]["AfterPlanCost"]);

						// 期前累计发生
						this.lblBeforeHappenCost.Text = BLL.StringRule.BuildMoneyWanFormatString(drsBudget[0]["BeforeHappenCost"]);

						// 本期动态
						this.lblCurrentDynamicCost.Text = BLL.StringRule.BuildMoneyWanFormatString(drsBudget[0]["CurrentPlanCost"]);

						this.lblContractUse.Text = BLL.StringRule.BuildMoneyWanFormatString( BLL.CBSRule.GetContractAllocationCost(costCode,"",projectCode,"","",""));
						this.lblContractApply.Text = BLL.StringRule.BuildMoneyWanFormatString(BLL.CBSRule.GetApplyContractAllocationCost(costCode,"",projectCode,"",""));
						this.lblUnContractHappened.Text = BLL.StringRule.BuildMoneyWanFormatString(BLL.CBSRule.GetAHMoney(costCode,"","","","0"));
					}
					else
					{
						this.btnAdjust.Visible = true;
						if ( ! isChild )
							this.btnModifyDetail.Visible = true;

					}

					// 本费用项预算
					DataRow[] drsRef = refBudget.Tables["BudgetCost"].Select( String.Format( "CostCode='{0}'" ,costCode ) );
					if ( drsRef.Length>0)
					{
						if (! drsRef[0].IsNull("BudgetCost"))
							this.lblBudgetCost.Text = BLL.StringRule.BuildMoneyWanFormatString(drsRef[0]["BudgetCost"]);
					}
					#endregion

					DataTable tb=allCost.CurrentTable;
					// 设置临时表
					#region 
					tb.Columns.Add("BudgetCost");					// 预算费用
					tb.Columns.Add("DynamicCost");					// 预算费用
					tb.Columns.Add("BeforeHappenCost");				// 年前发生
					tb.Columns.Add("CurrentPlanCost");				// 当年计划
					tb.Columns.Add("AfterPlanCost");				// 剩余预算
					tb.Columns.Add("tdHappen");

					for ( int i=1;i<=IMaxMonth;i++)
						tb.Columns.Add("MonthAH" + i.ToString() );

					for ( int i=1;i<=IMaxMonth;i++)
						tb.Columns.Add("MonthBudget" + i.ToString() );

					for ( int i=1;i<=IMaxMonth;i++)
						tb.Columns.Add("MonthUse" + i.ToString() );

					for ( int i=1;i<=IMaxMonth;i++)
						tb.Columns.Add("MonthApply" + i.ToString() );

					for ( int i=1;i<=IMaxMonth;i++)
						tb.Columns.Add("MonthSurplusBudget" + i.ToString() );

					for ( int i=1;i<=IMaxPeriod;i++)
						tb.Columns.Add("YearAH" + i.ToString() );

					for ( int i=1;i<=IMaxPeriod;i++)
						tb.Columns.Add("YearBudget" + i.ToString() );

					for ( int i=1;i<=IMaxPeriod;i++)
						tb.Columns.Add("YearUse" + i.ToString() );

					for ( int i=1;i<=IMaxPeriod;i++)
						tb.Columns.Add("YearApply" + i.ToString() );

					for ( int i=1;i<=IMaxMonth;i++)
						tb.Columns.Add("YearSurplusBudget" + i.ToString() );

					#endregion

					// 取相关的成本项
					DataRow[] drs0 = tb.Select("CostCode='" +  costCode + "'" );
					string fullCode = (string) drs0[0]["FullCode"];

					DataRow[] drs  ;
					if ( this.chkChild.Checked )
						drs = allCost.CurrentTable.Select( String.Format( "FullCode like '{0}%' " , fullCode),"FullCode" );
					else
						drs = allCost.CurrentTable.Select( String.Format( "costCode='{0}'" , costCode) );
					int iLength = drs.Length;

					// 相关费用项编号字符串
					#region
					string codes = "";
					foreach ( DataRow dr in drs )
					{
						codes += (string)dr["CostCode"] + "," ;
					}
					this.txtAllCode.Value = codes;
					#endregion

					
					// 处理后续预算每期的名称
					#region
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
					#endregion

					// 循环计算每个费用项显示的内容
					#region
					string viewString = BLL.PageFacade.GetListGroupSelectedValues(this.chklistView);
					this.txtView.Value = viewString;
					bool isUse = (viewString.IndexOf("Use") >=0  || viewString == "") ;
					bool isApply = (viewString.IndexOf("Apply") >=0  || viewString == "") ;

					for ( int i =0;i<iLength;i++)
					{
						string tempCode = (string) drs[i]["CostCode"];

						refBudget.SetCurrentTable("BudgetCost");
						DataRow[] drSelect = refBudget.Tables["BudgetCost"].Select( String.Format( "CostCode ='{0}' " , tempCode) );
						if ( drSelect.Length > 0 )
						{
							drs[i]["BudgetCost"]=drSelect[0]["BudgetCost"];
						}


						budget.SetCurrentTable("BudgetCost");
						drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' " , tempCode) );
						if ( drSelect.Length > 0 )
						{
							drs[i]["DynamicCost"]=drSelect[0]["BudgetCost"];
							drs[i]["BeforeHappenCost"]=drSelect[0]["BeforeHappenCost"];
							drs[i]["CurrentPlanCost"]=drSelect[0]["CurrentPlanCost"];
							drs[i]["AfterPlanCost"]=drSelect[0]["AfterPlanCost"];
						}


						// 取月份
						budget.SetCurrentTable("BudgetMonth");
						for ( int m=1;m<=periodMonth;m++)
						{

							int month = m;
							// 本期开始时间
							string tempStartDate = periodStartDate.AddMonths(month-1).ToString("yyyy-MM-dd") ;
							// 本期结束时间
							string tempEndDate = periodStartDate.AddMonths(month).AddDays(-1).ToString("yyyy-MM-dd") ;
							// 上期开始时间
							string preTempStartDate = DateTime.Parse(tempStartDate).AddMonths(-1).ToString("yyyy-MM-dd");
							// 上期结束时间
							string preTempEndDate = DateTime.Parse(tempStartDate).AddDays(-1).ToString("yyyy-MM-dd");

							decimal monthAH = decimal.Zero ;
							decimal monthBudget = decimal.Zero;
							decimal monthUse = decimal.Zero;
							decimal monthApply = decimal.Zero;
							decimal monthSurplusBudget = decimal.Zero;

							// 已发生
							monthAH = BLL.CBSRule.GetAHMoney( tempCode,tempStartDate,tempEndDate,"","");
							drs[i]["MonthAH" + month.ToString() ] = @"<a href=## class=trAH code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='AH' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
								+ BLL.StringRule.BuildMoneyWanFormatString(monthAH) + @"</a>" ;


							// 合同占用：
							if ( isUse )
							{
								if ( iNowMonth == month )
								{
									// 本月合同应付款 ＝ （本预算周期开始至本月末为止总的合同应付款 －本预算周期开始至本月末为止的合同款项的已发生数）
									// 如果为负数，说明上一个月已经付掉了，则为0
									monthUse = BLL.CBSRule.GetContractAllocationCost(tempCode,"","",periodStartDate.ToString("yyyy-MM-dd"),tempEndDate)
										- BLL.CBSRule.GetContractAllocationHappenedCost(tempCode,"","",periodStartDate.ToString("yyyy-MM-dd"),tempEndDate) ;
									//									if ( monthUse < decimal.Zero )
									//										monthUse = decimal.Zero;
								}
								else if ( iNowMonth < month )
								{
									// 合同占用 ＝ 本时间段以内的合同款项总和－本时间段内的合同款项中已经发生的部分（该部分已经计算在已发生金额中了）
									monthUse = BLL.CBSRule.GetContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate)
										- BLL.CBSRule.GetContractAllocationHappenedCost(tempCode,"","",tempStartDate,tempEndDate) ;
									//									if ( monthUse < decimal.Zero )
									//										monthUse = decimal.Zero;
								}
							}
							drs[i]["MonthUse" + month.ToString() ] = @"<a href=## class=trUse code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Use' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
								+ BLL.StringRule.BuildMoneyWanFormatString(monthUse) + "</a>" ;

							// 待审批的合同，付款计划不会做到以前月份
							if ( isApply &&  ( iNowMonth <= month ) )
							{
								monthApply = BLL.CBSRule.GetApplyContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate);
							}
							drs[i]["MonthApply" + month.ToString() ] =@"<a href=## class=trApply code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Apply' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
								+  BLL.StringRule.BuildMoneyWanFormatString(monthApply) + "</a>" ;


							drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' and iMonth={1} " , tempCode,m ) );
							if ( drSelect.Length > 0 )
							{
								DataRow drMonth = drSelect[0];
								if ( drMonth["Money"] != System.DBNull.Value )
									monthBudget = (decimal) drMonth["Money"];

								if ( iNowMonth <= month )
									monthSurplusBudget = monthBudget - monthAH - monthUse - monthApply;

								drs[i]["MonthBudget" + month.ToString() ] =  BLL.StringRule.BuildMoneyWanFormatString(monthBudget)  ;

								if ( monthSurplusBudget > 0 )
									drs[i]["MonthSurplusBudget" + month.ToString() ] = BLL.StringRule.BuildMoneyWanFormatString(monthSurplusBudget);
								else
									drs[i]["MonthSurplusBudget" + month.ToString() ] = "<font class=tdAlert >" + BLL.StringRule.BuildMoneyWanFormatString(monthSurplusBudget) + "</font>" ;
							}
						}

						// 取年份
						budget.SetCurrentTable("BudgetYear");
						for ( int y=1;y<=afterPeriod;y++)
						{

							int year = y;
							string tempStartDate = periodStartDate.AddMonths( periodMonth * (year+periodIndex ) ).ToString("yyyy-MM-dd");
							string tempEndDate = periodStartDate.AddMonths( periodMonth * (year+periodIndex+1) ).AddDays(-1).ToString("yyyy-MM-dd");
						
							decimal yearAH = decimal.Zero ;
							decimal yearBudget = decimal.Zero;
							decimal yearUse = decimal.Zero;
							decimal yearApply = decimal.Zero;
							decimal yearSurplusBudget = decimal.Zero;

							// 已发生
							yearAH = BLL.CBSRule.GetAHMoney( tempCode,tempStartDate,tempEndDate,"","");
							drs[i]["YearAH" + year.ToString() ] = @"<a href=## class=trAH code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='AH' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
								+  BLL.StringRule.BuildMoneyWanFormatString(yearAH) + "</a>" ;

							// 合同占用：
							if ( isUse )
							{
								yearUse = BLL.CBSRule.GetContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate)
									- BLL.CBSRule.GetContractAllocationHappenedCost(tempCode,"","",tempStartDate,tempEndDate);
							}
							drs[i]["YearUse" + year.ToString() ] = @"<a href=## class=trUse code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Use' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
								+ BLL.StringRule.BuildMoneyWanFormatString(yearUse) + "</a>" ;

							if ( isApply )
							{
								yearApply = BLL.CBSRule.GetApplyContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate);
							}
							drs[i]["YearApply" + year.ToString() ] =@"<a href=## class=trApply code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Apply' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
								+ BLL.StringRule.BuildMoneyWanFormatString(yearApply) + "</a>";

							drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' and IYear = {1} " , tempCode,y ) );
							if ( drSelect.Length>0 )
							{
								DataRow drYear = drSelect[0];
								if ( drYear["Money"] != System.DBNull.Value )
									yearBudget = (decimal) drYear["Money"];

								yearSurplusBudget = yearBudget - yearAH - yearUse - yearApply;


								drs[i]["YearBudget" + year.ToString() ] = BLL.StringRule.BuildMoneyWanFormatString(yearBudget)  ;

								if ( yearSurplusBudget > 0 )
									drs[i]["YearSurplusBudget" + year.ToString() ] = BLL.StringRule.BuildMoneyWanFormatString(yearSurplusBudget);
								else
									drs[i]["YearSurplusBudget" + year.ToString() ] = "<font class=tdAlert >" + BLL.StringRule.BuildMoneyWanFormatString(yearSurplusBudget) + "</font>"  ;

							}
						}
					}

					if ( this.chkChild.Checked )
						this.repeat1.DataSource = new DataView( tb, String.Format(" FullCode like '{0}%' ",fullCode),"FullCode" ,DataViewRowState.CurrentRows);
					else
						this.repeat1.DataSource = new DataView( tb, String.Format(" CostCode = '{0}' ",costCode),"" ,DataViewRowState.CurrentRows);

					this.repeat1.DataBind();
					refBudget.Dispose();
					#endregion
				
				}
				budget.Dispose();
				allCost.Dispose();

				// 查找相应的合同和付款单
				#region
				DataTable dtContract = BLL.CBSRule.GetCostRelationContract( costCode,projectCode, 5);
				this.repeatContract.DataSource = dtContract;
				this.repeatContract.DataBind();
				dtContract.Dispose();
				#endregion

			}
			catch( Exception ex )
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

		protected void btnView_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}


	}
}
