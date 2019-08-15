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
using Rms.Web;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// 动态费用申请 的摘要说明。
	/// </summary>
	public partial class DynamicApplyInfo : PageBase
	{

		private const int IMaxPeriod = 10;
		private const int IMaxMonth = 12;

		private const int IWan = 10000;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
				ContralRight();
			}
		}

		private void ContralRight()
		{
			if ( !user.HasRight("040403"))
				this.btnCheck.Visible = false;

			if ( !user.HasRight("040402"))
				this.btnModify.Visible = false;

			if ( !user.HasRight("040402"))
				this.btnDelete.Visible = false;

		}

		private void IniPage()
		{

		}

		private void LoadData()
		{
			string budgetCode = Request["BudgetCode"] + "" ;
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				// 取费用分解结构和估算费用
				EntityData allCost = BLL.CBSRule.GetCostEstimate(projectCode);

				EntityData budget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
				// 参考动态费用
				string refBudgetCode = budget.GetString("ReferBudgetCode");
				EntityData refBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(refBudgetCode);

				int iYear = budget.GetInt("IYear");
				int iMonth = budget.GetInt("IMonth");
				int periodMonth = budget.GetInt("PeriodMonth");
				int afterPeriod  = budget.GetInt("AfterPeriod");
				string startDate = iYear.ToString() + "-" + iMonth.ToString() + "-1";
				string lastDateLastPeriod = DateTime.Parse(startDate).AddDays(-1).ToString("yyyy-MM-dd");
				int dynamicStartMonth = budget.GetInt("IDynamicStartMonth");

				int flag = budget.GetInt("Flag");
				if ( flag != 1 )
				{
					this.btnModify.Visible = false;
					this.btnDelete.Visible = false;
					this.btnCheck.Visible = false;
				}

				// 动态从什么月份开始做
				this.lblBudgetName.Text = budget.GetString("BudgetName")  ;
				this.lblReason.Text = budget.GetString("Reason");
				this.lblApplyDate.Text = budget.GetDateTimeOnlyDate("MakeDate");
				this.lblApplyPersonName.Text = BLL.SystemRule.GetUserName(budget.GetString("MakePerson"));
				this.lblCheckDate.Text = budget.GetDateTimeOnlyDate("CheckDate");
				this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName(budget.GetString("CheckPerson"));

				this.repeatList.DataSource = new DataView( budget.Tables["BudgetCost"],"IsModify=1","",DataViewRowState.CurrentRows );
				this.repeatList.DataBind();
				budget.Dispose();
				refBudget.Dispose();
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



		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string budgetCode = Request["BudgetCode"] + "";
			if ( budgetCode == "" )
				return;
			try
			{
				EntityData budget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
				DAL.EntityDAO.CBSDAO.DeleteStandard_Budget( budget);
				budget.Dispose();

				Response.Write( Rms.Web.JavaScript.ScriptStart );
				Response.Write( Rms.Web.JavaScript.OpenerReload(false) );
				Response.Write( Rms.Web.JavaScript.WinClose(false ));
				Response.Write( Rms.Web.JavaScript.ScriptEnd );
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}



		protected void btnCheck_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			string budgetCode = Request["BudgetCode"] + "" ;

			try
			{

				// 取费用分解结构和估算费用
				EntityData allCost = BLL.CBSRule.GetCostEstimate(projectCode);

				EntityData budget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
				// 参考动态费用
				string refBudgetCode = budget.GetString("ReferBudgetCode");
				EntityData refBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(refBudgetCode);
				// 预算
				string yuBudgetCode = refBudget.GetString("ReferBudgetCode");
				EntityData yuBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(yuBudgetCode);

				// 动态费用，用来与申请相混合
				EntityData copyRefBudget = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(refBudgetCode);

				int iYear = budget.GetInt("IYear");
				int iMonth = budget.GetInt("IMonth");
				int periodMonth = budget.GetInt("PeriodMonth");
				int afterPeriod  = budget.GetInt("AfterPeriod");
				int dynamicStartMonth = budget.GetInt("IDynamicStartMonth");
				string startDate = iYear.ToString() + "-" + iMonth.ToString() + "-1";
				string lastDateLastPeriod = DateTime.Parse(startDate).AddDays(-1).ToString("yyyy-MM-dd");

				// 混合
				MixBudget( copyRefBudget, budget , refBudgetCode );
				// 从下级加到上级总计
				BLL.CBSRule.AdCostEstimate(allCost,copyRefBudget,iYear,iMonth,periodMonth,afterPeriod,refBudgetCode,lastDateLastPeriod,projectCode);
				// 更新主表总费用
				//BLL.CBSRule.SumTotalMoney(allCost,copyRefBudget);

				DataRow dr = budget.Tables["Budget"].Rows[0];
				dr["Flag"] = 2;		//审核通过就变成历史纪录
				dr["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");
				dr["CheckPerson"] = base.user.UserCode;
//				dr["CheckOpinion"] = this.txtOpinion.Value;

				// 将调整后的动态费用结果总数记录在动态费用申请上
				DataRow copydr = copyRefBudget.Tables["Budget"].Rows[0];
				dr["TotalMoney"] = copydr["TotalMoney"];
				dr["BeforeHappenCost"] = copydr["BeforeHappenCost"];
				dr["CurrentPlanCost"] = copydr["CurrentPlanCost"];
				dr["AfterPlanCost"] = copydr["AfterPlanCost"];

				//将调整申请中修改的项的父项也记录到动态费用申请中，以便保持当前状态，（因为下一次修改，这些父项又要改动）
				TurnBackToBudgetApply( allCost,budget,copyRefBudget,budgetCode);

				DAL.EntityDAO.CBSDAO.SubmitAllStandard_Budget(budget);
				DAL.EntityDAO.CBSDAO.SubmitAllStandard_Budget(copyRefBudget);

				budget.Dispose();
				refBudget.Dispose();
				allCost.Dispose();
				yuBudget.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(JavaScript.Alert(false,"动态费用修改生效 ！"));
				Response.Write( JavaScript.Reload(false) );
				Response.Write(JavaScript.ScriptEnd);
				Response.End();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void MixBudget( EntityData copyRefBudget, EntityData budget, string refBudgetCode  )
		{
			foreach ( DataRow dr in budget.Tables["BudgetCost"].Rows )
			{
				string costCode = (string)dr["CostCode"];
				DataRow[] drTemps = null;

				drTemps = copyRefBudget.Tables["BudgetCost"].Select(String.Format("CostCode='{0}'",costCode));
				int iCount = drTemps.Length;
				for ( int i=iCount-1;i>=0;i--)
					drTemps[i].Delete();

				drTemps = copyRefBudget.Tables["BudgetMonth"].Select(String.Format("CostCode='{0}'",costCode));
				iCount = drTemps.Length;
				for ( int i=iCount-1;i>=0;i--)
					drTemps[i].Delete();

				drTemps = copyRefBudget.Tables["BudgetYear"].Select(String.Format("CostCode='{0}'",costCode));
				iCount = drTemps.Length;
				for ( int i=iCount-1;i>=0;i--)
					drTemps[i].Delete();

			}

			CopyTableData( budget.Tables["BudgetCost"],copyRefBudget.Tables["BudgetCost"],"BudgetCostCode" , refBudgetCode  );
			CopyTableData( budget.Tables["BudgetYear"],copyRefBudget.Tables["BudgetYear"],"BudgetYearCode" , refBudgetCode  );
			CopyTableData( budget.Tables["BudgetMonth"],copyRefBudget.Tables["BudgetMonth"],"BudgetMonthCode" , refBudgetCode );

		}
			
		private void CopyTableData ( DataTable dtSource, DataTable dtTarget, string keyColumnName , string budgetCode )
		{
			foreach ( DataRow dr in dtSource.Rows)
			{
				DataRow newRow = dtTarget.NewRow();
				newRow[keyColumnName] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
				newRow["BudgetCode"] = budgetCode;
				foreach ( DataColumn dc in dtSource.Columns)
				{
					if ( dtTarget.Columns.Contains(dc.ColumnName )  &&  ( dc.ColumnName != keyColumnName ) && ( dc.ColumnName != "BudgetCode" )  )
						newRow[dc.ColumnName] = dr[dc.ColumnName];
				}
				dtTarget.Rows.Add(newRow);
			}

		}

		private void TurnBackToBudgetApply( EntityData allCost, EntityData budget, EntityData copyRefBudget , string budgetCode )
		{
			int iCount = budget.Tables["BudgetCost"].Rows.Count;
			for ( int i = 0;i<iCount;i++)
			{
				DataRow dr = budget.Tables["BudgetCost"].Rows[i];
				string costCode = (string)dr["CostCode"];
				dr["IsModify"] = 1;
				CopyData ( allCost, budget, copyRefBudget ,budgetCode , costCode );
			}
		}

		private void CopyData ( EntityData allCost, EntityData budget, EntityData copyRefBudget , string budgetCode ,string costCode )
		{
			DataRow dr=allCost.CurrentTable.Select( String.Format( "CostCode='{0}'" ,costCode ) )[0];
			string parentCode = (string)dr["ParentCode"];
			if ( parentCode == "" )
				return;

			// 有了就不用复制了
			if ( budget.Tables["BudgetCost"].Select(String.Format("CostCode='{0}'",parentCode)).Length>0)
				return;

			CopyTableDataRow(copyRefBudget.Tables["BudgetCost"], budget.Tables["BudgetCost"],"BudgetCostCode" , budgetCode , parentCode );
			CopyTableDataRow( copyRefBudget.Tables["BudgetYear"],budget.Tables["BudgetYear"],"BudgetYearCode" , budgetCode , parentCode );
			CopyTableDataRow( copyRefBudget.Tables["BudgetMonth"],budget.Tables["BudgetMonth"],"BudgetMonthCode" , budgetCode , parentCode );

			CopyData ( allCost, budget, copyRefBudget ,budgetCode , parentCode );

		}

		private void CopyTableDataRow ( DataTable dtSource, DataTable dtTarget, string keyColumnName , string budgetCode , string costCode )
		{

			foreach ( DataRow dr in dtSource.Select( String.Format("CostCode='{0}'",costCode),"",DataViewRowState.CurrentRows ))
			{
				DataRow newRow = dtTarget.NewRow();
				newRow[keyColumnName] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
				newRow["BudgetCode"] = budgetCode;
				foreach ( DataColumn dc in dtSource.Columns)
				{
					if ( dtTarget.Columns.Contains(dc.ColumnName )  &&  ( dc.ColumnName != keyColumnName ) && ( dc.ColumnName != "BudgetCode" )  )
						newRow[dc.ColumnName] = dr[dc.ColumnName];
				}
				dtTarget.Rows.Add(newRow);
			}

		}


	}


}
