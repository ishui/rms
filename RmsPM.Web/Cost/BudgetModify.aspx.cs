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
	/// BudgetModify 的摘要说明。
	/// </summary>
	public partial class BudgetModify : PageBase
	{
		private const int IWan = 10000;
		private const int IMaxPeriod = 10;
		private const int IMaxMonth = 12;
		protected System.Web.UI.HtmlControls.HtmlTableCell Td1;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadData();
			}
		}


		private void LoadData()
		{

			string costCode = Request["CostCode"] + "";
			string projectCode = Request["ProjectCode"] + "";
			string budgetCode = Request["BudgetCode"] + "" ;
			string type = Request["Type"] + "";

			try
			{
				// 取费用分解结构和估算费用
				V_CBSCostStrategyBuilder sb = new V_CBSCostStrategyBuilder();
				sb.AddStrategy( new Strategy( V_CBSCostStrategyName.ProjectCode,projectCode));
				sb.AddStrategy( new Strategy( V_CBSCostStrategyName.Flag,"-1" ));
				sb.AddOrder( "SortID",true);
				string sql = sb.BuildMainQueryString();

				BudgetStrategyBuilder sb0 = new BudgetStrategyBuilder();
				sb0.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode ) );
				sb0.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic, "0,1" ) );
				sb0.AddOrder("MakeDate",false);
				string sql0 = sb0.BuildMainQueryString();
			
				QueryAgent qa = new QueryAgent();
				EntityData allBudget =qa.FillEntityData("Budget",sql0);
				EntityData allCost = qa.FillEntityData("V_CBSCost",sql);
				qa.Dispose();

				// 找历年的最后一次生效的预算(动态)
				string lastBudgetCode = "";
				DataRow[] drLast = allBudget.CurrentTable.Select( String.Format( " BudgetCode <> '{0}' and Flag in (0,2) ",budgetCode) ," MakeDate DESC " );
				EntityData lastBudget = new EntityData("Standard_Budget");
				if ( drLast.Length>0)
				{
					lastBudgetCode = (string) drLast[0]["BudgetCode"];
					lastBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(lastBudgetCode);
				}
				lastBudget.SetCurrentTable("BudgetYear");

				DataTable tb=allCost.CurrentTable;
				tb.Columns.Add("BudgetCost");					// 预算费用
				tb.Columns.Add("BeforeHappenCost");				// 年前发生
				tb.Columns.Add("CurrentPlanCost");				// 当年计划
				tb.Columns.Add("AfterPlanCost");				// 剩余预算

				for ( int i=1;i<=IMaxMonth;i++)
					tb.Columns.Add("CurrentPlanCost" + i.ToString() );
				for ( int i=1;i<=IMaxPeriod;i++)
					tb.Columns.Add("AfterPlanCost" + i.ToString() );

				for ( int i=1;i<=IMaxMonth;i++)
					tb.Columns.Add("MonthAH" + i.ToString() );
				for ( int i=1;i<=IMaxMonth;i++)
					tb.Columns.Add("MonthUse" + i.ToString() );
				for ( int i=1;i<=IMaxMonth;i++)
					tb.Columns.Add("MonthApply" + i.ToString() );

				for ( int i=1;i<=IMaxPeriod;i++)
					tb.Columns.Add("YearAH" + i.ToString() );
				for ( int i=1;i<=IMaxPeriod;i++)
					tb.Columns.Add("YearUse" + i.ToString() );
				for ( int i=1;i<=IMaxPeriod;i++)
					tb.Columns.Add("YearApply" + i.ToString() );


				// 取相关的成本项
				DataRow[] drs0 = tb.Select("CostCode='" +  costCode + "'" );
				string fullCode = (string) drs0[0]["FullCode"];
				string costName = (string) drs0[0]["CostName"];

				DataRow[] drs = null;
				if ( type == "Detail" )
				{
					drs = allCost.CurrentTable.Select( String.Format( "ParentCode = '{0}' or CostCode = '{0}' " , costCode ),"SortID" );
					foreach ( DataRow drC in drs )
					{
						string ccc = (string)drC["CostCode"];
						if ( ccc != costCode )
							drC["ChildCount"]=0;
					}
				}
				else
				{
					drs = allCost.CurrentTable.Select( String.Format( "CostCode = '{0}' " , costCode) );
					foreach ( DataRow drC in drs )
						drC["ChildCount"]=0;
				}

				int iLength = drs.Length;
				string codes = "";

				EntityData budget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
				int iYear = budget.GetInt("IYear");
				int iMonth = budget.GetInt("IMonth");
				this.txtYear.Value = iYear.ToString();
				this.txtMonth.Value = iMonth.ToString();
				int periodMonth = budget.GetInt("PeriodMonth");
				this.txtPeriodMonth.Value = periodMonth.ToString() ;
				int afterPeriod  = budget.GetInt("AfterPeriod");
				int periodIndex = budget.GetInt( "PeriodIndex");
				this.txtAfterPeriod.Value = afterPeriod.ToString();

				DateTime periodStartDate = DateTime.Parse(budget.GetDateTime("StartDate"));
				// 上一个周期最后一天
				string lastDateLastPeriod = periodStartDate.AddDays(-1).ToString("yyyy-MM-dd");

				this.lblBudgetName.Text = budget.GetString("BudgetName") +"  " + costName  ;

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

				for ( int i =0;i<iLength;i++)
				{
					string tempCode = (string) drs[i]["CostCode"];
					codes += tempCode + ",";

					budget.SetCurrentTable("BudgetCost");
					DataRow[] drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' " , tempCode) );
					if ( drSelect.Length > 0 )
					{
						drs[i]["BudgetCost"]=drSelect[0]["BudgetCost"];
						drs[i]["BeforeHappenCost"]=drSelect[0]["BeforeHappenCost"];
						drs[i]["CurrentPlanCost"]=drSelect[0]["CurrentPlanCost"];
						drs[i]["AfterPlanCost"]=drSelect[0]["AfterPlanCost"];
					}
					else
					{
						drs[i]["BeforeHappenCost"]=BLL.CBSRule.GetAHMoney( tempCode,"",lastDateLastPeriod);
					}

					// 取月份
					budget.SetCurrentTable("BudgetMonth");
					drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' " , tempCode) );
					foreach ( DataRow drMonth in drSelect )
					{
						int month = (int)drMonth["IMonth"];

						// 本期开始时间
						string tempStartDate = DateTime.Parse(String.Format("{0}-{1}-1",iYear,iMonth)).AddMonths(month-1).ToString("yyyy-MM-dd") ;
						// 本期结束时间
						string tempEndDate = DateTime.Parse(String.Format("{0}-{1}-1",iYear,iMonth)).AddMonths(month).AddDays(-1).ToString("yyyy-MM-dd") ;
						// 上期开始时间
						string preTempStartDate = DateTime.Parse(tempStartDate).AddMonths(-1).ToString("yyyy-MM-dd");
						// 上期结束时间
						string preTempEndDate = DateTime.Parse(tempStartDate).AddDays(-1).ToString("yyyy-MM-dd");
							
						decimal monthAH = decimal.Zero ;
						decimal monthUse = decimal.Zero;
						decimal monthApply = decimal.Zero;

						// 已发生
						monthAH = BLL.CBSRule.GetAHMoney( tempCode,tempStartDate,tempEndDate,"","");


						// 合同占用：
							// 合同占用 ＝ 本时间段以内的合同款项总和－本时间段内的合同款项中已经发生的部分（该部分已经计算在已发生金额中了）
						monthUse = BLL.CBSRule.GetContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate)
							- BLL.CBSRule.GetContractAllocationHappenedCost(tempCode,"","",tempStartDate,tempEndDate) ;


						// 待审批的合同
						monthApply = BLL.CBSRule.GetApplyContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate);

						drs[i]["MonthAH" + month.ToString() ] = @"<a href=## class=trAH code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='AH' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
							+ BLL.StringRule.BuildMoneyWanFormatString(monthAH) + @"</a>" ;

						drs[i]["MonthUse" + month.ToString() ] = @"<a href=## class=trUse code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Use' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
							+ BLL.StringRule.BuildMoneyWanFormatString(monthUse) + "</a>" ;

						drs[i]["MonthApply" + month.ToString() ] =@"<a href=## class=trApply code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Apply' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
							+  BLL.StringRule.BuildMoneyWanFormatString(monthApply) + "</a>" ;

						drs[i]["CurrentPlanCost" + month.ToString() ] = drMonth["Money"];
					}


					// 取年份
					budget.SetCurrentTable("BudgetYear");
					drSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' and IYear <> 0 " , tempCode) );
					foreach ( DataRow drYear in drSelect )
					{
						int year = (int)drYear["IYear"];
						string tempStartDate = periodStartDate.AddMonths( periodMonth * (year+periodIndex ) ).ToString("yyyy-MM-dd");
						string tempEndDate = periodStartDate.AddMonths( periodMonth * (year+periodIndex+1) ).AddDays(-1).ToString("yyyy-MM-dd");

						decimal yearAH = decimal.Zero ;
						decimal yearUse = decimal.Zero;
						decimal yearApply = decimal.Zero;

						// 已发生
						yearAH = BLL.CBSRule.GetAHMoney( tempCode,tempStartDate,tempEndDate,"","");

						yearUse = BLL.CBSRule.GetContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate)
							- BLL.CBSRule.GetContractAllocationHappenedCost(tempCode,"","",tempStartDate,tempEndDate);

						yearApply = BLL.CBSRule.GetApplyContractAllocationCost(tempCode,"","",tempStartDate,tempEndDate);

						drs[i]["YearAH" + year.ToString() ] = @"<a href=## class=trAH code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='AH' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
							+  BLL.StringRule.BuildMoneyWanFormatString(yearAH) + "</a>" ;

						drs[i]["YearUse" + year.ToString() ] = @"<a href=## class=trUse code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Use' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
							+ BLL.StringRule.BuildMoneyWanFormatString(yearUse) + "</a>" ;

						drs[i]["YearApply" + year.ToString() ] =@"<a href=## class=trApply code='"+tempCode+@"' startDate='"+tempStartDate+@"' endDate='"+tempEndDate+@"'  NumberType='Apply' onclick='ShowNumberDetail(this.code,this.NumberType,this.startDate,this.endDate);' >" 
							+ BLL.StringRule.BuildMoneyWanFormatString(yearApply) + "</a>";

						DataRow[] yearSelect = budget.CurrentTable.Select( String.Format( "CostCode ='{0}' and IYear={1}  " , tempCode ,year ) );
						if ( yearSelect.Length>0)
						{
							drs[i]["AfterPlanCost" + year.ToString() ] = yearSelect[0]["Money"];
						}
						else
						{
							// 现在IYear是期数。 所以，上次预算的第 5 期，应该等于这次预算的第4期
							DataRow[] lastYearSelect = lastBudget.CurrentTable.Select( String.Format( "CostCode ='{0}' and IYear={1}  " , tempCode ,(year+1) ) );
							if ( lastYearSelect.Length>0)
							{
								drs[i]["AfterPlanCost" + year.ToString() ] = lastYearSelect[0]["Money"];
							}
						}
					}
				}
				budget.Dispose();

				this.txtAllCode.Value = codes;
				if ( type == "Detail" )
					this.repeat1.DataSource = new DataView( tb, String.Format(" ParentCode = '{0}'  or CostCode = '{0}' ",costCode),"SortID" ,DataViewRowState.CurrentRows);
				else
					this.repeat1.DataSource = new DataView( tb, String.Format(" CostCode = '{0}' ",costCode),"" ,DataViewRowState.CurrentRows);

				this.repeat1.DataBind();

				allCost.Dispose();
			}
			catch (Exception ex)
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


		private RepeaterItem GetRepeaterItem( Repeater rp , string costCode )
		{
			foreach( RepeaterItem li in rp.Items )
			{
				string tempCode = ((HtmlInputHidden)li.FindControl("txtCostCode")).Value;
				if ( tempCode == costCode )
					return li;
			}
			return null;
		}


		private decimal GetInputNumber ( RepeaterItem li, string controlName)
		{
			decimal re = decimal.Zero;
			string inputText = ((HtmlInputText)li.FindControl(controlName)).Value;
			if ( Rms.Check.StringCheck.IsNumber(inputText))
				re = decimal.Parse(inputText);

			return re;
		}

		private void checkBudgetData ( RepeaterItem li, int iChildCount, string controlName, decimal cost )
		{
			if ( iChildCount > 0 && ! BLL.MathRule.CheckDecimalEqual( decimal.Zero,cost ) )
				((HtmlInputText)li.FindControl(controlName)).Value = BLL.StringRule.BuildGeneralNumberString(cost);
		}



		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			string budgetCode = Request["BudgetCode"] + "" ;
			string costCode = Request["CostCode"] + "";
			string type = Request["Type"] + "";

			try
			{
				EntityData budget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
				DataRow dr = budget.CurrentRow;

				int iYear = budget.GetInt("IYear");
				int iMonth = budget.GetInt("IMonth");
				int periodMonth = budget.GetInt("PeriodMonth");
				int afterPeriod = budget.GetInt("AfterPeriod");

				string firstDate = iYear.ToString() + "-" + iMonth.ToString() + "-1";
				string lastPeriodLastDate = DateTime.Parse(firstDate).AddDays(-1).ToString("yyyy-MM-dd");
				
				string flag = "-1";
				CostStrategyBuilder sb = new CostStrategyBuilder();
				sb.AddStrategy( new Strategy( CostStrategyName.ProjectCode,projectCode ) );
				sb.AddStrategy( new Strategy( CostStrategyName.Flag,flag ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData allCost = qa.FillEntityData( "Cost",sql );
				qa.Dispose();

				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);

				string[] codesTemp = this.txtResult.Value.Trim().Split( new char[]{';'} );
				for ( int i=0;i<codesTemp.Length;i++)
				{
					if ( codesTemp[i] != "" )
					{
						string[] va = codesTemp[i].Split( new char[]{','} );
						string costCodeTemp = va[0];
						string v1 = va[1];		// 标志是否有东西

						//DataRow[] drs = budget.Tables["Budget"].CurrentTable.Select( String.Format( "CostCode='{0}'" ,costCodeTemp) );

						// 先清数据
						//处理月
						BLL.CBSRule.ClearBudgetData( budget,costCodeTemp);

						//加入数据
						if ( v1 == "T" )
						{
							//先加入月份的
							for ( int m=1;m<=IMaxMonth;m++)
							{
								DataRow[] drMonths = budget.Tables["BudgetMonth"].Select( String.Format(" CostCode='{0}' and IMonth={1} ",costCodeTemp,m) );
								//原先有这一行, 
								if (  drMonths.Length>0 )
								{
									// 超过了 每一期的月份数值
									if ( m> periodMonth)
									{
										drMonths[0]["Money"] = decimal.Zero;
									}
									else
									{
										// 不是空
										if ( va[1+m] != "" )
											drMonths[0]["Money"] = decimal.Parse(va[1+m])*IWan;
										else
											drMonths[0]["Money"] = decimal.Zero;

									}
								}
									//原先没有这一行
								else
								{
									DataRow drMonth = budget.GetNewRecord("BudgetMonth");
									drMonth["BudgetMonthCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetMonthCode");
									drMonth["BudgetCode"] = budgetCode;
									// 当前一期 ，设置为 0 
									drMonth["IYear"] = 0 ;
									drMonth["IMonth"] = m;
									drMonth["ProjectCode"] = projectCode;
									drMonth["CostCode"] = costCodeTemp;
									budget.AddNewRecord(drMonth,"BudgetMonth");

									// 不是空
									if ( va[1+m] != "" )
										drMonth["Money"] = decimal.Parse(va[1+m])*IWan;
									else
										drMonth["Money"] = decimal.Zero;

								}
							}

							// 记录本期数值
							DataRow[] drCurrentYears = budget.Tables["BudgetYear"].Select( String.Format(" CostCode='{0}' and IYear=0 ",costCodeTemp));
							decimal currentYearCost = BLL.MathRule.SumColumn( budget.Tables["BudgetMonth"],"Money", String.Format(" CostCode='{0}'  ",costCodeTemp));
							if ( drCurrentYears.Length > 0 )
							{
								drCurrentYears[0]["Money"] = currentYearCost;
							}
							else
							{
								DataRow drCurrentYear = budget.GetNewRecord("BudgetYear");
								drCurrentYear["BudgetYearCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetYearCode");
								drCurrentYear["BudgetCode"] = budgetCode;
								// 期数 第0期
								drCurrentYear["IYear"] = 0;
								drCurrentYear["ProjectCode"] = projectCode;
								drCurrentYear["CostCode"] = costCodeTemp;
								budget.AddNewRecord(drCurrentYear,"BudgetYear");
								drCurrentYear["Money"] = currentYearCost;
							}

							//记录后续期
							for ( int m=1; m<IMaxPeriod;m++)
							{
								DataRow[] drYears = budget.Tables["BudgetYear"].Select( String.Format(" CostCode='{0}' and IYear={1} ",costCodeTemp,m ) );
								//原先有这一行, 
								if (  drYears.Length>0 )
								{
									// 超过了 每一期的月份数值
									if ( m> afterPeriod)
									{
										drYears[0]["Money"] = decimal.Zero;
									}
									else
									{
										// 不是空
										if ( va[13+m] != "" )
											drYears[0]["Money"] = decimal.Parse(va[13+m])*IWan;
										else
											drYears[0]["Money"] = decimal.Zero;

									}
								}
									//原先没有这一行
								else
								{
									DataRow drYear = budget.GetNewRecord("BudgetYear");
									drYear["BudgetYearCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetYearCode");
									drYear["BudgetCode"] = budgetCode;
									// 期数
									drYear["IYear"] = m;
									drYear["ProjectCode"] = projectCode;
									drYear["CostCode"] = costCodeTemp;
									budget.AddNewRecord(drYear,"BudgetYear");

									// 不是空
									if ( va[13+m] != "" )
										drYear["Money"] = decimal.Parse(va[13+m]) * IWan;
									else
										drYear["Money"] = decimal.Zero;

								}

							}

							//处理费用项的总预算
							
							DataRow[] drBudgetCosts = budget.Tables["BudgetCost"].Select( String.Format(" CostCode='{0}' ",costCodeTemp ) );
							DataRow drBudgetCost = null;

							if ( drBudgetCosts.Length>0)
								drBudgetCost=drBudgetCosts[0];
							else
							{	
								drBudgetCost = budget.GetNewRecord("BudgetCost");
								drBudgetCost["BudgetCostCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetCostCode");
								drBudgetCost["BudgetCode"] = budgetCode;
								drBudgetCost["ProjectCode"] = projectCode;
								drBudgetCost["CostCode"] = costCodeTemp;
								drBudgetCost["AccountPoint"] = 1;
								budget.AddNewRecord(drBudgetCost,"BudgetCost");
							}

							decimal bc = BLL.CBSRule.GetAHMoney(costCodeTemp,"",lastPeriodLastDate);		// 期前的实际发生数值
							//后续期数总费用
							decimal tempAfterTotalCost = BLL.MathRule.SumColumn(budget.Tables["BudgetYear"],"Money", String.Format(" CostCode='{0}' and IYear<>0 ",costCodeTemp ));
							drBudgetCost["BeforeHappenCost"] = bc;
							drBudgetCost["AfterPlanCost"] = tempAfterTotalCost  ;
							drBudgetCost["CurrentPlanCost"] = currentYearCost ;
							drBudgetCost["BudgetCost"] = bc+tempAfterTotalCost + currentYearCost ;
						}
					}
				}

				//累加子项到父项
				BLL.CBSRule.AdCostEstimate( costCode, type, cbs,budget,iYear,iMonth,periodMonth,afterPeriod,budgetCode,lastPeriodLastDate,projectCode);

				// 更新主表总费用
				//EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				//BLL.CBSRule.SumTotalMoney(cbs,budget);


				DAL.EntityDAO.CBSDAO.SubmitAllStandard_Budget( budget);
				budget.Dispose();
				cbs.Dispose();

				Response.Write( Rms.Web.JavaScript.ScriptStart);
				Response.Write( Rms.Web.JavaScript.Alert(false,"保存完毕 ！"));
				
				string from = Request["From"] + "";
				if ( from == "DynamicCost" )
					Response.Write( " window.navigate( 'DynamicCostInfo.aspx?ProjectCode="+projectCode +"&CostCode=" + costCode + "' ); ");
				else
					Response.Write( " window.navigate( 'BudgetInfo.aspx?ProjectCode="+projectCode+"&BudgetCode="+ budgetCode +"&CostCode=" + costCode + "' ); ");

				Response.Write( Rms.Web.JavaScript.OpenerReload(false) );
				Response.Write( Rms.Web.JavaScript.ScriptEnd);

			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

//		private void AdCostLevel ( EntityData cbs , EntityData budget)
//		{
//			// 先置0
//			foreach ( DataRow dr in budget.Tables["BudgetCost"].Rows)
//			{
//				dr["IsEffective"] = 0;
//			}
//
//			// 依次查看费用项，如果某个费用项做了预算，他的这一级别的兄弟费用项都做了预算
//			// IsEffective = 1
//			foreach ( DataRow dr in cbs.CurrentTable.Rows)
//			{
//				string costCode = (string)dr["CostCode"];
//				string parentCode = (string)dr["ParentCode"];
//				int deep = (int)dr["Deep"];
//
//				
//
//				if ( ! BLL.MathRule.CheckDecimalEqual( BLL.MathRule.SumColumn( budget.Tables["BudgetCost"],"BudgetCost", String.Format( " CostCode='{0}' " ,costCode) ) , decimal.Zero ) )
//				{
//					DataRow[] drFriends = null;
//					// 一级节点,只是处理自己，不管兄弟
//					if ( deep != 1 )
//						drFriends = cbs.CurrentTable.Select( String.Format( "ParentCode='{0}'",parentCode ) );
//					else
//						drFriends = cbs.CurrentTable.Select( String.Format( "CostCode='{0}'",costCode ) );
//
//					foreach( DataRow drFriend in drFriends)
//					{
//						string codeTemp = (string)drFriend["CostCode"];
//						DataRow[] drBudgetFriends = budget.Tables["BudgetCost"].Select( String.Format( "CostCode='{0}'" ,codeTemp ) );
//						if ( drBudgetFriends.Length>0)
//							drBudgetFriends[0]["IsEffective"] = 1;
//					}
//				}
//
//			}
//		}


	}
}
