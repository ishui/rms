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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// BudgetModifyCheck 的摘要说明。
	/// </summary>
	public partial class BudgetModifyCheck : PageBase
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
			this.lblCheckDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			this.lblCheckPerson.Text = base.user.UserName;
		}
		
		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				string budgetCode = Request["BudgetCode"] + "";
				EntityData budgetData = DAL.EntityDAO.CBSDAO.GetBudgetByCode(budgetCode);
				int flag = budgetData.GetInt("Flag");
				if ( flag == 1 )
					this.trView.Visible = false;
				else
				{
					this.trModify.Visible = false;
					this.lblOpinion.Text = budgetData.GetString("CheckOpinion");
					this.lblCheckDate0.Text = budgetData.GetDateTimeOnlyDate("CheckDate");
					this.lblCheckPerson0.Text = BLL.SystemRule.GetUserName( budgetData.GetString("CheckPerson"));
					this.lblInsureDate.Text = budgetData.GetDateTimeOnlyDate("InsureDate");
				}

				budgetData.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		
		//
//		private void CheckData()
//		{
//			try
//			{
//				// 取费用分解结构
//				EntityData cost = DAL.EntityDAO.CBSDAO.GetCBSByProject(base.ProjectCode);
//
//				// 取费用预算
//				string budgetCode = Request["BudgetCode"] + "";
//				EntityData budgetData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
//				budgetData.SetCurrentTable("BudgetCost");
//
//
//				int iCount = cost.CurrentTable.Rows.Count;
//				for ( int i=0;i<iCount; i++ )
//				{
//					string costCode = cost.GetString("CostCode");
//					string costName = cost.GetString("CostName");
//					string fullCode = cost.GetString("FullCode");
//					int deep = cost.GetInt("Deep");
//					int nextDeep = deep +1;
//
//
//					decimal budgetCost = decimal.Zero;
//					decimal moneySum = decimal.Zero;
//
//					//取费用预算金额
//					DataRow[] drs = budgetData.CurrentTable.Select( String.Format( " CostCode='{0}' " ,costCode )  );
//					if ( drs.Length>0)
//					{
//						DataRow dr = drs[0];
//						if ( !drs[0].IsNull("BudgetCost"))
//							budgetCost = (decimal) drs[0]["BudgetCost"];
//					}
//
//					//检查是否平衡
//					DataRow[] childRows = cost.CurrentTable.Select( String.Format( " FullCode like '{0}%' and Deep = {1} " , fullCode,nextDeep.ToString() )  );
//					int iChildCount = childRows.Length;
//					for ( int j=0; j<iChildCount; j++)
//					{
//						string codeTemp = (string) childRows[j]["CostCode"];
//						DataRow[] tempRows = budgetData.CurrentTable.Select(String.Format( " CostCode='{0}' " ,codeTemp ) );
//						if ( tempRows.Length > 0 )
//						{
//							if ( ! tempRows[0].IsNull("BudgetCost"))
//								moneySum += (decimal)tempRows[0]["BudgetCost"];
//						}
//
//					}
//
//					if ( BLL.MathRule.CheckDecimalEqual( decimal.Zero , moneySum ) || BLL.MathRule.CheckDecimalEqual( moneySum,budgetCost))
//					{}
//					else
//					{
//						this.lblMessage.Text = "发现不平衡的费用项：  " + costName; 
//						this.btnSave.Visible = false;
//						return;
//					}
//						
//				}
//			}
//			catch ( Exception ex )
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"");
//			}
//		}



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


		// 保存意见； 置标志位
		// 通时将原先的动态费用也标志为历史
		// 还有，原先等待审核的动态费用申请也要作废
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				// Flag : 0 生效， 1 修改中未审核； 2 历史记录； 3 作废
				string budgetCode = Request["BudgetCode"]+"";
				EntityData budgets = DAL.EntityDAO.CBSDAO.GetBudgetByProject( projectCode);

				// 置原先预算和动态的标志位
				foreach ( DataRow drOldBudget in budgets.CurrentTable.Select( "Flag=0 and IsDynamic in (0,1)" ))
					drOldBudget["Flag"] = 2;

				// 置新预算的标志位
				foreach ( DataRow drNewBudget in budgets.CurrentTable.Select ( String.Format("BudgetCode={0}",budgetCode) ) )
				{
					drNewBudget["Flag"] = 0;
					drNewBudget["CheckPerson"] = base.user.UserCode;
					drNewBudget["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");
					drNewBudget["CheckOpinion"] = this.txtOpinion.Value;
					drNewBudget["InsureDate"] = this.dtInsureDate.Value;
				}

				// 还有，原先等待审核的动态费用申请也要作废
				foreach ( DataRow drDynamicApply in budgets.CurrentTable.Select( "Flag=1 and IsDynamic=3 " ))
					drDynamicApply["Flag"] = 3;

				DAL.EntityDAO.CBSDAO.UpdateBudget(budgets);
				budgets.Dispose();


				// 复制新预算到一个新动态预算中。
				EntityData budgetData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
				EntityData newBudget = new EntityData("Standard_Budget");
				DataRow drBudget = budgetData.CurrentRow;				// 源行
				DataRow dr = newBudget.GetNewRecord();				// 目标行
				string newBudgetCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetCode");
				dr["BudgetCode"] = newBudgetCode ;
				dr["ReferBudgetCode"] = budgetCode;
				dr["IsDynamic"] = 1;
				dr["Flag"] = 0;
				dr["BudgetName"] = BLL.ProjectRule.GetProjectName(projectCode) + budgetData.GetInt("IYear").ToString()  + "年度动态费用" ;
				newBudget.AddNewRecord(dr);
				CopyMainTableData(drBudget,dr);

				CopyTableData( budgetData.Tables["BudgetCost"],newBudget.Tables["BudgetCost"],"BudgetCostCode" , newBudgetCode  );
				CopyTableData( budgetData.Tables["BudgetYear"],newBudget.Tables["BudgetYear"],"BudgetYearCode" , newBudgetCode  );
				CopyTableData( budgetData.Tables["BudgetMonth"],newBudget.Tables["BudgetMonth"],"BudgetMonthCode" , newBudgetCode );
				//CopyTableData( budgetData.Tables["BudgetYearName"],newBudget.Tables["BudgetYearName"],"BudgetYearNameCode" , newBudgetCode );

				DAL.EntityDAO.CBSDAO.SubmitAllStandard_Budget(newBudget);
				newBudget.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(JavaScript.Alert(false,"预算生效 ！"));
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
				Response.End();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void CopyMainTableData ( DataRow drSource, DataRow drTarget )
		{

			foreach ( DataColumn dc in drSource.Table.Columns)
			{
				if ( drTarget.Table.Columns.Contains(dc.ColumnName ) && ( dc.ColumnName != "BudgetCode"  )  &&  ( dc.ColumnName != "ReferBudgetCode" )
					&&  ( dc.ColumnName != "IsDynamic" ) &&  ( dc.ColumnName != "Flag" ) &&  ( dc.ColumnName != "BudgetName" ) )
				{
					drTarget[dc.ColumnName] = drSource[dc.ColumnName];
				}
			}
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

		protected void btnBlankout_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				// Flag : 0 生效， 1 修改中未审核； 2 历史记录； 3 作废
				string budgetCode = Request["BudgetCode"]+"";
				EntityData budgets = DAL.EntityDAO.CBSDAO.GetBudgetByProject( projectCode);

				// 置新预算的标志位
				foreach ( DataRow drNewBudget in budgets.CurrentTable.Select ( String.Format("BudgetCode={0}",budgetCode) ) )
				{
					drNewBudget["Flag"] = 3;
					drNewBudget["CheckPerson"] = base.user.UserCode;
					drNewBudget["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");
					drNewBudget["CheckOpinion"] = this.txtOpinion.Value;
				}

				DAL.EntityDAO.CBSDAO.UpdateBudget(budgets);
				budgets.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
				Response.End();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


	}
}
