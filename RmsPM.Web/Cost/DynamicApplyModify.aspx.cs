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
	/// 动态申请制定 的摘要说明。
	/// </summary>
	public partial class DynamicApplyModify : PageBase
	{

		private const int IMaxMonth = 12;
		private const int IWan = 10000;

		private const int IMaxPeriod = 10;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
				string costCode = Request["InputCostCode"]+"";
				if ( costCode != "" )
					LoadItemData(costCode);
			}

		}

		private void IniPage()
		{
//			string costCode = Request["CostCode"] + "";

			string budgetCode = Request["BudgetCode"] + "" ;
			string projectCode = Request["ProjectCode"] + "";

			this.lblApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			this.lblApplyPersonName.Text = user.UserName;

			bool isNew = ((Request["Action"] + "" ) == "AddNew");
			if ( budgetCode == "" )
				budgetCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetCode");
			this.ViewState["BudgetCode"] = budgetCode;

			try
			{

				// 当前生效的动态费用
				string cdBudgetCode = "";
				EntityData allBudget  = BLL.CBSRule.GetCurrentDynamicEntity( projectCode, ref cdBudgetCode);

				// 预算Budget
				string refBudgetCode = allBudget.GetString("BudgetCode");
				EntityData refBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(refBudgetCode);

				// 取相关的成本项
				int iYear = refBudget.GetInt("IYear");
				int iMonth = refBudget.GetInt("IMonth");
				int periodMonth = refBudget.GetInt("PeriodMonth");
				int afterPeriod  = refBudget.GetInt("AfterPeriod");
				int periodIndex = refBudget.GetInt("PeriodIndex");
				DateTime periodStartDate = DateTime.Parse(refBudget.GetDateTime("StartDate"));
				int dynamicStartMonth = (DateTime.Now.Year-iYear) * 12 + (DateTime.Now.Month-iMonth) + 1  ;
		
				EntityData budget = null;
				if ( isNew )
				{
					budget = new EntityData("Standard_Budget");
					DataRow dr = budget.GetNewRecord();
					dr["BudgetCode"] = budgetCode;
					dr["IYear"] = iYear;
					dr["IMonth"] = iMonth;
					dr["PeriodMonth"] = periodMonth;
					dr["AfterPeriod"] = afterPeriod;
					dr["IDynamicStartMonth"] = dynamicStartMonth;
					dr["PeriodIndex"]=periodIndex;
					budget.AddNewRecord(dr);
				}
				else
					budget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);

				Session["BudgetApplyEntityData"] = budget;
				budget.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";
			bool isNew = ((Request["Action"] + "" ) == "AddNew");
			if ( isNew ) 
				return;

			try
			{
				EntityData budget = (EntityData)Session["BudgetApplyEntityData"];
				string budgetCode = budget.GetString("BudgetCode");
				this.txtReason.Value = budget.GetString("Reason");
				this.lblApplyDate.Text = budget.GetDateTimeOnlyDate("MakeDate");
				this.lblApplyPersonName.Text = BLL.SystemRule.GetUserName(budget.GetString("MakePerson"));

//				// 生成编号串
//				string codes = "";
//				foreach ( DataRow dr in budget.Tables["BudgetCost"].Select("IsModify=1"))
//				{
//					string tempCode = (string)dr["CostCode"];
//					codes += tempCode + ",";
//				}
//				this.txtAllCode.Value=codes;
				budget.Dispose();
				LoadGrid();
			
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}

		private void LoadGrid()
		{
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				// 当前生效的动态费用
				string cdBudgetCode = "";
				EntityData allBudget  = BLL.CBSRule.GetCurrentDynamicEntity( projectCode, ref cdBudgetCode);

				EntityData budget = (EntityData)Session["BudgetApplyEntityData"];
				string budgetCode = budget.GetString("BudgetCode");
				// 取相关的成本项

				// 生成编号串
				string codes = "";
				foreach ( DataRow dr in budget.Tables["BudgetCost"].Rows )
				{
					string tempCode = (string)dr["CostCode"];
					codes += tempCode + ",";
				}

				DataTable tb = BLL.CBSRule.BuildAdjustStringTable( budget,allBudget);

				this.repeatList.DataSource = tb;
				this.repeatList.DataBind();

				budget.Dispose();
				allBudget.Dispose();
				tb.Dispose();

				this.txtAllCode.Value = codes;
				
			
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		private void LoadItemData( string costCode)
		{

			if ( costCode == ""  )
				return;

			this.ViewState["CostCode"]=costCode;

			this.btnDeleteItem.Visible=true;

			string budgetCode = ViewState["BudgetCode"].ToString();
			string projectCode = Request["ProjectCode"] + "";

			try
			{

				this.lblCostName.Text = BLL.CBSRule.GetCostName(costCode);

				// 取费用分解结构和估算费用
				EntityData allCost = BLL.CBSRule.GetCostEstimate(projectCode);
				EntityData budget = (EntityData)Session["BudgetApplyEntityData"];
				// 参考动态费用
				string cdBudgetCode = "";
				EntityData refBudget = BLL.CBSRule.GetCurrentDynamicEntity(projectCode,ref cdBudgetCode);
				// 预算
				string yuBudgetCode = refBudget.GetString("BudgetCode");
				EntityData yuBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(yuBudgetCode);

				int iYear = refBudget.GetInt("IYear");
				int iMonth = refBudget.GetInt("IMonth");
				int periodMonth = refBudget.GetInt("PeriodMonth");
				int afterPeriod  = refBudget.GetInt("AfterPeriod");
				int periodIndex = budget.GetInt( "PeriodIndex");

				DateTime periodEndDate = DateTime.Parse(refBudget.GetDateTime("EndDate"));
				DateTime periodStartDate = DateTime.Parse(refBudget.GetDateTime("StartDate"));

				this.txtYear.Value = iYear.ToString();
				this.txtMonth.Value = iMonth.ToString();
				this.txtPeriodMonth.Value = periodMonth.ToString() ;
				this.txtAfterPeriod.Value = afterPeriod.ToString();

				// 动态从什么月份开始做
				int dynamicStartMonth = budget.GetInt("IDynamicStartMonth");
				this.txtDynamicStartMonth.Value = dynamicStartMonth.ToString();
				if ( dynamicStartMonth == 1 )
				{
					this.tdCurrentHappenMonth.Visible = false;
				}
				else
				{
					this.tdCurrentHappenMonth.ColSpan = dynamicStartMonth -1 ;
					this.tdAfterHappenMonth.ColSpan = IMaxMonth - dynamicStartMonth +1 ;
				}

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

				DateTime budgetStartDate = DateTime.Parse( String.Format( "{0}-{1}-1" ,iYear,iMonth ) );
				
				// 计算期前节余
				// 期前节余有可能时负数，表示超付，在调整的时候必须调整成0
				decimal preSurplus = decimal.Zero;
				DataRow[] drsSelect = refBudget.Tables["BudgetCost"].Select( String.Format( "CostCode='{0}'" ,costCode ) );
				if ( drsSelect.Length > 0 )
				{
					if ( ! drsSelect[0].IsNull("SurplusCost"))
						preSurplus = (decimal)drsSelect[0]["SurplusCost"];
				}
				decimal preBudget = BLL.MathRule.SumColumn( refBudget.Tables["BudgetMonth"] ,"Money",String.Format( "IMonth<{0} and CostCode='{1}'" , dynamicStartMonth, costCode ));
				decimal preAH = BLL.CBSRule.GetAHMoney(costCode, budgetStartDate.ToString("yyyy-MM-dd"), DateTime.Parse(DateTime.Now.ToString("yyyy-MM-1")).AddDays(-1).ToString("yyyy-MM-dd"),"","" );
				preSurplus += preBudget-preAH;

				this.tdPreAH.InnerText = BLL.StringRule.BuildMoneyWanFormatString( preAH);
				this.tdPreOldBudget.InnerText = BLL.StringRule.BuildMoneyWanFormatString( preBudget);
				this.tdPreSurplus.InnerText = BLL.StringRule.BuildMoneyWanFormatString( preSurplus);
				this.txtOldSurplusCost.Value = preSurplus.ToString();

				//				this.lblSurplus.Text = BLL.StringRule.BuildMoneyWanFormatString( surplus);

				// 计算各个参考值
				for ( int i=1; i<=IMaxMonth; i++)
				{
					// 月份第一天
					string startMonthDate = budgetStartDate.AddMonths(i-1).ToString("yyyy-MM-dd");
					// 月份最后一天
					string endMonthDate = budgetStartDate.AddMonths(i).AddDays(-1).ToString("yyyy-MM-dd");

					// 本月实际发生
					HtmlTableCell cell =  GetNewFormatHtmlTableCell() ;
					cell.ID = "tdMonthAH" + i.ToString() ;
					cell.Attributes.Add("class","trAH");
					this.trAH.Cells.Add(cell);
					decimal ah =  BLL.CBSRule.GetAHMoney(costCode,startMonthDate,endMonthDate,"","" );
					cell.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( ah);

					// 合同占用 ＝ （本月合同款项－本月合同款项已经付出的）
					HtmlTableCell cell0 =  GetNewFormatHtmlTableCell() ;
					cell0.ID = "tdMonthUse" + i.ToString() ;
					cell0.Attributes.Add("class","trUse");
					this.trUse.Cells.Add(cell0);
					// 是否当月
					decimal contractUse = decimal.Zero;
					if ( i == dynamicStartMonth )
						contractUse =  BLL.CBSRule.GetContractAllocationCost(costCode,"","",periodStartDate.ToString("yyyy-MM-dd"),endMonthDate )
							- BLL.CBSRule.GetContractAllocationHappenedCost(costCode,"","",periodStartDate.ToString("yyyy-MM-dd"),endMonthDate );
					else
						contractUse =  BLL.CBSRule.GetContractAllocationCost(costCode,"","",startMonthDate,endMonthDate )
							- BLL.CBSRule.GetContractAllocationHappenedCost(costCode,"","",startMonthDate,endMonthDate );
					cell0.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( contractUse );

					// 合同申请中
					HtmlTableCell cell1 =  GetNewFormatHtmlTableCell() ;
					cell1.ID = "tdMonthApply" + i.ToString() ;
					cell1.Attributes.Add("class","trApply");
					this.trApply.Cells.Add(cell1);
					decimal contractApply = BLL.CBSRule.GetApplyContractAllocationCost(costCode,"","",startMonthDate,endMonthDate );
					cell1.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( contractApply );


					// 旧预算
					decimal oldMonthBudget = BLL.MathRule.SumColumn(refBudget.Tables["BudgetMonth"] ,"Money", String.Format( "CostCode='{0}' and IMonth={1} ",costCode,i ));
					HtmlTableCell cell2 =  GetNewFormatHtmlTableCell() ;
					cell2.ID = "tdMonthOldBudget" + i.ToString() ;
					cell2.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(oldMonthBudget);
					cell2.Attributes.Add("class","trBudget");
					this.trOldBudget.Cells.Add(cell2);

					// 新预算
					decimal newMonthBudget = decimal.Zero;
					DataRow [] drsNewMonthBudget = budget.Tables["BudgetMonth"].Select(String.Format( "CostCode='{0}' and IMonth={1} ",costCode,i ));
					// 没有制定新预算时用老预算
					if ( drsNewMonthBudget.Length > 0 )
					{
						if ( ! drsNewMonthBudget[0].IsNull("Money"))
							newMonthBudget = (decimal)drsNewMonthBudget[0]["Money"];
					}
					else
						newMonthBudget = oldMonthBudget;
					
					// 预算余额 = 旧预算 － 实际发生 - 占用 - 申请
					decimal balance = oldMonthBudget - ah - contractUse - contractApply ;
					HtmlTableCell cell3 =  GetNewFormatHtmlTableCell() ;
					cell3.ID = "tdMonthBalance" + i.ToString() ;
					cell3.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( balance );
					if ( oldMonthBudget <  (contractUse + contractApply ) )
						cell3.Attributes.Add("class","tdAlert");

					this.trBalance.Cells.Add(cell3);

					// 调整预算
					decimal monthSurplus = newMonthBudget - ah - contractUse - contractApply ;
					((HtmlInputText)this.FindControl("txtMonthNewBudget" + i.ToString() )).Value = BLL.StringRule.BuildMoneyWanFormatString(  monthSurplus ) ;

				}

				// 计算各个参考值
				for ( int i=1; i<=IMaxPeriod; i++)
				{
					// 后续期初第一天
					string startYearDate = budgetStartDate.AddMonths( periodMonth * i ).ToString("yyyy-MM-dd");
					// 后续期末
					string endYearDate = budgetStartDate.AddMonths( periodMonth * (i+1) ).AddDays(-1).ToString("yyyy-MM-dd");

					// 本期实际发生
					HtmlTableCell cell =  GetNewFormatHtmlTableCell() ;
					cell.ID = "tdYearAH" + i.ToString() ;
					cell.Attributes.Add("class","trAH");
					this.trAH.Cells.Add(cell);
					decimal ah =  BLL.CBSRule.GetAHMoney(costCode,startYearDate,endYearDate,"","" );
					cell.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( ah);

					// 合同占用
					HtmlTableCell cell0 =  GetNewFormatHtmlTableCell() ;
					cell0.ID = "tdYearUse" + i.ToString() ;
					cell0.Attributes.Add("class","trUse");
					this.trUse.Cells.Add(cell0);
					decimal contractUse =  BLL.CBSRule.GetContractAllocationCost(costCode,"","",startYearDate,endYearDate )
						- BLL.CBSRule.GetContractAllocationHappenedCost(costCode,"","",startYearDate,endYearDate );
					cell0.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( contractUse);

					// 合同申请中
					HtmlTableCell cell1 =  GetNewFormatHtmlTableCell() ;
					cell1.ID = "tdYearApply" + i.ToString() ;
					cell1.Attributes.Add("class","trApply");
					this.trApply.Cells.Add(cell1);
					decimal contractApply =  BLL.CBSRule.GetApplyContractAllocationCost(costCode,"","",startYearDate,endYearDate );
					cell1.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( contractApply );

					// 旧预算
					decimal oldYearBudget = BLL.MathRule.SumColumn(refBudget.Tables["BudgetYear"] ,"Money", String.Format( "CostCode='{0}' and IYear={1} ",costCode,i ));
					HtmlTableCell cell2 =  GetNewFormatHtmlTableCell() ;
					cell2.ID = "tdYearOldBudget" + i.ToString() ;
					cell2.Attributes.Add("class","trBudget");
					cell2.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(oldYearBudget);
					this.trOldBudget.Cells.Add(cell2);

					// 新预算
					decimal newYearBudget = decimal.Zero;
					DataRow [] drsNewYearBudget = budget.Tables["BudgetYear"].Select(String.Format( "CostCode='{0}' and IYear={1} ",costCode,i ));
					if (drsNewYearBudget.Length>0)
					{
						if ( ! drsNewYearBudget[0].IsNull("Money"))
							newYearBudget = (decimal)drsNewYearBudget[0]["Money"];
					}
					else
						newYearBudget = oldYearBudget;


					// 预算余额 = 旧预算 － 实际发生
					decimal balance = oldYearBudget - ah - contractUse - contractApply ;
					HtmlTableCell cell3 =  GetNewFormatHtmlTableCell() ;
					cell3.ID = "tdYearBalance" + i.ToString() ;
					cell3.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( balance );
					if ( oldYearBudget < ( contractApply + contractUse ) )
						cell3.Attributes.Add("class","tdAlert");

					this.trBalance.Cells.Add(cell3);

					// 调整预算
					decimal yearSurplus = newYearBudget - ah - contractUse - contractApply ;
					((HtmlInputText)this.FindControl("txtYearNewBudget" + i.ToString() )).Value = BLL.StringRule.BuildMoneyWanFormatString(  yearSurplus ) ;


				}
					
				// 已发生费用
				decimal apMoney = BLL.CBSRule.GetAHMoney(costCode,"",DateTime.Now.ToString("yyyy-MM-dd"));

				// 动态费用
				decimal dynamicCost = BLL.MathRule.SumColumn(  refBudget.Tables["BudgetCost"],"BudgetCost",String.Format( "CostCode='{0}'",costCode ));
				// 待发生费用
				decimal dpMoney = dynamicCost - apMoney;

//				this.lblDynamciCost.Text = BLL.StringRule.BuildMoneyWanFormatString(dynamicCost);
//				this.lblAHMoney.Text = BLL.StringRule.BuildMoneyWanFormatString(apMoney);
//				this.lblDPMoney.Text = BLL.StringRule.BuildMoneyWanFormatString(dpMoney);
//
//				this.lblAfterPlanCost.Text = BLL.StringRule.BuildMoneyWanFormatString( BLL.MathRule.SumColumn(  refBudget.Tables["BudgetCost"],"AfterPlanCost",String.Format( "CostCode='{0}'",costCode )) );
//				this.lblBeforeHappenCost.Text = BLL.StringRule.BuildMoneyWanFormatString(BLL.MathRule.SumColumn(  refBudget.Tables["BudgetCost"],"BeforeHappenCost",String.Format( "CostCode='{0}'",costCode )));
//
//				this.lblCurrentDynamicCost.Text = BLL.StringRule.BuildMoneyWanFormatString( BLL.MathRule.SumColumn(  refBudget.Tables["BudgetYear"],"Money",String.Format( " IYear=0 and CostCode='{0}'",costCode )));
//
//				this.lblBudgetCost.Text = BLL.StringRule.BuildMoneyWanFormatString( BLL.MathRule.SumColumn( yuBudget.Tables["BudgetCost"],"BudgetCost",String.Format( "CostCode='{0}'",costCode )));

				yuBudget.Dispose();
				budget.Dispose();
				refBudget.Dispose();
				allCost.Dispose();
			
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		private bool SaveItemData()
		{
			string costCode = this.ViewState["CostCode"].ToString();
			if ( costCode == "" )
				return true;

			string budgetCode = ViewState["BudgetCode"].ToString();
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				// 取费用分解结构和估算费用
				EntityData allCost = BLL.CBSRule.GetCostEstimate(projectCode);

				EntityData budget = (EntityData)Session["BudgetApplyEntityData"];

				// 参考动态费用
				string cdBudgetCode = "";
				EntityData refBudget = BLL.CBSRule.GetCurrentDynamicEntity(projectCode,ref cdBudgetCode);
				// 预算
				string yuBudgetCode = refBudget.GetString("ReferBudgetCode");
				EntityData yuBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(yuBudgetCode);

				int iYear = budget.GetInt("IYear");
				int iMonth = budget.GetInt("IMonth");
				int periodMonth = budget.GetInt("PeriodMonth");
				int afterPeriod  = budget.GetInt("AfterPeriod");
				int dynamicStartMonth = budget.GetInt("IDynamicStartMonth");
				DateTime periodEndDate = DateTime.Parse(refBudget.GetDateTime("EndDate"));
				DateTime periodStartDate = DateTime.Parse(refBudget.GetDateTime("StartDate"));


				DataRow dr = budget.CurrentRow;
				foreach ( DataRow drTemp in budget.Tables["BudgetYear"].Select(String.Format("CostCode='{0}'",costCode)))
					drTemp.Delete();
				foreach ( DataRow drTemp in budget.Tables["BudgetMonth"].Select(String.Format("CostCode='{0}'",costCode)))
					drTemp.Delete();
				foreach ( DataRow drTemp in budget.Tables["BudgetCost"].Select(String.Format("CostCode='{0}'",costCode)))
					drTemp.Delete();

				for ( int i=1;i<=periodMonth;i++ )
				{

					// 月份第一天
					string startMonthDate = periodStartDate.AddMonths(i-1).ToString("yyyy-MM-dd");
					// 月份最后一天
					string endMonthDate = periodStartDate.AddMonths(i).AddDays(-1).ToString("yyyy-MM-dd");


					DataRow drMonth = budget.GetNewRecord( "BudgetMonth" );
					drMonth["BudgetMonthCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetMonthCode");
					drMonth["BudgetCode"] = budgetCode;
					drMonth["ProjectCode"] = projectCode;
					drMonth["CostCode"] = costCode;
					drMonth["IYear"] = 0;
					drMonth["IMonth"] = i;
					// 已发生费用

					decimal ah =  BLL.CBSRule.GetAHMoney(costCode,startMonthDate,endMonthDate,"","" );

					decimal contractUse = decimal.Zero;
					if ( i == dynamicStartMonth)
						contractUse =  BLL.CBSRule.GetContractAllocationCost(costCode,"","",periodStartDate.ToString("yyyy-MM-dd"),endMonthDate )
							-BLL.CBSRule.GetContractAllocationHappenedCost(costCode,"","",periodStartDate.ToString("yyyy-MM-dd"),endMonthDate );
					else
                        contractUse =  BLL.CBSRule.GetContractAllocationCost(costCode,"","",startMonthDate,endMonthDate )
							-BLL.CBSRule.GetContractAllocationHappenedCost(costCode,"","",startMonthDate,endMonthDate );

					decimal contractApply = BLL.CBSRule.GetApplyContractAllocationCost(costCode,"","",startMonthDate,endMonthDate );

					string money = ((HtmlInputText)this.FindControl( "txtMonthNewBudget" + i.ToString() )).Value;
					//decimal oldMonthBudget = BLL.MathRule.SumColumn(refBudget.Tables["BudgetMonth"] ,"Money", String.Format( "CostCode='{0}' and IMonth={1} ",costCode,i ));

					// 在调整月以前的动态费用数就是实际发生数
					if ( i<dynamicStartMonth )
					{
						drMonth["Money"] = ah;
					}
					else
					{
						if ( Rms.Check.StringCheck.IsNumber(money))
							drMonth["Money"] = decimal.Parse( money ) * IWan + ah + contractUse + contractApply ;
						else
							drMonth["Money"] = ah + contractUse + contractApply ;
					}
					budget.AddNewRecord(drMonth,"BudgetMonth");
						
				}

				for ( int i=1;i<=afterPeriod;i++)
				{

					// 后续期初第一天
					string startYearDate = periodStartDate.AddMonths( periodMonth * i ).ToString("yyyy-MM-dd");
					// 后续期末
					string endYearDate = periodStartDate.AddMonths( periodMonth * (i+1) ).AddDays(-1).ToString("yyyy-MM-dd");

					DataRow drYear = budget.GetNewRecord( "BudgetYear" );
					drYear["BudgetYearCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetYearCode");
					drYear["BudgetCode"] = budgetCode;
					drYear["ProjectCode"] = projectCode;
					drYear["CostCode"] = costCode;
					drYear["IYear"] = i;
					// 已发生
					decimal ah =  BLL.CBSRule.GetAHMoney(costCode,startYearDate,endYearDate,"","" );
					decimal contractUse =  BLL.CBSRule.GetContractAllocationCost(costCode,"","",startYearDate,endYearDate )
						- BLL.CBSRule.GetContractAllocationHappenedCost(costCode,"","",startYearDate,endYearDate );
					decimal contractApply =  BLL.CBSRule.GetApplyContractAllocationCost(costCode,"","",startYearDate,endYearDate );
					string money = ((HtmlInputText)this.FindControl( "txtYearNewBudget" + i.ToString() )).Value;

					//decimal oldYearBudget = BLL.MathRule.SumColumn(refBudget.Tables["BudgetYear"] ,"Money", String.Format( "CostCode='{0}' and IYear={1} ",costCode,i ));
					if ( Rms.Check.StringCheck.IsNumber(money))
						drYear["Money"] =  decimal.Parse(money) * IWan + ah + contractUse + contractApply  ;
					else
						drYear["Money"] =  ah + contractUse + contractApply ;
					budget.AddNewRecord(drYear,"BudgetYear");
				}

				DataRow drBudgetCost = budget.GetNewRecord("BudgetCost");
				drBudgetCost["BudgetCostCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetCostCode");
				drBudgetCost["CostCode"] = costCode;
				drBudgetCost["BudgetCode"] = budgetCode;
				drBudgetCost["ProjectCode"] = projectCode;
				// 调整的当然都是 控制点
				drBudgetCost["AccountPoint"] = 1;
				drBudgetCost["IsModify"] = 1;

				// 上个月结束日期
				string lastMonthEndDate = periodStartDate.AddMonths( dynamicStartMonth ).AddDays(-1).ToString("yyyy-MM-dd");
				decimal bp = BLL.CBSRule.GetAHMoney(costCode,"", lastMonthEndDate );
				decimal cp = BLL.MathRule.SumColumn( budget.Tables["BudgetMonth"],"Money",String.Format( "CostCode='{0}'" ,costCode ));
				decimal ap = BLL.MathRule.SumColumn( budget.Tables["BudgetYear"],"Money",String.Format( "CostCode='{0}'" ,costCode ));

				drBudgetCost["BeforeHappenCost"] = bp;
				drBudgetCost["CurrentPlanCost"] = cp;
				drBudgetCost["AfterPlanCost"] = ap;
				drBudgetCost["BudgetCost"] = bp+cp+ap;

				string sPreSurplus = this.txtPreSurplus.Value;
				decimal dPreSurplus = decimal.Zero;
				if ( sPreSurplus != "" )
				{
					dPreSurplus = decimal.Parse(sPreSurplus);
				}
				drBudgetCost["SurplusCost"] = dPreSurplus;
				budget.AddNewRecord(drBudgetCost,"BudgetCost");

				Session["BudgetApplyEntityData"] = budget;
				budget.Dispose();
				allCost.Dispose();
				refBudget.Dispose();
				yuBudget.Dispose();

				LoadGrid();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			return true;
		}

		private HtmlTableCell GetNewFormatHtmlTableCell()
		{
			HtmlTableCell cell = new HtmlTableCell();
			cell.Align="Right";
			return cell;
		}

		private void ClearItemData()
		{
			this.tableItemMain.Visible = false;

			this.btnDeleteItem.Visible=false;
			this.ViewState["CostCode"]="";
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


		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			if ( ! SaveItemData() )
				return;

			string projectCode = Request["ProjectCode"] + "";
			if ( this.txtReason.Value.Trim()=="")
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"必须填写调整原因 ！"));
				return;
			}

			try
			{
				// 当前生效的动态费用
				string cdBudgetCode = "";
				EntityData allBudget  = BLL.CBSRule.GetCurrentDynamicEntity( projectCode, ref cdBudgetCode);

				// 找当前动态
				string refBudgetCode = allBudget.GetString("BudgetCode");
				EntityData refBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(refBudgetCode);

				// 取参数
				int iYear = refBudget.GetInt("IYear");
				int iMonth = refBudget.GetInt("IMonth");
				int periodMonth = refBudget.GetInt("PeriodMonth");
				int afterPeriod  = refBudget.GetInt("AfterPeriod");
				int dynamicStartMonth = (DateTime.Now.Year-iYear) * 12 + (DateTime.Now.Month-iMonth) + 1  ;

				//bool isNew = ((Request["Action"] + "" ) == "AddNew");
				EntityData budget = (EntityData)Session["BudgetApplyEntityData"];
				string budgetCode = budget.GetString("BudgetCode");

				DataTable tb = BLL.CBSRule.BuildAdjustStringTable( budget,allBudget);
				foreach ( DataRow drTemp in tb.Rows )
				{
					string codeTemp = (string)drTemp["CostCode"];
					foreach ( DataRow drT in budget.Tables["BudgetCost"].Select(String.Format( "CostCode='{0}'" ,codeTemp )))
						drT["AdjustDetail"] = drTemp["AdjustString"];
				}

				DataRow dr = budget.Tables["Budget"].Rows[0];
//				dr["BudgetName"] = this.txtBudgetName.Value;
				dr["ProjectCode"] = projectCode ;
//				dr["Detail"] = this.txtDetail.Value;
				dr["Reason"] = this.txtReason.Value;
				dr["MakePerson"] = base.user.UserCode;
				dr["MakeDate"] = DateTime.Now.Date;
				dr["ReferBudgetCode"] = cdBudgetCode;
				dr["IsDynamic"] = 2;
				dr["Flag"] = 1;

				DAL.EntityDAO.CBSDAO.SubmitAllStandard_Budget(budget);
				budget.Dispose();
				refBudget.Dispose();

				Response.Write( Rms.Web.JavaScript.ScriptStart );
				Response.Write( "window.navigate('../Cost/DynamicApplyInfo.aspx?BudgetCode="+budgetCode+ "&ProjectCode=" + projectCode  + "')  ;" );
				Response.Write( Rms.Web.JavaScript.ScriptEnd );
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}

		protected void btnRefreshItem_ServerClick(object sender, System.EventArgs e)
		{
			LoadGrid();
		}

		protected void btnReturnSelectCodes_ServerClick(object sender, System.EventArgs e)
		{
			if ( !SaveItemData())
				return;
			string costCode = this.txtSelectReturnCodes.Value;
			if ( costCode != "" )
				LoadItemData(costCode);
		}

		protected void btnDeleteItem_ServerClick(object sender, System.EventArgs e)
		{
			string costCode = this.ViewState["CostCode"].ToString();
			if ( costCode == "" )
				return;

			try
			{
				EntityData budget = (EntityData)Session["BudgetApplyEntityData"];
				foreach ( DataRow dr in budget.Tables["BudgetMonth"].Select( String.Format( "CostCode='{0}'",costCode ) ))
					dr.Delete();

				foreach ( DataRow dr in budget.Tables["BudgetYear"].Select( String.Format( "CostCode='{0}'",costCode ) ))
					dr.Delete();

				foreach ( DataRow dr in budget.Tables["BudgetCost"].Select( String.Format( "CostCode='{0}'",costCode ) ))
					dr.Delete();

				Session["BudgetApplyEntityData"] = budget;
				budget.Dispose();
				
				ClearItemData();
				LoadGrid();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
			}
		}

		protected void btnContinue_ServerClick(object sender, System.EventArgs e)
		{
		
		}


		

	}
}
