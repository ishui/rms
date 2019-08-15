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
using RmsPM.BLL;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// 选择预算年度 的摘要说明。
	/// 检查预算列表，看看是否已经有一个新预算，有就进入到该预算
	/// </summary>
	public partial class SelectBudgetYear : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				int periodMonth = 12;
				int totalPeriods = 0;
				int currentPeriodIndex = 0;
				int periodIndex = 1;

				// 取项目的计划定义

				EntityData planData = DAL.EntityDAO.SystemManageDAO.GetPeriodDefineByProjectCode(projectCode);
				if (! planData.HasRecord())
				{
					planData.Dispose();
					Response.Write(Rms.Web.JavaScript.ScriptStart);
					Response.Write(Rms.Web.JavaScript.Alert(false,"请先在系统管理中定义项目的计划 ！"));
					Response.Write(Rms.Web.JavaScript.WinClose(false));
					Response.Write(Rms.Web.JavaScript.ScriptEnd);
					Response.End();
				}

				// 取项目定义
				ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
				sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , projectCode ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
                				
				DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='PeriodMonth'" ));
				if ( drSelects.Length>0)
				{
					if ( !drSelects[0].IsNull("ConfigData"))
						periodMonth = int.Parse((string)drSelects[0]["ConfigData"]);
				}
				projectConfig.Dispose();

				drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='TotalPeriods'" ));
				if ( drSelects.Length>0)
				{
					if ( !drSelects[0].IsNull("ConfigData"))
						totalPeriods = int.Parse((string)drSelects[0]["ConfigData"]);
				}
				projectConfig.Dispose();
				this.txtTotalPeriods.Value = totalPeriods.ToString();
				this.txtPeriodMonth.Value = periodMonth.ToString();


				// 检查所有预算，如果最新的预算是没有审核过的新预算, 就跳到这个预算
				// 否则计算下一个预算Index，进入这个预算中
				BudgetStrategyBuilder sb0 = new BudgetStrategyBuilder();
				sb0.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode ) );
				sb0.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic,"0" ) );
				sb0.AddStrategy( new Strategy( BudgetStrategyName.Flag,"0,1,2" ) );
				sb0.AddOrder( "MakeDate",false );
				sql = sb0.BuildMainQueryString();
				EntityData budget = qa.FillEntityData( "Budget",sql );
				qa.Dispose();

				int iCurrentYear = DateTime.Now.Year;
				if ( budget.HasRecord())
				{
					int flag = budget.GetInt( "Flag" );
					currentPeriodIndex = budget.GetInt("PeriodIndex");
					
					int iYear = budget.GetInt("IYear");
					string budgetCode = budget.GetString("BudgetCode");
					budget.Dispose();
					if ( flag == 1 )
					{
						Response.Write( Rms.Web.JavaScript.ScriptStart );
						Response.Write( "window.opener.navigate( '../Cost/Budget.aspx?IsDynamic=0&BudgetCode='"+ budgetCode +" );" );
						Response.Write( "window.close();" );
						Response.Write( Rms.Web.JavaScript.ScriptEnd );
						return;
					}
				}
				periodIndex = currentPeriodIndex + 1;
				this.txtPeriodIndex.Value = periodIndex.ToString();

				DataRow[] drsPlan = planData.CurrentTable.Select( String.Format( " PeriodIndex={0} ",periodIndex ) );
				if ( drsPlan.Length == 0)
				{
					planData.Dispose();
					Response.Write(Rms.Web.JavaScript.ScriptStart);
					Response.Write(Rms.Web.JavaScript.Alert(false,"请先在系统管理中定义项目的计划 ！"));
					Response.Write(Rms.Web.JavaScript.WinClose(false));
					Response.Write(Rms.Web.JavaScript.ScriptEnd);
					Response.End();
				}

				DateTime dtStart = (DateTime)drsPlan[0]["StartDate"];
				DateTime dtEnd = (DateTime)drsPlan[0]["EndDate"];
				this.txtStartDate.Value = dtStart.ToString("yyyy-MM-dd");
				this.txtEndDate.Value = dtEnd.ToString("yyyy-MM-dd");
				this.txtbudgetName.Value = dtStart.Year.ToString() + "年 年度预算";

				budget.Dispose();
				planData.Dispose();


			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"" );
			}
		}

		private void IniPage()
		{
			try
			{
				string projectCode = Request["ProjectCode"] + "";
				this.lblMakeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.lblMakePersonName.Text = base.user.UserName;
			}
			catch(Exception ex)
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


		// 保存这个空预算
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				EntityData newBudget = new EntityData("Budget");
				DataRow dr = newBudget.GetNewRecord();

				string budgetCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BudgetCode");
				dr["BudgetCode"] = budgetCode ;
				dr["BudgetName"] = this.txtbudgetName.Value ;
				dr["PeriodMonth"] = int.Parse(this.txtPeriodMonth.Value );
				int totalPeriods = int.Parse( this.txtTotalPeriods.Value);
				int periodIndex = int.Parse(this.txtPeriodIndex.Value );
				dr["PeriodIndex"] = periodIndex;
				dr["AfterPeriod"] = totalPeriods - periodIndex;
				dr["ProjectCode"] = projectCode;
				DateTime startDate = DateTime.Parse(this.txtStartDate.Value);
				dr["IYear"] = startDate.Year;
				dr["IMonth"] = startDate.Month;
				dr["IsDynamic"] = 0;
				dr["Flag"] = 1;
				dr["MakePerson"] = base.user.UserCode;
				dr["MakeDate"] = DateTime.Now.Date;
				dr["Remark"] = this.txtRemark.Value;
				dr["StartDate"]=startDate;
				dr["EndDate"]=DateTime.Parse(this.txtEndDate.Value);
				newBudget.AddNewRecord(dr);
				DAL.EntityDAO.CBSDAO.InsertBudget(newBudget);
				newBudget.Dispose();

				Response.Write( Rms.Web.JavaScript.ScriptStart );
				Response.Write( "window.opener.navigate( '../Cost/Budget.aspx?IsDynamic=0&ProjectCode="+projectCode+"&BudgetCode="+ budgetCode +"' );" );
				Response.Write( "window.close();" );
				Response.Write( Rms.Web.JavaScript.ScriptEnd );


			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}
	
	}
}
